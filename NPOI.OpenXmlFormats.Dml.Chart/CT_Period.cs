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
	public class CT_Period
	{
		private byte valField;

		[XmlAttribute]
		[DefaultValue(typeof(byte), "2")]
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

		public CT_Period()
		{
			valField = 2;
		}

		public static CT_Period Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Period cT_Period = new CT_Period();
			if (node.Attributes["val"] != null)
			{
				cT_Period.val = XmlHelper.ReadByte(node.Attributes["val"]);
			}
			return cT_Period;
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
