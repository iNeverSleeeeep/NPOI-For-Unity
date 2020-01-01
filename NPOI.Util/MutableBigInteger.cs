using System;

namespace NPOI.Util
{
	internal class MutableBigInteger
	{
		private const long LONG_MASK = 4294967295L;

		private const long INFLATED = long.MinValue;

		/// Holds the magnitude of this MutableBigInteger in big endian order.
		/// The magnitude may start at an offset into the value array, and it may
		/// end before the length of the value array.
		private int[] _value;

		/// The number of ints of the value array that are currently used
		/// to hold the magnitude of this MutableBigInteger. The magnitude starts
		/// at an offset and offset + intLen may be less than value.Length.
		private int intLen;

		/// The offset into the value array where the magnitude of this
		/// MutableBigInteger begins.
		private int offset;

		/// MutableBigInteger with one element value array with the value 1. Used by
		/// BigDecimal divideAndRound to increment the quotient. Use this constant
		/// only when the method is not going to modify this object.
		private static readonly MutableBigInteger One = new MutableBigInteger(1);

		/// The default constructor. An empty MutableBigInteger is created with
		/// a one word capacity.
		public MutableBigInteger()
		{
			_value = new int[1];
			intLen = 0;
		}

		/// Construct a new MutableBigInteger with a magnitude specified by
		/// the int val.
		public MutableBigInteger(int val)
		{
			_value = new int[1];
			intLen = 1;
			_value[0] = val;
		}

		/// Construct a new MutableBigInteger with the specified value array
		/// up to the length of the array supplied.
		public MutableBigInteger(int[] val)
		{
			_value = val;
			intLen = val.Length;
		}

		public static int[] ArraysCopyOf(int[] original, int newLength)
		{
			int[] array = new int[newLength];
			Array.Copy(original, 0, array, 0, Math.Min(original.Length, newLength));
			return array;
		}

		public static long[] ArraysCopyOf(long[] original, int newLength)
		{
			long[] array = new long[newLength];
			Array.Copy(original, 0, array, 0, Math.Min(original.Length, newLength));
			return array;
		}

		public static int[] ArraysCopyOfRange(int[] original, int from, int to)
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

		public static long[] ArraysCopyOfRange(long[] original, int from, int to)
		{
			int num = to - from;
			if (num < 0)
			{
				throw new ArgumentException(from + " > " + to);
			}
			long[] array = new long[num];
			Array.Copy(original, from, array, 0, Math.Min(original.Length - from, num));
			return array;
		}

		/// Construct a new MutableBigInteger with a magnitude equal to the
		/// specified BigInteger.
		private MutableBigInteger(BigInteger b)
		{
			intLen = b.mag.Length;
			_value = ArraysCopyOf(b.mag, intLen);
		}

		/// Construct a new MutableBigInteger with a magnitude equal to the
		/// specified MutableBigInteger.
		private MutableBigInteger(MutableBigInteger val)
		{
			intLen = val.intLen;
			_value = ArraysCopyOfRange(val._value, val.offset, val.offset + intLen);
		}

		/// Internal helper method to return the magnitude array. The caller is not
		/// supposed to modify the returned array.
		private int[] getMagnitudeArray()
		{
			if (offset > 0 || _value.Length != intLen)
			{
				return ArraysCopyOfRange(_value, offset, offset + intLen);
			}
			return _value;
		}

		/// Convert this MutableBigInteger to a long value. The caller has to make
		/// sure this MutableBigInteger can be fit into long.
		private long toLong()
		{
			if (intLen == 0)
			{
				return 0L;
			}
			long num = _value[offset] & uint.MaxValue;
			if (intLen != 2)
			{
				return num;
			}
			return (num << 32) | (_value[offset + 1] & uint.MaxValue);
		}

		/// Convert this MutableBigInteger to a BigInteger object.
		public BigInteger toBigInteger(int sign)
		{
			if (intLen == 0 || sign == 0)
			{
				return BigInteger.ZERO;
			}
			return new BigInteger(getMagnitudeArray(), sign);
		}

		/// Clear out a MutableBigInteger for reuse.
		private void clear()
		{
			offset = (intLen = 0);
			int i = 0;
			for (int num = _value.Length; i < num; i++)
			{
				_value[i] = 0;
			}
		}

		/// Set a MutableBigInteger to zero, removing its offset.
		private void reset()
		{
			offset = (intLen = 0);
		}

		/// Compare the magnitude of two MutableBigIntegers. Returns -1, 0 or 1
		/// as this MutableBigInteger is numerically less than, equal to, or
		/// greater than <c>b</c>.
		private int compare(MutableBigInteger b)
		{
			int num = b.intLen;
			if (intLen < num)
			{
				return -1;
			}
			if (intLen > num)
			{
				return 1;
			}
			int[] value = b._value;
			int num2 = offset;
			int num3 = b.offset;
			while (num2 < intLen + offset)
			{
				int num4 = (int)(_value[num2] + 2147483648u);
				int num5 = (int)(value[num3] + 2147483648u);
				if (num4 < num5)
				{
					return -1;
				}
				if (num4 > num5)
				{
					return 1;
				}
				num2++;
				num3++;
			}
			return 0;
		}

