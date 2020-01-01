using NPOI.OpenXml4Net.Util;
using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CalcChainDocument
	{
		private CT_CalcChain calcChain;

		public CalcChainDocument()
		{
			calcChain = new CT_CalcChain();
		}

		internal CalcChainDocument(CT_CalcChain calcChain)
		{
			this.calcChain = calcChain;
		}

		public CT_CalcChain GetCalcChain()
		{
			return calcChain;
		}

		public void SetCalcChain(CT_CalcChain calcchain)
		{
			calcChain = calcchain;
		}

		public static CalcChainDocument Parse(XmlDocument xmlDoc, XmlNamespaceManager NameSpaceManager)
		{
			CalcChainDocument calcChainDocument = new CalcChainDocument();
			foreach (XmlElement item in xmlDoc.SelectNodes("//d:c", NameSpaceManager))
			{
				CT_CalcCell cT_CalcCell = new CT_CalcCell();
				if (item.GetAttributeNode("i") != null)
				{
					cT_CalcCell.i = XmlHelper.ReadInt(item.GetAttributeNode("i"));
					cT_CalcCell.iSpecified = true;
				}
				cT_CalcCell.r = item.GetAttribute("r");
				cT_CalcCell.t = XmlHelper.ReadBool(item.GetAttributeNode("t"));
				cT_CalcCell.s = XmlHelper.ReadBool(item.GetAttributeNode("s"));
				cT_CalcCell.l = XmlHelper.ReadBool(item.GetAttributeNode("l"));
				calcChainDocument.calcChain.AddC(cT_CalcCell);
			}
			return calcChainDocument;
		}

		public void Save(Stream stream)
		{
			StreamWriter streamWriter = new StreamWriter(stream);
			streamWriter.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?>");
			streamWriter.Write("<calcChain xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">");
			foreach (CT_CalcCell item in calcChain.c)
			{
				streamWriter.Write("<c");
				streamWriter.Write(" r=\"" + item.r + "\"");
				if (item.i > 0)
				{
					streamWriter.Write(" i=\"" + item.i + "\"");
				}
				if (item.s)
				{
					streamWriter.Write(" s=\"" + (item.s ? 1 : 0) + "\"");
				}
				if (item.t)
				{
					streamWriter.Write(" t=\"" + (item.t ? 1 : 0) + "\"");
				}
				if (item.l)
				{
					streamWriter.Write(" l=\"" + (item.l ? 1 : 0) + "\"");
				}
				streamWriter.Write("/>");
			}
			streamWriter.Write("</calcChain>");
			streamWriter.Flush();
		}
	}
}
