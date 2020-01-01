using System;

namespace NPOI.Util
{
	public class Number
	{
		private static Type BoolType = typeof(bool);

		private static Type CharType = typeof(char);

		private static Type IntPtrType = typeof(IntPtr);

		private static Type UIntPtrType = typeof(UIntPtr);

		private static Type DecimalType = typeof(decimal);

		public static bool IsNumber(object value)
		{
			if (value == null)
			{
				return false;
			}
			Type type = value.GetType();
			if (type.IsPrimitive || type == DecimalType)
			{
				if (type != BoolType && type != CharType && type != IntPtrType)
				{
					return type != UIntPtrType;
				}
				return false;
			}
			return false;
		}

		public static bool IsInteger(object value)
		{
			if (value == null)
			{
				return false;
			}
			if (value is int)
			{
				return true;
			}
			if (value is uint)
			{
				return true;
			}
			if (value is long)
			{
				return true;
			}
			if (value is ulong)
			{
				return true;
			}
			if (value is sbyte)
			{
				return true;
			}
			if (value is byte)
			{
				return true;
			}
			if (value is short)
			{
				return true;
			}
			if (value is ushort)
			{
				return true;
			}
			return false;
		}
	}
}