		/// Compare this against half of a MutableBigInteger object (Needed for
		/// remainder tests).
		/// Assumes no leading unnecessary zeros, which holds for results
		/// from divide().
		private int compareHalf(MutableBigInteger b)
		{
			int num = b.intLen;
			int num2 = intLen;
			if (num2 <= 0)
			{
				if (num > 0)
				{
					return -1;
				}
				return 0;
			}
			if (num2 > num)
			{
				return 1;
			}
			if (num2 < num - 1)
			{
				return -1;
			}
			int[] value = b._value;
			int num3 = 0;
			int num4 = 0;
			if (num2 != num)
			{
				if (value[num3] != 1)
				{
					return -1;
				}
				num3++;
				num4 = -2147483648;
			}
			int[] value2 = _value;
			int num5 = offset;
			int num6 = num3;
			while (num5 < num2 + offset)
			{
				int num8 = value[num6++];
				long num9 = (Operator.UnsignedRightShift(num8, 1) + num4) & uint.MaxValue;
				long num11 = value2[num5++] & uint.MaxValue;
				if (num11 != num9)
				{
					if (num11 >= num9)
					{
						return 1;
					}
					return -1;
				}
				num4 = (num8 & 1) << 31;
			}
			if (num4 != 0)
			{
				return -1;
			}
			return 0;
		}

		/// Return the index of the lowest set bit in this MutableBigInteger. If the
		/// magnitude of this MutableBigInteger is zero, -1 is returned.
		private int getLowestSetBit()
		{
			if (intLen == 0)
			{
				return -1;
			}
			int num = intLen - 1;
			while (num > 0 && _value[num + offset] == 0)
			{
				num--;
			}
			int num2 = _value[num + offset];
			if (num2 == 0)
			{
				return -1;
			}
			return (intLen - 1 - num << 5) + BigInteger.NumberOfTrailingZeros(num2);
		}

		/// Return the int in use in this MutableBigInteger at the specified
		/// index. This method is not used because it is not inlined on all
		/// platforms.
		private int getInt(int index)
		{
			return _value[offset + index];
		}

		/// Return a long which is equal to the unsigned value of the int in
		/// use in this MutableBigInteger at the specified index. This method is
		/// not used because it is not inlined on all platforms.
		private long getLong(int index)
		{
			return _value[offset + index] & uint.MaxValue;
		}

		/// Ensure that the MutableBigInteger is in normal form, specifically
		/// making sure that there are no leading zeros, and that if the
		/// magnitude is zero, then intLen is zero.
		private void normalize()
		{
			if (intLen == 0)
			{
				offset = 0;
			}
			else
			{
				int num = offset;
				if (_value[num] == 0)
				{
					int num2 = num + intLen;
					do
					{
						num++;
					}
					while (num < num2 && _value[num] == 0);
					int num3 = num - offset;
					intLen -= num3;
					offset = ((intLen != 0) ? (offset + num3) : 0);
				}
			}
		}

		/// If this MutableBigInteger cannot hold len words, increase the size
		/// of the value array to len words.
		private void ensureCapacity(int len)
		{
			if (_value.Length < len)
			{
				_value = new int[len];
				offset = 0;
				intLen = len;
			}
		}

		/// Convert this MutableBigInteger into an int array with no leading
		/// zeros, of a length that is equal to this MutableBigInteger's intLen.
		private int[] toIntArray()
		{
			int[] array = new int[intLen];
			for (int i = 0; i < intLen; i++)
			{
				array[i] = _value[offset + i];
			}
			return array;
		}

		/// Sets the int at index+offset in this MutableBigInteger to val.
		/// This does not get inlined on all platforms so it is not used
		/// as often as originally intended.
		private void setInt(int index, int val)
		{
			_value[offset + index] = val;
		}

		/// Sets this MutableBigInteger's value array to the specified array.
		/// The intLen is set to the specified length.
		private void setValue(int[] val, int length)
		{
			_value = val;
			intLen = length;
			offset = 0;
		}

		/// Sets this MutableBigInteger's value array to a copy of the specified
		/// array. The intLen is set to the length of the new array.
		private void copyValue(MutableBigInteger src)
		{
			int num = src.intLen;
			if (_value.Length < num)
			{
				_value = new int[num];
			}
			Array.Copy(src._value, src.offset, _value, 0, num);
			intLen = num;
			offset = 0;
		}

		/// Sets this MutableBigInteger's value array to a copy of the specified
		/// array. The intLen is set to the length of the specified array.
		private void copyValue(int[] val)
		{
			int num = val.Length;
			if (_value.Length < num)
			{
				_value = new int[num];
			}
			Array.Copy(val, 0, _value, 0, num);
			intLen = num;
			offset = 0;
		}

		/// Returns true iff this MutableBigInteger has a value of one.
		private bool isOne()
		{
			if (intLen == 1)
			{
				return _value[offset] == 1;
			}
			return false;
		}

		/// Returns true iff this MutableBigInteger has a value of zero.
		private bool isZero()
		{
			return intLen == 0;
		}

		/// Returns true iff this MutableBigInteger is even.
		private bool isEven()
		{
			if (intLen != 0)
			{
				return (_value[offset + intLen - 1] & 1) == 0;
			}
			return true;
		}

		/// Returns true iff this MutableBigInteger is odd.
		private bool isOdd()
		{
			if (!isZero())
			{
				return (_value[offset + intLen - 1] & 1) == 1;
			}
			return false;
		}

