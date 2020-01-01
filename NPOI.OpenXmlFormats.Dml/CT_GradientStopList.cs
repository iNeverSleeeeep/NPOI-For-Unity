using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_GradientStopList
	{
		private List<CT_GradientStop> gsField;

		[XmlElement("gs", Order = 0)]
		public List<CT_GradientStop> gs
		{
			get
			{
				return gsField;
			}
			set
			{
				gsField = value;
			}
		}

		public static CT_GradientStopList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GradientStopList cT_GradientStopList = new CT_GradientStopList();
			cT_GradientStopList.gs = new List<CT_GradientStop>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "gs")
				{
					cT_GradientStopList.gs.Add(CT_GradientStop.Parse(childNode, namespaceManager));
				}
			}
			return cT_GradientStopList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (gs != null)
			{
				foreach (CT_GradientStop g in gs)
				{
					g.Write(sw, "gs");
				}
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}

		public CT_GradientStopList()
		{
			gsField = new List<CT_GradientStop>();
		}
	}
}
