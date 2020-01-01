using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	[XmlRoot("inline", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing", IsNullable = false)]
	[DesignerCategory("code")]
	public class CT_NonVisualGraphicFrameProperties
	{
		private CT_GraphicalObjectFrameLocking graphicFrameLocksField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_GraphicalObjectFrameLocking graphicFrameLocks
		{
			get
			{
				return graphicFrameLocksField;
			}
			set
			{
				graphicFrameLocksField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public static CT_NonVisualGraphicFrameProperties Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NonVisualGraphicFrameProperties cT_NonVisualGraphicFrameProperties = new CT_NonVisualGraphicFrameProperties();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "graphicFrameLocks")
				{
					cT_NonVisualGraphicFrameProperties.graphicFrameLocks = CT_GraphicalObjectFrameLocking.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_NonVisualGraphicFrameProperties.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_NonVisualGraphicFrameProperties;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<wp:{0}", nodeName));
			sw.Write(">");
			if (graphicFrameLocks != null)
			{
				graphicFrameLocks.Write(sw, "graphicFrameLocks");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</wp:{0}>", nodeName));
		}
	}
}
