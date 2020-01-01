namespace NPOI.SS.Util
{
	public class IEEEDouble
	{
		private const long EXPONENT_MASK = 9218868437227405312L;

		private const int EXPONENT_SHIFT = 52;

		public const long FRAC_MASK = 4503599627370495L;

		public const int EXPONENT_BIAS = 1023;

		public const long FRAC_ASSUMED_HIGH_BIT = 4503599627370496L;

		/// The value the exponent field Gets for all <i>NaN</i> and <i>InfInity</i> values
		public const int BIASED_EXPONENT_SPECIAL_VALUE = 2047;

		/// @param rawBits the 64 bit binary representation of the double value
		/// @return the top 12 bits (sign and biased exponent value)
		public static int GetBiasedExponent(long rawBits)
		{
			return (int)((rawBits & 0x7FF0000000000000) >> 52);
		}
	}
}
