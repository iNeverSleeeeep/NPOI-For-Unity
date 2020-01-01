using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;

namespace NPOI.XSSF.UserModel.Helpers
{
	/// Utility to update formulas and named ranges when a sheet name was Changed
	///
	/// @author Yegor Kozlov
	public class XSSFFormulaUtils
	{
		private XSSFWorkbook _wb;

		private XSSFEvaluationWorkbook _fpwb;

		public XSSFFormulaUtils(XSSFWorkbook wb)
		{
			_wb = wb;
			_fpwb = XSSFEvaluationWorkbook.Create(_wb);
		}

		/// Update sheet name in all formulas and named ranges.
		/// Called from {@link XSSFWorkbook#SetSheetName(int, String)}
		/// <p />
		/// <p>
		/// The idea is to parse every formula and render it back to string
		/// with the updated sheet name. The IFormulaParsingWorkbook passed to the formula Parser
		/// is constructed from the old workbook (sheet name is not yet updated) and
		/// the FormulaRenderingWorkbook passed to FormulaRenderer#toFormulaString is a custom implementation that
		/// returns the new sheet name.
		/// </p>
		///
		/// @param sheetIndex the 0-based index of the sheet being Changed
		/// @param name       the new sheet name
		public void UpdateSheetName(int sheetIndex, string name)
		{
			IFormulaRenderingWorkbook frwb = new XSSFFormulaRenderingWorkbook(_fpwb, sheetIndex, name);
			for (int i = 0; i < _wb.NumberOfNames; i++)
			{
				IName nameAt = _wb.GetNameAt(i);
				if (nameAt.SheetIndex == -1 || nameAt.SheetIndex == sheetIndex)
				{
					UpdateName(nameAt, frwb);
				}
			}
			foreach (ISheet item in _wb)
			{
				foreach (IRow item2 in item)
				{
					foreach (ICell item3 in item2)
					{
						if (item3.CellType == CellType.Formula)
						{
							UpdateFormula((XSSFCell)item3, frwb);
						}
					}
				}
			}
		}

		/// Parse cell formula and re-assemble it back using the specified FormulaRenderingWorkbook.
		///
		/// @param cell the cell to update
		/// @param frwb the formula rendering workbbok that returns new sheet name
		private void UpdateFormula(XSSFCell cell, IFormulaRenderingWorkbook frwb)
		{
			CT_CellFormula f = cell.GetCTCell().f;
			if (f != null)
			{
				string value = f.Value;
				if (value != null && value.Length > 0)
				{
					int sheetIndex = _wb.GetSheetIndex(cell.Sheet);
					Ptg[] ptgs = FormulaParser.Parse(value, _fpwb, FormulaType.Cell, sheetIndex);
					string value2 = FormulaRenderer.ToFormulaString(frwb, ptgs);
					if (!value.Equals(value2))
					{
						f.Value = value2;
					}
				}
			}
		}

		/// Parse formula in the named range and re-assemble it  back using the specified FormulaRenderingWorkbook.
		///
		/// @param name the name to update
		/// @param frwb the formula rendering workbbok that returns new sheet name
		private void UpdateName(IName name, IFormulaRenderingWorkbook frwb)
		{
			string refersToFormula = name.RefersToFormula;
			if (refersToFormula != null)
			{
				int sheetIndex = name.SheetIndex;
				Ptg[] ptgs = FormulaParser.Parse(refersToFormula, _fpwb, FormulaType.NamedRange, sheetIndex);
				string text = FormulaRenderer.ToFormulaString(frwb, ptgs);
				if (!refersToFormula.Equals(text))
				{
					name.RefersToFormula = text;
				}
			}
		}
	}
}
