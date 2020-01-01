using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.XSSF.UserModel
{
	/// Rich text unicode string.  These strings can have fonts applied to arbitary parts of the string.
	///
	/// <p>
	/// Most strings in a workbook have formatting applied at the cell level, that is, the entire string in the cell has the
	/// same formatting applied. In these cases, the formatting for the cell is stored in the styles part,
	/// and the string for the cell can be shared across the workbook. The following code illustrates the example.
	/// </p>
	///
	/// <blockquote>
	/// <pre>
	///     cell1.SetCellValue(new XSSFRichTextString("Apache POI"));
	///     cell2.SetCellValue(new XSSFRichTextString("Apache POI"));
	///     cell3.SetCellValue(new XSSFRichTextString("Apache POI"));
	/// </pre>
	/// </blockquote>
	/// In the above example all three cells will use the same string cached on workbook level.
	///
	/// <p>
	/// Some strings in the workbook may have formatting applied at a level that is more granular than the cell level.
	/// For instance, specific characters within the string may be bolded, have coloring, italicizing, etc.
	/// In these cases, the formatting is stored along with the text in the string table, and is treated as
	/// a unique entry in the workbook. The following xml and code snippet illustrate this.
	/// </p>
	///
	/// <blockquote>
	/// <pre>
	///     XSSFRichTextString s1 = new XSSFRichTextString("Apache POI");
	///     s1.ApplyFont(boldArial);
	///     cell1.SetCellValue(s1);
	///
	///     XSSFRichTextString s2 = new XSSFRichTextString("Apache POI");
	///     s2.ApplyFont(italicCourier);
	///     cell2.SetCellValue(s2);
	/// </pre>
	/// </blockquote>
	///
	///
	/// @author Yegor Kozlov
	public class XSSFRichTextString : IRichTextString
	{
		private static Regex utfPtrn = new Regex("_x([0-9A-F]{4})_");

		private CT_Rst st;

		private StylesTable styles;

		public string String
		{
			get
			{
				if (st.sizeOfRArray() == 0)
				{
					return UtfDecode(st.t);
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (CT_RElt item in st.r)
				{
					stringBuilder.Append(item.t);
				}
				return UtfDecode(stringBuilder.ToString());
			}
			set
			{
				ClearFormatting();
				st.t = value;
				PreserveSpaces(st.t);
			}
		}

		/// Returns the number of characters in this string.
		public int Length
		{
			get
			{
				return String.Length;
			}
		}

		/// @return  The number of formatting Runs used.
		public int NumFormattingRuns
		{
			get
			{
				return st.sizeOfRArray();
			}
		}

		/// Create a rich text string
		public XSSFRichTextString(string str)
		{
			st = new CT_Rst();
			st.t = str;
			PreserveSpaces(st.t);
		}

		public void SetStylesTableReference(StylesTable stylestable)
		{
			styles = stylestable;
			if (st.sizeOfRArray() > 0)
			{
				foreach (CT_RElt item in st.r)
				{
					CT_RPrElt rPr = item.rPr;
					if (rPr != null && rPr.sizeOfRFontArray() > 0)
					{
						string val = rPr.GetRFontArray(0).val;
						if (val.StartsWith("#"))
						{
							int idx = int.Parse(val.Substring(1));
							XSSFFont fontAt = styles.GetFontAt(idx);
							rPr.rFont = null;
							SetRunAttributes(fontAt.GetCTFont(), rPr);
						}
					}
				}
			}
		}

		/// Create empty rich text string and Initialize it with empty string
		public XSSFRichTextString()
		{
			st = new CT_Rst();
		}

		/// Create a rich text string from the supplied XML bean
		public XSSFRichTextString(CT_Rst st)
		{
			this.st = st;
		}

		/// Applies a font to the specified characters of a string.
		///
		/// @param startIndex    The start index to apply the font to (inclusive)
		/// @param endIndex      The end index to apply the font to (exclusive)
		/// @param fontIndex     The font to use.
		public void ApplyFont(int startIndex, int endIndex, short fontIndex)
		{
			XSSFFont xSSFFont;
			if (styles == null)
			{
				xSSFFont = new XSSFFont();
				xSSFFont.FontName = "#" + fontIndex;
			}
			else
			{
				xSSFFont = styles.GetFontAt(fontIndex);
			}
			ApplyFont(startIndex, endIndex, xSSFFont);
		}

		internal void ApplyFont(SortedDictionary<int, CT_RPrElt> formats, int startIndex, int endIndex, CT_RPrElt fmt)
		{
			List<int> list = new List<int>();
			SortedDictionary<int, CT_RPrElt>.KeyCollection.Enumerator enumerator = formats.Keys.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int current = enumerator.Current;
				if (current >= startIndex && current < endIndex)
				{
					list.Add(current);
				}
			}
			foreach (int item in list)
			{
				formats.Remove(item);
			}
			if (startIndex > 0 && !formats.ContainsKey(startIndex))
			{
				foreach (KeyValuePair<int, CT_RPrElt> format in formats)
				{
					if (format.Key > startIndex)
					{
						formats[startIndex] = format.Value;
						break;
					}
				}
			}
			formats[endIndex] = fmt;
		}

		/// Applies a font to the specified characters of a string.
		///
		/// @param startIndex    The start index to apply the font to (inclusive)
		/// @param endIndex      The end index to apply to font to (exclusive)
		/// @param font          The index of the font to use.
		public void ApplyFont(int startIndex, int endIndex, IFont font)
		{
			if (startIndex > endIndex)
			{
				throw new ArgumentException("Start index must be less than end index.");
			}
			if (startIndex < 0 || endIndex > Length)
			{
				throw new ArgumentException("Start and end index not in range.");
			}
			if (startIndex != endIndex)
			{
				if (st.sizeOfRArray() == 0 && st.IsSetT())
				{
					st.AddNewR().t = st.t;
					st.unsetT();
				}
				string @string = String;
				XSSFFont xSSFFont = (XSSFFont)font;
				SortedDictionary<int, CT_RPrElt> formatMap = GetFormatMap(st);
				CT_RPrElt cT_RPrElt = new CT_RPrElt();
				SetRunAttributes(xSSFFont.GetCTFont(), cT_RPrElt);
				ApplyFont(formatMap, startIndex, endIndex, cT_RPrElt);
				CT_Rst o = buildCTRst(@string, formatMap);
				st.Set(o);
			}
		}

		internal SortedDictionary<int, CT_RPrElt> GetFormatMap(CT_Rst entry)
		{
			int num = 0;
			SortedDictionary<int, CT_RPrElt> sortedDictionary = new SortedDictionary<int, CT_RPrElt>();
			foreach (CT_RElt item in entry.r)
			{
				string t = item.t;
				CT_RPrElt rPr = item.rPr;
				num += t.Length;
				sortedDictionary[num] = rPr;
			}
			return sortedDictionary;
		}

		/// Sets the font of the entire string.
		/// @param font          The font to use.
		public void ApplyFont(IFont font)
		{
			string @string = String;
			ApplyFont(0, @string.Length, font);
		}

		/// Applies the specified font to the entire string.
		///
		/// @param fontIndex  the font to Apply.
		public void ApplyFont(short fontIndex)
		{
			XSSFFont xSSFFont;
			if (styles == null)
			{
				xSSFFont = new XSSFFont();
				xSSFFont.FontName = "#" + fontIndex;
			}
			else
			{
				xSSFFont = styles.GetFontAt(fontIndex);
			}
			string @string = String;
			ApplyFont(0, @string.Length, xSSFFont);
		}

		/// Append new text to this text run and apply the specify font to it
		///
		/// @param text  the text to append
		/// @param font  the font to apply to the Appended text or <code>null</code> if no formatting is required
		public void Append(string text, XSSFFont font)
		{
			if (st.sizeOfRArray() == 0 && st.IsSetT())
			{
				CT_RElt cT_RElt = st.AddNewR();
				cT_RElt.t = st.t;
				PreserveSpaces(cT_RElt.t);
				st.unsetT();
			}
			CT_RElt cT_RElt2 = st.AddNewR();
			cT_RElt2.t = text;
			PreserveSpaces(cT_RElt2.t);
			CT_RPrElt pr = cT_RElt2.AddNewRPr();
			if (font != null)
			{
				SetRunAttributes(font.GetCTFont(), pr);
			}
		}

		/// Append new text to this text run
		///
		/// @param text  the text to append
		public void Append(string text)
		{
			Append(text, null);
		}

		/// Copy font attributes from CTFont bean into CTRPrElt bean
		private void SetRunAttributes(CT_Font ctFont, CT_RPrElt pr)
		{
			if (ctFont.sizeOfBArray() > 0)
			{
				pr.AddNewB().val = ctFont.GetBArray(0).val;
			}
			if (ctFont.sizeOfUArray() > 0)
			{
				pr.AddNewU().val = ctFont.GetUArray(0).val;
			}
			if (ctFont.sizeOfIArray() > 0)
			{
				pr.AddNewI().val = ctFont.GetIArray(0).val;
			}
			if (ctFont.sizeOfColorArray() > 0)
			{
				CT_Color colorArray = ctFont.GetColorArray(0);
				CT_Color cT_Color = pr.AddNewColor();
				if (colorArray.IsSetAuto())
				{
					cT_Color.auto = colorArray.auto;
					cT_Color.autoSpecified = true;
				}
				if (colorArray.IsSetIndexed())
				{
					cT_Color.indexed = colorArray.indexed;
					cT_Color.indexedSpecified = true;
				}
				if (colorArray.IsSetRgb())
				{
					cT_Color.SetRgb(colorArray.rgb);
					cT_Color.rgbSpecified = true;
				}
				if (colorArray.IsSetTheme())
				{
					cT_Color.theme = colorArray.theme;
					cT_Color.themeSpecified = true;
				}
				if (colorArray.IsSetTint())
				{
					cT_Color.tint = colorArray.tint;
					cT_Color.tintSpecified = true;
				}
			}
			if (ctFont.sizeOfSzArray() > 0)
			{
				pr.AddNewSz().val = ctFont.GetSzArray(0).val;
			}
			if (ctFont.sizeOfNameArray() > 0)
			{
				pr.AddNewRFont().val = ctFont.name.val;
			}
			if (ctFont.sizeOfFamilyArray() > 0)
			{
				pr.AddNewFamily().val = ctFont.GetFamilyArray(0).val;
			}
			if (ctFont.sizeOfSchemeArray() > 0)
			{
				pr.AddNewScheme().val = ctFont.GetSchemeArray(0).val;
			}
			if (ctFont.sizeOfCharsetArray() > 0)
			{
				pr.AddNewCharset().val = ctFont.GetCharsetArray(0).val;
			}
			if (ctFont.sizeOfCondenseArray() > 0)
			{
				pr.AddNewCondense().val = ctFont.GetCondenseArray(0).val;
			}
			if (ctFont.sizeOfExtendArray() > 0)
			{
				pr.AddNewExtend().val = ctFont.GetExtendArray(0).val;
			}
			if (ctFont.sizeOfVertAlignArray() > 0)
			{
				pr.AddNewVertAlign().val = ctFont.GetVertAlignArray(0).val;
			}
			if (ctFont.sizeOfOutlineArray() > 0)
			{
				pr.AddNewOutline().val = ctFont.GetOutlineArray(0).val;
			}
			if (ctFont.sizeOfShadowArray() > 0)
			{
				pr.AddNewShadow().val = ctFont.GetShadowArray(0).val;
			}
			if (ctFont.sizeOfStrikeArray() > 0)
			{
				pr.AddNewStrike().val = ctFont.GetStrikeArray(0).val;
			}
		}

		/// Removes any formatting that may have been applied to the string.
		public void ClearFormatting()
		{
			string @string = String;
			st.r = null;
			st.t = @string;
		}

		/// The index within the string to which the specified formatting run applies.
		///
		/// @param index     the index of the formatting run
		/// @return  the index within the string.
		public int GetIndexOfFormattingRun(int index)
		{
			if (st.sizeOfRArray() == 0)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < st.sizeOfRArray(); i++)
			{
				CT_RElt rArray = st.GetRArray(i);
				if (i == index)
				{
					return num;
				}
				num += rArray.t.Length;
			}
			return -1;
		}

		/// Returns the number of characters this format run covers.
		///
		/// @param index     the index of the formatting run
		/// @return  the number of characters this format run covers
		public int GetLengthOfFormattingRun(int index)
		{
			if (st.sizeOfRArray() == 0)
			{
				return Length;
			}
			for (int i = 0; i < st.sizeOfRArray(); i++)
			{
				CT_RElt rArray = st.GetRArray(i);
				if (i == index)
				{
					return rArray.t.Length;
				}
			}
			return -1;
		}

		/// Returns the plain string representation.
		public override string ToString()
		{
			return String;
		}

		/// Gets a copy of the font used in a particular formatting Run.
		///
		/// @param index     the index of the formatting run
		/// @return  A copy of the  font used or null if no formatting is applied to the specified text Run.
		public IFont GetFontOfFormattingRun(int index)
		{
			if (st.sizeOfRArray() == 0)
			{
				return null;
			}
			for (int i = 0; i < st.sizeOfRArray(); i++)
			{
				CT_RElt rArray = st.GetRArray(i);
				if (i == index)
				{
					XSSFFont xSSFFont = new XSSFFont(ToCTFont(rArray.rPr));
					xSSFFont.SetThemesTable(GetThemesTable());
					return xSSFFont;
				}
			}
			return null;
		}

		/// Return a copy of the font in use at a particular index.
		///
		/// @param index         The index.
		/// @return              A copy of the  font that's currently being applied at that
		///                      index or null if no font is being applied or the
		///                      index is out of range.
		public short GetFontAtIndex(int index)
		{
			if (st.sizeOfRArray() == 0)
			{
				return -1;
			}
			int num = 0;
			for (int i = 0; i < st.sizeOfRArray(); i++)
			{
				CT_RElt rArray = st.GetRArray(i);
				if (index >= num && index < num + rArray.t.Length)
				{
					XSSFFont xSSFFont = new XSSFFont(ToCTFont(rArray.rPr));
					xSSFFont.SetThemesTable(GetThemesTable());
					return xSSFFont.Index;
				}
				num += rArray.t.Length;
			}
			return -1;
		}

		/// Return the underlying xml bean
		public CT_Rst GetCTRst()
		{
			return st;
		}

		/// CTRPrElt --&gt; CTFont adapter
		protected static CT_Font ToCTFont(CT_RPrElt pr)
		{
			CT_Font cT_Font = new CT_Font();
			if (pr.sizeOfBArray() > 0)
			{
				cT_Font.AddNewB().val = pr.GetBArray(0).val;
			}
			if (pr.sizeOfUArray() > 0)
			{
				cT_Font.AddNewU().val = pr.GetUArray(0).val;
			}
			if (pr.sizeOfIArray() > 0)
			{
				cT_Font.AddNewI().val = pr.GetIArray(0).val;
			}
			if (pr.sizeOfColorArray() > 0)
			{
				CT_Color colorArray = pr.GetColorArray(0);
				CT_Color cT_Color = cT_Font.AddNewColor();
				if (colorArray.IsSetAuto())
				{
					cT_Color.auto = colorArray.auto;
					cT_Color.autoSpecified = true;
				}
				if (colorArray.IsSetIndexed())
				{
					cT_Color.indexed = colorArray.indexed;
					cT_Color.indexedSpecified = true;
				}
				if (colorArray.IsSetRgb())
				{
					cT_Color.SetRgb(colorArray.GetRgb());
					cT_Color.rgbSpecified = true;
				}
				if (colorArray.IsSetTheme())
				{
					cT_Color.theme = colorArray.theme;
					cT_Color.themeSpecified = true;
				}
				if (colorArray.IsSetTint())
				{
					cT_Color.tint = colorArray.tint;
					cT_Color.tintSpecified = true;
				}
			}
			if (pr.sizeOfSzArray() > 0)
			{
				cT_Font.AddNewSz().val = pr.GetSzArray(0).val;
			}
			if (pr.sizeOfRFontArray() > 0)
			{
				cT_Font.AddNewName().val = pr.GetRFontArray(0).val;
			}
			if (pr.sizeOfFamilyArray() > 0)
			{
				cT_Font.AddNewFamily().val = pr.GetFamilyArray(0).val;
			}
			if (pr.sizeOfSchemeArray() > 0)
			{
				cT_Font.AddNewScheme().val = pr.GetSchemeArray(0).val;
			}
			if (pr.sizeOfCharsetArray() > 0)
			{
				cT_Font.AddNewCharset().val = pr.GetCharsetArray(0).val;
			}
			if (pr.sizeOfCondenseArray() > 0)
			{
				cT_Font.AddNewCondense().val = pr.GetCondenseArray(0).val;
			}
			if (pr.sizeOfExtendArray() > 0)
			{
				cT_Font.AddNewExtend().val = pr.GetExtendArray(0).val;
			}
			if (pr.sizeOfVertAlignArray() > 0)
			{
				cT_Font.AddNewVertAlign().val = pr.GetVertAlignArray(0).val;
			}
			if (pr.sizeOfOutlineArray() > 0)
			{
				cT_Font.AddNewOutline().val = pr.GetOutlineArray(0).val;
			}
			if (pr.sizeOfShadowArray() > 0)
			{
				cT_Font.AddNewShadow().val = pr.GetShadowArray(0).val;
			}
			if (pr.sizeOfStrikeArray() > 0)
			{
				cT_Font.AddNewStrike().val = pr.GetStrikeArray(0).val;
			}
			return cT_Font;
		}

		/// **
		protected static void PreserveSpaces(string xs)
		{
			if (xs != null && xs.Length > 0)
			{
				char c = xs[0];
				char c2 = xs[xs.Length - 1];
				if (!char.IsWhiteSpace(c))
				{
					char.IsWhiteSpace(c2);
				}
			}
		}

		/// For all characters which cannot be represented in XML as defined by the XML 1.0 specification,
		/// the characters are escaped using the Unicode numerical character representation escape character
		/// format _xHHHH_, where H represents a hexadecimal character in the character's value.
		/// <p>
		/// Example: The Unicode character 0D is invalid in an XML 1.0 document,
		/// so it shall be escaped as <code>_x000D_</code>.
		/// </p>
		/// See section 3.18.9 in the OOXML spec.
		///
		/// @param   value the string to decode
		/// @return  the decoded string
		private static string UtfDecode(string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			MatchCollection matchCollection = utfPtrn.Matches(value);
			int num = 0;
			for (int i = 0; i < matchCollection.Count; i++)
			{
				int index = matchCollection[i].Index;
				if (index > num)
				{
					stringBuilder.Append(value.Substring(num, index - num));
				}
				string value2 = matchCollection[i].Groups[1].Value;
				int num2 = int.Parse(value2, NumberStyles.AllowHexSpecifier);
				stringBuilder.Append((char)num2);
				num = matchCollection[i].Index + matchCollection[i].Length;
			}
			stringBuilder.Append(value.Substring(num));
			return stringBuilder.ToString();
		}

		public int GetLastKey(SortedDictionary<int, CT_RPrElt>.KeyCollection keys)
		{
			int num = 0;
			foreach (int key in keys)
			{
				if (num == keys.Count - 1)
				{
					return key;
				}
				num++;
			}
			throw new ArgumentOutOfRangeException("GetLastKey failed");
		}

		private CT_Rst buildCTRst(string text, SortedDictionary<int, CT_RPrElt> formats)
		{
			if (text.Length != GetLastKey(formats.Keys))
			{
				throw new ArgumentException("Text length was " + text.Length + " but the last format index was " + GetLastKey(formats.Keys));
			}
			CT_Rst cT_Rst = new CT_Rst();
			int num = 0;
			SortedDictionary<int, CT_RPrElt>.KeyCollection.Enumerator enumerator = formats.Keys.GetEnumerator();
			while (enumerator.MoveNext())
			{
				int current = enumerator.Current;
				CT_RElt cT_RElt = cT_Rst.AddNewR();
				string text3 = cT_RElt.t = text.Substring(num, current - num);
				PreserveSpaces(cT_RElt.t);
				CT_RPrElt cT_RPrElt = formats[current];
				if (cT_RPrElt != null)
				{
					cT_RElt.rPr = cT_RPrElt;
				}
				num = current;
			}
			return cT_Rst;
		}

		private ThemesTable GetThemesTable()
		{
			if (styles == null)
			{
				return null;
			}
			return styles.GetTheme();
		}
	}
}
