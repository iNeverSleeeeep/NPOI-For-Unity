using System;
using System.Globalization;
using System.Text;

namespace NPOI.Util
{
	public class BigInteger : IComparable<BigInteger>
	{
		/// This mask is used to obtain the value of an int as if it were unsigned.
		public const long LONG_MASK = 4294967295L;

		public const long INFLATED = long.MinValue;

		public const int Min_RADIX = 2;

		public const int Max_RADIX = 36;

		private const int Max_CONSTANT = 16;

		/// The signum of this BigInteger: -1 for negative, 0 for zero, or
		/// 1 for positive.  Note that the BigInteger zero <i>must</i> have
		/// a signum of 0.  This is necessary to ensures that there is exactly one
		/// representation for each BigInteger value.
		///
		/// @serial
		private int _signum;

		/// The magnitude of this BigInteger, in <i>big-endian</i> order: the
		/// zeroth element of this array is the most-significant int of the
		/// magnitude.  The magnitude must be "minimal" in that the most-significant
		/// int ({@code mag[0]}) must be non-zero.  This is necessary to
		/// ensure that there is exactly one representation for each BigInteger
		/// value.  Note that this implies that the BigInteger zero has a
		/// zero-length mag array.
		internal int[] mag;

		/// One plus the bitCount of this BigInteger. Zeros means unitialized.
		///
		/// @serial
		/// @see #bitCount
		/// @deprecated Deprecated since logical value is offset from stored
		/// value and correction factor is applied in accessor method.
		private int bitCount;

		/// One plus the bitLength of this BigInteger. Zeros means unitialized.
		/// (either value is acceptable).
		///
		/// @serial
		/// @see #bitLength()
		/// @deprecated Deprecated since logical value is offset from stored
		/// value and correction factor is applied in accessor method.
		private int bitLength;

		/// Two plus the index of the lowest-order int in the magnitude of this
		/// BigInteger that contains a nonzero int, or -2 (either value is acceptable).
		/// The least significant int has int-number 0, the next int in order of
		/// increasing significance has int-number 1, and so forth.
		/// @deprecated Deprecated since logical value is offset from stored
		/// value and correction factor is applied in accessor method.
		private int firstNonzeroIntNum;

		private static BigInteger[] posConst;

		private static BigInteger[] negConst;

		private static readonly string[] zeros;

		/// The BigInteger constant zero.
		///
		/// @since   1.2
		public static readonly BigInteger ZERO;

		/// The BigInteger constant one.
		///
		/// @since   1.2
		public static readonly BigInteger One;

		/// The BigInteger constant two.  (Not exported.)
		private static readonly BigInteger Two;

		/// The BigInteger constant ten.
		///
		/// @since   1.5
		public static readonly BigInteger TEN;

		private static readonly int[] digitsPerLong;

		private static readonly BigInteger[] longRadix;

		private static readonly long[] bitsPerDigit;

		private static readonly int[] digitsPerInt;

		private static readonly int[] intRadix;

		static BigInteger()
		{
			posConst = new BigInteger[17];
			negConst = new BigInteger[17];
			zeros = new string[64];
			ZERO = new BigInteger(new int[0], 0);
			One = ValueOf(1L);
			Two = ValueOf(2L);
			TEN = ValueOf(10L);
			digitsPerLong = new int[37]
			{
				0,
				0,
				62,
				39,
				31,
				27,
				24,
				22,
				20,
				19,
				18,
				18,
				17,
				17,
				16,
				16,
				15,
				15,
				15,
				14,
				14,
				14,
				14,
				13,
				13,
				13,
				13,
				13,
				13,
				12,
				12,
				12,
				12,
				12,
				12,
				12,
				12
			};
			longRadix = new BigInteger[37]
			{
				null,
				null,
				ValueOf(4611686018427387904L),
				ValueOf(4052555153018976267L),
				ValueOf(4611686018427387904L),
				ValueOf(7450580596923828125L),
				ValueOf(4738381338321616896L),
				ValueOf(3909821048582988049L),
				ValueOf(1152921504606846976L),
				ValueOf(1350851717672992089L),
				ValueOf(1000000000000000000L),
				ValueOf(5559917313492231481L),
				ValueOf(2218611106740436992L),
				ValueOf(8650415919381337933L),
				ValueOf(2177953337809371136L),
				ValueOf(6568408355712890625L),
				ValueOf(1152921504606846976L),
				ValueOf(2862423051509815793L),
				ValueOf(6746640616477458432L),
				ValueOf(799006685782884121L),
				ValueOf(1638400000000000000L),
				ValueOf(3243919932521508681L),
				ValueOf(6221821273427820544L),
				ValueOf(504036361936467383L),
				ValueOf(876488338465357824L),
				ValueOf(1490116119384765625L),
				ValueOf(2481152873203736576L),
				ValueOf(4052555153018976267L),
				ValueOf(6502111422497947648L),
				ValueOf(353814783205469041L),
				ValueOf(531441000000000000L),
				ValueOf(787662783788549761L),
				ValueOf(1152921504606846976L),
				ValueOf(1667889514952984961L),
				ValueOf(2386420683693101056L),
				ValueOf(3379220508056640625L),
				ValueOf(4738381338321616896L)
			};
			bitsPerDigit = new long[37]
			{
				0L,
				0L,
				1024L,
				1624L,
				2048L,
				2378L,
				2648L,
				2875L,
				3072L,
				3247L,
				3402L,
				3543L,
				3672L,
				3790L,
				3899L,
				4001L,
				4096L,
				4186L,
				4271L,
				4350L,
				4426L,
				4498L,
				4567L,
				4633L,
				4696L,
				4756L,
				4814L,
				4870L,
				4923L,
				4975L,
				5025L,
				5074L,
				5120L,
				5166L,
				5210L,
				5253L,
				5295L
			};
			digitsPerInt = new int[37]
			{
				0,
				0,
				30,
				19,
				15,
				13,
				11,
				11,
				10,
				9,
				9,
				8,
				8,
				8,
				8,
				7,
				7,
				7,
				7,
				7,
				7,
				7,
				6,
				6,
				6,
				6,
				6,
				6,
				6,
				6,
				6,
				6,
				6,
				6,
				6,
				6,
				5
			};
			intRadix = new int[37]
			{
				0,
				0,
				1073741824,
				1162261467,
				1073741824,
				1220703125,
				362797056,
				1977326743,
				1073741824,
				387420489,
				1000000000,
				214358881,
				429981696,
				815730721,
				1475789056,
				170859375,
				268435456,
				410338673,
				612220032,
				893871739,
				1280000000,
				1801088541,
				113379904,
				148035889,
				191102976,
				244140625,
				308915776,
				387420489,
				481890304,
				594823321,
				729000000,
				887503681,
				1073741824,
				1291467969,
				1544804416,
				1838265625,
				60466176
			};
			Init();
		}

