using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.HSSF.Record.CF;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// High level representation of Conditional Formatting Rule.
	/// It allows to specify formula based conditions for the Conditional Formatting
	/// and the formatting Settings such as font, border and pattern.
	///
	/// @author Dmitriy Kumshayev
	public class HSSFConditionalFormattingRule : IConditionalFormattingRule
	{
		private const byte CELL_COMPARISON = 1;

		private CFRuleRecord cfRuleRecord;

		private HSSFWorkbook workbook;

		public CFRuleRecord CfRuleRecord => cfRuleRecord;

		/// @return -  the conditiontype for the cfrule
		public ConditionType ConditionType => (ConditionType)cfRuleRecord.ConditionType;

		/// @return - the comparisionoperatation for the cfrule
		public ComparisonOperator ComparisonOperation => (ComparisonOperator)cfRuleRecord.ComparisonOperation;

		public string Formula1 => ToFormulaString(cfRuleRecord.ParsedExpression1);

		public string Formula2
		{
			get
			{
				byte conditionType = cfRuleRecord.ConditionType;
				if (conditionType == 1)
				{
					switch (cfRuleRecord.ComparisonOperation)
					{
					case 1:
					case 2:
						return ToFormulaString(cfRuleRecord.ParsedExpression2);
					}
				}
				return null;
			}
		}

		public HSSFConditionalFormattingRule(HSSFWorkbook pWorkbook, CFRuleRecord pRuleRecord)
		{
			workbook = pWorkbook;
			cfRuleRecord = pRuleRecord;
		}

		private HSSFFontFormatting GetFontFormatting(bool Create)
		{
			FontFormatting fontFormatting = cfRuleRecord.FontFormatting;
			if (fontFormatting != null)
			{
				cfRuleRecord.FontFormatting = fontFormatting;
				return new HSSFFontFormatting(cfRuleRecord);
			}
			if (Create)
			{
				fontFormatting = new FontFormatting();
				cfRuleRecord.FontFormatting = fontFormatting;
				return new HSSFFontFormatting(cfRuleRecord);
			}
			return null;
		}

		/// @return - font formatting object  if defined,  <c>null</c> otherwise
		public IFontFormatting GetFontFormatting()
		{
			return GetFontFormatting(Create: false);
		}

		/// Create a new font formatting structure if it does not exist, 
		/// otherwise just return existing object.
		/// @return - font formatting object, never returns <c>null</c>. 
		public IFontFormatting CreateFontFormatting()
		{
			return GetFontFormatting(Create: true);
		}

		private HSSFBorderFormatting GetBorderFormatting(bool Create)
		{
			BorderFormatting borderFormatting = cfRuleRecord.BorderFormatting;
			if (borderFormatting != null)
			{
				cfRuleRecord.BorderFormatting = borderFormatting;
				return new HSSFBorderFormatting(cfRuleRecord);
			}
			if (Create)
			{
				borderFormatting = new BorderFormatting();
				cfRuleRecord.BorderFormatting = borderFormatting;
				return new HSSFBorderFormatting(cfRuleRecord);
			}
			return null;
		}

		/// @return - border formatting object  if defined,  <c>null</c> otherwise
		public IBorderFormatting GetBorderFormatting()
		{
			return GetBorderFormatting(Create: false);
		}

		/// Create a new border formatting structure if it does not exist, 
		/// otherwise just return existing object.
		/// @return - border formatting object, never returns <c>null</c>. 
		public IBorderFormatting CreateBorderFormatting()
		{
			return GetBorderFormatting(Create: true);
		}

		private HSSFPatternFormatting GetPatternFormatting(bool Create)
		{
			PatternFormatting patternFormatting = cfRuleRecord.PatternFormatting;
			if (patternFormatting != null)
			{
				cfRuleRecord.PatternFormatting = patternFormatting;
				return new HSSFPatternFormatting(cfRuleRecord);
			}
			if (Create)
			{
				patternFormatting = new PatternFormatting();
				cfRuleRecord.PatternFormatting = patternFormatting;
				return new HSSFPatternFormatting(cfRuleRecord);
			}
			return null;
		}

		/// @return - pattern formatting object  if defined, <c>null</c> otherwise
		public IPatternFormatting GetPatternFormatting()
		{
			return GetPatternFormatting(Create: false);
		}

		/// Create a new pattern formatting structure if it does not exist, 
		/// otherwise just return existing object.
		/// @return - pattern formatting object, never returns <c>null</c>. 
		public IPatternFormatting CreatePatternFormatting()
		{
			return GetPatternFormatting(Create: true);
		}

		private string ToFormulaString(Ptg[] ParsedExpression)
		{
			if (ParsedExpression == null)
			{
				return null;
			}
			return HSSFFormulaParser.ToFormulaString(workbook, ParsedExpression);
		}
	}
}
