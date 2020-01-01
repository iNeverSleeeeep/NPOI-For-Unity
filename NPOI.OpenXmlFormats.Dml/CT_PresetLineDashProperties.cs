using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_PresetLineDashProperties
	{
		private ST_PresetLineDashVal valField;

		private bool valFieldSpecified;

		[XmlAttribute]
		public ST_PresetLineDashVal val
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

		public static CT_PresetLineDashProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PresetLineDashProperties cT_PresetLineDashProperties = new CT_PresetLineDashProperties();
			if (node.Attributes["val"] != null)
			{
				cT_PresetLineDashProperties.val = (ST_PresetLineDashVal)Enum.Parse(typeof(ST_PresetLineDashVal), node.Attributes["val"].Value);
			}
			return cT_PresetLineDashProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
