using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;

namespace NPOI.SS.Util
{
	/// Various utility functions that make working with a cells and rows easier. The various methods
	/// that deal with style's allow you to create your CellStyles as you need them. When you apply a
	/// style change to a cell, the code will attempt to see if a style already exists that meets your
	/// needs. If not, then it will create a new style. This is to prevent creating too many styles.
	/// there is an upper limit in Excel on the number of styles that can be supported.
	///
	///             @author Eric Pugh epugh@upstate.com
	///             @author (secondary) Avinash Kewalramani akewalramani@accelrys.com
	public class CellUtil
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

		public const string BORDER_DIAGONAL = "borderDiagonal";

		public const string BORDER_LEFT = "borderLeft";

		public const string BORDER_RIGHT = "borderRight";

		public const string BORDER_TOP = "borderTop";

		public const string BOTTOM_BORDER_COLOR = "bottomBorderColor";

		public const string DATA_FORMAT = "dataFormat";

		public const string DIAGONAL_BORDER_COLOR = "diagonalBorderColor";

		public const string DIAGONAL_BORDER_LINE_STYLE = "diagonalBorderLineStyle";

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

		public const string SHRINK_TO_FIT = "shrinkToFit";

		public const string TOP_BORDER_COLOR = "topBorderColor";

		public const string VERTICAL_ALIGNMENT = "verticalAlignment";

		public const string WRAP_TEXT = "wrapText";

		private static UnicodeMapping[] unicodeMappings;

		private CellUtil()
		{
		}

		public static ICell CopyCell(IRow row, int sourceIndex, int targetIndex)
		{
			if (sourceIndex == targetIndex)
			{
				throw new ArgumentException("sourceIndex and targetIndex cannot be same");
			}
			ICell cell = row.GetCell(sourceIndex);
			if (cell == null)
			{
				return null;
			}
			ICell cell2 = row.GetCell(targetIndex);
			if (cell2 == null)
			{
				cell2 = row.CreateCell(targetIndex);
			}
			if (cell.CellStyle != null)
			{
				cell2.CellStyle = cell.CellStyle;
			}
			if (cell.CellComment != null)
			{
				cell2.CellComment = cell.CellComment;
			}
			if (cell.Hyperlink != null)
			{
				cell2.Hyperlink = cell.Hyperlink;
			}
			cell2.SetCellType(cell.CellType);
			switch (cell.CellType)
			{
			case CellType.Blank:
				cell2.SetCellValue(cell.StringCellValue);
				break;
			case CellType.Boolean:
				cell2.SetCellValue(cell.BooleanCellValue);
				break;
			case CellType.Error:
				cell2.SetCellErrorValue(cell.ErrorCellValue);
				break;
			case CellType.Formula:
				cell2.SetCellFormula(cell.CellFormula);
				break;
			case CellType.Numeric:
				cell2.SetCellValue(cell.NumericCellValue);
				break;
			case CellType.String:
				cell2.SetCellValue(cell.RichStringCellValue);
				break;
			}
			return cell2;
		}

		/// Get a row from the spreadsheet, and create it if it doesn't exist.
		///
		///             @param rowIndex The 0 based row number
		///             @param sheet The sheet that the row is part of.
		///             @return The row indicated by the rowCounter
		public static IRow GetRow(int rowIndex, ISheet sheet)
		{
			IRow row = sheet.GetRow(rowIndex);
			if (row == null)
			{
				row = sheet.CreateRow(rowIndex);
			}
			return row;
		}

		/// Get a specific cell from a row. If the cell doesn't exist, then create it.
		///
		///             @param row The row that the cell is part of
		///             @param columnIndex The column index that the cell is in.
		///             @return The cell indicated by the column.
		public static ICell GetCell(IRow row, int columnIndex)
		{
			ICell cell = row.GetCell(columnIndex);
			if (cell == null)
			{
				cell = row.CreateCell(columnIndex);
			}
			return cell;
		}

		/// Creates a cell, gives it a value, and applies a style if provided
		///
		/// @param  row     the row to create the cell in
		/// @param  column  the column index to create the cell in
		/// @param  value   The value of the cell
		/// @param  style   If the style is not null, then set
		/// @return         A new Cell
		public static ICell CreateCell(IRow row, int column, string value, ICellStyle style)
		{
			ICell cell = GetCell(row, column);
			cell.SetCellValue(cell.Row.Sheet.Workbook.GetCreationHelper().CreateRichTextString(value));
			if (style != null)
			{
				cell.CellStyle = style;
			}
			return cell;
		}

