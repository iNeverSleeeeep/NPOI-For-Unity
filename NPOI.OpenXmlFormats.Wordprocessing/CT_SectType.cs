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
	public class CT_SectType
	{
		private ST_SectionMark valField;

		private bool valFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_SectionMark val
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

		[XmlIgnore]
		public bool valSpecified
		{
			get
			{
				return valFieldSpecified;
			}
			set
			{
				valFieldSpecified = value;
			}
		}

		public static CT_SectType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SectType cT_SectType = new CT_SectType();
			if (node.Attributes["w:val"] != null)
			{
				cT_SectType.val = (ST_SectionMark)Enum.Parse(typeof(ST_SectionMark), node.Attributes["w:val"].Value);
			}
			return cT_SectType;
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
