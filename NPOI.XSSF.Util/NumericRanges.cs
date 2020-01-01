namespace NPOI.XSSF.Util
{
	public class NumericRanges
	{
		public const int NO_OVERLAPS = -1;

		public const int OVERLAPS_1_MINOR = 0;

		public const int OVERLAPS_2_MINOR = 1;

		public const int OVERLAPS_1_WRAPS = 2;

		public const int OVERLAPS_2_WRAPS = 3;

		public static long[] GetOverlappingRange(long[] range1, long[] range2)
		{
			switch (GetOverlappingType(range1, range2))
			{
			case 0:
				return new long[2]
				{
					range2[0],
					range1[1]
				};
			case 1:
				return new long[2]
				{
					range1[0],
					range2[1]
				};
			case 3:
				return range1;
			case 2:
				return range2;
			default:
				return new long[2]
				{
					-1L,
					-1L
				};
			}
		}

		public static int GetOverlappingType(long[] range1, long[] range2)
		{
			long num = range1[0];
			long num2 = range1[1];
			long num3 = range2[0];
			long num4 = range2[1];
			if (num >= num3 && num2 <= num4)
			{
				return 3;
			}
			if (num3 >= num && num4 <= num2)
			{
				return 2;
			}
			if (num3 >= num && num3 <= num2 && num4 >= num2)
			{
				return 0;
			}
			if (num >= num3 && num <= num4 && num2 >= num4)
			{
				return 1;
			}
			return -1;
		}
	}
}
