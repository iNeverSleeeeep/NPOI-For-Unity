using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel.Extensions
{
	/// Cell Settings avaiable in the Format/Alignment tab
	public class XSSFCellAlignment
	{
		private CT_CellAlignment cellAlignement;

		/// Get the type of vertical alignment for the cell
		///
		/// @return the type of aligment
		/// @see VerticalAlignment
		public VerticalAlignment Vertical
		{
			get
			{
				return (VerticalAlignment)cellAlignement.vertical;
			}
			set
			{
				cellAlignement.vertical = (ST_VerticalAlignment)value;
				cellAlignement.verticalSpecified = true;
			}
		}

		/// Get the type of horizontal alignment for the cell
		///
		/// @return the type of aligment
		/// @see HorizontalAlignment
		public HorizontalAlignment Horizontal
		{
			get
			{
				return (HorizontalAlignment)cellAlignement.horizontal;
			}
			set
			{
				cellAlignement.horizontal = (ST_HorizontalAlignment)value;
				cellAlignement.horizontalSpecified = true;
			}
		}

		/// Get the number of spaces to indent the text in the cell
		///
		/// @return indent - number of spaces
		public long Indent
		{
			get
			{
				return cellAlignement.indent;
			}
			set
			{
				cellAlignement.indent = value;
				cellAlignement.indentSpecified = true;
			}
		}

		public long TextRotation
		{
			get
			{
				return cellAlignement.textRotation;
			}
			set
			{
				cellAlignement.textRotation = value;
				cellAlignement.textRotationSpecified = true;
			}
		}

		/// Whether the text should be wrapped
		///
		/// @return a bool value indicating if the text in a cell should be line-wrapped within the cell.
		public bool WrapText
		{
			get
			{
				return cellAlignement.wrapText;
			}
			set
			{
				cellAlignement.wrapText = value;
				cellAlignement.wrapTextSpecified = true;
			}
		}

		/// Creates a Cell Alignment from the supplied XML defInition
		///
		/// @param cellAlignment
		public XSSFCellAlignment(CT_CellAlignment cellAlignment)
		{
			cellAlignement = cellAlignment;
		}

		/// Access to low-level data
		public CT_CellAlignment GetCTCellAlignment()
		{
			return cellAlignement;
		}
	}
}
