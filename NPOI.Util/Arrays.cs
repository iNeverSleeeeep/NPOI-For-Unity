using System;
using System.Collections;
using System.Text;

namespace NPOI.Util
{
	public class Arrays
	{
		/// <summary>
		/// Fills the specified array.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="defaultValue">The default value.</param>
		public static void Fill(byte[] array, byte defaultValue)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = defaultValue;
			}
		}

		public static void Fill(char[] array, char defaultValue)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = defaultValue;
			}
		}

		public static void Fill<T>(T[] array, T defaultValue)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = defaultValue;
			}
		}

		/// <summary>
		/// Assigns the specified byte value to each element of the specified
		/// range of the specified array of bytes.  The range to be filled
		/// extends from index <tt>fromIndex</tt>, inclusive, to index
		/// <tt>toIndex</tt>, exclusive.  (If <tt>fromIndex==toIndex</tt>, the
		/// range to be filled is empty.)
		/// </summary>
		/// <param name="a">the array to be filled</param>
		/// <param name="fromIndex">the index of the first element (inclusive) to be filled with the specified value</param>
		/// <param name="toIndex">the index of the last element (exclusive) to be filled with the specified value</param>
		/// <param name="val">the value to be stored in all elements of the array</param>
		/// <exception cref="T:System.ArgumentException">if <c>fromIndex &gt; toIndex</c></exception>
		/// <exception cref="T:System.IndexOutOfRangeException"> if <c>fromIndex &lt; 0</c> or <c>toIndex &gt; a.length</c></exception>
		public static void Fill(byte[] a, int fromIndex, int toIndex, byte val)
		{
			RangeCheck(a.Length, fromIndex, toIndex);
			for (int i = fromIndex; i < toIndex; i++)
			{
				a[i] = val;
			}
		}

		/// <summary>
		/// Checks that {@code fromIndex} and {@code toIndex} are in
		/// the range and throws an appropriate exception, if they aren't.
		/// </summary>
		/// <param name="length"></param>
		/// <param name="fromIndex"></param>
		/// <param name="toIndex"></param>
		private static void RangeCheck(int length, int fromIndex, int toIndex)
		{
			if (fromIndex > toIndex)
			{
				throw new ArgumentException("fromIndex(" + fromIndex + ") > toIndex(" + toIndex + ")");
			}
			if (fromIndex < 0)
			{
				throw new IndexOutOfRangeException("fromIndex(" + fromIndex + ")");
			}
			if (toIndex > length)
			{
				throw new IndexOutOfRangeException("toIndex(" + toIndex + ")");
			}
		}

		/// <summary>
		/// Convert Array to ArrayList
		/// </summary>
		/// <param name="arr">source array</param>
		/// <returns></returns>
		public static ArrayList AsList(Array arr)
		{
			if (arr.Length <= 0)
			{
				return new ArrayList();
			}
			ArrayList arrayList = new ArrayList(arr.Length);
			for (int i = 0; i < arr.Length; i++)
			{
				arrayList.Add(arr.GetValue(i));
			}
			return arrayList;
		}

		/// <summary>
		/// Fills the specified array.
		/// </summary>
		/// <param name="array">The array.</param>
		/// <param name="defaultValue">The default value.</param>
		public static void Fill(int[] array, byte defaultValue)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = defaultValue;
			}
		}

		/// <summary>
		/// Equals the specified a1.
		/// </summary>
		/// <param name="a1">The a1.</param>
		/// <param name="b1">The b1.</param>
		/// <returns></returns>
		public new static bool Equals(object a1, object b1)
		{
			if (a1 == null || b1 == null)
			{
				return false;
			}
			Array array = a1 as Array;
			Array array2 = b1 as Array;
			if (array.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array.Length; i++)
			{
				if (!array.GetValue(i).Equals(array2.GetValue(i)))
				{
					return false;
				}
			}
			return true;
		}

		/// Returns <c>true</c> if the two specified arrays of Objects are
		/// <i>equal</i> to one another.  The two arrays are considered equal if
		/// both arrays contain the same number of elements, and all corresponding
		/// pairs of elements in the two arrays are equal.  Two objects <c>e1</c>
		/// and <c>e2</c> are considered <i>equal</i> if <c>(e1==null ? e2==null
		/// : e1.equals(e2))</c>.  In other words, the two arrays are equal if
		/// they contain the same elements in the same order.  Also, two array
		/// references are considered equal if both are <c>null</c>.
		///
		/// @param a one array to be tested for equality
		/// @param a2 the other array to be tested for equality
		/// @return <c>true</c> if the two arrays are equal
		public static bool Equals(object[] a, object[] a2)
		{
			if (a == a2)
			{
				return true;
			}
			if (a == null || a2 == null)
			{
				return false;
			}
			int num = a.Length;
			if (a2.Length != num)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				object obj = a[i];
				object obj2 = a2[i];
				if (!(obj?.Equals(obj2) ?? (obj2 == null)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Moves a number of entries in an array to another point in the array, shifting those inbetween as required.
		/// </summary>
		/// <param name="array">The array to alter</param>
		/// <param name="moveFrom">The (0 based) index of the first entry to move</param>
		/// <param name="moveTo">The (0 based) index of the positition to move to</param>
		/// <param name="numToMove">The number of entries to move</param>
		public static void ArrayMoveWithin(object[] array, int moveFrom, int moveTo, int numToMove)
		{
			if (numToMove > 0 && moveFrom != moveTo)
			{
				if (moveFrom < 0 || moveFrom >= array.Length)
				{
					throw new ArgumentException("The moveFrom must be a valid array index");
				}
				if (moveTo < 0 || moveTo >= array.Length)
				{
					throw new ArgumentException("The moveTo must be a valid array index");
				}
				if (moveFrom + numToMove > array.Length)
				{
					throw new ArgumentException("Asked to move more entries than the array has");
				}
				if (moveTo + numToMove > array.Length)
				{
					throw new ArgumentException("Asked to move to a position that doesn't have enough space");
				}
				object[] array2 = new object[numToMove];
				Array.Copy(array, moveFrom, array2, 0, numToMove);
				object[] array3;
				int destinationIndex;
				if (moveFrom > moveTo)
				{
					array3 = new object[moveFrom - moveTo];
					Array.Copy(array, moveTo, array3, 0, array3.Length);
					destinationIndex = moveTo + numToMove;
				}
				else
				{
					array3 = new object[moveTo - moveFrom];
					Array.Copy(array, moveFrom + numToMove, array3, 0, array3.Length);
					destinationIndex = moveFrom;
				}
				Array.Copy(array2, 0, array, moveTo, array2.Length);
				Array.Copy(array3, 0, array, destinationIndex, array3.Length);
			}
		}

		/// <summary>
		///  Copies the specified array, truncating or padding with zeros (if
		/// necessary) so the copy has the specified length. This method is temporary
		/// replace for Arrays.copyOf() until we start to require JDK 1.6.
		/// </summary>
		/// <param name="source">the array to be copied</param>
		/// <param name="newLength">the length of the copy to be returned</param>
		/// <returns>a copy of the original array, truncated or padded with zeros to obtain the specified length</returns>
		public static byte[] CopyOf(byte[] source, int newLength)
		{
			byte[] array = new byte[newLength];
			Array.Copy(source, 0, array, 0, Math.Min(source.Length, newLength));
			return array;
		}

		internal static int[] CopyOfRange(int[] original, int from, int to)
		{
			int num = to - from;
			if (num < 0)
			{
				throw new ArgumentException(from + " > " + to);
			}
			int[] array = new int[num];
			Array.Copy(original, from, array, 0, Math.Min(original.Length - from, num));
			return array;
		}

		internal static byte[] CopyOfRange(byte[] original, int from, int to)
		{
			int num = to - from;
			if (num < 0)
			{
				throw new ArgumentException(from + " > " + to);
			}
			byte[] array = new byte[num];
			Array.Copy(original, from, array, 0, Math.Min(original.Length - from, num));
			return array;
		}

		/// Returns a string representation of the contents of the specified array.
		/// If the array contains other arrays as elements, they are converted to
		/// strings by the {@link Object#toString} method inherited from
		/// <tt>Object</tt>, which describes their <i>identities</i> rather than
		/// their contents.
		///
		/// <p>The value returned by this method is equal to the value that would
		/// be returned by <tt>Arrays.asList(a).toString()</tt>, unless <tt>a</tt>
		/// is <tt>null</tt>, in which case <tt>"null"</tt> is returned.</p>
		///
		/// @param a the array whose string representation to return
		/// @return a string representation of <tt>a</tt>
		/// @see #deepToString(Object[])
		/// @since 1.5
		public static string ToString(object[] a)
		{
			if (a == null)
			{
				return "null";
			}
			int num = a.Length - 1;
			if (num == -1)
			{
				return "[]";
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('[');
			int num2 = 0;
			while (true)
			{
				stringBuilder.Append(a[num2].ToString());
				if (num2 == num)
				{
					break;
				}
				stringBuilder.Append(", ");
				num2++;
			}
			return stringBuilder.Append(']').ToString();
		}
	}
}
