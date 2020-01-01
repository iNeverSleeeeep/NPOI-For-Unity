using NPOI.Util;
using System;

namespace NPOI.HPSF
{
	public class Vector
	{
		private short _type;

		private TypedPropertyValue[] _values;

		public TypedPropertyValue[] Values => _values;

		public Vector(byte[] data, int startOffset, short type)
		{
			_type = type;
			Read(data, startOffset);
		}

		public Vector(short type)
		{
			_type = type;
		}

		public int Read(byte[] data, int startOffset)
		{
			long uInt = LittleEndian.GetUInt(data, startOffset);
			int num = startOffset + 4;
			if (uInt > 2147483647)
			{
				throw new InvalidOperationException("Vector is too long -- " + uInt);
			}
			int num2 = (int)uInt;
			_values = new TypedPropertyValue[num2];
			if (_type == 12)
			{
				for (int i = 0; i < num2; i++)
				{
					TypedPropertyValue typedPropertyValue = new TypedPropertyValue();
					num += typedPropertyValue.Read(data, num);
					_values[i] = typedPropertyValue;
				}
			}
			else
			{
				for (int j = 0; j < num2; j++)
				{
					TypedPropertyValue typedPropertyValue2 = new TypedPropertyValue(_type, null);
					num += typedPropertyValue2.ReadValue(data, num);
					_values[j] = typedPropertyValue2;
				}
			}
			return num - startOffset;
		}
	}
}
