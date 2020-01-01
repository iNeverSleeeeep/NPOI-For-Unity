using System;
using System.Globalization;
using System.IO;

namespace NPOI.Util
{
	public class LongField
	{
		private long _value;

		private int _offset;

		/// <summary>
		/// Getg or sets the LongField's current value
		/// </summary>
		/// <value>The current value</value>
		public long Value
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
		/// construct the <see cref="T:NPOI.Util.LongField" /> with its offset into its containing byte array
		/// </summary>
		/// <param name="offset">The offset.</param>
		public LongField(int offset)
		{
			if (offset < 0)
			{
				throw new IndexOutOfRangeException("Illegal offset: " + offset);
			}
			_offset = offset;
		}

		/// <summary>
		/// construct the LongField with its offset into its containing
		/// byte array and initialize its value
		/// </summary>
		/// <param name="offset">offset of the field within its byte array</param>
		/// <param name="value">the initial value</param>
		public LongField(int offset, long value)
			: this(offset)
		{
			Value = value;
		}

		/// <summary>
		/// Construct the <see cref="T:NPOI.Util.LongField" /> class with its offset into its containing
		/// byte array and initialize its value from its byte array
		/// </summary>
		/// <param name="offset">The offset of the field within its byte array</param>
		/// <param name="data">the byte array to read the value from</param>
		public LongField(int offset, byte[] data)
			: this(offset)
		{
			ReadFromBytes(data);
		}

		/// <summary>
		/// construct the <see cref="T:NPOI.Util.LongField" /> class with its offset into its containing
		/// byte array, initialize its value, and write the value to a byte
		/// array
		/// </summary>
		/// <param name="offset">offset of the field within its byte array</param>
		/// <param name="value">the initial value</param>
		/// <param name="data">the byte array to write the value to</param>
		public LongField(int offset, long value, byte[] data)
			: this(offset)
		{
			Set(value, data);
		}

		/// <summary>
		/// set the LongField's current value and write it to a byte array
		/// </summary>
		/// <param name="value">value to be set</param>
		/// <param name="data">the byte array to write the value to</param>
		public void Set(long value, byte[] data)
		{
			_value = value;
			WriteToBytes(data);
		}

		/// <summary>
		/// set the value from its offset into an array of bytes
		/// </summary>
		/// <param name="data">the byte array from which the value is to be read</param>
		public void ReadFromBytes(byte[] data)
		{
			_value = LittleEndian.GetLong(data, _offset);
		}

		/// <summary>
		/// set the value from an Stream
		/// </summary>
		/// <param name="stream">the Stream from which the value is to be</param>
		public void ReadFromStream(Stream stream)
		{
			_value = LittleEndian.ReadLong(stream);
		}

		/// <summary>
		/// write the value out to an array of bytes at the appropriate offset
		/// </summary>
		/// <param name="data">the array of bytes to which the value is to be written</param>
		public void WriteToBytes(byte[] data)
		{
			LittleEndian.PutLong(data, _offset, _value);
		}

		/// <summary>
		/// Same as using the constructor <see cref="T:NPOI.Util.LongField" /> with the same
		/// parameter list. Avoid creation of an useless object.
		/// </summary>
		/// <param name="offset">offset of the field within its byte array</param>
		/// <param name="value">the initial value</param>
		/// <param name="data">the byte array to write the value to</param>
		public static void Write(int offset, long value, byte[] data)
		{
			LittleEndian.PutLong(data, offset, value);
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
