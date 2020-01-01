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
	public class CT_DepthPercent
	{
		private ushort valField;

		[XmlAttribute]
		[DefaultValue(typeof(ushort), "100")]
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

		public CT_DepthPercent()
		{
			valField = 100;
		}

		public static CT_DepthPercent Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DepthPercent cT_DepthPercent = new CT_DepthPercent();
			if (node.Attributes["val"] != null)
			{
				cT_DepthPercent.val = XmlHelper.ReadUShort(node.Attributes["val"]);
			}
			return cT_DepthPercent;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			if (valField != 100)
			{
				XmlHelper.WriteAttribute(sw, "val", val);
			}
			sw.Write(">");
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
