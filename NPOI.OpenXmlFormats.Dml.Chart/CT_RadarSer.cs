using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public class CT_RadarSer
	{
		private CT_UnsignedInt idxField;

		private CT_UnsignedInt orderField;

		private CT_SerTx txField;

		private CT_ShapeProperties spPrField;

		private CT_Marker markerField;

		private List<CT_DPt> dPtField;

		private CT_DLbls dLblsField;

		private CT_AxDataSource catField;

		private CT_NumDataSource valField;

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
		public CT_UnsignedInt order
		{
			get
			{
				return orderField;
			}
			set
			{
				orderField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_SerTx tx
		{
			get
			{
				return txField;
			}
			set
			{
				txField = value;
			}
		}

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement("dPt", Order = 5)]
		public List<CT_DPt> dPt
		{
			get
			{
				return dPtField;
			}
			set
			{
				dPtField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_DLbls dLbls
		{
			get
			{
				return dLblsField;
			}
			set
			{
				dLblsField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_AxDataSource cat
		{
			get
			{
				return catField;
			}
			set
			{
				catField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_NumDataSource val
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

		[XmlElement(Order = 9)]
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

		public static CT_RadarSer Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_RadarSer cT_RadarSer = new CT_RadarSer();
			cT_RadarSer.dPt = new List<CT_DPt>();
			cT_RadarSer.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idx")
				{
					cT_RadarSer.idx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "order")
				{
					cT_RadarSer.order = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tx")
				{
					cT_RadarSer.tx = CT_SerTx.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_RadarSer.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "marker")
				{
					cT_RadarSer.marker = CT_Marker.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_RadarSer.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cat")
				{
					cT_RadarSer.cat = CT_AxDataSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "val")
				{
					cT_RadarSer.val = CT_NumDataSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dPt")
				{
					cT_RadarSer.dPt.Add(CT_DPt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_RadarSer.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_RadarSer;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (idx != null)
			{
				idx.Write(sw, "idx");
			}
			if (order != null)
			{
				order.Write(sw, "order");
			}
			if (tx != null)
			{
				tx.Write(sw, "tx");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (marker != null)
			{
				marker.Write(sw, "marker");
			}
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (cat != null)
			{
				cat.Write(sw, "cat");
			}
			if (val != null)
			{
				val.Write(sw, "val");
			}
			if (dPt != null)
			{
				foreach (CT_DPt item in dPt)
				{
					item.Write(sw, "dPt");
				}
			}
			if (extLst != null)
			{
				foreach (CT_Extension item2 in extLst)
				{
					item2.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