		private static void Init()
		{
			if (zeros[63] == null)
			{
				for (int i = 1; i <= 16; i++)
				{
					int[] magnitude = new int[1]
					{
						i
					};
					posConst[i] = new BigInteger(magnitude, 1);
					negConst[i] = new BigInteger(magnitude, -1);
				}
				zeros[63] = "000000000000000000000000000000000000000000000000000000000000000";
				for (int j = 0; j < 63; j++)
				{
					zeros[j] = zeros[63].Substring(0, j);
				}
			}
		}

		/// This internal constructor differs from its public cousin
		/// with the arguments reversed in two ways: it assumes that its
		/// arguments are correct, and it doesn't copy the magnitude array.
		public BigInteger(int[] magnitude, int signum)
		{
			_signum = ((magnitude.Length != 0) ? signum : 0);
			mag = magnitude;
		}

		/// Translates a byte array containing the two's-complement binary
		/// representation of a BigInteger into a BigInteger.  The input array is
		/// assumed to be in <i>big-endian</i> byte-order: the most significant
		/// byte is in the zeroth element.
		///
		/// @param  val big-endian two's-complement binary representation of
		///         BigInteger.
		/// @throws NumberFormatException {@code val} is zero bytes long.
		public BigInteger(byte[] val)
		{
			if (val.Length == 0)
			{
				throw new ArgumentException("Zero length BigInteger");
			}
			if ((sbyte)val[0] < 0)
			{
				mag = makePositive(val);
				_signum = -1;
			}
			else
			{
				mag = stripLeadingZeroBytes(val);
				_signum = ((mag.Length != 0) ? 1 : 0);
			}
		}

		/// This private constructor translates an int array containing the
		/// two's-complement binary representation of a BigInteger into a
		/// BigInteger. The input array is assumed to be in <i>big-endian</i>
		/// int-order: the most significant int is in the zeroth element.
		public BigInteger(int[] val)
		{
			if (val.Length == 0)
			{
				throw new ArgumentException("Zero length BigInteger");
			}
			if (val[0] < 0)
			{
				mag = makePositive(val);
				_signum = -1;
			}
			else
			{
				mag = TrustedStripLeadingZeroInts(val);
				_signum = ((mag.Length != 0) ? 1 : 0);
			}
		}

		/// Constructs a BigInteger with the specified value, which may not be zero.
		public BigInteger(long val)
		{
			if (val < 0)
			{
				val = -val;
				_signum = -1;
			}
			else
			{
				_signum = 1;
			}
			int num = (int)Operator.UnsignedRightShift(val, 32);
			if (num == 0)
			{
				mag = new int[1];
				mag[0] = (int)val;
			}
			else
			{
				mag = new int[2];
				mag[0] = num;
				mag[1] = (int)val;
			}
		}

		public BigInteger(string val, int radix)
		{
			int i = 0;
			int length = val.Length;
			if (radix < 2 || radix > 36)
			{
				throw new FormatException("Radix out of range");
			}
			if (length == 0)
			{
				throw new FormatException("Zero length BigInteger");
			}
			int signum = 1;
			int num = val.LastIndexOf('-');
			int num2 = val.LastIndexOf('+');
			if (num + num2 > -1)
			{
				throw new FormatException("Illegal embedded sign character");
			}
			if (num == 0 || num2 == 0)
			{
				i = 1;
				if (length == 1)
				{
					throw new FormatException("Zero length BigInteger");
				}
			}
			if (num == 0)
			{
				signum = -1;
			}
			for (; i < length && val[i] == '0'; i++)
			{
			}
			if (i == length)
			{
				_signum = 0;
				mag = ZERO.mag;
			}
			else
			{
				int num3 = length - i;
				_signum = signum;
				int num4 = (int)(Operator.UnsignedRightShift(num3 * bitsPerDigit[radix], 10) + 1);
				int num5 = Operator.UnsignedRightShift(num4 + 31, 5);
				int[] array = new int[num5];
				int num6 = num3 % digitsPerInt[radix];
				if (num6 == 0)
				{
					num6 = digitsPerInt[radix];
				}
				string s = val.Substring(i, i += num6);
				array[num5 - 1] = int.Parse(s, CultureInfo.InvariantCulture);
				if (array[num5 - 1] < 0)
				{
					throw new FormatException("Illegal digit");
				}
				int y = intRadix[radix];
				int num7 = 0;
				while (i < length)
				{
					s = val.Substring(i, i += digitsPerInt[radix]);
					num7 = int.Parse(s, CultureInfo.InvariantCulture);
					if (num7 < 0)
					{
						throw new FormatException("Illegal digit");
					}
					DestructiveMulAdd(array, y, num7);
				}
				mag = TrustedStripLeadingZeroInts(array);
			}
		}

		/// Returns the input array stripped of any leading zero bytes.
		/// Since the source is trusted the copying may be skipped.
		private static int[] TrustedStripLeadingZeroInts(int[] val)
		{
			int num = val.Length;
			int i;
			for (i = 0; i < num && val[i] == 0; i++)
			{
			}
			if (i != 0)
			{
				return Arrays.CopyOfRange(val, i, num);
			}
			return val;
		}

		private static void DestructiveMulAdd(int[] x, int y, int z)
		{
			long num = y & uint.MaxValue;
			long num2 = z & uint.MaxValue;
			int num3 = x.Length;
			long num4 = 0L;
			long num5 = 0L;
			for (int num6 = num3 - 1; num6 >= 0; num6--)
			{
				num4 = num * (x[num6] & uint.MaxValue) + num5;
				x[num6] = (int)num4;
				num5 = Operator.UnsignedRightShift(num4, 32);
			}
			long num7 = (x[num3 - 1] & uint.MaxValue) + num2;
			x[num3 - 1] = (int)num7;
			num5 = Operator.UnsignedRightShift(num7, 32);
			for (int num8 = num3 - 2; num8 >= 0; num8--)
			{
				num7 = (x[num8] & uint.MaxValue) + num5;
				x[num8] = (int)num7;
				num5 = Operator.UnsignedRightShift(num7, 32);
			}
		}

