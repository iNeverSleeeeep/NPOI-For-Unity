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
	public class CT_PaperSource
	{
		private string firstField;

		private string otherField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string first
		{
			get
			{
				return firstField;
			}
			set
			{
				firstField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
		public string other
		{
			get
			{
				return otherField;
			}
			set
			{
				otherField = value;
			}
		}

		public static CT_PaperSource Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PaperSource cT_PaperSource = new CT_PaperSource();
			cT_PaperSource.first = XmlHelper.ReadString(node.Attributes["w:first"]);
			cT_PaperSource.other = XmlHelper.ReadString(node.Attributes["w:other"]);
			return cT_PaperSource;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:first", first);
			XmlHelper.WriteAttribute(sw, "w:other", other);
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
