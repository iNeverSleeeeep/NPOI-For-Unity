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
	public class CT_MailMergeDest
	{
		private ST_MailMergeDest valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_MailMergeDest val
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

		public static CT_MailMergeDest Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MailMergeDest cT_MailMergeDest = new CT_MailMergeDest();
			if (node.Attributes["w:val"] != null)
			{
				cT_MailMergeDest.val = (ST_MailMergeDest)Enum.Parse(typeof(ST_MailMergeDest), node.Attributes["w:val"].Value);
			}
			return cT_MailMergeDest;
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
