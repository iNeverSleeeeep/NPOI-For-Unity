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
	public class CT_Trendline
	{
		private string nameField;

		private CT_ShapeProperties spPrField;

		private CT_TrendlineType trendlineTypeField;

		private CT_Order orderField;

		private CT_Period periodField;

		private CT_Double forwardField;

		private CT_Double backwardField;

		private CT_Double interceptField;

		private CT_Boolean dispRSqrField;

		private CT_Boolean dispEqField;

		private CT_TrendlineLbl trendlineLblField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
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
		public CT_TrendlineType trendlineType
		{
			get
			{
				return trendlineTypeField;
			}
			set
			{
				trendlineTypeField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Order order
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

		[XmlElement(Order = 4)]
		public CT_Period period
		{
			get
			{
				return periodField;
			}
			set
			{
				periodField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Double forward
		{
			get
			{
				return forwardField;
			}
			set
			{
				forwardField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Double backward
		{
			get
			{
				return backwardField;
			}
			set
			{
				backwardField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_Double intercept
		{
			get
			{
				return interceptField;
			}
			set
			{
				interceptField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_Boolean dispRSqr
		{
			get
			{
				return dispRSqrField;
			}
			set
			{
				dispRSqrField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Boolean dispEq
		{
			get
			{
				return dispEqField;
			}
			set
			{
				dispEqField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_TrendlineLbl trendlineLbl
		{
			get
			{
				return trendlineLblField;
			}
			set
			{
				trendlineLblField = value;
			}
		}

		[XmlElement(Order = 11)]
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

		public static CT_Trendline Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Trendline cT_Trendline = new CT_Trendline();
			cT_Trendline.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "name")
				{
					cT_Trendline.name = childNode.InnerText;
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_Trendline.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "trendlineType")
				{
					cT_Trendline.trendlineType = CT_TrendlineType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "order")
				{
					cT_Trendline.order = CT_Order.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "period")
				{
					cT_Trendline.period = CT_Period.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "forward")
				{
					cT_Trendline.forward = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "backward")
				{
					cT_Trendline.backward = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "intercept")
				{
					cT_Trendline.intercept = CT_Double.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dispRSqr")
				{
					cT_Trendline.dispRSqr = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dispEq")
				{
					cT_Trendline.dispEq = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "trendlineLbl")
				{
					cT_Trendline.trendlineLbl = CT_TrendlineLbl.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Trendline.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Trendline;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (name != null)
			{
				sw.Write(string.Format("<name>{0}</name>", name));
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (trendlineType != null)
			{
				trendlineType.Write(sw, "trendlineType");
			}
			if (order != null)
			{
				order.Write(sw, "order");
			}
			if (period != null)
			{
				period.Write(sw, "period");
			}
			if (forward != null)
			{
				forward.Write(sw, "forward");
			}
			if (backward != null)
			{
				backward.Write(sw, "backward");
			}
			if (intercept != null)
			{
				intercept.Write(sw, "intercept");
			}
			if (dispRSqr != null)
			{
				dispRSqr.Write(sw, "dispRSqr");
			}
			if (dispEq != null)
			{
				dispEq.Write(sw, "dispEq");
			}
			if (trendlineLbl != null)
			{
				trendlineLbl.Write(sw, "trendlineLbl");
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
