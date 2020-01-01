using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_SupplementalFont
	{
		private string scriptField;

		private string typefaceField;

		[XmlAttribute]
		public string script
		{
			get
			{
				return scriptField;
			}
			set
			{
				scriptField = value;
			}
		}

		[XmlAttribute]
		public string typeface
		{
			get
			{
				return typefaceField;
			}
			set
			{
				typefaceField = value;
			}
		}

		public static CT_SupplementalFont Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SupplementalFont cT_SupplementalFont = new CT_SupplementalFont();
			cT_SupplementalFont.script = XmlHelper.ReadString(node.Attributes["script"]);
			cT_SupplementalFont.typeface = XmlHelper.ReadString(node.Attributes["typeface"]);
			return cT_SupplementalFont;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "script", script);
			XmlHelper.WriteAttribute(sw, "typeface", typeface);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
