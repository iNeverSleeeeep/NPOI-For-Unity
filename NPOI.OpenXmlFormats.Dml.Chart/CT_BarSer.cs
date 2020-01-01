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
	public class CT_BarSer
	{
		private CT_UnsignedInt idxField;

		private CT_UnsignedInt orderField;

		private CT_SerTx txField;

		private CT_ShapeProperties spPrField;

		private CT_Boolean invertIfNegativeField;

		private CT_PictureOptions pictureOptionsField;

		private List<CT_DPt> dPtField;

		private CT_DLbls dLblsField;

		private List<CT_Trendline> trendlineField;

		private CT_ErrBars errBarsField;

		private CT_AxDataSource catField;

		private CT_NumDataSource valField;

		private CT_Shape shapeField;

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

		[XmlElement(Order = 5)]
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

		[XmlElement("dPt", Order = 6)]
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

		[XmlElement(Order = 7)]
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

		[XmlElement("trendline", Order = 8)]
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

		[XmlElement(Order = 9)]
		public CT_ErrBars errBars
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

		[XmlElement(Order = 10)]
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

		[XmlElement(Order = 11)]
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

		[XmlElement(Order = 12)]
		public CT_Shape shape
		{
			get
			{
				return shapeField;
			}
			set
			{
				shapeField = value;
			}
		}

		[XmlElement(Order = 13)]
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

		public static CT_BarSer Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BarSer cT_BarSer = new CT_BarSer();
			cT_BarSer.dPt = new List<CT_DPt>();
			cT_BarSer.trendline = new List<CT_Trendline>();
			cT_BarSer.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "idx")
				{
					cT_BarSer.idx = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "order")
				{
					cT_BarSer.order = CT_UnsignedInt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tx")
				{
					cT_BarSer.tx = CT_SerTx.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_BarSer.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "invertIfNegative")
				{
					cT_BarSer.invertIfNegative = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pictureOptions")
				{
					cT_BarSer.pictureOptions = CT_PictureOptions.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dLbls")
				{
					cT_BarSer.dLbls = CT_DLbls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "errBars")
				{
					cT_BarSer.errBars = CT_ErrBars.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cat")
				{
					cT_BarSer.cat = CT_AxDataSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "val")
				{
					cT_BarSer.val = CT_NumDataSource.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shape")
				{
					cT_BarSer.shape = CT_Shape.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dPt")
				{
					cT_BarSer.dPt.Add(CT_DPt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "trendline")
				{
					cT_BarSer.trendline.Add(CT_Trendline.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_BarSer.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_BarSer;
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
			if (invertIfNegative != null)
			{
				invertIfNegative.Write(sw, "invertIfNegative");
			}
			if (pictureOptions != null)
			{
				pictureOptions.Write(sw, "pictureOptions");
			}
			if (dLbls != null)
			{
				dLbls.Write(sw, "dLbls");
			}
			if (errBars != null)
			{
				errBars.Write(sw, "errBars");
			}
			if (cat != null)
			{
				cat.Write(sw, "cat");
			}
			if (val != null)
			{
				val.Write(sw, "val");
			}
			if (shape != null)
			{
				shape.Write(sw, "shape");
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
			if (extLst != null)
			{
				foreach (CT_Extension item3 in extLst)
				{
					item3.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}
	}
}