		/// Returns the String representation of this BigInteger in the
		/// given radix.  If the radix is outside the range from {@link
		/// Character#Min_RADIX} to {@link Character#Max_RADIX} inclusive,
		/// it will default to 10 (as is the case for
		/// {@code Integer.toString}).  The digit-to-character mapping
		/// provided by {@code Character.forDigit} is used, and a minus
		/// sign is prepended if appropriate.  (This representation is
		/// compatible with the {@link #BigInteger(String, int) (String,
		/// int)} constructor.)
		///
		/// @param  radix  radix of the String representation.
		/// @return String representation of this BigInteger in the given radix.
		/// @see    Integer#toString
		/// @see    Character#forDigit
		/// @see    #BigInteger(java.lang.String, int)
		public string ToString(int radix)
		{
			if (_signum == 0)
			{
				return "0";
			}
			if (radix < 2 || radix > 36)
			{
				radix = 10;
			}
			if (radix != 10)
			{
				throw new ArgumentException("Only support 10 radix rendering");
			}
			int num = (4 * mag.Length + 6) / 7;
			string[] array = new string[num];
			BigInteger bigInteger = Abs();
			int num2 = 0;
			while (bigInteger._signum != 0)
			{
				BigInteger bigInteger2 = longRadix[radix];
				MutableBigInteger mutableBigInteger = new MutableBigInteger();
				MutableBigInteger mutableBigInteger2 = new MutableBigInteger(bigInteger.mag);
				MutableBigInteger b = new MutableBigInteger(bigInteger2.mag);
				MutableBigInteger mutableBigInteger3 = mutableBigInteger2.divide(b, mutableBigInteger);
				BigInteger bigInteger3 = mutableBigInteger.toBigInteger(bigInteger._signum * bigInteger2._signum);
				BigInteger bigInteger4 = mutableBigInteger3.toBigInteger(bigInteger._signum * bigInteger2._signum);
				array[num2++] = bigInteger4.LongValue().ToString(CultureInfo.InvariantCulture);
				bigInteger = bigInteger3;
			}
			StringBuilder stringBuilder = new StringBuilder(num2 * digitsPerLong[radix] + 1);
			if (_signum < 0)
			{
				stringBuilder.Append('-');
			}
			stringBuilder.Append(array[num2 - 1]);
			for (int num4 = num2 - 2; num4 >= 0; num4--)
			{
				int num5 = digitsPerLong[radix] - array[num4].Length;
				if (num5 != 0)
				{
					stringBuilder.Append(zeros[num5]);
				}
				stringBuilder.Append(array[num4]);
			}
			return stringBuilder.ToString();
		}

		/// Returns a BigInteger whose value is equal to that of the
		/// specified {@code long}.  This "static factory method" is
		/// provided in preference to a ({@code long}) constructor
		/// because it allows for reuse of frequently used BigIntegers.
		///
		/// @param  val value of the BigInteger to return.
		/// @return a BigInteger with the specified value.
		public static BigInteger ValueOf(long val)
		{
			Init();
			if (val == 0)
			{
				return ZERO;
			}
			if (val > 0 && val <= 16)
			{
				return posConst[(int)val];
			}
			if (val < 0 && val >= -16)
			{
				return negConst[(int)(-val)];
			}
			return new BigInteger(val);
		}

		/// Returns a BigInteger with the given two's complement representation.
		/// Assumes that the input array will not be modified (the returned
		/// BigInteger will reference the input array if feasible).
		private static BigInteger ValueOf(int[] val)
		{
			if (val[0] <= 0)
			{
				return new BigInteger(val);
			}
			return new BigInteger(val, 1);
		}

		/// Package private method to return bit length for an integer.
		public static int BitLengthForInt(int n)
		{
			return 32 - NumberOfLeadingZeros(n);
		}

		public int BitLength()
		{
			int num = bitLength - 1;
			if (num == -1)
			{
				int[] array = mag;
				int num2 = array.Length;
				if (num2 == 0)
				{
					num = 0;
				}
				else
				{
					int num3 = (num2 - 1 << 5) + BitLengthForInt(mag[0]);
					if (_signum < 0)
					{
						bool flag = BitCountForInt(mag[0]) == 1;
						for (int i = 1; i < num2; i++)
						{
							if (!flag)
							{
								break;
							}
							flag = (mag[i] == 0);
						}
						num = (flag ? (num3 - 1) : num3);
					}
					else
					{
						num = num3;
					}
				}
				bitLength = num + 1;
			}
			return num;
		}

		/// Returns the number of bits in the two's complement representation
		/// of this BigInteger that differ from its sign bit.  This method is
		/// useful when implementing bit-vector style sets atop BigIntegers.
		///
		/// @return number of bits in the two's complement representation
		///         of this BigInteger that differ from its sign bit.
		public int BitCount()
		{
			int num = bitCount - 1;
			if (num == -1)
			{
				num = 0;
				for (int i = 0; i < mag.Length; i++)
				{
					num += BitCountForInt(mag[i]);
				}
				if (_signum < 0)
				{
					int num2 = 0;
					int num3 = mag.Length - 1;
					while (mag[num3] == 0)
					{
						num2 += 32;
						num3--;
					}
					num2 += NumberOfTrailingZeros(mag[num3]);
					num += num2 - 1;
				}
				bitCount = num + 1;
			}
			return num;
		}

		/// Returns a BigInteger whose value is the absolute value of this
		/// BigInteger.
		///
		/// @return {@code abs(this)}
		public BigInteger Abs()
		{
			if (_signum < 0)
			{
				return Negate();
			}
			return this;
		}

		/// Returns a BigInteger whose value is {@code (-this)}.
		///
		/// @return {@code -this}
		public BigInteger Negate()
		{
			return new BigInteger(mag, -_signum);
		}

