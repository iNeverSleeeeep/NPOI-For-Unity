using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Formula;
using NPOI.SS.Formula.Udf;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.Util;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// High level representation of a SpreadsheetML workbook.  This is the first object most users
	/// will construct whether they are Reading or writing a workbook.  It is also the
	/// top level object for creating new sheets/etc.
	public class XSSFWorkbook : POIXMLDocument, IWorkbook
	{
		private static Regex COMMA_PATTERN = new Regex(",");

		/// Width of one character of the default font in pixels. Same for Calibry and Arial.
		public static float DEFAULT_CHARACTER_WIDTH = 7.0017f;

		/// Excel silently tRuncates long sheet names to 31 chars.
		/// This constant is used to ensure uniqueness in the first 31 chars
		private static int Max_SENSITIVE_SHEET_NAME_LEN = 31;

		/// Extended windows meta file 
		public static int PICTURE_TYPE_EMF = 2;

		/// Windows Meta File 
		public static int PICTURE_TYPE_WMF = 3;

		/// Mac PICT format 
		public static int PICTURE_TYPE_PICT = 4;

		/// JPEG format 
		public static int PICTURE_TYPE_JPEG = 5;

		/// PNG format 
		public static int PICTURE_TYPE_PNG = 6;

		/// Device independent bitmap 
		public static int PICTURE_TYPE_DIB = 7;

		/// Images formats supported by XSSF but not by HSSF
		public static int PICTURE_TYPE_GIF = 8;

		public static int PICTURE_TYPE_TIFF = 9;

		public static int PICTURE_TYPE_EPS = 10;

		public static int PICTURE_TYPE_BMP = 11;

		public static int PICTURE_TYPE_WPG = 12;

		/// The underlying XML bean
		private CT_Workbook workbook;

		/// this holds the XSSFSheet objects attached to this workbook
		private List<XSSFSheet> sheets;

		/// this holds the XSSFName objects attached to this workbook
		private List<XSSFName> namedRanges;

		/// shared string table - a cache of strings in this workbook
		private SharedStringsTable sharedStringSource;

		/// A collection of shared objects used for styling content,
		/// e.g. fonts, cell styles, colors, etc.
		private StylesTable stylesSource;

		private ThemesTable theme;

		/// The locator of user-defined functions.
		/// By default includes functions from the Excel Analysis Toolpack
		private IndexedUDFFinder _udfFinder = new IndexedUDFFinder(UDFFinder.DEFAULT);

		/// TODO
		private CalculationChain calcChain;

		/// A collection of custom XML mappings
		private MapInfo mapInfo;

		/// Used to keep track of the data formatter so that all
		/// CreateDataFormatter calls return the same one for a given
		/// book.  This ensures that updates from one places is visible
		/// someplace else.
		private XSSFDataFormat formatter;

		/// The policy to apply in the event of missing or
		///  blank cells when fetching from a row.
		/// See {@link NPOI.ss.usermodel.Row.MissingCellPolicy}
		private MissingCellPolicy _missingCellPolicy = MissingCellPolicy.RETURN_NULL_AND_BLANK;

		/// array of pictures for this workbook
		private List<XSSFPictureData> pictures;

		private static POILogger logger = POILogFactory.GetLogger(typeof(XSSFWorkbook));

		/// cached instance of XSSFCreationHelper for this workbook
		/// @see {@link #getCreationHelper()}
		private XSSFCreationHelper _creationHelper;

		private WorkbookDocument doc;

		/// Convenience method to Get the active sheet.  The active sheet is is the sheet
		/// which is currently displayed when the workbook is viewed in Excel.
		/// 'Selected' sheet(s) is a distinct concept.
		public int ActiveSheetIndex
		{
			get
			{
				return (int)workbook.bookViews.GetWorkbookViewArray(0).activeTab;
			}
		}

		/// Get the number of styles the workbook Contains
		///
		/// @return count of cell styles
		public short NumCellStyles
		{
			get
			{
				return (short)stylesSource.NumCellStyles;
			}
		}

		/// Get the number of fonts in the this workbook
		///
		/// @return number of fonts
		public short NumberOfFonts
		{
			get
			{
				return (short)stylesSource.GetFonts().Count;
			}
		}

		/// Get the number of named ranges in the this workbook
		///
		/// @return number of named ranges
		public int NumberOfNames
		{
			get
			{
				return namedRanges.Count;
			}
		}

		/// Get the number of worksheets in the this workbook
		///
		/// @return number of worksheets
		public int NumberOfSheets
		{
			get
			{
				return sheets.Count;
			}
		}

		/// Retrieves the current policy on what to do when
		///  Getting missing or blank cells from a row.
		/// The default is to return blank and null cells.
		///  {@link MissingCellPolicy}
		public MissingCellPolicy MissingCellPolicy
		{
			get
			{
				return _missingCellPolicy;
			}
			set
			{
				_missingCellPolicy = value;
			}
		}

		/// Gets the first tab that is displayed in the list of tabs in excel.
		///
		/// @return integer that Contains the index to the active sheet in this book view.
		public int FirstVisibleTab
		{
			get
			{
				CT_BookViews bookViews = workbook.bookViews;
				CT_BookView workbookViewArray = bookViews.GetWorkbookViewArray(0);
				return (int)workbookViewArray.activeTab;
			}
			set
			{
				CT_BookViews bookViews = workbook.bookViews;
				CT_BookView workbookViewArray = bookViews.GetWorkbookViewArray(0);
				workbookViewArray.activeTab = (uint)value;
			}
		}

		public bool IsHidden
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public ISheet this[int index]
		{
			get
			{
				return GetSheetAt(index);
			}
			set
			{
				if (sheets[index] != null)
				{
					sheets[index] = (XSSFSheet)value;
				}
				else
				{
					sheets.Insert(index, (XSSFSheet)value);
				}
			}
		}

		public int Count
		{
			get
			{
				return NumberOfSheets;
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// Create a new SpreadsheetML workbook.
		public XSSFWorkbook()
			: base(newPackage())
		{
			OnWorkbookCreate();
		}

		/// Constructs a XSSFWorkbook object given a OpenXML4J <code>Package</code> object,
		///  see <a href="http://poi.apache.org/oxml4j/">http://poi.apache.org/oxml4j/</a>.
		///
		/// Once you have finished working with the Workbook, you should close the package
		/// by calling pkg.close, to avoid leaving file handles open.
		///
		/// Creating a XSSFWorkbook from a file-backed OPC Package has a lower memory
		///  footprint than an InputStream backed one.
		///
		/// @param pkg the OpenXML4J <code>OPC Package</code> object.
		public XSSFWorkbook(OPCPackage pkg)
			: base(pkg)
		{
			Load(XSSFFactory.GetInstance());
		}

		/// Constructs a XSSFWorkbook object, by buffering the whole stream into memory
		///  and then opening an {@link OPCPackage} object for it.
		///
		/// Using an {@link InputStream} requires more memory than using a File, so
		///  if a {@link File} is available then you should instead do something like
		///   <pre><code>
		///       OPCPackage pkg = OPCPackage.open(path);
		///       XSSFWorkbook wb = new XSSFWorkbook(pkg);
		///       // work with the wb object
		///       ......
		///       pkg.close(); // gracefully closes the underlying zip file
		///   </code></pre>     
		public XSSFWorkbook(Stream is1)
			: base(PackageHelper.Open(is1))
		{
			Load(XSSFFactory.GetInstance());
		}

		[Obsolete]
		public XSSFWorkbook(string path)
			: this(POIXMLDocument.OpenPackage(path))
		{
		}

		internal override void OnDocumentRead()
		{
			try
			{
				XmlDocument xmlDoc = POIXMLDocumentPart.ConvertStreamToXml(GetPackagePart().GetInputStream());
				doc = WorkbookDocument.Parse(xmlDoc, POIXMLDocumentPart.NamespaceManager);
				workbook = doc.Workbook;
				Dictionary<string, XSSFSheet> dictionary = new Dictionary<string, XSSFSheet>();
				foreach (POIXMLDocumentPart relation in GetRelations())
				{
					if (relation is SharedStringsTable)
					{
						sharedStringSource = (SharedStringsTable)relation;
					}
					else if (relation is StylesTable)
					{
						stylesSource = (StylesTable)relation;
					}
					else if (relation is ThemesTable)
					{
						theme = (ThemesTable)relation;
					}
					else if (relation is CalculationChain)
					{
						calcChain = (CalculationChain)relation;
					}
					else if (relation is MapInfo)
					{
						mapInfo = (MapInfo)relation;
					}
					else if (relation is XSSFSheet)
					{
						dictionary.Add(relation.GetPackageRelationship().Id, (XSSFSheet)relation);
					}
				}
				if (stylesSource != null)
				{
					stylesSource.SetTheme(theme);
				}
				if (sharedStringSource == null)
				{
					sharedStringSource = (SharedStringsTable)CreateRelationship(XSSFRelation.SHARED_STRINGS, XSSFFactory.GetInstance());
				}
				sheets = new List<XSSFSheet>(dictionary.Count);
				foreach (CT_Sheet item in workbook.sheets.sheet)
				{
					XSSFSheet xSSFSheet = dictionary[item.id];
					if (xSSFSheet == null)
					{
						logger.Log(5, "Sheet with name " + item.name + " and r:id " + item.id + " was defined, but didn't exist in package, skipping");
					}
					else
					{
						xSSFSheet.sheet = item;
						xSSFSheet.OnDocumentRead();
						sheets.Add(xSSFSheet);
					}
				}
				namedRanges = new List<XSSFName>();
				if (workbook.IsSetDefinedNames())
				{
					foreach (CT_DefinedName item2 in workbook.definedNames.definedName)
					{
						namedRanges.Add(new XSSFName(item2, this));
					}
				}
			}
			catch (XmlException ex)
			{
				throw new POIXMLException(ex);
			}
		}

		/// Create a new CT_Workbook with all values Set to default
		private void OnWorkbookCreate()
		{
			doc = new WorkbookDocument();
			workbook = doc.Workbook;
			CT_WorkbookPr cT_WorkbookPr = workbook.AddNewWorkbookPr();
			cT_WorkbookPr.date1904 = false;
			CT_BookViews cT_BookViews = workbook.AddNewBookViews();
			CT_BookView cT_BookView = cT_BookViews.AddNewWorkbookView();
			cT_BookView.activeTab = 0u;
			workbook.AddNewSheets();
			ExtendedProperties extendedProperties = GetProperties().ExtendedProperties;
			CT_ExtendedProperties underlyingProperties = extendedProperties.GetUnderlyingProperties();
			underlyingProperties.Application = POIXMLDocument.DOCUMENT_CREATOR;
			underlyingProperties.DocSecurity = 0;
			underlyingProperties.DocSecuritySpecified = true;
			underlyingProperties.ScaleCrop = false;
			underlyingProperties.ScaleCropSpecified = true;
			underlyingProperties.LinksUpToDate = false;
			underlyingProperties.LinksUpToDateSpecified = true;
			underlyingProperties.HyperlinksChanged = false;
			underlyingProperties.HyperlinksChangedSpecified = true;
			underlyingProperties.SharedDoc = false;
			underlyingProperties.SharedDocSpecified = true;
			sharedStringSource = (SharedStringsTable)CreateRelationship(XSSFRelation.SHARED_STRINGS, XSSFFactory.GetInstance());
			stylesSource = (StylesTable)CreateRelationship(XSSFRelation.STYLES, XSSFFactory.GetInstance());
			namedRanges = new List<XSSFName>();
			sheets = new List<XSSFSheet>();
		}

		/// Create a new SpreadsheetML namespace and Setup the default minimal content
		protected static OPCPackage newPackage()
		{
			try
			{
				OPCPackage oPCPackage = OPCPackage.Create(new MemoryStream());
				PackagePartName packagePartName = PackagingUriHelper.CreatePartName(XSSFRelation.WORKBOOK.DefaultFileName);
				oPCPackage.AddRelationship(packagePartName, TargetMode.Internal, "http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument");
				oPCPackage.CreatePart(packagePartName, XSSFRelation.WORKBOOK.ContentType);
				oPCPackage.GetPackageProperties().SetCreatorProperty(POIXMLDocument.DOCUMENT_CREATOR);
				return oPCPackage;
			}
			catch (Exception ex)
			{
				throw new POIXMLException(ex);
			}
		}

		/// Return the underlying XML bean
		///
		/// @return the underlying CT_Workbook bean
		public CT_Workbook GetCTWorkbook()
		{
			return workbook;
		}

		/// Adds a picture to the workbook.
		///
		/// @param pictureData       The bytes of the picture
		/// @param format            The format of the picture.
		///
		/// @return the index to this picture (0 based), the Added picture can be obtained from {@link #getAllPictures()} .
		/// @see Workbook#PICTURE_TYPE_EMF
		/// @see Workbook#PICTURE_TYPE_WMF
		/// @see Workbook#PICTURE_TYPE_PICT
		/// @see Workbook#PICTURE_TYPE_JPEG
		/// @see Workbook#PICTURE_TYPE_PNG
		/// @see Workbook#PICTURE_TYPE_DIB
		/// @see #getAllPictures()
		public int AddPicture(byte[] pictureData, int format)
		{
			return AddPicture(pictureData, (PictureType)format);
		}

		/// Adds a picture to the workbook.
		///
		/// @param is                The sream to read image from
		/// @param format            The format of the picture.
		///
		/// @return the index to this picture (0 based), the Added picture can be obtained from {@link #getAllPictures()} .
		/// @see Workbook#PICTURE_TYPE_EMF
		/// @see Workbook#PICTURE_TYPE_WMF
		/// @see Workbook#PICTURE_TYPE_PICT
		/// @see Workbook#PICTURE_TYPE_JPEG
		/// @see Workbook#PICTURE_TYPE_PNG
		/// @see Workbook#PICTURE_TYPE_DIB
		/// @see #getAllPictures()
		public int AddPicture(Stream picStream, int format)
		{
			int num = GetAllPictures().Count + 1;
			XSSFPictureData xSSFPictureData = (XSSFPictureData)CreateRelationship(XSSFPictureData.RELATIONS[format], XSSFFactory.GetInstance(), num, true);
			Stream outputStream = xSSFPictureData.GetPackagePart().GetOutputStream();
			IOUtils.Copy(picStream, outputStream);
			outputStream.Close();
			pictures.Add(xSSFPictureData);
			return num - 1;
		}

		/// Create an XSSFSheet from an existing sheet in the XSSFWorkbook.
		///  The Cloned sheet is a deep copy of the original.
		///
		/// @return XSSFSheet representing the Cloned sheet.
		/// @throws ArgumentException if the sheet index in invalid
		/// @throws POIXMLException if there were errors when cloning
		public ISheet CloneSheet(int sheetNum)
		{
			ValidateSheetIndex(sheetNum);
			XSSFSheet xSSFSheet = sheets[sheetNum];
			return xSSFSheet.CopySheet(xSSFSheet.SheetName);
		}

		/// Create a new XSSFCellStyle and add it to the workbook's style table
		///
		/// @return the new XSSFCellStyle object
		public ICellStyle CreateCellStyle()
		{
			return stylesSource.CreateCellStyle();
		}

		/// Returns the instance of XSSFDataFormat for this workbook.
		///
		/// @return the XSSFDataFormat object
		/// @see NPOI.ss.usermodel.DataFormat
		public IDataFormat CreateDataFormat()
		{
			if (formatter == null)
			{
				formatter = new XSSFDataFormat(stylesSource);
			}
			return formatter;
		}

		/// Create a new Font and add it to the workbook's font table
		///
		/// @return new font object
		public IFont CreateFont()
		{
			XSSFFont xSSFFont = new XSSFFont();
			xSSFFont.RegisterTo(stylesSource);
			return xSSFFont;
		}

		public IName CreateName()
		{
			CT_DefinedName cT_DefinedName = new CT_DefinedName();
			cT_DefinedName.name = "";
			XSSFName xSSFName = new XSSFName(cT_DefinedName, this);
			namedRanges.Add(xSSFName);
			return xSSFName;
		}

		/// Create an XSSFSheet for this workbook, Adds it to the sheets and returns
		/// the high level representation.  Use this to create new sheets.
		///
		/// @return XSSFSheet representing the new sheet.
		public ISheet CreateSheet()
		{
			string text = "Sheet" + sheets.Count;
			int num = 0;
			while (GetSheet(text) != null)
			{
				text = "Sheet" + num;
				num++;
			}
			return CreateSheet(text);
		}

		/// Create a new sheet for this Workbook and return the high level representation.
		/// Use this to create new sheets.
		///
		/// <p>
		///     Note that Excel allows sheet names up to 31 chars in length but other applications
		///     (such as OpenOffice) allow more. Some versions of Excel crash with names longer than 31 chars,
		///     others - tRuncate such names to 31 character.
		/// </p>
		/// <p>
		///     POI's SpreadsheetAPI silently tRuncates the input argument to 31 characters.
		///     Example:
		///
		///     <pre><code>
		///     Sheet sheet = workbook.CreateSheet("My very long sheet name which is longer than 31 chars"); // will be tRuncated
		///     assert 31 == sheet.SheetName.Length;
		///     assert "My very long sheet name which i" == sheet.SheetName;
		///     </code></pre>
		/// </p>
		///
		/// Except the 31-character constraint, Excel applies some other rules:
		/// <p>
		/// Sheet name MUST be unique in the workbook and MUST NOT contain the any of the following characters:
		/// <ul>
		/// <li> 0x0000 </li>
		/// <li> 0x0003 </li>
		/// <li> colon (:) </li>
		/// <li> backslash (\) </li>
		/// <li> asterisk (*) </li>
		/// <li> question mark (?) </li>
		/// <li> forward slash (/) </li>
		/// <li> opening square bracket ([) </li>
		/// <li> closing square bracket (]) </li>
		/// </ul>
		/// The string MUST NOT begin or end with the single quote (') character.
		/// </p>
		///
		/// <p>
		/// See {@link org.apache.poi.ss.util.WorkbookUtil#createSafeSheetName(String nameProposal)}
		///      for a safe way to create valid names
		/// </p>
		/// @param sheetname  sheetname to set for the sheet.
		/// @return Sheet representing the new sheet.
		/// @throws IllegalArgumentException if the name is null or invalid
		///  or workbook already contains a sheet with this name
		/// @see org.apache.poi.ss.util.WorkbookUtil#createSafeSheetName(String nameProposal)
		public ISheet CreateSheet(string sheetname)
		{
			if (sheetname == null)
			{
				throw new ArgumentException("sheetName must not be null");
			}
			if (ContainsSheet(sheetname, sheets.Count))
			{
				throw new ArgumentException("The workbook already contains a sheet of this name");
			}
			if (sheetname.Length > 31)
			{
				sheetname = sheetname.Substring(0, 31);
			}
			WorkbookUtil.ValidateSheetName(sheetname);
			CT_Sheet cT_Sheet = AddSheet(sheetname);
			int num = 1;
			foreach (XSSFSheet sheet in sheets)
			{
				num = (int)Math.Max(sheet.sheet.sheetId + 1, num);
			}
			XSSFSheet xSSFSheet = (XSSFSheet)CreateRelationship(XSSFRelation.WORKSHEET, XSSFFactory.GetInstance(), num);
			xSSFSheet.sheet = cT_Sheet;
			cT_Sheet.id = xSSFSheet.GetPackageRelationship().Id;
			cT_Sheet.sheetId = (uint)num;
			if (sheets.Count == 0)
			{
				xSSFSheet.IsSelected = true;
			}
			sheets.Add(xSSFSheet);
			return xSSFSheet;
		}

		protected XSSFDialogsheet CreateDialogsheet(string sheetname, CT_Dialogsheet dialogsheet)
		{
			ISheet sheet = CreateSheet(sheetname);
			return new XSSFDialogsheet((XSSFSheet)sheet);
		}

		private CT_Sheet AddSheet(string sheetname)
		{
			CT_Sheet cT_Sheet = workbook.sheets.AddNewSheet();
			cT_Sheet.name = sheetname;
			return cT_Sheet;
		}

		/// Finds a font that matches the one with the supplied attributes
		public IFont FindFont(short boldWeight, short color, short fontHeight, string name, bool italic, bool strikeout, FontSuperScript typeOffset, FontUnderlineType underline)
		{
			return stylesSource.FindFont(boldWeight, color, fontHeight, name, italic, strikeout, typeOffset, underline);
		}

		/// Gets all pictures from the Workbook.
		///
		/// @return the list of pictures (a list of {@link XSSFPictureData} objects.)
		/// @see #AddPicture(byte[], int)
		public IList GetAllPictures()
		{
			if (pictures == null)
			{
				List<PackagePart> partsByName = base.Package.GetPartsByName(new Regex("/xl/media/.*?"));
				pictures = new List<XSSFPictureData>(partsByName.Count);
				foreach (PackagePart item in partsByName)
				{
					pictures.Add(new XSSFPictureData(item, null));
				}
			}
			return pictures;
		}

		/// Get the cell style object at the given index
		///
		/// @param idx  index within the Set of styles
		/// @return XSSFCellStyle object at the index
		public ICellStyle GetCellStyleAt(short idx)
		{
			return stylesSource.GetStyleAt(idx);
		}

		/// Get the font at the given index number
		///
		/// @param idx  index number
		/// @return XSSFFont at the index
		public IFont GetFontAt(short idx)
		{
			return stylesSource.GetFontAt(idx);
		}

		public IName GetName(string name)
		{
			int nameIndex = GetNameIndex(name);
			if (nameIndex < 0)
			{
				return null;
			}
			return namedRanges[nameIndex];
		}

		public IName GetNameAt(int nameIndex)
		{
			int count = namedRanges.Count;
			if (count < 1)
			{
				throw new InvalidOperationException("There are no defined names in this workbook");
			}
			if (nameIndex < 0 || nameIndex > count)
			{
				throw new ArgumentException("Specified name index " + nameIndex + " is outside the allowable range (0.." + (count - 1) + ").");
			}
			return namedRanges[nameIndex];
		}

		/// Gets the named range index by his name
		/// <i>Note:</i>Excel named ranges are case-insensitive and
		/// this method performs a case-insensitive search.
		///
		/// @param name named range name
		/// @return named range index
		public int GetNameIndex(string name)
		{
			int num = 0;
			foreach (XSSFName namedRange in namedRanges)
			{
				if (namedRange.NameName.Equals(name))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		/// Retrieves the reference for the printarea of the specified sheet, the sheet name is Appended to the reference even if it was not specified.
		/// @param sheetIndex Zero-based sheet index (0 Represents the first sheet to keep consistent with java)
		/// @return String Null if no print area has been defined
		public string GetPrintArea(int sheetIndex)
		{
			XSSFName builtInName = GetBuiltInName(XSSFName.BUILTIN_PRINT_AREA, sheetIndex);
			if (builtInName == null)
			{
				return null;
			}
			return builtInName.RefersToFormula;
		}

		/// Get sheet with the given name (case insensitive match)
		///
		/// @param name of the sheet
		/// @return XSSFSheet with the name provided or <code>null</code> if it does not exist
		public ISheet GetSheet(string name)
		{
			foreach (XSSFSheet sheet in sheets)
			{
				if (name.Equals(sheet.SheetName, StringComparison.InvariantCultureIgnoreCase))
				{
					return sheet;
				}
			}
			return null;
		}

		public ISheet GetSheetAt(int index)
		{
			ValidateSheetIndex(index);
			return sheets[index];
		}

		/// <summary>
		/// Returns the index of the sheet by his name (case insensitive match)
		/// </summary>
		/// <param name="name">the sheet name</param>
		/// <returns>index of the sheet (0 based) or -1 if not found</returns>
		public int GetSheetIndex(string name)
		{
			for (int i = 0; i < sheets.Count; i++)
			{
				XSSFSheet xSSFSheet = sheets[i];
				if (name.Equals(xSSFSheet.SheetName, StringComparison.InvariantCultureIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}

		/// Returns the index of the given sheet
		///
		/// @param sheet the sheet to look up
		/// @return index of the sheet (0 based). <tt>-1</tt> if not found
		public int GetSheetIndex(ISheet sheet)
		{
			int num = 0;
			foreach (XSSFSheet sheet2 in sheets)
			{
				if (sheet2 == sheet)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		/// Get the sheet name
		///
		/// @param sheetIx Number
		/// @return Sheet name
		public string GetSheetName(int sheetIx)
		{
			ValidateSheetIndex(sheetIx);
			return sheets[sheetIx].SheetName;
		}

		/// Allows foreach loops:
		/// <pre><code>
		/// XSSFWorkbook wb = new XSSFWorkbook(package);
		/// for(XSSFSheet sheet : wb){
		///
		/// }
		/// </code></pre>
		public IEnumerator GetEnumerator()
		{
			return sheets.GetEnumerator();
		}

		/// Are we a normal workbook (.xlsx), or a
		///  macro enabled workbook (.xlsm)?
		public bool IsMacroEnabled()
		{
			return GetPackagePart().ContentType.Equals(XSSFRelation.MACROS_WORKBOOK.ContentType);
		}

		public void RemoveName(int nameIndex)
		{
			namedRanges.RemoveAt(nameIndex);
		}

		public void RemoveName(string name)
		{
			for (int i = 0; i < namedRanges.Count; i++)
			{
				XSSFName xSSFName = namedRanges[i];
				if (xSSFName.NameName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					RemoveName(i);
					return;
				}
			}
			throw new ArgumentException("Named range was not found: " + name);
		}

		/// As {@link #removeName(String)} is not necessarily unique 
		/// (name + sheet index is unique), this method is more accurate.
		///
		/// @param name the name to remove.
		public void RemoveName(XSSFName name)
		{
			if (!namedRanges.Remove(name))
			{
				throw new ArgumentException("Name was not found: " + name);
			}
		}

		/// Delete the printarea for the sheet specified
		///
		/// @param sheetIndex 0-based sheet index (0 = First Sheet)
		public void RemovePrintArea(int sheetIndex)
		{
			int num = 0;
			foreach (XSSFName namedRange in namedRanges)
			{
				if (namedRange.NameName.Equals(XSSFName.BUILTIN_PRINT_AREA) && namedRange.SheetIndex == sheetIndex)
				{
					namedRanges.RemoveAt(num);
					break;
				}
				num++;
			}
		}

		/// Removes sheet at the given index.<p />
		///
		/// Care must be taken if the Removed sheet is the currently active or only selected sheet in
		/// the workbook. There are a few situations when Excel must have a selection and/or active
		/// sheet. (For example when printing - see Bug 40414).<br />
		///
		/// This method Makes sure that if the Removed sheet was active, another sheet will become
		/// active in its place.  Furthermore, if the Removed sheet was the only selected sheet, another
		/// sheet will become selected.  The newly active/selected sheet will have the same index, or
		/// one less if the Removed sheet was the last in the workbook.
		///
		/// @param index of the sheet  (0-based)
		public void RemoveSheetAt(int index)
		{
			ValidateSheetIndex(index);
			bool flag = false;
			if (ActiveSheetIndex > index)
			{
				flag = true;
			}
			OnSheetDelete(index);
			XSSFSheet part = (XSSFSheet)GetSheetAt(index);
			RemoveRelation(part);
			sheets.RemoveAt(index);
			if (flag)
			{
				SetActiveSheet(0);
			}
		}

		/// Gracefully remove references to the sheet being deleted
		///
		/// @param index the 0-based index of the sheet to delete
		private void OnSheetDelete(int index)
		{
			workbook.sheets.RemoveSheet(index);
			if (calcChain != null)
			{
				RemoveRelation(calcChain);
				calcChain = null;
			}
			List<XSSFName> list = new List<XSSFName>();
			foreach (XSSFName namedRange in namedRanges)
			{
				CT_DefinedName cTName = namedRange.GetCTName();
				if (cTName.IsSetLocalSheetId())
				{
					if (cTName.localSheetId == index)
					{
						list.Add(namedRange);
					}
					else if (cTName.localSheetId > index)
					{
						cTName.localSheetId--;
						cTName.localSheetIdSpecified = true;
					}
				}
			}
			foreach (XSSFName item in list)
			{
				namedRanges.Remove(item);
			}
		}

		/// Validate sheet index
		///
		/// @param index the index to validate
		/// @throws ArgumentException if the index is out of range (index
		///            &lt; 0 || index &gt;= NumberOfSheets).
		private void ValidateSheetIndex(int index)
		{
			int num = sheets.Count - 1;
			if (index < 0 || index > num)
			{
				throw new ArgumentException("Sheet index (" + index + ") is out of range (0.." + num + ")");
			}
		}

		public void SetPrintArea(int sheetIndex, string reference)
		{
			XSSFName xSSFName = GetBuiltInName(XSSFName.BUILTIN_PRINT_AREA, sheetIndex);
			if (xSSFName == null)
			{
				xSSFName = CreateBuiltInName(XSSFName.BUILTIN_PRINT_AREA, sheetIndex);
			}
			string[] array = COMMA_PATTERN.Split(reference);
			StringBuilder stringBuilder = new StringBuilder(32);
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				SheetNameFormatter.AppendFormat(stringBuilder, GetSheetName(sheetIndex));
				stringBuilder.Append("!");
				stringBuilder.Append(array[i]);
			}
			xSSFName.RefersToFormula = stringBuilder.ToString();
		}

		/// For the Convenience of Java Programmers maintaining pointers.
		/// @see #setPrintArea(int, String)
		/// @param sheetIndex Zero-based sheet index (0 = First Sheet)
		/// @param startColumn Column to begin printarea
		/// @param endColumn Column to end the printarea
		/// @param startRow Row to begin the printarea
		/// @param endRow Row to end the printarea
		public void SetPrintArea(int sheetIndex, int startColumn, int endColumn, int startRow, int endRow)
		{
			string referencePrintArea = GetReferencePrintArea(GetSheetName(sheetIndex), startColumn, endColumn, startRow, endRow);
			SetPrintArea(sheetIndex, referencePrintArea);
		}

		/// Sets the repeating rows and columns for a sheet.
		/// <p />
		/// To Set just repeating columns:
		/// <pre>
		///  workbook.SetRepeatingRowsAndColumns(0,0,1,-1,-1);
		/// </pre>
		/// To Set just repeating rows:
		/// <pre>
		///  workbook.SetRepeatingRowsAndColumns(0,-1,-1,0,4);
		/// </pre>
		/// To remove all repeating rows and columns for a sheet.
		/// <pre>
		///  workbook.SetRepeatingRowsAndColumns(0,-1,-1,-1,-1);
		/// </pre>
		///
		/// @param sheetIndex  0 based index to sheet.
		/// @param startColumn 0 based start of repeating columns.
		/// @param endColumn   0 based end of repeating columns.
		/// @param startRow    0 based start of repeating rows.
		/// @param endRow      0 based end of repeating rows.
		[Obsolete("use XSSFSheet#setRepeatingRows(CellRangeAddress) or XSSFSheet#setRepeatingColumns(CellRangeAddress)")]
		public void SetRepeatingRowsAndColumns(int sheetIndex, int startColumn, int endColumn, int startRow, int endRow)
		{
			XSSFSheet xSSFSheet = (XSSFSheet)GetSheetAt(sheetIndex);
			CellRangeAddress repeatingRows = null;
			CellRangeAddress repeatingColumns = null;
			if (startRow != -1)
			{
				repeatingRows = new CellRangeAddress(startRow, endRow, -1, -1);
			}
			if (startColumn != -1)
			{
				repeatingColumns = new CellRangeAddress(-1, -1, startColumn, endColumn);
			}
			xSSFSheet.RepeatingRows = repeatingRows;
			xSSFSheet.RepeatingColumns = repeatingColumns;
		}

		private static string GetReferenceBuiltInRecord(string sheetName, int startC, int endC, int startR, int endR)
		{
			CellReference cellReference = new CellReference(sheetName, 0, startC, true, true);
			CellReference cellReference2 = new CellReference(sheetName, 0, endC, true, true);
			string text = SheetNameFormatter.Format(sheetName);
			string value = (startC != -1 || endC != -1) ? (text + "!$" + cellReference.CellRefParts[2] + ":$" + cellReference2.CellRefParts[2]) : "";
			CellReference cellReference3 = new CellReference(sheetName, startR, 0, true, true);
			CellReference cellReference4 = new CellReference(sheetName, endR, 0, true, true);
			string text2 = "";
			if (startR == -1 && endR == -1)
			{
				text2 = "";
			}
			else if (!cellReference3.CellRefParts[1].Equals("0") && !cellReference4.CellRefParts[1].Equals("0"))
			{
				text2 = text + "!$" + cellReference3.CellRefParts[1] + ":$" + cellReference4.CellRefParts[1];
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			if (stringBuilder.Length > 0 && text2.Length > 0)
			{
				stringBuilder.Append(',');
			}
			stringBuilder.Append(text2);
			return stringBuilder.ToString();
		}

		private static string GetReferencePrintArea(string sheetName, int startC, int endC, int startR, int endR)
		{
			CellReference cellReference = new CellReference(sheetName, startR, startC, true, true);
			CellReference cellReference2 = new CellReference(sheetName, endR, endC, true, true);
			return "$" + cellReference.CellRefParts[2] + "$" + cellReference.CellRefParts[1] + ":$" + cellReference2.CellRefParts[2] + "$" + cellReference2.CellRefParts[1];
		}

		public XSSFName GetBuiltInName(string builtInCode, int sheetNumber)
		{
			foreach (XSSFName namedRange in namedRanges)
			{
				if (namedRange.NameName.Equals(builtInCode, StringComparison.InvariantCultureIgnoreCase) && namedRange.SheetIndex == sheetNumber)
				{
					return namedRange;
				}
			}
			return null;
		}

		/// Generates a NameRecord to represent a built-in region
		///
		/// @return a new NameRecord
		/// @throws ArgumentException if sheetNumber is invalid
		/// @throws POIXMLException if such a name already exists in the workbook
		internal XSSFName CreateBuiltInName(string builtInName, int sheetNumber)
		{
			ValidateSheetIndex(sheetNumber);
			CT_DefinedNames cT_DefinedNames = (workbook.definedNames == null) ? workbook.AddNewDefinedNames() : workbook.definedNames;
			CT_DefinedName cT_DefinedName = cT_DefinedNames.AddNewDefinedName();
			cT_DefinedName.name = builtInName;
			cT_DefinedName.localSheetId = (uint)sheetNumber;
			cT_DefinedName.localSheetIdSpecified = true;
			XSSFName xSSFName = new XSSFName(cT_DefinedName, this);
			foreach (XSSFName namedRange in namedRanges)
			{
				if (namedRange.Equals(xSSFName))
				{
					throw new POIXMLException("Builtin (" + builtInName + ") already exists for sheet (" + sheetNumber + ")");
				}
			}
			namedRanges.Add(xSSFName);
			return xSSFName;
		}

		/// We only Set one sheet as selected for compatibility with HSSF.
		public void SetSelectedTab(int index)
		{
			for (int i = 0; i < sheets.Count; i++)
			{
				XSSFSheet xSSFSheet = sheets[i];
				xSSFSheet.IsSelected = (i == index);
			}
		}

		/// Set the sheet name.
		///
		/// @param sheetIndex sheet number (0 based)
		/// @param sheetname  the new sheet name
		/// @throws ArgumentException if the name is null or invalid
		///  or workbook already Contains a sheet with this name
		/// @see {@link #CreateSheet(String)}
		/// @see {@link NPOI.ss.util.WorkbookUtil#CreateSafeSheetName(String nameProposal)}
		///      for a safe way to create valid names
		public void SetSheetName(int sheetIndex, string sheetname)
		{
			ValidateSheetIndex(sheetIndex);
			if (sheetname != null && sheetname.Length > 31)
			{
				sheetname = sheetname.Substring(0, 31);
			}
			WorkbookUtil.ValidateSheetName(sheetname);
			if (ContainsSheet(sheetname, sheetIndex))
			{
				throw new ArgumentException("The workbook already contains a sheet of this name");
			}
			XSSFFormulaUtils xSSFFormulaUtils = new XSSFFormulaUtils(this);
			xSSFFormulaUtils.UpdateSheetName(sheetIndex, sheetname);
			workbook.sheets.GetSheetArray(sheetIndex).name = sheetname;
		}

		/// Sets the order of appearance for a given sheet.
		///
		/// @param sheetname the name of the sheet to reorder
		/// @param pos the position that we want to insert the sheet into (0 based)
		public void SetSheetOrder(string sheetname, int pos)
		{
			int sheetIndex = GetSheetIndex(sheetname);
			XSSFSheet item = sheets[sheetIndex];
			sheets.RemoveAt(sheetIndex);
			sheets.Insert(pos, item);
			CT_Sheets cT_Sheets = workbook.sheets;
			CT_Sheet sheet = cT_Sheets.GetSheetArray(sheetIndex).Copy();
			workbook.sheets.RemoveSheet(sheetIndex);
			CT_Sheet cT_Sheet = cT_Sheets.InsertNewSheet(pos);
			cT_Sheet.Set(sheet);
			for (int i = 0; i < sheets.Count; i++)
			{
				sheets[i].sheet = cT_Sheets.GetSheetArray(i);
			}
		}

		/// marshal named ranges from the {@link #namedRanges} collection to the underlying CT_Workbook bean
		private void SaveNamedRanges()
		{
			if (namedRanges.Count > 0)
			{
				CT_DefinedNames cT_DefinedNames = new CT_DefinedNames();
				List<CT_DefinedName> list = new List<CT_DefinedName>(namedRanges.Count);
				foreach (XSSFName namedRange in namedRanges)
				{
					list.Add(namedRange.GetCTName());
				}
				cT_DefinedNames.SetDefinedNameArray(list);
				workbook.SetDefinedNames(cT_DefinedNames);
			}
			else if (workbook.IsSetDefinedNames())
			{
				workbook.unsetDefinedNames();
			}
		}

		private void SaveCalculationChain()
		{
			if (calcChain != null && calcChain.GetCTCalcChain().SizeOfCArray() == 0)
			{
				RemoveRelation(calcChain);
				calcChain = null;
			}
		}

		protected override void Commit()
		{
			SaveNamedRanges();
			SaveCalculationChain();
			PackagePart packagePart = GetPackagePart();
			doc.Save(packagePart.GetOutputStream());
		}

		/// Returns SharedStringsTable - tha cache of string for this workbook
		///
		/// @return the shared string table
		public SharedStringsTable GetSharedStringSource()
		{
			return sharedStringSource;
		}

		/// Return a object representing a collection of shared objects used for styling content,
		/// e.g. fonts, cell styles, colors, etc.
		public StylesTable GetStylesSource()
		{
			return stylesSource;
		}

		/// Returns the Theme of current workbook.
		public ThemesTable GetTheme()
		{
			return theme;
		}

		/// Returns an object that handles instantiating concrete
		///  classes of the various instances for XSSF.
		public ICreationHelper GetCreationHelper()
		{
			if (_creationHelper == null)
			{
				_creationHelper = new XSSFCreationHelper(this);
			}
			return _creationHelper;
		}

		/// Determines whether a workbook Contains the provided sheet name.
		/// For the purpose of comparison, long names are tRuncated to 31 chars.
		///
		/// @param name the name to Test (case insensitive match)
		/// @param excludeSheetIdx the sheet to exclude from the check or -1 to include all sheets in the Check.
		/// @return true if the sheet Contains the name, false otherwise.
		private bool ContainsSheet(string name, int excludeSheetIdx)
		{
			List<CT_Sheet> sheet = workbook.sheets.sheet;
			if (name.Length > Max_SENSITIVE_SHEET_NAME_LEN)
			{
				name = name.Substring(0, Max_SENSITIVE_SHEET_NAME_LEN);
			}
			for (int i = 0; i < sheet.Count; i++)
			{
				string text = sheet[i].name;
				if (text.Length > Max_SENSITIVE_SHEET_NAME_LEN)
				{
					text = text.Substring(0, Max_SENSITIVE_SHEET_NAME_LEN);
				}
				if (excludeSheetIdx != i && name.Equals(text, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		/// Gets a bool value that indicates whether the date systems used in the workbook starts in 1904.
		/// <p>
		/// The default value is false, meaning that the workbook uses the 1900 date system,
		/// where 1/1/1900 is the first day in the system..
		/// </p>
		/// @return true if the date systems used in the workbook starts in 1904
		internal bool IsDate1904()
		{
			CT_WorkbookPr workbookPr = workbook.workbookPr;
			if (workbookPr.date1904Specified)
			{
				return workbookPr.date1904;
			}
			return false;
		}

		/// Get the document's embedded files.
		public override List<PackagePart> GetAllEmbedds()
		{
			List<PackagePart> list = new List<PackagePart>();
			foreach (XSSFSheet sheet in sheets)
			{
				foreach (PackageRelationship item in sheet.GetPackagePart().GetRelationshipsByType(XSSFRelation.OLEEMBEDDINGS.Relation))
				{
					list.Add(sheet.GetPackagePart().GetRelatedPart(item));
				}
				foreach (PackageRelationship item2 in sheet.GetPackagePart().GetRelationshipsByType(XSSFRelation.PACKEMBEDDINGS.Relation))
				{
					list.Add(sheet.GetPackagePart().GetRelatedPart(item2));
				}
			}
			return list;
		}

		/// Check whether a sheet is hidden.
		/// <p>
		/// Note that a sheet could instead be Set to be very hidden, which is different
		///  ({@link #isSheetVeryHidden(int)})
		/// </p>
		/// @param sheetIx Number
		/// @return <code>true</code> if sheet is hidden
		public bool IsSheetHidden(int sheetIx)
		{
			ValidateSheetIndex(sheetIx);
			CT_Sheet sheet = sheets[sheetIx].sheet;
			return sheet.state == ST_SheetState.hidden;
		}

		/// Check whether a sheet is very hidden.
		/// <p>
		/// This is different from the normal hidden status
		///  ({@link #isSheetHidden(int)})
		/// </p>
		/// @param sheetIx sheet index to check
		/// @return <code>true</code> if sheet is very hidden
		public bool IsSheetVeryHidden(int sheetIx)
		{
			ValidateSheetIndex(sheetIx);
			CT_Sheet sheet = sheets[sheetIx].sheet;
			return sheet.state == ST_SheetState.veryHidden;
		}

		/// Sets the visible state of this sheet.
		/// <p>
		///   Calling <code>setSheetHidden(sheetIndex, true)</code> is equivalent to
		///   <code>setSheetHidden(sheetIndex, Workbook.SHEET_STATE_HIDDEN)</code>.
		/// <br />
		///   Calling <code>setSheetHidden(sheetIndex, false)</code> is equivalent to
		///   <code>setSheetHidden(sheetIndex, Workbook.SHEET_STATE_VISIBLE)</code>.
		/// </p>
		///
		/// @param sheetIx   the 0-based index of the sheet
		/// @param hidden whether this sheet is hidden
		/// @see #setSheetHidden(int, int)
		public void SetSheetHidden(int sheetIx, bool hidden)
		{
			SetSheetHidden(sheetIx, hidden ? SheetState.Hidden : SheetState.Visible);
		}

		/// Hide or unhide a sheet.
		///
		/// <ul>
		///  <li>0 - visible. </li>
		///  <li>1 - hidden. </li>
		///  <li>2 - very hidden.</li>
		/// </ul>
		/// @param sheetIx the sheet index (0-based)
		/// @param state one of the following <code>Workbook</code> constants:
		///        <code>Workbook.SHEET_STATE_VISIBLE</code>,
		///        <code>Workbook.SHEET_STATE_HIDDEN</code>, or
		///        <code>Workbook.SHEET_STATE_VERY_HIDDEN</code>.
		/// @throws ArgumentException if the supplied sheet index or state is invalid
		public void SetSheetHidden(int sheetIx, SheetState state)
		{
			ValidateSheetIndex(sheetIx);
			WorkbookUtil.ValidateSheetState(state);
			CT_Sheet sheet = sheets[sheetIx].sheet;
			sheet.state = (ST_SheetState)state;
		}

		/// <summary>
		/// Hide or unhide a sheet.
		/// </summary>
		/// <param name="sheetIx">The sheet number</param>
		/// <param name="hidden">0 for not hidden, 1 for hidden, 2 for very hidden</param>
		public void SetSheetHidden(int sheetIx, int hidden)
		{
			ValidateSheetIndex(sheetIx);
			SetSheetHidden(sheetIx, (SheetState)hidden);
		}

		/// Fired when a formula is deleted from this workbook,
		/// for example when calling cell.SetCellFormula(null)
		///
		/// @see XSSFCell#setCellFormula(String)
		internal void OnDeleteFormula(XSSFCell cell)
		{
			if (calcChain != null)
			{
				int sheetId = (int)((XSSFSheet)cell.Sheet).sheet.sheetId;
				calcChain.RemoveItem(sheetId, cell.GetReference());
			}
		}

		/// Return the CalculationChain object for this workbook
		/// <p>
		///   The calculation chain object specifies the order in which the cells in a workbook were last calculated
		/// </p>
		///
		/// @return the <code>CalculationChain</code> object or <code>null</code> if not defined
		public CalculationChain GetCalculationChain()
		{
			return calcChain;
		}

		/// @return a collection of custom XML mappings defined in this workbook
		public List<XSSFMap> GetCustomXMLMappings()
		{
			if (mapInfo != null)
			{
				return mapInfo.GetAllXSSFMaps();
			}
			return new List<XSSFMap>();
		}

		/// @return the helper class used to query the custom XML mapping defined in this workbook
		public MapInfo GetMapInfo()
		{
			return mapInfo;
		}

		/// Specifies a bool value that indicates whether structure of workbook is locked. <br />
		/// A value true indicates the structure of the workbook is locked. Worksheets in the workbook can't be Moved,
		/// deleted, hidden, unhidden, or Renamed, and new worksheets can't be inserted.<br />
		/// A value of false indicates the structure of the workbook is not locked.<br />
		///
		/// @return true if structure of workbook is locked
		public bool IsStructureLocked()
		{
			if (WorkbookProtectionPresent())
			{
				return workbook.workbookProtection.lockStructure;
			}
			return false;
		}

		/// Specifies a bool value that indicates whether the windows that comprise the workbook are locked. <br />
		/// A value of true indicates the workbook windows are locked. Windows are the same size and position each time the
		/// workbook is opened.<br />
		/// A value of false indicates the workbook windows are not locked.
		///
		/// @return true if windows that comprise the workbook are locked
		public bool IsWindowsLocked()
		{
			if (WorkbookProtectionPresent())
			{
				return workbook.workbookProtection.lockWindows;
			}
			return false;
		}

		/// Specifies a bool value that indicates whether the workbook is locked for revisions.
		///
		/// @return true if the workbook is locked for revisions.
		public bool IsRevisionLocked()
		{
			if (WorkbookProtectionPresent())
			{
				return workbook.workbookProtection.lockRevision;
			}
			return false;
		}

		/// Locks the structure of workbook.
		public void LockStructure()
		{
			CreateProtectionFieldIfNotPresent();
			workbook.workbookProtection.lockStructure = true;
		}

		/// Unlocks the structure of workbook.
		public void UnlockStructure()
		{
			CreateProtectionFieldIfNotPresent();
			workbook.workbookProtection.lockStructure = false;
		}

		/// Locks the windows that comprise the workbook. 
		public void LockWindows()
		{
			CreateProtectionFieldIfNotPresent();
			workbook.workbookProtection.lockWindows = true;
		}

		/// Unlocks the windows that comprise the workbook. 
		public void UnlockWindows()
		{
			CreateProtectionFieldIfNotPresent();
			workbook.workbookProtection.lockWindows = false;
		}

		/// Locks the workbook for revisions.
		public void LockRevision()
		{
			CreateProtectionFieldIfNotPresent();
			workbook.workbookProtection.lockRevision = true;
		}

		/// Unlocks the workbook for revisions.
		public void UnlockRevision()
		{
			CreateProtectionFieldIfNotPresent();
			workbook.workbookProtection.lockRevision = false;
		}

		private bool WorkbookProtectionPresent()
		{
			return workbook.workbookProtection != null;
		}

		private void CreateProtectionFieldIfNotPresent()
		{
			if (workbook.workbookProtection == null)
			{
				workbook.workbookProtection = new CT_WorkbookProtection();
			}
		}

		/// Returns the locator of user-defined functions.
		/// <p>
		/// The default instance : the built-in functions with the Excel Analysis Tool Pack.
		/// To Set / Evaluate custom functions you need to register them as follows:
		///
		///
		///
		/// </p>
		/// @return wrapped instance of UDFFinder that allows seeking functions both by index and name
		internal UDFFinder GetUDFFinder()
		{
			return _udfFinder;
		}

		/// Register a new toolpack in this workbook.
		///
		/// @param toopack the toolpack to register
		public void AddToolPack(UDFFinder toopack)
		{
			_udfFinder.Add(toopack);
		}

		/// Whether the application shall perform a full recalculation when the workbook is opened.
		/// <p>
		/// Typically you want to force formula recalculation when you modify cell formulas or values
		/// of a workbook previously Created by Excel. When Set to true, this flag will tell Excel
		/// that it needs to recalculate all formulas in the workbook the next time the file is opened.
		/// </p>
		/// <p>
		/// Note, that recalculation updates cached formula results and, thus, modifies the workbook.
		/// Depending on the version, Excel may prompt you with "Do you want to save the Changes in <em>filename</em>?"
		/// on close.
		/// </p>
		///
		/// @param value true if the application will perform a full recalculation of
		/// workbook values when the workbook is opened
		/// @since 3.8
		public void SetForceFormulaRecalculation(bool value)
		{
			CT_Workbook cTWorkbook = GetCTWorkbook();
			CT_CalcPr cT_CalcPr = cTWorkbook.IsSetCalcPr() ? cTWorkbook.calcPr : cTWorkbook.AddNewCalcPr();
			cT_CalcPr.calcId = 0u;
			if (value && cT_CalcPr.calcMode == ST_CalcMode.manual)
			{
				cT_CalcPr.calcMode = ST_CalcMode.auto;
			}
		}

		/// Whether Excel will be asked to recalculate all formulas when the  workbook is opened.
		///
		/// @since 3.8
		public bool GetForceFormulaRecalculation()
		{
			CT_Workbook cTWorkbook = GetCTWorkbook();
			CT_CalcPr calcPr = cTWorkbook.calcPr;
			if (calcPr != null)
			{
				return calcPr.calcId != 0;
			}
			return false;
		}

		public void SetActiveSheet(int sheetIndex)
		{
			ValidateSheetIndex(sheetIndex);
			foreach (CT_BookView item in workbook.bookViews.workbookView)
			{
				item.activeTab = (uint)sheetIndex;
			}
		}

		public int AddPicture(byte[] pictureData, PictureType format)
		{
			int num = GetAllPictures().Count + 1;
			XSSFPictureData xSSFPictureData = (XSSFPictureData)CreateRelationship(XSSFPictureData.RELATIONS[(int)format], XSSFFactory.GetInstance(), num, true);
			try
			{
				Stream outputStream = xSSFPictureData.GetPackagePart().GetOutputStream();
				outputStream.Write(pictureData, 0, pictureData.Length);
				outputStream.Close();
			}
			catch (IOException ex)
			{
				throw new POIXMLException(ex);
			}
			pictures.Add(xSSFPictureData);
			return num - 1;
		}

		public int IndexOf(ISheet item)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, ISheet item)
		{
			sheets.Insert(index, (XSSFSheet)item);
		}

		public void RemoveAt(int index)
		{
			RemoveSheetAt(index);
		}

		public void Add(ISheet item)
		{
			sheets.Add((XSSFSheet)item);
		}

		public void Clear()
		{
			sheets.Clear();
		}

		public bool Contains(ISheet item)
		{
			throw new NotImplementedException();
		}

		public void CopyTo(ISheet[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		public bool Remove(ISheet item)
		{
			return sheets.Remove((XSSFSheet)item);
		}
	}
}
