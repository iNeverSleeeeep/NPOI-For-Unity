using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_OleSize
	{
		private string refField;

		[XmlAttribute]
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

		public static CT_OleSize Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_OleSize cT_OleSize = new CT_OleSize();
			cT_OleSize.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			return cT_OleSize;
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
