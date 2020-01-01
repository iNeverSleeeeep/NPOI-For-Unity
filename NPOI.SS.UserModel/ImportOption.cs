namespace NPOI.SS.UserModel
{
	public enum ImportOption
	{
		NONE,
		/// <summary>
		/// Only Text and Formulas are imported. Pictures, Drawing, Styles etc. are all ignored.
		/// </summary>
		SheetContentOnly,
		/// <summary>
		/// Only Text, Comments and Formulas are imported. Pictures, Drawing, Styles etc. are all ignored.
		/// </summary>
		TextOnly,
		/// <summary>
		/// Everything is imported - this is the same as NONE.
		/// </summary>
		All
	}
}
