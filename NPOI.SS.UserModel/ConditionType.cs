namespace NPOI.SS.UserModel
{
	public enum ConditionType : byte
	{
		/// <summary>
		/// Allow accessing the Initial value.
		/// </summary>
		None,
		/// This conditional formatting rule Compares a cell value
		/// to a formula calculated result, using an operator
		CellValueIs,
		/// This conditional formatting rule Contains a formula to Evaluate.
		/// When the formula result is true, the cell is highlighted.
		Formula
	}
}
