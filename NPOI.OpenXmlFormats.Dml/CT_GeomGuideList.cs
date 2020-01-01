using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DebuggerStepThrough]
	public class CT_GeomGuideList
	{
		private List<CT_GeomGuide> gdField;

		[XmlElement("gd", Order = 0)]
		public List<CT_GeomGuide> gd
		{
			get
			{
				return gdField;
			}
			set
			{
				gdField = value;
			}
		}

		internal static CT_GeomGuideList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			CT_GeomGuideList cT_GeomGuideList = new CT_GeomGuideList();
			cT_GeomGuideList.gdField = new List<CT_GeomGuide>();
			if (node.ChildNodes != null)
			{
				foreach (XmlNode childNode in node.ChildNodes)
				{
					if (childNode.LocalName == "gd")
					{
						cT_GeomGuideList.gdField.Add(CT_GeomGuide.Parse(childNode, namespaceManager));
					}
				}
				return cT_GeomGuideList;
			}
			return cT_GeomGuideList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write("<a:{0}>", nodeName);
			if (gdField != null)
			{
				foreach (CT_GeomGuide item in gdField)
				{
					item.Write(sw, "gd");
				}
			}
			sw.Write("</a:{0}>", nodeName);
		}
	}
}
