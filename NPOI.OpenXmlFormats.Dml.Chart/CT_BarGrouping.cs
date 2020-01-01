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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_BarGrouping
	{
		private ST_BarGrouping valField;

		[DefaultValue(ST_BarGrouping.clustered)]
		[XmlAttribute]
		public ST_BarGrouping val
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

		public CT_BarGrouping()
		{
			valField = ST_BarGrouping.clustered;
		}

		public static CT_BarGrouping Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BarGrouping cT_BarGrouping = new CT_BarGrouping();
			if (node.Attributes["val"] != null)
			{
				cT_BarGrouping.val = (ST_BarGrouping)Enum.Parse(typeof(ST_BarGrouping), node.Attributes["val"].Value);
			}
			return cT_BarGrouping;
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
