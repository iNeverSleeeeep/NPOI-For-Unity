using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_GraphicalObject
	{
		private CT_GraphicalObjectData graphicDataField;

		[XmlElement(Order = 0)]
		public CT_GraphicalObjectData graphicData
		{
			get
			{
				return graphicDataField;
			}
			set
			{
				graphicDataField = value;
			}
		}

		public CT_GraphicalObjectData AddNewGraphicData()
		{
			graphicDataField = new CT_GraphicalObjectData();
			return graphicDataField;
		}

		public static CT_GraphicalObject Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_GraphicalObject cT_GraphicalObject = new CT_GraphicalObject();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "graphicData")
				{
					cT_GraphicalObject.graphicData = CT_GraphicalObjectData.Parse(childNode, namespaceManager);
				}
			}
			return cT_GraphicalObject;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (graphicData != null)
			{
				graphicData.Write(sw, "graphicData");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
