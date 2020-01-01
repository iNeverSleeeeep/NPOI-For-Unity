using System;
using System.Collections.Generic;

namespace NPOI.HSSF.Util
{
	/// Utility for delaying the concatenation of multiple byte arrays.  Doing this up-front
	/// causes significantly more copying, which for a large number of byte arrays can cost
	/// a large amount of time.
	public class LazilyConcatenatedByteArray
	{
		private List<byte[]> arrays = new List<byte[]>(1);

		/// Clears the array (sets the concatenated length back to zero.
		public void Clear()
		{
			arrays.Clear();
		}

		/// Concatenates an array onto the end of our array.
		/// This is a relatively fast operation.
		///
		/// @param array the array to concatenate.
		/// @throws ArgumentException if {@code array} is {@code null}.
		public void Concatenate(byte[] array)
		{
			if (array == null)
			{
				throw new ArgumentException("array cannot be null");
			}
			arrays.Add(array);
		}

		/// Gets the concatenated contents as a single byte array.
		///
		/// This is a slower operation, but the concatenated array is stored off as a single
		/// array again so that subsequent calls will not perform Additional copying.
		///
		/// @return the byte array.  Returns {@code null} if no data has been placed into it.
		public byte[] ToArray()
		{
			if (arrays.Count == 0)
			{
				return null;
			}
			if (arrays.Count > 1)
			{
				int num = 0;
				foreach (byte[] array2 in arrays)
				{
					num += array2.Length;
				}
				byte[] array = new byte[num];
				int num2 = 0;
				foreach (byte[] array3 in arrays)
				{
					Array.Copy(array3, 0, array, num2, array3.Length);
					num2 += array3.Length;
				}
				arrays.Clear();
				arrays.Add(array);
			}
			return arrays[0];
		}
	}
}
