using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;

namespace NPOI.XSSF.UserModel.Helpers
{
	/// @author Yegor Kozlov
	public class XSSFRowShifter
	{
		private XSSFSheet sheet;

		public XSSFRowShifter(XSSFSheet sh)
		{
			sheet = sh;
		}

		/// Shift merged regions
		///
		/// @param startRow the row to start Shifting
		/// @param endRow   the row to end Shifting
		/// @param n        the number of rows to shift
		/// @return an array of affected cell regions
		public List<CellRangeAddress> ShiftMerged(int startRow, int endRow, int n)
		{
			List<CellRangeAddress> list = new List<CellRangeAddress>();
			for (int i = 0; i < sheet.NumMergedRegions; i++)
			{
				CellRangeAddress mergedRegion = sheet.GetMergedRegion(i);
				bool flag = mergedRegion.FirstRow >= startRow || mergedRegion.LastRow >= startRow;
				bool flag2 = mergedRegion.FirstRow <= endRow || mergedRegion.LastRow <= endRow;
				if (flag && flag2 && !ContainsCell(mergedRegion, startRow - 1, 0) && !ContainsCell(mergedRegion, endRow + 1, 0))
				{
					mergedRegion.FirstRow += n;
					mergedRegion.LastRow += n;
					list.Add(mergedRegion);
					sheet.RemoveMergedRegion(i);
					i--;
				}
			}
			foreach (CellRangeAddress item in list)
			{
				sheet.AddMergedRegion(item);
			}
			return list;
		}

		/// Check if the  row and column are in the specified cell range
		///
		/// @param cr    the cell range to check in
		/// @param rowIx the row to check
		/// @param colIx the column to check
		/// @return true if the range Contains the cell [rowIx,colIx]
		private static bool ContainsCell(CellRangeAddress cr, int rowIx, int colIx)
		{
			if (cr.FirstRow <= rowIx && cr.LastRow >= rowIx && cr.FirstColumn <= colIx && cr.LastColumn >= colIx)
			{
				return true;
			}
			return false;
		}

		/// Updated named ranges
		public void UpdateNamedRanges(FormulaShifter shifter)
		{
			IWorkbook workbook = sheet.Workbook;
			XSSFEvaluationWorkbook xSSFEvaluationWorkbook = XSSFEvaluationWorkbook.Create(workbook);
			for (int i = 0; i < workbook.NumberOfNames; i++)
			{
				IName nameAt = workbook.GetNameAt(i);
				string refersToFormula = nameAt.RefersToFormula;
				int sheetIndex = nameAt.SheetIndex;
				Ptg[] ptgs = FormulaParser.Parse(refersToFormula, xSSFEvaluationWorkbook, FormulaType.NamedRange, sheetIndex);
				if (shifter.AdjustFormula(ptgs, sheetIndex))
				{
					string text2 = nameAt.RefersToFormula = FormulaRenderer.ToFormulaString(xSSFEvaluationWorkbook, ptgs);
				}
			}
		}

		/// Update formulas.
		public void UpdateFormulas(FormulaShifter shifter)
		{
			UpdateSheetFormulas(sheet, shifter);
			IWorkbook workbook = sheet.Workbook;
			foreach (XSSFSheet item in workbook)
			{
				if (sheet != item)
				{
					UpdateSheetFormulas(item, shifter);
				}
			}
		}

		private void UpdateSheetFormulas(XSSFSheet sh, FormulaShifter Shifter)
		{
			foreach (IRow item in sh)
			{
				XSSFRow row2 = (XSSFRow)item;
				updateRowFormulas(row2, Shifter);
			}
		}

		private void updateRowFormulas(XSSFRow row, FormulaShifter Shifter)
		{
			foreach (ICell item in row)
			{
				XSSFCell xSSFCell = (XSSFCell)item;
				CT_Cell cTCell = xSSFCell.GetCTCell();
				if (cTCell.IsSetF())
				{
					CT_CellFormula f = cTCell.f;
					string value = f.Value;
					if (value.Length > 0)
					{
						string text = ShiftFormula(row, value, Shifter);
						if (text != null)
						{
							f.Value = text;
							if (f.t == ST_CellFormulaType.shared)
							{
								int si = (int)f.si;
								CT_CellFormula sharedFormula = ((XSSFSheet)row.Sheet).GetSharedFormula(si);
								sharedFormula.Value = text;
							}
						}
					}
					if (f.isSetRef())
					{
						string @ref = f.@ref;
						string text2 = ShiftFormula(row, @ref, Shifter);
						if (text2 != null)
						{
							f.@ref = text2;
						}
					}
				}
			}
		}

