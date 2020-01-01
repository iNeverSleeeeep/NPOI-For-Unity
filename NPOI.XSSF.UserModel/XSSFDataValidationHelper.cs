using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace NPOI.XSSF.UserModel
{
	/// @author <a href="rjankiraman@emptoris.com">Radhakrishnan J</a>
	public class XSSFDataValidationHelper : IDataValidationHelper
	{
		private XSSFSheet xssfSheet;

		public XSSFDataValidationHelper(XSSFSheet xssfSheet)
		{
			this.xssfSheet = xssfSheet;
		}

		public IDataValidationConstraint CreateDateConstraint(int operatorType, string formula1, string formula2, string dateFormat)
		{
			return new XSSFDataValidationConstraint(4, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateDecimalConstraint(int operatorType, string formula1, string formula2)
		{
			return new XSSFDataValidationConstraint(2, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateExplicitListConstraint(string[] listOfValues)
		{
			return new XSSFDataValidationConstraint(listOfValues);
		}

		public IDataValidationConstraint CreateFormulaListConstraint(string listFormula)
		{
			return new XSSFDataValidationConstraint(3, listFormula);
		}

		public IDataValidationConstraint CreateNumericConstraint(int validationType, int operatorType, string formula1, string formula2)
		{
			switch (validationType)
			{
			case 1:
				return CreateintConstraint(operatorType, formula1, formula2);
			case 2:
				return CreateDecimalConstraint(operatorType, formula1, formula2);
			case 6:
				return CreateTextLengthConstraint(operatorType, formula1, formula2);
			default:
				return null;
			}
		}

		public IDataValidationConstraint CreateintConstraint(int operatorType, string formula1, string formula2)
		{
			return new XSSFDataValidationConstraint(1, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateTextLengthConstraint(int operatorType, string formula1, string formula2)
		{
			return new XSSFDataValidationConstraint(6, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateTimeConstraint(int operatorType, string formula1, string formula2)
		{
			return new XSSFDataValidationConstraint(5, operatorType, formula1, formula2);
		}

		public IDataValidationConstraint CreateCustomConstraint(string formula)
		{
			return new XSSFDataValidationConstraint(7, formula);
		}

		public IDataValidation CreateValidation(IDataValidationConstraint constraint, CellRangeAddressList cellRangeAddressList)
		{
			XSSFDataValidationConstraint constraint2 = (XSSFDataValidationConstraint)constraint;
			CT_DataValidation cT_DataValidation = new CT_DataValidation();
			int validationType = constraint.GetValidationType();
			switch (validationType)
			{
			case 3:
				cT_DataValidation.type = ST_DataValidationType.list;
				cT_DataValidation.formula1 = constraint.Formula1;
				break;
			case 0:
				cT_DataValidation.type = ST_DataValidationType.none;
				break;
			case 6:
				cT_DataValidation.type = ST_DataValidationType.textLength;
				break;
			case 4:
				cT_DataValidation.type = ST_DataValidationType.date;
				break;
			case 1:
				cT_DataValidation.type = ST_DataValidationType.whole;
				break;
			case 2:
				cT_DataValidation.type = ST_DataValidationType.@decimal;
				break;
			case 5:
				cT_DataValidation.type = ST_DataValidationType.time;
				break;
			case 7:
				cT_DataValidation.type = ST_DataValidationType.custom;
				break;
			default:
				cT_DataValidation.type = ST_DataValidationType.none;
				break;
			}
			if (validationType != 0 && validationType != 3)
			{
				cT_DataValidation.@operator = ST_DataValidationOperator.between;
				if (XSSFDataValidation.operatorTypeMappings.ContainsKey(constraint.Operator))
				{
					cT_DataValidation.@operator = XSSFDataValidation.operatorTypeMappings[constraint.Operator];
				}
				if (constraint.Formula1 != null)
				{
					cT_DataValidation.formula1 = constraint.Formula1;
				}
				if (constraint.Formula2 != null)
				{
					cT_DataValidation.formula2 = constraint.Formula2;
				}
			}
			CellRangeAddress[] cellRangeAddresses = cellRangeAddressList.CellRangeAddresses;
			string text = string.Empty;
			foreach (CellRangeAddress cellRangeAddress in cellRangeAddresses)
			{
				text = ((text.Length != 0) ? (" " + cellRangeAddress.FormatAsString()) : cellRangeAddress.FormatAsString());
			}
			cT_DataValidation.sqref = text;
			return new XSSFDataValidation(constraint2, cellRangeAddressList, cT_DataValidation);
		}
	}
}