		/// Returns a BigInteger whose value is <c>(this<sup>exponent</sup>)</c>.
		/// Note that {@code exponent} is an integer rather than a BigInteger.
		///
		/// @param  exponent exponent to which this BigInteger is to be raised.
		/// @return <c>this<sup>exponent</sup></c>
		/// @throws ArithmeticException {@code exponent} is negative.  (This would
		///         cause the operation to yield a non-integer value.)
		public BigInteger Pow(int exponent)
		{
			if (exponent < 0)
			{
				throw new ArithmeticException("Negative exponent");
			}
			if (_signum == 0)
			{
				if (exponent != 0)
				{
					return this;
				}
				return One;
			}
			int signum = (_signum >= 0 || (exponent & 1) != 1) ? 1 : (-1);
			int[] array = mag;
			int[] array2 = new int[1]
			{
				1
			};
			while (exponent != 0)
			{
				if ((exponent & 1) == 1)
				{
					array2 = MultiplyToLen(array2, array2.Length, array, array.Length, null);
					array2 = TrustedStripLeadingZeroInts(array2);
				}
				exponent = Operator.UnsignedRightShift(exponent, 1);
				if (exponent != 0)
				{
					array = squareToLen(array, array.Length, null);
					array = TrustedStripLeadingZeroInts(array);
				}
			}
			return new BigInteger(array2, signum);
		}

		/// Multiplies int arrays x and y to the specified lengths and places
		/// the result into z. There will be no leading zeros in the resultant array.
		private int[] MultiplyToLen(int[] x, int xlen, int[] y, int ylen, int[] z)
		{
			int num = xlen - 1;
			int num2 = ylen - 1;
			if (z == null || z.Length < xlen + ylen)
			{
				z = new int[xlen + ylen];
			}
			long num3 = 0L;
			int num4 = num2;
			int num5 = num2 + 1 + num;
			while (num4 >= 0)
			{
				long num6 = (y[num4] & uint.MaxValue) * (x[num] & uint.MaxValue) + num3;
				z[num5] = (int)num6;
				num3 = Operator.UnsignedRightShift(num6, 32);
				num4--;
				num5--;
			}
			z[num] = (int)num3;
			for (int num7 = num - 1; num7 >= 0; num7--)
			{
				num3 = 0L;
				int num8 = num2;
				int num9 = num2 + 1 + num7;
				while (num8 >= 0)
				{
					long num10 = (y[num8] & uint.MaxValue) * (x[num7] & uint.MaxValue) + (z[num9] & uint.MaxValue) + num3;
					z[num9] = (int)num10;
					num3 = Operator.UnsignedRightShift(num10, 32);
					num8--;
					num9--;
				}
				z[num7] = (int)num3;
			}
			return z;
		}

		/// Multiply an array by one word k and add to result, return the carry
		private static int mulAdd(int[] output, int[] input, int offset, int len, int k)
		{
			long num = k & uint.MaxValue;
			long num2 = 0L;
			offset = output.Length - offset - 1;
			for (int num3 = len - 1; num3 >= 0; num3--)
			{
				long num4 = (input[num3] & uint.MaxValue) * num + (output[offset] & uint.MaxValue) + num2;
				output[offset--] = (int)num4;
				num2 = Operator.UnsignedRightShift(num4, 32);
			}
			return (int)num2;
		}

		/// Squares the contents of the int array x. The result is placed into the
		/// int array z.  The contents of x are not changed.
		private static int[] squareToLen(int[] x, int len, int[] z)
		{
			int num = len << 1;
			if (z == null || z.Length < num)
			{
				z = new int[num];
			}
			int num2 = 0;
			int i = 0;
			int num3 = 0;
			for (; i < len; i++)
			{
				long num4 = x[i] & uint.MaxValue;
				long num5 = num4 * num4;
				z[num3++] = ((num2 << 31) | (int)Operator.UnsignedRightShift(num5, 33));
				z[num3++] = (int)Operator.UnsignedRightShift(num5, 1);
				num2 = (int)num5;
			}
			int num8 = len;
			int num9 = 1;
			while (num8 > 0)
			{
				int k = x[num8 - 1];
				k = mulAdd(z, x, num9, num8 - 1, k);
				addOne(z, num9 - 1, num8, k);
				num8--;
				num9 += 2;
			}
			PrimitiveLeftShift(z, num, 1);
			z[num - 1] |= (x[len - 1] & 1);
			return z;
		}

		public static void PrimitiveLeftShift(int[] a, int len, int n)
		{
			if (len != 0 && n != 0)
			{
				int val = 32 - n;
				int i = 0;
				int num = a[i];
				for (int num2 = i + len - 1; i < num2; i++)
				{
					int num3 = num;
					num = a[i + 1];
					a[i] = ((num3 << n) | Operator.UnsignedRightShift(num, val));
				}
				a[len - 1] <<= n;
			}
		}

		/// Add one word to the number a mlen words into a. Return the resulting
		/// carry.
		private static int addOne(int[] a, int offset, int mlen, int carry)
		{
			offset = a.Length - 1 - mlen - offset;
			long num = (a[offset] & uint.MaxValue) + (carry & uint.MaxValue);
			a[offset] = (int)num;
			if (num >> 32 == 0)
			{
				return 0;
			}
			while (--mlen >= 0)
			{
				if (--offset < 0)
				{
					return 1;
				}
				a[offset]++;
				if (a[offset] != 0)
				{
					return 0;
				}
			}
			return 1;
		}

		/// Returns the signum function of this BigInteger.
		///
		/// @return -1, 0 or 1 as the value of this BigInteger is negative, zero or
		///         positive.
		public int Signum()
		{
			return _signum;
		}

		/// Returns a byte array containing the two's-complement
		/// representation of this BigInteger.  The byte array will be in
		/// <i>big-endian</i> byte-order: the most significant byte is in
		/// the zeroth element.  The array will contain the minimum number
		/// of bytes required to represent this BigInteger, including at
		/// least one sign bit, which is {@code (ceil((this.bitLength() +
		/// 1)/8))}.  (This representation is compatible with the
		/// {@link #BigInteger(byte[]) (byte[])} constructor.)
		///
		/// @return a byte array containing the two's-complement representation of
		///         this BigInteger.
		/// @see    #BigInteger(byte[])
		public byte[] ToByteArray()
		{
			int num = BitLength() / 8 + 1;
			byte[] array = new byte[num];
			int num2 = num - 1;
			int num3 = 4;
			int num4 = 0;
			int num5 = 0;
			while (num2 >= 0)
			{
				if (num3 == 4)
				{
					num4 = GetInt(num5++);
					num3 = 1;
				}
				else
				{
					num4 = Operator.UnsignedRightShift(num4, 8);
					num3++;
				}
				array[num2] = (byte)num4;
				num2--;
			}
			return array;
		}

