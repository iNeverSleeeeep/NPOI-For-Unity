namespace NPOI.SS.UserModel
{
	public enum SheetState
	{
		/// <summary>
		/// Indicates the sheet is visible.
		/// </summary>
		Visible,
		/// <summary>
		/// Indicates the book window is hidden, but can be shown by the user via the user interface.
		/// </summary>
		Hidden,
		/// <summary>
		/// Indicates the sheet is hidden and cannot be shown in the user interface (UI).
		/// </summary>
		/// <remarks>
		/// In Excel this state is only available programmatically in VBA:
		/// ThisWorkbook.Sheets("MySheetName").Visible = xlSheetVeryHidden
		///
		/// </remarks>
		VeryHidden
	}
}
