using System;

namespace NPOI.Util
{
	/// <summary>
	/// Manage operations dealing with bit-mapped fields.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// </summary>
	[Serializable]
	public class BitField
	{
		private int _mask;

		private int _shift_count;

		/// <summary>
		/// Create a <see cref="T:NPOI.Util.BitField" /> instance
		/// </summary>
		/// <param name="mask">
		/// the mask specifying which bits apply to this
		/// BitField. Bits that are set in this mask are the
		/// bits that this BitField operates on
		/// </param>
		public BitField(int mask)
		{
			_mask = mask;
			int num = 0;
			int num2 = mask;
			if (num2 != 0)
			{
				while ((num2 & 1) == 0)
				{
					num++;
					num2 >>= 1;
				}
			}
			_shift_count = num;
		}

		/// <summary>
		/// Create a <see cref="T:NPOI.Util.BitField" /> instance
		/// </summary>
		/// <param name="mask">
		/// the mask specifying which bits apply to this
		/// BitField. Bits that are set in this mask are the
		/// bits that this BitField operates on
		/// </param>
		public BitField(uint mask)
			: this((int)mask)
		{
		}

		/// <summary>
		/// Clear the bits.
		/// </summary>
		/// <param name="holder">the int data containing the bits we're interested in</param>
		/// <returns>the value of holder with the specified bits cleared (set to 0)</returns>
		public int Clear(int holder)
		{
			return holder & ~_mask;
		}

		/// <summary>
		/// Clear the bits.
		/// </summary>
		/// <param name="holder">the short data containing the bits we're interested in</param>
		/// <returns>the value of holder with the specified bits cleared (set to 0)</returns>
		public short ClearShort(short holder)
		{
			return (short)Clear(holder);
		}

		/// <summary>
		/// Obtain the value for the specified BitField, appropriately
		/// shifted right. Many users of a BitField will want to treat the
		/// specified bits as an int value, and will not want to be aware
		/// that the value is stored as a BitField (and so shifted left so
		/// many bits)
		/// </summary>
		/// <param name="holder">the int data containing the bits we're interested in</param>
		/// <returns>the selected bits, shifted right appropriately</returns>
		public int GetRawValue(int holder)
		{
			return holder & _mask;
		}

		/// <summary>
		/// Obtain the value for the specified BitField, unshifted
		/// </summary>
		/// <param name="holder">the short data containing the bits we're interested in</param>
		/// <returns>the selected bits</returns>
		public short GetShortRawValue(short holder)
		{
			return (short)GetRawValue(holder);
		}

		/// <summary>
		/// Obtain the value for the specified BitField, appropriately
		/// shifted right, as a short. Many users of a BitField will want
		/// to treat the specified bits as an int value, and will not want
		/// to be aware that the value is stored as a BitField (and so
		/// shifted left so many bits)
		/// </summary>
		/// <param name="holder">the short data containing the bits we're interested in</param>
		/// <returns>the selected bits, shifted right appropriately</returns>
		public short GetShortValue(short holder)
		{
			return (short)GetValue(holder);
		}

		/// <summary>
		/// Obtain the value for the specified BitField, appropriately
		/// shifted right. Many users of a BitField will want to treat the
		/// specified bits as an int value, and will not want to be aware
		/// that the value is stored as a BitField (and so shifted left so
		/// many bits)
		/// </summary>
		/// <param name="holder">the int data containing the bits we're interested in</param>
		/// <returns>the selected bits, shifted right appropriately</returns>
		public int GetValue(int holder)
		{
			return Operator.UnsignedRightShift(GetRawValue(holder), _shift_count);
		}

		/// <summary>
		/// Are all of the bits set or not? This is a stricter test than
		/// isSet, in that all of the bits in a multi-bit set must be set
		/// for this method to return true
		/// </summary>
		/// <param name="holder">the int data containing the bits we're interested in</param>
		/// <returns>
		/// 	<c>true</c> if all of the bits are set otherwise, <c>false</c>.
		/// </returns>
		public bool IsAllSet(int holder)
		{
			return (holder & _mask) == _mask;
		}