		/// Returns true iff this MutableBigInteger is in normal form. A
		/// MutableBigInteger is in normal form if it has no leading zeros
		/// after the offset, and intLen + offset &lt;= value.Length.
		private bool isNormal()
		{
			if (intLen + offset > _value.Length)
			{
				return false;
			}
			if (intLen == 0)
			{
				return true;
			}
			return _value[offset] != 0;
		}

		/// Returns a String representation of this MutableBigInteger in radix 10.
		public string toString()
		{
			BigInteger bigInteger = toBigInteger(1);
			return bigInteger.ToString();
		}

		/// Right shift this MutableBigInteger n bits. The MutableBigInteger is left
		/// in normal form.
		private void rightShift(int n)
		{
			if (intLen != 0)
			{
				int num = Operator.UnsignedRightShift(n, 5);
				int num2 = n & 0x1F;
				intLen -= num;
				if (num2 != 0)
				{
					int num3 = BigInteger.BitLengthForInt(_value[offset]);
					if (num2 >= num3)
					{
						primitiveLeftShift(32 - num2);
						intLen--;
					}
					else
					{
						primitiveRightShift(num2);
					}
				}
			}
		}

		/// Left shift this MutableBigInteger n bits.
		private void leftShift(int n)
		{
			if (intLen != 0)
			{
				int num = Operator.UnsignedRightShift(n, 5);
				int num2 = n & 0x1F;
				int num3 = BigInteger.BitLengthForInt(_value[offset]);
				if (n <= 32 - num3)
				{
					primitiveLeftShift(num2);
				}
				else
				{
					int num4 = intLen + num + 1;
					if (num2 <= 32 - num3)
					{
						num4--;
					}
					if (_value.Length < num4)
					{
						int[] array = new int[num4];
						for (int i = 0; i < intLen; i++)
						{
							array[i] = _value[offset + i];
						}
						setValue(array, num4);
					}
					else if (_value.Length - offset >= num4)
					{
						for (int j = 0; j < num4 - intLen; j++)
						{
							_value[offset + intLen + j] = 0;
						}
					}
					else
					{
						for (int k = 0; k < intLen; k++)
						{
							_value[k] = _value[offset + k];
						}
						for (int l = intLen; l < num4; l++)
						{
							_value[l] = 0;
						}
						offset = 0;
					}
					intLen = num4;
					if (num2 != 0)
					{
						if (num2 <= 32 - num3)
						{
							primitiveLeftShift(num2);
						}
						else
						{
							primitiveRightShift(32 - num2);
						}
					}
				}
			}
		}

		/// A primitive used for division. This method adds in one multiple of the
		/// divisor a back to the dividend result at a specified offset. It is used
		/// when qhat was estimated too large, and must be adjusted.
		private int divadd(int[] a, int[] result, int offset)
		{
			long num = 0L;
			for (int num2 = a.Length - 1; num2 >= 0; num2--)
			{
				long num3 = (a[num2] & uint.MaxValue) + (result[num2 + offset] & uint.MaxValue) + num;
				result[num2 + offset] = (int)num3;
				num = Operator.UnsignedRightShift(num3, 32);
			}
			return (int)num;
		}

		/// This method is used for division. It multiplies an n word input a by one
		/// word input x, and subtracts the n word product from q. This is needed
		/// when subtracting qhat*divisor from dividend.
		private int mulsub(int[] q, int[] a, int x, int len, int offset)
		{
			long num = x & uint.MaxValue;
			long num2 = 0L;
			offset += len;
			for (int num3 = len - 1; num3 >= 0; num3--)
			{
				long num4 = (a[num3] & uint.MaxValue) * num + num2;
				long num5 = q[offset] - num4;
				q[offset--] = (int)num5;
				num2 = Operator.UnsignedRightShift(num4, 32) + (((num5 & uint.MaxValue) > ((int)(~num4) & uint.MaxValue)) ? 1 : 0);
			}
			return (int)num2;
		}

		/// Right shift this MutableBigInteger n bits, where n is
		/// less than 32.
		/// Assumes that intLen &gt; 0, n &gt; 0 for speed
		private void primitiveRightShift(int n)
		{
			int[] value = _value;
			int num = 32 - n;
			int num2 = offset + intLen - 1;
			int num3 = value[num2];
			while (num2 > offset)
			{
				int operand = num3;
				num3 = value[num2 - 1];
				value[num2] = ((num3 << num) | Operator.UnsignedRightShift(operand, n));
				num2--;
			}
			value[offset] = Operator.UnsignedRightShift(value[offset], n);
		}

		/// Left shift this MutableBigInteger n bits, where n is
		/// less than 32.
		/// Assumes that intLen &gt; 0, n &gt; 0 for speed
		private void primitiveLeftShift(int n)
		{
			int[] value = _value;
			int val = 32 - n;
			int i = offset;
			int num = value[i];
			for (int num2 = i + intLen - 1; i < num2; i++)
			{
				int num3 = num;
				num = value[i + 1];
				value[i] = ((num3 << n) | Operator.UnsignedRightShift(num, val));
			}
			value[offset + intLen - 1] <<= n;
		}

