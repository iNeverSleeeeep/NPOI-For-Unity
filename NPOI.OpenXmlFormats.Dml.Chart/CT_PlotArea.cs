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
	public class CT_PlotArea
	{
		private CT_Layout layoutField;

		private CT_DTable dTableField;

		private CT_ShapeProperties spPrField;

		private List<CT_Extension> extLstField;

		private List<CT_ValAx> valAxField;

		private List<CT_SerAx> serAxField;

		private List<CT_CatAx> catAxField;

		private List<CT_DateAx> dateAxField;

		private List<CT_SurfaceChart> surfaceChartField;

		private List<CT_LineChart> lineChartField;

		private List<CT_BubbleChart> bubbleChartField;

		private List<CT_Bar3DChart> bar3DChartField;

		private List<CT_Area3DChart> area3DChartField;

		private List<CT_DoughnutChart> doughnutChartField;

		private List<CT_Pie3DChart> pie3DChartField;

		private List<CT_Line3DChart> line3DChartField;

		private List<CT_Surface3DChart> surface3DChartField;

		private List<CT_BarChart> barChartField;

		private List<CT_RadarChart> radarChartField;

		private List<CT_AreaChart> areaChartField;

		private List<CT_ScatterChart> scatterChartField;

		private List<CT_OfPieChart> ofPieChartField;

		private List<CT_PieChart> pieChartField;

		private List<CT_StockChart> stockChartField;

		[XmlElement(Order = 0)]
		public CT_Layout layout
		{
			get
			{
				return layoutField;
			}
			set
			{
				layoutField = value;
			}
		}

		public List<CT_SurfaceChart> surfaceChart
		{
			get
			{
				return surfaceChartField;
			}
			set
			{
				surfaceChartField = value;
			}
		}

		public List<CT_LineChart> lineChart
		{
			get
			{
				return lineChartField;
			}
			set
			{
				lineChartField = value;
			}
		}

		public List<CT_BubbleChart> bubbleChart
		{
			get
			{
				return bubbleChartField;
			}
			set
			{
				bubbleChartField = value;
			}
		}

		public List<CT_Bar3DChart> bar3DChart
		{
			get
			{
				return bar3DChartField;
			}
			set
			{
				bar3DChartField = value;
			}
		}

		public List<CT_Area3DChart> area3DChart
		{
			get
			{
				return area3DChartField;
			}
			set
			{
				area3DChartField = value;
			}
		}

		public List<CT_DoughnutChart> doughnutChart
		{
			get
			{
				return doughnutChartField;
			}
			set
			{
				doughnutChartField = value;
			}
		}

		public List<CT_Pie3DChart> pie3DChart
		{
			get
			{
				return pie3DChartField;
			}
			set
			{
				pie3DChartField = value;
			}
		}

		public List<CT_Line3DChart> line3DChart
		{
			get
			{
				return line3DChartField;
			}
			set
			{
				line3DChartField = value;
			}
		}

		public List<CT_Surface3DChart> surface3DChart
		{
			get
			{
				return surface3DChartField;
			}
			set
			{
				surface3DChartField = value;
			}
		}

		public List<CT_BarChart> barChart
		{
			get
			{
				return barChartField;
			}
			set
			{
				barChartField = value;
			}
		}

		public List<CT_RadarChart> radarChart
		{
			get
			{
				return radarChartField;
			}
			set
			{
				radarChartField = value;
			}
		}

		public List<CT_AreaChart> areaChart
		{
			get
			{
				return areaChartField;
			}
			set
			{
				areaChartField = value;
			}
		}

		public List<CT_ScatterChart> scatterChart
		{
			get
			{
				return scatterChartField;
			}
			set
			{
				scatterChartField = value;
			}
		}

		public List<CT_OfPieChart> ofPieChart
		{
			get
			{
				return ofPieChartField;
			}
			set
			{
				ofPieChartField = value;
			}
		}

		public List<CT_PieChart> pieChart
		{
			get
			{
				return pieChartField;
			}
			set
			{
				pieChartField = value;
			}
		}

		public List<CT_StockChart> stockChart
		{
			get
			{
				return stockChartField;
			}
			set
			{
				stockChartField = value;
			}
		}

		[XmlElement(Order = 21)]
		public List<CT_ValAx> valAx
		{
			get
			{
				return valAxField;
			}
			set
			{
				valAxField = value;
			}
		}

		[XmlElement(Order = 22)]
		public List<CT_SerAx> serAx
		{
			get
			{
				return serAxField;
			}
			set
			{
				serAxField = value;
			}
		}

		[XmlElement(Order = 23)]
		public List<CT_DateAx> dateAx
		{
			get
			{
				return dateAxField;
			}
			set
			{
				dateAxField = value;
			}
		}

		[XmlElement(Order = 24)]
		public List<CT_CatAx> catAx
		{
			get
			{
				return catAxField;
			}
			set
			{
				catAxField = value;
			}
		}

		[XmlElement(Order = 25)]
		public CT_DTable dTable
		{
			get
			{
				return dTableField;
			}
			set
			{
				dTableField = value;
			}
		}

		[XmlElement(Order = 26)]
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

		[XmlElement(Order = 27)]
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

		public static CT_PlotArea Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PlotArea cT_PlotArea = new CT_PlotArea();
			cT_PlotArea.surfaceChart = new List<CT_SurfaceChart>();
			cT_PlotArea.lineChart = new List<CT_LineChart>();
			cT_PlotArea.bubbleChart = new List<CT_BubbleChart>();
			cT_PlotArea.bar3DChart = new List<CT_Bar3DChart>();
			cT_PlotArea.area3DChart = new List<CT_Area3DChart>();
			cT_PlotArea.doughnutChart = new List<CT_DoughnutChart>();
			cT_PlotArea.pie3DChart = new List<CT_Pie3DChart>();
			cT_PlotArea.line3DChart = new List<CT_Line3DChart>();
			cT_PlotArea.surface3DChart = new List<CT_Surface3DChart>();
			cT_PlotArea.barChart = new List<CT_BarChart>();
			cT_PlotArea.radarChart = new List<CT_RadarChart>();
			cT_PlotArea.areaChart = new List<CT_AreaChart>();
			cT_PlotArea.scatterChart = new List<CT_ScatterChart>();
			cT_PlotArea.ofPieChart = new List<CT_OfPieChart>();
			cT_PlotArea.pieChart = new List<CT_PieChart>();
			cT_PlotArea.stockChart = new List<CT_StockChart>();
			cT_PlotArea.valAx = new List<CT_ValAx>();
			cT_PlotArea.serAx = new List<CT_SerAx>();
			cT_PlotArea.dateAx = new List<CT_DateAx>();
			cT_PlotArea.catAx = new List<CT_CatAx>();
			cT_PlotArea.extLst = new List<CT_Extension>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "layout")
				{
					cT_PlotArea.layout = CT_Layout.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dTable")
				{
					cT_PlotArea.dTable = CT_DTable.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spPr")
				{
					cT_PlotArea.spPr = CT_ShapeProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "surfaceChart")
				{
					cT_PlotArea.surfaceChart.Add(CT_SurfaceChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "lineChart")
				{
					cT_PlotArea.lineChart.Add(CT_LineChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "bubbleChart")
				{
					cT_PlotArea.bubbleChart.Add(CT_BubbleChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "bar3DChart")
				{
					cT_PlotArea.bar3DChart.Add(CT_Bar3DChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "area3DChart")
				{
					cT_PlotArea.area3DChart.Add(CT_Area3DChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "doughnutChart")
				{
					cT_PlotArea.doughnutChart.Add(CT_DoughnutChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "pie3DChart")
				{
					cT_PlotArea.pie3DChart.Add(CT_Pie3DChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "line3DChart")
				{
					cT_PlotArea.line3DChart.Add(CT_Line3DChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "surface3DChart")
				{
					cT_PlotArea.surface3DChart.Add(CT_Surface3DChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "barChart")
				{
					cT_PlotArea.barChart.Add(CT_BarChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "radarChart")
				{
					cT_PlotArea.radarChart.Add(CT_RadarChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "areaChart")
				{
					cT_PlotArea.areaChart.Add(CT_AreaChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "scatterChart")
				{
					cT_PlotArea.scatterChart.Add(CT_ScatterChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "ofPieChart")
				{
					cT_PlotArea.ofPieChart.Add(CT_OfPieChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "pieChart")
				{
					cT_PlotArea.pieChart.Add(CT_PieChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "stockChart")
				{
					cT_PlotArea.stockChart.Add(CT_StockChart.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "valAx")
				{
					cT_PlotArea.valAx.Add(CT_ValAx.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "serAx")
				{
					cT_PlotArea.serAx.Add(CT_SerAx.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "dateAx")
				{
					cT_PlotArea.dateAx.Add(CT_DateAx.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "catAx")
				{
					cT_PlotArea.catAx.Add(CT_CatAx.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_PlotArea.extLst.Add(CT_Extension.Parse(childNode, namespaceManager));
				}
			}
			return cT_PlotArea;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<c:{0}", nodeName));
			sw.Write(">");
			if (layout != null)
			{
				layout.Write(sw, "layout");
			}
			if (dTable != null)
			{
				dTable.Write(sw, "dTable");
			}
			if (spPr != null)
			{
				spPr.Write(sw, "spPr");
			}
			if (surfaceChart != null)
			{
				foreach (CT_SurfaceChart item in surfaceChart)
				{
					item.Write(sw, "surfaceChart");
				}
			}
			if (bubbleChart != null)
			{
				foreach (CT_BubbleChart item2 in bubbleChart)
				{
					item2.Write(sw, "bubbleChart");
				}
			}
			if (bar3DChart != null)
			{
				foreach (CT_Bar3DChart item3 in bar3DChart)
				{
					item3.Write(sw, "bar3DChart");
				}
			}
			if (area3DChart != null)
			{
				foreach (CT_Area3DChart item4 in area3DChart)
				{
					item4.Write(sw, "area3DChart");
				}
			}
			if (doughnutChart != null)
			{
				foreach (CT_DoughnutChart item5 in doughnutChart)
				{
					item5.Write(sw, "doughnutChart");
				}
			}
			if (pie3DChart != null)
			{
				foreach (CT_Pie3DChart item6 in pie3DChart)
				{
					item6.Write(sw, "pie3DChart");
				}
			}
			if (line3DChart != null)
			{
				foreach (CT_Line3DChart item7 in line3DChart)
				{
					item7.Write(sw, "line3DChart");
				}
			}
			if (surface3DChart != null)
			{
				foreach (CT_Surface3DChart item8 in surface3DChart)
				{
					item8.Write(sw, "surface3DChart");
				}
			}
			if (barChart != null)
			{
				foreach (CT_BarChart item9 in barChart)
				{
					item9.Write(sw, "barChart");
				}
			}
			if (lineChart != null)
			{
				foreach (CT_LineChart item10 in lineChart)
				{
					item10.Write(sw, "lineChart");
				}
			}
			if (radarChart != null)
			{
				foreach (CT_RadarChart item11 in radarChart)
				{
					item11.Write(sw, "radarChart");
				}
			}
			if (areaChart != null)
			{
				foreach (CT_AreaChart item12 in areaChart)
				{
					item12.Write(sw, "areaChart");
				}
			}
			if (scatterChart != null)
			{
				foreach (CT_ScatterChart item13 in scatterChart)
				{
					item13.Write(sw, "scatterChart");
				}
			}
			if (ofPieChart != null)
			{
				foreach (CT_OfPieChart item14 in ofPieChart)
				{
					item14.Write(sw, "ofPieChart");
				}
			}
			if (pieChart != null)
			{
				foreach (CT_PieChart item15 in pieChart)
				{
					item15.Write(sw, "pieChart");
				}
			}
			if (stockChart != null)
			{
				foreach (CT_StockChart item16 in stockChart)
				{
					item16.Write(sw, "stockChart");
				}
			}
			if (serAx != null)
			{
				foreach (CT_SerAx item17 in serAx)
				{
					item17.Write(sw, "serAx");
				}
			}
			if (dateAx != null)
			{
				foreach (CT_DateAx item18 in dateAx)
				{
					item18.Write(sw, "dateAx");
				}
			}
			if (catAx != null)
			{
				foreach (CT_CatAx item19 in catAx)
				{
					item19.Write(sw, "catAx");
				}
			}
			if (valAx != null)
			{
				foreach (CT_ValAx item20 in valAx)
				{
					item20.Write(sw, "valAx");
				}
			}
			if (extLst != null)
			{
				foreach (CT_Extension item21 in extLst)
				{
					item21.Write(sw, "extLst");
				}
			}
			sw.Write(string.Format("</c:{0}>", nodeName));
		}

		public CT_ValAx AddNewValAx()
		{
			if (valAxField == null)
			{
				valAxField = new List<CT_ValAx>();
			}
			CT_ValAx cT_ValAx = new CT_ValAx();
			valAxField.Add(cT_ValAx);
			return cT_ValAx;
		}

		public CT_SerAx AddNewSerAx()
		{
			if (serAxField == null)
			{
				serAxField = new List<CT_SerAx>();
			}
			CT_SerAx cT_SerAx = new CT_SerAx();
			serAxField.Add(cT_SerAx);
			return cT_SerAx;
		}

		public bool IsSetLayout()
		{
			return layoutField != null;
		}

		public CT_Layout AddNewLayout()
		{
			layoutField = new CT_Layout();
			return layoutField;
		}

		public CT_Pie3DChart AddNewPie3DChart()
		{
			if (pie3DChartField == null)
			{
				pie3DChartField = new List<CT_Pie3DChart>();
			}
			CT_Pie3DChart cT_Pie3DChart = new CT_Pie3DChart();
			pie3DChartField.Add(cT_Pie3DChart);
			return cT_Pie3DChart;
		}

		public CT_ScatterChart AddNewScatterChart()
		{
			if (scatterChartField == null)
			{
				scatterChartField = new List<CT_ScatterChart>();
			}
			CT_ScatterChart cT_ScatterChart = new CT_ScatterChart();
			scatterChartField.Add(cT_ScatterChart);
			return cT_ScatterChart;
		}

		public CT_LineChart AddNewLineChart()
		{
			CT_LineChart cT_LineChart = new CT_LineChart();
			if (lineChartField == null)
			{
				lineChartField = new List<CT_LineChart>();
			}
			lineChartField.Add(cT_LineChart);
			return cT_LineChart;
		}

		public CT_CatAx AddNewCatAx()
		{
			CT_CatAx cT_CatAx = new CT_CatAx();
			if (catAxField == null)
			{
				catAxField = new List<CT_CatAx>();
			}
			catAxField.Add(cT_CatAx);
			return cT_CatAx;
		}
	}
}
