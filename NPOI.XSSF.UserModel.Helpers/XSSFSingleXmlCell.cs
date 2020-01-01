using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.Model;

namespace NPOI.XSSF.UserModel.Helpers
{
	/// * 
	/// * This class is a wrapper around the CTSingleXmlCell  (Open Office XML Part 4:
	/// * chapter 3.5.2.1) 
	/// * 
	///
	/// * 
	/// * @author Roberto Manicardi
	/// *
	public class XSSFSingleXmlCell
	{
		private CT_SingleXmlCell SingleXmlCell;

		private SingleXmlCells parent;

		public XSSFSingleXmlCell(CT_SingleXmlCell SingleXmlCell, SingleXmlCells parent)
		{
			this.SingleXmlCell = SingleXmlCell;
			this.parent = parent;
		}

		/// Gets the XSSFCell referenced by the R attribute or Creates a new one if cell doesn't exists
		/// @return the referenced XSSFCell, null if the cell reference is invalid
		public ICell GetReferencedCell()
		{
			ICell cell = null;
			CellReference cellReference = new CellReference(SingleXmlCell.r);
			IRow row = parent.GetXSSFSheet().GetRow(cellReference.Row);
			if (row == null)
			{
				row = parent.GetXSSFSheet().CreateRow(cellReference.Row);
			}
			cell = row.GetCell(cellReference.Col);
			if (cell == null)
			{
				cell = row.CreateCell(cellReference.Col);
			}
			return cell;
		}

		public string GetXpath()
		{
			CT_XmlCellPr xmlCellPr = SingleXmlCell.xmlCellPr;
			CT_XmlPr xmlPr = xmlCellPr.xmlPr;
			return xmlPr.xpath;
		}

		public long GetMapId()
		{
			return SingleXmlCell.xmlCellPr.xmlPr.mapId;
		}

		public ST_XmlDataType GetXmlDataType()
		{
			CT_XmlCellPr xmlCellPr = SingleXmlCell.xmlCellPr;
			CT_XmlPr xmlPr = xmlCellPr.xmlPr;
			return xmlPr.xmlDataType;
		}
	}
}