		/// Adds the contents of two MutableBigInteger objects.The result
		/// is placed within this MutableBigInteger.
		/// The contents of the addend are not changed.
		private void add(MutableBigInteger addend)
		{
			int num = intLen;
			int num2 = addend.intLen;
			int num3 = (intLen > addend.intLen) ? intLen : addend.intLen;
			int[] array = (_value.Length < num3) ? new int[num3] : _value;
			int num4 = array.Length - 1;
			long num5 = 0L;
			while (num > 0 && num2 > 0)
			{
				num--;
				num2--;
				long num6 = (_value[num + offset] & uint.MaxValue) + (addend._value[num2 + addend.offset] & uint.MaxValue) + num5;
				array[num4--] = (int)num6;
				num5 = Operator.UnsignedRightShift(num6, 32);
			}
			while (num > 0)
			{
				num--;
				if (num5 == 0 && array == _value && num4 == num + offset)
				{
					return;
				}
				long num6 = (_value[num + offset] & uint.MaxValue) + num5;
				array[num4--] = (int)num6;
				num5 = Operator.UnsignedRightShift(num6, 32);
			}
			while (num2 > 0)
			{
				num2--;
				long num6 = (addend._value[num2 + addend.offset] & uint.MaxValue) + num5;
				array[num4--] = (int)num6;
				num5 = Operator.UnsignedRightShift(num6, 32);
			}
			if (num5 > 0)
			{
				num3++;
				if (array.Length < num3)
				{
					int[] array2 = new int[num3];
					Array.Copy(array, 0, array2, 1, array.Length);
					array2[0] = 1;
					array = array2;
				}
				else
				{
					array[num4--] = 1;
				}
			}
			_value = array;
			intLen = num3;
			offset = array.Length - num3;
		}

		/// Subtracts the smaller of this and b from the larger and places the
		/// result into this MutableBigInteger.
		private int subtract(MutableBigInteger b)
		{
			MutableBigInteger mutableBigInteger = this;
			int[] array = _value;
			int num = mutableBigInteger.compare(b);
			if (num == 0)
			{
				reset();
				return 0;
			}
			if (num < 0)
			{
				MutableBigInteger mutableBigInteger2 = mutableBigInteger;
				mutableBigInteger = b;
				b = mutableBigInteger2;
			}
			int num2 = mutableBigInteger.intLen;
			if (array.Length < num2)
			{
				array = new int[num2];
			}
			long num3 = 0L;
			int num4 = mutableBigInteger.intLen;
			int num5 = b.intLen;
			int num6 = array.Length - 1;
			while (num5 > 0)
			{
				num4--;
				num5--;
				num3 = (mutableBigInteger._value[num4 + mutableBigInteger.offset] & uint.MaxValue) - (b._value[num5 + b.offset] & uint.MaxValue) - (int)(-(num3 >> 32));
				array[num6--] = (int)num3;
			}
			while (num4 > 0)
			{
				num4--;
				num3 = (mutableBigInteger._value[num4 + mutableBigInteger.offset] & uint.MaxValue) - (int)(-(num3 >> 32));
				array[num6--] = (int)num3;
			}
			_value = array;
			intLen = num2;
			offset = _value.Length - num2;
			normalize();
			return num;
		}

		/// Subtracts the smaller of a and b from the larger and places the result
		/// into the larger. Returns 1 if the answer is in a, -1 if in b, 0 if no
		/// operation was performed.
		private int difference(MutableBigInteger b)
		{
			MutableBigInteger mutableBigInteger = this;
			int num = mutableBigInteger.compare(b);
			if (num == 0)
			{
				return 0;
			}
			if (num < 0)
			{
				MutableBigInteger mutableBigInteger2 = mutableBigInteger;
				mutableBigInteger = b;
				b = mutableBigInteger2;
			}
			long num2 = 0L;
			int num3 = mutableBigInteger.intLen;
			int num4 = b.intLen;
			while (num4 > 0)
			{
				num3--;
				num4--;
				num2 = (mutableBigInteger._value[mutableBigInteger.offset + num3] & uint.MaxValue) - (b._value[b.offset + num4] & uint.MaxValue) - (int)(-(num2 >> 32));
				mutableBigInteger._value[mutableBigInteger.offset + num3] = (int)num2;
			}
			while (num3 > 0)
			{
				num3--;
				num2 = (mutableBigInteger._value[mutableBigInteger.offset + num3] & uint.MaxValue) - (int)(-(num2 >> 32));
				mutableBigInteger._value[mutableBigInteger.offset + num3] = (int)num2;
			}
			mutableBigInteger.normalize();
			return num;
		}

		/// Multiply the contents of two MutableBigInteger objects. The result is
		/// placed into MutableBigInteger z. The contents of y are not changed.
		private void multiply(MutableBigInteger y, MutableBigInteger z)
		{
			int num = intLen;
			int num2 = y.intLen;
			int num3 = num + num2;
			if (z._value.Length < num3)
			{
				z._value = new int[num3];
			}
			z.offset = 0;
			z.intLen = num3;
			long num4 = 0L;
			int num5 = num2 - 1;
			int num6 = num2 + num - 1;
			while (num5 >= 0)
			{
				long num7 = (y._value[num5 + y.offset] & uint.MaxValue) * (_value[num - 1 + offset] & uint.MaxValue) + num4;
				z._value[num6] = (int)num7;
				num4 = Operator.UnsignedRightShift(num7, 32);
				num5--;
				num6--;
			}
			z._value[num - 1] = (int)num4;
			for (int num8 = num - 2; num8 >= 0; num8--)
			{
				num4 = 0L;
				int num9 = num2 - 1;
				int num10 = num2 + num8;
				while (num9 >= 0)
				{
					long num11 = (y._value[num9 + y.offset] & uint.MaxValue) * (_value[num8 + offset] & uint.MaxValue) + (z._value[num10] & uint.MaxValue) + num4;
					z._value[num10] = (int)num11;
					num4 = Operator.UnsignedRightShift(num11, 32);
					num9--;
					num10--;
				}
				z._value[num8] = (int)num4;
			}
			z.normalize();
		}

