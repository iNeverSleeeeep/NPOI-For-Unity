using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_TextAlignment
	{
		private ST_TextAlignment valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TextAlignment val
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

		public static CT_TextAlignment Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextAlignment cT_TextAlignment = new CT_TextAlignment();
			if (node.Attributes["w:val"] != null)
			{
				cT_TextAlignment.val = (ST_TextAlignment)Enum.Parse(typeof(ST_TextAlignment), node.Attributes["w:val"].Value);
			}
			return cT_TextAlignment;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write("/>");
		}
	}
}
