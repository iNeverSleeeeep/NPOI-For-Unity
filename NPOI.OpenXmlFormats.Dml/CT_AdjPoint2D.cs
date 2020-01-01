using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_AdjPoint2D
	{
		private string xField;

		private string yField;

		[XmlAttribute]
		public string x
		{
			get
			{
				return xField;
			}
			set
			{
				xField = value;
			}
		}

		[XmlAttribute]
		public string y
		{
			get
			{
				return yField;
			}
			set
			{
				yField = value;
			}
		}

		public static CT_AdjPoint2D Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_AdjPoint2D cT_AdjPoint2D = new CT_AdjPoint2D();
			cT_AdjPoint2D.x = XmlHelper.ReadString(node.Attributes["x"]);
			cT_AdjPoint2D.y = XmlHelper.ReadString(node.Attributes["y"]);
			return cT_AdjPoint2D;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "x", x);
			XmlHelper.WriteAttribute(sw, "y", y);
			sw.Write(">");
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
