using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_NumFmt
	{
		private uint numFmtIdField;

		private string formatCodeField;

		[XmlAttribute]
		public uint numFmtId
		{
			get
			{
				return numFmtIdField;
			}
			set
			{
				numFmtIdField = value;
			}
		}

		[XmlAttribute]
		public string formatCode
		{
			get
			{
				return formatCodeField;
			}
			set
			{
				formatCodeField = value;
			}
		}

		public static CT_NumFmt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumFmt cT_NumFmt = new CT_NumFmt();
			cT_NumFmt.numFmtId = XmlHelper.ReadUInt(node.Attributes["numFmtId"]);
			cT_NumFmt.formatCode = XmlHelper.ReadString(node.Attributes["formatCode"]);
			return cT_NumFmt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "numFmtId", numFmtId);
			XmlHelper.WriteAttribute(sw, "formatCode", formatCode);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
