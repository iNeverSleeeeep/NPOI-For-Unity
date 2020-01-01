using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Dml;
using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.SS.UserModel;
using NPOI.SS.UserModel.Charts;
using NPOI.XSSF.UserModel.Charts;
using System.Collections.Generic;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// Represents a SpreadsheetML Chart
	/// @author Nick Burch
	/// @author Roman Kashitsyn
	public class XSSFChart : POIXMLDocumentPart, IChart, ManuallyPositionable, IChartAxisFactory
	{
		/// Parent graphic frame.
		private XSSFGraphicFrame frame;

		/// Root element of the SpreadsheetML Chart part
		private ChartSpaceDocument chartSpaceDocument;

		/// The Chart within that
		private CT_Chart chart;

		private List<IChartAxis> axis;

		/// Returns the title, or null if none is Set
		public XSSFRichTextString Title
		{
			get
			{
				if (!chart.IsSetTitle())
				{
					return null;
				}
				CT_Title title = chart.title;
				if (title.tx == null)
				{
					return null;
				}
				if (title.tx.rich == null)
				{
					return null;
				}
				return new XSSFRichTextString(title.tx.rich.ToString());
			}
		}

		/// Create a new SpreadsheetML chart
		public XSSFChart()
		{
			axis = new List<IChartAxis>();
			CreateChart();
		}

		/// Construct a SpreadsheetML chart from a namespace part.
		///
		/// @param part the namespace part holding the chart data,
		/// the content type must be <code>application/vnd.Openxmlformats-officedocument.Drawingml.chart+xml</code>
		/// @param rel  the namespace relationship holding this chart,
		/// the relationship type must be http://schemas.Openxmlformats.org/officeDocument/2006/relationships/chart
		protected XSSFChart(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			XmlDocument xmldoc = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			chartSpaceDocument = ChartSpaceDocument.Parse(xmldoc, POIXMLDocumentPart.NamespaceManager);
			chart = chartSpaceDocument.GetChartSpace().chart;
		}

		/// Construct a new CTChartSpace bean.
		/// By default, it's just an empty placeholder for chart objects.
		///
		/// @return a new CTChartSpace bean
		private void CreateChart()
		{
			chartSpaceDocument = new ChartSpaceDocument();
			chart = chartSpaceDocument.GetChartSpace().AddNewChart();
			CT_PlotArea cT_PlotArea = chart.AddNewPlotArea();
			cT_PlotArea.AddNewLayout();
			chart.AddNewPlotVisOnly().val = 1;
			CT_PrintSettings cT_PrintSettings = chartSpaceDocument.GetChartSpace().AddNewPrintSettings();
			cT_PrintSettings.AddNewHeaderFooter();
			CT_PageMargins cT_PageMargins = cT_PrintSettings.AddNewPageMargins();
			cT_PageMargins.b = 0.75;
			cT_PageMargins.l = 0.7;
			cT_PageMargins.r = 0.7;
			cT_PageMargins.t = 0.75;
			cT_PageMargins.header = 0.3;
			cT_PageMargins.footer = 0.3;
			cT_PrintSettings.AddNewPageSetup();
		}

		/// Return the underlying CTChartSpace bean, the root element of the SpreadsheetML Chart part.
		///
		/// @return the underlying CTChartSpace bean
		internal CT_ChartSpace GetCTChartSpace()
		{
			return chartSpaceDocument.GetChartSpace();
		}

		/// Return the underlying CTChart bean, within the Chart Space
		///
		/// @return the underlying CTChart bean
		internal CT_Chart GetCTChart()
		{
			return chart;
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			chartSpaceDocument.Save(packagePart.GetOutputStream());
		}

		/// Returns the parent graphic frame.
		/// @return the graphic frame this chart belongs to
		public XSSFGraphicFrame GetGraphicFrame()
		{
			return frame;
		}

		/// Sets the parent graphic frame.
		internal void SetGraphicFrame(XSSFGraphicFrame frame)
		{
			this.frame = frame;
		}

		public IChartDataFactory GetChartDataFactory()
		{
			return XSSFChartDataFactory.GetInstance();
		}

		public IChartAxisFactory GetChartAxisFactory()
		{
			return this;
		}

		public void Plot(IChartData data, params IChartAxis[] axis)
		{
			data.FillChart(this, axis);
		}

		public IValueAxis CreateValueAxis(AxisPosition pos)
		{
			long id = axis.Count + 1;
			XSSFValueAxis xSSFValueAxis = new XSSFValueAxis(this, id, pos);
			if (axis.Count == 1)
			{
				IChartAxis chartAxis = axis[0];
				chartAxis.CrossAxis(xSSFValueAxis);
				xSSFValueAxis.CrossAxis(chartAxis);
			}
			axis.Add(xSSFValueAxis);
			return xSSFValueAxis;
		}

		public IChartAxis CreateCategoryAxis(AxisPosition pos)
		{
			long id = axis.Count + 1;
			XSSFCategoryAxis xSSFCategoryAxis = new XSSFCategoryAxis(this, id, pos);
			if (axis.Count == 1)
			{
				IChartAxis chartAxis = axis[0];
				chartAxis.CrossAxis(xSSFCategoryAxis);
				xSSFCategoryAxis.CrossAxis(chartAxis);
			}
			axis.Add(xSSFCategoryAxis);
			return xSSFCategoryAxis;
		}

		public List<IChartAxis> GetAxis()
		{
			if (axis.Count == 0 && HasAxis())
			{
				ParseAxis();
			}
			return axis;
		}

		public IManualLayout GetManualLayout()
		{
			return new XSSFManualLayout(this);
		}

		/// @return true if only visible cells will be present on the chart,
		///         false otherwise
		public bool IsPlotOnlyVisibleCells()
		{
			if (chart.plotVisOnly.val != 1)
			{
				return false;
			}
			return true;
		}

		/// @param plotVisOnly a flag specifying if only visible cells should be
		///        present on the chart
		public void SetPlotOnlyVisibleCells(bool plotVisOnly)
		{
			chart.plotVisOnly.val = (plotVisOnly ? 1 : 0);
		}

		public IChartLegend GetOrCreateLegend()
		{
			return new XSSFChartLegend(this);
		}

		public void DeleteLegend()
		{
			if (chart.IsSetLegend())
			{
				chart.unsetLegend();
			}
		}

		private bool HasAxis()
		{
			CT_PlotArea plotArea = chart.plotArea;
			int num = ((plotArea.valAx != null) ? plotArea.valAx.Count : 0) + ((plotArea.catAx != null) ? plotArea.catAx.Count : 0) + ((plotArea.dateAx != null) ? plotArea.dateAx.Count : 0) + ((plotArea.serAx != null) ? plotArea.serAx.Count : 0);
			return num > 0;
		}

		private void ParseAxis()
		{
			ParseValueAxis();
		}

		private void ParseValueAxis()
		{
			if (chart.plotArea.valAx != null)
			{
				foreach (CT_ValAx item in chart.plotArea.valAx)
				{
					axis.Add(new XSSFValueAxis(this, item));
				}
			}
		}
	}
}
