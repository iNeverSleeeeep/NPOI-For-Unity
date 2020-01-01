using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.Util
{
	/// <summary>
	/// Various utility functions that make working with a cells and rows easier.  The various
	/// methods that deal with style's allow you to Create your HSSFCellStyles as you need them.
	/// When you apply a style change to a cell, the code will attempt to see if a style already
	/// exists that meets your needs.  If not, then it will Create a new style.  This is to prevent
	/// creating too many styles.  there is an upper limit in Excel on the number of styles that
	/// can be supported.
	/// @author     Eric Pugh epugh@upstate.com
	/// </summary>
	public class HSSFCellUtil
	{
		private class UnicodeMapping
		{
			public string entityName;

			public string resolvedValue;

			public UnicodeMapping(string pEntityName, string pResolvedValue)
			{
				entityName = "&" + pEntityName + ";";
				resolvedValue = pResolvedValue;
			}
		}

		public const string ALIGNMENT = "alignment";

		public const string BORDER_BOTTOM = "borderBottom";

		public const string BORDER_LEFT = "borderLeft";

		public const string BORDER_RIGHT = "borderRight";

		public const string BORDER_TOP = "borderTop";

		public const string BOTTOM_BORDER_COLOR = "bottomBorderColor";

		public const string DATA_FORMAT = "dataFormat";

		public const string FILL_BACKGROUND_COLOR = "fillBackgroundColor";

		public const string FILL_FOREGROUND_COLOR = "fillForegroundColor";

		public const string FILL_PATTERN = "fillPattern";

		public const string FONT = "font";

		public const string HIDDEN = "hidden";

		public const string INDENTION = "indention";

		public const string LEFT_BORDER_COLOR = "leftBorderColor";

		public const string LOCKED = "locked";

		public const string RIGHT_BORDER_COLOR = "rightBorderColor";

		public const string ROTATION = "rotation";

		public const string TOP_BORDER_COLOR = "topBorderColor";

		public const string VERTICAL_ALIGNMENT = "verticalAlignment";

		public const string WRAP_TEXT = "wrapText";

		private static UnicodeMapping[] unicodeMappings;

		static HSSFCellUtil()
		{
			unicodeMappings = new UnicodeMapping[15];
			unicodeMappings[0] = um("alpha", "α");
			unicodeMappings[1] = um("beta", "β");
			unicodeMappings[2] = um("gamma", "γ");
			unicodeMappings[3] = um("delta", "δ");
			unicodeMappings[4] = um("epsilon", "ε");
			unicodeMappings[5] = um("zeta", "ζ");
			unicodeMappings[6] = um("eta", "η");
			unicodeMappings[7] = um("theta", "θ");
			unicodeMappings[8] = um("iota", "ι");
			unicodeMappings[9] = um("kappa", "κ");
			unicodeMappings[10] = um("lambda", "λ");
			unicodeMappings[11] = um("mu", "μ");
			unicodeMappings[12] = um("nu", "ν");
			unicodeMappings[13] = um("xi", "ξ");
			unicodeMappings[14] = um("omicron", "ο");
		}

		private HSSFCellUtil()
		{
		}

		/// <summary>
		/// Get a row from the spreadsheet, and Create it if it doesn't exist.
		/// </summary>
		/// <param name="rowCounter">The 0 based row number</param>
		/// <param name="sheet">The sheet that the row is part of.</param>
		/// <returns>The row indicated by the rowCounter</returns>
		public static IRow GetRow(int rowCounter, HSSFSheet sheet)
		{
			IRow row = sheet.GetRow(rowCounter);
			if (row == null)
			{
				row = sheet.CreateRow(rowCounter);
			}
			return row;
		}

		/// <summary>
		/// Get a specific cell from a row. If the cell doesn't exist,
		/// </summary>
		/// <param name="row">The row that the cell is part of</param>
		/// <param name="column">The column index that the cell is in.</param>
		/// <returns>The cell indicated by the column.</returns>
		public static ICell GetCell(IRow row, int column)
		{
			ICell cell = row.GetCell(column);
			if (cell == null)
			{
				cell = row.CreateCell(column);
			}
			return cell;
		}

		/// <summary>
		/// Creates a cell, gives it a value, and applies a style if provided
		/// </summary>
		/// <param name="row">the row to Create the cell in</param>
		/// <param name="column">the column index to Create the cell in</param>
		/// <param name="value">The value of the cell</param>
		/// <param name="style">If the style is not null, then Set</param>
		/// <returns>A new HSSFCell</returns>
		public static ICell CreateCell(IRow row, int column, string value, HSSFCellStyle style)
		{
			ICell cell = GetCell(row, column);
			cell.SetCellValue(new HSSFRichTextString(value));
			if (style != null)
			{
				cell.CellStyle = style;
			}
			return cell;
		}

		/// <summary>
		/// Create a cell, and give it a value.
		/// </summary>
		/// <param name="row">the row to Create the cell in</param>
		/// <param name="column">the column index to Create the cell in</param>
		/// <param name="value">The value of the cell</param>
		/// <returns>A new HSSFCell.</returns>
		public static ICell CreateCell(IRow row, int column, string value)
		{
			return CreateCell(row, column, value, null);
		}

		/// <summary>
		/// Translate color palette entries from the source to the destination sheet
		/// </summary>
		private static void RemapCellStyle(HSSFCellStyle stylish, Dictionary<short, short> paletteMap)
		{
			if (paletteMap.ContainsKey(stylish.BorderDiagonalColor))
			{
				stylish.BorderDiagonalColor = paletteMap[stylish.BorderDiagonalColor];
			}
			if (paletteMap.ContainsKey(stylish.BottomBorderColor))
			{
				stylish.BottomBorderColor = paletteMap[stylish.BottomBorderColor];
			}
			if (paletteMap.ContainsKey(stylish.FillBackgroundColor))
			{
				stylish.FillBackgroundColor = paletteMap[stylish.FillBackgroundColor];
			}
			if (paletteMap.ContainsKey(stylish.FillForegroundColor))
			{
				stylish.FillForegroundColor = paletteMap[stylish.FillForegroundColor];
			}
			if (paletteMap.ContainsKey(stylish.LeftBorderColor))
			{
				stylish.LeftBorderColor = paletteMap[stylish.LeftBorderColor];
			}
			if (paletteMap.ContainsKey(stylish.RightBorderColor))
			{
				stylish.RightBorderColor = paletteMap[stylish.RightBorderColor];
			}
			if (paletteMap.ContainsKey(stylish.TopBorderColor))
			{
				stylish.TopBorderColor = paletteMap[stylish.TopBorderColor];
			}
		}

		public static void CopyCell(HSSFCell oldCell, HSSFCell newCell, IDictionary<int, HSSFCellStyle> styleMap, Dictionary<short, short> paletteMap, bool keepFormulas)
		{
			if (styleMap != null)
			{
				if (oldCell.CellStyle != null)
				{
					if (oldCell.Sheet.Workbook == newCell.Sheet.Workbook)
					{
						newCell.CellStyle = oldCell.CellStyle;
					}
					else
					{
						int hashCode = oldCell.CellStyle.GetHashCode();
						if (styleMap.ContainsKey(hashCode))
						{
							newCell.CellStyle = styleMap[hashCode];
						}
						else
						{
							HSSFCellStyle hSSFCellStyle = (HSSFCellStyle)newCell.Sheet.Workbook.CreateCellStyle();
							hSSFCellStyle.CloneStyleFrom(oldCell.CellStyle);
							RemapCellStyle(hSSFCellStyle, paletteMap);
							newCell.CellStyle = hSSFCellStyle;
							IFont font = hSSFCellStyle.GetFont(newCell.Sheet.Workbook);
							if (font.Color > 0 && paletteMap.ContainsKey(font.Color))
							{
								font.Color = paletteMap[font.Color];
							}
							styleMap.Add(hashCode, hSSFCellStyle);
						}
					}
				}
				else
				{
					newCell.CellStyle = null;
				}
			}
			switch (oldCell.CellType)
			{
			case CellType.String:
			{
				HSSFRichTextString hSSFRichTextString = oldCell.RichStringCellValue as HSSFRichTextString;
				newCell.SetCellValue(hSSFRichTextString);
				if (hSSFRichTextString != null)
				{
					for (int i = 0; i < hSSFRichTextString.NumFormattingRuns; i++)
					{
						short fontOfFormattingRun = hSSFRichTextString.GetFontOfFormattingRun(i);
						int indexOfFormattingRun = hSSFRichTextString.GetIndexOfFormattingRun(i);
						int num = 0;
						num = ((i + 1 != hSSFRichTextString.NumFormattingRuns) ? hSSFRichTextString.GetIndexOfFormattingRun(i + 1) : hSSFRichTextString.Length);
						FontRecord fontRecord = newCell.BoundWorkbook.CreateNewFont();
						fontRecord.CloneStyleFrom(oldCell.BoundWorkbook.GetFontRecordAt(fontOfFormattingRun));
						HSSFFont font2 = new HSSFFont((short)newCell.BoundWorkbook.GetFontIndex(fontRecord), fontRecord);
						newCell.RichStringCellValue.ApplyFont(indexOfFormattingRun, num, font2);
					}
				}
				break;
			}
			case CellType.Numeric:
				newCell.SetCellValue(oldCell.NumericCellValue);
				break;
			case CellType.Blank:
				newCell.SetCellType(CellType.Blank);
				break;
			case CellType.Boolean:
				newCell.SetCellValue(oldCell.BooleanCellValue);
				break;
			case CellType.Error:
				newCell.SetCellValue((double)(int)oldCell.ErrorCellValue);
				break;
			case CellType.Formula:
				if (keepFormulas)
				{
					newCell.SetCellType(CellType.Formula);
					newCell.CellFormula = oldCell.CellFormula;
				}
				else
				{
					try
					{
						newCell.SetCellType(CellType.Numeric);
						newCell.SetCellValue(oldCell.NumericCellValue);
					}
					catch (Exception)
					{
						newCell.SetCellType(CellType.String);
						newCell.SetCellValue(oldCell.ToString());
					}
				}
				break;
			}
		}

		/// <summary>
		/// Take a cell, and align it.
		/// </summary>
		/// <param name="cell">the cell to Set the alignment for</param>
		/// <param name="workbook">The workbook that is being worked with.</param>
		/// <param name="align">the column alignment to use.</param>
		public static void SetAlignment(ICell cell, HSSFWorkbook workbook, short align)
		{
			SetCellStyleProperty(cell, workbook, "alignment", align);
		}

		/// <summary>
		/// Take a cell, and apply a font to it
		/// </summary>
		/// <param name="cell">the cell to Set the alignment for</param>
		/// <param name="workbook">The workbook that is being worked with.</param>
		/// <param name="font">The HSSFFont that you want to Set...</param>
		public static void SetFont(ICell cell, HSSFWorkbook workbook, HSSFFont font)
		{
			SetCellStyleProperty(cell, workbook, "font", font);
		}

		private static bool CompareHashTableKeyValueIsEqual(Hashtable a, Hashtable b)
		{
			IDictionaryEnumerator enumerator = a.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
					IDictionaryEnumerator enumerator2 = b.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							DictionaryEntry dictionaryEntry2 = (DictionaryEntry)enumerator2.Current;
							if (dictionaryEntry.Key.ToString() == dictionaryEntry2.Key.ToString() && ((dictionaryEntry.Value is short && dictionaryEntry2.Value is short && (short)dictionaryEntry.Value != (short)dictionaryEntry2.Value) || (dictionaryEntry.Value is bool && dictionaryEntry2.Value is bool && (bool)dictionaryEntry.Value != (bool)dictionaryEntry2.Value)))
							{
								return false;
							}
						}
					}
					finally
					{
						(enumerator2 as IDisposable)?.Dispose();
					}
				}
			}
			finally
			{
				(enumerator as IDisposable)?.Dispose();
			}
			return true;
		}

		/// This method attempt to find an already existing HSSFCellStyle that matches
		/// what you want the style to be. If it does not find the style, then it
		/// Creates a new one. If it does Create a new one, then it applies the
		/// propertyName and propertyValue to the style. This is necessary because
		/// Excel has an upper limit on the number of Styles that it supports.
		///
		///             @param  workbook               The workbook that is being worked with.
		///             @param  propertyName           The name of the property that is to be
		///     changed.
		///             @param  propertyValue          The value of the property that is to be
		///     changed.
		///             @param  cell                   The cell that needs it's style changes
		///             @exception  NestableException  Thrown if an error happens.
		public static void SetCellStyleProperty(ICell cell, HSSFWorkbook workbook, string propertyName, object propertyValue)
		{
			ICellStyle cellStyle = cell.CellStyle;
			ICellStyle cellStyle2 = null;
			Hashtable formatProperties = GetFormatProperties(cellStyle);
			formatProperties[propertyName] = propertyValue;
			short numCellStyles = workbook.NumCellStyles;
			for (short num = 0; num < numCellStyles; num = (short)(num + 1))
			{
				ICellStyle cellStyleAt = workbook.GetCellStyleAt(num);
				Hashtable formatProperties2 = GetFormatProperties(cellStyleAt);
				if (CompareHashTableKeyValueIsEqual(formatProperties2, formatProperties))
				{
					cellStyle2 = cellStyleAt;
					break;
				}
			}
			if (cellStyle2 == null)
			{
				cellStyle2 = workbook.CreateCellStyle();
				SetFormatProperties(cellStyle2, workbook, formatProperties);
			}
			cell.CellStyle = cellStyle2;
		}

		/// <summary>
		/// Returns a map containing the format properties of the given cell style.
		/// </summary>
		/// <param name="style">cell style</param>
		/// <returns>map of format properties (String -&gt; Object)</returns>
		private static Hashtable GetFormatProperties(ICellStyle style)
		{
			Hashtable hashtable = new Hashtable();
			PutShort(hashtable, "alignment", (short)style.Alignment);
			PutShort(hashtable, "borderBottom", (short)style.BorderBottom);
			PutShort(hashtable, "borderLeft", (short)style.BorderLeft);
			PutShort(hashtable, "borderRight", (short)style.BorderRight);
			PutShort(hashtable, "borderTop", (short)style.BorderTop);
			PutShort(hashtable, "bottomBorderColor", style.BottomBorderColor);
			PutShort(hashtable, "dataFormat", style.DataFormat);
			PutShort(hashtable, "fillBackgroundColor", style.FillBackgroundColor);
			PutShort(hashtable, "fillForegroundColor", style.FillForegroundColor);
			PutShort(hashtable, "fillPattern", (short)style.FillPattern);
			PutShort(hashtable, "font", style.FontIndex);
			PutBoolean(hashtable, "hidden", style.IsHidden);
			PutShort(hashtable, "indention", style.Indention);
			PutShort(hashtable, "leftBorderColor", style.LeftBorderColor);
			PutBoolean(hashtable, "locked", style.IsLocked);
			PutShort(hashtable, "rightBorderColor", style.RightBorderColor);
			PutShort(hashtable, "rotation", style.Rotation);
			PutShort(hashtable, "topBorderColor", style.TopBorderColor);
			PutShort(hashtable, "verticalAlignment", (short)style.VerticalAlignment);
			PutBoolean(hashtable, "wrapText", style.WrapText);
			return hashtable;
		}

		/// <summary>
		/// Sets the format properties of the given style based on the given map.
		/// </summary>
		/// <param name="style">The cell style</param>
		/// <param name="workbook">The parent workbook.</param>
		/// <param name="properties">The map of format properties (String -&gt; Object).</param>
		private static void SetFormatProperties(ICellStyle style, HSSFWorkbook workbook, Hashtable properties)
		{
			style.Alignment = (HorizontalAlignment)GetShort(properties, "alignment");
			style.BorderBottom = (BorderStyle)GetShort(properties, "borderBottom");
			style.BorderLeft = (BorderStyle)GetShort(properties, "borderLeft");
			style.BorderRight = (BorderStyle)GetShort(properties, "borderRight");
			style.BorderTop = (BorderStyle)GetShort(properties, "borderTop");
			style.BottomBorderColor = GetShort(properties, "bottomBorderColor");
			style.DataFormat = GetShort(properties, "dataFormat");
			style.FillBackgroundColor = GetShort(properties, "fillBackgroundColor");
			style.FillForegroundColor = GetShort(properties, "fillForegroundColor");
			style.FillPattern = (FillPattern)GetShort(properties, "fillPattern");
			style.SetFont(workbook.GetFontAt(GetShort(properties, "font")));
			style.IsHidden = GetBoolean(properties, "hidden");
			style.Indention = GetShort(properties, "indention");
			style.LeftBorderColor = GetShort(properties, "leftBorderColor");
			style.IsLocked = GetBoolean(properties, "locked");
			style.RightBorderColor = GetShort(properties, "rightBorderColor");
			style.Rotation = GetShort(properties, "rotation");
			style.TopBorderColor = GetShort(properties, "topBorderColor");
			style.VerticalAlignment = (VerticalAlignment)GetShort(properties, "verticalAlignment");
			style.WrapText = GetBoolean(properties, "wrapText");
		}

		/// <summary>
		/// Utility method that returns the named short value form the given map.
		/// Returns zero if the property does not exist, or is not a {@link Short}.
		/// </summary>
		/// <param name="properties">The map of named properties (String -&gt; Object)</param>
		/// <param name="name">The property name.</param>
		/// <returns>property value, or zero</returns>
		private static short GetShort(Hashtable properties, string name)
		{
			object obj = properties[name];
			if (obj is short)
			{
				return (short)obj;
			}
			return 0;
		}

		/// <summary>
		/// Utility method that returns the named boolean value form the given map.
		/// Returns false if the property does not exist, or is not a {@link Boolean}.
		/// </summary>
		/// <param name="properties">map of properties (String -&gt; Object)</param>
		/// <param name="name">The property name.</param>
		/// <returns>property value, or false</returns>
		private static bool GetBoolean(Hashtable properties, string name)
		{
			object obj = properties[name];
			if (obj is bool)
			{
				return (bool)obj;
			}
			return false;
		}

		/// <summary>
		/// Utility method that Puts the named short value to the given map.
		/// </summary>
		/// <param name="properties">The map of properties (String -&gt; Object).</param>
		/// <param name="name">The property name.</param>
		/// <param name="value">The property value.</param>
		private static void PutShort(Hashtable properties, string name, short value)
		{
			properties[name] = value;
		}

		/// <summary>
		/// Utility method that Puts the named boolean value to the given map.
		/// </summary>
		/// <param name="properties">map of properties (String -&gt; Object)</param>
		/// <param name="name">property name</param>
		/// <param name="value">property value</param>
		private static void PutBoolean(Hashtable properties, string name, bool value)
		{
			properties[name] = value;
		}

		/// <summary>
		/// Looks for text in the cell that should be unicode, like alpha; and provides the
		/// unicode version of it.
		/// </summary>
		/// <param name="cell">The cell to check for unicode values</param>
		/// <returns>transalted to unicode</returns>
		public static ICell TranslateUnicodeValues(ICell cell)
		{
			string text = cell.RichStringCellValue.String;
			bool flag = false;
			string text2 = text.ToLower();
			for (int i = 0; i < unicodeMappings.Length; i++)
			{
				UnicodeMapping unicodeMapping = unicodeMappings[i];
				string entityName = unicodeMapping.entityName;
				if (text2.IndexOf(entityName, StringComparison.Ordinal) != -1)
				{
					text = text.Replace(entityName, unicodeMapping.resolvedValue);
					flag = true;
				}
			}
			if (flag)
			{
				cell.SetCellValue(new HSSFRichTextString(text));
			}
			return cell;
		}

		private static UnicodeMapping um(string entityName, string resolvedValue)
		{
			return new UnicodeMapping(entityName, resolvedValue);
		}
	}
}
