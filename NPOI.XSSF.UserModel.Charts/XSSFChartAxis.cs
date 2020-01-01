using NPOI.OpenXmlFormats.Dml.Chart;
using NPOI.SS.UserModel.Charts;
using System;

namespace NPOI.XSSF.UserModel.Charts
{
	/// Base class for all axis types.
	///
	/// @author Roman Kashitsyn
	public abstract class XSSFChartAxis : IChartAxis
	{
		protected XSSFChart chart;

		private static double Min_LOG_BASE = 2.0;

		private static double Max_LOG_BASE = 1000.0;

		public bool IsVisible
		{
			get
			{
				return GetDelete().val == 1;
			}
			set
			{
				GetDelete().val = (value ? 1 : 0);
			}
		}

		protected XSSFChartAxis(XSSFChart chart)
		{
			this.chart = chart;
		}

		public abstract long GetId();

		public abstract void CrossAxis(IChartAxis axis);

		public AxisPosition GetPosition()
		{
			return toAxisPosition(GetCTAxPos());
		}

		public void SetPosition(AxisPosition position)
		{
			GetCTAxPos().val = fromAxisPosition(position);
		}

		public void SetNumberFormat(string format)
		{
			GetCTNumFmt().formatCode = format;
			GetCTNumFmt().sourceLinked = true;
		}

		public string GetNumberFormat()
		{
			return GetCTNumFmt().formatCode;
		}

		public bool IsSetLogBase()
		{
			return GetCTScaling().IsSetLogBase();
		}

		public void SetLogBase(double logBase)
		{
			if (logBase < Min_LOG_BASE || Max_LOG_BASE < logBase)
			{
				throw new ArgumentException("Axis log base must be between 2 and 1000 (inclusive), got: " + logBase);
			}
			CT_Scaling cTScaling = GetCTScaling();
			if (cTScaling.IsSetLogBase())
			{
				cTScaling.logBase.val = logBase;
			}
			else
			{
				cTScaling.AddNewLogBase().val = logBase;
			}
		}

		public double GetLogBase()
		{
			CT_LogBase logBase = GetCTScaling().logBase;
			if (logBase != null)
			{
				return logBase.val;
			}
			return 0.0;
		}

		public bool IsSetMinimum()
		{
			return GetCTScaling().IsSetMin();
		}

		public void SetMinimum(double min)
		{
			CT_Scaling cTScaling = GetCTScaling();
			if (cTScaling.IsSetMin())
			{
				cTScaling.min.val = min;
			}
			else
			{
				cTScaling.AddNewMin().val = min;
			}
		}

		public double GetMinimum()
		{
			CT_Scaling cTScaling = GetCTScaling();
			if (cTScaling.IsSetMin())
			{
				return cTScaling.min.val;
			}
			return 0.0;
		}

		public bool IsSetMaximum()
		{
			return GetCTScaling().IsSetMax();
		}

		public void SetMaximum(double max)
		{
			CT_Scaling cTScaling = GetCTScaling();
			if (cTScaling.IsSetMax())
			{
				cTScaling.max.val = max;
			}
			else
			{
				cTScaling.AddNewMax().val = max;
			}
		}

		public double GetMaximum()
		{
			CT_Scaling cTScaling = GetCTScaling();
			if (cTScaling.IsSetMax())
			{
				return cTScaling.max.val;
			}
			return 0.0;
		}

		public AxisOrientation GetOrientation()
		{
			return toAxisOrientation(GetCTScaling().orientation);
		}

		public void SetOrientation(AxisOrientation orientation)
		{
			CT_Scaling cTScaling = GetCTScaling();
			ST_Orientation val = fromAxisOrientation(orientation);
			if (cTScaling.IsSetOrientation())
			{
				cTScaling.orientation.val = val;
			}
			else
			{
				GetCTScaling().AddNewOrientation().val = val;
			}
		}

		public AxisCrosses GetCrosses()
		{
			return toAxisCrosses(GetCTCrosses());
		}

		public void SetCrosses(AxisCrosses crosses)
		{
			GetCTCrosses().val = fromAxisCrosses(crosses);
		}

		protected abstract CT_AxPos GetCTAxPos();

		protected abstract CT_NumFmt GetCTNumFmt();

