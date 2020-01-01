using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CellWatch
	{
		private string rField;

		public string r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		public static CT_CellWatch Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellWatch cT_CellWatch = new CT_CellWatch();
			cT_CellWatch.r = XmlHelper.ReadString(node.Attributes["r"]);
			return cT_CellWatch;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r", r);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