		/// <summary>
		/// is the field set or not? This is most commonly used for a
		/// single-bit field, which is often used to represent a boolean
		/// value; the results of using it for a multi-bit field is to
		/// determine whether *any* of its bits are set
		/// </summary>
		/// <param name="holder">the int data containing the bits we're interested in</param>
		/// <returns>
		/// 	<c>true</c> if any of the bits are set; otherwise, <c>false</c>.
		/// </returns>
		public bool IsSet(int holder)
		{
			return (holder & _mask) != 0;
		}

		/// <summary>
		/// Set the bits.
		/// </summary>
		/// <param name="holder">the int data containing the bits we're interested in</param>
		/// <returns>the value of holder with the specified bits set to 1</returns>
		public int Set(int holder)
		{
			return holder | _mask;
		}

		/// <summary>
		/// Set a boolean BitField
		/// </summary>
		/// <param name="holder">the int data containing the bits we're interested in</param>
		/// <param name="flag">indicating whether to set or clear the bits</param>
		/// <returns>the value of holder with the specified bits set or cleared</returns>
		public int SetBoolean(int holder, bool flag)
		{
			if (flag)
			{
				return Set(holder);
			}
			return Clear(holder);
		}

		/// <summary>
		/// Set the bits.
		/// </summary>
		/// <param name="holder">the short data containing the bits we're interested in</param>
		/// <returns>the value of holder with the specified bits set to 1</returns>
		public short SetShort(short holder)
		{
			return (short)Set(holder);
		}

		/// <summary>
		/// Set a boolean BitField
		/// </summary>
		/// <param name="holder">the short data containing the bits we're interested in</param>
		/// <param name="flag">indicating whether to set or clear the bits</param>
		/// <returns>the value of holder with the specified bits set or cleared</returns>
		public short SetShortBoolean(short holder, bool flag)
		{
			if (flag)
			{
				return SetShort(holder);
			}
			return ClearShort(holder);
		}

		/// <summary>
		/// Obtain the value for the specified BitField, appropriately
		/// shifted right, as a short. Many users of a BitField will want
		/// to treat the specified bits as an int value, and will not want
		/// to be aware that the value is stored as a BitField (and so
		/// shifted left so many bits)
		/// </summary>
		/// <param name="holder">the short data containing the bits we're interested in</param>
		/// <param name="value">the new value for the specified bits</param>
		/// <returns>the selected bits, shifted right appropriately</returns>
		public short SetShortValue(short holder, short value)
		{
			return (short)SetValue(holder, value);
		}

		/// <summary>
		/// Sets the value.
		/// </summary>
		/// <param name="holder">the byte data containing the bits we're interested in</param>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public int SetValue(int holder, int value)
		{
			return (holder & ~_mask) | ((value << _shift_count) & _mask);
		}

		/// <summary>
		/// Set a boolean BitField
		/// </summary>
		/// <param name="holder"> the byte data containing the bits we're interested in</param>
		/// <param name="flag">indicating whether to set or clear the bits</param>
		/// <returns>the value of holder with the specified bits set or cleared</returns>
		public byte SetByteBoolean(byte holder, bool flag)
		{
			if (flag)
			{
				return SetByte(holder);
			}
			return ClearByte(holder);
		}

		/// <summary>
		/// Clears the bits.
		/// </summary>
		/// <param name="holder">the byte data containing the bits we're interested in</param>
		/// <returns>the value of holder with the specified bits cleared</returns>
		public byte ClearByte(byte holder)
		{
			return (byte)Clear(holder);
		}

		/// <summary>
		/// Set the bits.
		/// </summary>
		/// <param name="holder">the byte data containing the bits we're interested in</param>
		/// <returns>the value of holder with the specified bits set to 1</returns>
		public byte SetByte(byte holder)
		{
			return (byte)Set(holder);
		}
	}
}
