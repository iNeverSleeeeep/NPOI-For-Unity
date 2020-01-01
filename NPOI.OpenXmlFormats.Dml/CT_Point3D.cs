using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_Point3D
	{
		private long xField;

		private long yField;

		private long zField;

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

		[XmlAttribute]
		public long z
		{
			get
			{
				return zField;
			}
			set
			{
				zField = value;
			}
		}

		public static CT_Point3D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Point3D cT_Point3D = new CT_Point3D();
			cT_Point3D.x = XmlHelper.ReadLong(node.Attributes["x"]);
			cT_Point3D.y = XmlHelper.ReadLong(node.Attributes["y"]);
			cT_Point3D.z = XmlHelper.ReadLong(node.Attributes["z"]);
			return cT_Point3D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "x", (double)x);
			XmlHelper.WriteAttribute(sw, "y", (double)y);
			XmlHelper.WriteAttribute(sw, "z", (double)z);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
