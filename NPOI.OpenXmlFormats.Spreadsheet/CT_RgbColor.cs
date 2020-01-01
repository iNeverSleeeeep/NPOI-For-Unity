using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_RgbColor
	{
		private byte[] rgbField;

		[XmlAttribute(DataType = "hexBinary")]
		public byte[] rgb
		{
			get
			{
				return rgbField;
			}
			set
			{
				rgbField = value;
			}
		}

		public static CT_RgbColor Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RgbColor cT_RgbColor = new CT_RgbColor();
			cT_RgbColor.rgb = XmlHelper.ReadBytes(node.Attributes["rgb"]);
			return cT_RgbColor;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "rgb", rgb);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
