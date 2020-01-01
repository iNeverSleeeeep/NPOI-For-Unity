using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;

namespace NPOI.XSSF.UserModel
{
	/// Represents a font used in a workbook.
	///
	/// @author Gisella Bronzetti
	public class XSSFFont : IFont
	{
		/// By default, Microsoft Office Excel 2007 uses the Calibry font in font size 11
		public const string DEFAULT_FONT_NAME = "Calibri";

		/// By default, Microsoft Office Excel 2007 uses the Calibry font in font size 11
		public const short DEFAULT_FONT_SIZE = 11;

		/// Default font color is black
		/// @see NPOI.SS.usermodel.IndexedColors#BLACK
		public static short DEFAULT_FONT_COLOR = IndexedColors.Black.Index;

		private ThemesTable _themes;

		private CT_Font _ctFont;

		private short _index;

		/// get a bool value for the boldness to use.
		///
		/// @return bool - bold
		public bool IsBold
		{
			get
			{
				CT_BooleanProperty cT_BooleanProperty = (_ctFont.sizeOfBArray() == 0) ? null : _ctFont.GetBArray(0);
				if (cT_BooleanProperty != null)
				{
					return cT_BooleanProperty.val;
				}
				return false;
			}
			set
			{
				if (value)
				{
					CT_BooleanProperty cT_BooleanProperty = (_ctFont.sizeOfBArray() == 0) ? _ctFont.AddNewB() : _ctFont.GetBArray(0);
					cT_BooleanProperty.val = value;
				}
				else
				{
					_ctFont.SetBArray(null);
				}
			}
		}

		/// get character-set to use.
		///
		/// @return int - character-set (0-255)
		/// @see NPOI.SS.usermodel.FontCharset
		public short Charset
		{
			get
			{
				CT_IntProperty cT_IntProperty = (_ctFont.sizeOfCharsetArray() == 0) ? null : _ctFont.GetCharsetArray(0);
				int num = (cT_IntProperty == null) ? FontCharset.ANSI.Value : FontCharset.ValueOf(cT_IntProperty.val).Value;
				return (short)num;
			}
			set
			{
			}
		}

		/// get the indexed color value for the font
		/// References a color defined in IndexedColors.
		///
		/// @return short - indexed color to use
		/// @see IndexedColors
		public short Color
		{
			get
			{
				CT_Color cT_Color = (_ctFont.sizeOfColorArray() == 0) ? null : _ctFont.GetColorArray(0);
				if (cT_Color == null)
				{
					return IndexedColors.Black.Index;
				}
				if (!cT_Color.indexedSpecified)
				{
					return IndexedColors.Black.Index;
				}
				long num = cT_Color.indexed;
				if (num == DEFAULT_FONT_COLOR)
				{
					return IndexedColors.Black.Index;
				}
				if (num == IndexedColors.Red.Index)
				{
					return IndexedColors.Red.Index;
				}
				return (short)num;
			}
			set
			{
				CT_Color cT_Color = (_ctFont.sizeOfColorArray() == 0) ? _ctFont.AddNewColor() : _ctFont.GetColorArray(0);
				switch (value)
				{
				case short.MaxValue:
					cT_Color.indexed = (uint)DEFAULT_FONT_COLOR;
					cT_Color.indexedSpecified = true;
					break;
				case 10:
					cT_Color.indexed = (uint)IndexedColors.Red.Index;
					cT_Color.indexedSpecified = true;
					break;
				default:
					cT_Color.indexed = (uint)value;
					cT_Color.indexedSpecified = true;
					break;
				}
			}
		}

		/// get the font height in point.
		///
		/// @return short - height in point
		public double FontHeight
		{
			get
			{
				CT_FontSize cT_FontSize = (_ctFont.sizeOfSzArray() == 0) ? null : _ctFont.GetSzArray(0);
				if (cT_FontSize != null)
				{
					double val = cT_FontSize.val;
					return (double)(short)(val * 20.0);
				}
				return 220.0;
			}
			set
			{
				CT_FontSize cT_FontSize = (_ctFont.sizeOfSzArray() == 0) ? _ctFont.AddNewSz() : _ctFont.GetSzArray(0);
				cT_FontSize.val = value;
			}
		}

