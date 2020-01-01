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
	public class CT_SdtText
	{
		private ST_OnOff multiLineField;

		private bool multiLineFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff multiLine
		{
			get
			{
				return multiLineField;
			}
			set
			{
				multiLineField = value;
			}
		}

		[XmlIgnore]
		public bool multiLineSpecified
		{
			get
			{
				return multiLineFieldSpecified;
			}
			set
			{
				multiLineFieldSpecified = value;
			}
		}

		public static CT_SdtText Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SdtText cT_SdtText = new CT_SdtText();
			if (node.Attributes["w:multiLine"] != null)
			{
				cT_SdtText.multiLine = (ST_OnOff)Enum.Parse(typeof(ST_OnOff), node.Attributes["w:multiLine"].Value);
			}
			return cT_SdtText;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:multiLine", multiLine.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
