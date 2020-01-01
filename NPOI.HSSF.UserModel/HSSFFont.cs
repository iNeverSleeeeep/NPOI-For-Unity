using NPOI.HSSF.Record;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Represents a Font used in a workbook.
	/// @version 1.0-pre
	/// @author  Andrew C. Oliver
	/// </summary>
	public class HSSFFont : IFont
	{
		public const string FONT_ARIAL = "Arial";

		private FontRecord font;

		private short index;

		/// <summary>
		/// Get the name for the font (i.e. Arial)
		/// </summary>
		/// <value>the name of the font to use</value>
		public string FontName
		{
			get
			{
				return font.FontName;
			}
			set
			{
				font.FontName = value;
			}
		}

		/// <summary>
		/// Get the index within the HSSFWorkbook (sequence within the collection of Font objects)
		/// </summary>
		/// <value>Unique index number of the Underlying record this Font represents (probably you don't care
		/// Unless you're comparing which one is which)</value>
		public short Index => index;

		/// <summary>
		/// Get or sets the font height in Unit's of 1/20th of a point.  Maybe you might want to
		/// use the GetFontHeightInPoints which matches to the familiar 10, 12, 14 etc..
		/// </summary>
		/// <value>height in 1/20ths of a point.</value>
		public double FontHeight
		{
			get
			{
				return (double)font.FontHeight;
			}
			set
			{
				font.FontHeight = (short)value;
			}
		}

		/// <summary>
		/// Gets or sets the font height in points.
		/// </summary>
		/// <value>height in the familiar Unit of measure - points.</value>
		public short FontHeightInPoints
		{
			get
			{
				return (short)(font.FontHeight / 20);
			}
			set
			{
				font.FontHeight = (short)(value * 20);
			}
		}

		/// <summary>
		/// Gets or sets whether to use italics or not
		/// </summary>
		/// <value><c>true</c> if this instance is italic; otherwise, <c>false</c>.</value>
		public bool IsItalic
		{
			get
			{
				return font.IsItalic;
			}
			set
			{
				font.IsItalic = value;
			}
		}

		/// <summary>
		/// Get whether to use a strikeout horizontal line through the text or not
		/// </summary>
		/// <value>
		/// strikeout or not
		/// </value>
		public bool IsStrikeout
		{
			get
			{
				return font.IsStrikeout;
			}
			set
			{
				font.IsStrikeout = value;
			}
		}

		/// <summary>
		/// Gets or sets the color for the font.
		/// </summary>
		/// <value>The color to use.</value>
		public short Color
		{
			get
			{
				return font.ColorPaletteIndex;
			}
			set
			{
				font.ColorPaletteIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets the boldness to use
		/// </summary>
		/// <value>The boldweight.</value>
		public short Boldweight
		{
			get
			{
				return font.BoldWeight;
			}
			set
			{
				font.BoldWeight = value;
			}
		}

		/// <summary>
		/// Gets or sets normal,base or subscript.
		/// </summary>
		/// <value>offset type to use (none,base,sub)</value>
		public FontSuperScript TypeOffset
		{
			get
			{
				return font.SuperSubScript;
			}
			set
			{
				font.SuperSubScript = value;
			}
		}

		/// <summary>
		/// Gets or sets the type of text Underlining to use
		/// </summary>
		/// <value>The Underlining type.</value>
		public FontUnderlineType Underline
		{
			get
			{
				return font.Underline;
			}
			set
			{
				font.Underline = value;
			}
		}

		/// <summary>
		/// Gets or sets the char set to use.
		/// </summary>
		/// <value>The char set.</value>
		public short Charset
		{
			get
			{
				return font.Charset;
			}
			set
			{
				font.Charset = (byte)value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFFont" /> class.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="rec">The record.</param>
		public HSSFFont(short index, FontRecord rec)
		{
			font = rec;
			this.index = index;
		}

		/// <summary>
		/// get the color value for the font
		/// </summary>
		/// <param name="wb">HSSFWorkbook</param>
		/// <returns></returns>
		public HSSFColor GetHSSFColor(HSSFWorkbook wb)
		{
			HSSFPalette customPalette = wb.GetCustomPalette();
			return customPalette.GetColor(Color);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			return "NPOI.HSSF.UserModel.HSSFFont{" + font + "}";
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		public override int GetHashCode()
		{
			int num = 31;
			int num2 = 1;
			num2 = num * num2 + ((font != null) ? font.GetHashCode() : 0);
			return num * num2 + index;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">
		/// The <paramref name="obj" /> parameter is null.
		/// </exception>
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (obj is HSSFFont)
			{
				HSSFFont hSSFFont = (HSSFFont)obj;
				if (font == null)
				{
					if (hSSFFont.font != null)
					{
						return false;
					}
				}
				else if (!font.Equals(hSSFFont.font))
				{
					return false;
				}
				if (index != hSSFFont.index)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
