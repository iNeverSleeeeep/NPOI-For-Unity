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
	public class CT_Br
	{
		private ST_BrType typeField;

		private bool typeFieldSpecified;

		private ST_BrClear clearField;

		private bool clearFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_BrType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
				typeFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool typeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_BrClear clear
		{
			get
			{
				return clearField;
			}
			set
			{
				clearField = value;
				clearFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool clearSpecified
		{
			get
			{
				return clearFieldSpecified;
			}
			set
			{
				clearFieldSpecified = value;
			}
		}

		public static CT_Br Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Br cT_Br = new CT_Br();
			if (node.Attributes["w:type"] != null)
			{
				cT_Br.type = (ST_BrType)Enum.Parse(typeof(ST_BrType), node.Attributes["w:type"].Value);
			}
			if (node.Attributes["w:clear"] != null)
			{
				cT_Br.clear = (ST_BrClear)Enum.Parse(typeof(ST_BrClear), node.Attributes["w:clear"].Value);
			}
			return cT_Br;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:type", type.ToString());
			XmlHelper.WriteAttribute(sw, "w:clear", clear.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
