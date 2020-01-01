using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel
{
	/// @author Yegor Kozlov
	public class XSSFPatternFormatting : IPatternFormatting
	{
		private CT_Fill _fill;

		public short FillBackgroundColor
		{
			get
			{
				if (!_fill.IsSetPatternFill())
				{
					return 0;
				}
				if (!_fill.GetPatternFill().bgColor.indexedSpecified)
				{
					return 0;
				}
				return (short)_fill.GetPatternFill().bgColor.indexed;
			}
			set
			{
				CT_PatternFill cT_PatternFill = _fill.IsSetPatternFill() ? _fill.GetPatternFill() : _fill.AddNewPatternFill();
				CT_Color cT_Color = new CT_Color();
				cT_Color.indexed = (uint)value;
				cT_Color.indexedSpecified = true;
				cT_PatternFill.bgColor = cT_Color;
			}
		}

		public short FillForegroundColor
		{
			get
			{
				if (!_fill.IsSetPatternFill() || !_fill.GetPatternFill().IsSetFgColor())
				{
					return 0;
				}
				if (!_fill.GetPatternFill().fgColor.indexedSpecified)
				{
					return 0;
				}
				return (short)_fill.GetPatternFill().fgColor.indexed;
			}
			set
			{
				CT_PatternFill cT_PatternFill = _fill.IsSetPatternFill() ? _fill.GetPatternFill() : _fill.AddNewPatternFill();
				CT_Color cT_Color = new CT_Color();
				cT_Color.indexed = (uint)value;
				cT_Color.indexedSpecified = true;
				cT_PatternFill.fgColor = cT_Color;
			}
		}

		public short FillPattern
		{
			get
			{
				if (!_fill.IsSetPatternFill() || !_fill.GetPatternFill().IsSetPatternType())
				{
					return 0;
				}
				return (short)_fill.GetPatternFill().patternType;
			}
			set
			{
				CT_PatternFill cT_PatternFill = _fill.IsSetPatternFill() ? _fill.GetPatternFill() : _fill.AddNewPatternFill();
				cT_PatternFill.patternType = (ST_PatternType)value;
			}
		}

		public XSSFPatternFormatting(CT_Fill fill)
		{
			_fill = fill;
		}
	}
}
