using NPOI.HSSF.Record.CF;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace NPOI.XSSF.UserModel
{
	/// @author Yegor Kozlov
	public class XSSFSheetConditionalFormatting : ISheetConditionalFormatting
	{
		private XSSFSheet _sheet;

		/// @return number of Conditional Formatting objects of the sheet
		public int NumConditionalFormattings
		{
			get
			{
				return _sheet.GetCTWorksheet().SizeOfConditionalFormattingArray();
			}
		}

		internal XSSFSheetConditionalFormatting(XSSFSheet sheet)
		{
			_sheet = sheet;
		}

		/// A factory method allowing to create a conditional formatting rule
		/// with a cell comparison operator<p />
		/// TODO - formulas Containing cell references are currently not Parsed properly
		///
		/// @param comparisonOperation - a constant value from
		///             		 <tt>{@link NPOI.hssf.record.CFRuleRecord.ComparisonOperator}</tt>: <p>
		/// <ul>
		///             		 <li>BETWEEN</li>
		///             		 <li>NOT_BETWEEN</li>
		///             		 <li>EQUAL</li>
		///             		 <li>NOT_EQUAL</li>
		///             		 <li>GT</li>
		///             		 <li>LT</li>
		///             		 <li>GE</li>
		///             		 <li>LE</li>
		/// </ul>
		/// </p>
		/// @param formula1 - formula for the valued, Compared with the cell
		/// @param formula2 - second formula (only used with
		/// {@link NPOI.ss.usermodel.ComparisonOperator#BETWEEN}) and
		/// {@link NPOI.ss.usermodel.ComparisonOperator#NOT_BETWEEN} operations)
		public IConditionalFormattingRule CreateConditionalFormattingRule(ComparisonOperator comparisonOperation, string formula1, string formula2)
		{
			XSSFConditionalFormattingRule xSSFConditionalFormattingRule = new XSSFConditionalFormattingRule(_sheet);
			CT_CfRule cTCfRule = xSSFConditionalFormattingRule.GetCTCfRule();
			cTCfRule.AddFormula(formula1);
			if (formula2 != null)
			{
				cTCfRule.AddFormula(formula2);
			}
			cTCfRule.type = ST_CfType.cellIs;
			ST_ConditionalFormattingOperator value;
			switch (comparisonOperation)
			{
			case ComparisonOperator.Between:
				value = ST_ConditionalFormattingOperator.between;
				break;
			case ComparisonOperator.NotBetween:
				value = ST_ConditionalFormattingOperator.notBetween;
				break;
			case ComparisonOperator.LessThan:
				value = ST_ConditionalFormattingOperator.lessThan;
				break;
			case ComparisonOperator.LessThanOrEqual:
				value = ST_ConditionalFormattingOperator.lessThanOrEqual;
				break;
			case ComparisonOperator.GreaterThan:
				value = ST_ConditionalFormattingOperator.greaterThan;
				break;
			case ComparisonOperator.GreaterThanOrEqual:
				value = ST_ConditionalFormattingOperator.greaterThanOrEqual;
				break;
			case ComparisonOperator.Equal:
				value = ST_ConditionalFormattingOperator.equal;
				break;
			case ComparisonOperator.NotEqual:
				value = ST_ConditionalFormattingOperator.notEqual;
				break;
			default:
				throw new ArgumentException("Unknown comparison operator: " + comparisonOperation);
			}
			cTCfRule.@operator = value;
			return xSSFConditionalFormattingRule;
		}

		public IConditionalFormattingRule CreateConditionalFormattingRule(ComparisonOperator comparisonOperation, string formula)
		{
			return CreateConditionalFormattingRule(comparisonOperation, formula, null);
		}

		public IConditionalFormattingRule CreateConditionalFormattingRule(string formula)
		{
			XSSFConditionalFormattingRule xSSFConditionalFormattingRule = new XSSFConditionalFormattingRule(_sheet);
			CT_CfRule cTCfRule = xSSFConditionalFormattingRule.GetCTCfRule();
			cTCfRule.AddFormula(formula);
			cTCfRule.type = ST_CfType.expression;
			return xSSFConditionalFormattingRule;
		}

		public int AddConditionalFormatting(CellRangeAddress[] regions, IConditionalFormattingRule[] cfRules)
		{
			if (regions == null)
			{
				throw new ArgumentException("regions must not be null");
			}
			foreach (CellRangeAddress cellRangeAddress in regions)
			{
				cellRangeAddress.Validate(SpreadsheetVersion.EXCEL2007);
			}
			if (cfRules == null)
			{
				throw new ArgumentException("cfRules must not be null");
			}
			if (cfRules.Length == 0)
			{
				throw new ArgumentException("cfRules must not be empty");
			}
			if (cfRules.Length > 3)
			{
				throw new ArgumentException("Number of rules must not exceed 3");
			}
			if (cfRules is XSSFConditionalFormattingRule[])
			{
				XSSFConditionalFormattingRule[] array = (XSSFConditionalFormattingRule[])cfRules;
			}
			else
			{
				XSSFConditionalFormattingRule[] array = new XSSFConditionalFormattingRule[cfRules.Length];
				Array.Copy(cfRules, 0, array, 0, array.Length);
			}
			CellRangeAddress[] array2 = CellRangeUtil.MergeCellRanges(regions);
			CT_ConditionalFormatting cT_ConditionalFormatting = _sheet.GetCTWorksheet().AddNewConditionalFormatting();
			string text = string.Empty;
			CellRangeAddress[] array3 = array2;
			foreach (CellRangeAddress cellRangeAddress2 in array3)
			{
				text = ((text.Length != 0) ? (text + " " + cellRangeAddress2.FormatAsString()) : cellRangeAddress2.FormatAsString());
			}
			cT_ConditionalFormatting.sqref = text;
			int num = 1;
			foreach (CT_ConditionalFormatting item in _sheet.GetCTWorksheet().conditionalFormatting)
			{
				num += item.sizeOfCfRuleArray();
			}
			foreach (IConditionalFormattingRule conditionalFormattingRule in cfRules)
			{
				XSSFConditionalFormattingRule xSSFConditionalFormattingRule = (XSSFConditionalFormattingRule)conditionalFormattingRule;
				xSSFConditionalFormattingRule.GetCTCfRule().priority = num++;
				cT_ConditionalFormatting.AddNewCfRule().Set(xSSFConditionalFormattingRule.GetCTCfRule());
			}
			return _sheet.GetCTWorksheet().SizeOfConditionalFormattingArray() - 1;
		}

		public int AddConditionalFormatting(CellRangeAddress[] regions, IConditionalFormattingRule rule1)
		{
			return AddConditionalFormatting(regions, (rule1 == null) ? null : new XSSFConditionalFormattingRule[1]
			{
				(XSSFConditionalFormattingRule)rule1
			});
		}

		public int AddConditionalFormatting(CellRangeAddress[] regions, IConditionalFormattingRule rule1, IConditionalFormattingRule rule2)
		{
			return AddConditionalFormatting(regions, (rule1 == null) ? null : new XSSFConditionalFormattingRule[2]
			{
				(XSSFConditionalFormattingRule)rule1,
				(XSSFConditionalFormattingRule)rule2
			});
		}

		public int AddConditionalFormatting(IConditionalFormatting cf)
		{
			XSSFConditionalFormatting xSSFConditionalFormatting = (XSSFConditionalFormatting)cf;
			CT_Worksheet cTWorksheet = _sheet.GetCTWorksheet();
			cTWorksheet.AddNewConditionalFormatting().Set(xSSFConditionalFormatting.GetCTConditionalFormatting());
			return cTWorksheet.SizeOfConditionalFormattingArray() - 1;
		}

		/// Gets Conditional Formatting object at a particular index
		///
		/// @param index
		///             			of the Conditional Formatting object to fetch
		/// @return Conditional Formatting object
		public IConditionalFormatting GetConditionalFormattingAt(int index)
		{
			CheckIndex(index);
			CT_ConditionalFormatting conditionalFormattingArray = _sheet.GetCTWorksheet().GetConditionalFormattingArray(index);
			return new XSSFConditionalFormatting(_sheet, conditionalFormattingArray);
		}

		/// Removes a Conditional Formatting object by index
		/// @param index of a Conditional Formatting object to remove
		public void RemoveConditionalFormatting(int index)
		{
			CheckIndex(index);
			_sheet.GetCTWorksheet().conditionalFormatting.RemoveAt(index);
		}

		private void CheckIndex(int index)
		{
			int numConditionalFormattings = NumConditionalFormattings;
			if (index < 0 || index >= numConditionalFormattings)
			{
				throw new ArgumentException("Specified CF index " + index + " is outside the allowable range (0.." + (numConditionalFormattings - 1) + ")");
			}
		}
	}
}
