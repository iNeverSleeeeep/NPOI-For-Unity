using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace NPOI.XSSF.UserModel
{
	/// XSSF Implementation of a Hyperlink.
	/// Note - unlike with HSSF, many kinds of hyperlink
	/// are largely stored as relations of the sheet
	public class XSSFHyperlink : IHyperlink
	{
		private HyperlinkType _type;

		private PackageRelationship _externalRel;

		private CT_Hyperlink _ctHyperlink;

		private string _location;

		/// Return the type of this hyperlink
		///
		/// @return the type of this hyperlink
		public HyperlinkType Type
		{
			get
			{
				return _type;
			}
		}

		/// Hyperlink Address. Depending on the hyperlink type it can be URL, e-mail, path to a file
		///
		/// @return the Address of this hyperlink
		public string Address
		{
			get
			{
				return _location;
			}
			set
			{
				Validate(value);
				_location = value;
				if (_type == HyperlinkType.Document)
				{
					Location = value;
				}
			}
		}

		/// Return text label for this hyperlink
		///
		/// @return text to display
		public string Label
		{
			get
			{
				return _ctHyperlink.display;
			}
			set
			{
				_ctHyperlink.display = value;
			}
		}

		/// Location within target. If target is a workbook (or this workbook) this shall refer to a
		/// sheet and cell or a defined name. Can also be an HTML anchor if target is HTML file.
		///
		/// @return location
		public string Location
		{
			get
			{
				return _ctHyperlink.location;
			}
			set
			{
				_ctHyperlink.location = value;
			}
		}

		/// Return the column of the first cell that Contains the hyperlink
		///
		/// @return the 0-based column of the first cell that Contains the hyperlink
		public int FirstColumn
		{
			get
			{
				return buildCellReference().Col;
			}
			set
			{
				_ctHyperlink.@ref = new CellReference(FirstRow, value).FormatAsString();
			}
		}

		/// Return the column of the last cell that Contains the hyperlink
		///
		/// @return the 0-based column of the last cell that Contains the hyperlink
		public int LastColumn
		{
			get
			{
				return buildCellReference().Col;
			}
			set
			{
				FirstColumn = value;
			}
		}

		/// Return the row of the first cell that Contains the hyperlink
		///
		/// @return the 0-based row of the cell that Contains the hyperlink
		public int FirstRow
		{
			get
			{
				return buildCellReference().Row;
			}
			set
			{
				_ctHyperlink.@ref = new CellReference(value, FirstColumn).FormatAsString();
			}
		}

		/// Return the row of the last cell that Contains the hyperlink
		///
		/// @return the 0-based row of the last cell that Contains the hyperlink
		public int LastRow
		{
			get
			{
				return buildCellReference().Row;
			}
			set
			{
				FirstRow = value;
			}
		}

		public string TextMark
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

		/// <summary>
		/// get or set additional text to help the user understand more about the hyperlink
		/// </summary>
		public string Tooltip
		{
			get
			{
				return _ctHyperlink.tooltip;
			}
			set
			{
				_ctHyperlink.tooltip = value;
			}
		}

		/// Create a new XSSFHyperlink. This method is protected to be used only by XSSFCreationHelper
		///
		/// @param type - the type of hyperlink to create
		public XSSFHyperlink(HyperlinkType type)
		{
			_type = type;
			_ctHyperlink = new CT_Hyperlink();
		}

		/// Create a XSSFHyperlink amd Initialize it from the supplied CTHyperlink bean and namespace relationship
		///
		/// @param ctHyperlink the xml bean Containing xml properties
		/// @param hyperlinkRel the relationship in the underlying OPC namespace which stores the actual link's Address
		public XSSFHyperlink(CT_Hyperlink ctHyperlink, PackageRelationship hyperlinkRel)
		{
			_ctHyperlink = ctHyperlink;
			_externalRel = hyperlinkRel;
			if (ctHyperlink.location != null)
			{
				_type = HyperlinkType.Document;
				_location = ctHyperlink.location;
			}
			else if (_externalRel == null)
			{
				if (ctHyperlink.id != null)
				{
					throw new InvalidOperationException("The hyperlink for cell " + ctHyperlink.@ref + " references relation " + ctHyperlink.id + ", but that didn't exist!");
				}
				_type = HyperlinkType.Document;
			}
			else
			{
				Uri targetUri = _externalRel.TargetUri;
				try
				{
					_location = targetUri.ToString();
				}
				catch (UriFormatException)
				{
					_location = targetUri.OriginalString;
				}
				if (_location.StartsWith("http://") || _location.StartsWith("https://") || _location.StartsWith("ftp://"))
				{
					_type = HyperlinkType.Url;
				}
				else if (_location.StartsWith("mailto:"))
				{
					_type = HyperlinkType.Email;
				}
				else
				{
					_type = HyperlinkType.File;
				}
			}
		}

		/// @return the underlying CTHyperlink object
		public CT_Hyperlink GetCTHyperlink()
		{
			return _ctHyperlink;
		}

		/// Do we need to a relation too, to represent
		/// this hyperlink?
		public bool NeedsRelationToo()
		{
			return _type != HyperlinkType.Document;
		}

		/// Generates the relation if required
		internal void GenerateRelationIfNeeded(PackagePart sheetPart)
		{
			if (_externalRel == null && NeedsRelationToo())
			{
				PackageRelationship packageRelationship = sheetPart.AddExternalRelationship(_location, XSSFRelation.SHEET_HYPERLINKS.Relation);
				_ctHyperlink.id = packageRelationship.Id;
			}
		}

		/// Get the reference of the cell this applies to,
		/// es A55
		public string GetCellRef()
		{
			return _ctHyperlink.@ref;
		}

		private void Validate(string address)
		{
			switch (_type)
			{
			case HyperlinkType.Document:
				break;
			case HyperlinkType.Url:
			case HyperlinkType.Email:
			case HyperlinkType.File:
				if (!Uri.IsWellFormedUriString(address, UriKind.RelativeOrAbsolute))
				{
					throw new ArgumentException("Address of hyperlink must be a valid URI:" + address);
				}
				break;
			}
		}

		/// Assigns this hyperlink to the given cell reference
		internal void SetCellReference(string ref1)
		{
			_ctHyperlink.@ref = ref1;
		}

		private CellReference buildCellReference()
		{
			return new CellReference(_ctHyperlink.@ref);
		}
	}
}