		/// @see #GetFontHeight()
		public short FontHeightInPoints
		{
			get
			{
				return (short)(FontHeight / 20.0);
			}
			set
			{
				CT_FontSize cT_FontSize = (_ctFont.sizeOfSzArray() == 0) ? _ctFont.AddNewSz() : _ctFont.GetSzArray(0);
				cT_FontSize.val = (double)value;
			}
		}

		/// get the name of the font (i.e. Arial)
		///
		/// @return String - a string representing the name of the font to use
		public string FontName
		{
			get
			{
				CT_FontName name = _ctFont.name;
				if (name != null)
				{
					return name.val;
				}
				return "Calibri";
			}
			set
			{
				CT_FontName cT_FontName = (_ctFont.name == null) ? _ctFont.AddNewName() : _ctFont.name;
				cT_FontName.val = ((value == null) ? "Calibri" : value);
			}
		}

		/// get a bool value that specify whether to use italics or not
		///
		/// @return bool - value for italic
		public bool IsItalic
		{
			get
			{
				CT_BooleanProperty cT_BooleanProperty = (_ctFont.sizeOfIArray() == 0) ? null : _ctFont.GetIArray(0);
				if (cT_BooleanProperty != null)
				{
					return cT_BooleanProperty.val;
				}
				return false;
			}
			set
			{
				if (value)
				{
					CT_BooleanProperty cT_BooleanProperty = (_ctFont.sizeOfIArray() == 0) ? _ctFont.AddNewI() : _ctFont.GetIArray(0);
					cT_BooleanProperty.val = value;
				}
				else
				{
					_ctFont.SetIArray(null);
				}
			}
		}

		/// get a bool value that specify whether to use a strikeout horizontal line through the text or not
		///
		/// @return bool - value for strikeout
		public bool IsStrikeout
		{
			get
			{
				CT_BooleanProperty cT_BooleanProperty = (_ctFont.sizeOfStrikeArray() == 0) ? null : _ctFont.GetStrikeArray(0);
				if (cT_BooleanProperty != null)
				{
					return cT_BooleanProperty.val;
				}
				return false;
			}
			set
			{
				if (!value)
				{
					_ctFont.SetStrikeArray(null);
				}
				else
				{
					CT_BooleanProperty cT_BooleanProperty = (_ctFont.sizeOfStrikeArray() == 0) ? _ctFont.AddNewStrike() : _ctFont.GetStrikeArray(0);
					cT_BooleanProperty.val = value;
				}
			}
		}

		/// get normal,super or subscript.
		///
		/// @return short - offset type to use (none,super,sub)
		/// @see Font#SS_NONE
		/// @see Font#SS_SUPER
		/// @see Font#SS_SUB
		public FontSuperScript TypeOffset
		{
			get
			{
				CT_VerticalAlignFontProperty cT_VerticalAlignFontProperty = (_ctFont.sizeOfVertAlignArray() == 0) ? null : _ctFont.GetVertAlignArray(0);
				if (cT_VerticalAlignFontProperty != null)
				{
					ST_VerticalAlignRun val = cT_VerticalAlignFontProperty.val;
					switch (val)
					{
					case ST_VerticalAlignRun.baseline:
						return FontSuperScript.None;
					case ST_VerticalAlignRun.subscript:
						return FontSuperScript.Sub;
					case ST_VerticalAlignRun.superscript:
						return FontSuperScript.Super;
					default:
						throw new POIXMLException("Wrong offset value " + val);
					}
				}
				return FontSuperScript.None;
			}
			set
			{
				if (value == FontSuperScript.None)
				{
					_ctFont.SetVertAlignArray(null);
				}
				else
				{
					CT_VerticalAlignFontProperty cT_VerticalAlignFontProperty = (_ctFont.sizeOfVertAlignArray() == 0) ? _ctFont.AddNewVertAlign() : _ctFont.GetVertAlignArray(0);
					switch (value)
					{
					case FontSuperScript.None:
						cT_VerticalAlignFontProperty.val = ST_VerticalAlignRun.baseline;
						break;
					case FontSuperScript.Sub:
						cT_VerticalAlignFontProperty.val = ST_VerticalAlignRun.subscript;
						break;
					case FontSuperScript.Super:
						cT_VerticalAlignFontProperty.val = ST_VerticalAlignRun.superscript;
						break;
					}
				}
			}
		}

