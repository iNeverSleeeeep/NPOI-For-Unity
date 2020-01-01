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
	public class CT_MailMergeSourceType
	{
		private ST_MailMergeSourceType valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_MailMergeSourceType val
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

		public static CT_MailMergeSourceType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MailMergeSourceType cT_MailMergeSourceType = new CT_MailMergeSourceType();
			if (node.Attributes["w:val"] != null)
			{
				cT_MailMergeSourceType.val = (ST_MailMergeSourceType)Enum.Parse(typeof(ST_MailMergeSourceType), node.Attributes["w:val"].Value);
			}
			return cT_MailMergeSourceType;
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
