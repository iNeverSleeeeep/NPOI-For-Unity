using NPOI.Util;
using System;

namespace NPOI.SS.Formula.Constant
{
	/// To support Constant Values (2.5.7) as required by the CRN record.
	/// This class is also used for two dimensional arrays which are encoded by 
	/// EXTERNALNAME (5.39) records and Array tokens.<p />
	///
	/// @author Josh Micich
	public class ConstantValueParser
	{
		private const int TYPE_EMPTY = 0;

		private const int TYPE_NUMBER = 1;

		private const int TYPE_STRING = 2;

		private const int TYPE_BOOLEAN = 4;

		private const int TYPE_ERROR_CODE = 16;

		private const int TRUE_ENCODING = 1;

		private const int FALSE_ENCODING = 0;

		private const object EMPTY_REPRESENTATION = null;

		private ConstantValueParser()
		{
		}

		public static object[] Parse(ILittleEndianInput in1, int nValues)
		{
			object[] array = new object[nValues];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ReadAConstantValue(in1);
			}
			return array;
		}

		private static object ReadAConstantValue(ILittleEndianInput in1)
		{
			byte b = (byte)in1.ReadByte();
			switch (b)
			{
			case 0:
				in1.ReadLong();
				return null;
			case 1:
				return in1.ReadDouble();
			case 2:
				return StringUtil.ReadUnicodeString(in1);
			case 4:
				return ReadBoolean(in1);
			case 16:
			{
				int errorCode = in1.ReadUShort();
				in1.ReadUShort();
				in1.ReadInt();
				return ErrorConstant.ValueOf(errorCode);
			}
			default:
				throw new Exception("Unknown grbit value (" + b + ")");
			}
		}

		private static object ReadBoolean(ILittleEndianInput in1)
		{
			byte b = (byte)in1.ReadLong();
			switch (b)
			{
			case 0:
				return false;
			case 1:
				return true;
			default:
				throw new Exception("unexpected bool encoding (" + b + ")");
			}
		}

		public static int GetEncodedSize(Array values)
		{
			int num = values.Length;
			for (int i = 0; i < values.Length; i++)
			{
				num += GetEncodedSize(values.GetValue(i));
			}
			return num;
		}

		/// @return encoded size without the 'type' code byte
		private static int GetEncodedSize(object obj)
		{
			if (obj == null)
			{
				return 8;
			}
			Type type = obj.GetType();
			if (type == typeof(bool) || type == typeof(double) || type == typeof(ErrorConstant))
			{
				return 8;
			}
			string value = (string)obj;
			return StringUtil.GetEncodedSize(value);
		}

		public static void Encode(ILittleEndianOutput out1, Array values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				EncodeSingleValue(out1, values.GetValue(i));
			}
		}

		private static void EncodeSingleValue(ILittleEndianOutput out1, object value)
		{
			if (value == null)
			{
				out1.WriteByte(0);
				out1.WriteLong(0L);
			}
			else if (value is bool)
			{
				bool flag = (bool)value;
				out1.WriteByte(4);
				long v = flag ? 1 : 0;
				out1.WriteLong(v);
			}
			else if (value is double)
			{
				double v2 = (double)value;
				out1.WriteByte(1);
				out1.WriteDouble(v2);
			}
			else if (value is string)
			{
				string value2 = (string)value;
				out1.WriteByte(2);
				StringUtil.WriteUnicodeString(out1, value2);
			}
			else
			{
				if (!(value is ErrorConstant))
				{
					throw new Exception("Unexpected value type (" + value.GetType().Name + "'");
				}
				ErrorConstant errorConstant = (ErrorConstant)value;
				out1.WriteByte(16);
				long v3 = errorConstant.ErrorCode;
				out1.WriteLong(v3);
			}
		}
	}
}