		/// Shift a formula using the supplied FormulaShifter
		///
		/// @param row     the row of the cell this formula belongs to. Used to get a reference to the parent workbook.
		/// @param formula the formula to shift
		/// @param Shifter the FormulaShifter object that operates on the Parsed formula tokens
		/// @return the Shifted formula if the formula was Changed,
		///         <code>null</code> if the formula wasn't modified
		private static string ShiftFormula(XSSFRow row, string formula, FormulaShifter Shifter)
		{
			ISheet sheet = row.Sheet;
			IWorkbook workbook = sheet.Workbook;
			int sheetIndex = workbook.GetSheetIndex(sheet);
			XSSFEvaluationWorkbook xSSFEvaluationWorkbook = XSSFEvaluationWorkbook.Create(workbook);
			Ptg[] ptgs = FormulaParser.Parse(formula, xSSFEvaluationWorkbook, FormulaType.Cell, sheetIndex);
			string result = null;
			if (Shifter.AdjustFormula(ptgs, sheetIndex))
			{
				result = FormulaRenderer.ToFormulaString(xSSFEvaluationWorkbook, ptgs);
			}
			return result;
		}

		public void UpdateConditionalFormatting(FormulaShifter Shifter)
		{
			IWorkbook workbook = sheet.Workbook;
			int sheetIndex = workbook.GetSheetIndex(sheet);
			XSSFEvaluationWorkbook xSSFEvaluationWorkbook = XSSFEvaluationWorkbook.Create(workbook);
			List<CT_ConditionalFormatting> conditionalFormatting = sheet.GetCTWorksheet().conditionalFormatting;
			for (int i = 0; conditionalFormatting != null && i < conditionalFormatting.Count; i++)
			{
				CT_ConditionalFormatting cT_ConditionalFormatting = conditionalFormatting[i];
				List<CellRangeAddress> list = new List<CellRangeAddress>();
				string[] array = cT_ConditionalFormatting.sqref.ToString().Split(' ');
				for (int j = 0; j < array.Length; j++)
				{
					list.Add(CellRangeAddress.ValueOf(array[j]));
				}
				bool flag = false;
				List<CellRangeAddress> list2 = new List<CellRangeAddress>();
				for (int k = 0; k < list.Count; k++)
				{
					CellRangeAddress cellRangeAddress = list[k];
					CellRangeAddress cellRangeAddress2 = ShiftRange(Shifter, cellRangeAddress, sheetIndex);
					if (cellRangeAddress2 == null)
					{
						flag = true;
					}
					else
					{
						list2.Add(cellRangeAddress2);
						if (cellRangeAddress2 != cellRangeAddress)
						{
							flag = true;
						}
					}
				}
				if (flag)
				{
					if (list2.Count == 0)
					{
						conditionalFormatting.RemoveAt(i);
						continue;
					}
					string text = string.Empty;
					foreach (CellRangeAddress item in list2)
					{
						text = ((text.Length != 0) ? (text + " " + item.FormatAsString()) : item.FormatAsString());
					}
					cT_ConditionalFormatting.sqref = text;
				}
				foreach (CT_CfRule item2 in cT_ConditionalFormatting.cfRule)
				{
					List<string> formula = item2.formula;
					for (int l = 0; l < formula.Count; l++)
					{
						string formula2 = formula[l];
						Ptg[] ptgs = FormulaParser.Parse(formula2, xSSFEvaluationWorkbook, FormulaType.Cell, sheetIndex);
						if (Shifter.AdjustFormula(ptgs, sheetIndex))
						{
							string text3 = formula[l] = FormulaRenderer.ToFormulaString(xSSFEvaluationWorkbook, ptgs);
						}
					}
				}
			}
		}

		private static CellRangeAddress ShiftRange(FormulaShifter Shifter, CellRangeAddress cra, int currentExternSheetIx)
		{
			AreaPtg areaPtg = new AreaPtg(cra.FirstRow, cra.LastRow, cra.FirstColumn, cra.LastColumn, false, false, false, false);
			Ptg[] array = new Ptg[1]
			{
				areaPtg
			};
			if (!Shifter.AdjustFormula(array, currentExternSheetIx))
			{
				return cra;
			}
			Ptg ptg = array[0];
			if (ptg is AreaPtg)
			{
				AreaPtg areaPtg2 = (AreaPtg)ptg;
				return new CellRangeAddress(areaPtg2.FirstRow, areaPtg2.LastRow, areaPtg2.FirstColumn, areaPtg2.LastColumn);
			}
			if (ptg is AreaErrPtg)
			{
				return null;
			}
			throw new InvalidOperationException("Unexpected Shifted ptg class (" + ptg.GetType().Name + ")");
		}
	}
}
