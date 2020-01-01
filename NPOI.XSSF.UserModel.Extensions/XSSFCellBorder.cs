using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using System;

namespace NPOI.XSSF.UserModel.Extensions
{
	/// This element Contains border formatting information, specifying border defInition formats (left, right, top, bottom, diagonal)
	/// for cells in the workbook.
	/// Color is optional.
	public class XSSFCellBorder
	{
		private ThemesTable _theme;

		private CT_Border border;

		/// Creates a Cell Border from the supplied XML defInition
		public XSSFCellBorder(CT_Border border, ThemesTable theme)
			: this(border)
		{
			_theme = theme;
		}

		/// Creates a Cell Border from the supplied XML defInition
		public XSSFCellBorder(CT_Border border)
		{
			this.border = border;
		}

		/// Creates a new, empty Cell Border.
		/// You need to attach this to the Styles Table
		public XSSFCellBorder()
		{
			border = new CT_Border();
		}

		/// Records the Themes Table that is associated with
		///  the current font, used when looking up theme
		///  based colours and properties.
		public void SetThemesTable(ThemesTable themes)
		{
			_theme = themes;
		}

		/// Returns the underlying XML bean.
		///
		/// @return CT_Border
		public CT_Border GetCTBorder()
		{
			return border;
		}

		/// Get the type of border to use for the selected border
		///
		/// @param side -  - where to apply the color defInition
		/// @return borderstyle - the type of border to use. default value is NONE if border style is not Set.
		/// @see BorderStyle
		public BorderStyle GetBorderStyle(BorderSide side)
		{
			CT_BorderPr cT_BorderPr = GetBorder(side);
			ST_BorderStyle? sT_BorderStyle = (cT_BorderPr != null) ? cT_BorderPr.style : ST_BorderStyle.none;
			return (BorderStyle)sT_BorderStyle.Value;
		}

		/// Set the type of border to use for the selected border
		///
		/// @param side  -  - where to apply the color defInition
		/// @param style - border style
		/// @see BorderStyle
		public void SetBorderStyle(BorderSide side, BorderStyle style)
		{
			GetBorder(side, true).style = (ST_BorderStyle)Enum.GetValues(typeof(ST_BorderStyle)).GetValue((int)(style + 1));
		}

		/// Get the color to use for the selected border
		///
		/// @param side - where to apply the color defInition
		/// @return color - color to use as XSSFColor. null if color is not set
		public XSSFColor GetBorderColor(BorderSide side)
		{
			CT_BorderPr cT_BorderPr = GetBorder(side);
			if (cT_BorderPr != null && cT_BorderPr.IsSetColor())
			{
				XSSFColor xSSFColor = new XSSFColor(cT_BorderPr.color);
				if (_theme != null)
				{
					_theme.InheritFromThemeAsRequired(xSSFColor);
				}
				return xSSFColor;
			}
			return null;
		}

		/// Set the color to use for the selected border
		///
		/// @param side  - where to apply the color defInition
		/// @param color - the color to use
		public void SetBorderColor(BorderSide side, XSSFColor color)
		{
			CT_BorderPr cT_BorderPr = GetBorder(side, true);
			if (color == null)
			{
				cT_BorderPr.UnsetColor();
			}
			else
			{
				cT_BorderPr.color = color.GetCTColor();
			}
		}

		private CT_BorderPr GetBorder(BorderSide side)
		{
			return GetBorder(side, false);
		}

		private CT_BorderPr GetBorder(BorderSide side, bool ensure)
		{
			CT_BorderPr cT_BorderPr;
			switch (side)
			{
			case BorderSide.TOP:
				cT_BorderPr = border.top;
				if (ensure && cT_BorderPr == null)
				{
					cT_BorderPr = border.AddNewTop();
				}
				break;
			case BorderSide.RIGHT:
				cT_BorderPr = border.right;
				if (ensure && cT_BorderPr == null)
				{
					cT_BorderPr = border.AddNewRight();
				}
				break;
			case BorderSide.BOTTOM:
				cT_BorderPr = border.bottom;
				if (ensure && cT_BorderPr == null)
				{
					cT_BorderPr = border.AddNewBottom();
				}
				break;
			case BorderSide.LEFT:
				cT_BorderPr = border.left;
				if (ensure && cT_BorderPr == null)
				{
					cT_BorderPr = border.AddNewLeft();
				}
				break;
			case BorderSide.DIAGONAL:
				cT_BorderPr = border.diagonal;
				if (ensure && cT_BorderPr == null)
				{
					cT_BorderPr = border.AddNewDiagonal();
				}
				break;
			default:
				throw new ArgumentException("No suitable side specified for the border");
			}
			return cT_BorderPr;
		}

		public override int GetHashCode()
		{
			return border.ToString().GetHashCode();
		}

		public override bool Equals(object o)
		{
			if (!(o is XSSFCellBorder))
			{
				return false;
			}
			XSSFCellBorder xSSFCellBorder = (XSSFCellBorder)o;
			return border.ToString().Equals(xSSFCellBorder.GetCTBorder().ToString());
		}
	}
}
