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
	public class CT_ScatterSer
	{
		private CT_UnsignedInt idxField;

		private CT_UnsignedInt orderField;

		private CT_SerTx txField;

		private CT_ShapeProperties spPrField;

		private CT_Marker markerField;

		private List<CT_DPt> dPtField;

		private CT_DLbls dLblsField;

		private List<CT_Trendline> trendlineField;

		private List<CT_ErrBars> errBarsField;

		private CT_AxDataSource xValField;

		private CT_NumDataSource yValField;

		private CT_Boolean smoothField;

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

		[XmlElement("trendline", Order = 7)]
		public List<CT_Trendline> trendline
		{
			get
			{
				return trendlineField;
			}
			set
			{
				trendlineField = value;
			}
		}

		[XmlElement("errBars", Order = 8)]
		public List<CT_ErrBars> errBars
		{
			get
			{
				return errBarsField;
			}
			set
			{
				errBarsField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_AxDataSource xVal
		{
			get
			{
				return xValField;
			}
			set
			{
				xValField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_NumDataSource yVal
		{
			get
			{
				return yValField;
			}
			set
			{
				yValField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_Boolean smooth
		{
			get
			{
				return smoothField;
			}
			set
			{
				smoothField = value;
			}
		}

		[XmlElement(Order = 12)]
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

		public static CT_ScatterSer Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ScatterSer cT_ScatterSer = new CT_ScatterSer();
			cT_ScatterSer.dPt = new List<CT_DPt>();
			cT_ScatterSer.trendline = new List<CT_Trendline>();
			cT_ScatterSer.errBars = new List<CT_ErrBars>();
			cT_ScatterSer.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idx")
				{
					cT_ScatterSer.idx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "order")
				{
					cT_ScatterSer.order = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tx")
				{
					cT_ScatterSer.tx = CT_SerTx.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_ScatterSer.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "marker")
				{
					cT_ScatterSer.marker = CT_Marker.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_ScatterSer.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "xVal")
				{
					cT_ScatterSer.xVal = CT_AxDataSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "yVal")
				{
					cT_ScatterSer.yVal = CT_NumDataSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "smooth")
				{
					cT_ScatterSer.smooth = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dPt")
				{
					cT_ScatterSer.dPt.Add(CT_DPt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "trendline")
				{
					cT_ScatterSer.trendline.Add(CT_Trendline.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "errBars")
				{
					cT_ScatterSer.errBars.Add(CT_ErrBars.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_ScatterSer.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_ScatterSer;
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
			if (xVal != null)
			{
				xVal.Write(sw, "xVal");
			}
			if (yVal != null)
			{
				yVal.Write(sw, "yVal");
			}
			if (smooth != null)
			{
				smooth.Write(sw, "smooth");
			}
			if (dPt != null)
			{
				foreach (CT_DPt item in dPt)
				{
					item.Write(sw, "dPt");
				}
			}
			if (trendline != null)
			{
				foreach (CT_Trendline item2 in trendline)
				{
					item2.Write(sw, "trendline");
				}
			}
			if (errBars != null)
			{
				foreach (CT_ErrBars errBar in errBars)
				{
					errBar.Write(sw, "errBars");
				}
			}
			if (extLst != null)
			{
				foreach (CT_Extension item3 in extLst)
				{
					item3.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_UnsignedInt AddNewIdx()
		{
			idxField = new CT_UnsignedInt();
			return idxField;
		}

		public CT_UnsignedInt AddNewOrder()
		{
			orderField = new CT_UnsignedInt();
			return orderField;
		}

		public CT_AxDataSource AddNewXVal()
		{
			xValField = new CT_AxDataSource();
			return xValField;
		}

		public CT_NumDataSource AddNewYVal()
		{
			yValField = new CT_NumDataSource();
			return yValField;
		}
	}
}
