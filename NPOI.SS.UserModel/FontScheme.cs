namespace NPOI.SS.UserModel
{
	/// Defines the font scheme to which this font belongs.
	/// When a font defInition is part of a theme defInition, then the font is categorized as either a major or minor font scheme component.
	/// When a new theme is chosen, every font that is part of a theme defInition is updated to use the new major or minor font defInition for that
	/// theme.
	/// Usually major fonts are used for styles like headings, and minor fonts are used for body and paragraph text.
	///
	/// @author Gisella Bronzetti
	public class FontScheme
	{
		public static readonly FontScheme NONE = new FontScheme(1);

		public static readonly FontScheme MAJOR = new FontScheme(2);

		public static readonly FontScheme MINOR = new FontScheme(3);

		private int value;

		public int Value => value;

		private FontScheme(int val)
		{
			value = val;
		}

		public static FontScheme ValueOf(int value)
		{
			switch (value)
			{
			case 1:
				return NONE;
			case 2:
				return MAJOR;
			case 3:
				return MINOR;
			default:
				return NONE;
			}
		}
	}
}
