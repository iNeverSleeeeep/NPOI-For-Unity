using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// The Conditional Formatting facet of HSSFSheet
	/// @author Dmitriy Kumshayev
	/// </summary>
	public class HSSFSheetConditionalFormatting : ISheetConditionalFormatting
	{
		private HSSFSheet _sheet;

		private ConditionalFormattingTable _conditionalFormattingTable;

		/// <summary>
		/// the number of Conditional Formatting objects of the sheet
		/// </summary>
		/// <value>The num conditional formattings.</value>
		public int NumConditionalFormattings => _conditionalFormattingTable.Count;

		public HSSFSheetConditionalFormatting(HSSFSheet sheet)
		{
			_sheet = sheet;
			_conditionalFormattingTable = sheet.Sheet.ConditionalFormattingTable;
		}

		/// <summary>
		/// A factory method allowing to Create a conditional formatting rule
		/// with a cell comparison operator
		/// TODO - formulas containing cell references are currently not Parsed properly
		/// </summary>
		/// <param name="comparisonOperation">a constant value from HSSFConditionalFormattingRule.ComparisonOperator</param>
		/// <param name="formula1">formula for the valued, Compared with the cell</param>
		/// <param name="formula2">second formula (only used with HSSFConditionalFormattingRule#COMPARISON_OPERATOR_BETWEEN 
		/// and HSSFConditionalFormattingRule#COMPARISON_OPERATOR_NOT_BETWEEN operations)</param>
		/// <returns></returns>
		public IConditionalFormattingRule CreateConditionalFormattingRule(ComparisonOperator comparisonOperation, string formula1, string formula2)
		{
			HSSFWorkbook pWorkbook = (HSSFWorkbook)_sheet.Workbook;
			CFRuleRecord pRuleRecord = CFRuleRecord.Create(_sheet, (byte)comparisonOperation, formula1, formula2);
			return new HSSFConditionalFormattingRule(pWorkbook, pRuleRecord);
		}

		/// <summary>
		/// A factory method allowing to Create a conditional formatting rule with a formula.
		/// The formatting rules are applied by Excel when the value of the formula not equal to 0.
		/// TODO - formulas containing cell references are currently not Parsed properly
		/// </summary>
		/// <param name="formula">formula for the valued, Compared with the cell</param>
		/// <returns></returns>
		public IConditionalFormattingRule CreateConditionalFormattingRule(string formula)
		{
			HSSFWorkbook pWorkbook = (HSSFWorkbook)_sheet.Workbook;
			CFRuleRecord pRuleRecord = CFRuleRecord.Create(_sheet, formula);
			return new HSSFConditionalFormattingRule(pWorkbook, pRuleRecord);
		}

		public IConditionalFormattingRule CreateConditionalFormattingRule(ComparisonOperator comparisonOperation, string formula1)
		{
			HSSFWorkbook pWorkbook = (HSSFWorkbook)_sheet.Workbook;
			CFRuleRecord pRuleRecord = CFRuleRecord.Create(_sheet, (byte)comparisonOperation, formula1, null);
			return new HSSFConditionalFormattingRule(pWorkbook, pRuleRecord);
		}

		/// <summary>
		/// Adds a copy of HSSFConditionalFormatting object to the sheet
		/// This method could be used to copy HSSFConditionalFormatting object
		/// from one sheet to another.
		/// </summary>
		/// <param name="cf">HSSFConditionalFormatting object</param>
		/// <returns>index of the new Conditional Formatting object</returns>
		/// <example>
		/// HSSFConditionalFormatting cf = sheet.GetConditionalFormattingAt(index);
		/// newSheet.AddConditionalFormatting(cf);
		/// </example>
		public int AddConditionalFormatting(IConditionalFormatting cf)
		{
			CFRecordsAggregate cfAggregate = ((HSSFConditionalFormatting)cf).CFRecordsAggregate.CloneCFAggregate();
			return _conditionalFormattingTable.Add(cfAggregate);
		}

		/// <summary>
		/// Allows to Add a new Conditional Formatting Set to the sheet.
		/// </summary>
		/// <param name="regions">list of rectangular regions to apply conditional formatting rules</param>
		/// <param name="cfRules">Set of up to three conditional formatting rules</param>
		/// <returns>index of the newly Created Conditional Formatting object</returns>
		public int AddConditionalFormatting(CellRangeAddress[] regions, IConditionalFormattingRule[] cfRules)
		{
			if (regions == null)
			{
				throw new ArgumentException("regions must not be null");
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
			CFRuleRecord[] array = new CFRuleRecord[cfRules.Length];
			for (int i = 0; i != cfRules.Length; i++)
			{
				array[i] = ((HSSFConditionalFormattingRule)cfRules[i]).CfRuleRecord;
			}
			CFRecordsAggregate cfAggregate = new CFRecordsAggregate(regions, array);
			return _conditionalFormattingTable.Add(cfAggregate);
		}

		public int AddConditionalFormatting(CellRangeAddress[] regions, HSSFConditionalFormattingRule rule1)
		{
			return AddConditionalFormatting(regions, (rule1 == null) ? null : new HSSFConditionalFormattingRule[1]
			{
				rule1
			});
		}

		/// <summary>
		/// Adds the conditional formatting.
		/// </summary>
		/// <param name="regions">The regions.</param>
		/// <param name="rule1">The rule1.</param>
		/// <returns></returns>
		public int AddConditionalFormatting(CellRangeAddress[] regions, IConditionalFormattingRule rule1)
		{
			return AddConditionalFormatting(regions, (HSSFConditionalFormattingRule)rule1);
		}

		/// <summary>
		/// Adds the conditional formatting.
		/// </summary>
		/// <param name="regions">The regions.</param>
		/// <param name="rule1">The rule1.</param>
		/// <param name="rule2">The rule2.</param>
		/// <returns></returns>
		public int AddConditionalFormatting(CellRangeAddress[] regions, IConditionalFormattingRule rule1, IConditionalFormattingRule rule2)
		{
			return AddConditionalFormatting(regions, new HSSFConditionalFormattingRule[2]
			{
				(HSSFConditionalFormattingRule)rule1,
				(HSSFConditionalFormattingRule)rule2
			});
		}

		/// <summary>
		/// Gets Conditional Formatting object at a particular index
		/// @param index
		/// of the Conditional Formatting object to fetch
		/// </summary>
		/// <param name="index">Conditional Formatting object</param>
		/// <returns></returns>
		public IConditionalFormatting GetConditionalFormattingAt(int index)
		{
			CFRecordsAggregate cFRecordsAggregate = _conditionalFormattingTable.Get(index);
			if (cFRecordsAggregate == null)
			{
				return null;
			}
			return new HSSFConditionalFormatting((HSSFWorkbook)_sheet.Workbook, cFRecordsAggregate);
		}

		/// <summary>
		/// Removes a Conditional Formatting object by index
		/// </summary>
		/// <param name="index">index of a Conditional Formatting object to Remove</param>
		public void RemoveConditionalFormatting(int index)
		{
			_conditionalFormattingTable.Remove(index);
		}
	}
}
