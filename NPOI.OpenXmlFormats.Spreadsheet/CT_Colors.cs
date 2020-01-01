using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Colors
	{
		private List<CT_RgbColor> indexedColorsField;

		private List<CT_Color> mruColorsField;

		[XmlArrayItem("rgbColor", IsNullable = false)]
		[XmlArray(Order = 0)]
		public List<CT_RgbColor> indexedColors
		{
			get
			{
				return indexedColorsField;
			}
			set
			{
				indexedColorsField = value;
			}
		}

		[XmlArray(Order = 1)]
		[XmlArrayItem("color", IsNullable = false)]
		public List<CT_Color> mruColors
		{
			get
			{
				return mruColorsField;
			}
			set
			{
				mruColorsField = value;
			}
		}

		public static CT_Colors Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Colors cT_Colors = new CT_Colors();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "indexedColors")
				{
					cT_Colors.indexedColors = new List<CT_RgbColor>();
					foreach (XmlNode childNode2 in childNode.ChildNodes)
					{
						cT_Colors.indexedColors.Add(CT_RgbColor.Parse(childNode2, namespaceManager));
					}
				}
				else if (childNode.LocalName == "mruColors")
				{
					cT_Colors.mruColors = new List<CT_Color>();
					foreach (XmlNode childNode3 in childNode.ChildNodes)
					{
						cT_Colors.mruColors.Add(CT_Color.Parse(childNode3, namespaceManager));
					}
				}
			}
			return cT_Colors;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}>", nodeName));
			if (indexedColors != null)
			{
				sw.Write("<indexedColors>");
				foreach (CT_RgbColor indexedColor in indexedColors)
				{
					indexedColor.Write(sw, "rgbColor");
				}
				sw.Write("</indexedColors>");
			}
			if (mruColors != null)
			{
				sw.Write("<mruColors>");
				foreach (CT_Color mruColor in mruColors)
				{
					mruColor.Write(sw, "color");
				}
				sw.Write("</mruColors>");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