		/// Returns the length of the two's complement representation in ints,
		/// including space for at least one sign bit.
		private int intLength()
		{
			return Operator.UnsignedRightShift(BitLength(), 5) + 1;
		}

		private int signBit()
		{
			if (_signum >= 0)
			{
				return 0;
			}
			return 1;
		}

		private int signInt()
		{
			if (_signum >= 0)
			{
				return 0;
			}
			return -1;
		}

		/// Returns the specified int of the little-endian two's complement
		/// representation (int 0 is the least significant).  The int number can
		/// be arbitrarily high (values are logically preceded by infinitely many
		/// sign ints).
		private int GetInt(int n)
		{
			if (n < 0)
			{
				return 0;
			}
			if (n >= mag.Length)
			{
				return signInt();
			}
			int num = mag[mag.Length - n - 1];
			if (_signum < 0)
			{
				if (n > FirstNonzeroIntNum())
				{
					return ~num;
				}
				return -num;
			}
			return num;
		}

		/// Returns the index of the int that contains the first nonzero int in the
		/// little-endian binary representation of the magnitude (int 0 is the
		/// least significant). If the magnitude is zero, return value is undefined.
		private int FirstNonzeroIntNum()
		{
			int num = firstNonzeroIntNum - 2;
			if (num == -2)
			{
				num = 0;
				int num2 = mag.Length;
				int num3 = num2 - 1;
				while (num3 >= 0 && mag[num3] == 0)
				{
					num3--;
				}
				num = num2 - num3 - 1;
				firstNonzeroIntNum = num + 2;
			}
			return num;
		}

		/// Returns a copy of the input array stripped of any leading zero bytes.
		private static int[] stripLeadingZeroBytes(byte[] a)
		{
			int num = a.Length;
			int i;
			for (i = 0; i < num && a[i] == 0; i++)
			{
			}
			int num2 = Operator.UnsignedRightShift(num - i + 3, 2);
			int[] array = new int[num2];
			int num3 = num - 1;
			for (int num4 = num2 - 1; num4 >= 0; num4--)
			{
				array[num4] = (a[num3--] & 0xFF);
				int val = num3 - i + 1;
				int num6 = Math.Min(3, val);
				for (int j = 8; j <= num6 << 3; j += 8)
				{
					array[num4] |= (a[num3--] & 0xFF) << j;
				}
			}
			return array;
		}

		/// Takes an array a representing a negative 2's-complement number and
		/// returns the minimal (no leading zero bytes) unsigned whose value is -a.
		private static int[] makePositive(byte[] a)
		{
			int num = a.Length;
			int i;
			for (i = 0; i < num && (sbyte)a[i] == -1; i++)
			{
			}
			int j;
			for (j = i; j < num && a[j] == 0; j++)
			{
			}
			int num2 = (j == num) ? 1 : 0;
			int num3 = (num - i + num2 + 3) / 4;
			int[] array = new int[num3];
			int num4 = num - 1;
			for (int num5 = num3 - 1; num5 >= 0; num5--)
			{
				array[num5] = (a[num4--] & 0xFF);
				int num7 = Math.Min(3, num4 - i + 1);
				if (num7 < 0)
				{
					num7 = 0;
				}
				for (int k = 8; k <= 8 * num7; k += 8)
				{
					array[num5] |= (a[num4--] & 0xFF) << k;
				}
				int num9 = Operator.UnsignedRightShift(-1, 8 * (3 - num7));
				array[num5] = (~array[num5] & num9);
			}
			for (int num10 = array.Length - 1; num10 >= 0; num10--)
			{
				array[num10] = (int)((array[num10] & uint.MaxValue) + 1);
				if (array[num10] != 0)
				{
					break;
				}
			}
			return array;
		}

		/// Takes an array a representing a negative 2's-complement number and
		/// returns the minimal (no leading zero ints) unsigned whose value is -a.
		private static int[] makePositive(int[] a)
		{
			int i;
			for (i = 0; i < a.Length && a[i] == -1; i++)
			{
			}
			int j;
			for (j = i; j < a.Length && a[j] == 0; j++)
			{
			}
			int num = (j == a.Length) ? 1 : 0;
			int[] array = new int[a.Length - i + num];
			for (int k = i; k < a.Length; k++)
			{
				array[k - i + num] = ~a[k];
			}
			int num2 = array.Length - 1;
			while (++array[num2] == 0)
			{
				num2--;
			}
			return array;
		}

		/// Returns the number of zero bits preceding the highest-order
		/// ("leftmost") one-bit in the two's complement binary representation
		/// of the specified {@code int} value.  Returns 32 if the
		/// specified value has no one-bits in its two's complement representation,
		/// in other words if it is equal to zero.
		///
		/// Note that this method is closely related to the logarithm base 2.
		/// For all positive {@code int} values x:
		/// <ul>
		/// <li>floor(log<sub>2</sub>(x)) = {@code 31 - numberOfLeadingZeros(x)}</li>
		/// <li>ceil(log<sub>2</sub>(x)) = {@code 32 - numberOfLeadingZeros(x - 1)}</li>
		/// </ul>
		///
		/// @return the number of zero bits preceding the highest-order
		///     ("leftmost") one-bit in the two's complement binary representation
		///     of the specified {@code int} value, or 32 if the value
		///     is equal to zero.
		/// @since 1.5
		public static int NumberOfLeadingZeros(int i)
		{
			if (i == 0)
			{
				return 32;
			}
			int num = 1;
			if (Operator.UnsignedRightShift(i, 16) == 0)
			{
				num += 16;
				i <<= 16;
			}
			if (Operator.UnsignedRightShift(i, 24) == 0)
			{
				num += 8;
				i <<= 8;
			}
			if (Operator.UnsignedRightShift(i, 28) == 0)
			{
				num += 4;
				i <<= 4;
			}
			if (Operator.UnsignedRightShift(i, 30) == 0)
			{
				num += 2;
				i <<= 2;
			}
			return num - Operator.UnsignedRightShift(i, 31);
		}

