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
	public class CT_MailMergeDataType
	{
		private ST_MailMergeDataType valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_MailMergeDataType val
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

		public static CT_MailMergeDataType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MailMergeDataType cT_MailMergeDataType = new CT_MailMergeDataType();
			if (node.Attributes["w:val"] != null)
			{
				cT_MailMergeDataType.val = (ST_MailMergeDataType)Enum.Parse(typeof(ST_MailMergeDataType), node.Attributes["w:val"].Value);
			}
			return cT_MailMergeDataType;
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
