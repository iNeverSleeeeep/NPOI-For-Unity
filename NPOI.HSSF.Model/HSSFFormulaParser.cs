using NPOI.HSSF.UserModel;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;

namespace NPOI.HSSF.Model
{
	/// HSSF wrapper for the {@link FormulaParser} and {@link FormulaRenderer} 
	///
	/// @author Josh Micich
	public class HSSFFormulaParser
	{
		private static IFormulaParsingWorkbook CreateParsingWorkbook(HSSFWorkbook book)
		{
			return HSSFEvaluationWorkbook.Create(book);
		}

		private HSSFFormulaParser()
		{
		}

		/// Convenience method for parsing cell formulas. see {@link #parse(String, HSSFWorkbook, int)}
		public static Ptg[] Parse(string formula, HSSFWorkbook workbook)
		{
			return FormulaParser.Parse(formula, CreateParsingWorkbook(workbook));
		}

		/// @param formulaType a constant from {@link FormulaType}
		/// @return the parsed formula tokens
		public static Ptg[] Parse(string formula, HSSFWorkbook workbook, FormulaType formulaType)
		{
			return FormulaParser.Parse(formula, CreateParsingWorkbook(workbook), formulaType);
		}

		/// @param formula     the formula to parse
		/// @param workbook    the parent workbook
		/// @param formulaType a constant from {@link FormulaType}
		/// @param sheetIndex  the 0-based index of the sheet this formula belongs to.
		/// The sheet index is required to resolve sheet-level names. <code>-1</code> means that
		/// the scope of the name will be ignored and  the parser will match named ranges only by name
		///
		/// @return the parsed formula tokens
		public static Ptg[] Parse(string formula, HSSFWorkbook workbook, FormulaType formulaType, int sheetIndex)
		{
			return FormulaParser.Parse(formula, CreateParsingWorkbook(workbook), formulaType, sheetIndex);
		}

		/// Static method to convert an array of {@link Ptg}s in RPN order
		/// to a human readable string format in infix mode.
		/// @param book  used for defined names and 3D references
		/// @param ptgs  must not be <c>null</c>
		/// @return a human readable String
		public static string ToFormulaString(HSSFWorkbook book, Ptg[] ptgs)
		{
			return FormulaRenderer.ToFormulaString(HSSFEvaluationWorkbook.Create(book), ptgs);
		}
	}
}
