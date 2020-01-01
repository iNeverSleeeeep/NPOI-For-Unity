namespace NPOI.SS.UserModel
{
	/// <summary>
	/// Common interface for NPOI.SS.UserModel.Header and NPOI.SS.UserModel.Footer
	/// </summary>
	public interface IHeaderFooter
	{
		/// <summary>
		/// Gets or sets the left side of the header or footer.
		/// </summary>
		/// <value>The string representing the left side.</value>
		string Left
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the center of the header or footer.
		/// </summary>
		/// <value>The string representing the center.</value>
		string Center
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the right side of the header or footer.
		/// </summary>
		/// <value>The string representing the right side.</value>
		string Right
		{
			get;
			set;
		}
	}
}
