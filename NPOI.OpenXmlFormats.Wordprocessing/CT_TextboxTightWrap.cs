using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TextboxTightWrap
	{
		private ST_TextboxTightWrap valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TextboxTightWrap val
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

		public static CT_TextboxTightWrap Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TextboxTightWrap cT_TextboxTightWrap = new CT_TextboxTightWrap();
			if (node.Attributes["w:val"] != null)
			{
				cT_TextboxTightWrap.val = (ST_TextboxTightWrap)Enum.Parse(typeof(ST_TextboxTightWrap), node.Attributes["w:val"].Value);
			}
			return cT_TextboxTightWrap;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
