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
	public class CT_BarDir
	{
		private ST_BarDir valField;

		[XmlAttribute]
		[DefaultValue(ST_BarDir.col)]
		public ST_BarDir val
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

		public CT_BarDir()
		{
			valField = ST_BarDir.col;
		}

		public static CT_BarDir Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BarDir cT_BarDir = new CT_BarDir();
			if (node.Attributes["val"] != null)
			{
				cT_BarDir.val = (ST_BarDir)Enum.Parse(typeof(ST_BarDir), node.Attributes["val"].Value);
			}
			return cT_BarDir;
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