		/// get type of text underlining to use
		///
		/// @return byte - underlining type
		/// @see NPOI.SS.usermodel.FontUnderline
		public FontUnderlineType Underline
		{
			get
			{
				CT_UnderlineProperty cT_UnderlineProperty = (_ctFont.sizeOfUArray() == 0) ? null : _ctFont.GetUArray(0);
				if (cT_UnderlineProperty != null)
				{
					FontUnderline fontUnderline = FontUnderline.ValueOf((int)cT_UnderlineProperty.val);
					return (FontUnderlineType)fontUnderline.ByteValue;
				}
				return (FontUnderlineType)FontUnderline.NONE.ByteValue;
			}
			set
			{
				SetUnderline(value);
			}
		}

		/// get the boldness to use
		/// @return boldweight
		/// @see #BOLDWEIGHT_NORMAL
		/// @see #BOLDWEIGHT_BOLD
		public short Boldweight
		{
			get
			{
				if (!IsBold)
				{
					return 400;
				}
				return 700;
			}
			set
			{
				IsBold = (value == 700);
			}
		}

		/// get the font family to use.
		///
		/// @return the font family to use
		/// @see NPOI.SS.usermodel.FontFamily
		public int Family
		{
			get
			{
				CT_IntProperty cT_IntProperty = (_ctFont.sizeOfFamilyArray() == 0) ? _ctFont.AddNewFamily() : _ctFont.GetFamilyArray(0);
				if (cT_IntProperty != null)
				{
					return FontFamily.ValueOf(cT_IntProperty.val).Value;
				}
				return FontFamily.NOT_APPLICABLE.Value;
			}
			set
			{
				CT_IntProperty cT_IntProperty = (_ctFont.sizeOfFamilyArray() == 0) ? _ctFont.AddNewFamily() : _ctFont.GetFamilyArray(0);
				cT_IntProperty.val = value;
			}
		}

		/// get the index within the XSSFWorkbook (sequence within the collection of Font objects)
		/// @return unique index number of the underlying record this Font represents (probably you don't care
		///  unless you're comparing which one is which)
		public short Index
		{
			get
			{
				return _index;
			}
		}

		/// Create a new XSSFFont
		///
		/// @param font the underlying CT_Font bean
		public XSSFFont(CT_Font font)
		{
			_ctFont = font;
			_index = 0;
		}

		public XSSFFont(CT_Font font, int index)
		{
			_ctFont = font;
			_index = (short)index;
		}

		/// Create a new XSSFont. This method is protected to be used only by XSSFWorkbook
		public XSSFFont()
		{
			_ctFont = new CT_Font();
			FontName = "Calibri";
			FontHeight = 11.0;
		}

		/// get the underlying CT_Font font
		public CT_Font GetCTFont()
		{
			return _ctFont;
		}

		/// get the color value for the font
		/// References a color defined as  Standard Alpha Red Green Blue color value (ARGB).
		///
		/// @return XSSFColor - rgb color to use
		public XSSFColor GetXSSFColor()
		{
			CT_Color cT_Color = (_ctFont.sizeOfColorArray() == 0) ? null : _ctFont.GetColorArray(0);
			if (cT_Color != null)
			{
				XSSFColor xSSFColor = new XSSFColor(cT_Color);
				if (_themes != null)
				{
					_themes.InheritFromThemeAsRequired(xSSFColor);
				}
				return xSSFColor;
			}
			return null;
		}

		/// get the color value for the font
		/// References a color defined in theme.
		///
		/// @return short - theme defined to use
		public short GetThemeColor()
		{
			CT_Color cT_Color = (_ctFont.sizeOfColorArray() == 0) ? null : _ctFont.GetColorArray(0);
			long num = (cT_Color != null && cT_Color.themeSpecified) ? cT_Color.theme : 0;
			return (short)num;
		}

		/// set character-set to use.
		///
		/// @param charset - charset
		/// @see FontCharset
		public void SetCharSet(byte charSet)
		{
			int num = charSet;
			if (num < 0)
			{
				num += 256;
			}
			SetCharSet(num);
		}

		/// set character-set to use.
		///
		/// @param charset - charset
		/// @see FontCharset
		public void SetCharSet(int charset)
		{
			FontCharset fontCharset = FontCharset.ValueOf(charset);
			if (fontCharset != null)
			{
				SetCharSet(fontCharset);
				return;
			}
			throw new POIXMLException("Attention: an attempt to set a type of unknow charset and charSet");
		}

