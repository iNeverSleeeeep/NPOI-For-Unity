using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.Collections.Generic;

namespace NPOI.XSSF.UserModel
{
	/// @author Yegor Kozlov
	public class XSSFConditionalFormatting : IConditionalFormatting
	{
		private CT_ConditionalFormatting _cf;

		private XSSFSheet _sh;

		/// @return number of Conditional Formatting rules.
		public int NumberOfRules
		{
			get
			{
				return _cf.sizeOfCfRuleArray();
			}
		}

		internal XSSFConditionalFormatting(XSSFSheet sh)
		{
			_cf = new CT_ConditionalFormatting();
			_sh = sh;
		}

		internal XSSFConditionalFormatting(XSSFSheet sh, CT_ConditionalFormatting cf)
		{
			_cf = cf;
			_sh = sh;
		}

		internal CT_ConditionalFormatting GetCTConditionalFormatting()
		{
			return _cf;
		}

		/// @return array of <tt>CellRangeAddress</tt>s. Never <code>null</code>
		public CellRangeAddress[] GetFormattingRanges()
		{
			List<CellRangeAddress> list = new List<CellRangeAddress>();
			string[] array = _cf.sqref.Split(' ');
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(CellRangeAddress.ValueOf(array[i]));
			}
			return list.ToArray();
		}

		/// Replaces an existing Conditional Formatting rule at position idx.
		/// Excel allows to create up to 3 Conditional Formatting rules.
		/// This method can be useful to modify existing  Conditional Formatting rules.
		///
		/// @param idx position of the rule. Should be between 0 and 2.
		/// @param cfRule - Conditional Formatting rule
		public void SetRule(int idx, IConditionalFormattingRule cfRule)
		{
			XSSFConditionalFormattingRule xSSFConditionalFormattingRule = (XSSFConditionalFormattingRule)cfRule;
			_cf.GetCfRuleArray(idx).Set(xSSFConditionalFormattingRule.GetCTCfRule());
		}

		/// Add a Conditional Formatting rule.
		/// Excel allows to create up to 3 Conditional Formatting rules.
		///
		/// @param cfRule - Conditional Formatting rule
		public void AddRule(IConditionalFormattingRule cfRule)
		{
			XSSFConditionalFormattingRule xSSFConditionalFormattingRule = (XSSFConditionalFormattingRule)cfRule;
			_cf.AddNewCfRule().Set(xSSFConditionalFormattingRule.GetCTCfRule());
		}

		/// @return the Conditional Formatting rule at position idx.
		public IConditionalFormattingRule GetRule(int idx)
		{
			return new XSSFConditionalFormattingRule(_sh, _cf.GetCfRuleArray(idx));
		}
	}
}
