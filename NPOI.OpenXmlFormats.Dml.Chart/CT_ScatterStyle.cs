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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[DesignerCategory("code")]
	public class CT_ScatterStyle
	{
		private ST_ScatterStyle valField;

		[DefaultValue(ST_ScatterStyle.marker)]
		[XmlAttribute]
		public ST_ScatterStyle val
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

		public static CT_ScatterStyle Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ScatterStyle cT_ScatterStyle = new CT_ScatterStyle();
			if (node.Attributes["val"] != null)
			{
				cT_ScatterStyle.val = (ST_ScatterStyle)Enum.Parse(typeof(ST_ScatterStyle), node.Attributes["val"].Value);
			}
			return cT_ScatterStyle;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_ScatterStyle()
		{
			valField = ST_ScatterStyle.marker;
		}
	}
}
