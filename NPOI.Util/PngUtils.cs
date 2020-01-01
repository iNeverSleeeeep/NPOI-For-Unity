namespace NPOI.Util
{
	public class PngUtils
	{
		/// File header for PNG format.
		private static byte[] PNG_FILE_HEADER = new byte[8]
		{
			137,
			80,
			78,
			71,
			13,
			10,
			26,
			10
		};

		private PngUtils()
		{
		}

		/// Checks if the offset matches the PNG header.
		///
		/// @param data the data to check.
		/// @param offset the offset to check at.
		/// @return {@code true} if the offset matches.
		public static bool MatchesPngHeader(byte[] data, int offset)
		{
			if (data == null || data.Length - offset < PNG_FILE_HEADER.Length)
			{
				return false;
			}
			for (int i = 0; i < PNG_FILE_HEADER.Length; i++)
			{
				if (PNG_FILE_HEADER[i] != data[i + offset])
				{
					return false;
				}
			}
			return true;
		}
	}
}
