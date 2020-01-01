using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_VerticalAlignFontProperty
	{
		private ST_VerticalAlignRun valField;

		[XmlAttribute]
		public ST_VerticalAlignRun val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public static CT_VerticalAlignFontProperty Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_VerticalAlignFontProperty cT_VerticalAlignFontProperty = new CT_VerticalAlignFontProperty();
			if (node.Attributes["val"] != null)
			{
				cT_VerticalAlignFontProperty.val = (ST_VerticalAlignRun)Enum.Parse(typeof(ST_VerticalAlignRun), node.Attributes["val"].Value);
			}
			return cT_VerticalAlignFontProperty;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write("/>");
		}
	}
}