		/// Returns the number of zero bits following the lowest-order ("rightmost")
		/// one-bit in the two's complement binary representation of the specified
		/// {@code int} value.  Returns 32 if the specified value has no
		/// one-bits in its two's complement representation, in other words if it is
		/// equal to zero.
		///
		/// @return the number of zero bits following the lowest-order ("rightmost")
		///     one-bit in the two's complement binary representation of the
		///     specified {@code int} value, or 32 if the value is equal
		///     to zero.
		/// @since 1.5
		public static int NumberOfTrailingZeros(int i)
		{
			if (i == 0)
			{
				return 32;
			}
			int num = 31;
			int num2 = i << 16;
			if (num2 != 0)
			{
				num -= 16;
				i = num2;
			}
			num2 = i << 8;
			if (num2 != 0)
			{
				num -= 8;
				i = num2;
			}
			num2 = i << 4;
			if (num2 != 0)
			{
				num -= 4;
				i = num2;
			}
			num2 = i << 2;
			if (num2 != 0)
			{
				num -= 2;
				i = num2;
			}
			return num - Operator.UnsignedRightShift(i << 1, 31);
		}

		/// Returns the number of one-bits in the two's complement binary
		/// representation of the specified {@code int} value.  This function is
		/// sometimes referred to as the <i>population count</i>.
		///
		/// @return the number of one-bits in the two's complement binary
		///     representation of the specified {@code int} value.
		/// @since 1.5
		public static int BitCountForInt(int i)
		{
			uint num = (uint)(i - (int)(((uint)i >> 1) & 0x55555555));
			num = (num & 0x33333333) + ((num >> 2) & 0x33333333);
			num = ((num + (num >> 4)) & 0xF0F0F0F);
			num += num >> 8;
			num += num >> 16;
			return (int)(num & 0x3F);
		}

		public int CompareTo(BigInteger val)
		{
			if (_signum == val._signum)
			{
				switch (_signum)
				{
				case 1:
					return compareMagnitude(val);
				case -1:
					return val.compareMagnitude(this);
				default:
					return 0;
				}
			}
			if (_signum <= val._signum)
			{
				return -1;
			}
			return 1;
		}

		/// Compares the magnitude array of this BigInteger with the specified
		/// BigInteger's. This is the version of compareTo ignoring sign.
		///
		/// @param val BigInteger whose magnitude array to be compared.
		/// @return -1, 0 or 1 as this magnitude array is less than, equal to or
		///         greater than the magnitude aray for the specified BigInteger's.
		private int compareMagnitude(BigInteger val)
		{
			int[] array = mag;
			int num = array.Length;
			int[] array2 = val.mag;
			int num2 = array2.Length;
			if (num < num2)
			{
				return -1;
			}
			if (num > num2)
			{
				return 1;
			}
			for (int i = 0; i < num; i++)
			{
				int num3 = array[i];
				int num4 = array2[i];
				if (num3 != num4)
				{
					if ((num3 & uint.MaxValue) >= (num4 & uint.MaxValue))
					{
						return 1;
					}
					return -1;
				}
			}
			return 0;
		}

		/// Compares this BigInteger with the specified Object for equality.
		///
		/// @param  x Object to which this BigInteger is to be compared.
		/// @return {@code true} if and only if the specified Object is a
		///         BigInteger whose value is numerically equal to this BigInteger.
		public override bool Equals(object x)
		{
			if (object.ReferenceEquals(x, this))
			{
				return true;
			}
			if (!(x is BigInteger) || x == null)
			{
				return false;
			}
			BigInteger bigInteger = (BigInteger)x;
			if (bigInteger._signum != _signum)
			{
				return false;
			}
			int[] array = mag;
			int num = array.Length;
			int[] array2 = bigInteger.mag;
			if (num != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				if (array2[i] != array[i])
				{
					return false;
				}
			}
			return true;
		}

		/// Returns the minimum of this BigInteger and {@code val}.
		///
		/// @param  val value with which the minimum is to be computed.
		/// @return the BigInteger whose value is the lesser of this BigInteger and
		///         {@code val}.  If they are equal, either may be returned.
		public BigInteger Min(BigInteger val)
		{
			if (CompareTo(val) >= 0)
			{
				return val;
			}
			return this;
		}

		/// Returns the maximum of this BigInteger and {@code val}.
		///
		/// @param  val value with which the maximum is to be computed.
		/// @return the BigInteger whose value is the greater of this and
		///         {@code val}.  If they are equal, either may be returned.
		public BigInteger Max(BigInteger val)
		{
			if (CompareTo(val) <= 0)
			{
				return val;
			}
			return this;
		}

