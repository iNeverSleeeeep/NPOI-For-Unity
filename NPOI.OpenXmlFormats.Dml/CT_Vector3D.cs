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
	public class CT_Vector3D
	{
		private long dxField;

		private long dyField;

		private long dzField;

		[XmlAttribute]
		public long dx
		{
			get
			{
				return dxField;
			}
			set
			{
				dxField = value;
			}
		}

		[XmlAttribute]
		public long dy
		{
			get
			{
				return dyField;
			}
			set
			{
				dyField = value;
			}
		}

		[XmlAttribute]
		public long dz
		{
			get
			{
				return dzField;
			}
			set
			{
				dzField = value;
			}
		}

		public static CT_Vector3D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Vector3D cT_Vector3D = new CT_Vector3D();
			cT_Vector3D.dx = XmlHelper.ReadLong(node.Attributes["dx"]);
			cT_Vector3D.dy = XmlHelper.ReadLong(node.Attributes["dy"]);
			cT_Vector3D.dz = XmlHelper.ReadLong(node.Attributes["dz"]);
			return cT_Vector3D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "dx", (double)dx);
			XmlHelper.WriteAttribute(sw, "dy", (double)dy);
			XmlHelper.WriteAttribute(sw, "dz", (double)dz);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
