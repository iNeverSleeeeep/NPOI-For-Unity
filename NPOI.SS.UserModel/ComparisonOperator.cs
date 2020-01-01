namespace NPOI.SS.UserModel
{
	/// The conditional format operators used for "Highlight Cells That Contain..." rules.
	/// <p>
	/// For example, "highlight cells that begin with "M2" and contain "Mountain Gear".
	/// </p>
	///
	/// @author Dmitriy Kumshayev
	/// @author Yegor Kozlov
	public enum ComparisonOperator : byte
	{
		NoComparison,
		/// 'Between' operator
		Between,
		/// 'Not between' operator
		NotBetween,
		/// 'Equal to' operator
		Equal,
		/// 'Not equal to' operator
		NotEqual,
		/// 'Greater than' operator
		GreaterThan,
		/// 'Less than' operator
		LessThan,
		/// 'Greater than or equal to' operator
		GreaterThanOrEqual,
		/// 'Less than or equal to' operator
		LessThanOrEqual
	}
}
