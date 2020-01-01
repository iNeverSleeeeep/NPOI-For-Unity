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
	public class CT_TickLblPos
	{
		private ST_TickLblPos valField;

		[DefaultValue(ST_TickLblPos.nextTo)]
		[XmlAttribute]
		public ST_TickLblPos val
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

		public CT_TickLblPos()
		{
			valField = ST_TickLblPos.nextTo;
		}

		public static CT_TickLblPos Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TickLblPos cT_TickLblPos = new CT_TickLblPos();
			if (node.Attributes["val"] != null)
			{
				cT_TickLblPos.val = (ST_TickLblPos)Enum.Parse(typeof(ST_TickLblPos), node.Attributes["val"].Value);
			}
			return cT_TickLblPos;
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
