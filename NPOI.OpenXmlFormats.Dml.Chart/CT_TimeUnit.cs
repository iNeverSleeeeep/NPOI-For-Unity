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
	public class CT_TimeUnit
	{
		private ST_TimeUnit valField;

		[XmlAttribute]
		[DefaultValue(ST_TimeUnit.days)]
		public ST_TimeUnit val
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

		public static CT_TimeUnit Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TimeUnit cT_TimeUnit = new CT_TimeUnit();
			if (node.Attributes["val"] != null)
			{
				cT_TimeUnit.val = (ST_TimeUnit)Enum.Parse(typeof(ST_TimeUnit), node.Attributes["val"].Value);
			}
			return cT_TimeUnit;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_TimeUnit()
		{
			valField = ST_TimeUnit.days;
		}
	}
}