		/// Multiply the contents of this MutableBigInteger by the word y. The
		/// result is placed into z.
		public void mul(int y, MutableBigInteger z)
		{
			switch (y)
			{
			case 1:
				z.copyValue(this);
				break;
			case 0:
				z.clear();
				break;
			default:
			{
				long num = y & uint.MaxValue;
				int[] array = (z._value.Length < intLen + 1) ? new int[intLen + 1] : z._value;
				long num2 = 0L;
				for (int num3 = intLen - 1; num3 >= 0; num3--)
				{
					long num4 = num * (_value[num3 + offset] & uint.MaxValue) + num2;
					array[num3 + 1] = (int)num4;
					num2 = Operator.UnsignedRightShift(num4, 32);
				}
				if (num2 == 0)
				{
					z.offset = 1;
					z.intLen = intLen;
				}
				else
				{
					z.offset = 0;
					z.intLen = intLen + 1;
					array[0] = (int)num2;
				}
				z._value = array;
				break;
			}
			}
		}

		/// This method is used for division of an n word dividend by a one word
		/// divisor. The quotient is placed into quotient. The one word divisor is
		/// specified by divisor.
		///
		/// @return the remainder of the division is returned.
		public int divideOneWord(int divisor, MutableBigInteger quotient)
		{
			long num = divisor & uint.MaxValue;
			if (intLen == 1)
			{
				long num2 = _value[offset] & uint.MaxValue;
				int num3 = (int)(num2 / num);
				int result = (int)(num2 - num3 * num);
				quotient._value[0] = num3;
				quotient.intLen = ((num3 != 0) ? 1 : 0);
				quotient.offset = 0;
				return result;
			}
			if (quotient._value.Length < intLen)
			{
				quotient._value = new int[intLen];
			}
			quotient.offset = 0;
			quotient.intLen = intLen;
			int num4 = BigInteger.NumberOfLeadingZeros(divisor);
			int num5 = _value[offset];
			long num6 = num5 & uint.MaxValue;
			if (num6 < num)
			{
				quotient._value[0] = 0;
			}
			else
			{
				quotient._value[0] = (int)(num6 / num);
				num5 = (int)(num6 - quotient._value[0] * num);
				num6 = (num5 & uint.MaxValue);
			}
			int num7 = intLen;
			int[] array = new int[2];
			while (--num7 > 0)
			{
				long num8 = (num6 << 32) | (_value[offset + intLen - num7] & uint.MaxValue);
				if (num8 >= 0)
				{
					array[0] = (int)(num8 / num);
					array[1] = (int)(num8 - array[0] * num);
				}
				else
				{
					divWord(array, num8, divisor);
				}
				quotient._value[intLen - num7] = array[0];
				num5 = array[1];
				num6 = (num5 & uint.MaxValue);
			}
			quotient.normalize();
			if (num4 > 0)
			{
				return num5 % divisor;
			}
			return num5;
		}

		/// Calculates the quotient of this div b and places the quotient in the
		/// provided MutableBigInteger objects and the remainder object is returned.
		///
		/// Uses Algorithm D in Knuth section 4.3.1.
		/// Many optimizations to that algorithm have been adapted from the Colin
		/// Plumb C library.
		/// It special cases one word divisors for speed. The content of b is not
		/// changed.
		public MutableBigInteger divide(MutableBigInteger b, MutableBigInteger quotient)
		{
			if (b.intLen == 0)
			{
				throw new ArithmeticException("BigInteger divide by zero");
			}
			if (intLen == 0)
			{
				quotient.intLen = quotient.offset;
				return new MutableBigInteger();
			}
			int num = compare(b);
			if (num < 0)
			{
				quotient.intLen = (quotient.offset = 0);
				return new MutableBigInteger(this);
			}
			if (num == 0)
			{
				quotient._value[0] = (quotient.intLen = 1);
				quotient.offset = 0;
				return new MutableBigInteger();
			}
			quotient.clear();
			if (b.intLen == 1)
			{
				int num2 = divideOneWord(b._value[b.offset], quotient);
				if (num2 == 0)
				{
					return new MutableBigInteger();
				}
				return new MutableBigInteger(num2);
			}
			int[] divisor = ArraysCopyOfRange(b._value, b.offset, b.offset + b.intLen);
			return divideMagnitude(divisor, quotient);
		}

		/// Internally used  to calculate the quotient of this div v and places the
		/// quotient in the provided MutableBigInteger object and the remainder is
		/// returned.
		///
		/// @return the remainder of the division will be returned.
		public long divide(long v, MutableBigInteger quotient)
		{
			if (v == 0)
			{
				throw new ArithmeticException("BigInteger divide by zero");
			}
			if (intLen == 0)
			{
				quotient.intLen = (quotient.offset = 0);
				return 0L;
			}
			if (v < 0)
			{
				v = -v;
			}
			int num = (int)Operator.UnsignedRightShift(v, 32);
			quotient.clear();
			if (num == 0)
			{
				return divideOneWord((int)v, quotient) & uint.MaxValue;
			}
			int[] divisor = new int[2]
			{
				num,
				(int)(v & uint.MaxValue)
			};
			return divideMagnitude(divisor, quotient).toLong();
		}

