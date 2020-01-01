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
	public class CT_RubyAlign
	{
		private ST_RubyAlign valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_RubyAlign val
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

		public static CT_RubyAlign Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RubyAlign cT_RubyAlign = new CT_RubyAlign();
			if (node.Attributes["w:val"] != null)
			{
				cT_RubyAlign.val = (ST_RubyAlign)Enum.Parse(typeof(ST_RubyAlign), node.Attributes["w:val"].Value);
			}
			return cT_RubyAlign;
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
