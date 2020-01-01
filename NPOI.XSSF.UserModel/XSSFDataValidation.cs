using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Collections.Generic;
using System.Text;

namespace NPOI.XSSF.UserModel
{
	/// @author <a href="rjankiraman@emptoris.com">Radhakrishnan J</a>
	public class XSSFDataValidation : IDataValidation
	{
		private CT_DataValidation ctDdataValidation;

		private XSSFDataValidationConstraint validationConstraint;

		private CellRangeAddressList regions;

		internal static Dictionary<int, ST_DataValidationOperator> operatorTypeMappings;

		internal static Dictionary<ST_DataValidationOperator, int> operatorTypeReverseMappings;

		internal static Dictionary<int, ST_DataValidationType> validationTypeMappings;

		internal static Dictionary<ST_DataValidationType, int> validationTypeReverseMappings;

		internal static Dictionary<int, ST_DataValidationErrorStyle> errorStyleMappings;

		public bool EmptyCellAllowed
		{
			get
			{
				return ctDdataValidation.allowBlank;
			}
			set
			{
				ctDdataValidation.allowBlank = value;
			}
		}

		public string ErrorBoxText
		{
			get
			{
				return ctDdataValidation.error;
			}
		}

		public string ErrorBoxTitle
		{
			get
			{
				return ctDdataValidation.errorTitle;
			}
		}

		public int ErrorStyle
		{
			get
			{
				return (int)ctDdataValidation.errorStyle;
			}
			set
			{
				ctDdataValidation.errorStyle = errorStyleMappings[value];
			}
		}

		public string PromptBoxText
		{
			get
			{
				return ctDdataValidation.prompt;
			}
		}

		public string PromptBoxTitle
		{
			get
			{
				return ctDdataValidation.promptTitle;
			}
		}

		public bool ShowErrorBox
		{
			get
			{
				return ctDdataValidation.showErrorMessage;
			}
			set
			{
				ctDdataValidation.showErrorMessage = value;
			}
		}

		public bool ShowPromptBox
		{
			get
			{
				return ctDdataValidation.showInputMessage;
			}
			set
			{
				ctDdataValidation.showInputMessage = value;
			}
		}

		public bool SuppressDropDownArrow
		{
			get
			{
				return !ctDdataValidation.showDropDown;
			}
			set
			{
				if (validationConstraint.GetValidationType() == 3)
				{
					ctDdataValidation.showDropDown = !value;
				}
			}
		}

		public IDataValidationConstraint ValidationConstraint
		{
			get
			{
				return validationConstraint;
			}
		}

		public CellRangeAddressList Regions
		{
			get
			{
				return regions;
			}
		}

		static XSSFDataValidation()
		{
			operatorTypeMappings = new Dictionary<int, ST_DataValidationOperator>();
			operatorTypeReverseMappings = new Dictionary<ST_DataValidationOperator, int>();
			validationTypeMappings = new Dictionary<int, ST_DataValidationType>();
			validationTypeReverseMappings = new Dictionary<ST_DataValidationType, int>();
			errorStyleMappings = new Dictionary<int, ST_DataValidationErrorStyle>();
			errorStyleMappings[2] = ST_DataValidationErrorStyle.information;
			errorStyleMappings[0] = ST_DataValidationErrorStyle.stop;
			errorStyleMappings[1] = ST_DataValidationErrorStyle.warning;
			operatorTypeMappings[0] = ST_DataValidationOperator.between;
			operatorTypeMappings[1] = ST_DataValidationOperator.notBetween;
			operatorTypeMappings[2] = ST_DataValidationOperator.equal;
			operatorTypeMappings[3] = ST_DataValidationOperator.notEqual;
			operatorTypeMappings[4] = ST_DataValidationOperator.greaterThan;
			operatorTypeMappings[6] = ST_DataValidationOperator.greaterThanOrEqual;
			operatorTypeMappings[5] = ST_DataValidationOperator.lessThan;
			operatorTypeMappings[7] = ST_DataValidationOperator.lessThanOrEqual;
			foreach (KeyValuePair<int, ST_DataValidationOperator> operatorTypeMapping in operatorTypeMappings)
			{
				operatorTypeReverseMappings[operatorTypeMapping.Value] = operatorTypeMapping.Key;
			}
			validationTypeMappings[7] = ST_DataValidationType.custom;
			validationTypeMappings[4] = ST_DataValidationType.date;
			validationTypeMappings[2] = ST_DataValidationType.@decimal;
			validationTypeMappings[3] = ST_DataValidationType.list;
			validationTypeMappings[0] = ST_DataValidationType.none;
			validationTypeMappings[6] = ST_DataValidationType.textLength;
			validationTypeMappings[5] = ST_DataValidationType.time;
			validationTypeMappings[1] = ST_DataValidationType.whole;
			foreach (KeyValuePair<int, ST_DataValidationType> validationTypeMapping in validationTypeMappings)
			{
				validationTypeReverseMappings[validationTypeMapping.Value] = validationTypeMapping.Key;
			}
		}

		public XSSFDataValidation(CellRangeAddressList regions, CT_DataValidation ctDataValidation)
		{
			validationConstraint = GetConstraint(ctDataValidation);
			ctDdataValidation = ctDataValidation;
			this.regions = regions;
			ctDdataValidation.errorStyle = ST_DataValidationErrorStyle.stop;
			ctDdataValidation.allowBlank = true;
		}

		public XSSFDataValidation(XSSFDataValidationConstraint constraint, CellRangeAddressList regions, CT_DataValidation ctDataValidation)
		{
			validationConstraint = constraint;
			ctDdataValidation = ctDataValidation;
			this.regions = regions;
			ctDdataValidation.errorStyle = ST_DataValidationErrorStyle.stop;
			ctDdataValidation.allowBlank = true;
		}

		internal CT_DataValidation GetCTDataValidation()
		{
			return ctDdataValidation;
		}

		public void CreateErrorBox(string title, string text)
		{
			ctDdataValidation.errorTitle = title;
			ctDdataValidation.error = text;
		}

		public void CreatePromptBox(string title, string text)
		{
			ctDdataValidation.promptTitle = title;
			ctDdataValidation.prompt = text;
		}

		public string PrettyPrint()
		{
			StringBuilder stringBuilder = new StringBuilder();
			CellRangeAddress[] cellRangeAddresses = regions.CellRangeAddresses;
			foreach (CellRangeAddress cellRangeAddress in cellRangeAddresses)
			{
				stringBuilder.Append(cellRangeAddress.FormatAsString());
			}
			stringBuilder.Append(" => ");
			stringBuilder.Append(validationConstraint.PrettyPrint());
			return stringBuilder.ToString();
		}

		private XSSFDataValidationConstraint GetConstraint(CT_DataValidation ctDataValidation)
		{
			XSSFDataValidationConstraint xSSFDataValidationConstraint = null;
			string formula = ctDataValidation.formula1;
			string formula2 = ctDataValidation.formula2;
			ST_DataValidationOperator @operator = ctDataValidation.@operator;
			ST_DataValidationType type = ctDataValidation.type;
			int validationType = validationTypeReverseMappings[type];
			int operator2 = operatorTypeReverseMappings[@operator];
			return new XSSFDataValidationConstraint(validationType, operator2, formula, formula2);
		}
	}
}
