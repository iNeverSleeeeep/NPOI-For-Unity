using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ColorScale
	{
		private List<CT_Cfvo> cfvoField;

		private List<CT_Color> colorField;

		public List<CT_Cfvo> cfvo
		{
			get
			{
				return cfvoField;
			}
			set
			{
				cfvoField = value;
			}
		}

		public List<CT_Color> color
		{
			get
			{
				return colorField;
			}
			set
			{
				colorField = value;
			}
		}

		public CT_ColorScale()
		{
			colorField = new List<CT_Color>();
			cfvoField = new List<CT_Cfvo>();
		}

		public static CT_ColorScale Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ColorScale cT_ColorScale = new CT_ColorScale();
			cT_ColorScale.cfvo = new List<CT_Cfvo>();
			cT_ColorScale.color = new List<CT_Color>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cfvo")
				{
					cT_ColorScale.cfvo.Add(CT_Cfvo.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "color")
				{
					cT_ColorScale.color.Add(CT_Color.Parse(childNode, namespaceManager));
				}
			}
			return cT_ColorScale;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (cfvo != null)
			{
				foreach (CT_Cfvo item in cfvo)
				{
					item.Write(sw, "cfvo");
				}
			}
			if (color != null)
			{
				foreach (CT_Color item2 in color)
				{
					item2.Write(sw, "color");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
