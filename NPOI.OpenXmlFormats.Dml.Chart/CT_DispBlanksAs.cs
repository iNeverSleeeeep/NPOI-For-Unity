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
	public class CT_DispBlanksAs
	{
		private ST_DispBlanksAs valField;

		[DefaultValue(ST_DispBlanksAs.zero)]
		[XmlAttribute]
		public ST_DispBlanksAs val
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

		public CT_DispBlanksAs()
		{
			valField = ST_DispBlanksAs.zero;
		}

		public static CT_DispBlanksAs Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DispBlanksAs cT_DispBlanksAs = new CT_DispBlanksAs();
			if (node.Attributes["val"] != null)
			{
				cT_DispBlanksAs.val = (ST_DispBlanksAs)Enum.Parse(typeof(ST_DispBlanksAs), node.Attributes["val"].Value);
			}
			return cT_DispBlanksAs;
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
