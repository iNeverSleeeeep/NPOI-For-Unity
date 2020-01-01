using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.Model
{
	/// The cells in a workbook can be calculated in different orders depending on various optimizations and
	/// dependencies. The calculation chain object specifies the order in which the cells in a workbook were last calculated.
	///
	/// @author Yegor Kozlov
	public class CalculationChain : POIXMLDocumentPart
	{
		private CT_CalcChain chain;

		public CalculationChain()
		{
			chain = new CT_CalcChain();
		}

		internal CalculationChain(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			XmlDocument xml = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			ReadFrom(xml);
		}

		public void ReadFrom(XmlDocument xml)
		{
			CalcChainDocument calcChainDocument = CalcChainDocument.Parse(xml, POIXMLDocumentPart.NamespaceManager);
			chain = calcChainDocument.GetCalcChain();
		}

		public void WriteTo(Stream out1)
		{
			CalcChainDocument calcChainDocument = new CalcChainDocument();
			calcChainDocument.SetCalcChain(chain);
			calcChainDocument.Save(out1);
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			WriteTo(outputStream);
			outputStream.Close();
		}

		public CT_CalcChain GetCTCalcChain()
		{
			return chain;
		}

		/// Remove a formula reference from the calculation chain
		///
		/// @param sheetId  the sheet Id of a sheet the formula belongs to.
		/// @param ref  A1 style reference to the cell Containing the formula.
		public void RemoveItem(int sheetId, string ref1)
		{
			int num = -1;
			List<CT_CalcCell> c = chain.c;
			int num2 = 0;
			while (true)
			{
				if (num2 >= c.Count)
				{
					return;
				}
				if (c[num2].iSpecified)
				{
					num = c[num2].i;
				}
				if (num == sheetId && c[num2].r.Equals(ref1))
				{
					break;
				}
				num2++;
			}
			if (c[num2].iSpecified && num2 < c.Count - 1 && !c[num2 + 1].iSpecified)
			{
				c[num2 + 1].i = num;
			}
			chain.RemoveC(num2);
		}
	}
}
