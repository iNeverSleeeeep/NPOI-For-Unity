using System;

namespace NPOI.Util
{
	/// @author Yegor Kozlov
	public class Units
	{
		public static int EMU_PER_PIXEL = 9525;

		public static int EMU_PER_POINT = 12700;

		public static int ToEMU(double value)
		{
			return (int)Math.Round((double)EMU_PER_POINT * value);
		}

		public static double ToPoints(long emu)
		{
			return (double)emu / (double)EMU_PER_POINT;
		}
	}
}
