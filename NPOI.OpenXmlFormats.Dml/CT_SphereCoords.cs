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
	public class CT_SphereCoords
	{
		private int latField;

		private int lonField;

		private int revField;

		[XmlAttribute]
		public int lat
		{
			get
			{
				return latField;
			}
			set
			{
				latField = value;
			}
		}

		[XmlAttribute]
		public int lon
		{
			get
			{
				return lonField;
			}
			set
			{
				lonField = value;
			}
		}

		[XmlAttribute]
		public int rev
		{
			get
			{
				return revField;
			}
			set
			{
				revField = value;
			}
		}

		public static CT_SphereCoords Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SphereCoords cT_SphereCoords = new CT_SphereCoords();
			cT_SphereCoords.lat = XmlHelper.ReadInt(node.Attributes["lat"]);
			cT_SphereCoords.lon = XmlHelper.ReadInt(node.Attributes["lon"]);
			cT_SphereCoords.rev = XmlHelper.ReadInt(node.Attributes["rev"]);
			return cT_SphereCoords;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "lat", lat);
			XmlHelper.WriteAttribute(sw, "lon", lon);
			XmlHelper.WriteAttribute(sw, "rev", rev);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
