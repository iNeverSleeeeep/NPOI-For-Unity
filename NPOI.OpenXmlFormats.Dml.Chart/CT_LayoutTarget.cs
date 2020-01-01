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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_LayoutTarget
	{
		private ST_LayoutTarget valField;

		[DefaultValue(ST_LayoutTarget.outer)]
		[XmlAttribute]
		public ST_LayoutTarget val
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

		public CT_LayoutTarget()
		{
			valField = ST_LayoutTarget.outer;
		}

		public static CT_LayoutTarget Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LayoutTarget cT_LayoutTarget = new CT_LayoutTarget();
			if (node.Attributes["val"] != null)
			{
				cT_LayoutTarget.val = (ST_LayoutTarget)Enum.Parse(typeof(ST_LayoutTarget), node.Attributes["val"].Value);
			}
			return cT_LayoutTarget;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			if (val != ST_LayoutTarget.outer)
			{
				XmlHelper.WriteAttribute(sw, "val", val.ToString());
			}
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
