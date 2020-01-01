using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.XSSF.UserModel
{
	/// @author <a href="rjankiraman@emptoris.com">Radhakrishnan J</a>
	public class XSSFDataValidationConstraint : IDataValidationConstraint
	{
		private string formula1;

		private string formula2;

		private int validationType = -1;

		private int operator1 = -1;

		private string[] explicitListOfValues;

		public string[] ExplicitListValues
		{
			get
			{
				return explicitListOfValues;
			}
			set
			{
				explicitListOfValues = value;
				if (explicitListOfValues != null && explicitListOfValues.Length > 0)
				{
					StringBuilder stringBuilder = new StringBuilder("\"");
					foreach (string value2 in value)
					{
						if (stringBuilder.Length > 1)
						{
							stringBuilder.Append(",");
						}
						stringBuilder.Append(value2);
					}
					stringBuilder.Append("\"");
					Formula1 = stringBuilder.ToString();
				}
			}
		}

		public string Formula1
		{
			get
			{
				return formula1;
			}
			set
			{
				formula1 = RemoveLeadingEquals(value);
			}
		}

		public string Formula2
		{
			get
			{
				return formula2;
			}
			set
			{
				formula2 = RemoveLeadingEquals(value);
			}
		}

		public int Operator
		{
			get
			{
				return operator1;
			}
			set
			{
				operator1 = value;
			}
		}

		public XSSFDataValidationConstraint(string[] explicitListOfValues)
		{
			if (explicitListOfValues == null || explicitListOfValues.Length == 0)
			{
				throw new ArgumentException("List validation with explicit values must specify at least one value");
			}
			validationType = 3;
			ExplicitListValues = explicitListOfValues;
			Validate();
		}

		public XSSFDataValidationConstraint(int validationType, string formula1)
		{
			Formula1 = formula1;
			this.validationType = validationType;
			Validate();
		}

		public XSSFDataValidationConstraint(int validationType, int operator1, string formula1)
		{
			Formula1 = formula1;
			this.validationType = validationType;
			this.operator1 = operator1;
			Validate();
		}

		public XSSFDataValidationConstraint(int validationType, int operator1, string formula1, string formula2)
		{
			Formula1 = formula1;
			Formula2 = formula2;
			this.validationType = validationType;
			this.operator1 = operator1;
			Validate();
			if (3 == validationType)
			{
				explicitListOfValues = formula1.Split(',');
			}
		}

		public int GetValidationType()
		{
			return validationType;
		}

		protected string RemoveLeadingEquals(string formula1)
		{
			if (!IsFormulaEmpty(formula1))
			{
				if (formula1[0] != '=')
				{
					return formula1;
				}
				return formula1.Substring(1);
			}
			return formula1;
		}

		public void Validate()
		{
			if (validationType != 0)
			{
				if (validationType == 3)
				{
					if (IsFormulaEmpty(formula1))
					{
						throw new ArgumentException("A valid formula or a list of values must be specified for list validation.");
					}
				}
				else
				{
					if (IsFormulaEmpty(formula1))
					{
						throw new ArgumentException("Formula is not specified. Formula is required for all validation types except explicit list validation.");
					}
					if (validationType != 7)
					{
						if (operator1 == -1)
						{
							throw new ArgumentException("This validation type requires an operator to be specified.");
						}
						if ((operator1 == 0 || operator1 == 1) && IsFormulaEmpty(formula2))
						{
							throw new ArgumentException("Between and not between comparisons require two formulae to be specified.");
						}
					}
				}
			}
		}

		protected bool IsFormulaEmpty(string formula1)
		{
			if (formula1 != null)
			{
				return formula1.Trim().Length == 0;
			}
			return true;
		}

		public string PrettyPrint()
		{
			StringBuilder stringBuilder = new StringBuilder();
			ST_DataValidationType sT_DataValidationType = XSSFDataValidation.validationTypeMappings[validationType];
			Enum value = XSSFDataValidation.operatorTypeMappings[operator1];
			stringBuilder.Append(sT_DataValidationType);
			stringBuilder.Append(' ');
			if (validationType != 0)
			{
				if (validationType != 3 && validationType != 0 && validationType != 7)
				{
					stringBuilder.Append(",").Append(value).Append(", ");
				}
				string value2 = "";
				if (validationType == 3 && explicitListOfValues != null)
				{
					stringBuilder.Append(value2).Append(Arrays.AsList(explicitListOfValues)).Append(value2)
						.Append(' ');
				}
				else
				{
					stringBuilder.Append(value2).Append(formula1).Append(value2)
						.Append(' ');
				}
				if (formula2 != null)
				{
					stringBuilder.Append(value2).Append(formula2).Append(value2)
						.Append(' ');
				}
			}
			return stringBuilder.ToString();
		}
	}
}
