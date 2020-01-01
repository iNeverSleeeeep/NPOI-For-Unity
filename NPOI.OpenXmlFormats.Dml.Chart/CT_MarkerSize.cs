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
	public class CT_MarkerSize
	{
		private byte valField;

		[DefaultValue(typeof(byte), "5")]
		[XmlAttribute]
		public byte val
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

		public CT_MarkerSize()
		{
			valField = 5;
		}

		public static CT_MarkerSize Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MarkerSize cT_MarkerSize = new CT_MarkerSize();
			cT_MarkerSize.val = XmlHelper.ReadByte(node.Attributes["val"]);
			return cT_MarkerSize;
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
