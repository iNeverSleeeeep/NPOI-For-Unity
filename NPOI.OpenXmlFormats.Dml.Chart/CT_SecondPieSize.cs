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
	public class CT_SecondPieSize
	{
		private ushort valField;

		[XmlAttribute]
		[DefaultValue(typeof(ushort), "75")]
		public ushort val
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

		public CT_SecondPieSize()
		{
			valField = 75;
		}

		public static CT_SecondPieSize Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SecondPieSize cT_SecondPieSize = new CT_SecondPieSize();
			if (node.Attributes["val"] != null)
			{
				cT_SecondPieSize.val = XmlHelper.ReadUShort(node.Attributes["val"]);
			}
			return cT_SecondPieSize;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "val", val);
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
