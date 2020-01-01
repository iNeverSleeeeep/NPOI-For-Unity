using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_Orientation
	{
		private ST_Orientation valField;

		[XmlAttribute]
		public ST_Orientation val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public CT_Orientation()
		{
			valField = ST_Orientation.minMax;
		}

		public static CT_Orientation Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Orientation cT_Orientation = new CT_Orientation();
			if (node.Attributes["val"] != null)
			{
				cT_Orientation.val = (ST_Orientation)Enum.Parse(typeof(ST_Orientation), node.Attributes["val"].Value);
			}
			return cT_Orientation;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
