using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_PageMargins
	{
		private double leftField;

		private double rightField;

		private double topField;

		private double bottomField;

		private double headerField;

		private double footerField;

		[XmlAttribute]
		public double left
		{
			get
			{
				return leftField;
			}
			set
			{
				leftField = value;
			}
		}

		[XmlAttribute]
		public double right
		{
			get
			{
				return rightField;
			}
			set
			{
				rightField = value;
			}
		}

		[XmlAttribute]
		public double top
		{
			get
			{
				return topField;
			}
			set
			{
				topField = value;
			}
		}

		[XmlAttribute]
		public double bottom
		{
			get
			{
				return bottomField;
			}
			set
			{
				bottomField = value;
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
			cT_PageMargins.left = XmlHelper.ReadDouble(node.Attributes["left"]);
			cT_PageMargins.right = XmlHelper.ReadDouble(node.Attributes["right"]);
			cT_PageMargins.top = XmlHelper.ReadDouble(node.Attributes["top"]);
			cT_PageMargins.bottom = XmlHelper.ReadDouble(node.Attributes["bottom"]);
			cT_PageMargins.header = XmlHelper.ReadDouble(node.Attributes["header"]);
			cT_PageMargins.footer = XmlHelper.ReadDouble(node.Attributes["footer"]);
			return cT_PageMargins;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "left", left, true);
			XmlHelper.WriteAttribute(sw, "right", right, true);
			XmlHelper.WriteAttribute(sw, "top", top, true);
			XmlHelper.WriteAttribute(sw, "bottom", bottom, true);
			XmlHelper.WriteAttribute(sw, "header", header, true);
			XmlHelper.WriteAttribute(sw, "footer", footer, true);
			sw.Write("/>");
		}
	}
}
