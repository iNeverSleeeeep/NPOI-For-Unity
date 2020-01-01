using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_FontScheme
	{
		private ST_FontScheme valField;

		[XmlAttribute]
		public ST_FontScheme val
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

		public static CT_FontScheme Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FontScheme cT_FontScheme = new CT_FontScheme();
			if (node.Attributes["val"] != null)
			{
				cT_FontScheme.val = (ST_FontScheme)Enum.Parse(typeof(ST_FontScheme), node.Attributes["val"].Value);
			}
			return cT_FontScheme;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write("/>");
		}
	}
}
