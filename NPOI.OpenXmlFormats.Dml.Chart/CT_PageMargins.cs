using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_PageMargins
	{
		private double lField;

		private double rField;

		private double tField;

		private double bField;

		private double headerField;

		private double footerField;

		[XmlAttribute]
		public double l
		{
			get
			{
				return lField;
			}
			set
			{
				lField = value;
			}
		}

		[XmlAttribute]
		public double r
		{
			get
			{
				return rField;
			}
			set
			{
				rField = value;
			}
		}

		[XmlAttribute]
		public double t
		{
			get
			{
				return tField;
			}
			set
			{
				tField = value;
			}
		}

		[XmlAttribute]
		public double b
		{
			get
			{
				return bField;
			}
			set
			{
				bField = value;
			}
		}

		[XmlAttribute]
		public double header
		{
			get
			{
				return headerField;
			}
			set
			{
				headerField = value;
			}
		}

		[XmlAttribute]
		public double footer
		{
			get
			{
				return footerField;
			}
			set
			{
				footerField = value;
			}
		}

		public static CT_PageMargins Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageMargins cT_PageMargins = new CT_PageMargins();
			if (node.Attributes["l"] != null)
			{
				cT_PageMargins.l = XmlHelper.ReadDouble(node.Attributes["l"]);
			}
			if (node.Attributes["r"] != null)
			{
				cT_PageMargins.r = XmlHelper.ReadDouble(node.Attributes["r"]);
			}
			if (node.Attributes["t"] != null)
			{
				cT_PageMargins.t = XmlHelper.ReadDouble(node.Attributes["t"]);
			}
			if (node.Attributes["b"] != null)
			{
				cT_PageMargins.b = XmlHelper.ReadDouble(node.Attributes["b"]);
			}
			if (node.Attributes["header"] != null)
			{
				cT_PageMargins.header = XmlHelper.ReadDouble(node.Attributes["header"]);
			}
			if (node.Attributes["footer"] != null)
			{
				cT_PageMargins.footer = XmlHelper.ReadDouble(node.Attributes["footer"]);
			}
			return cT_PageMargins;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "l", l);
			XmlHelper.WriteAttribute(sw, "r", r);
			XmlHelper.WriteAttribute(sw, "t", t);
			XmlHelper.WriteAttribute(sw, "b", b);
			XmlHelper.WriteAttribute(sw, "header", header);
			XmlHelper.WriteAttribute(sw, "footer", footer);
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
