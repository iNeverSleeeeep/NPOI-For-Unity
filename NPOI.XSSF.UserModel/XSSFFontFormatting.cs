using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel
{
	/// @author Yegor Kozlov
	public class XSSFFontFormatting : IFontFormatting
	{
		private CT_Font _font;

		/// Get the type of super or subscript for the font
		///
		/// @return super or subscript option
		/// @see #SS_NONE
		/// @see #SS_SUPER
		/// @see #SS_SUB
		public FontSuperScript EscapementType
		{
			get
			{
				if (_font.sizeOfVertAlignArray() == 0)
				{
					return FontSuperScript.None;
				}
				CT_VerticalAlignFontProperty vertAlignArray = _font.GetVertAlignArray(0);
				return (FontSuperScript)(vertAlignArray.val - 1);
			}
			set
			{
				_font.SetVertAlignArray(null);
				if (value != 0)
				{
					_font.AddNewVertAlign().val = (ST_VerticalAlignRun)(value + 1);
				}
			}
		}

		/// @return font color index
		public short FontColorIndex
		{
			get
			{
				if (_font.sizeOfColorArray() == 0)
				{
					return -1;
				}
				int num = 0;
				CT_Color colorArray = _font.GetColorArray(0);
				if (colorArray.IsSetIndexed())
				{
					num = (int)colorArray.indexed;
				}
				return (short)num;
			}
			set
			{
				_font.SetColorArray(null);
				if (value != -1)
				{
					CT_Color cT_Color = _font.AddNewColor();
					cT_Color.indexed = (uint)value;
					cT_Color.indexedSpecified = true;
				}
			}
		}

		/// Gets the height of the font in 1/20th point units
		///
		/// @return fontheight (in points/20); or -1 if not modified
		public int FontHeight
		{
			get
			{
				if (_font.sizeOfSzArray() == 0)
				{
					return -1;
				}
				CT_FontSize szArray = _font.GetSzArray(0);
				return (short)(20.0 * szArray.val);
			}
			set
			{
				_font.SetSzArray(null);
				if (value != -1)
				{
					_font.AddNewSz().val = (double)value / 20.0;
				}
			}
		}

		/// Get the type of underlining for the font
		///
		/// @return font underlining type
		///
		/// @see #U_NONE
		/// @see #U_SINGLE
		/// @see #U_DOUBLE
		/// @see #U_SINGLE_ACCOUNTING
		/// @see #U_DOUBLE_ACCOUNTING
		public FontUnderlineType UnderlineType
		{
			get
			{
				if (_font.sizeOfUArray() != 0)
				{
					CT_UnderlineProperty uArray = _font.GetUArray(0);
					switch (uArray.val)
					{
					case ST_UnderlineValues.single:
						return FontUnderlineType.Single;
					case ST_UnderlineValues.@double:
						return FontUnderlineType.Double;
					case ST_UnderlineValues.singleAccounting:
						return FontUnderlineType.SingleAccounting;
					case ST_UnderlineValues.doubleAccounting:
						return FontUnderlineType.DoubleAccounting;
					default:
						return FontUnderlineType.None;
					}
				}
				return FontUnderlineType.None;
			}
			set
			{
				_font.SetUArray(null);
				if (value != 0)
				{
					FontUnderline fontUnderline = FontUnderline.ValueOf(value);
					ST_UnderlineValues value2 = (ST_UnderlineValues)fontUnderline.Value;
					_font.AddNewU().val = value2;
				}
			}
		}

		/// Get whether the font weight is Set to bold or not
		///
		/// @return bold - whether the font is bold or not
		public bool IsBold
		{
			get
			{
				if (_font.sizeOfBArray() == 1)
				{
					return _font.GetBArray(0).val;
				}
				return false;
			}
		}

		/// @return true if font style was Set to <i>italic</i>
		public bool IsItalic
		{
			get
			{
				if (_font.sizeOfIArray() == 1)
				{
					return _font.GetIArray(0).val;
				}
				return false;
			}
		}

		internal XSSFFontFormatting(CT_Font font)
		{
			_font = font;
		}

		/// @return xssf color wrapper or null if color info is missing
		public XSSFColor GetXSSFColor()
		{
			if (_font.sizeOfColorArray() == 0)
			{
				return null;
			}
			return new XSSFColor(_font.GetColorArray(0));
		}

		/// Set font style options.
		///
		/// @param italic - if true, Set posture style to italic, otherwise to normal
		/// @param bold if true, Set font weight to bold, otherwise to normal
		public void SetFontStyle(bool italic, bool bold)
		{
			_font.SetIArray(null);
			_font.SetBArray(null);
			if (italic)
			{
				_font.AddNewI().val = true;
			}
			if (bold)
			{
				_font.AddNewB().val = true;
			}
		}

		/// Set font style options to default values (non-italic, non-bold)
		public void ResetFontStyle()
		{
			_font = new CT_Font();
		}
	}
}