		/// Returns the hash code for this BigInteger.
		///
		/// @return hash code for this BigInteger.
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < mag.Length; i++)
			{
				num = (int)(31 * num + (mag[i] & uint.MaxValue));
			}
			return num * _signum;
		}

		/// Converts this BigInteger to an {@code int}.  This
		/// conversion is analogous to a
		/// <i>narrowing primitive conversion</i> from {@code long} to
		/// {@code int} as defined in section 5.1.3 of
		/// <cite>The Java(TM) Language Specification</cite>:
		/// if this BigInteger is too big to fit in an
		/// {@code int}, only the low-order 32 bits are returned.
		/// Note that this conversion can lose information about the
		/// overall magnitude of the BigInteger value as well as return a
		/// result with the opposite sign.
		///
		/// @return this BigInteger converted to an {@code int}.
		public int IntValue()
		{
			int num = 0;
			return GetInt(0);
		}

		public BigInteger ShiftLeft(int n)
		{
			if (_signum == 0)
			{
				return ZERO;
			}
			if (n == 0)
			{
				return this;
			}
			if (n < 0)
			{
				if (n == -2147483648)
				{
					throw new ArithmeticException("Shift distance of Integer.Min_VALUE not supported.");
				}
				return ShiftRight(-n);
			}
			int num = Operator.UnsignedRightShift(n, 5);
			int num2 = n & 0x1F;
			int num3 = mag.Length;
			int[] array = null;
			if (num2 == 0)
			{
				array = new int[num3 + num];
				for (int i = 0; i < num3; i++)
				{
					array[i] = mag[i];
				}
			}
			else
			{
				int num4 = 0;
				int val = 32 - num2;
				int num5 = Operator.UnsignedRightShift(mag[0], val);
				if (num5 != 0)
				{
					array = new int[num3 + num + 1];
					array[num4++] = num5;
				}
				else
				{
					array = new int[num3 + num];
				}
				int num7 = 0;
				while (num7 < num3 - 1)
				{
					array[num4++] = ((mag[num7++] << num2) | Operator.UnsignedRightShift(mag[num7], val));
				}
				array[num4] = mag[num7] << num2;
			}
			return new BigInteger(array, _signum);
		}

		/// Converts this BigInteger to a {@code long}.  This
		/// conversion is analogous to a
		/// <i>narrowing primitive conversion</i> from {@code long} to
		/// {@code int} as defined in section 5.1.3 of
		/// <cite>The Java(TM) Language Specification</cite>:
		/// if this BigInteger is too big to fit in a
		/// {@code long}, only the low-order 64 bits are returned.
		/// Note that this conversion can lose information about the
		/// overall magnitude of the BigInteger value as well as return a
		/// result with the opposite sign.
		///
		/// @return this BigInteger converted to a {@code long}.
		public long LongValue()
		{
			long num = 0L;
			for (int num2 = 1; num2 >= 0; num2--)
			{
				num = (num << 32) + (GetInt(num2) & uint.MaxValue);
			}
			return num;
		}

		/// Returns a BigInteger whose value is {@code (this &gt;&gt; n)}.  Sign
		/// extension is performed.  The shift distance, {@code n}, may be
		/// negative, in which case this method performs a left shift.
		/// (Computes <c>floor(this / 2<sup>n</sup>)</c>.)
		///
		/// @param  n shift distance, in bits.
		/// @return {@code this &gt;&gt; n}
		/// @throws ArithmeticException if the shift distance is {@code
		///         Integer.Min_VALUE}.
		/// @see #shiftLeft
		public BigInteger ShiftRight(int n)
		{
			if (n == 0)
			{
				return this;
			}
			if (n < 0)
			{
				if (n == -2147483648)
				{
					throw new ArithmeticException("Shift distance of Integer.Min_VALUE not supported.");
				}
				return ShiftLeft(-n);
			}
			int num = Operator.UnsignedRightShift(n, 5);
			int num2 = n & 0x1F;
			int num3 = mag.Length;
			int[] array = null;
			if (num >= num3)
			{
				if (_signum < 0)
				{
					return negConst[1];
				}
				return ZERO;
			}
			if (num2 == 0)
			{
				int num4 = num3 - num;
				array = new int[num4];
				for (int i = 0; i < num4; i++)
				{
					array[i] = mag[i];
				}
			}
			else
			{
				int num5 = 0;
				int num6 = Operator.UnsignedRightShift(mag[0], num2);
				if (num6 != 0)
				{
					array = new int[num3 - num];
					array[num5++] = num6;
				}
				else
				{
					array = new int[num3 - num - 1];
				}
				int num8 = 32 - num2;
				int num9 = 0;
				while (num9 < num3 - num - 1)
				{
					array[num5++] = ((mag[num9++] << num8) | Operator.UnsignedRightShift(mag[num9], num2));
				}
			}
			if (_signum < 0)
			{
				bool flag = false;
				int num12 = num3 - 1;
				int num13 = num3 - num;
				while (num12 >= num13 && !flag)
				{
					flag = (mag[num12] != 0);
					num12--;
				}
				if (!flag && num2 != 0)
				{
					flag = (mag[num3 - num - 1] << 32 - num2 != 0);
				}
				if (flag)
				{
					array = Increment(array);
				}
			}
			return new BigInteger(array, _signum);
		}

		private int[] Increment(int[] val)
		{
			int num = 0;
			int num2 = val.Length - 1;
			while (num2 >= 0 && num == 0)
			{
				num = ++val[num2];
				num2--;
			}
			if (num == 0)
			{
				val = new int[val.Length + 1];
				val[0] = 1;
			}
			return val;
		}

		public BigInteger and(BigInteger val)
		{
			int[] array = new int[Math.Max(intLength(), val.intLength())];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (GetInt(array.Length - i - 1) & val.GetInt(array.Length - i - 1));
			}
			return ValueOf(array);
		}

		/// Returns a BigInteger whose value is {@code (~this)}.  (This method
		/// returns a negative value if and only if this BigInteger is
		/// non-negative.)
		///
		/// @return {@code ~this}
		public BigInteger Not()
		{
			int[] array = new int[intLength()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ~GetInt(array.Length - i - 1);
			}
			return ValueOf(array);
		}

		/// Returns a BigInteger whose value is {@code (this | val)}.  (This method
		/// returns a negative BigInteger if and only if either this or val is
		/// negative.)
		///
		/// @param val value to be OR'ed with this BigInteger.
		/// @return {@code this | val}
		public BigInteger Or(BigInteger val)
		{
			int[] array = new int[Math.Max(intLength(), val.intLength())];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (GetInt(array.Length - i - 1) | val.GetInt(array.Length - i - 1));
			}
			return ValueOf(array);
		}

		/// Package private methods used by BigDecimal code to multiply a BigInteger
		/// with a long. Assumes v is not equal to INFLATED.
		private BigInteger Multiply(long v)
		{
			if (v == 0 || _signum == 0)
			{
				return ZERO;
			}
			if (v == -9223372036854775808L)
			{
				return Multiply(ValueOf(v));
			}
			int signum = (v > 0) ? _signum : (-_signum);
			if (v < 0)
			{
				v = -v;
			}
			long num = Operator.UnsignedRightShift(v, 32);
			long num2 = v & uint.MaxValue;
			int num3 = mag.Length;
			int[] array = mag;
			int[] array2 = (num == 0) ? new int[num3 + 1] : new int[num3 + 2];
			long num4 = 0L;
			int num5 = array2.Length - 1;
			for (int num6 = num3 - 1; num6 >= 0; num6--)
			{
				long num7 = (array[num6] & uint.MaxValue) * num2 + num4;
				array2[num5--] = (int)num7;
				num4 = Operator.UnsignedRightShift(num7, 32);
			}
			array2[num5] = (int)num4;
			if (num != 0)
			{
				num4 = 0L;
				num5 = array2.Length - 2;
				for (int num9 = num3 - 1; num9 >= 0; num9--)
				{
					long num10 = (array[num9] & uint.MaxValue) * num + (array2[num5] & uint.MaxValue) + num4;
					array2[num5--] = (int)num10;
					num4 = Operator.UnsignedRightShift(num10, 32);
				}
				array2[0] = (int)num4;
			}
			if (num4 == 0)
			{
				array2 = Arrays.CopyOfRange(array2, 1, array2.Length);
			}
			return new BigInteger(array2, signum);
		}

		/// Returns a BigInteger whose value is {@code (this * val)}.
		///
		/// @param  val value to be multiplied by this BigInteger.
		/// @return {@code this * val}
		public BigInteger Multiply(BigInteger val)
		{
			if (val._signum == 0 || _signum == 0)
			{
				return ZERO;
			}
			int[] val2 = MultiplyToLen(mag, mag.Length, val.mag, val.mag.Length, null);
			val2 = TrustedStripLeadingZeroInts(val2);
			return new BigInteger(val2, (_signum == val._signum) ? 1 : (-1));
		}

		/// Returns a BigInteger whose value is {@code (this + val)}.
		///
		/// @param  val value to be added to this BigInteger.
		/// @return {@code this + val}
		public BigInteger Add(BigInteger val)
		{
			if (val._signum == 0)
			{
				return this;
			}
			if (_signum == 0)
			{
				return val;
			}
			if (val._signum == _signum)
			{
				return new BigInteger(add(mag, val.mag), _signum);
			}
			int num = compareMagnitude(val);
			if (num == 0)
			{
				return ZERO;
			}
			int[] val2 = (num > 0) ? Subtract(mag, val.mag) : Subtract(val.mag, mag);
			val2 = TrustedStripLeadingZeroInts(val2);
			return new BigInteger(val2, (num == _signum) ? 1 : (-1));
		}

		/// Adds the contents of the int arrays x and y. This method allocates
		/// a new int array to hold the answer and returns a reference to that
		/// array.
		private static int[] add(int[] x, int[] y)
		{
			if (x.Length < y.Length)
			{
				int[] array = x;
				x = y;
				y = array;
			}
			int num = x.Length;
			int num2 = y.Length;
			int[] array2 = new int[num];
			long num3 = 0L;
			while (num2 > 0)
			{
				num3 = (x[--num] & uint.MaxValue) + (y[--num2] & uint.MaxValue) + Operator.UnsignedRightShift(num3, 32);
				array2[num] = (int)num3;
			}
			bool flag = Operator.UnsignedRightShift(num3, 32) != 0;
			while (num > 0 && flag)
			{
				flag = ((array2[--num] = x[num] + 1) == 0);
			}
			while (num > 0)
			{
				array2[--num] = x[num];
			}
			if (flag)
			{
				int[] array3 = new int[array2.Length + 1];
				Array.Copy(array2, 0, array3, 1, array2.Length);
				array3[0] = 1;
				return array3;
			}
			return array2;
		}

		/// Returns a BigInteger whose value is {@code (this - val)}.
		///
		/// @param  val value to be subtracted from this BigInteger.
		/// @return {@code this - val}
		public BigInteger Subtract(BigInteger val)
		{
			if (val._signum == 0)
			{
				return this;
			}
			if (_signum == 0)
			{
				return val.Negate();
			}
			if (val._signum != _signum)
			{
				return new BigInteger(add(mag, val.mag), _signum);
			}
			int num = compareMagnitude(val);
			if (num == 0)
			{
				return ZERO;
			}
			int[] val2 = (num > 0) ? Subtract(mag, val.mag) : Subtract(val.mag, mag);
			val2 = TrustedStripLeadingZeroInts(val2);
			return new BigInteger(val2, (num == _signum) ? 1 : (-1));
		}

		/// Subtracts the contents of the second int arrays (little) from the
		/// first (big).  The first int array (big) must represent a larger number
		/// than the second.  This method allocates the space necessary to hold the
		/// answer.
		private static int[] Subtract(int[] big, int[] little)
		{
			int num = big.Length;
			int[] array = new int[num];
			int num2 = little.Length;
			long num3 = 0L;
			while (num2 > 0)
			{
				num3 = (big[--num] & uint.MaxValue) - (little[--num2] & uint.MaxValue) + (num3 >> 32);
				array[num] = (int)num3;
			}
			bool flag = num3 >> 32 != 0;
			while (num > 0 && flag)
			{
				flag = ((array[--num] = big[num] - 1) == -1);
			}
			while (num > 0)
			{
				array[--num] = big[num];
			}
			return array;
		}

		/// Returns a BigInteger whose value is {@code (this / val)}.
		///
		/// @param  val value by which this BigInteger is to be divided.
		/// @return {@code this / val}
		/// @throws ArithmeticException if {@code val} is zero.
		public BigInteger Divide(BigInteger val)
		{
			MutableBigInteger mutableBigInteger = new MutableBigInteger();
			MutableBigInteger mutableBigInteger2 = new MutableBigInteger(mag);
			MutableBigInteger b = new MutableBigInteger(val.mag);
			mutableBigInteger2.divide(b, mutableBigInteger);
			return mutableBigInteger.toBigInteger((_signum == val._signum) ? 1 : (-1));
		}

		public static BigInteger operator >>(BigInteger bi1, int shiftVal)
		{
			return bi1.ShiftRight(shiftVal);
		}

		public static BigInteger operator <<(BigInteger bi1, int shiftVal)
		{
			return bi1.ShiftLeft(shiftVal);
		}

		public static BigInteger operator &(BigInteger bi1, BigInteger bi2)
		{
			return bi1.and(bi2);
		}

		public static BigInteger operator |(BigInteger bi1, BigInteger bi2)
		{
			return bi1.Or(bi2);
		}

		public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
		{
			return bi1.Multiply(bi2);
		}

		public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
		{
			return bi1.Add(bi2);
		}

		public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
		{
			return bi1.Subtract(bi2);
		}

		public static bool operator <(BigInteger bi1, BigInteger bi2)
		{
			return bi1.CompareTo(bi2) < 0;
		}

		public static bool operator >(BigInteger bi1, BigInteger bi2)
		{
			return bi1.CompareTo(bi2) > 0;
		}

		public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
		{
			return bi1.Divide(bi2);
		}

		public static bool operator ==(BigInteger bi1, BigInteger bi2)
		{
			return bi1.Equals(bi2);
		}

		public static bool operator !=(BigInteger bi1, BigInteger bi2)
		{
			return !(bi1 == bi2);
		}
	}
}
