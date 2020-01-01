using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace NPOI.HSSF.UserModel
{
	/// @author <a href="rjankiraman@emptoris.com">Radhakrishnan J</a>
	public class HSSFDataValidationHelper : IDataValidationHelper
	{
		private HSSFSheet sheet;

		public HSSFDataValidationHelper(HSSFSheet sheet)
		{
			this.sheet = sheet;
		}

		public IDataValidationConstraint CreateDateConstraint(int operatorType, string formula1, string formula2, string dateFormat)
		{
			return DVConstraint.CreateDateConstraint(operatorType, formula1, formula2, dateFormat);
		}

		public IDataValidationConstraint CreateExplicitListConstraint(string[] listOfValues)
		{
			return DVConstraint.CreateExplicitListConstraint(listOfValues);
		}

		public IDataValidationConstraint CreateFormulaListConstraint(string listFormula)
		{
			return DVConstraint.CreateFormulaListConstraint(listFormula);
		}

		public IDataValidationConstraint CreateNumericConstraint(int validationType, int operatorType, string formula1, string formula2)
		{
			return DVConstraint.CreateNumericConstraint(validationType, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateintConstraint(int operatorType, string formula1, string formula2)
		{
			return DVConstraint.CreateNumericConstraint(1, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateDecimalConstraint(int operatorType, string formula1, string formula2)
		{
			return DVConstraint.CreateNumericConstraint(2, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateTextLengthConstraint(int operatorType, string formula1, string formula2)
		{
			return DVConstraint.CreateNumericConstraint(6, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateTimeConstraint(int operatorType, string formula1, string formula2)
		{
			return DVConstraint.CreateTimeConstraint(operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateCustomConstraint(string formula)
		{
			return DVConstraint.CreateCustomFormulaConstraint(formula);
		}

		public IDataValidation CreateValidation(IDataValidationConstraint constraint, CellRangeAddressList cellRangeAddressList)
		{
			return new HSSFDataValidation(cellRangeAddressList, constraint);
		}
	}
}
