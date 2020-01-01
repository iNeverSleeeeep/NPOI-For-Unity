namespace NPOI.SS.Format
{
	/// The different kinds of formats that the formatter understands.
	///
	/// @author Ken Arnold, Industrious Media LLC
	public abstract class CellFormatType
	{
		/// The general (default) format; also used for <tt>"General"</tt>. 
		public static readonly CellFormatType GENERAL = new GeneralCellFormatType();

		/// A numeric format. 
		public static readonly CellFormatType NUMBER = new NumberCellFormatType();

		/// A date format. 
		public static readonly CellFormatType DATE = new DateCellFormatType();

		/// An elapsed time format. 
		public static readonly CellFormatType ELAPSED = new ElapsedCellFormatType();

		/// A text format. 
		public static readonly CellFormatType TEXT = new TextCellFormatType();

		/// Returns <tt>true</tt> if the format is special and needs to be quoted.
		///
		/// @param ch The character to test.
		///
		/// @return <tt>true</tt> if the format is special and needs to be quoted.
		public abstract bool IsSpecial(char ch);

		/// Returns a new formatter of the appropriate type, for the given pattern.
		/// The pattern must be appropriate for the type.
		///
		/// @param pattern The pattern to use.
		///
		/// @return A new formatter of the appropriate type, for the given pattern.
		public abstract CellFormatter Formatter(string pattern);
	}
}
