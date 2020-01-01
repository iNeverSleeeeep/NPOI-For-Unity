using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.SS.UserModel;
using NPOI.XSSF.Model;

namespace NPOI.XSSF.UserModel
{
	/// @author Yegor Kozlov
	public class XSSFConditionalFormattingRule : IConditionalFormattingRule
	{
		private CT_CfRule _cfRule;

		private XSSFSheet _sh;

		/// Type of conditional formatting rule.
		/// <p>
		/// MUST be either {@link ConditionalFormattingRule#CONDITION_TYPE_CELL_VALUE_IS}
		/// or  {@link ConditionalFormattingRule#CONDITION_TYPE_FORMULA}
		/// </p>
		///
		/// @return the type of condition
		public ConditionType ConditionType
		{
			get
			{
				switch (_cfRule.type)
				{
				case ST_CfType.expression:
					return ConditionType.Formula;
				case ST_CfType.cellIs:
					return ConditionType.CellValueIs;
				default:
					return ConditionType.None;
				}
			}
		}

		/// The comparison function used when the type of conditional formatting is Set to
		/// {@link ConditionalFormattingRule#CONDITION_TYPE_CELL_VALUE_IS}
		/// <p>
		///     MUST be a constant from {@link NPOI.ss.usermodel.ComparisonOperator}
		/// </p>
		///
		/// @return the conditional format operator
		public ComparisonOperator ComparisonOperation
		{
			get
			{
				ST_ConditionalFormattingOperator? @operator = _cfRule.@operator;
				if (@operator.HasValue)
				{
					switch (@operator)
					{
					case ST_ConditionalFormattingOperator.lessThan:
						return ComparisonOperator.LessThan;
					case ST_ConditionalFormattingOperator.lessThanOrEqual:
						return ComparisonOperator.LessThanOrEqual;
					case ST_ConditionalFormattingOperator.greaterThan:
						return ComparisonOperator.GreaterThan;
					case ST_ConditionalFormattingOperator.greaterThanOrEqual:
						return ComparisonOperator.GreaterThanOrEqual;
					case ST_ConditionalFormattingOperator.equal:
						return ComparisonOperator.Equal;
					case ST_ConditionalFormattingOperator.notEqual:
						return ComparisonOperator.NotEqual;
					case ST_ConditionalFormattingOperator.between:
						return ComparisonOperator.Between;
					case ST_ConditionalFormattingOperator.notBetween:
						return ComparisonOperator.NotBetween;
					default:
						return ComparisonOperator.NoComparison;
					}
				}
				return ComparisonOperator.NoComparison;
			}
		}

		/// The formula used to Evaluate the first operand for the conditional formatting rule.
		/// <p>
		/// If the condition type is {@link ConditionalFormattingRule#CONDITION_TYPE_CELL_VALUE_IS},
		/// this field is the first operand of the comparison.
		/// If type is {@link ConditionalFormattingRule#CONDITION_TYPE_FORMULA}, this formula is used
		/// to determine if the conditional formatting is applied.
		/// </p>
		/// <p>
		/// If comparison type is {@link ConditionalFormattingRule#CONDITION_TYPE_FORMULA} the formula MUST be a Boolean function
		/// </p>
		///
		/// @return  the first formula
		public string Formula1
		{
			get
			{
				if (_cfRule.sizeOfFormulaArray() <= 0)
				{
					return null;
				}
				return _cfRule.GetFormulaArray(0);
			}
		}

		/// The formula used to Evaluate the second operand of the comparison when
		/// comparison type is  {@link ConditionalFormattingRule#CONDITION_TYPE_CELL_VALUE_IS} and operator
		/// is either {@link NPOI.ss.usermodel.ComparisonOperator#BETWEEN} or {@link NPOI.ss.usermodel.ComparisonOperator#NOT_BETWEEN}
		///
		/// @return  the second formula
		public string Formula2
		{
			get
			{
				if (_cfRule.sizeOfFormulaArray() != 2)
				{
					return null;
				}
				return _cfRule.GetFormulaArray(1);
			}
		}

		public XSSFConditionalFormattingRule(XSSFSheet sh)
		{
			_cfRule = new CT_CfRule();
			_sh = sh;
		}

		internal XSSFConditionalFormattingRule(XSSFSheet sh, CT_CfRule cfRule)
		{
			_cfRule = cfRule;
			_sh = sh;
		}

		internal CT_CfRule GetCTCfRule()
		{
			return _cfRule;
		}

		internal CT_Dxf GetDxf(bool create)
		{
			StylesTable stylesSource = ((XSSFWorkbook)_sh.Workbook).GetStylesSource();
			CT_Dxf cT_Dxf = null;
			if (stylesSource.DXfsSize > 0 && _cfRule.IsSetDxfId())
			{
				int dxfId = (int)_cfRule.dxfId;
				cT_Dxf = stylesSource.GetDxfAt(dxfId);
			}
			if (create && cT_Dxf == null)
			{
				cT_Dxf = new CT_Dxf();
				int num = stylesSource.PutDxf(cT_Dxf);
				_cfRule.dxfId = (uint)(num - 1);
			}
			return cT_Dxf;
		}

		/// Create a new border formatting structure if it does not exist,
		/// otherwise just return existing object.
		///
		/// @return - border formatting object, never returns <code>null</code>.
		public IBorderFormatting CreateBorderFormatting()
		{
			CT_Dxf dxf = GetDxf(true);
			CT_Border border = dxf.IsSetBorder() ? dxf.border : dxf.AddNewBorder();
			return new XSSFBorderFormatting(border);
		}

		/// @return - border formatting object  if defined,  <code>null</code> otherwise
		public IBorderFormatting GetBorderFormatting()
		{
			CT_Dxf dxf = GetDxf(false);
			if (dxf == null || !dxf.IsSetBorder())
			{
				return null;
			}
			return new XSSFBorderFormatting(dxf.border);
		}

		/// Create a new font formatting structure if it does not exist,
		/// otherwise just return existing object.
		///
		/// @return - font formatting object, never returns <code>null</code>.
		public IFontFormatting CreateFontFormatting()
		{
			CT_Dxf dxf = GetDxf(true);
			CT_Font font = dxf.IsSetFont() ? dxf.font : dxf.AddNewFont();
			return new XSSFFontFormatting(font);
		}

		/// @return - font formatting object  if defined,  <code>null</code> otherwise
		public IFontFormatting GetFontFormatting()
		{
			CT_Dxf dxf = GetDxf(false);
			if (dxf == null || !dxf.IsSetFont())
			{
				return null;
			}
			return new XSSFFontFormatting(dxf.font);
		}

		/// Create a new pattern formatting structure if it does not exist,
		/// otherwise just return existing object.
		///
		/// @return - pattern formatting object, never returns <code>null</code>.
		public IPatternFormatting CreatePatternFormatting()
		{
			CT_Dxf dxf = GetDxf(true);
			CT_Fill fill = dxf.IsSetFill() ? dxf.fill : dxf.AddNewFill();
			return new XSSFPatternFormatting(fill);
		}

		/// @return - pattern formatting object  if defined,  <code>null</code> otherwise
		public IPatternFormatting GetPatternFormatting()
		{
			CT_Dxf dxf = GetDxf(false);
			if (dxf == null || !dxf.IsSetFill())
			{
				return null;
			}
			return new XSSFPatternFormatting(dxf.fill);
		}
	}
}
