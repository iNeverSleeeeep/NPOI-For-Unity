using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_Grouping
	{
		private ST_Grouping valField;

		[DefaultValue(ST_Grouping.standard)]
		[XmlAttribute]
		public ST_Grouping val
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

		public CT_Grouping()
		{
			valField = ST_Grouping.standard;
		}

		public static CT_Grouping Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Grouping cT_Grouping = new CT_Grouping();
			if (node.Attributes["val"] != null)
			{
				cT_Grouping.val = (ST_Grouping)Enum.Parse(typeof(ST_Grouping), node.Attributes["val"].Value);
			}
			return cT_Grouping;
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
