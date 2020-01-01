namespace NPOI.SS.UserModel
{
	/// Charset represents the basic set of characters associated with a font (that it can display), and 
	/// corresponds to the ANSI codepage (8-bit or DBCS) of that character set used by a given language. 
	///
	/// @author Gisella Bronzetti
	public class FontCharset
	{
		public static readonly FontCharset ANSI;

		public static readonly FontCharset DEFAULT;

		public static readonly FontCharset SYMBOL;

		public static readonly FontCharset MAC;

		public static readonly FontCharset SHIFTJIS;

		public static readonly FontCharset HANGEUL;

		public static readonly FontCharset JOHAB;

		public static readonly FontCharset GB2312;

		public static readonly FontCharset CHINESEBIG5;

		public static readonly FontCharset GREEK;

		public static readonly FontCharset TURKISH;

		public static readonly FontCharset VIETNAMESE;

		public static readonly FontCharset HEBREW;

		public static readonly FontCharset ARABIC;

		public static readonly FontCharset BALTIC;

		public static readonly FontCharset RUSSIAN;

		public static readonly FontCharset THAI;

		public static readonly FontCharset EASTEUROPE;

		public static readonly FontCharset OEM;

		private int charset;

		private static FontCharset[] _table;

		/// Returns value of this charset
		///
		/// @return value of this charset
		public int Value => charset;

		private FontCharset(int value)
		{
			charset = value;
		}

		static FontCharset()
		{
			ANSI = new FontCharset(0);
			DEFAULT = new FontCharset(1);
			SYMBOL = new FontCharset(2);
			MAC = new FontCharset(77);
			SHIFTJIS = new FontCharset(128);
			HANGEUL = new FontCharset(129);
			JOHAB = new FontCharset(130);
			GB2312 = new FontCharset(134);
			CHINESEBIG5 = new FontCharset(136);
			GREEK = new FontCharset(161);
			TURKISH = new FontCharset(162);
			VIETNAMESE = new FontCharset(163);
			HEBREW = new FontCharset(177);
			ARABIC = new FontCharset(178);
			BALTIC = new FontCharset(186);
			RUSSIAN = new FontCharset(204);
			THAI = new FontCharset(222);
			EASTEUROPE = new FontCharset(238);
			OEM = new FontCharset(255);
			_table = null;
			if (_table == null)
			{
				_table = new FontCharset[256];
				_table[0] = ANSI;
				_table[1] = DEFAULT;
				_table[2] = SYMBOL;
				_table[77] = MAC;
				_table[128] = SHIFTJIS;
				_table[129] = HANGEUL;
				_table[130] = JOHAB;
				_table[134] = GB2312;
				_table[136] = CHINESEBIG5;
				_table[161] = GREEK;
				_table[162] = TURKISH;
				_table[163] = VIETNAMESE;
				_table[177] = HEBREW;
				_table[178] = ARABIC;
				_table[186] = BALTIC;
				_table[204] = RUSSIAN;
				_table[222] = THAI;
				_table[238] = EASTEUROPE;
				_table[255] = OEM;
			}
		}

		public static FontCharset ValueOf(int value)
		{
			if (value >= 0 && value <= 255)
			{
				return _table[value];
			}
			return null;
		}
	}
}
