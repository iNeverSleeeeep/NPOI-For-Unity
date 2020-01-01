using NPOI.HSSF.Record;
using NPOI.HSSF.Record.Aggregates;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// HSSFConditionalFormatting class encapsulates all Settings of Conditional Formatting.
	/// The class can be used to make a copy HSSFConditionalFormatting Settings
	/// </summary>
	/// <example>
	/// HSSFConditionalFormatting cf = sheet.GetConditionalFormattingAt(index);
	/// newSheet.AddConditionalFormatting(cf);
	/// or to modify existing Conditional Formatting Settings (formatting regions and/or rules).
	/// Use {@link HSSFSheet#GetConditionalFormattingAt(int)} to Get access to an instance of this class.
	/// To Create a new Conditional Formatting Set use the following approach:
	///
	/// // Define a Conditional Formatting rule, which triggers formatting
	/// // when cell's value Is greater or equal than 100.0 and
	/// // applies patternFormatting defined below.
	/// HSSFConditionalFormattingRule rule = sheet.CreateConditionalFormattingRule(
	/// ComparisonOperator.GE,
	/// "100.0", // 1st formula
	/// null     // 2nd formula Is not used for comparison operator GE
	/// );
	/// // Create pattern with red background
	/// HSSFPatternFormatting patternFmt = rule.cretePatternFormatting();
	/// patternFormatting.SetFillBackgroundColor(HSSFColor.RED.index);
	/// // Define a region containing first column
	/// Region [] regions =
	/// {
	/// new Region(1,(short)1,-1,(short)1)
	/// };
	/// // Apply Conditional Formatting rule defined above to the regions
	/// sheet.AddConditionalFormatting(regions, rule);
	/// </example>
	/// <remarks>@author Dmitriy Kumshayev</remarks>
	public class HSSFConditionalFormatting : IConditionalFormatting
	{
		private HSSFWorkbook _workbook;

		private CFRecordsAggregate cfAggregate;

		/// <summary>
		/// Gets the CF records aggregate.
		/// </summary>
		/// <returns></returns>
		public CFRecordsAggregate CFRecordsAggregate => cfAggregate;

		/// <summary>
		/// Gets the number of Conditional Formatting rules.
		/// </summary>
		/// <value>The number of rules.</value>
		public int NumberOfRules => cfAggregate.NumberOfRules;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFConditionalFormatting" /> class.
		/// </summary>
		/// <param name="workbook">The workbook.</param>
		/// <param name="cfAggregate">The cf aggregate.</param>
		public HSSFConditionalFormatting(HSSFWorkbook workbook, CFRecordsAggregate cfAggregate)
		{
			if (workbook == null)
			{
				throw new ArgumentException("workbook must not be null");
			}
			if (cfAggregate == null)
			{
				throw new ArgumentException("cfAggregate must not be null");
			}
			_workbook = workbook;
			this.cfAggregate = cfAggregate;
		}

		/// <summary>
		/// Gets the array of Regions
		/// </summary>
		/// <returns></returns>
		[Obsolete]
		public Region[] GetFormattingRegions()
		{
			CellRangeAddress[] formattingRanges = GetFormattingRanges();
			return Region.ConvertCellRangesToRegions(formattingRanges);
		}

		/// <summary>
		/// Gets array of CellRangeAddresses
		/// </summary>
		/// <returns></returns>
		public CellRangeAddress[] GetFormattingRanges()
		{
			return cfAggregate.Header.CellRanges;
		}

		/// <summary>
		/// Replaces an existing Conditional Formatting rule at position idx.
		/// Excel allows to Create up to 3 Conditional Formatting rules.
		/// This method can be useful to modify existing  Conditional Formatting rules.
		/// </summary>
		/// <param name="idx">position of the rule. Should be between 0 and 2.</param>
		/// <param name="cfRule">Conditional Formatting rule</param>
		public void SetRule(int idx, HSSFConditionalFormattingRule cfRule)
		{
			cfAggregate.SetRule(idx, cfRule.CfRuleRecord);
		}

		public void SetRule(int idx, IConditionalFormattingRule cfRule)
		{
			SetRule(idx, (HSSFConditionalFormattingRule)cfRule);
		}

		/// <summary>
		/// Add a Conditional Formatting rule.
		/// Excel allows to Create up to 3 Conditional Formatting rules.
		/// </summary>
		/// <param name="cfRule">Conditional Formatting rule</param>
		public void AddRule(HSSFConditionalFormattingRule cfRule)
		{
			cfAggregate.AddRule(cfRule.CfRuleRecord);
		}

		public void AddRule(IConditionalFormattingRule cfRule)
		{
			AddRule((HSSFConditionalFormattingRule)cfRule);
		}

		/// <summary>
		/// Gets the Conditional Formatting rule at position idx
		/// </summary>
		/// <param name="idx">The index.</param>
		/// <returns></returns>
		public IConditionalFormattingRule GetRule(int idx)
		{
			CFRuleRecord rule = cfAggregate.GetRule(idx);
			return new HSSFConditionalFormattingRule(_workbook, rule);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			return cfAggregate.ToString();
		}
	}
}
