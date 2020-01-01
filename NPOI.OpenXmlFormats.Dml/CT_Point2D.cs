using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Point2D
	{
		private long xField;

		private long yField;

		[XmlAttribute]
		public long x
		{
			get
			{
				return xField;
			}
			set
			{
				xField = value;
			}
		}

		[XmlAttribute]
		public long y
		{
			get
			{
				return yField;
			}
			set
			{
				yField = value;
			}
		}

		public static CT_Point2D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Point2D cT_Point2D = new CT_Point2D();
			cT_Point2D.x = XmlHelper.ReadLong(node.Attributes["x"]);
			cT_Point2D.y = XmlHelper.ReadLong(node.Attributes["y"]);
			return cT_Point2D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "x", (double)x, true);
			XmlHelper.WriteAttribute(sw, "y", (double)y, true);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
