using NPOI.SS.UserModel;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NPOI.SS.Util
{
	/// Helper methods for when working with Usermodel sheets
	///
	/// @author Yegor Kozlov
	public class SheetUtil
	{
		public class DummyEvaluator : IFormulaEvaluator
		{
			public bool DebugEvaluationOutputForNextEval
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			public void ClearAllCachedResultValues()
			{
			}

			public void NotifySetFormula(ICell cell)
			{
			}

			public void NotifyDeleteCell(ICell cell)
			{
			}

			public void NotifyUpdateCell(ICell cell)
			{
			}

			public CellValue Evaluate(ICell cell)
			{
				return null;
			}

			public ICell EvaluateInCell(ICell cell)
			{
				return null;
			}

			public void EvaluateAll()
			{
			}

			public CellType EvaluateFormulaCell(ICell cell)
			{
				return cell.CachedFormulaResultType;
			}
		}

		private static char defaultChar = '0';

		/// Dummy formula Evaluator that does nothing.
		/// YK: The only reason of having this class is that
		/// {@link NPOI.SS.UserModel.DataFormatter#formatCellValue(NPOI.SS.UserModel.Cell)}
		/// returns formula string for formula cells. Dummy Evaluator Makes it to format the cached formula result.
		///
		/// See Bugzilla #50021 
		private static IFormulaEvaluator dummyEvaluator = new DummyEvaluator();

		public static IRow CopyRow(ISheet sourceSheet, int sourceRowIndex, ISheet targetSheet, int targetRowIndex)
		{
			IRow row = targetSheet.GetRow(targetRowIndex);
			IRow row2 = sourceSheet.GetRow(sourceRowIndex);
			if (row != null)
			{
				targetSheet.RemoveRow(row);
			}
			row = targetSheet.CreateRow(targetRowIndex);
			if (row2 == null)
			{
				throw new ArgumentNullException("source row doesn't exist");
			}
			for (int i = row2.FirstCellNum; i < row2.LastCellNum; i++)
			{
				ICell cell = row2.GetCell(i);
				if (cell != null)
				{
					ICell cell2 = row.CreateCell(i);
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
				}
			}
			for (int j = 0; j < sourceSheet.NumMergedRegions; j++)
			{
				CellRangeAddress mergedRegion = sourceSheet.GetMergedRegion(j);
				if (mergedRegion.FirstRow == row2.RowNum)
				{
					CellRangeAddress region = new CellRangeAddress(row.RowNum, row.RowNum + (mergedRegion.LastRow - mergedRegion.FirstRow), mergedRegion.FirstColumn, mergedRegion.LastColumn);
					targetSheet.AddMergedRegion(region);
				}
			}
			return row;
		}

		public static IRow CopyRow(ISheet sheet, int sourceRowIndex, int targetRowIndex)
		{
			if (sourceRowIndex == targetRowIndex)
			{
				throw new ArgumentException("sourceIndex and targetIndex cannot be same");
			}
			IRow row = sheet.GetRow(targetRowIndex);
			IRow row2 = sheet.GetRow(sourceRowIndex);
			if (row != null)
			{
				sheet.ShiftRows(targetRowIndex, sheet.LastRowNum, 1);
			}
			else
			{
				row = sheet.CreateRow(targetRowIndex);
			}
			for (int i = row2.FirstCellNum; i < row2.LastCellNum; i++)
			{
				ICell cell = row2.GetCell(i);
				if (cell != null)
				{
					ICell cell2 = row.CreateCell(i);
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
				}
			}
			for (int j = 0; j < sheet.NumMergedRegions; j++)
			{
				CellRangeAddress mergedRegion = sheet.GetMergedRegion(j);
				if (mergedRegion.FirstRow == row2.RowNum)
				{
					CellRangeAddress region = new CellRangeAddress(row.RowNum, row.RowNum + (mergedRegion.LastRow - mergedRegion.FirstRow), mergedRegion.FirstColumn, mergedRegion.LastColumn);
					sheet.AddMergedRegion(region);
				}
			}
			return row;
		}

		/// Compute width of a single cell
		///
		/// @param cell the cell whose width is to be calculated
		/// @param defaultCharWidth the width of a single character
		/// @param formatter formatter used to prepare the text to be measured
		/// @param useMergedCells    whether to use merged cells
		/// @return  the width in pixels
		public static double GetCellWidth(ICell cell, int defaultCharWidth, DataFormatter formatter, bool useMergedCells)
		{
			ISheet sheet = cell.Sheet;
			IWorkbook workbook = sheet.Workbook;
			IRow row = cell.Row;
			int columnIndex = cell.ColumnIndex;
			int num = 1;
			for (int i = 0; i < sheet.NumMergedRegions; i++)
			{
				CellRangeAddress mergedRegion = sheet.GetMergedRegion(i);
				if (ContainsCell(mergedRegion, row.RowNum, columnIndex))
				{
					if (!useMergedCells)
					{
						return -1.0;
					}
					cell = row.GetCell(mergedRegion.FirstColumn);
					num = 1 + mergedRegion.LastColumn - mergedRegion.FirstColumn;
				}
			}
			ICellStyle cellStyle = cell.CellStyle;
			CellType cellType = cell.CellType;
			IFont fontAt = workbook.GetFontAt(0);
			Font font = IFont2Font(fontAt);
			if (cellType == CellType.Formula)
			{
				cellType = cell.CachedFormulaResultType;
			}
			IFont fontAt2 = workbook.GetFontAt(cellStyle.FontIndex);
			double num2 = -1.0;
			using (Bitmap image = new Bitmap(2048, 100))
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					if (cellType != CellType.String)
					{
						string text = null;
						switch (cellType)
						{
						case CellType.Numeric:
							try
							{
								text = formatter.FormatCellValue(cell, dummyEvaluator);
							}
							catch (Exception)
							{
								text = cell.NumericCellValue.ToString();
							}
							break;
						case CellType.Boolean:
							text = cell.BooleanCellValue.ToString().ToUpper();
							break;
						}
						if (text != null)
						{
							string text2 = text + defaultChar;
							font = IFont2Font(fontAt2);
							if (cellStyle.Rotation != 0)
							{
								double num3 = (double)cellStyle.Rotation * 2.0 * 3.1415926535897931 / 360.0;
								SizeF sizeF = graphics.MeasureString(text2, font);
								double num4 = (double)sizeF.Height * Math.Sin(num3);
								double num5 = (double)sizeF.Width * Math.Cos(num3);
								double num6 = Math.Round(num4 + num5, 0, MidpointRounding.ToEven);
								return Math.Max(num2, num6 / (double)num / (double)defaultCharWidth * 2.0 + (double)cell.CellStyle.Indention);
							}
							double num7 = Math.Round((double)graphics.MeasureString(text2, font).Width, 0, MidpointRounding.ToEven);
							return Math.Max(num2, num7 * 1.0 / (double)num / (double)defaultCharWidth * 2.0 + (double)cell.CellStyle.Indention);
						}
						return num2;
					}
					IRichTextString richStringCellValue = cell.RichStringCellValue;
					string[] array = richStringCellValue.String.Split("\n".ToCharArray());
					for (int j = 0; j < array.Length; j++)
					{
						string text3 = array[j] + defaultChar;
						font = IFont2Font(fontAt2);
						int numFormattingRun = richStringCellValue.NumFormattingRuns;
						if (cellStyle.Rotation != 0)
						{
							double num8 = (double)cellStyle.Rotation * 2.0 * 3.1415926535897931 / 360.0;
							SizeF sizeF2 = graphics.MeasureString(text3, font);
							double num9 = Math.Abs((double)sizeF2.Height * Math.Sin(num8));
							double num10 = Math.Abs((double)sizeF2.Width * Math.Cos(num8));
							double num11 = Math.Round(num9 + num10, 0, MidpointRounding.ToEven);
							num2 = Math.Max(num2, num11 / (double)num / (double)defaultCharWidth * 2.0 + (double)cell.CellStyle.Indention);
						}
						else
						{
							double num12 = Math.Round((double)graphics.MeasureString(text3, font).Width, 0, MidpointRounding.ToEven);
							num2 = Math.Max(num2, num12 / (double)num / (double)defaultCharWidth * 2.0 + (double)cell.CellStyle.Indention);
						}
					}
					return num2;
				}
			}
		}

		/// Compute width of a column and return the result
		///
		/// @param sheet the sheet to calculate
		/// @param column    0-based index of the column
		/// @param useMergedCells    whether to use merged cells
		/// @return  the width in pixels
		public static double GetColumnWidth(ISheet sheet, int column, bool useMergedCells)
		{
			IWorkbook workbook = sheet.Workbook;
			DataFormatter formatter = new DataFormatter();
			IFont fontAt = workbook.GetFontAt(0);
			Font font = IFont2Font(fontAt);
			int width = TextRenderer.MeasureText(new string(defaultChar, 1) ?? "", font).Width;
			double num = -1.0;
			foreach (IRow item in sheet)
			{
				ICell cell = item.GetCell(column);
				if (cell != null)
				{
					double cellWidth = GetCellWidth(cell, width, formatter, useMergedCells);
					num = Math.Max(num, cellWidth);
				}
			}
			return num;
		}

		/// Compute width of a column based on a subset of the rows and return the result
		///
		/// @param sheet the sheet to calculate
		/// @param column    0-based index of the column
		/// @param useMergedCells    whether to use merged cells
		/// @param firstRow  0-based index of the first row to consider (inclusive)
		/// @param lastRow   0-based index of the last row to consider (inclusive)
		/// @return  the width in pixels
		public static double GetColumnWidth(ISheet sheet, int column, bool useMergedCells, int firstRow, int lastRow)
		{
			IWorkbook workbook = sheet.Workbook;
			DataFormatter formatter = new DataFormatter();
			IFont fontAt = workbook.GetFontAt(0);
			Font font = IFont2Font(fontAt);
			int width = TextRenderer.MeasureText(new string(defaultChar, 1) ?? "", font).Width;
			double num = -1.0;
			for (int i = firstRow; i <= lastRow; i++)
			{
				IRow row = sheet.GetRow(i);
				if (row != null)
				{
					ICell cell = row.GetCell(column);
					if (cell != null)
					{
						double cellWidth = GetCellWidth(cell, width, formatter, useMergedCells);
						num = Math.Max(num, cellWidth);
					}
				}
			}
			return num;
		}

		/// <summary>
		/// Convert HSSFFont to Font.
		/// </summary>
		/// <param name="font1">The font.</param>
		/// <returns></returns>
		internal static Font IFont2Font(IFont font1)
		{
			FontStyle fontStyle = FontStyle.Regular;
			if (font1.Boldweight == 700)
			{
				fontStyle |= FontStyle.Bold;
			}
			if (font1.IsItalic)
			{
				fontStyle |= FontStyle.Italic;
			}
			if (font1.Underline == FontUnderlineType.Single)
			{
				fontStyle |= FontStyle.Underline;
			}
			return new Font(font1.FontName, (float)font1.FontHeightInPoints, fontStyle, GraphicsUnit.Point);
		}

		public static bool ContainsCell(CellRangeAddress cr, int rowIx, int colIx)
		{
			if (cr.FirstRow <= rowIx && cr.LastRow >= rowIx && cr.FirstColumn <= colIx && cr.LastColumn >= colIx)
			{
				return true;
			}
			return false;
		}

		/// Generate a valid sheet name based on the existing one. Used when cloning sheets.
		///
		/// @param srcName the original sheet name to
		/// @return clone sheet name
		public static string GetUniqueSheetName(IWorkbook wb, string srcName)
		{
			if (wb.GetSheetIndex(srcName) == -1)
			{
				return srcName;
			}
			int num = 2;
			string text = srcName;
			int num2 = srcName.LastIndexOf('(');
			if (num2 > 0 && srcName.EndsWith(")"))
			{
				string text2 = srcName.Substring(num2 + 1, srcName.Length - num2 - 2);
				try
				{
					num = int.Parse(text2.Trim());
					num++;
					text = srcName.Substring(0, num2).Trim();
				}
				catch (FormatException)
				{
				}
			}
			string text4;
			do
			{
				string text3 = num++.ToString();
				text4 = ((text.Length + text3.Length + 2 >= 31) ? (text.Substring(0, 31 - text3.Length - 2) + "(" + text3 + ")") : (text + " (" + text3 + ")"));
			}
			while (wb.GetSheetIndex(text4) != -1);
			return text4;
		}
	}
}
