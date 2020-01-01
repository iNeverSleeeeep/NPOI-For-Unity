using NPOI.SS.Util;

namespace NPOI.SS.UserModel
{
	/// @author <a href="rjankiraman@emptoris.com">Radhakrishnan J</a>
	public interface IDataValidationHelper
	{
		IDataValidationConstraint CreateFormulaListConstraint(string listFormula);

		IDataValidationConstraint CreateExplicitListConstraint(string[] listOfValues);

		IDataValidationConstraint CreateNumericConstraint(int validationType, int operatorType, string formula1, string formula2);

		IDataValidationConstraint CreateTextLengthConstraint(int operatorType, string formula1, string formula2);

		IDataValidationConstraint CreateDecimalConstraint(int operatorType, string formula1, string formula2);

		IDataValidationConstraint CreateintConstraint(int operatorType, string formula1, string formula2);

		IDataValidationConstraint CreateDateConstraint(int operatorType, string formula1, string formula2, string dateFormat);

		IDataValidationConstraint CreateTimeConstraint(int operatorType, string formula1, string formula2);

		IDataValidationConstraint CreateCustomConstraint(string formula);

		IDataValidation CreateValidation(IDataValidationConstraint constraint, CellRangeAddressList cellRangeAddressList);
	}
}
