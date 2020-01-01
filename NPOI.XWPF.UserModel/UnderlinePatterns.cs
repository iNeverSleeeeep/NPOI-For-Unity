namespace NPOI.XWPF.UserModel
{
	/// Specifies the types of patterns which may be used to create the underline
	/// applied beneath the text in a Run.
	///
	/// @author Gisella Bronzetti
	public enum UnderlinePatterns
	{
		/// Specifies an underline consisting of a single line beneath all characters
		/// in this Run.
		Single = 1,
		/// Specifies an underline consisting of a single line beneath all non-space
		/// characters in the Run. There shall be no underline beneath any space
		/// character (breaking or non-breaking).
		Words,
		/// Specifies an underline consisting of two lines beneath all characters in
		/// this run
		Double,
		/// Specifies an underline consisting of a single thick line beneath all
		/// characters in this Run.
		Thick,
		/// Specifies an underline consisting of a series of dot characters beneath
		/// all characters in this Run.
		Dotted,
		/// Specifies an underline consisting of a series of thick dot characters
		/// beneath all characters in this Run.
		DottedHeavy,
		/// Specifies an underline consisting of a dashed line beneath all characters
		/// in this Run.
		Dash,
		/// Specifies an underline consisting of a series of thick dashes beneath all
		/// characters in this Run.
		DashedHeavy,
		/// Specifies an underline consisting of long dashed characters beneath all
		/// characters in this Run.
		DashLong,
		/// Specifies an underline consisting of thick long dashed characters beneath
		/// all characters in this Run.
		DashLongHeavy,
		/// Specifies an underline consisting of a series of dash, dot characters
		/// beneath all characters in this Run.
		DotDash,
		/// Specifies an underline consisting of a series of thick dash, dot
		/// characters beneath all characters in this Run.
		DashDotHeavy,
		/// Specifies an underline consisting of a series of dash, dot, dot
		/// characters beneath all characters in this Run.
		DotDotDash,
		/// Specifies an underline consisting of a series of thick dash, dot, dot
		/// characters beneath all characters in this Run.
		DashDotDotHeavy,
		/// Specifies an underline consisting of a single wavy line beneath all
		/// characters in this Run.
		Wave,
		/// Specifies an underline consisting of a single thick wavy line beneath all
		/// characters in this Run.
		WavyHeavy,
		/// Specifies an underline consisting of a pair of wavy lines beneath all
		/// characters in this Run.
		WavyDouble,
		/// Specifies no underline beneath this Run.
		None
	}
}
