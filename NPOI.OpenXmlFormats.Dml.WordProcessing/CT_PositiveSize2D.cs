using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	[XmlRoot("inline", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = false)]
	public class CT_PositiveSize2D
	{
		private long cxField;

		private long cyField;

		[XmlAttribute]
		public long cx
		{
			get
			{
				return cxField;
			}
			set
			{
				cxField = value;
			}
		}

		[XmlAttribute]
		public long cy
		{
			get
			{
				return cyField;
			}
			set
			{
				cyField = value;
			}
		}

		public static CT_PositiveSize2D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PositiveSize2D cT_PositiveSize2D = new CT_PositiveSize2D();
			cT_PositiveSize2D.cx = XmlHelper.ReadLong(node.Attributes["cx"]);
			cT_PositiveSize2D.cy = XmlHelper.ReadLong(node.Attributes["cy"]);
			return cT_PositiveSize2D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<wp:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "cx", (double)cx, true);
			XmlHelper.WriteAttribute(sw, "cy", (double)cy, true);
			sw.Write("/>");
		}
	}
}