		protected abstract CT_Scaling GetCTScaling();

		protected abstract CT_Crosses GetCTCrosses();

		protected abstract CT_Boolean GetDelete();

		protected abstract CT_TickMark GetMajorCTTickMark();

		protected abstract CT_TickMark GetMinorCTTickMark();

		private static ST_Orientation fromAxisOrientation(AxisOrientation orientation)
		{
			switch (orientation)
			{
			case AxisOrientation.MinToMax:
				return ST_Orientation.minMax;
			case AxisOrientation.MaxToMin:
				return ST_Orientation.maxMin;
			default:
				throw new ArgumentException();
			}
		}

		private static AxisOrientation toAxisOrientation(CT_Orientation ctOrientation)
		{
			switch (ctOrientation.val)
			{
			case ST_Orientation.minMax:
				return AxisOrientation.MinToMax;
			case ST_Orientation.maxMin:
				return AxisOrientation.MaxToMin;
			default:
				throw new ArgumentException();
			}
		}

		private static ST_Crosses fromAxisCrosses(AxisCrosses crosses)
		{
			switch (crosses)
			{
			case AxisCrosses.AutoZero:
				return ST_Crosses.autoZero;
			case AxisCrosses.Min:
				return ST_Crosses.min;
			case AxisCrosses.Max:
				return ST_Crosses.max;
			default:
				throw new ArgumentException();
			}
		}

		private static AxisCrosses toAxisCrosses(CT_Crosses ctCrosses)
		{
			switch (ctCrosses.val)
			{
			case ST_Crosses.autoZero:
				return AxisCrosses.AutoZero;
			case ST_Crosses.max:
				return AxisCrosses.Max;
			case ST_Crosses.min:
				return AxisCrosses.Min;
			default:
				throw new ArgumentException();
			}
		}

		private static ST_AxPos fromAxisPosition(AxisPosition position)
		{
			switch (position)
			{
			case AxisPosition.Bottom:
				return ST_AxPos.b;
			case AxisPosition.Left:
				return ST_AxPos.l;
			case AxisPosition.Right:
				return ST_AxPos.r;
			case AxisPosition.Top:
				return ST_AxPos.t;
			default:
				throw new ArgumentException();
			}
		}

		private static AxisPosition toAxisPosition(CT_AxPos ctAxPos)
		{
			switch (ctAxPos.val)
			{
			case ST_AxPos.b:
				return AxisPosition.Bottom;
			case ST_AxPos.l:
				return AxisPosition.Left;
			case ST_AxPos.r:
				return AxisPosition.Right;
			case ST_AxPos.t:
				return AxisPosition.Top;
			default:
				return AxisPosition.Bottom;
			}
		}

		private static ST_TickMark fromAxisTickMark(AxisTickMark tickMark)
		{
			switch (tickMark)
			{
			case AxisTickMark.None:
				return ST_TickMark.none;
			case AxisTickMark.In:
				return ST_TickMark.@in;
			case AxisTickMark.Out:
				return ST_TickMark.@out;
			case AxisTickMark.Cross:
				return ST_TickMark.cross;
			default:
				throw new ArgumentException("Unknown AxisTickMark: " + tickMark);
			}
		}

		private static AxisTickMark toAxisTickMark(CT_TickMark ctTickMark)
		{
			switch (ctTickMark.val)
			{
			case ST_TickMark.none:
				return AxisTickMark.None;
			case ST_TickMark.@in:
				return AxisTickMark.In;
			case ST_TickMark.@out:
				return AxisTickMark.Out;
			case ST_TickMark.cross:
				return AxisTickMark.Cross;
			default:
				return AxisTickMark.Cross;
			}
		}

		public AxisTickMark GetMajorTickMark()
		{
			return toAxisTickMark(GetMajorCTTickMark());
		}

		public void SetMajorTickMark(AxisTickMark tickMark)
		{
			GetMajorCTTickMark().val = fromAxisTickMark(tickMark);
		}

		public AxisTickMark GetMinorTickMark()
		{
			return toAxisTickMark(GetMinorCTTickMark());
		}

		public void SetMinorTickMark(AxisTickMark tickMark)
		{
			GetMinorCTTickMark().val = fromAxisTickMark(tickMark);
		}
	}
}
