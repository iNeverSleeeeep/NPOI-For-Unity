using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_BorderPr
	{
		private CT_Color colorField;

		private ST_BorderStyle styleField;

		[XmlElement]
		public CT_Color color
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

		[DefaultValue(ST_BorderStyle.none)]
		[XmlAttribute]
		public ST_BorderStyle style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		public static CT_BorderPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BorderPr cT_BorderPr = new CT_BorderPr();
			if (node.Attributes["style"] != null)
			{
				cT_BorderPr.style = (ST_BorderStyle)Enum.Parse(typeof(ST_BorderStyle), node.Attributes["style"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "color")
				{
					cT_BorderPr.color = CT_Color.Parse(childNode, namespaceManager);
				}
			}
			return cT_BorderPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (style != 0)
			{
				XmlHelper.WriteAttribute(sw, "style", style.ToString());
			}
			if (color != null)
			{
				sw.Write(">");
				color.Write(sw, "color");
				sw.Write(string.Format("</{0}>", nodeName));
			}
			else
			{
				sw.Write("/>");
			}
		}

		public CT_BorderPr()
		{
			styleField = ST_BorderStyle.none;
		}

		public void SetColor(CT_Color color)
		{
			colorField = color;
		}

		public bool IsSetColor()
		{
			return colorField != null;
		}

		public void UnsetColor()
		{
			colorField = null;
		}

		public bool IsSetStyle()
		{
			return styleField != ST_BorderStyle.none;
		}
	}
}
