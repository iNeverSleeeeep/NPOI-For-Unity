using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SheetDimension
	{
		private string refField;

		[XmlAttribute("ref")]
		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
			}
		}

		public static CT_SheetDimension Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SheetDimension cT_SheetDimension = new CT_SheetDimension();
			cT_SheetDimension.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			return cT_SheetDimension;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