		/// Create a cell, and give it a value.
		///
		///             @param  row     the row to create the cell in
		///             @param  column  the column index to create the cell in
		///             @param  value   The value of the cell
		///             @return         A new Cell.
		public static ICell CreateCell(IRow row, int column, string value)
		{
			return CreateCell(row, column, value, null);
		}

		/// Take a cell, and align it.
		///
		///             @param cell the cell to set the alignment for
		///             @param workbook The workbook that is being worked with.
		///             @param align the column alignment to use.
		///
		/// @see CellStyle for alignment options
		public static void SetAlignment(ICell cell, IWorkbook workbook, short align)
		{
			SetCellStyleProperty(cell, workbook, "alignment", align);
		}

		/// Take a cell, and apply a font to it
		///
		///             @param cell the cell to set the alignment for
		///             @param workbook The workbook that is being worked with.
		///             @param font The Font that you want to set...
		public static void SetFont(ICell cell, IWorkbook workbook, IFont font)
		{
			SetCellStyleProperty(cell, workbook, "font", font.Index);
		}

		/// This method attempt to find an already existing CellStyle that matches what you want the
		/// style to be. If it does not find the style, then it creates a new one. If it does create a
		/// new one, then it applies the propertyName and propertyValue to the style. This is necessary
		/// because Excel has an upper limit on the number of Styles that it supports.
		///
		///             @param workbook The workbook that is being worked with.
		///             @param propertyName The name of the property that is to be changed.
		///             @param propertyValue The value of the property that is to be changed.
		///             @param cell The cell that needs it's style changes
		public static void SetCellStyleProperty(ICell cell, IWorkbook workbook, string propertyName, object propertyValue)
		{
			ICellStyle cellStyle = cell.CellStyle;
			ICellStyle cellStyle2 = null;
			Dictionary<string, object> formatProperties = GetFormatProperties(cellStyle);
			if (formatProperties.ContainsKey(propertyName))
			{
				formatProperties[propertyName] = propertyValue;
			}
			else
			{
				formatProperties.Add(propertyName, propertyValue);
			}
			short numCellStyles = workbook.NumCellStyles;
			for (short num = 0; num < numCellStyles; num = (short)(num + 1))
			{
				ICellStyle cellStyleAt = workbook.GetCellStyleAt(num);
				Dictionary<string, object> formatProperties2 = GetFormatProperties(cellStyleAt);
				if (formatProperties.Keys.Count == formatProperties2.Keys.Count)
				{
					bool flag = true;
					foreach (string key in formatProperties.Keys)
					{
						if (!formatProperties2.ContainsKey(key))
						{
							flag = false;
							break;
						}
						if (!formatProperties[key].Equals(formatProperties2[key]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						cellStyle2 = cellStyleAt;
						break;
					}
				}
			}
			if (cellStyle2 == null)
			{
				cellStyle2 = workbook.CreateCellStyle();
				SetFormatProperties(cellStyle2, workbook, formatProperties);
			}
			cell.CellStyle = cellStyle2;
		}

		/// Returns a map containing the format properties of the given cell style.
		///
		/// @param style cell style
		/// @return map of format properties (String -&gt; Object)
		/// @see #setFormatProperties(org.apache.poi.ss.usermodel.CellStyle, org.apache.poi.ss.usermodel.Workbook, java.util.Map)
		private static Dictionary<string, object> GetFormatProperties(ICellStyle style)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PutShort(dictionary, "alignment", (short)style.Alignment);
			PutShort(dictionary, "borderBottom", (short)style.BorderBottom);
			PutShort(dictionary, "borderDiagonal", (short)style.BorderDiagonal);
			PutShort(dictionary, "borderLeft", (short)style.BorderLeft);
			PutShort(dictionary, "borderRight", (short)style.BorderRight);
			PutShort(dictionary, "borderTop", (short)style.BorderTop);
			PutShort(dictionary, "bottomBorderColor", style.BottomBorderColor);
			PutShort(dictionary, "dataFormat", style.DataFormat);
			PutShort(dictionary, "diagonalBorderColor", style.BorderDiagonalColor);
			PutShort(dictionary, "diagonalBorderLineStyle", (short)style.BorderDiagonalLineStyle);
			PutShort(dictionary, "fillBackgroundColor", style.FillBackgroundColor);
			PutShort(dictionary, "fillForegroundColor", style.FillForegroundColor);
			PutShort(dictionary, "fillPattern", (short)style.FillPattern);
			PutShort(dictionary, "font", style.FontIndex);
			PutBoolean(dictionary, "hidden", style.IsHidden);
			PutShort(dictionary, "indention", style.Indention);
			PutShort(dictionary, "leftBorderColor", style.LeftBorderColor);
			PutBoolean(dictionary, "locked", style.IsLocked);
			PutShort(dictionary, "rightBorderColor", style.RightBorderColor);
			PutShort(dictionary, "rotation", style.Rotation);
			PutBoolean(dictionary, "shrinkToFit", style.ShrinkToFit);
			PutShort(dictionary, "topBorderColor", style.TopBorderColor);
			PutShort(dictionary, "verticalAlignment", (short)style.VerticalAlignment);
			PutBoolean(dictionary, "wrapText", style.WrapText);
			return dictionary;
		}

		/// Sets the format properties of the given style based on the given map.
		///
		/// @param style cell style
		/// @param workbook parent workbook
		/// @param properties map of format properties (String -&gt; Object)
		/// @see #getFormatProperties(CellStyle)
		private static void SetFormatProperties(ICellStyle style, IWorkbook workbook, Dictionary<string, object> properties)
		{
			style.Alignment = (HorizontalAlignment)GetShort(properties, "alignment");
			style.BorderBottom = (BorderStyle)GetShort(properties, "borderBottom");
			style.BorderDiagonalColor = GetShort(properties, "diagonalBorderColor");
			style.BorderDiagonal = (BorderDiagonal)GetShort(properties, "borderDiagonal");
			style.BorderDiagonalLineStyle = (BorderStyle)GetShort(properties, "diagonalBorderLineStyle");
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
			style.ShrinkToFit = GetBoolean(properties, "shrinkToFit");
			style.TopBorderColor = GetShort(properties, "topBorderColor");
			style.VerticalAlignment = (VerticalAlignment)GetShort(properties, "verticalAlignment");
			style.WrapText = GetBoolean(properties, "wrapText");
		}

		/// Utility method that returns the named short value form the given map.
		/// @return zero if the property does not exist, or is not a {@link Short}.
		///
		/// @param properties map of named properties (String -&gt; Object)
		/// @param name property name
		/// @return property value, or zero
		private static short GetShort(Dictionary<string, object> properties, string name)
		{
			object obj = properties[name];
			short result = 0;
			if (short.TryParse(obj.ToString(), out result))
			{
				return result;
			}
			return 0;
		}

		/// Utility method that returns the named boolean value form the given map.
		/// @return false if the property does not exist, or is not a {@link Boolean}.
		///
		/// @param properties map of properties (String -&gt; Object)
		/// @param name property name
		/// @return property value, or false
		private static bool GetBoolean(Dictionary<string, object> properties, string name)
		{
			object obj = properties[name];
			bool result = false;
			if (bool.TryParse(obj.ToString(), out result))
			{
				return result;
			}
			return false;
		}

		/// Utility method that puts the named short value to the given map.
		///
		/// @param properties map of properties (String -&gt; Object)
		/// @param name property name
		/// @param value property value
		private static void PutShort(Dictionary<string, object> properties, string name, short value)
		{
			if (properties.ContainsKey(name))
			{
				properties[name] = value;
			}
			else
			{
				properties.Add(name, value);
			}
		}

		/// Utility method that puts the named boolean value to the given map.
		///
		/// @param properties map of properties (String -&gt; Object)
		/// @param name property name
		/// @param value property value
		private static void PutBoolean(Dictionary<string, object> properties, string name, bool value)
		{
			if (properties.ContainsKey(name))
			{
				properties[name] = value;
			}
			else
			{
				properties.Add(name, value);
			}
		}

		/// Looks for text in the cell that should be unicode, like an alpha and provides the
		/// unicode version of it.
		///
		///             @param  cell  The cell to check for unicode values
		///             @return       translated to unicode
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
				cell.SetCellValue(cell.Row.Sheet.Workbook.GetCreationHelper().CreateRichTextString(text));
			}
			return cell;
		}

		static CellUtil()
		{
			unicodeMappings = new UnicodeMapping[15]
			{
				um("alpha", "α"),
				um("beta", "β"),
				um("gamma", "γ"),
				um("delta", "δ"),
				um("epsilon", "ε"),
				um("zeta", "ζ"),
				um("eta", "η"),
				um("theta", "θ"),
				um("iota", "ι"),
				um("kappa", "κ"),
				um("lambda", "λ"),
				um("mu", "μ"),
				um("nu", "ν"),
				um("xi", "ξ"),
				um("omicron", "ο")
			};
		}

		private static UnicodeMapping um(string entityName, string resolvedValue)
		{
			return new UnicodeMapping(entityName, resolvedValue);
		}
	}
}
