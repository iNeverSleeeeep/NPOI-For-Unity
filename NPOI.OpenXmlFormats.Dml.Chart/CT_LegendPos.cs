using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_LegendPos
	{
		private ST_LegendPos valField;

		[DefaultValue(ST_LegendPos.r)]
		[XmlAttribute]
		public ST_LegendPos val
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

		public CT_LegendPos()
		{
			valField = ST_LegendPos.r;
		}

		public static CT_LegendPos Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LegendPos cT_LegendPos = new CT_LegendPos();
			if (node.Attributes["val"] != null)
			{
				cT_LegendPos.val = (ST_LegendPos)Enum.Parse(typeof(ST_LegendPos), node.Attributes["val"].Value);
			}
			return cT_LegendPos;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			if (val != ST_LegendPos.r)
			{
				XmlHelper.WriteAttribute(sw, "val", val.ToString());
			}
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
