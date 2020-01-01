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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_Perspective
	{
		private byte valField;

		[DefaultValue(typeof(byte), "30")]
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

		public CT_Perspective()
		{
			valField = 30;
		}

		public static CT_Perspective Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Perspective cT_Perspective = new CT_Perspective();
			if (node.Attributes["val"] != null)
			{
				cT_Perspective.val = XmlHelper.ReadByte(node.Attributes["val"]);
			}
			return cT_Perspective;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			if (val != 30)
			{
				XmlHelper.WriteAttribute(sw, "val", val);
			}
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
