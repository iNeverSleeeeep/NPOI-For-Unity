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
	public class CT_MailMergeOdsoFMDFieldType
	{
		private ST_MailMergeOdsoFMDFieldType valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_MailMergeOdsoFMDFieldType val
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

		public static CT_MailMergeOdsoFMDFieldType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MailMergeOdsoFMDFieldType cT_MailMergeOdsoFMDFieldType = new CT_MailMergeOdsoFMDFieldType();
			if (node.Attributes["w:val"] != null)
			{
				cT_MailMergeOdsoFMDFieldType.val = (ST_MailMergeOdsoFMDFieldType)Enum.Parse(typeof(ST_MailMergeOdsoFMDFieldType), node.Attributes["w:val"].Value);
			}
			return cT_MailMergeOdsoFMDFieldType;
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
