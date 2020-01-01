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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", ElementName = "chart", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_Chart
	{
		private CT_Title titleField;

		private CT_Boolean autoTitleDeletedField;

		private List<CT_PivotFmt> pivotFmtsField;

		private CT_View3D view3DField;

		private CT_Surface floorField;

		private CT_Surface sideWallField;

		private CT_Surface backWallField;

		private CT_PlotArea plotAreaField;

		private CT_Legend legendField;

		private CT_Boolean plotVisOnlyField;

		private CT_DispBlanksAs dispBlanksAsField;

		private CT_Boolean showDLblsOverMaxField;

		private List<CT_Extension> extLstField;

		[XmlElement(Order = 0)]
		public CT_Title title
		{
			get
			{
				return titleField;
			}
			set
			{
				titleField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Boolean autoTitleDeleted
		{
			get
			{
				return autoTitleDeletedField;
			}
			set
			{
				autoTitleDeletedField = value;
			}
		}

		[XmlElement(Order = 2)]
		public List<CT_PivotFmt> pivotFmts
		{
			get
			{
				return pivotFmtsField;
			}
			set
			{
				pivotFmtsField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_View3D view3D
		{
			get
			{
				return view3DField;
			}
			set
			{
				view3DField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_Surface floor
		{
			get
			{
				return floorField;
			}
			set
			{
				floorField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Surface sideWall
		{
			get
			{
				return sideWallField;
			}
			set
			{
				sideWallField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Surface backWall
		{
			get
			{
				return backWallField;
			}
			set
			{
				backWallField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_PlotArea plotArea
		{
			get
			{
				return plotAreaField;
			}
			set
			{
				plotAreaField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_Legend legend
		{
			get
			{
				return legendField;
			}
			set
			{
				legendField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Boolean plotVisOnly
		{
			get
			{
				return plotVisOnlyField;
			}
			set
			{
				plotVisOnlyField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_DispBlanksAs dispBlanksAs
		{
			get
			{
				return dispBlanksAsField;
			}
			set
			{
				dispBlanksAsField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_Boolean showDLblsOverMax
		{
			get
			{
				return showDLblsOverMaxField;
			}
			set
			{
				showDLblsOverMaxField = value;
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

		public static CT_Chart Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Chart cT_Chart = new CT_Chart();
			cT_Chart.pivotFmts = new List<CT_PivotFmt>();
			cT_Chart.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "title")
				{
					cT_Chart.title = CT_Title.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoTitleDeleted")
				{
					cT_Chart.autoTitleDeleted = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "view3D")
				{
					cT_Chart.view3D = CT_View3D.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "floor")
				{
					cT_Chart.floor = CT_Surface.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sideWall")
				{
					cT_Chart.sideWall = CT_Surface.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "backWall")
				{
					cT_Chart.backWall = CT_Surface.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "plotArea")
				{
					cT_Chart.plotArea = CT_PlotArea.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "legend")
				{
					cT_Chart.legend = CT_Legend.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "plotVisOnly")
				{
					cT_Chart.plotVisOnly = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dispBlanksAs")
				{
					cT_Chart.dispBlanksAs = CT_DispBlanksAs.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showDLblsOverMax")
				{
					cT_Chart.showDLblsOverMax = CT_Boolean.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pivotFmts")
				{
					cT_Chart.pivotFmts.Add(CT_PivotFmt.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Chart.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_Chart;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (title != null)
			{
				title.Write(sw, "title");
			}
			if (autoTitleDeleted != null)
			{
				autoTitleDeleted.Write(sw, "autoTitleDeleted");
			}
			if (view3D != null)
			{
				view3D.Write(sw, "view3D");
			}
			if (floor != null)
			{
				floor.Write(sw, "floor");
			}
			if (sideWall != null)
			{
				sideWall.Write(sw, "sideWall");
			}
			if (backWall != null)
			{
				backWall.Write(sw, "backWall");
			}
			if (plotArea != null)
			{
				plotArea.Write(sw, "plotArea");
			}
			if (legend != null)
			{
				legend.Write(sw, "legend");
			}
			if (plotVisOnly != null)
			{
				plotVisOnly.Write(sw, "plotVisOnly");
			}
			if (dispBlanksAs != null)
			{
				dispBlanksAs.Write(sw, "dispBlanksAs");
			}
			if (showDLblsOverMax != null)
			{
				showDLblsOverMax.Write(sw, "showDLblsOverMax");
			}
			if (pivotFmts != null)
			{
				foreach (CT_PivotFmt pivotFmt in pivotFmts)
				{
					pivotFmt.Write(sw, "pivotFmts");
				}
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

		public CT_Boolean AddNewPlotVisOnly()
		{
			plotVisOnlyField = new CT_Boolean();
			return plotVisOnlyField;
		}

		public CT_PlotArea AddNewPlotArea()
		{
			plotAreaField = new CT_PlotArea();
			return plotAreaField;
		}

		public bool IsSetTitle()
		{
			return titleField != null;
		}

		public bool IsSetLegend()
		{
			return legendField != null;
		}

		public void unsetLegend()
		{
			legendField = null;
		}

		public CT_Legend AddNewLegend()
		{
			if (legendField == null)
			{
				legendField = new CT_Legend();
			}
			return legendField;
		}
	}
}
