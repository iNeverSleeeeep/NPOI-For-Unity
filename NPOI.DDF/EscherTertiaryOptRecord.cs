namespace NPOI.DDF
{
	/// "The OfficeArtTertiaryFOPT record specifies a table of OfficeArtRGFOPTE properties, as defined in section 2.3.1."
	/// -- [MS-ODRAW] -- v20110608; Office Drawing Binary File Format
	///
	/// @author Sergey Vladimirov (vlsergey {at} gmail {dot} com)
	public class EscherTertiaryOptRecord : AbstractEscherOptRecord
	{
		public const short RECORD_ID = -3806;

		public override string RecordName => "TertiaryOpt";
	}
}
