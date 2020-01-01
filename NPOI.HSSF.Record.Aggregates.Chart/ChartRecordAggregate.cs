using NPOI.HSSF.Record.Chart;
using System.Collections.Generic;

namespace NPOI.HSSF.Record.Aggregates.Chart
{
	public abstract class ChartRecordAggregate : RecordAggregate
	{
		private class StartBlockStack
		{
			private List<StartBlockRecord> blockList = new List<StartBlockRecord>(16);

			public int Count => blockList.Count;

			public void Push(StartBlockRecord item)
			{
				blockList.Add(item);
			}

			public StartBlockRecord Pop()
			{
				if (blockList.Count == 0)
				{
					return null;
				}
				StartBlockRecord result = blockList[blockList.Count - 1];
				blockList.RemoveAt(blockList.Count - 1);
				return result;
			}

			public StartBlockRecord Peek()
			{
				if (blockList.Count == 0)
				{
					return null;
				}
				return blockList[blockList.Count - 1];
			}

			public bool Contains(ObjectKind objectKind)
			{
				foreach (StartBlockRecord block in blockList)
				{
					if (block.ObjectKind == objectKind)
					{
						return true;
					}
				}
				return false;
			}
		}

		public const string RuleName_DAT = "DAT";

		public const string RuleName_TEXTPROPS = "TEXTPROPS";

		public const string RuleName_SS = "SS";

		public const string RuleName_SHAPEPROPS = "SHAPEPROPS";

		public const string RuleName_SERIESFORMAT = "SERIESFORMAT";

		public const string RuleName_SERIESAXIS = "SERIESAXIS";

		public const string RuleName_LD = "LD";

		public const string RuleName_IVAXIS = "IVAXIS";

		public const string RuleName_GELFRAME = "GELFRAME";

		public const string RuleName_FRAME = "FRAME";

		public const string RuleName_FONTLIST = "FONTLIST";

		public const string RuleName_DVAXIS = "DVAXIS";

		public const string RuleName_DROPBAR = "DROPBAR";

		public const string RuleName_DFTTEXT = "DFTTEXT";

		public const string RuleName_CRTMLFRT = "CRTMLFRT";

		public const string RuleName_CRT = "CRT";

		public const string RuleName_CHARTFOMATS = "CHARTFOMATS";

		public const string RuleName_AXS = "AXS";

		public const string RuleName_AXM = "AXM";

		public const string RuleName_AXISPARENT = "AXISPARENT";

		public const string RuleName_AXES = "AXES";

		public const string RuleName_ATTACHEDLABEL = "ATTACHEDLABEL";

		public const string RuleName_LEGENDEXCEPTION = "LEGENDEXCEPTION";

		public const string RuleName_CHARTSHEET = "CHARTSHEET";

		public const short ChartSpecificFutureRecordLowerSid = 2048;

		public const short ChartSpecificFutureRecordHigherSid = 2303;

		private string _rulename;

		private ChartRecordAggregate _container;

		private static StartBlockStack blocks = new StartBlockStack();

		private bool _isInStartObject;

		protected string RuleName
		{
			get
			{
				return _rulename;
			}
			private set
			{
				_rulename = value;
			}
		}

		protected ChartRecordAggregate Container
		{
			get
			{
				return _container;
			}
			private set
			{
				_container = value;
			}
		}

		protected bool IsInStartObject
		{
			get
			{
				return _isInStartObject;
			}
			set
			{
				_isInStartObject = value;
			}
		}

		protected ChartRecordAggregate(string ruleName, ChartRecordAggregate container)
		{
			RuleName = ruleName;
			Container = container;
		}

		protected virtual bool ShoudWriteStartBlock()
		{
			return false;
		}

		private bool IsInRule(string ruleName)
		{
			for (ChartRecordAggregate chartRecordAggregate = this; chartRecordAggregate != null; chartRecordAggregate = chartRecordAggregate.Container)
			{
				if (chartRecordAggregate.RuleName == ruleName)
				{
					return true;
				}
			}
			return false;
		}