		/// Divide this MutableBigInteger by the divisor represented by its magnitude
		/// array. The quotient will be placed into the provided quotient object &amp;
		/// the remainder object is returned.
		private MutableBigInteger divideMagnitude(int[] divisor, MutableBigInteger quotient)
		{
			MutableBigInteger mutableBigInteger = new MutableBigInteger(new int[intLen + 1]);
			Array.Copy(_value, offset, mutableBigInteger._value, 1, intLen);
			mutableBigInteger.intLen = intLen;
			mutableBigInteger.offset = 1;
			int num = mutableBigInteger.intLen;
			int num2 = divisor.Length;
			int num3 = num - num2 + 1;
			if (quotient._value.Length < num3)
			{
				quotient._value = new int[num3];
				quotient.offset = 0;
			}
			quotient.intLen = num3;
			int[] value = quotient._value;
			int num4 = BigInteger.NumberOfLeadingZeros(divisor[0]);
			if (num4 > 0)
			{
				BigInteger.PrimitiveLeftShift(divisor, num2, num4);
				mutableBigInteger.leftShift(num4);
			}
			if (mutableBigInteger.intLen == num)
			{
				mutableBigInteger.offset = 0;
				mutableBigInteger._value[0] = 0;
				mutableBigInteger.intLen++;
			}
			int num5 = divisor[0];
			long num6 = num5 & uint.MaxValue;
			int num7 = divisor[1];
			int[] array = new int[2];
			for (int i = 0; i < num3; i++)
			{
				int num8 = 0;
				int num9 = 0;
				bool flag = false;
				int num10 = mutableBigInteger._value[i + mutableBigInteger.offset];
				int num11 = (int)(num10 + 2147483648u);
				int num12 = mutableBigInteger._value[i + 1 + mutableBigInteger.offset];
				if (num10 == num5)
				{
					num8 = -1;
					num9 = num10 + num12;
					flag = (num9 + 2147483648u < num11);
				}
				else
				{
					long num13 = ((long)num10 << 32) | (num12 & uint.MaxValue);
					if (num13 >= 0)
					{
						num8 = (int)(num13 / num6);
						num9 = (int)(num13 - num8 * num6);
					}
					else
					{
						divWord(array, num13, num5);
						num8 = array[0];
						num9 = array[1];
					}
				}
				if (num8 != 0)
				{
					if (!flag)
					{
						long num14 = mutableBigInteger._value[i + 2 + mutableBigInteger.offset] & uint.MaxValue;
						long two = ((num9 & uint.MaxValue) << 32) | num14;
						long num15 = (num7 & uint.MaxValue) * (num8 & uint.MaxValue);
						if (unsignedLongCompare(num15, two))
						{
							num8--;
							num9 = (int)((num9 & uint.MaxValue) + num6);
							if ((num9 & uint.MaxValue) >= num6)
							{
								num15 -= (num7 & uint.MaxValue);
								two = (((num9 & uint.MaxValue) << 32) | num14);
								if (unsignedLongCompare(num15, two))
								{
									num8--;
								}
							}
						}
					}
					mutableBigInteger._value[i + mutableBigInteger.offset] = 0;
					int num16 = mulsub(mutableBigInteger._value, divisor, num8, num2, i + mutableBigInteger.offset);
					if ((int)(num16 + 2147483648u) > num11)
					{
						divadd(divisor, mutableBigInteger._value, i + 1 + mutableBigInteger.offset);
						num8--;
					}
					value[i] = num8;
				}
			}
			if (num4 > 0)
			{
				mutableBigInteger.rightShift(num4);
			}
			quotient.normalize();
			mutableBigInteger.normalize();
			return mutableBigInteger;
		}

		/// Compare two longs as if they were unsigned.
		/// Returns true iff one is bigger than two.
		private bool unsignedLongCompare(long one, long two)
		{
			return one + -9223372036854775808L > two + -9223372036854775808L;
		}

		/// This method divides a long quantity by an int to estimate
		/// qhat for two multi precision numbers. It is used when
		/// the signed value of n is less than zero.
		private void divWord(int[] result, long n, int d)
		{
			long num = d & uint.MaxValue;
			if (num == 1)
			{
				result[0] = (int)n;
				result[1] = 0;
			}
			else
			{
				long num2 = Operator.UnsignedRightShift(n, 1) / Operator.UnsignedRightShift(num, 1);
				long num3 = n - num2 * num;
				while (num3 < 0)
				{
					num3 += num;
					num2--;
				}
				while (num3 >= num)
				{
					num3 -= num;
					num2++;
				}
				result[0] = (int)num2;
				result[1] = (int)num3;
			}
		}

		/// Calculate GCD of this and b. This and b are changed by the computation.
		private MutableBigInteger hybridGCD(MutableBigInteger b)
		{
			MutableBigInteger mutableBigInteger = this;
			MutableBigInteger quotient = new MutableBigInteger();
			while (b.intLen != 0)
			{
				if (Math.Abs(mutableBigInteger.intLen - b.intLen) < 2)
				{
					return mutableBigInteger.binaryGCD(b);
				}
				MutableBigInteger mutableBigInteger2 = mutableBigInteger.divide(b, quotient);
				mutableBigInteger = b;
				b = mutableBigInteger2;
			}
			return mutableBigInteger;
		}

