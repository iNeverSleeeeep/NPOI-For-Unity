namespace NPOI.SS.UserModel
{
	/// The enumeration value indicating the print orientation for a sheet.
	///
	/// @author Gisella Bronzetti
	public class PrintOrientation
	{
		/// orientation not specified
		public static PrintOrientation DEFAULT;

		/// portrait orientation
		public static PrintOrientation PORTRAIT;

		/// landscape orientations
		public static PrintOrientation LANDSCAPE;

		private int orientation;

		private static PrintOrientation[] _table;

		public int Value => orientation;

		static PrintOrientation()
		{
			_table = new PrintOrientation[4];
			DEFAULT = new PrintOrientation(1);
			PORTRAIT = new PrintOrientation(2);
			LANDSCAPE = new PrintOrientation(3);
		}

		private PrintOrientation(int orientation)
		{
			this.orientation = orientation;
			_table[Value] = this;
		}

		public static PrintOrientation ValueOf(int value)
		{
			return _table[value];
		}
	}
}
