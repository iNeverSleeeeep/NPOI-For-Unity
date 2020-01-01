using NPOI.HSSF.Record;
using NPOI.HSSF.Record.CF;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// High level representation for Border Formatting component
	/// of Conditional Formatting Settings
	///
	/// @author Dmitriy Kumshayev
	public class HSSFBorderFormatting : IBorderFormatting
	{
		private CFRuleRecord cfRuleRecord;

		private BorderFormatting borderFormatting;

		public BorderStyle BorderBottom
		{
			get
			{
				return borderFormatting.BorderBottom;
			}
			set
			{
				borderFormatting.BorderBottom = value;
				if (value != 0)
				{
					cfRuleRecord.IsBottomBorderModified = true;
				}
			}
		}

		public BorderStyle BorderDiagonal
		{
			get
			{
				return borderFormatting.BorderDiagonal;
			}
			set
			{
				borderFormatting.BorderDiagonal = value;
				if (value != 0)
				{
					cfRuleRecord.IsBottomLeftTopRightBorderModified = true;
					cfRuleRecord.IsTopLeftBottomRightBorderModified = true;
				}
			}
		}

		public BorderStyle BorderLeft
		{
			get
			{
				return borderFormatting.BorderLeft;
			}
			set
			{
				borderFormatting.BorderLeft = value;
				if (value != 0)
				{
					cfRuleRecord.IsLeftBorderModified = true;
				}
			}
		}

		public BorderStyle BorderRight
		{
			get
			{
				return borderFormatting.BorderRight;
			}
			set
			{
				borderFormatting.BorderRight = value;
				if (value != 0)
				{
					cfRuleRecord.IsRightBorderModified = true;
				}
			}
		}

		public BorderStyle BorderTop
		{
			get
			{
				return borderFormatting.BorderTop;
			}
			set
			{
				borderFormatting.BorderTop = value;
				if (value != 0)
				{
					cfRuleRecord.IsTopBorderModified = true;
				}
			}
		}

		public short BottomBorderColor
		{
			get
			{
				return borderFormatting.BottomBorderColor;
			}
			set
			{
				borderFormatting.BottomBorderColor = value;
				if (value != 0)
				{
					cfRuleRecord.IsBottomBorderModified = true;
				}
			}
		}

		public short DiagonalBorderColor
		{
			get
			{
				return borderFormatting.DiagonalBorderColor;
			}
			set
			{
				borderFormatting.DiagonalBorderColor = value;
				if (value != 0)
				{
					cfRuleRecord.IsBottomLeftTopRightBorderModified = true;
					cfRuleRecord.IsTopLeftBottomRightBorderModified = true;
				}
			}
		}

		public short LeftBorderColor
		{
			get
			{
				return borderFormatting.LeftBorderColor;
			}
			set
			{
				borderFormatting.LeftBorderColor = value;
				if (value != 0)
				{
					cfRuleRecord.IsLeftBorderModified = true;
				}
			}
		}

		public short RightBorderColor
		{
			get
			{
				return borderFormatting.RightBorderColor;
			}
			set
			{
				borderFormatting.RightBorderColor = value;
				if (value != 0)
				{
					cfRuleRecord.IsRightBorderModified = true;
				}
			}
		}

		public short TopBorderColor
		{
			get
			{
				return borderFormatting.TopBorderColor;
			}
			set
			{
				borderFormatting.TopBorderColor = value;
				if (value != 0)
				{
					cfRuleRecord.IsTopBorderModified = true;
				}
			}
		}

		public bool IsBackwardDiagonalOn
		{
			get
			{
				return borderFormatting.IsBackwardDiagonalOn;
			}
			set
			{
				borderFormatting.IsBackwardDiagonalOn = value;
				if (value)
				{
					cfRuleRecord.IsTopLeftBottomRightBorderModified = value;
				}
			}
		}

		public bool IsForwardDiagonalOn
		{
			get
			{
				return borderFormatting.IsForwardDiagonalOn;
			}
			set
			{
				borderFormatting.IsForwardDiagonalOn = value;
				if (value)
				{
					cfRuleRecord.IsBottomLeftTopRightBorderModified = value;
				}
			}
		}

		public HSSFBorderFormatting(CFRuleRecord cfRuleRecord)
		{
			this.cfRuleRecord = cfRuleRecord;
			borderFormatting = cfRuleRecord.BorderFormatting;
		}

		public BorderFormatting GetBorderFormattingBlock()
		{
			return borderFormatting;
		}
	}
}
