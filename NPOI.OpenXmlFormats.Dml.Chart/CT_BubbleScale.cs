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
	public class CT_BubbleScale
	{
		private uint valField;

		[DefaultValue(typeof(uint), "100")]
		[XmlAttribute]
		public uint val
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

		public CT_BubbleScale()
		{
			valField = 100u;
		}

		public static CT_BubbleScale Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BubbleScale cT_BubbleScale = new CT_BubbleScale();
			if (node.Attributes["val"] != null)
			{
				cT_BubbleScale.val = XmlHelper.ReadUInt(node.Attributes["val"]);
			}
			return cT_BubbleScale;
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
