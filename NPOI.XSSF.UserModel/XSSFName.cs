using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace NPOI.XSSF.UserModel
{
	/// Represents a defined named range in a SpreadsheetML workbook.
	/// <p>
	/// Defined names are descriptive text that is used to represents a cell, range of cells, formula, or constant value.
	/// Use easy-to-understand names, such as Products, to refer to hard to understand ranges, such as <code>Sales!C20:C30</code>.
	/// </p>
	/// Example:
	/// <pre><blockquote>
	///   XSSFWorkbook wb = new XSSFWorkbook();
	///   XSSFSheet sh = wb.CreateSheet("Sheet1");
	///
	///   //applies to the entire workbook
	///   XSSFName name1 = wb.CreateName();
	///   name1.SetNameName("FMLA");
	///   name1.SetRefersToFormula("Sheet1!$B$3");
	///
	///   //applies to Sheet1
	///   XSSFName name2 = wb.CreateName();
	///   name2.SetNameName("SheetLevelName");
	///   name2.SetComment("This name is scoped to Sheet1");
	///   name2.SetLocalSheetId(0);
	///   name2.SetRefersToFormula("Sheet1!$B$3");
	///
	/// </blockquote></pre>
	///
	/// @author Nick Burch
	/// @author Yegor Kozlov
	public class XSSFName : IName
	{
		/// A built-in defined name that specifies the workbook's print area
		public static string BUILTIN_PRINT_AREA = "_xlnm.Print_Area";

		/// A built-in defined name that specifies the row(s) or column(s) to repeat
		/// at the top of each printed page.
		public static string BUILTIN_PRINT_TITLE = "_xlnm.Print_Titles";

		/// A built-in defined name that refers to a range Containing the criteria values
		/// to be used in Applying an advanced filter to a range of data
		public static string BUILTIN_CRITERIA = "_xlnm.Criteria:";

		/// this defined name refers to the range Containing the filtered
		/// output values resulting from Applying an advanced filter criteria to a source
		/// range
		public static string BUILTIN_EXTRACT = "_xlnm.Extract:";

		public static string BUILTIN_FILTER_DB = "_xlnm._FilterDatabase";

		/// A built-in defined name that refers to a consolidation area
		public static string BUILTIN_CONSOLIDATE_AREA = "_xlnm.Consolidate_Area";

		/// A built-in defined name that specified that the range specified is from a database data source
		public static string BUILTIN_DATABASE = "_xlnm.Database";

		/// A built-in defined name that refers to a sheet title.
		public static string BUILTIN_SHEET_TITLE = "_xlnm.Sheet_Title";

		private XSSFWorkbook _workbook;

		private CT_DefinedName _ctName;

		/// Returns the name that will appear in the user interface for the defined name.
		///
		/// @return text name of this defined name
		public string NameName
		{
			get
			{
				return _ctName.name;
			}
			set
			{
				validateName(value);
				int sheetIndex = SheetIndex;
				for (int i = 0; i < _workbook.NumberOfNames; i++)
				{
					IName nameAt = _workbook.GetNameAt(i);
					if (nameAt != this && value.Equals(nameAt.NameName, StringComparison.InvariantCultureIgnoreCase) && sheetIndex == nameAt.SheetIndex)
					{
						string message = "The " + ((sheetIndex == -1) ? "workbook" : "sheet") + " already contains this name: " + value;
						throw new ArgumentException(message);
					}
				}
				_ctName.name = value;
			}
		}

		public string RefersToFormula
		{
			get
			{
				string value = _ctName.Value;
				if (value == null || value.Length < 1)
				{
					return null;
				}
				return value;
			}
			set
			{
				XSSFEvaluationWorkbook workbook = XSSFEvaluationWorkbook.Create(_workbook);
				FormulaParser.Parse(value, workbook, FormulaType.NamedRange, SheetIndex);
				_ctName.Value = value;
			}
		}

		public bool IsDeleted
		{
			get
			{
				string refersToFormula = RefersToFormula;
				if (refersToFormula == null)
				{
					return false;
				}
				XSSFEvaluationWorkbook workbook = XSSFEvaluationWorkbook.Create(_workbook);
				Ptg[] ptgs = FormulaParser.Parse(refersToFormula, workbook, FormulaType.NamedRange, SheetIndex);
				return Ptg.DoesFormulaReferToDeletedCell(ptgs);
			}
		}

		/// Returns the sheet index this name applies to.
		///
		/// @return the sheet index this name applies to, -1 if this name applies to the entire workbook
		public int SheetIndex
		{
			get
			{
				if (!_ctName.IsSetLocalSheetId())
				{
					return -1;
				}
				return (int)_ctName.localSheetId;
			}
			set
			{
				int num = _workbook.NumberOfSheets - 1;
				if (value < -1 || value > num)
				{
					throw new ArgumentException("Sheet index (" + value + ") is out of range" + ((num == -1) ? "" : (" (0.." + num + ")")));
				}
				if (value == -1)
				{
					if (_ctName.IsSetLocalSheetId())
					{
						_ctName.UnsetLocalSheetId();
					}
				}
				else
				{
					_ctName.localSheetId = (uint)value;
					_ctName.localSheetIdSpecified = true;
				}
			}
		}

		/// Indicates that the defined name refers to a user-defined function.
		/// This attribute is used when there is an Add-in or other code project associated with the file.
		///
		/// @return <code>true</code> indicates the name refers to a function.
		public bool Function
		{
			get
			{
				return _ctName.function;
			}
			set
			{
				_ctName.function = value;
			}
		}

		/// Returns the function group index if the defined name refers to a function. The function
		/// group defines the general category for the function. This attribute is used when there is
		/// an Add-in or other code project associated with the file.
		///
		/// @return the function group index that defines the general category for the function
		public int FunctionGroupId
		{
			get
			{
				return (int)_ctName.functionGroupId;
			}
			set
			{
				_ctName.functionGroupId = (uint)value;
			}
		}

		/// Get the sheets name which this named range is referenced to
		///
		/// @return sheet name, which this named range referred to.
		/// Empty string if the referenced sheet name weas not found.
		public string SheetName
		{
			get
			{
				if (_ctName.IsSetLocalSheetId())
				{
					int localSheetId = (int)_ctName.localSheetId;
					return _workbook.GetSheetName(localSheetId);
				}
				string refersToFormula = RefersToFormula;
				AreaReference areaReference = new AreaReference(refersToFormula);
				return areaReference.FirstCell.SheetName;
			}
		}

		/// Is the name refers to a user-defined function ?
		///
		/// @return <code>true</code> if this name refers to a user-defined function
		public bool IsFunctionName
		{
			get
			{
				return Function;
			}
		}

		/// Returns the comment the user provided when the name was Created.
		///
		/// @return the user comment for this named range
		public string Comment
		{
			get
			{
				return _ctName.comment;
			}
			set
			{
				_ctName.comment = value;
			}
		}

		/// Creates an XSSFName object - called internally by XSSFWorkbook.
		///
		/// @param name - the xml bean that holds data represenring this defined name.
		/// @param workbook - the workbook object associated with the name
		/// @see NPOI.XSSF.usermodel.XSSFWorkbook#CreateName()
		public XSSFName(CT_DefinedName name, XSSFWorkbook workbook)
		{
			_workbook = workbook;
			_ctName = name;
		}

		/// Returns the underlying named range object
		internal CT_DefinedName GetCTName()
		{
			return _ctName;
		}

		public void SetFunction(bool value)
		{
			Function = value;
		}

		public override int GetHashCode()
		{
			return _ctName.ToString().GetHashCode();
		}

		/// Compares this name to the specified object.
		/// The result is <code>true</code> if the argument is XSSFName and the
		/// underlying CTDefinedName bean Equals to the CTDefinedName representing this name
		///
		/// @param   o   the object to compare this <code>XSSFName</code> against.
		/// @return  <code>true</code> if the <code>XSSFName </code>are Equal;
		///          <code>false</code> otherwise.
		public override bool Equals(object o)
		{
			if (o == this)
			{
				return true;
			}
			if (!(o is XSSFName))
			{
				return false;
			}
			XSSFName xSSFName = (XSSFName)o;
			if (_ctName.name == xSSFName.GetCTName().name)
			{
				return _ctName.localSheetId == xSSFName.GetCTName().localSheetId;
			}
			return false;
		}

		private static void validateName(string name)
		{
			if (name.Length == 0)
			{
				throw new ArgumentException("Name cannot be blank");
			}
			char c = name[0];
			if ((c != '_' && !char.IsLetter(c)) || name.IndexOf(' ') != -1)
			{
				throw new ArgumentException("Invalid name: '" + name + "'; Names must begin with a letter or underscore and not contain spaces");
			}
		}
	}
}
