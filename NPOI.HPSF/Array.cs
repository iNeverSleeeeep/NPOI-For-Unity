using NPOI.Util;
using System;

namespace NPOI.HPSF
{
	public class Array
	{
		internal class ArrayDimension
		{
			public const int SIZE = 8;

			private int _indexOffset;

			internal long _size;

			public ArrayDimension(byte[] data, int offset)
			{
				_size = LittleEndian.GetUInt(data, offset);
				_indexOffset = LittleEndian.GetInt(data, offset + 4);
			}
		}

		internal class ArrayHeader
		{
			private ArrayDimension[] _dimensions;

			internal int _type;

			public long NumberOfScalarValues
			{
				get
				{
					long num = 1L;
					ArrayDimension[] dimensions = _dimensions;
					foreach (ArrayDimension arrayDimension in dimensions)
					{
						num *= arrayDimension._size;
					}
					return num;
				}
			}

			public int Size => 8 + _dimensions.Length * 8;

			public int Type => _type;

			public ArrayHeader(byte[] data, int startOffset)
			{
				_type = LittleEndian.GetInt(data, startOffset);
				int num = startOffset + 4;
				long uInt = LittleEndian.GetUInt(data, num);
				num += 4;
				if (1 > uInt || uInt > 31)
				{
					throw new IllegalPropertySetDataException("Array dimension number " + uInt + " is not in [1; 31] range");
				}
				int num2 = (int)uInt;
				_dimensions = new ArrayDimension[num2];
				for (int i = 0; i < num2; i++)
				{
					_dimensions[i] = new ArrayDimension(data, num);
					num += 8;
				}
			}
		}

		private ArrayHeader _header;

		private TypedPropertyValue[] _values;

		public Array()
		{
		}

		public Array(byte[] data, int offset)
		{
			Read(data, offset);
		}

		public int Read(byte[] data, int startOffset)
		{
			_header = new ArrayHeader(data, startOffset);
			int num = startOffset + _header.Size;
			long numberOfScalarValues = _header.NumberOfScalarValues;
			if (numberOfScalarValues > 2147483647)
			{
				throw new InvalidOperationException("Sorry, but POI can't store array of properties with size of " + numberOfScalarValues + " in memory");
			}
			int num2 = (int)numberOfScalarValues;
			_values = new TypedPropertyValue[num2];
			int type = _header._type;
			if (type == 12)
			{
				for (int i = 0; i < num2; i++)
				{
					TypedPropertyValue typedPropertyValue = new TypedPropertyValue();
					num += typedPropertyValue.Read(data, num);
				}
			}
			else
			{
				for (int j = 0; j < num2; j++)
				{
					TypedPropertyValue typedPropertyValue2 = new TypedPropertyValue(type, null);
					num += typedPropertyValue2.ReadValuePadded(data, num);
				}
			}
			return num - startOffset;
		}
	}
}