		/// Calculate GCD of this and v.
		/// Assumes that this and v are not zero.
		private MutableBigInteger binaryGCD(MutableBigInteger v)
		{
			MutableBigInteger mutableBigInteger = this;
			MutableBigInteger mutableBigInteger2 = new MutableBigInteger();
			int lowestSetBit = mutableBigInteger.getLowestSetBit();
			int lowestSetBit2 = v.getLowestSetBit();
			int num = (lowestSetBit < lowestSetBit2) ? lowestSetBit : lowestSetBit2;
			if (num != 0)
			{
				mutableBigInteger.rightShift(num);
				v.rightShift(num);
			}
			bool flag = num == lowestSetBit;
			MutableBigInteger mutableBigInteger3 = flag ? v : mutableBigInteger;
			int num2 = (!flag) ? 1 : (-1);
			int lowestSetBit3;
			while ((lowestSetBit3 = mutableBigInteger3.getLowestSetBit()) >= 0)
			{
				mutableBigInteger3.rightShift(lowestSetBit3);
				if (num2 > 0)
				{
					mutableBigInteger = mutableBigInteger3;
				}
				else
				{
					v = mutableBigInteger3;
				}
				if (mutableBigInteger.intLen < 2 && v.intLen < 2)
				{
					int a = mutableBigInteger._value[mutableBigInteger.offset];
					int b = v._value[v.offset];
					a = binaryGcd(a, b);
					mutableBigInteger2._value[0] = a;
					mutableBigInteger2.intLen = 1;
					mutableBigInteger2.offset = 0;
					if (num > 0)
					{
						mutableBigInteger2.leftShift(num);
					}
					return mutableBigInteger2;
				}
				if ((num2 = mutableBigInteger.difference(v)) == 0)
				{
					break;
				}
				mutableBigInteger3 = ((num2 >= 0) ? mutableBigInteger : v);
			}
			if (num > 0)
			{
				mutableBigInteger.leftShift(num);
			}
			return mutableBigInteger;
		}

		/// Calculate GCD of a and b interpreted as unsigned integers.
		private static int binaryGcd(int a, int b)
		{
			if (b == 0)
			{
				return a;
			}
			if (a == 0)
			{
				return b;
			}
			int num = BigInteger.NumberOfTrailingZeros(a);
			int num2 = BigInteger.NumberOfTrailingZeros(b);
			a = Operator.UnsignedRightShift(a, num);
			b = Operator.UnsignedRightShift(b, num2);
			int num3 = (num < num2) ? num : num2;
			while (a != b)
			{
				if (a + 2147483648u > b + 2147483648u)
				{
					a -= b;
					a = Operator.UnsignedRightShift(a, BigInteger.NumberOfTrailingZeros(a));
				}
				else
				{
					b -= a;
					b = Operator.UnsignedRightShift(b, BigInteger.NumberOfTrailingZeros(b));
				}
			}
			return a << num3;
		}

		/// Returns the modInverse of this mod p. This and p are not affected by
		/// the operation.
		private MutableBigInteger mutableModInverse(MutableBigInteger p)
		{
			if (p.isOdd())
			{
				return modInverse(p);
			}
			if (isEven())
			{
				throw new ArithmeticException("BigInteger not invertible.");
			}
			int lowestSetBit = p.getLowestSetBit();
			MutableBigInteger mutableBigInteger = new MutableBigInteger(p);
			mutableBigInteger.rightShift(lowestSetBit);
			if (mutableBigInteger.isOne())
			{
				return modInverseMP2(lowestSetBit);
			}
			MutableBigInteger mutableBigInteger2 = modInverse(mutableBigInteger);
			MutableBigInteger mutableBigInteger3 = modInverseMP2(lowestSetBit);
			MutableBigInteger y = modInverseBP2(mutableBigInteger, lowestSetBit);
			MutableBigInteger y2 = mutableBigInteger.modInverseMP2(lowestSetBit);
			MutableBigInteger mutableBigInteger4 = new MutableBigInteger();
			MutableBigInteger mutableBigInteger5 = new MutableBigInteger();
			MutableBigInteger mutableBigInteger6 = new MutableBigInteger();
			mutableBigInteger2.leftShift(lowestSetBit);
			mutableBigInteger2.multiply(y, mutableBigInteger6);
			mutableBigInteger3.multiply(mutableBigInteger, mutableBigInteger4);
			mutableBigInteger4.multiply(y2, mutableBigInteger5);
			mutableBigInteger6.add(mutableBigInteger5);
			return mutableBigInteger6.divide(p, mutableBigInteger4);
		}

		private MutableBigInteger modInverseMP2(int k)
		{
			if (isEven())
			{
				throw new ArithmeticException("Non-invertible. (GCD != 1)");
			}
			if (k > 64)
			{
				return euclidModInverse(k);
			}
			int num = inverseMod32(_value[offset + intLen - 1]);
			if (k < 33)
			{
				num = ((k == 32) ? num : (num & ((1 << k) - 1)));
				return new MutableBigInteger(num);
			}
			long num2 = _value[offset + intLen - 1] & uint.MaxValue;
			if (intLen > 1)
			{
				num2 |= (long)_value[offset + intLen - 2] << 32;
			}
			long num3 = num & uint.MaxValue;
			num3 *= 2 - num2 * num3;
			num3 = ((k == 64) ? num3 : (num3 & ((1L << k) - 1)));
			MutableBigInteger mutableBigInteger = new MutableBigInteger(new int[2]);
			mutableBigInteger._value[0] = (int)Operator.UnsignedRightShift(num3, 32);
			mutableBigInteger._value[1] = (int)num3;
			mutableBigInteger.intLen = 2;
			mutableBigInteger.normalize();
			return mutableBigInteger;
		}

