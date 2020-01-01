using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using NPOI.XSSF.UserModel.Extensions;
using System;
using System.Xml;

namespace NPOI.XSSF.UserModel
{
	/// High level representation of the the possible formatting information for the contents of the cells on a sheet in a
	/// SpreadsheetML document.
	///
	/// @see NPOI.xssf.usermodel.XSSFWorkbook#CreateCellStyle()
	/// @see NPOI.xssf.usermodel.XSSFWorkbook#getCellStyleAt(short)
	/// @see NPOI.xssf.usermodel.XSSFCell#setCellStyle(NPOI.ss.usermodel.CellStyle)
	public class XSSFCellStyle : ICellStyle
	{
		private int _cellXfId;

		private StylesTable _stylesSource;

		private CT_Xf _cellXf;

		private CT_Xf _cellStyleXf;

		private XSSFFont _font;

		private XSSFCellAlignment _cellAlignment;

		private ThemesTable _theme;

		public HorizontalAlignment Alignment
		{
			get
			{
				return GetAlignmentEnum();
			}
			set
			{
				GetCellAlignment().Horizontal = value;
			}
		}

		public BorderStyle BorderBottom
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return BorderStyle.None;
				}
				int borderId = (int)_cellXf.borderId;
				CT_Border cTBorder = _stylesSource.GetBorderAt(borderId).GetCTBorder();
				if (!cTBorder.IsSetBottom())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)cTBorder.bottom.style;
			}
			set
			{
				CT_Border cTBorder = GetCTBorder();
				CT_BorderPr cT_BorderPr = cTBorder.IsSetBottom() ? cTBorder.bottom : cTBorder.AddNewBottom();
				if (value == BorderStyle.None)
				{
					cTBorder.unsetBottom();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		public BorderStyle BorderLeft
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return BorderStyle.None;
				}
				int borderId = (int)_cellXf.borderId;
				CT_Border cTBorder = _stylesSource.GetBorderAt(borderId).GetCTBorder();
				if (!cTBorder.IsSetLeft())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)cTBorder.left.style;
			}
			set
			{
				CT_Border cTBorder = GetCTBorder();
				CT_BorderPr cT_BorderPr = cTBorder.IsSetLeft() ? cTBorder.left : cTBorder.AddNewLeft();
				if (value == BorderStyle.None)
				{
					cTBorder.unsetLeft();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		/// <summary>
		/// Get the type of border to use for the right border of the cell
		/// </summary>
		public BorderStyle BorderRight
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return BorderStyle.None;
				}
				int borderId = (int)_cellXf.borderId;
				CT_Border cTBorder = _stylesSource.GetBorderAt(borderId).GetCTBorder();
				if (!cTBorder.IsSetRight())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)cTBorder.right.style;
			}
			set
			{
				CT_Border cTBorder = GetCTBorder();
				CT_BorderPr cT_BorderPr = cTBorder.IsSetRight() ? cTBorder.right : cTBorder.AddNewRight();
				if (value == BorderStyle.None)
				{
					cTBorder.unsetRight();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		public BorderStyle BorderTop
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return BorderStyle.None;
				}
				int borderId = (int)_cellXf.borderId;
				CT_Border cTBorder = _stylesSource.GetBorderAt(borderId).GetCTBorder();
				if (!cTBorder.IsSetTop())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)cTBorder.top.style;
			}
			set
			{
				CT_Border cTBorder = GetCTBorder();
				CT_BorderPr cT_BorderPr = cTBorder.IsSetTop() ? cTBorder.top : cTBorder.AddNewTop();
				if (value == BorderStyle.None)
				{
					cTBorder.unsetTop();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		/// Get the color to use for the bottom border
		/// Color is optional. When missing, IndexedColors.Automatic is implied.
		/// @return the index of the color defInition, default value is {@link NPOI.ss.usermodel.IndexedColors#AUTOMATIC}
		/// @see NPOI.ss.usermodel.IndexedColors
		public short BottomBorderColor
		{
			get
			{
				XSSFColor bottomBorderXSSFColor = BottomBorderXSSFColor;
				if (bottomBorderXSSFColor != null)
				{
					return bottomBorderXSSFColor.Indexed;
				}
				return IndexedColors.Black.Index;
			}
			set
			{
				XSSFColor xSSFColor = new XSSFColor();
				xSSFColor.Indexed = value;
				SetBottomBorderColor(xSSFColor);
			}
		}

		/// Get the color to use for the bottom border as a {@link XSSFColor}
		///
		/// @return the used color or <code>null</code> if not Set
		public XSSFColor BottomBorderXSSFColor
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return null;
				}
				int borderId = (int)_cellXf.borderId;
				XSSFCellBorder borderAt = _stylesSource.GetBorderAt(borderId);
				return borderAt.GetBorderColor(BorderSide.BOTTOM);
			}
		}

		/// Get the index of the number format (numFmt) record used by this cell format.
		///
		/// @return the index of the number format
		public short DataFormat
		{
			get
			{
				return (short)_cellXf.numFmtId;
			}
			set
			{
				_cellXf.applyNumberFormat = true;
				_cellXf.numFmtId = (uint)value;
			}
		}

		/// <summary>
		/// Get the background fill color.
		/// Note - many cells are actually filled with a foreground fill, not a background fill
		/// </summary>
		public short FillBackgroundColor
		{
			get
			{
				XSSFColor xSSFColor = (XSSFColor)FillBackgroundColorColor;
				if (xSSFColor != null)
				{
					return xSSFColor.Indexed;
				}
				return IndexedColors.Automatic.Index;
			}
			set
			{
				XSSFColor xSSFColor = new XSSFColor();
				xSSFColor.Indexed = value;
				SetFillBackgroundColor(xSSFColor);
			}
		}

		/// Get the background fill color.
		/// <p>
		/// Note - many cells are actually Filled with a foreground
		///  Fill, not a background fill - see {@link #getFillForegroundColor()}
		/// </p>
		/// @see NPOI.xssf.usermodel.XSSFColor#getRgb()
		/// @return XSSFColor - fill color or <code>null</code> if not Set
		public IColor FillBackgroundColorColor
		{
			get
			{
				return FillBackgroundXSSFColor;
			}
			set
			{
				FillBackgroundXSSFColor = (XSSFColor)value;
			}
		}

		public XSSFColor FillBackgroundXSSFColor
		{
			get
			{
				if (!_cellXf.applyFill)
				{
					return null;
				}
				int fillId = (int)_cellXf.fillId;
				XSSFCellFill fillAt = _stylesSource.GetFillAt(fillId);
				XSSFColor fillBackgroundColor = fillAt.GetFillBackgroundColor();
				if (fillBackgroundColor != null && _theme != null)
				{
					_theme.InheritFromThemeAsRequired(fillBackgroundColor);
				}
				return fillBackgroundColor;
			}
			set
			{
				CT_Fill cTFill = GetCTFill();
				CT_PatternFill cT_PatternFill = cTFill.patternFill;
				if (value == null)
				{
					if (cT_PatternFill != null)
					{
						cT_PatternFill.UnsetBgColor();
					}
				}
				else
				{
					if (cT_PatternFill == null)
					{
						cT_PatternFill = cTFill.AddNewPatternFill();
					}
					cT_PatternFill.bgColor = value.GetCTColor();
				}
				int fillId = _stylesSource.PutFill(new XSSFCellFill(cTFill));
				_cellXf.fillId = (uint)fillId;
				_cellXf.applyFill = true;
			}
		}

		/// Get the foreground fill color.
		/// <p>
		/// Many cells are Filled with this, instead of a
		///  background color ({@link #getFillBackgroundColor()})
		/// </p>
		/// @see IndexedColors
		/// @return fill color, default value is {@link NPOI.ss.usermodel.IndexedColors#AUTOMATIC}
		public short FillForegroundColor
		{
			get
			{
				XSSFColor xSSFColor = (XSSFColor)FillForegroundColorColor;
				if (xSSFColor != null)
				{
					return xSSFColor.Indexed;
				}
				return IndexedColors.Automatic.Index;
			}
			set
			{
				XSSFColor xSSFColor = new XSSFColor();
				xSSFColor.Indexed = value;
				SetFillForegroundColor(xSSFColor);
			}
		}

		/// <summary>
		/// Get the foreground fill color.
		/// </summary>
		public IColor FillForegroundColorColor
		{
			get
			{
				return FillForegroundXSSFColor;
			}
			set
			{
				FillForegroundXSSFColor = (XSSFColor)value;
			}
		}

		/// <summary>
		/// Get the foreground fill color.
		/// </summary>
		public XSSFColor FillForegroundXSSFColor
		{
			get
			{
				if (!_cellXf.applyFill)
				{
					return null;
				}
				int fillId = (int)_cellXf.fillId;
				XSSFCellFill fillAt = _stylesSource.GetFillAt(fillId);
				XSSFColor fillForegroundColor = fillAt.GetFillForegroundColor();
				if (fillForegroundColor != null && _theme != null)
				{
					_theme.InheritFromThemeAsRequired(fillForegroundColor);
				}
				return fillForegroundColor;
			}
			set
			{
				CT_Fill cTFill = GetCTFill();
				CT_PatternFill cT_PatternFill = cTFill.patternFill;
				if (value == null)
				{
					if (cT_PatternFill != null)
					{
						cT_PatternFill.UnsetFgColor();
					}
				}
				else
				{
					if (cT_PatternFill == null)
					{
						cT_PatternFill = cTFill.AddNewPatternFill();
					}
					cT_PatternFill.fgColor = value.GetCTColor();
				}
				int fillId = _stylesSource.PutFill(new XSSFCellFill(cTFill));
				_cellXf.fillId = (uint)fillId;
				_cellXf.applyFill = true;
			}
		}

		public FillPattern FillPattern
		{
			get
			{
				if (!_cellXf.applyFill)
				{
					return FillPattern.NoFill;
				}
				int fillId = (int)_cellXf.fillId;
				XSSFCellFill fillAt = _stylesSource.GetFillAt(fillId);
				ST_PatternType patternType = fillAt.GetPatternType();
				return (FillPattern)patternType;
			}
			set
			{
				CT_Fill cTFill = GetCTFill();
				CT_PatternFill cT_PatternFill = cTFill.IsSetPatternFill() ? cTFill.GetPatternFill() : cTFill.AddNewPatternFill();
				if (value == FillPattern.NoFill && cT_PatternFill.IsSetPatternType())
				{
					cT_PatternFill.UnsetPatternType();
				}
				else
				{
					cT_PatternFill.patternType = (ST_PatternType)value;
				}
				int fillId = _stylesSource.PutFill(new XSSFCellFill(cTFill));
				_cellXf.fillId = (uint)fillId;
				_cellXf.applyFill = true;
			}
		}

		/// Gets the index of the font for this style
		///
		/// @return short - font index
		/// @see NPOI.xssf.usermodel.XSSFWorkbook#getFontAt(short)
		public short FontIndex
		{
			get
			{
				return (short)FontId;
			}
		}

		/// Get whether the cell's using this style are to be hidden
		///
		/// @return bool -  whether the cell using this style is hidden
		public bool IsHidden
		{
			get
			{
				if (!_cellXf.IsSetProtection() || !_cellXf.protection.IsSetHidden())
				{
					return false;
				}
				return _cellXf.protection.hidden;
			}
			set
			{
				if (!_cellXf.IsSetProtection())
				{
					_cellXf.AddNewProtection();
				}
				_cellXf.protection.hidden = value;
			}
		}

		/// Get the number of spaces to indent the text in the cell
		///
		/// @return indent - number of spaces
		public short Indention
		{
			get
			{
				CT_CellAlignment alignment = _cellXf.alignment;
				return (short)((alignment == null) ? 0 : alignment.indent);
			}
			set
			{
				GetCellAlignment().Indent = value;
			}
		}

		/// Get the index within the StylesTable (sequence within the collection of CT_Xf elements)
		///
		/// @return unique index number of the underlying record this style represents
		public short Index
		{
			get
			{
				return (short)_cellXfId;
			}
		}

		/// Get the color to use for the left border
		///
		/// @return the index of the color defInition, default value is {@link NPOI.ss.usermodel.IndexedColors#BLACK}
		/// @see NPOI.ss.usermodel.IndexedColors
		public short LeftBorderColor
		{
			get
			{
				XSSFColor leftBorderXSSFColor = LeftBorderXSSFColor;
				if (leftBorderXSSFColor != null)
				{
					return leftBorderXSSFColor.Indexed;
				}
				return IndexedColors.Black.Index;
			}
			set
			{
				XSSFColor xSSFColor = new XSSFColor();
				xSSFColor.Indexed = value;
				SetLeftBorderColor(xSSFColor);
			}
		}

		public XSSFColor DiagonalBorderXSSFColor
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return null;
				}
				int borderId = (int)_cellXf.borderId;
				XSSFCellBorder borderAt = _stylesSource.GetBorderAt(borderId);
				return borderAt.GetBorderColor(BorderSide.DIAGONAL);
			}
		}

		/// Get the color to use for the left border
		///
		/// @return the index of the color defInition or <code>null</code> if not Set
		/// @see NPOI.ss.usermodel.IndexedColors
		public XSSFColor LeftBorderXSSFColor
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return null;
				}
				int borderId = (int)_cellXf.borderId;
				XSSFCellBorder borderAt = _stylesSource.GetBorderAt(borderId);
				return borderAt.GetBorderColor(BorderSide.LEFT);
			}
		}

		/// <summary>
		/// Get whether the cell's using this style are locked
		/// </summary>
		public bool IsLocked
		{
			get
			{
				if (!_cellXf.IsSetProtection())
				{
					return true;
				}
				return _cellXf.protection.locked;
			}
			set
			{
				if (!_cellXf.IsSetProtection())
				{
					_cellXf.AddNewProtection();
				}
				_cellXf.protection.locked = value;
			}
		}

		/// <summary>
		/// Get the color to use for the right border
		/// </summary>
		public short RightBorderColor
		{
			get
			{
				XSSFColor rightBorderXSSFColor = RightBorderXSSFColor;
				if (rightBorderXSSFColor != null)
				{
					return rightBorderXSSFColor.Indexed;
				}
				return IndexedColors.Black.Index;
			}
			set
			{
				XSSFColor xSSFColor = new XSSFColor();
				xSSFColor.Indexed = value;
				SetRightBorderColor(xSSFColor);
			}
		}

		/// <summary>
		/// Get the color to use for the right border
		/// </summary>
		/// <returns></returns>
		public XSSFColor RightBorderXSSFColor
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return null;
				}
				int borderId = (int)_cellXf.borderId;
				XSSFCellBorder borderAt = _stylesSource.GetBorderAt(borderId);
				return borderAt.GetBorderColor(BorderSide.RIGHT);
			}
		}

		/// <summary>
		/// Get the degree of rotation (between 0 and 180 degrees) for the text in the cell
		/// </summary>
		/// <example>
		/// Expressed in degrees. Values range from 0 to 180. The first letter of
		/// the text is considered the center-point of the arc.
		/// For 0 - 90, the value represents degrees above horizon. For 91-180 the degrees below the horizon is calculated as:
		/// <code>[degrees below horizon] = 90 - textRotation.</code>
		/// </example>
		public short Rotation
		{
			get
			{
				CT_CellAlignment alignment = _cellXf.alignment;
				return (short)((alignment == null) ? 0 : alignment.textRotation);
			}
			set
			{
				GetCellAlignment().TextRotation = value;
			}
		}

		/// Get the color to use for the top border
		///
		/// @return the index of the color defInition, default value is {@link NPOI.ss.usermodel.IndexedColors#BLACK}
		/// @see NPOI.ss.usermodel.IndexedColors
		public short TopBorderColor
		{
			get
			{
				XSSFColor topBorderXSSFColor = TopBorderXSSFColor;
				if (topBorderXSSFColor != null)
				{
					return topBorderXSSFColor.Indexed;
				}
				return IndexedColors.Black.Index;
			}
			set
			{
				XSSFColor xSSFColor = new XSSFColor();
				xSSFColor.Indexed = value;
				SetTopBorderColor(xSSFColor);
			}
		}

		/// <summary>
		/// Get the color to use for the top border
		/// </summary>
		/// <returns></returns>
		public XSSFColor TopBorderXSSFColor
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return null;
				}
				int borderId = (int)_cellXf.borderId;
				XSSFCellBorder borderAt = _stylesSource.GetBorderAt(borderId);
				return borderAt.GetBorderColor(BorderSide.TOP);
			}
		}

		/// <summary>
		/// Get the type of vertical alignment for the cell
		/// </summary>
		public VerticalAlignment VerticalAlignment
		{
			get
			{
				return GetVerticalAlignmentEnum();
			}
			set
			{
				GetCellAlignment().Vertical = value;
			}
		}

		/// <summary>
		/// Whether the text in a cell should be line-wrapped within the cell.
		/// </summary>
		public bool WrapText
		{
			get
			{
				CT_CellAlignment alignment = _cellXf.alignment;
				if (alignment != null)
				{
					return alignment.wrapText;
				}
				return false;
			}
			set
			{
				GetCellAlignment().WrapText = value;
			}
		}

		private int FontId
		{
			get
			{
				if (_cellXf.IsSetFontId())
				{
					return (int)_cellXf.fontId;
				}
				return (int)_cellStyleXf.fontId;
			}
		}

		public bool ShrinkToFit
		{
			get
			{
				CT_CellAlignment alignment = _cellXf.alignment;
				if (alignment != null)
				{
					return alignment.shrinkToFit;
				}
				return false;
			}
			set
			{
				GetCTCellAlignment().shrinkToFit = value;
			}
		}

		public short BorderDiagonalColor
		{
			get
			{
				XSSFColor diagonalBorderXSSFColor = DiagonalBorderXSSFColor;
				if (diagonalBorderXSSFColor != null)
				{
					return diagonalBorderXSSFColor.Indexed;
				}
				return IndexedColors.Black.Index;
			}
			set
			{
				XSSFColor xSSFColor = new XSSFColor();
				xSSFColor.Indexed = value;
				SetDiagonalBorderColor(xSSFColor);
			}
		}

		public BorderStyle BorderDiagonalLineStyle
		{
			get
			{
				if (!_cellXf.applyBorder)
				{
					return BorderStyle.None;
				}
				int borderId = (int)_cellXf.borderId;
				CT_Border cTBorder = _stylesSource.GetBorderAt(borderId).GetCTBorder();
				if (!cTBorder.IsSetDiagonal())
				{
					return BorderStyle.None;
				}
				return (BorderStyle)cTBorder.diagonal.style;
			}
			set
			{
				CT_Border cTBorder = GetCTBorder();
				CT_BorderPr cT_BorderPr = cTBorder.IsSetDiagonal() ? cTBorder.diagonal : cTBorder.AddNewDiagonal();
				if (value == BorderStyle.None)
				{
					cTBorder.unsetDiagonal();
				}
				else
				{
					cT_BorderPr.style = (ST_BorderStyle)value;
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		public BorderDiagonal BorderDiagonal
		{
			get
			{
				CT_Border cTBorder = GetCTBorder();
				if (cTBorder.diagonalDown && cTBorder.diagonalUp)
				{
					return BorderDiagonal.Both;
				}
				if (cTBorder.diagonalDown)
				{
					return BorderDiagonal.Backward;
				}
				if (cTBorder.diagonalUp)
				{
					return BorderDiagonal.Forward;
				}
				return BorderDiagonal.None;
			}
			set
			{
				CT_Border cTBorder = GetCTBorder();
				switch (value)
				{
				case BorderDiagonal.Both:
					cTBorder.diagonalDown = true;
					cTBorder.diagonalDownSpecified = true;
					cTBorder.diagonalUp = true;
					cTBorder.diagonalUpSpecified = true;
					break;
				case BorderDiagonal.Forward:
					cTBorder.diagonalDown = false;
					cTBorder.diagonalDownSpecified = false;
					cTBorder.diagonalUp = true;
					cTBorder.diagonalUpSpecified = true;
					break;
				case BorderDiagonal.Backward:
					cTBorder.diagonalDown = true;
					cTBorder.diagonalDownSpecified = true;
					cTBorder.diagonalUp = false;
					cTBorder.diagonalUpSpecified = false;
					break;
				default:
					cTBorder.unsetDiagonal();
					cTBorder.diagonalDown = false;
					cTBorder.diagonalDownSpecified = false;
					cTBorder.diagonalUp = false;
					cTBorder.diagonalUpSpecified = false;
					break;
				}
			}
		}

		/// Creates a Cell Style from the supplied parts
		/// @param cellXfId The main XF for the cell. Must be a valid 0-based index into the XF table
		/// @param cellStyleXfId Optional, style xf. A value of <code>-1</code> means no xf.
		/// @param stylesSource Styles Source to work off
		public XSSFCellStyle(int cellXfId, int cellStyleXfId, StylesTable stylesSource, ThemesTable theme)
		{
			_cellXfId = cellXfId;
			_stylesSource = stylesSource;
			_cellXf = stylesSource.GetCellXfAt(_cellXfId);
			_cellStyleXf = ((cellStyleXfId == -1) ? null : stylesSource.GetCellStyleXfAt(cellStyleXfId));
			_theme = theme;
		}

		/// Used so that StylesSource can figure out our location
		public CT_Xf GetCoreXf()
		{
			return _cellXf;
		}

		/// Used so that StylesSource can figure out our location
		public CT_Xf GetStyleXf()
		{
			return _cellStyleXf;
		}

		/// <summary>
		/// Creates an empty Cell Style
		/// </summary>
		/// <param name="stylesSource"></param>
		public XSSFCellStyle(StylesTable stylesSource)
		{
			_stylesSource = stylesSource;
			_cellXf = new CT_Xf();
			_cellStyleXf = null;
		}

		/// Verifies that this style belongs to the supplied Workbook
		///  Styles Source.
		/// Will throw an exception if it belongs to a different one.
		/// This is normally called when trying to assign a style to a
		///  cell, to ensure the cell and the style are from the same
		///  workbook (if they're not, it won't work)
		/// @throws ArgumentException if there's a workbook mis-match
		public void VerifyBelongsToStylesSource(StylesTable src)
		{
			if (_stylesSource != src)
			{
				throw new ArgumentException("This Style does not belong to the supplied Workbook Stlyes Source. Are you trying to assign a style from one workbook to the cell of a differnt workbook?");
			}
		}

		/// Clones all the style information from another
		///  XSSFCellStyle, onto this one. This
		///  XSSFCellStyle will then have all the same
		///  properties as the source, but the two may
		///  be edited independently.
		/// Any stylings on this XSSFCellStyle will be lost!
		///
		/// The source XSSFCellStyle could be from another
		///  XSSFWorkbook if you like. This allows you to
		///  copy styles from one XSSFWorkbook to another.
		public void CloneStyleFrom(ICellStyle source)
		{
			if (source is XSSFCellStyle)
			{
				XSSFCellStyle xSSFCellStyle = (XSSFCellStyle)source;
				if (xSSFCellStyle._stylesSource == _stylesSource)
				{
					_cellXf = xSSFCellStyle.GetCoreXf().Copy();
					_cellStyleXf = xSSFCellStyle.GetStyleXf().Copy();
				}
				else
				{
					try
					{
						if (_cellXf.IsSetAlignment())
						{
							_cellXf.UnsetAlignment();
						}
						if (_cellXf.IsSetExtLst())
						{
							_cellXf.UnsetExtLst();
						}
						_cellXf = xSSFCellStyle.GetCoreXf().Copy();
						_stylesSource.ReplaceCellXfAt(_cellXfId, _cellXf);
					}
					catch (XmlException ex)
					{
						throw new POIXMLException(ex);
					}
					string dataFormatString = xSSFCellStyle.GetDataFormatString();
					DataFormat = new XSSFDataFormat(_stylesSource).GetFormat(dataFormatString);
					try
					{
						CT_Font font = xSSFCellStyle.GetFont().GetCTFont().Clone();
						XSSFFont xSSFFont = new XSSFFont(font);
						xSSFFont.RegisterTo(_stylesSource);
						SetFont(xSSFFont);
					}
					catch (XmlException ex2)
					{
						throw new POIXMLException(ex2);
					}
				}
				_font = null;
				_cellAlignment = null;
				return;
			}
			throw new ArgumentException("Can only clone from one XSSFCellStyle to another, not between HSSFCellStyle and XSSFCellStyle");
		}

		/// <summary>
		/// Get the type of horizontal alignment for the cell
		/// </summary>
		/// <returns>the type of alignment</returns>
		internal HorizontalAlignment GetAlignmentEnum()
		{
			CT_CellAlignment alignment = _cellXf.alignment;
			if (alignment != null && alignment.IsSetHorizontal())
			{
				return (HorizontalAlignment)alignment.horizontal;
			}
			return HorizontalAlignment.General;
		}

		/// Get the contents of the format string, by looking up
		/// the StylesSource
		///
		/// @return the number format string
		public string GetDataFormatString()
		{
			int dataFormat = DataFormat;
			return new XSSFDataFormat(_stylesSource).GetFormat((short)dataFormat);
		}

		/// Gets the font for this style
		/// @return Font - font
		public XSSFFont GetFont()
		{
			if (_font == null)
			{
				_font = _stylesSource.GetFontAt(FontId);
			}
			return _font;
		}

		/// <summary>
		/// Get the type of vertical alignment for the cell
		/// </summary>
		/// <returns></returns>
		internal VerticalAlignment GetVerticalAlignmentEnum()
		{
			CT_CellAlignment alignment = _cellXf.alignment;
			if (alignment != null && alignment.IsSetVertical())
			{
				return (VerticalAlignment)alignment.vertical;
			}
			return VerticalAlignment.Bottom;
		}

		/// Set the color to use for the bottom border
		///
		/// @param color the color to use, null means no color
		public void SetBottomBorderColor(XSSFColor color)
		{
			CT_Border cTBorder = GetCTBorder();
			if (color != null || cTBorder.IsSetBottom())
			{
				CT_BorderPr cT_BorderPr = cTBorder.IsSetBottom() ? cTBorder.bottom : cTBorder.AddNewBottom();
				if (color != null)
				{
					cT_BorderPr.SetColor(color.GetCTColor());
				}
				else
				{
					cT_BorderPr.UnsetColor();
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		public void SetFillBackgroundColor(XSSFColor color)
		{
			CT_Fill cTFill = GetCTFill();
			CT_PatternFill cT_PatternFill = cTFill.GetPatternFill();
			if (color == null)
			{
				if (cT_PatternFill != null)
				{
					cT_PatternFill.UnsetBgColor();
				}
			}
			else
			{
				if (cT_PatternFill == null)
				{
					cT_PatternFill = cTFill.AddNewPatternFill();
				}
				cT_PatternFill.bgColor = color.GetCTColor();
			}
			int fillId = _stylesSource.PutFill(new XSSFCellFill(cTFill));
			_cellXf.fillId = (uint)fillId;
			_cellXf.applyFill = true;
		}

		/// * Set the foreground fill color represented as a {@link XSSFColor} value.
		///  * <br />
		/// * <i>Note: Ensure Foreground color is Set prior to background color.</i>
		/// * @param color the color to use
		/// * @see #setFillBackgroundColor(NPOI.xssf.usermodel.XSSFColor) )
		public void SetFillForegroundColor(XSSFColor color)
		{
			CT_Fill cTFill = GetCTFill();
			CT_PatternFill cT_PatternFill = cTFill.GetPatternFill();
			if (color == null)
			{
				if (cT_PatternFill != null)
				{
					cT_PatternFill.UnsetFgColor();
				}
			}
			else
			{
				if (cT_PatternFill == null)
				{
					cT_PatternFill = cTFill.AddNewPatternFill();
				}
				cT_PatternFill.fgColor = color.GetCTColor();
			}
			int fillId = _stylesSource.PutFill(new XSSFCellFill(cTFill));
			_cellXf.fillId = (uint)fillId;
			_cellXf.applyFill = true;
		}

		/// Get a <b>copy</b> of the currently used CT_Fill, if none is used, return a new instance.
		public CT_Fill GetCTFill()
		{
			if (_cellXf.applyFill)
			{
				int fillId = (int)_cellXf.fillId;
				XSSFCellFill fillAt = _stylesSource.GetFillAt(fillId);
				return fillAt.GetCTFill().Copy();
			}
			return new CT_Fill();
		}

		/// Get a <b>copy</b> of the currently used CT_Border, if none is used, return a new instance.
		public CT_Border GetCTBorder()
		{
			CT_Border cT_Border;
			if (_cellXf.applyBorder)
			{
				int borderId = (int)_cellXf.borderId;
				XSSFCellBorder borderAt = _stylesSource.GetBorderAt(borderId);
				cT_Border = borderAt.GetCTBorder();
			}
			else
			{
				cT_Border = new CT_Border();
				cT_Border.AddNewLeft();
				cT_Border.AddNewRight();
				cT_Border.AddNewTop();
				cT_Border.AddNewBottom();
				cT_Border.AddNewDiagonal();
			}
			return cT_Border;
		}

		/// Set the font for this style
		///
		/// @param font  a font object Created or retreived from the XSSFWorkbook object
		/// @see NPOI.xssf.usermodel.XSSFWorkbook#CreateFont()
		/// @see NPOI.xssf.usermodel.XSSFWorkbook#getFontAt(short)
		public void SetFont(IFont font)
		{
			if (font != null)
			{
				long num = font.Index;
				_cellXf.fontId = (uint)num;
				_cellXf.fontIdSpecified = true;
				_cellXf.applyFont = true;
			}
			else
			{
				_cellXf.applyFont = false;
			}
		}

		public void SetDiagonalBorderColor(XSSFColor color)
		{
			CT_Border cTBorder = GetCTBorder();
			if (color != null || cTBorder.IsSetDiagonal())
			{
				CT_BorderPr cT_BorderPr = cTBorder.IsSetDiagonal() ? cTBorder.diagonal : cTBorder.AddNewDiagonal();
				if (color != null)
				{
					cT_BorderPr.color = color.GetCTColor();
				}
				else
				{
					cT_BorderPr.UnsetColor();
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		/// Set the color to use for the left border as a {@link XSSFColor} value
		///
		/// @param color the color to use
		public void SetLeftBorderColor(XSSFColor color)
		{
			CT_Border cTBorder = GetCTBorder();
			if (color != null || cTBorder.IsSetLeft())
			{
				CT_BorderPr cT_BorderPr = cTBorder.IsSetLeft() ? cTBorder.left : cTBorder.AddNewLeft();
				if (color != null)
				{
					cT_BorderPr.color = color.GetCTColor();
				}
				else
				{
					cT_BorderPr.UnsetColor();
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		/// Set the color to use for the right border as a {@link XSSFColor} value
		///
		/// @param color the color to use
		public void SetRightBorderColor(XSSFColor color)
		{
			CT_Border cTBorder = GetCTBorder();
			if (color != null || cTBorder.IsSetRight())
			{
				CT_BorderPr cT_BorderPr = cTBorder.IsSetRight() ? cTBorder.right : cTBorder.AddNewRight();
				if (color != null)
				{
					cT_BorderPr.color = color.GetCTColor();
				}
				else
				{
					cT_BorderPr.UnsetColor();
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		/// Set the color to use for the top border as a {@link XSSFColor} value
		///
		/// @param color the color to use
		public void SetTopBorderColor(XSSFColor color)
		{
			CT_Border cTBorder = GetCTBorder();
			if (color != null || cTBorder.IsSetTop())
			{
				CT_BorderPr cT_BorderPr = cTBorder.IsSetTop() ? cTBorder.top : cTBorder.AddNewTop();
				if (color != null)
				{
					cT_BorderPr.color = color.GetCTColor();
				}
				else
				{
					cT_BorderPr.UnsetColor();
				}
				int borderId = _stylesSource.PutBorder(new XSSFCellBorder(cTBorder, _theme));
				_cellXf.borderId = (uint)borderId;
				_cellXf.applyBorder = true;
			}
		}

		/// Set the type of vertical alignment for the cell
		///
		/// @param align - align the type of alignment
		/// @see NPOI.ss.usermodel.CellStyle#VERTICAL_TOP
		/// @see NPOI.ss.usermodel.CellStyle#VERTICAL_CENTER
		/// @see NPOI.ss.usermodel.CellStyle#VERTICAL_BOTTOM
		/// @see NPOI.ss.usermodel.CellStyle#VERTICAL_JUSTIFY
		/// @see NPOI.ss.usermodel.VerticalAlignment
		public void SetVerticalAlignment(short align)
		{
			GetCellAlignment().Vertical = (VerticalAlignment)align;
		}

		/// Gets border color
		///
		/// @param side the border side
		/// @return the used color
		public XSSFColor GetBorderColor(BorderSide side)
		{
			switch (side)
			{
			case BorderSide.BOTTOM:
				return BottomBorderXSSFColor;
			case BorderSide.RIGHT:
				return RightBorderXSSFColor;
			case BorderSide.TOP:
				return TopBorderXSSFColor;
			case BorderSide.LEFT:
				return LeftBorderXSSFColor;
			default:
				throw new ArgumentException("Unknown border: " + side);
			}
		}

		/// Set the color to use for the selected border
		///
		/// @param side - where to apply the color defInition
		/// @param color - the color to use
		public void SetBorderColor(BorderSide side, XSSFColor color)
		{
			switch (side)
			{
			case BorderSide.BOTTOM:
				SetBottomBorderColor(color);
				break;
			case BorderSide.RIGHT:
				SetRightBorderColor(color);
				break;
			case BorderSide.TOP:
				SetTopBorderColor(color);
				break;
			case BorderSide.LEFT:
				SetLeftBorderColor(color);
				break;
			}
		}

		/// Get the cellAlignment object to use for manage alignment
		/// @return XSSFCellAlignment - cell alignment
		internal XSSFCellAlignment GetCellAlignment()
		{
			if (_cellAlignment == null)
			{
				_cellAlignment = new XSSFCellAlignment(GetCTCellAlignment());
			}
			return _cellAlignment;
		}

		/// Return the CT_CellAlignment instance for alignment
		///
		/// @return CT_CellAlignment
		internal CT_CellAlignment GetCTCellAlignment()
		{
			if (_cellXf.alignment == null)
			{
				_cellXf.alignment = new CT_CellAlignment();
			}
			return _cellXf.alignment;
		}

		/// Returns a hash code value for the object. The hash is derived from the underlying CT_Xf bean.
		///
		/// @return the hash code value for this style
		public override int GetHashCode()
		{
			return _cellXf.ToString().GetHashCode();
		}

		/// Checks is the supplied style is equal to this style
		///
		/// @param o the style to check
		/// @return true if the supplied style is equal to this style
		public override bool Equals(object o)
		{
			if (o == null || !(o is XSSFCellStyle))
			{
				return false;
			}
			XSSFCellStyle xSSFCellStyle = (XSSFCellStyle)o;
			return _cellXf.ToString().Equals(xSSFCellStyle.GetCoreXf().ToString());
		}

		/// Make a copy of this style. The underlying CT_Xf bean is Cloned,
		/// the references to Fills and borders remain.
		///
		/// @return a copy of this style
		public object Clone()
		{
			CT_Xf cellXf = _cellXf.Copy();
			int styleXfsSize = _stylesSource.StyleXfsSize;
			int num = _stylesSource.PutCellXf(cellXf);
			return new XSSFCellStyle(num - 1, styleXfsSize - 1, _stylesSource, _theme);
		}

		public IFont GetFont(IWorkbook parentWorkbook)
		{
			return GetFont();
		}
	}
}
