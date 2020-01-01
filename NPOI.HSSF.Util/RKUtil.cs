using System;

namespace NPOI.HSSF.Util
{
	/// Utility class for helping convert RK numbers.
	///
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @author Rolf-J黵gen Moll
	///
	/// @see org.apache.poi.hssf.record.MulRKRecord
	/// @see org.apache.poi.hssf.record.RKRecord
	public class RKUtil
	{
		/// Do the dirty work of decoding; made a private static method to
		/// facilitate testing the algorithm
		public static double DecodeNumber(int number)
		{
			long num = number;
			num >>= 2;
			double num2 = 0.0;
			num2 = (((number & 2) != 2) ? BitConverter.Int64BitsToDouble(num << 34) : ((double)num));
			if ((number & 1) == 1)
			{
				num2 /= 100.0;
			}
			return num2;
		}
	}
}
