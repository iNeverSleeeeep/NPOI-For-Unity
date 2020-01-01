using NPOI.HSSF.Record;
using NPOI.HSSF.Record.CF;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// High level representation for Conditional Formatting Settings
	/// @author Dmitriy Kumshayev
	/// </summary>
	public class HSSFPatternFormatting : IPatternFormatting
	{
		private CFRuleRecord cfRuleRecord;

		private PatternFormatting patternFormatting;

		/// <summary>
		/// Gets the pattern formatting block.
		/// </summary>
		/// <value>The pattern formatting block.</value>
		public PatternFormatting PatternFormattingBlock => patternFormatting;

		/// <summary>
		/// Gets or sets the color of the fill background.
		/// </summary>
		/// <value>The color of the fill background.</value>
		public short FillBackgroundColor
		{
			get
			{
				return patternFormatting.FillBackgroundColor;
			}
			set
			{
				patternFormatting.FillBackgroundColor = value;
				if (value != 0)
				{
					cfRuleRecord.IsPatternBackgroundColorModified = true;
				}
			}
		}

		/// <summary>
		/// Gets or sets the color of the fill foreground.
		/// </summary>
		/// <value>The color of the fill foreground.</value>
		public short FillForegroundColor
		{
			get
			{
				return patternFormatting.FillForegroundColor;
			}
			set
			{
				patternFormatting.FillForegroundColor = value;
				if (value != 0)
				{
					cfRuleRecord.IsPatternColorModified = true;
				}
			}
		}

		/// <summary>
		/// Gets or sets the fill pattern.
		/// </summary>
		/// <value>The fill pattern.</value>
		public short FillPattern
		{
			get
			{
				return (short)patternFormatting.FillPattern;
			}
			set
			{
				patternFormatting.FillPattern = (FillPattern)value;
				if (value != 0)
				{
					cfRuleRecord.IsPatternStyleModified = true;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFPatternFormatting" /> class.
		/// </summary>
		/// <param name="cfRuleRecord">The cf rule record.</param>
		public HSSFPatternFormatting(CFRuleRecord cfRuleRecord)
		{
			this.cfRuleRecord = cfRuleRecord;
			patternFormatting = cfRuleRecord.PatternFormatting;
		}
	}
}
