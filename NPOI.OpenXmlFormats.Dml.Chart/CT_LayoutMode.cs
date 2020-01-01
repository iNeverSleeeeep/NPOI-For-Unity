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
	public class CT_LayoutMode
	{
		private ST_LayoutMode valField;

		[DefaultValue(ST_LayoutMode.factor)]
		[XmlAttribute]
		public ST_LayoutMode val
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

		public CT_LayoutMode()
		{
			valField = ST_LayoutMode.factor;
		}

		public static CT_LayoutMode Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LayoutMode cT_LayoutMode = new CT_LayoutMode();
			if (node.Attributes["val"] != null)
			{
				cT_LayoutMode.val = (ST_LayoutMode)Enum.Parse(typeof(ST_LayoutMode), node.Attributes["val"].Value);
			}
			return cT_LayoutMode;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			if (val != ST_LayoutMode.factor)
			{
				XmlHelper.WriteAttribute(sw, "val", val.ToString());
			}
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
