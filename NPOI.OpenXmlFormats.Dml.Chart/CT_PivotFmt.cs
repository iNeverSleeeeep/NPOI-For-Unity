using System;
using System.Collections.Generic;
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
	public class CT_PivotFmt
	{
		private CT_UnsignedInt idxField;

		private CT_ShapeProperties spPrField;

		private CT_TextBody txPrField;

		private CT_Marker markerField;

		private CT_DLbl dLblField;

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

		[XmlElement(Order = 2)]
		public CT_TextBody txPr
		{
			get
			{
				return txPrField;
			}
			set
			{
				txPrField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
		public CT_DLbl dLbl
		{
			get
			{
				return dLblField;
			}
			set
			{
				dLblField = value;
			}
		}

		[XmlElement(Order = 5)]
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

		public static CT_PivotFmt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PivotFmt cT_PivotFmt = new CT_PivotFmt();
			cT_PivotFmt.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idx")
				{
					cT_PivotFmt.idx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_PivotFmt.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "txPr")
				{
					cT_PivotFmt.txPr = CT_TextBody.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "marker")
				{
					cT_PivotFmt.marker = CT_Marker.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbl")
				{
					cT_PivotFmt.dLbl = CT_DLbl.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_PivotFmt.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_PivotFmt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (idx != null)
			{
				idx.Write(sw, "idx");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (txPr != null)
			{
				txPr.Write(sw, "txPr");
			}
			if (marker != null)
			{
				marker.Write(sw, "marker");
			}
			if (dLbl != null)
			{
				dLbl.Write(sw, "dLbl");
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
