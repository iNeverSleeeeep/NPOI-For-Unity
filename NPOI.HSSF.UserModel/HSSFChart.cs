using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Chart;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.UserModel
{
	/// Has methods for construction of a chart object.
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	public class HSSFChart
	{
		/// A series in a chart
		public class HSSFSeries
		{
			internal SeriesRecord series;

			internal SeriesTextRecord seriesTitleText;

			private BRAIRecord dataName;

			private BRAIRecord dataValues;

			private BRAIRecord dataCategoryLabels;

			private BRAIRecord dataSecondaryCategoryLabels;

			public short NumValues => series.NumValues;

			/// See {@link SeriesRecord}
			public short ValueType => series.ValuesDataType;

			/// Returns the series' title, if there is one,
			///  or null if not
			public string SeriesTitle
			{
				get
				{
					if (seriesTitleText != null)
					{
						return seriesTitleText.Text;
					}
					return null;
				}
				set
				{
					if (seriesTitleText != null)
					{
						seriesTitleText.Text = value;
						return;
					}
					throw new InvalidOperationException("No series title found to Change");
				}
			}

			public HSSFSeries(SeriesRecord series)
			{
				this.series = series;
			}

			internal void InsertData(BRAIRecord data)
			{
				switch (data.LinkType)
				{
				case 0:
					dataName = data;
					break;
				case 1:
					dataValues = data;
					break;
				case 2:
					dataCategoryLabels = data;
					break;
				case 3:
					dataSecondaryCategoryLabels = data;
					break;
				}
			}

			internal void SetSeriesTitleText(SeriesTextRecord seriesTitleText)
			{
				this.seriesTitleText = seriesTitleText;
			}

			/// @return record with data names
			public BRAIRecord GetDataName()
			{
				return dataName;
			}

			/// @return record with data values
			public BRAIRecord GetDataValues()
			{
				return dataValues;
			}

			/// @return record with data category labels
			public BRAIRecord GetDataCategoryLabels()
			{
				return dataCategoryLabels;
			}

			/// @return record with data secondary category labels
			public BRAIRecord GetDataSecondaryCategoryLabels()
			{
				return dataSecondaryCategoryLabels;
			}

			/// @return record with series
			public SeriesRecord GetSeries()
			{
				return series;
			}

			private CellRangeAddressBase GetCellRange(BRAIRecord linkedDataRecord)
			{
				if (linkedDataRecord == null)
				{
					return null;
				}
				int firstRow = 0;
				int lastRow = 0;
				int firstCol = 0;
				int lastCol = 0;
				Ptg[] formulaOfLink = linkedDataRecord.FormulaOfLink;
				foreach (Ptg ptg in formulaOfLink)
				{
					if (ptg is AreaPtgBase)
					{
						AreaPtgBase areaPtgBase = (AreaPtgBase)ptg;
						firstRow = areaPtgBase.FirstRow;
						lastRow = areaPtgBase.LastRow;
						firstCol = areaPtgBase.FirstColumn;
						lastCol = areaPtgBase.LastColumn;
					}
				}
				return new CellRangeAddress(firstRow, lastRow, firstCol, lastCol);
			}

			public CellRangeAddressBase GetValuesCellRange()
			{
				return GetCellRange(dataValues);
			}

			public CellRangeAddressBase GetCategoryLabelsCellRange()
			{
				return GetCellRange(dataCategoryLabels);
			}

			private int SetVerticalCellRange(BRAIRecord linkedDataRecord, CellRangeAddressBase range)
			{
				if (linkedDataRecord == null)
				{
					throw new ArgumentNullException("linkedDataRecord should not be null");
				}
				List<Ptg> list = new List<Ptg>();
				int num = range.LastRow - range.FirstRow + 1;
				int num2 = range.LastColumn - range.FirstColumn + 1;
				Ptg[] formulaOfLink = linkedDataRecord.FormulaOfLink;
				foreach (Ptg ptg in formulaOfLink)
				{
					if (ptg is AreaPtgBase)
					{
						AreaPtgBase areaPtgBase = (AreaPtgBase)ptg;
						areaPtgBase.FirstRow = range.FirstRow;
						areaPtgBase.LastRow = range.LastRow;
						areaPtgBase.FirstColumn = range.FirstColumn;
						areaPtgBase.LastColumn = range.LastColumn;
						list.Add(areaPtgBase);
					}
				}
				linkedDataRecord.FormulaOfLink = list.ToArray();
				return num * num2;
			}

			public void SetValuesCellRange(CellRangeAddressBase range)
			{
				int num = SetVerticalCellRange(dataValues, range);
				series.NumValues = (short)num;
			}

			public void SetCategoryLabelsCellRange(CellRangeAddressBase range)
			{
				int num = SetVerticalCellRange(dataCategoryLabels, range);
				series.NumCategories = (short)num;
			}
		}

		private HSSFSheet sheet;

		private ChartRecord chartRecord;

		private LegendRecord legendRecord;

		private AlRunsRecord chartTitleFormat;

		private SeriesTextRecord chartTitleText;

		private List<ValueRangeRecord> valueRanges = new List<ValueRangeRecord>();

		private HSSFChartType type;

		private List<HSSFSeries> series = new List<HSSFSeries>();

		/// Get the X offset of the chart 
		public int ChartX
		{
			get
			{
				return chartRecord.X;
			}
			set
			{
				chartRecord.X = value;
			}
		}

		/// Get the Y offset of the chart 
		public int ChartY
		{
			get
			{
				return chartRecord.Y;
			}
			set
			{
				chartRecord.Y = value;
			}
		}

		/// Get the width of the chart. {@link ChartRecord} 
		public int ChartWidth
		{
			get
			{
				return chartRecord.Width;
			}
			set
			{
				chartRecord.Width = value;
			}
		}

		/// Get the height of the chart. {@link ChartRecord} 
		public int ChartHeight
		{
			get
			{
				return chartRecord.Height;
			}
			set
			{
				chartRecord.Height = value;
			}
		}

		/// Returns the series of the chart
		public HSSFSeries[] Series => series.ToArray();

		/// Returns the chart's title, if there is one,
		///  or null if not
		public string ChartTitle
		{
			get
			{
				if (chartTitleText != null)
				{
					return chartTitleText.Text;
				}
				return null;
			}
			set
			{
				if (chartTitleText != null)
				{
					chartTitleText.Text = value;
					return;
				}
				throw new InvalidOperationException("No chart title found to change");
			}
		}

		public HSSFChartType Type => type;

		private HSSFChart(HSSFSheet sheet, ChartRecord chartRecord)
		{
			this.chartRecord = chartRecord;
			this.sheet = sheet;
		}

		/// Creates a bar chart.  API needs some work. :)
		///
		/// NOTE:  Does not yet work...  checking it in just so others
		/// can take a look.
		public void CreateBarChart(HSSFWorkbook workbook, HSSFSheet sheet)
		{
			List<RecordBase> list = new List<RecordBase>();
			list.Add(CreateMSDrawingObjectRecord());
			list.Add(CreateOBJRecord());
			list.Add(CreateBOFRecord());
			list.Add(new HeaderRecord(string.Empty));
			list.Add(new FooterRecord(string.Empty));
			list.Add(CreateHCenterRecord());
			list.Add(CreateVCenterRecord());
			list.Add(CreatePrintSetupRecord());
			list.Add(CreateFontBasisRecord1());
			list.Add(CreateFontBasisRecord2());
			list.Add(new ProtectRecord(isProtected: false));
			list.Add(CreateUnitsRecord());
			list.Add(CreateChartRecord(0, 0, 30434904, 19031616));
			list.Add(CreateBeginRecord());
			list.Add(CreateSCLRecord(1, 1));
			list.Add(CreatePlotGrowthRecord(65536, 65536));
			list.Add(CreateFrameRecord1());
			list.Add(CreateBeginRecord());
			list.Add(CreateLineFormatRecord(drawTicks: true));
			list.Add(CreateAreaFormatRecord1());
			list.Add(CreateEndRecord());
			list.Add(CreateSeriesRecord());
			list.Add(CreateBeginRecord());
			list.Add(CreateTitleLinkedDataRecord());
			list.Add(CreateValuesLinkedDataRecord());
			list.Add(CreateCategoriesLinkedDataRecord());
			list.Add(CreateDataFormatRecord());
			list.Add(new SerToCrtRecord());
			list.Add(CreateEndRecord());
			list.Add(CreateSheetPropsRecord());
			list.Add(CreateDefaultTextRecord(2));
			list.Add(CreateAllTextRecord());
			list.Add(CreateBeginRecord());
			list.Add(CreateFontIndexRecord(5));
			list.Add(CreateDirectLinkRecord());
			list.Add(CreateEndRecord());
			list.Add(CreateDefaultTextRecord(3));
			list.Add(CreateUnknownTextRecord());
			list.Add(CreateBeginRecord());
			list.Add(CreateFontIndexRecord(6));
			list.Add(CreateDirectLinkRecord());
			list.Add(CreateEndRecord());
			list.Add(CreateAxisUsedRecord(1));
			CreateAxisRecords(list);
			list.Add(CreateEndRecord());
			list.Add(CreateDimensionsRecord());
			list.Add(CreateSeriesIndexRecord(2));
			list.Add(CreateSeriesIndexRecord(1));
			list.Add(CreateSeriesIndexRecord(3));
			list.Add(EOFRecord.instance);
			sheet.InsertChartRecords(list);
			workbook.InsertChartRecord();
		}

		/// Returns all the charts for the given sheet.
		///
		/// NOTE: You won't be able to do very much with
		///  these charts yet, as this is very limited support
		public static HSSFChart[] GetSheetCharts(HSSFSheet sheet)
		{
			List<HSSFChart> list = new List<HSSFChart>();
			HSSFChart hSSFChart = null;
			HSSFSeries hSSFSeries = null;
			IList records = sheet.Sheet.Records;
			foreach (RecordBase item in records)
			{
				if (item is ChartRecord)
				{
					hSSFSeries = null;
					hSSFChart = new HSSFChart(sheet, (ChartRecord)item);
					list.Add(hSSFChart);
				}
				else if (item is LegendRecord)
				{
					hSSFChart.legendRecord = (LegendRecord)item;
				}
				else if (item is SeriesRecord)
				{
					HSSFSeries hSSFSeries2 = new HSSFSeries((SeriesRecord)item);
					hSSFChart.series.Add(hSSFSeries2);
					hSSFSeries = hSSFSeries2;
				}
				else if (item is AlRunsRecord)
				{
					hSSFChart.chartTitleFormat = (AlRunsRecord)item;
				}
				else if (item is SeriesTextRecord)
				{
					SeriesTextRecord seriesTitleText = (SeriesTextRecord)item;
					if (hSSFChart.legendRecord == null && hSSFChart.series.Count > 0)
					{
						HSSFSeries hSSFSeries3 = hSSFChart.series[hSSFChart.series.Count - 1];
						hSSFSeries3.seriesTitleText = seriesTitleText;
					}
					else
					{
						hSSFChart.chartTitleText = seriesTitleText;
					}
				}
				else if (item is BRAIRecord)
				{
					BRAIRecord data = (BRAIRecord)item;
					hSSFSeries?.InsertData(data);
				}
				else if (item is ValueRangeRecord)
				{
					hSSFChart.valueRanges.Add((ValueRangeRecord)item);
				}
				else if (item is NPOI.HSSF.Record.Record && hSSFChart != null)
				{
					NPOI.HSSF.Record.Record record = (NPOI.HSSF.Record.Record)item;
					foreach (int value in Enum.GetValues(typeof(HSSFChartType)))
					{
						if (value != 0 && record.Sid == value)
						{
							hSSFChart.type = (HSSFChartType)value;
							break;
						}
					}
				}
			}
			return list.ToArray();
		}

		/// Set value range (basic Axis Options) 
		/// @param axisIndex 0 - primary axis, 1 - secondary axis
		/// @param minimum minimum value; Double.NaN - automatic; null - no change
		/// @param maximum maximum value; Double.NaN - automatic; null - no change
		/// @param majorUnit major unit value; Double.NaN - automatic; null - no change
		/// @param minorUnit minor unit value; Double.NaN - automatic; null - no change
		public void SetValueRange(int axisIndex, double? minimum, double? maximum, double? majorUnit, double? minorUnit)
		{
			ValueRangeRecord valueRangeRecord = valueRanges[axisIndex];
			if (valueRangeRecord != null)
			{
				if (minimum.HasValue)
				{
					valueRangeRecord.IsAutomaticMinimum = double.IsNaN(minimum.Value);
					valueRangeRecord.MinimumAxisValue = minimum.Value;
				}
				if (maximum.HasValue)
				{
					valueRangeRecord.IsAutomaticMaximum = double.IsNaN(maximum.Value);
					valueRangeRecord.MaximumAxisValue = maximum.Value;
				}
				if (majorUnit.HasValue)
				{
					valueRangeRecord.IsAutomaticMajor = double.IsNaN(majorUnit.Value);
					valueRangeRecord.MajorIncrement = majorUnit.Value;
				}
				if (minorUnit.HasValue)
				{
					valueRangeRecord.IsAutomaticMinor = double.IsNaN(minorUnit.Value);
					valueRangeRecord.MinorIncrement = minorUnit.Value;
				}
			}
		}

		private SeriesIndexRecord CreateSeriesIndexRecord(int index)
		{
			SeriesIndexRecord seriesIndexRecord = new SeriesIndexRecord();
			seriesIndexRecord.Index = (short)index;
			return seriesIndexRecord;
		}

		private DimensionsRecord CreateDimensionsRecord()
		{
			DimensionsRecord dimensionsRecord = new DimensionsRecord();
			dimensionsRecord.FirstRow = 0;
			dimensionsRecord.LastRow = 31;
			dimensionsRecord.FirstCol = 0;
			dimensionsRecord.LastCol = 1;
			return dimensionsRecord;
		}

		private HCenterRecord CreateHCenterRecord()
		{
			HCenterRecord hCenterRecord = new HCenterRecord();
			hCenterRecord.HCenter = false;
			return hCenterRecord;
		}

		private VCenterRecord CreateVCenterRecord()
		{
			VCenterRecord vCenterRecord = new VCenterRecord();
			vCenterRecord.VCenter = false;
			return vCenterRecord;
		}

		private PrintSetupRecord CreatePrintSetupRecord()
		{
			PrintSetupRecord printSetupRecord = new PrintSetupRecord();
			printSetupRecord.PaperSize = 0;
			printSetupRecord.Scale = 18;
			printSetupRecord.PageStart = 1;
			printSetupRecord.FitWidth = 1;
			printSetupRecord.FitHeight = 1;
			printSetupRecord.LeftToRight = false;
			printSetupRecord.Landscape = false;
			printSetupRecord.ValidSettings = true;
			printSetupRecord.NoColor = false;
			printSetupRecord.Draft = false;
			printSetupRecord.Notes = false;
			printSetupRecord.NoOrientation = false;
			printSetupRecord.UsePage = false;
			printSetupRecord.HResolution = 0;
			printSetupRecord.VResolution = 0;
			printSetupRecord.HeaderMargin = 0.5;
			printSetupRecord.FooterMargin = 0.5;
			printSetupRecord.Copies = 15;
			return printSetupRecord;
		}

		private FbiRecord CreateFontBasisRecord1()
		{
			FbiRecord fbiRecord = new FbiRecord();
			fbiRecord.XBasis = 9120;
			fbiRecord.YBasis = 5640;
			fbiRecord.HeightBasis = 200;
			fbiRecord.Scale = 0;
			fbiRecord.IndexToFontTable = 5;
			return fbiRecord;
		}

		private FbiRecord CreateFontBasisRecord2()
		{
			FbiRecord fbiRecord = CreateFontBasisRecord1();
			fbiRecord.IndexToFontTable = 6;
			return fbiRecord;
		}

		private BOFRecord CreateBOFRecord()
		{
			BOFRecord bOFRecord = new BOFRecord();
			bOFRecord.Version = 600;
			bOFRecord.Type = BOFRecordType.Chart;
			bOFRecord.Build = 7422;
			bOFRecord.BuildYear = 1997;
			bOFRecord.HistoryBitMask = 16585;
			bOFRecord.RequiredVersion = 106;
			return bOFRecord;
		}

		private UnknownRecord CreateOBJRecord()
		{
			byte[] data = new byte[26]
			{
				21,
				0,
				18,
				0,
				5,
				0,
				2,
				0,
				17,
				96,
				0,
				0,
				0,
				0,
				184,
				3,
				135,
				3,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			return new UnknownRecord(93, data);
		}

		private UnknownRecord CreateMSDrawingObjectRecord()
		{
			byte[] data = new byte[200]
			{
				15,
				0,
				2,
				240,
				192,
				0,
				0,
				0,
				16,
				0,
				8,
				240,
				8,
				0,
				0,
				0,
				2,
				0,
				0,
				0,
				2,
				4,
				0,
				0,
				15,
				0,
				3,
				240,
				168,
				0,
				0,
				0,
				15,
				0,
				4,
				240,
				40,
				0,
				0,
				0,
				1,
				0,
				9,
				240,
				16,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				2,
				0,
				10,
				240,
				8,
				0,
				0,
				0,
				0,
				4,
				0,
				0,
				5,
				0,
				0,
				0,
				15,
				0,
				4,
				240,
				112,
				0,
				0,
				0,
				146,
				12,
				10,
				240,
				8,
				0,
				0,
				0,
				2,
				4,
				0,
				0,
				0,
				10,
				0,
				0,
				147,
				0,
				11,
				240,
				54,
				0,
				0,
				0,
				127,
				0,
				4,
				1,
				4,
				1,
				191,
				0,
				8,
				0,
				8,
				0,
				129,
				1,
				78,
				0,
				0,
				8,
				131,
				1,
				77,
				0,
				0,
				8,
				191,
				1,
				16,
				0,
				17,
				0,
				192,
				1,
				77,
				0,
				0,
				8,
				byte.MaxValue,
				1,
				8,
				0,
				8,
				0,
				63,
				2,
				0,
				0,
				2,
				0,
				191,
				3,
				0,
				0,
				8,
				0,
				0,
				0,
				16,
				240,
				18,
				0,
				0,
				0,
				0,
				0,
				4,
				0,
				192,
				2,
				10,
				0,
				244,
				0,
				14,
				0,
				102,
				1,
				32,
				0,
				233,
				0,
				0,
				0,
				17,
				240,
				0,
				0,
				0,
				0
			};
			return new UnknownRecord(236, data);
		}

		private void CreateAxisRecords(IList records)
		{
			records.Add(CreateAxisParentRecord());
			records.Add(CreateBeginRecord());
			records.Add(CreateAxisRecord(0));
			records.Add(CreateBeginRecord());
			records.Add(CreateCategorySeriesAxisRecord());
			records.Add(CreateAxisOptionsRecord());
			records.Add(CreateTickRecord1());
			records.Add(CreateEndRecord());
			records.Add(CreateAxisRecord(1));
			records.Add(CreateBeginRecord());
			records.Add(CreateValueRangeRecord());
			records.Add(CreateTickRecord2());
			records.Add(CreateAxisLineFormatRecord(1));
			records.Add(CreateLineFormatRecord(drawTicks: false));
			records.Add(CreateEndRecord());
			records.Add(CreatePlotAreaRecord());
			records.Add(CreateFrameRecord2());
			records.Add(CreateBeginRecord());
			records.Add(CreateLineFormatRecord2());
			records.Add(CreateAreaFormatRecord2());
			records.Add(CreateEndRecord());
			records.Add(CreateChartFormatRecord());
			records.Add(CreateBeginRecord());
			records.Add(CreateBarRecord());
			records.Add(CreateLegendRecord());
			records.Add(CreateBeginRecord());
			records.Add(CreateTextRecord());
			records.Add(CreateBeginRecord());
			records.Add(CreateLinkedDataRecord());
			records.Add(CreateEndRecord());
			records.Add(CreateEndRecord());
			records.Add(CreateEndRecord());
			records.Add(CreateEndRecord());
		}

		private BRAIRecord CreateLinkedDataRecord()
		{
			BRAIRecord bRAIRecord = new BRAIRecord();
			bRAIRecord.LinkType = 0;
			bRAIRecord.ReferenceType = 1;
			bRAIRecord.IsCustomNumberFormat = false;
			bRAIRecord.IndexNumberFmtRecord = 0;
			bRAIRecord.FormulaOfLink = null;
			return bRAIRecord;
		}

		private TextRecord CreateTextRecord()
		{
			TextRecord textRecord = new TextRecord();
			textRecord.HorizontalAlignment = 2;
			textRecord.VerticalAlignment = 2;
			textRecord.DisplayMode = 1;
			textRecord.RgbColor = 0;
			textRecord.X = -37;
			textRecord.Y = -60;
			textRecord.Width = 0;
			textRecord.Height = 0;
			textRecord.IsAutoColor = true;
			textRecord.ShowKey = false;
			textRecord.ShowValue = false;
			textRecord.IsAutoGeneratedText = true;
			textRecord.IsGenerated = true;
			textRecord.IsAutoLabelDeleted = false;
			textRecord.IsAutoBackground = true;
			textRecord.ShowCategoryLabelAsPercentage = false;
			textRecord.ShowValueAsPercentage = false;
			textRecord.ShowBubbleSizes = false;
			textRecord.ShowLabel = false;
			textRecord.IndexOfColorValue = 77;
			textRecord.DataLabelPlacement = 0;
			textRecord.TextRotation = 0;
			return textRecord;
		}

		private LegendRecord CreateLegendRecord()
		{
			LegendRecord legendRecord = new LegendRecord();
			legendRecord.XAxisUpperLeft = 3542;
			legendRecord.YAxisUpperLeft = 1566;
			legendRecord.XSize = 437;
			legendRecord.YSize = 213;
			legendRecord.Type = 3;
			legendRecord.Spacing = 1;
			legendRecord.IsAutoPosition = true;
			legendRecord.IsAutoSeries = true;
			legendRecord.IsAutoXPositioning = true;
			legendRecord.IsAutoYPositioning = true;
			legendRecord.IsVertical = true;
			legendRecord.IsDataTable = false;
			return legendRecord;
		}

		private BarRecord CreateBarRecord()
		{
			BarRecord barRecord = new BarRecord();
			barRecord.BarSpace = 0;
			barRecord.CategorySpace = 150;
			barRecord.IsHorizontal = false;
			barRecord.IsStacked = false;
			barRecord.IsDisplayAsPercentage = false;
			barRecord.IsShadow = false;
			return barRecord;
		}

		private ChartFormatRecord CreateChartFormatRecord()
		{
			ChartFormatRecord chartFormatRecord = new ChartFormatRecord();
			chartFormatRecord.XPosition = 0;
			chartFormatRecord.YPosition = 0;
			chartFormatRecord.Width = 0;
			chartFormatRecord.Height = 0;
			chartFormatRecord.VaryDisplayPattern = false;
			return chartFormatRecord;
		}

		private PlotAreaRecord CreatePlotAreaRecord()
		{
			return new PlotAreaRecord();
		}

		private AxisLineRecord CreateAxisLineFormatRecord(short format)
		{
			AxisLineRecord axisLineRecord = new AxisLineRecord();
			axisLineRecord.AxisType = format;
			return axisLineRecord;
		}

		private ValueRangeRecord CreateValueRangeRecord()
		{
			ValueRangeRecord valueRangeRecord = new ValueRangeRecord();
			valueRangeRecord.MinimumAxisValue = 0.0;
			valueRangeRecord.MaximumAxisValue = 0.0;
			valueRangeRecord.MajorIncrement = 0.0;
			valueRangeRecord.MinorIncrement = 0.0;
			valueRangeRecord.CategoryAxisCross = 0.0;
			valueRangeRecord.IsAutomaticMinimum = true;
			valueRangeRecord.IsAutomaticMaximum = true;
			valueRangeRecord.IsAutomaticMajor = true;
			valueRangeRecord.IsAutomaticMinor = true;
			valueRangeRecord.IsAutomaticCategoryCrossing = true;
			valueRangeRecord.IsLogarithmicScale = false;
			valueRangeRecord.IsValuesInReverse = false;
			valueRangeRecord.IsCrossCategoryAxisAtMaximum = false;
			valueRangeRecord.IsReserved = true;
			return valueRangeRecord;
		}

		private TickRecord CreateTickRecord1()
		{
			TickRecord tickRecord = new TickRecord();
			tickRecord.MajorTickType = 2;
			tickRecord.MinorTickType = 0;
			tickRecord.LabelPosition = 3;
			tickRecord.Background = 1;
			tickRecord.LabelColorRgb = 0;
			tickRecord.Zero1 = 0;
			tickRecord.Zero2 = 0;
			tickRecord.Zero3 = 45;
			tickRecord.IsAutorotate = true;
			tickRecord.IsAutoTextBackground = true;
			tickRecord.Rotation = 0;
			tickRecord.IsAutorotate = true;
			tickRecord.TickColor = 77;
			return tickRecord;
		}

		private TickRecord CreateTickRecord2()
		{
			TickRecord tickRecord = CreateTickRecord1();
			tickRecord.Zero3 = 0;
			return tickRecord;
		}

		private AxcExtRecord CreateAxisOptionsRecord()
		{
			AxcExtRecord axcExtRecord = new AxcExtRecord();
			axcExtRecord.MinimumDate = -28644;
			axcExtRecord.MaximumDate = -28715;
			axcExtRecord.MajorInterval = 2;
			axcExtRecord.MajorUnit = DateUnit.Days;
			axcExtRecord.MinorInterval = 1;
			axcExtRecord.MinorUnit = DateUnit.Days;
			axcExtRecord.BaseUnit = DateUnit.Days;
			axcExtRecord.CrossDate = -28644;
			axcExtRecord.IsAutoMin = true;
			axcExtRecord.IsAutoMax = true;
			axcExtRecord.IsAutoMajor = true;
			axcExtRecord.IsAutoMinor = true;
			axcExtRecord.IsDateAxis = true;
			axcExtRecord.IsAutoBase = true;
			axcExtRecord.IsAutoCross = true;
			axcExtRecord.IsAutoDate = true;
			return axcExtRecord;
		}

		private CatSerRangeRecord CreateCategorySeriesAxisRecord()
		{
			CatSerRangeRecord catSerRangeRecord = new CatSerRangeRecord();
			catSerRangeRecord.CrossPoint = 1;
			catSerRangeRecord.LabelInterval = 1;
			catSerRangeRecord.MarkInterval = 1;
			catSerRangeRecord.IsBetween = true;
			catSerRangeRecord.IsMaxCross = false;
			catSerRangeRecord.IsReverse = false;
			return catSerRangeRecord;
		}

		private AxisRecord CreateAxisRecord(short axisType)
		{
			AxisRecord axisRecord = new AxisRecord();
			axisRecord.AxisType = axisType;
			return axisRecord;
		}

		private AxisParentRecord CreateAxisParentRecord()
		{
			AxisParentRecord axisParentRecord = new AxisParentRecord();
			axisParentRecord.AxisType = 0;
			axisParentRecord.X = 479;
			axisParentRecord.Y = 221;
			axisParentRecord.Width = 2995;
			axisParentRecord.Height = 2902;
			return axisParentRecord;
		}

		private AxesUsedRecord CreateAxisUsedRecord(short numAxis)
		{
			AxesUsedRecord axesUsedRecord = new AxesUsedRecord();
			axesUsedRecord.NumAxis = numAxis;
			return axesUsedRecord;
		}

		private BRAIRecord CreateDirectLinkRecord()
		{
			BRAIRecord bRAIRecord = new BRAIRecord();
			bRAIRecord.LinkType = 0;
			bRAIRecord.ReferenceType = 1;
			bRAIRecord.IsCustomNumberFormat = false;
			bRAIRecord.IndexNumberFmtRecord = 0;
			bRAIRecord.FormulaOfLink = null;
			return bRAIRecord;
		}

		private FontXRecord CreateFontIndexRecord(int index)
		{
			FontXRecord fontXRecord = new FontXRecord();
			fontXRecord.FontIndex = (short)index;
			return fontXRecord;
		}

		private TextRecord CreateAllTextRecord()
		{
			TextRecord textRecord = new TextRecord();
			textRecord.HorizontalAlignment = 2;
			textRecord.VerticalAlignment = 2;
			textRecord.DisplayMode = 1;
			textRecord.RgbColor = 0;
			textRecord.X = -37;
			textRecord.Y = -60;
			textRecord.Width = 0;
			textRecord.Height = 0;
			textRecord.IsAutoColor = true;
			textRecord.ShowKey = false;
			textRecord.ShowValue = true;
			textRecord.IsAutoGeneratedText = true;
			textRecord.IsGenerated = true;
			textRecord.IsAutoLabelDeleted = false;
			textRecord.IsAutoBackground = true;
			textRecord.ShowCategoryLabelAsPercentage = false;
			textRecord.ShowValueAsPercentage = false;
			textRecord.ShowBubbleSizes = false;
			textRecord.ShowLabel = false;
			textRecord.IndexOfColorValue = 77;
			textRecord.DataLabelPlacement = 0;
			textRecord.TextRotation = 0;
			return textRecord;
		}

		private TextRecord CreateUnknownTextRecord()
		{
			TextRecord textRecord = new TextRecord();
			textRecord.HorizontalAlignment = 2;
			textRecord.VerticalAlignment = 2;
			textRecord.DisplayMode = 1;
			textRecord.RgbColor = 0;
			textRecord.X = -37;
			textRecord.Y = -60;
			textRecord.Width = 0;
			textRecord.Height = 0;
			textRecord.IsAutoColor = true;
			textRecord.ShowKey = false;
			textRecord.ShowValue = false;
			textRecord.IsAutoGeneratedText = true;
			textRecord.IsGenerated = true;
			textRecord.IsAutoLabelDeleted = false;
			textRecord.IsAutoBackground = true;
			textRecord.ShowCategoryLabelAsPercentage = false;
			textRecord.ShowValueAsPercentage = false;
			textRecord.ShowBubbleSizes = false;
			textRecord.ShowLabel = false;
			textRecord.IndexOfColorValue = 77;
			textRecord.DataLabelPlacement = 11088;
			textRecord.TextRotation = 0;
			return textRecord;
		}

		private DefaultTextRecord CreateDefaultTextRecord(short categoryDataType)
		{
			DefaultTextRecord defaultTextRecord = new DefaultTextRecord();
			defaultTextRecord.FormatType = (TextFormatInfo)categoryDataType;
			return defaultTextRecord;
		}

		private ShtPropsRecord CreateSheetPropsRecord()
		{
			ShtPropsRecord shtPropsRecord = new ShtPropsRecord();
			shtPropsRecord.IsManSerAlloc = false;
			shtPropsRecord.IsPlotVisibleOnly = true;
			shtPropsRecord.IsNotSizeWithWindow = false;
			shtPropsRecord.IsManPlotArea = true;
			shtPropsRecord.IsAlwaysAutoPlotArea = false;
			return shtPropsRecord;
		}

		private DataFormatRecord CreateDataFormatRecord()
		{
			DataFormatRecord dataFormatRecord = new DataFormatRecord();
			dataFormatRecord.PointNumber = -1;
			dataFormatRecord.SeriesIndex = 0;
			dataFormatRecord.SeriesNumber = 0;
			dataFormatRecord.UseExcel4Colors = false;
			return dataFormatRecord;
		}

		private BRAIRecord CreateCategoriesLinkedDataRecord()
		{
			BRAIRecord bRAIRecord = new BRAIRecord();
			bRAIRecord.LinkType = 2;
			bRAIRecord.ReferenceType = 2;
			bRAIRecord.IsCustomNumberFormat = false;
			bRAIRecord.IndexNumberFmtRecord = 0;
			Area3DPtg area3DPtg = new Area3DPtg(0, 31, 1, 1, firstRowRelative: false, lastRowRelative: false, firstColRelative: false, lastColRelative: false, 0);
			bRAIRecord.FormulaOfLink = new Ptg[1]
			{
				area3DPtg
			};
			return bRAIRecord;
		}

		private BRAIRecord CreateValuesLinkedDataRecord()
		{
			BRAIRecord bRAIRecord = new BRAIRecord();
			bRAIRecord.LinkType = 1;
			bRAIRecord.ReferenceType = 2;
			bRAIRecord.IsCustomNumberFormat = false;
			bRAIRecord.IndexNumberFmtRecord = 0;
			Area3DPtg area3DPtg = new Area3DPtg(0, 31, 0, 0, firstRowRelative: false, lastRowRelative: false, firstColRelative: false, lastColRelative: false, 0);
			bRAIRecord.FormulaOfLink = new Ptg[1]
			{
				area3DPtg
			};
			return bRAIRecord;
		}

		private BRAIRecord CreateTitleLinkedDataRecord()
		{
			BRAIRecord bRAIRecord = new BRAIRecord();
			bRAIRecord.LinkType = 0;
			bRAIRecord.ReferenceType = 1;
			bRAIRecord.IsCustomNumberFormat = false;
			bRAIRecord.IndexNumberFmtRecord = 0;
			bRAIRecord.FormulaOfLink = null;
			return bRAIRecord;
		}

		private SeriesRecord CreateSeriesRecord()
		{
			SeriesRecord seriesRecord = new SeriesRecord();
			seriesRecord.CategoryDataType = 1;
			seriesRecord.ValuesDataType = 1;
			seriesRecord.NumCategories = 32;
			seriesRecord.NumValues = 31;
			seriesRecord.BubbleSeriesType = 1;
			seriesRecord.NumBubbleValues = 0;
			return seriesRecord;
		}

		private EndRecord CreateEndRecord()
		{
			return new EndRecord();
		}

		private AreaFormatRecord CreateAreaFormatRecord1()
		{
			AreaFormatRecord areaFormatRecord = new AreaFormatRecord();
			areaFormatRecord.ForegroundColor = 16777215;
			areaFormatRecord.BackgroundColor = 0;
			areaFormatRecord.Pattern = 1;
			areaFormatRecord.IsAutomatic = true;
			areaFormatRecord.IsInvert = false;
			areaFormatRecord.ForecolorIndex = 78;
			areaFormatRecord.BackcolorIndex = 77;
			return areaFormatRecord;
		}

		private AreaFormatRecord CreateAreaFormatRecord2()
		{
			AreaFormatRecord areaFormatRecord = new AreaFormatRecord();
			areaFormatRecord.ForegroundColor = 12632256;
			areaFormatRecord.BackgroundColor = 0;
			areaFormatRecord.Pattern = 1;
			areaFormatRecord.IsAutomatic = false;
			areaFormatRecord.IsInvert = false;
			areaFormatRecord.ForecolorIndex = 22;
			areaFormatRecord.BackcolorIndex = 79;
			return areaFormatRecord;
		}

		private LineFormatRecord CreateLineFormatRecord(bool drawTicks)
		{
			LineFormatRecord lineFormatRecord = new LineFormatRecord();
			lineFormatRecord.LineColor = 0;
			lineFormatRecord.LinePattern = 0;
			lineFormatRecord.Weight = -1;
			lineFormatRecord.IsAuto = true;
			lineFormatRecord.IsDrawTicks = drawTicks;
			lineFormatRecord.ColourPaletteIndex = 77;
			return lineFormatRecord;
		}

		private LineFormatRecord CreateLineFormatRecord2()
		{
			LineFormatRecord lineFormatRecord = new LineFormatRecord();
			lineFormatRecord.LineColor = 8421504;
			lineFormatRecord.LinePattern = 0;
			lineFormatRecord.Weight = 0;
			lineFormatRecord.IsAuto = false;
			lineFormatRecord.IsDrawTicks = false;
			lineFormatRecord.IsUnknown = false;
			lineFormatRecord.ColourPaletteIndex = 23;
			return lineFormatRecord;
		}

		private FrameRecord CreateFrameRecord1()
		{
			FrameRecord frameRecord = new FrameRecord();
			frameRecord.BorderType = 0;
			frameRecord.IsAutoSize = false;
			frameRecord.IsAutoPosition = true;
			return frameRecord;
		}

		private FrameRecord CreateFrameRecord2()
		{
			FrameRecord frameRecord = new FrameRecord();
			frameRecord.BorderType = 0;
			frameRecord.IsAutoSize = true;
			frameRecord.IsAutoPosition = true;
			return frameRecord;
		}

		private PlotGrowthRecord CreatePlotGrowthRecord(int horizScale, int vertScale)
		{
			PlotGrowthRecord plotGrowthRecord = new PlotGrowthRecord();
			plotGrowthRecord.HorizontalScale = horizScale;
			plotGrowthRecord.VerticalScale = vertScale;
			return plotGrowthRecord;
		}

		private SCLRecord CreateSCLRecord(short numerator, short denominator)
		{
			SCLRecord sCLRecord = new SCLRecord();
			sCLRecord.Denominator = denominator;
			sCLRecord.Numerator = numerator;
			return sCLRecord;
		}

		private BeginRecord CreateBeginRecord()
		{
			return new BeginRecord();
		}

		private ChartRecord CreateChartRecord(int x, int y, int width, int height)
		{
			ChartRecord chartRecord = new ChartRecord();
			chartRecord.X = x;
			chartRecord.Y = y;
			chartRecord.Width = width;
			chartRecord.Height = height;
			return chartRecord;
		}

		private UnitsRecord CreateUnitsRecord()
		{
			UnitsRecord unitsRecord = new UnitsRecord();
			unitsRecord.Units = 0;
			return unitsRecord;
		}

		public HSSFSeries CreateSeries()
		{
			List<RecordBase> list = new List<RecordBase>();
			bool flag = false;
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			int num6 = -1;
			int num7 = 0;
			IList records = sheet.Sheet.Records;
			foreach (RecordBase item in records)
			{
				num++;
				if (item is BeginRecord)
				{
					num2++;
				}
				else if (item is EndRecord)
				{
					num2--;
					if (num5 == num2)
					{
						num5 = -1;
						num6 = num;
						if (!flag)
						{
							list.Add(item);
							flag = true;
						}
					}
					if (num4 == num2)
					{
						break;
					}
				}
				if (item is ChartRecord)
				{
					if (item == chartRecord)
					{
						num3 = num;
						num4 = num2;
					}
				}
				else if (item is SeriesRecord && num3 != -1)
				{
					num7++;
					num5 = num2;
				}
				if (num5 != -1 && !flag)
				{
					list.Add(item);
				}
			}
			if (num6 == -1)
			{
				return null;
			}
			num = num6 + 1;
			HSSFSeries hSSFSeries = null;
			List<RecordBase> list2 = new List<RecordBase>();
			foreach (RecordBase item2 in list)
			{
				NPOI.HSSF.Record.Record record = null;
				if (item2 is BeginRecord)
				{
					record = new BeginRecord();
				}
				else if (item2 is EndRecord)
				{
					record = new EndRecord();
				}
				else if (item2 is SeriesRecord)
				{
					SeriesRecord seriesRecord = (SeriesRecord)((SeriesRecord)item2).Clone();
					hSSFSeries = new HSSFSeries(seriesRecord);
					record = seriesRecord;
				}
				else if (item2 is BRAIRecord)
				{
					BRAIRecord bRAIRecord = (BRAIRecord)((BRAIRecord)item2).Clone();
					hSSFSeries?.InsertData(bRAIRecord);
					record = bRAIRecord;
				}
				else if (item2 is DataFormatRecord)
				{
					DataFormatRecord dataFormatRecord = (DataFormatRecord)((DataFormatRecord)item2).Clone();
					dataFormatRecord.SeriesIndex = (short)num7;
					dataFormatRecord.SeriesNumber = (short)num7;
					record = dataFormatRecord;
				}
				else if (item2 is SeriesTextRecord)
				{
					SeriesTextRecord seriesTextRecord = (SeriesTextRecord)((SeriesTextRecord)item2).Clone();
					hSSFSeries?.SetSeriesTitleText(seriesTextRecord);
					record = seriesTextRecord;
				}
				else if (item2 is NPOI.HSSF.Record.Record)
				{
					record = (NPOI.HSSF.Record.Record)((NPOI.HSSF.Record.Record)item2).Clone();
				}
				if (record != null)
				{
					list2.Add(record);
				}
			}
			if (hSSFSeries == null)
			{
				return null;
			}
			foreach (RecordBase item3 in list2)
			{
				records.Insert(num++, item3);
			}
			return hSSFSeries;
		}

		public bool RemoveSeries(HSSFSeries series)
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			int num4 = -1;
			int num5 = -1;
			bool flag = false;
			bool flag2 = false;
			bool result = false;
			IList records = sheet.Sheet.Records;
			IEnumerator enumerator = records.GetEnumerator();
			while (enumerator.MoveNext())
			{
				RecordBase recordBase = (RecordBase)enumerator.Current;
				num++;
				if (recordBase is BeginRecord)
				{
					num2++;
				}
				else if (recordBase is EndRecord)
				{
					num2--;
					if (num4 == num2)
					{
						num4 = -1;
						if (flag)
						{
							flag = false;
							result = true;
							records.Remove(recordBase);
						}
					}
					if (num3 == num2)
					{
						break;
					}
				}
				if (recordBase is ChartRecord)
				{
					if (recordBase == chartRecord)
					{
						num3 = num2;
						flag2 = true;
					}
				}
				else if (recordBase is SeriesRecord)
				{
					if (flag2)
					{
						if (series.series == recordBase)
						{
							num4 = num2;
							flag = true;
						}
						else
						{
							num5++;
						}
					}
				}
				else if (recordBase is DataFormatRecord && flag2 && !flag)
				{
					DataFormatRecord dataFormatRecord = (DataFormatRecord)recordBase;
					dataFormatRecord.SeriesIndex = (short)num5;
					dataFormatRecord.SeriesNumber = (short)num5;
				}
				if (flag)
				{
					records.Remove(recordBase);
				}
			}
			return result;
		}
	}
}
