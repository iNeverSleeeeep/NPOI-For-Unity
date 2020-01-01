namespace NPOI.SS.UserModel
{
	/// <summary>
	/// Represents an Excel hyperlink.
	/// </summary>
	public interface IHyperlink
	{
		/// <summary>
		/// Hyperlink address. Depending on the hyperlink type it can be URL, e-mail, patrh to a file, etc.
		/// </summary>
		string Address
		{
			get;
			set;
		}

		/// <summary>
		/// text label for this hyperlink
		/// </summary>
		string Label
		{
			get;
			set;
		}

		/// <summary>
		/// the type of this hyperlink
		/// </summary>
		HyperlinkType Type
		{
			get;
		}

		/// <summary>
		/// the row of the first cell that Contains the hyperlink
		/// </summary>
		int FirstRow
		{
			get;
			set;
		}

		/// <summary>
		/// the row of the last cell that Contains the hyperlink
		/// </summary>
		int LastRow
		{
			get;
			set;
		}

		/// <summary>
		/// the column of the first cell that Contains the hyperlink
		/// </summary>
		int FirstColumn
		{
			get;
			set;
		}

		/// <summary>
		/// the column of the last cell that Contains the hyperlink
		/// </summary>
		int LastColumn
		{
			get;
			set;
		}

		string TextMark
		{
			get;
			set;
		}
	}
}