		private static int inverseMod32(int val)
		{
			int num = val * (2 - val * val);
			num *= 2 - val * num;
			num *= 2 - val * num;
			return num * (2 - val * num);
		}

		private static MutableBigInteger modInverseBP2(MutableBigInteger mod, int k)
		{
			return fixup(new MutableBigInteger(1), new MutableBigInteger(mod), k);
		}

		/// Calculate the multiplicative inverse of this mod mod, where mod is odd.
		/// This and mod are not changed by the calculation.
		///
		/// This method implements an algorithm due to Richard Schroeppel, that uses
		/// the same intermediate representation as Montgomery Reduction
		/// ("Montgomery Form").  The algorithm is described in an unpublished
		/// manuscript entitled "Fast Modular Reciprocals."
		private MutableBigInteger modInverse(MutableBigInteger mod)
		{
			throw new NotImplementedException("This method uses SignedMutableBigInteger class.");
		}

		private static MutableBigInteger fixup(MutableBigInteger c, MutableBigInteger p, int k)
		{
			MutableBigInteger mutableBigInteger = new MutableBigInteger();
			int num = -inverseMod32(p._value[p.offset + p.intLen - 1]);
			int i = 0;
			for (int num2 = k >> 5; i < num2; i++)
			{
				int y = num * c._value[c.offset + c.intLen - 1];
				p.mul(y, mutableBigInteger);
				c.add(mutableBigInteger);
				c.intLen--;
			}
			int num3 = k & 0x1F;
			if (num3 != 0)
			{
				int num4 = num * c._value[c.offset + c.intLen - 1];
				num4 &= (1 << num3) - 1;
				p.mul(num4, mutableBigInteger);
				c.add(mutableBigInteger);
				c.rightShift(num3);
			}
			while (c.compare(p) >= 0)
			{
				c.subtract(p);
			}
			return c;
		}

		/// Uses the extended Euclidean algorithm to compute the modInverse of base
		/// mod a modulus that is a power of 2. The modulus is 2^k.
		private MutableBigInteger euclidModInverse(int k)
		{
			MutableBigInteger mutableBigInteger = new MutableBigInteger(1);
			mutableBigInteger.leftShift(k);
			MutableBigInteger mutableBigInteger2 = new MutableBigInteger(mutableBigInteger);
			MutableBigInteger mutableBigInteger3 = new MutableBigInteger(this);
			MutableBigInteger mutableBigInteger4 = new MutableBigInteger();
			MutableBigInteger mutableBigInteger5 = mutableBigInteger.divide(mutableBigInteger3, mutableBigInteger4);
			MutableBigInteger mutableBigInteger6 = mutableBigInteger;
			mutableBigInteger = mutableBigInteger5;
			mutableBigInteger5 = mutableBigInteger6;
			MutableBigInteger mutableBigInteger7 = new MutableBigInteger(mutableBigInteger4);
			MutableBigInteger mutableBigInteger8 = new MutableBigInteger(1);
			MutableBigInteger mutableBigInteger9 = new MutableBigInteger();
			while (!mutableBigInteger.isOne())
			{
				mutableBigInteger5 = mutableBigInteger3.divide(mutableBigInteger, mutableBigInteger4);
				if (mutableBigInteger5.intLen == 0)
				{
					throw new ArithmeticException("BigInteger not invertible.");
				}
				mutableBigInteger6 = mutableBigInteger5;
				mutableBigInteger3 = mutableBigInteger6;
				if (mutableBigInteger4.intLen == 1)
				{
					mutableBigInteger7.mul(mutableBigInteger4._value[mutableBigInteger4.offset], mutableBigInteger9);
				}
				else
				{
					mutableBigInteger4.multiply(mutableBigInteger7, mutableBigInteger9);
				}
				mutableBigInteger6 = mutableBigInteger4;
				mutableBigInteger4 = mutableBigInteger9;
				mutableBigInteger9 = mutableBigInteger6;
				mutableBigInteger8.add(mutableBigInteger4);
				if (mutableBigInteger3.isOne())
				{
					return mutableBigInteger8;
				}
				mutableBigInteger5 = mutableBigInteger.divide(mutableBigInteger3, mutableBigInteger4);
				if (mutableBigInteger5.intLen == 0)
				{
					throw new ArithmeticException("BigInteger not invertible.");
				}
				mutableBigInteger6 = mutableBigInteger;
				mutableBigInteger = mutableBigInteger5;
				if (mutableBigInteger4.intLen == 1)
				{
					mutableBigInteger8.mul(mutableBigInteger4._value[mutableBigInteger4.offset], mutableBigInteger9);
				}
				else
				{
					mutableBigInteger4.multiply(mutableBigInteger8, mutableBigInteger9);
				}
				mutableBigInteger6 = mutableBigInteger4;
				mutableBigInteger4 = mutableBigInteger9;
				mutableBigInteger9 = mutableBigInteger6;
				mutableBigInteger7.add(mutableBigInteger4);
			}
			mutableBigInteger2.subtract(mutableBigInteger7);
			return mutableBigInteger2;
		}
	}
}
