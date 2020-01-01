using NPOI.SS.Util;

namespace NPOI.SS
{
	/// This enum allows spReadsheets from multiple Excel versions to be handled by the common code.
	/// Properties of this enum correspond to attributes of the <i>spReadsheet</i> that are easily
	/// discernable to the user.  It is not intended to deal with low-level issues like file formats.
	/// <p />
	///
	/// @author Josh Micich
	/// @author Yegor Kozlov
	public class SpreadsheetVersion
	{
		/// Excel97 format aka BIFF8
		/// <ul>
		/// <li>The total number of available columns is 256 (2^8)</li>
		/// <li>The total number of available rows is 64k (2^16)</li>
		/// <li>The maximum number of arguments to a function is 30</li>
		/// <li>Number of conditional format conditions on a cell is 3</li>
		/// <li>Length of text cell contents is unlimited </li>
		/// <li>Length of text cell contents is 32767</li>
		/// </ul>
		public static SpreadsheetVersion EXCEL97 = new SpreadsheetVersion(65536, 256, 30, 3, 32767);

		/// Excel2007
		///
		/// <ul>
		/// <li>The total number of available columns is 16K (2^14)</li>
		/// <li>The total number of available rows is 1M (2^20)</li>
		/// <li>The maximum number of arguments to a function is 255</li>
		/// <li>Number of conditional format conditions on a cell is unlimited
		/// (actually limited by available memory in Excel)</li>
		/// <li>Length of text cell contents is unlimited </li>
		/// </ul>
		public static SpreadsheetVersion EXCEL2007 = new SpreadsheetVersion(1048576, 16384, 255, 2147483647, 2147483647);

		private int _maxRows;

		private int _maxColumns;

		private int _maxFunctionArgs;

		private int _maxCondFormats;

		private int _maxTextLength;

		/// @return the maximum number of usable rows in each spReadsheet
		public int MaxRows => _maxRows;

		/// @return the last (maximum) valid row index, equals to <code> GetMaxRows() - 1 </code>
		public int LastRowIndex => _maxRows - 1;

		/// @return the maximum number of usable columns in each spReadsheet
		public int MaxColumns => _maxColumns;

		/// @return the last (maximum) valid column index, equals to <code> GetMaxColumns() - 1 </code>
		public int LastColumnIndex => _maxColumns - 1;

		/// @return the maximum number arguments that can be passed to a multi-arg function (e.g. COUNTIF)
		public int MaxFunctionArgs => _maxFunctionArgs;

		/// @return the maximum number of conditional format conditions on a cell
		public int MaxConditionalFormats => _maxCondFormats;

		/// @return the last valid column index in a ALPHA-26 representation
		///  (<code>IV</code> or <code>XFD</code>).
		public string LastColumnName => CellReference.ConvertNumToColString(LastColumnIndex);

		/// @return the maximum length of a text cell
		public int MaxTextLength => _maxTextLength;

		private SpreadsheetVersion(int maxRows, int maxColumns, int maxFunctionArgs, int maxCondFormats, int maxText)
		{
			_maxRows = maxRows;
			_maxColumns = maxColumns;
			_maxFunctionArgs = maxFunctionArgs;
			_maxCondFormats = maxCondFormats;
			_maxTextLength = maxText;
		}
	}
}