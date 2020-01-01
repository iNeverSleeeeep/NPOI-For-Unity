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
	public class CT_MailMergeDocType
	{
		private ST_MailMergeDocType valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_MailMergeDocType val
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

		public static CT_MailMergeDocType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MailMergeDocType cT_MailMergeDocType = new CT_MailMergeDocType();
			if (node.Attributes["w:val"] != null)
			{
				cT_MailMergeDocType.val = (ST_MailMergeDocType)Enum.Parse(typeof(ST_MailMergeDocType), node.Attributes["w:val"].Value);
			}
			return cT_MailMergeDocType;
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