		protected T GetContainer<T>(string ruleName) where T : ChartRecordAggregate
		{
			ChartRecordAggregate chartRecordAggregate = this;
			while (chartRecordAggregate != null && !(chartRecordAggregate.RuleName == ruleName))
			{
				chartRecordAggregate = chartRecordAggregate.Container;
			}
			return chartRecordAggregate as T;
		}

		protected void WriteStartBlock(RecordVisitor rv)
		{
			if (!IsInStartObject)
			{
				StartBlockRecord startBlockRecord = null;
				if (blocks.Count == 0)
				{
					startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.Sheet);
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("DAT") && !blocks.Contains(ObjectKind.DatRecord))
				{
					startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.DatRecord);
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("SERIESFORMAT") && !blocks.Contains(ObjectKind.Series))
				{
					startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.Series, 0, GetContainer<SeriesFormatAggregate>("SERIESFORMAT").SeriesIndex);
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("SS") && !blocks.Contains(ObjectKind.DataFormatRecord))
				{
					SSAggregate container = GetContainer<SSAggregate>("SS");
					startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.DataFormatRecord, container.DataFormat.SeriesIndex, container.DataFormat.PointNumber);
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("LEGENDEXCEPTION") && !blocks.Contains(ObjectKind.LegendException))
				{
					SeriesFormatAggregate.LegendExceptionAggregate container2 = GetContainer<SeriesFormatAggregate.LegendExceptionAggregate>("LEGENDEXCEPTION");
					startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.LegendException, 0, container2.LegendException.LegendEntry);
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("AXISPARENT") && !blocks.Contains(ObjectKind.AxisGroup))
				{
					AxisParentAggregate container3 = GetContainer<AxisParentAggregate>("AXISPARENT");
					startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.AxisGroup, 0, container3.AxisParent.AxisType);
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("CRT") && !blocks.Contains(ObjectKind.ChartGroup))
				{
					AxisParentAggregate container4 = GetContainer<AxisParentAggregate>("AXISPARENT");
					startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.ChartGroup, 0, container4.AxisParent.AxisType);
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("AXES") && !blocks.Contains(ObjectKind.Axis))
				{
					if (IsInRule("IVAXIS"))
					{
						startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.Axis, 0, 0);
						blocks.Push(startBlockRecord);
						rv.VisitRecord(startBlockRecord);
					}
					if (IsInRule("SERIESAXIS"))
					{
						startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.Axis, 0, 2);
						blocks.Push(startBlockRecord);
						rv.VisitRecord(startBlockRecord);
					}
					if (IsInRule("DVAXIS"))
					{
						DVAxisAggregate container5 = GetContainer<DVAxisAggregate>("DVAXIS");
						startBlockRecord = ((container5.Axis.AxisType != 0) ? StartBlockRecord.CreateStartBlock(ObjectKind.Axis, 0, 3) : StartBlockRecord.CreateStartBlock(ObjectKind.Axis, 0, 1));
						blocks.Push(startBlockRecord);
						rv.VisitRecord(startBlockRecord);
					}
				}
				if (IsInRule("DROPBAR") && !blocks.Contains(ObjectKind.DropBarRecord))
				{
					startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.DropBarRecord);
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("LD") && !blocks.Contains(ObjectKind.Legend))
				{
					startBlockRecord = ((!IsInRule("CRT")) ? StartBlockRecord.CreateStartBlock(ObjectKind.Legend, 0) : StartBlockRecord.CreateStartBlock(ObjectKind.Legend, 1));
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("ATTACHEDLABEL") && !blocks.Contains(ObjectKind.AttachedLabelRecord))
				{
					if (IsInRule("DFTTEXT"))
					{
						DFTTextAggregate container6 = GetContainer<DFTTextAggregate>("DFTTEXT");
						startBlockRecord = ((!IsInRule("CRT") || container6.DefaultText.FormatType < TextFormatInfo.FontScaleNotSet) ? StartBlockRecord.CreateStartBlock(ObjectKind.AttachedLabelRecord, 2, (short)container6.DefaultText.FormatType) : StartBlockRecord.CreateStartBlock(ObjectKind.AttachedLabelRecord, 2, -1));
					}
					else
					{
						AttachedLabelAggregate container7 = GetContainer<AttachedLabelAggregate>("ATTACHEDLABEL");
						startBlockRecord = ((container7.ObjectLink.Link1 == 3) ? StartBlockRecord.CreateStartBlock(ObjectKind.AttachedLabelRecord, 4, 0) : ((container7.ObjectLink.Link1 == 2) ? StartBlockRecord.CreateStartBlock(ObjectKind.AttachedLabelRecord, 4, 1) : ((container7.ObjectLink.Link1 == 7) ? StartBlockRecord.CreateStartBlock(ObjectKind.AttachedLabelRecord, 4, 2) : ((!container7.IsFirst) ? StartBlockRecord.CreateStartBlock(ObjectKind.AttachedLabelRecord, 5, container7.ObjectLink.Link1, container7.ObjectLink.Link2) : StartBlockRecord.CreateStartBlock(ObjectKind.AttachedLabelRecord, 0)))));
					}
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
				if (IsInRule("FRAME") && !blocks.Contains(ObjectKind.Frame))
				{
					if (IsInRule("ATTACHEDLABEL") || IsInRule("LD"))
					{
						startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.Frame, 0, 0);
					}
					else if (IsInRule("AXES"))
					{
						startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.Frame, 1, 0);
					}
					else if (IsInRule("CHARTSHEET"))
					{
						startBlockRecord = StartBlockRecord.CreateStartBlock(ObjectKind.Frame, 2, 0);
					}
					blocks.Push(startBlockRecord);
					rv.VisitRecord(startBlockRecord);
				}
			}
		}

		protected void WriteEndBlock(RecordVisitor rv)
		{
			if (!IsInStartObject)
			{
				StartBlockRecord startBlockRecord = blocks.Peek();
				if (RuleName == "AXISPARENT" && startBlockRecord.ObjectKind == ObjectKind.AxisGroup)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.AxisGroup));
					blocks.Pop();
				}
				else if (RuleName == "ATTACHEDLABEL" && startBlockRecord.ObjectKind == ObjectKind.AttachedLabelRecord)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.AttachedLabelRecord));
					blocks.Pop();
				}
				else if ((RuleName == "IVAXIS" || RuleName == "DVAXIS" || RuleName == "SERIESAXIS") && startBlockRecord.ObjectKind == ObjectKind.Axis)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.Axis));
					blocks.Pop();
				}
				else if (RuleName == "CRT" && startBlockRecord.ObjectKind == ObjectKind.ChartGroup)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.ChartGroup));
					blocks.Pop();
				}
				else if (RuleName == "DAT" && startBlockRecord.ObjectKind == ObjectKind.DatRecord)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.DatRecord));
					blocks.Pop();
				}
				else if (RuleName == "FRAME" && startBlockRecord.ObjectKind == ObjectKind.Frame)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.Frame));
					blocks.Pop();
				}
				else if (RuleName == "LD" && startBlockRecord.ObjectKind == ObjectKind.Legend)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.Legend));
					blocks.Pop();
				}
				else if (RuleName == "LEGENDEXCEPTION" && startBlockRecord.ObjectKind == ObjectKind.LegendException)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.LegendException));
					blocks.Pop();
				}
				else if (RuleName == "SERIESFORMAT" && startBlockRecord.ObjectKind == ObjectKind.Series)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.Series));
					blocks.Pop();
				}
				else if (RuleName == "CHARTFOMATS" && startBlockRecord.ObjectKind == ObjectKind.Sheet)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.Sheet));
					blocks.Pop();
				}
				else if (RuleName == "SS" && startBlockRecord.ObjectKind == ObjectKind.DataFormatRecord)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.DataFormatRecord));
					blocks.Pop();
				}
				else if (RuleName == "DROPBAR" && startBlockRecord.ObjectKind == ObjectKind.DropBarRecord)
				{
					rv.VisitRecord(EndBlockRecord.CreateEndBlock(ObjectKind.DropBarRecord));
					blocks.Pop();
				}
			}
		}
	}
}
