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
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_GeomGuide
	{
		private string nameField;

		private string fmlaField;

		[XmlAttribute(DataType = "token")]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlAttribute]
		public string fmla
		{
			get
			{
				return fmlaField;
			}
			set
			{
				fmlaField = value;
			}
		}

		public static CT_GeomGuide Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GeomGuide cT_GeomGuide = new CT_GeomGuide();
			cT_GeomGuide.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_GeomGuide.fmla = XmlHelper.ReadString(node.Attributes["fmla"]);
			return cT_GeomGuide;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "fmla", fmla);
			sw.Write("/>");
		}
	}
}
