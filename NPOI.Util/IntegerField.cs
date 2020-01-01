using System;
using System.Globalization;
using System.IO;

namespace NPOI.Util
{
	public class IntegerField : FixedField
	{
		private int _value;

		private int _offset;

		/// <summary>
		/// get or Set the IntegerField's current value
		/// </summary>
		/// <value>The value.</value>
		/// <returns></returns>
		public int Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		/// <summary>
		/// construct the <see cref="T:NPOI.Util.IntegerField" /> with its offset into its containing byte array  class.
		/// </summary>
		/// <param name="offset">offset of the field within its byte array.</param>
		public IntegerField(int offset)
		{
			if (offset < 0)
			{
				throw new IndexOutOfRangeException("negative offset");
			}
			_offset = offset;
		}

		/// <summary>
		/// construct the <see cref="T:NPOI.Util.IntegerField" /> with its offset into its containing
		/// byte array and initialize its value
		/// </summary>
		/// <param name="offset">offset of the field within its byte array</param>
		/// <param name="value">the initial value</param>
		public IntegerField(int offset, int value)
			: this(offset)
		{
			_value = value;
		}

		/// <summary>
		/// Construct the <see cref="T:NPOI.Util.IntegerField" /> with its offset into its containing
		/// byte array and initialize its value from its byte array
		/// </summary>
		/// <param name="offset">offset of the field within its byte array</param>
		/// <param name="data">the byte array to Read the value from</param>
		public IntegerField(int offset, byte[] data)
			: this(offset)
		{
			ReadFromBytes(data);
		}

		/// <summary>
		/// construct the <see cref="T:NPOI.Util.IntegerField" /> with its offset into its containing 
		/// byte array, initialize its value, and write the value to a byte
		/// </summary>
		/// <param name="offset">offset of the field within its byte array</param>
		/// <param name="value">the initial value</param>
		/// <param name="data">the byte array to write the value to</param>
		public IntegerField(int offset, int value, byte[] data)
			: this(offset)
		{
			Set(value, data);
		}

		/// <summary>
		/// Set the IntegerField's current value and write it to a byte array
		/// </summary>
		/// <param name="value">value to be Set</param>
		/// <param name="data">the byte array to write the value to</param>
		public void Set(int value, byte[] data)
		{
			_value = value;
			WriteToBytes(data);
		}

		/// <summary>
		/// Set the value from its offset into an array of bytes
		/// </summary>
		/// <param name="data">The data.</param>
		public void ReadFromBytes(byte[] data)
		{
			_value = LittleEndian.GetInt(data, _offset);
		}

		/// <summary>
		/// Set the value from an Stream
		/// </summary>
		/// <param name="stream">the Stream from which the value is to be Read</param>
		public void ReadFromStream(Stream stream)
		{
			_value = LittleEndian.ReadInt(stream);
		}

		/// <summary>
		/// write the value out to an array of bytes at the appropriate offset
		/// </summary>
		/// <param name="data"> the array of bytes to which the value is to be written </param>
		public void WriteToBytes(byte[] data)
		{
			LittleEndian.PutInt(data, _offset, _value);
		}

		/// <summary>
		/// Same as using the constructor <see cref="T:NPOI.Util.IntegerField" /> with the same
		/// parameter list. Avoid creation of an useless object.
		/// </summary>
		/// <param name="offset">offset of the field within its byte array</param>
		/// <param name="value">the initial value</param>
		/// <param name="data">the byte array to write the value to</param>
		public static void Write(int offset, int value, byte[] data)
		{
			LittleEndian.PutInt(data, offset, value);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			return Convert.ToString(_value, CultureInfo.CurrentCulture);
		}
	}
}
