namespace NPOI.Util
{
	public class Character
	{
		public static int GetNumericValue(char src)
		{
			if (src >= '0' && src <= '9')
			{
				return src;
			}
			if (src >= 'A' && src <= 'Z')
			{
				return src - 55;
			}
			if (src >= 'a' && src <= 'z')
			{
				return src - 87;
			}
			return -1;
		}
	}
}