		/// set character-set to use.
		///
		/// @param charSet
		public void SetCharSet(FontCharset charset)
		{
			CT_IntProperty cT_IntProperty = (_ctFont.sizeOfCharsetArray() != 0) ? _ctFont.GetCharsetArray(0) : _ctFont.AddNewCharset();
			cT_IntProperty.val = charset.Value;
		}

		/// set the color for the font in Standard Alpha Red Green Blue color value
		///
		/// @param color - color to use
		public void SetColor(XSSFColor color)
		{
			if (color == null)
			{
				_ctFont.SetColorArray(null);
			}
			else
			{
				CT_Color cT_Color = (_ctFont.sizeOfColorArray() == 0) ? _ctFont.AddNewColor() : _ctFont.GetColorArray(0);
				cT_Color.SetRgb(color.RGB);
			}
		}

		/// set the theme color for the font to use
		///
		/// @param theme - theme color to use
		public void SetThemeColor(short theme)
		{
			CT_Color cT_Color = (_ctFont.sizeOfColorArray() == 0) ? _ctFont.AddNewColor() : _ctFont.GetColorArray(0);
			cT_Color.theme = (uint)theme;
		}

		/// set an enumeration representing the style of underlining that is used.
		/// The none style is equivalent to not using underlining at all.
		/// The possible values for this attribute are defined by the FontUnderline
		///
		/// @param underline - FontUnderline enum value
		internal void SetUnderline(FontUnderlineType underline)
		{
			if (underline == FontUnderlineType.None && _ctFont.sizeOfUArray() > 0)
			{
				_ctFont.SetUArray(null);
			}
			else
			{
				CT_UnderlineProperty cT_UnderlineProperty = (_ctFont.sizeOfUArray() == 0) ? _ctFont.AddNewU() : _ctFont.GetUArray(0);
				ST_UnderlineValues sT_UnderlineValues = cT_UnderlineProperty.val = (ST_UnderlineValues)FontUnderline.ValueOf(underline).Value;
			}
		}

		public override string ToString()
		{
			return _ctFont.ToString();
		}

		/// **
		public long RegisterTo(StylesTable styles)
		{
			_themes = styles.GetTheme();
			return _index = (short)styles.PutFont(this, true);
		}

		/// Records the Themes Table that is associated with
		///  the current font, used when looking up theme
		///  based colours and properties.
		public void SetThemesTable(ThemesTable themes)
		{
			_themes = themes;
		}

		/// get the font scheme property.
		/// is used only in StylesTable to create the default instance of font
		///
		/// @return FontScheme
		/// @see NPOI.XSSF.model.StylesTable#CreateDefaultFont()
		public FontScheme GetScheme()
		{
			CT_FontScheme cT_FontScheme = (_ctFont.sizeOfSchemeArray() == 0) ? null : _ctFont.GetSchemeArray(0);
			if (cT_FontScheme != null)
			{
				return FontScheme.ValueOf((int)cT_FontScheme.val);
			}
			return FontScheme.NONE;
		}

		/// set font scheme property
		///
		/// @param scheme - FontScheme enum value
		/// @see FontScheme
		public void SetScheme(FontScheme scheme)
		{
			CT_FontScheme cT_FontScheme = (_ctFont.sizeOfSchemeArray() == 0) ? _ctFont.AddNewScheme() : _ctFont.GetSchemeArray(0);
			ST_FontScheme sT_FontScheme = cT_FontScheme.val = (ST_FontScheme)scheme.Value;
		}

		/// set an enumeration representing the font family this font belongs to.
		/// A font family is a set of fonts having common stroke width and serif characteristics.
		///
		/// @param family font family
		/// @link #SetFamily(int value)
		public void SetFamily(FontFamily family)
		{
			Family = family.Value;
		}

		public override int GetHashCode()
		{
			return _ctFont.ToString().GetHashCode();
		}

		public override bool Equals(object o)
		{
			if (!(o is XSSFFont))
			{
				return false;
			}
			XSSFFont xSSFFont = (XSSFFont)o;
			return _ctFont.ToString().Equals(xSSFFont.GetCTFont().ToString());
		}
	}
}
