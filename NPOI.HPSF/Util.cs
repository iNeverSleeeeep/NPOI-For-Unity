using System;
using System.Collections;

namespace NPOI.HPSF
{
	/// <summary>
	/// Provides various static utility methods.
	/// @author Rainer Klute (klute@rainer-klute.de)
	/// @since 2002-02-09
	/// </summary>
	public class Util
	{
		/// The difference between the Windows epoch (1601-01-01
		/// 00:00:00) and the Unix epoch (1970-01-01 00:00:00) in
		/// milliseconds: 11644473600000L. (Use your favorite spReadsheet
		/// program To verify the correctness of this value. By the way,
		/// did you notice that you can tell from the epochs which
		/// operating system is the modern one? :-))
		public static readonly long EPOCH_DIFF = new DateTime(1970, 1, 1).Ticks;

		/// <summary>
		/// Copies a part of a byte array into another byte array.
		/// </summary>
		/// <param name="src">The source byte array.</param>
		/// <param name="srcOffSet">OffSet in the source byte array.</param>
		/// <param name="Length">The number of bytes To Copy.</param>
		/// <param name="dst">The destination byte array.</param>
		/// <param name="dstOffSet">OffSet in the destination byte array.</param>
		public static void Copy(byte[] src, int srcOffSet, int Length, byte[] dst, int dstOffSet)
		{
			for (int i = 0; i < Length; i++)
			{
				dst[dstOffSet + i] = src[srcOffSet + i];
			}
		}

		/// <summary>
		/// Concatenates the contents of several byte arrays into a
		/// single one.
		/// </summary>
		/// <param name="byteArrays">The byte arrays To be conCatened.</param>
		/// <returns>A new byte array containing the conCatenated byte arrays.</returns>
		public static byte[] Cat(byte[][] byteArrays)
		{
			int num = 0;
			for (int i = 0; i < byteArrays.Length; i++)
			{
				num += byteArrays[i].Length;
			}
			byte[] array = new byte[num];
			int num2 = 0;
			for (int j = 0; j < byteArrays.Length; j++)
			{
				for (int k = 0; k < byteArrays[j].Length; k++)
				{
					array[num2++] = byteArrays[j][k];
				}
			}
			return array;
		}

		/// <summary>
		/// Copies bytes from a source byte array into a new byte
		/// array.
		/// </summary>
		/// <param name="src">Copy from this byte array.</param>
		/// <param name="offset">Start Copying here.</param>
		/// <param name="Length">Copy this many bytes.</param>
		/// <returns>The new byte array. Its Length is number of copied bytes.</returns>
		public static byte[] Copy(byte[] src, int offset, int Length)
		{
			byte[] array = new byte[Length];
			Copy(src, offset, Length, array, 0);
			return array;
		}

		/// <summary>
		/// Converts a Windows FILETIME into a {@link DateTime}. The Windows
		/// FILETIME structure holds a DateTime and time associated with a
		/// file. The structure identifies a 64-bit integer specifying the
		/// number of 100-nanosecond intervals which have passed since
		/// January 1, 1601. This 64-bit value is split into the two double
		/// words stored in the structure.
		/// </summary>
		/// <param name="high">The higher double word of the FILETIME structure.</param>
		/// <param name="low">The lower double word of the FILETIME structure.</param>
		/// <returns>The Windows FILETIME as a {@link DateTime}.</returns>
		public static DateTime FiletimeToDate(int high, int low)
		{
			long filetime = ((long)high << 32) | (low & uint.MaxValue);
			return FiletimeToDate(filetime);
		}

		/// <summary>
		/// Converts a Windows FILETIME into a {@link DateTime}. The Windows
		/// FILETIME structure holds a DateTime and time associated with a
		/// file. The structure identifies a 64-bit integer specifying the
		/// number of 100-nanosecond intervals which have passed since
		/// January 1, 1601.
		/// </summary>
		/// <param name="filetime">The filetime To Convert.</param>
		/// <returns>The Windows FILETIME as a {@link DateTime}.</returns>
		public static DateTime FiletimeToDate(long filetime)
		{
			return DateTime.FromFileTime(filetime);
		}

		/// <summary>
		/// Converts a {@link DateTime} into a filetime.
		/// </summary>
		/// <param name="dateTime">The DateTime To be Converted</param>
		/// <returns>The filetime</returns>
		public static long DateToFileTime(DateTime dateTime)
		{
			return dateTime.ToFileTime();
		}

		/// <summary>
		/// Compares To object arrays with regarding the objects' order. For
		/// example, [1, 2, 3] and [2, 1, 3] are equal.
		/// </summary>
		/// <param name="c1">The first object array.</param>
		/// <param name="c2">The second object array.</param>
		/// <returns><c>true</c>
		///  if the object arrays are equal,
		/// <c>false</c>
		///  if they are not.</returns>
		public static bool AreEqual(IList c1, IList c2)
		{
			return internalEquals(c1, c2);
		}

		/// <summary>
		/// Internals the equals.
		/// </summary>
		/// <param name="c1">The c1.</param>
		/// <param name="c2">The c2.</param>
		/// <returns></returns>
		private static bool internalEquals(IList c1, IList c2)
		{
			IEnumerator enumerator = c1.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object current = enumerator.Current;
				bool flag = false;
				IEnumerator enumerator2 = c2.GetEnumerator();
				while (!flag && enumerator2.MoveNext())
				{
					object current2 = enumerator2.Current;
					if (current.Equals(current2))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Pads a byte array with 0x00 bytes so that its Length is a multiple of
		/// 4.
		/// </summary>
		/// <param name="ba">The byte array To pad.</param>
		/// <returns>The padded byte array.</returns>
		public static byte[] Pad4(byte[] ba)
		{
			int num = 4;
			int num2 = ba.Length % num;
			byte[] array;
			if (num2 == 0)
			{
				array = ba;
			}
			else
			{
				num2 = num - num2;
				array = new byte[ba.Length + num2];
				System.Array.Copy(ba, array, ba.Length);
			}
			return array;
		}

		/// <summary>
		/// Pads a character array with 0x0000 characters so that its Length is a
		/// multiple of 4.
		/// </summary>
		/// <param name="ca">The character array To pad.</param>
		/// <returns>The padded character array.</returns>
		public static char[] Pad4(char[] ca)
		{
			int num = 4;
			int num2 = ca.Length % num;
			char[] array;
			if (num2 == 0)
			{
				array = ca;
			}
			else
			{
				num2 = num - num2;
				array = new char[ca.Length + num2];
				System.Array.Copy(ca, array, ca.Length);
			}
			return array;
		}

		/// <summary>
		/// Pads a string with 0x0000 characters so that its Length is a
		/// multiple of 4.
		/// </summary>
		/// <param name="s">The string To pad.</param>
		/// <returns> The padded string as a character array.</returns>
		public static char[] Pad4(string s)
		{
			return Pad4(s.ToCharArray());
		}
	}
}
