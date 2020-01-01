using System;
using System.Collections.Generic;
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
	public class CT_DPt
	{
		private CT_UnsignedInt idxField;

		private CT_Boolean invertIfNegativeField;

		private CT_Marker markerField;

		private CT_Boolean bubble3DField;

		private CT_UnsignedInt explosionField;

		private CT_ShapeProperties spPrField;

		private CT_PictureOptions pictureOptionsField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_UnsignedInt idx
		{
			get
			{
				return idxField;
			}
			set
			{
				idxField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Boolean invertIfNegative
		{
			get
			{
				return invertIfNegativeField;
			}
			set
			{
				invertIfNegativeField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Marker marker
		{
			get
			{
				return markerField;
			}
			set
			{
				markerField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Boolean bubble3D
		{
			get
			{
				return bubble3DField;
			}
			set
			{
				bubble3DField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_UnsignedInt explosion
		{
			get
			{
				return explosionField;
			}
			set
			{
				explosionField = value;
			}
		}

		[XmlElement(Order = 5)]
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

		[XmlElement(Order = 6)]
		public CT_PictureOptions pictureOptions
		{
			get
			{
				return pictureOptionsField;
			}
			set
			{
				pictureOptionsField = value;
			}
		}

		[XmlElement(Order = 7)]
		public List<CT_Extension> extLst
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

		public static CT_DPt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DPt cT_DPt = new CT_DPt();
			cT_DPt.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idx")
				{
					cT_DPt.idx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "invertIfNegative")
				{
					cT_DPt.invertIfNegative = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "marker")
				{
					cT_DPt.marker = CT_Marker.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bubble3D")
				{
					cT_DPt.bubble3D = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "explosion")
				{
					cT_DPt.explosion = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_DPt.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pictureOptions")
				{
					cT_DPt.pictureOptions = CT_PictureOptions.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_DPt.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_DPt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (idx != null)
			{
				idx.Write(sw, "idx");
			}
			if (invertIfNegative != null)
			{
				invertIfNegative.Write(sw, "invertIfNegative");
			}
			if (marker != null)
			{
				marker.Write(sw, "marker");
			}
			if (bubble3D != null)
			{
				bubble3D.Write(sw, "bubble3D");
			}
			if (explosion != null)
			{
				explosion.Write(sw, "explosion");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (pictureOptions != null)
			{
				pictureOptions.Write(sw, "pictureOptions");
			}
			if (extLst != null)
			{
				foreach (CT_Extension item in extLst)
				{
					item.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
