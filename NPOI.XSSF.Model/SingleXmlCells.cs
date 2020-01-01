using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.XSSF.UserModel;
using NPOI.XSSF.UserModel.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.Model
{
	/// This class : the Single Cell Tables Part (Open Office XML Part 4:
	/// chapter 3.5.2)
	///
	///
	/// @author Roberto Manicardi
	public class SingleXmlCells : POIXMLDocumentPart
	{
		private CT_SingleXmlCells SingleXMLCells;

		public SingleXmlCells()
		{
			SingleXMLCells = new CT_SingleXmlCells();
		}

		internal SingleXmlCells(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			ReadFrom(part.GetInputStream());
		}

		public void ReadFrom(Stream is1)
		{
			try
			{
				SingleXmlCellsDocument singleXmlCellsDocument = SingleXmlCellsDocument.Parse(is1);
				SingleXMLCells = singleXmlCellsDocument.GetSingleXmlCells();
			}
			catch (XmlException ex)
			{
				throw new IOException(ex.Message);
			}
		}

		public XSSFSheet GetXSSFSheet()
		{
			return (XSSFSheet)GetParent();
		}

		protected void WriteTo(Stream out1)
		{
			SingleXmlCellsDocument singleXmlCellsDocument = new SingleXmlCellsDocument();
			singleXmlCellsDocument.SetSingleXmlCells(SingleXMLCells);
			singleXmlCellsDocument.Save(out1);
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			WriteTo(outputStream);
			outputStream.Close();
		}

		public CT_SingleXmlCells GetCTSingleXMLCells()
		{
			return SingleXMLCells;
		}

		/// @return all the SimpleXmlCell Contained in this SingleXmlCells element
		public List<XSSFSingleXmlCell> GetAllSimpleXmlCell()
		{
			List<XSSFSingleXmlCell> list = new List<XSSFSingleXmlCell>();
			foreach (CT_SingleXmlCell item in SingleXMLCells.singleXmlCell)
			{
				list.Add(new XSSFSingleXmlCell(item, this));
			}
			return list;
		}
	}
}
