using NPOI.SS.UserModel;
using NPOI.Util;
using System;

namespace NPOI.SS.Util
{
	/// Class {@code SheetBuilder} provides an easy way of building workbook sheets
	/// from 2D array of Objects. It can be used in test cases to improve code
	/// readability or in Swing applications with tables.
	///
	/// @author Roman Kashitsyn
	public class SheetBuilder
	{
		private IWorkbook workbook;

		private object[][] cells;

		private bool shouldCreateEmptyCells;

		private string sheetName;

		public SheetBuilder(IWorkbook workbook, object[][] cells)
		{
			this.workbook = workbook;
			this.cells = cells;
		}

		/// Returns {@code true} if null array elements should be treated as empty
		/// cells.
		///
		/// @return {@code true} if null objects should be treated as empty cells
		///         and {@code false} otherwise
		public bool GetCreateEmptyCells()
		{
			return shouldCreateEmptyCells;
		}

		/// Specifies if null array elements should be treated as empty cells.
		///
		/// @param shouldCreateEmptyCells {@code true} if null array elements should be
		///                               treated as empty cells
		/// @return {@code this}
		public SheetBuilder SetCreateEmptyCells(bool shouldCreateEmptyCells)
		{
			this.shouldCreateEmptyCells = shouldCreateEmptyCells;
			return this;
		}

		/// Specifies name of the sheet to build. If not specified, default name (provided by
		/// workbook) will be used instead.
		/// @param sheetName sheet name to use
		/// @return {@code this}
		public SheetBuilder SetSheetName(string sheetName)
		{
			this.sheetName = sheetName;
			return this;
		}

		/// Builds sheet from parent workbook and 2D array with cell
		/// values. Creates rows anyway (even if row contains only null
		/// cells), creates cells if either corresponding array value is not
		/// null or createEmptyCells property is true.
		/// The conversion is performed in the following way:
		/// <p />
		/// <ul>
		/// <li>Numbers become numeric cells.</li>
		/// <li><code>java.util.Date</code> or <code>java.util.Calendar</code>
		/// instances become date cells.</li>
		/// <li>String with leading '=' char become formulas (leading '='
		/// will be truncated).</li>
		/// <li>Other objects become strings via <code>Object.toString()</code>
		/// method call.</li>
		/// </ul>
		///
		/// @return newly created sheet
		public ISheet Build()
		{
			ISheet sheet = (sheetName == null) ? workbook.CreateSheet() : workbook.CreateSheet(sheetName);
			IRow row = null;
			ICell cell = null;
			for (int i = 0; i < cells.Length; i++)
			{
				object[] array = cells[i];
				row = sheet.CreateRow(i);
				for (int j = 0; j < array.Length; j++)
				{
					object obj = array[j];
					if (obj != null || shouldCreateEmptyCells)
					{
						cell = row.CreateCell(j);
						SetCellValue(cell, obj);
					}
				}
			}
			return sheet;
		}

		/// Sets the cell value using object type information.
		/// @param cell cell to change
		/// @param value value to set
		private void SetCellValue(ICell cell, object value)
		{
			if (value != null && cell != null)
			{
				if (Number.IsNumber(value))
				{
					double.TryParse(value.ToString(), out double result);
					cell.SetCellValue(result);
				}
				else if (value is DateTime)
				{
					cell.SetCellValue((DateTime)value);
				}
				else if (IsFormulaDefinition(value))
				{
					cell.CellFormula = GetFormula(value);
				}
				else
				{
					cell.SetCellValue(value.ToString());
				}
			}
		}

		private bool IsFormulaDefinition(object obj)
		{
			if (obj is string)
			{
				string text = (string)obj;
				if (text.Length < 2)
				{
					return false;
				}
				return ((string)obj)[0] == '=';
			}
			return false;
		}

		private string GetFormula(object obj)
		{
			return ((string)obj).Substring(1);
		}
	}
}
