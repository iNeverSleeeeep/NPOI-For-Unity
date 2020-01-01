namespace NPOI.SS.UserModel
{
	/// Represents a description of a conditional formatting rule
	///
	/// @author Dmitriy Kumshayev
	/// @author Yegor Kozlov
	public interface IConditionalFormattingRule
	{
		/// Type of conditional formatting rule.
		/// <p>
		/// MUST be either {@link #CONDITION_TYPE_CELL_VALUE_IS} or  {@link #CONDITION_TYPE_FORMULA}
		/// </p>
		///
		/// @return the type of condition
		ConditionType ConditionType
		{
			get;
		}

		/// The comparison function used when the type of conditional formatting is Set to
		/// {@link #CONDITION_TYPE_CELL_VALUE_IS}
		/// <p>
		///     MUST be a constant from {@link ComparisonOperator}
		/// </p>
		///
		/// @return the conditional format operator
		ComparisonOperator ComparisonOperation
		{
			get;
		}

		/// The formula used to Evaluate the first operand for the conditional formatting rule.
		/// <p>
		/// If the condition type is {@link #CONDITION_TYPE_CELL_VALUE_IS},
		/// this field is the first operand of the comparison.
		/// If type is {@link #CONDITION_TYPE_FORMULA}, this formula is used
		/// to determine if the conditional formatting is applied.
		/// </p>
		/// <p>
		/// If comparison type is {@link #CONDITION_TYPE_FORMULA} the formula MUST be a Boolean function
		/// </p>
		///
		/// @return  the first formula
		string Formula1
		{
			get;
		}

		/// The formula used to Evaluate the second operand of the comparison when
		/// comparison type is  {@link #CONDITION_TYPE_CELL_VALUE_IS} and operator
		/// is either {@link ComparisonOperator#BETWEEN} or {@link ComparisonOperator#NOT_BETWEEN}
		///
		/// @return  the second formula
		string Formula2
		{
			get;
		}

		/// Create a new border formatting structure if it does not exist,
		/// otherwise just return existing object.
		///
		/// @return - border formatting object, never returns <code>null</code>.
		IBorderFormatting CreateBorderFormatting();

		/// @return - border formatting object  if defined,  <code>null</code> otherwise
		IBorderFormatting GetBorderFormatting();

		/// Create a new font formatting structure if it does not exist,
		/// otherwise just return existing object.
		///
		/// @return - font formatting object, never returns <code>null</code>.
		IFontFormatting CreateFontFormatting();

		/// @return - font formatting object  if defined,  <code>null</code> otherwise
		IFontFormatting GetFontFormatting();

		/// Create a new pattern formatting structure if it does not exist,
		/// otherwise just return existing object.
		///
		/// @return - pattern formatting object, never returns <code>null</code>.
		IPatternFormatting CreatePatternFormatting();

		/// @return - pattern formatting object  if defined,  <code>null</code> otherwise
		IPatternFormatting GetPatternFormatting();
	}
}
