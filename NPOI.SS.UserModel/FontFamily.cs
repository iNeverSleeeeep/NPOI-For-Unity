namespace NPOI.SS.UserModel
{
	/// The font family this font belongs to. A font family is a set of fonts having common stroke width and serif
	/// characteristics. The font name overrides when there are conflicting values.
	///
	/// @author Gisella Bronzetti
	public class FontFamily
	{
		public static readonly FontFamily NOT_APPLICABLE = new FontFamily(0);

		public static readonly FontFamily ROMAN = new FontFamily(1);

		public static readonly FontFamily SWISS = new FontFamily(2);

		public static readonly FontFamily MODERN = new FontFamily(3);

		public static readonly FontFamily SCRIPT = new FontFamily(4);

		public static readonly FontFamily DECORATIVE = new FontFamily(5);

		private int family;

		/// Returns index of this font family
		///
		/// @return index of this font family
		public int Value => family;

		private FontFamily(int value)
		{
			family = value;
		}

		public static FontFamily ValueOf(int family)
		{
			switch (family)
			{
			case 0:
				return NOT_APPLICABLE;
			case 1:
				return ROMAN;
			case 2:
				return SWISS;
			case 3:
				return MODERN;
			case 4:
				return SCRIPT;
			case 5:
				return DECORATIVE;
			default:
				return NOT_APPLICABLE;
			}
		}
	}
}
