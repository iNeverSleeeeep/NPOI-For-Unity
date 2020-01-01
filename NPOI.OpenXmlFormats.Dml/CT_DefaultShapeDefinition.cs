using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_DefaultShapeDefinition
	{
		private CT_ShapeProperties spPrField;

		private CT_TextBodyProperties bodyPrField;

		private CT_TextListStyle lstStyleField;

		private CT_ShapeStyle styleField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TextBodyProperties bodyPr
		{
			get
			{
				return bodyPrField;
			}
			set
			{
				bodyPrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TextListStyle lstStyle
		{
			get
			{
				return lstStyleField;
			}
			set
			{
				lstStyleField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_ShapeStyle style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[XmlElement(Order = 4)]
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

		public static CT_DefaultShapeDefinition Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DefaultShapeDefinition cT_DefaultShapeDefinition = new CT_DefaultShapeDefinition();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "spPr")
				{
					cT_DefaultShapeDefinition.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bodyPr")
				{
					cT_DefaultShapeDefinition.bodyPr = CT_TextBodyProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lstStyle")
				{
					cT_DefaultShapeDefinition.lstStyle = CT_TextListStyle.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "style")
				{
					cT_DefaultShapeDefinition.style = CT_ShapeStyle.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_DefaultShapeDefinition.extLst = CT_OfficeArtExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_DefaultShapeDefinition;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<a:{0}", nodeName));
			sw.Write(">");
			if (spPr != null)
			{
				spPr.Write(sw, "a:spPr");
			}
			if (bodyPr != null)
			{
				bodyPr.Write(sw, "bodyPr");
			}
			if (lstStyle != null)
			{
				lstStyle.Write(sw, "lstStyle");
			}
			if (style != null)
			{
				style.Write(sw, "style");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</a:{0}>", nodeName));
		}
	}
}
