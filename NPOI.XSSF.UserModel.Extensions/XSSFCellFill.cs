using NPOI.OpenXmlFormats.Spreadsheet;

namespace NPOI.XSSF.UserModel.Extensions
{
	/// This element specifies fill formatting.
	/// A cell fill consists of a background color, foreground color, and pattern to be applied across the cell.
	public class XSSFCellFill
	{
		private CT_Fill _fill;

		/// Creates a CellFill from the supplied parts
		///
		/// @param fill - fill
		public XSSFCellFill(CT_Fill fill)
		{
			_fill = fill;
		}

		/// Creates an empty CellFill
		public XSSFCellFill()
		{
			_fill = new CT_Fill();
		}

		/// Get the background fill color.
		///
		/// @return fill color, null if color is not set
		public XSSFColor GetFillBackgroundColor()
		{
			CT_PatternFill patternFill = _fill.GetPatternFill();
			if (patternFill == null)
			{
				return null;
			}
			CT_Color bgColor = patternFill.bgColor;
			if (bgColor != null)
			{
				return new XSSFColor(bgColor);
			}
			return null;
		}

		/// Set the background fill color represented as a indexed color value.
		///
		/// @param index
		public void SetFillBackgroundColor(int index)
		{
			CT_PatternFill cT_PatternFill = EnsureCTPatternFill();
			CT_Color cT_Color = cT_PatternFill.IsSetBgColor() ? cT_PatternFill.bgColor : cT_PatternFill.AddNewBgColor();
			cT_Color.indexed = (uint)index;
			cT_Color.indexedSpecified = true;
		}

		/// Set the background fill color represented as a {@link XSSFColor} value.
		///
		/// @param color
		public void SetFillBackgroundColor(XSSFColor color)
		{
			CT_PatternFill cT_PatternFill = EnsureCTPatternFill();
			cT_PatternFill.bgColor = color.GetCTColor();
		}

		/// Get the foreground fill color.
		///
		/// @return XSSFColor - foreground color. null if color is not set
		public XSSFColor GetFillForegroundColor()
		{
			CT_PatternFill patternFill = _fill.GetPatternFill();
			if (patternFill == null)
			{
				return null;
			}
			CT_Color fgColor = patternFill.fgColor;
			if (fgColor != null)
			{
				return new XSSFColor(fgColor);
			}
			return null;
		}

		/// Set the foreground fill color as a indexed color value
		///
		/// @param index - the color to use
		public void SetFillForegroundColor(int index)
		{
			CT_PatternFill cT_PatternFill = EnsureCTPatternFill();
			CT_Color cT_Color = cT_PatternFill.IsSetFgColor() ? cT_PatternFill.fgColor : cT_PatternFill.AddNewFgColor();
			cT_Color.indexed = (uint)index;
		}

		/// Set the foreground fill color represented as a {@link XSSFColor} value.
		///
		/// @param color - the color to use
		public void SetFillForegroundColor(XSSFColor color)
		{
			CT_PatternFill cT_PatternFill = EnsureCTPatternFill();
			cT_PatternFill.fgColor = color.GetCTColor();
		}

		/// get the fill pattern
		///
		/// @return fill pattern type. null if fill pattern is not set
		public ST_PatternType GetPatternType()
		{
			CT_PatternFill patternFill = _fill.GetPatternFill();
			if (patternFill != null)
			{
				return patternFill.patternType;
			}
			return ST_PatternType.none;
		}

		/// set the fill pattern
		///
		/// @param patternType fill pattern to use
		public void SetPatternType(ST_PatternType patternType)
		{
			CT_PatternFill cT_PatternFill = EnsureCTPatternFill();
			cT_PatternFill.patternType = patternType;
		}

		private CT_PatternFill EnsureCTPatternFill()
		{
			CT_PatternFill cT_PatternFill = _fill.GetPatternFill();
			if (cT_PatternFill == null)
			{
				cT_PatternFill = _fill.AddNewPatternFill();
			}
			return cT_PatternFill;
		}

		/// Returns the underlying XML bean.
		///
		/// @return CT_Fill
		internal CT_Fill GetCTFill()
		{
			return _fill;
		}

		public override int GetHashCode()
		{
			return _fill.ToString().GetHashCode();
		}

		public override bool Equals(object o)
		{
			if (!(o is XSSFCellFill))
			{
				return false;
			}
			XSSFCellFill xSSFCellFill = (XSSFCellFill)o;
			return _fill.ToString().Equals(xSSFCellFill.GetCTFill().ToString());
		}
	}
}
