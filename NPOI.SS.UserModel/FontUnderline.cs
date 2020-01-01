namespace NPOI.SS.UserModel
{
	/// the different types of possible underline formatting
	///
	/// @author Gisella Bronzetti
	public class FontUnderline
	{
		/// Single-line underlining under each character in the cell.
		/// The underline is drawn through the descenders of
		/// characters such as g and p..
		public static readonly FontUnderline SINGLE;

		/// Double-line underlining under each character in the
		/// cell. underlines are drawn through the descenders of
		/// characters such as g and p.
		public static readonly FontUnderline DOUBLE;

		/// Single-line accounting underlining under each
		/// character in the cell. The underline is drawn under the
		/// descenders of characters such as g and p.
		public static readonly FontUnderline SINGLE_ACCOUNTING;

		/// Double-line accounting underlining under each
		/// character in the cell. The underlines are drawn under
		/// the descenders of characters such as g and p.
		public static readonly FontUnderline DOUBLE_ACCOUNTING;

		/// No underline.
		public static readonly FontUnderline NONE;

		private int value;

		private static FontUnderline[] _table;

		public int Value => value;

		public byte ByteValue
		{
			get
			{
				if (this == DOUBLE)
				{
					return 2;
				}
				if (this == DOUBLE_ACCOUNTING)
				{
					return 34;
				}
				if (this == SINGLE_ACCOUNTING)
				{
					return 33;
				}
				if (this == NONE)
				{
					return 0;
				}
				if (this == SINGLE)
				{
					return 1;
				}
				return 1;
			}
		}

		private FontUnderline(int val)
		{
			value = val;
		}

		static FontUnderline()
		{
			SINGLE = new FontUnderline(1);
			DOUBLE = new FontUnderline(2);
			SINGLE_ACCOUNTING = new FontUnderline(3);
			DOUBLE_ACCOUNTING = new FontUnderline(4);
			NONE = new FontUnderline(5);
			_table = null;
			if (_table == null)
			{
				_table = new FontUnderline[6];
				_table[1] = SINGLE;
				_table[2] = DOUBLE;
				_table[3] = SINGLE_ACCOUNTING;
				_table[4] = DOUBLE_ACCOUNTING;
				_table[5] = NONE;
			}
		}

		public static FontUnderline ValueOf(int value)
		{
			return _table[value];
		}

		public static FontUnderline ValueOf(FontUnderlineType value)
		{
			switch (value)
			{
			case FontUnderlineType.Double:
				return DOUBLE;
			case FontUnderlineType.DoubleAccounting:
				return DOUBLE_ACCOUNTING;
			case FontUnderlineType.SingleAccounting:
				return SINGLE_ACCOUNTING;
			case FontUnderlineType.Single:
				return SINGLE;
			default:
				return NONE;
			}
		}
	}
}
