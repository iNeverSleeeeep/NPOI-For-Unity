using NPOI.Util;

namespace NPOI.HPSF
{
	public class UnicodeString
	{
		private byte[] _value;

		public int Size => 4 + _value.Length;

		public byte[] Value => _value;

		public UnicodeString(byte[] data, int offset)
		{
			int @int = LittleEndian.GetInt(data, offset);
			if (@int == 0)
			{
				_value = new byte[0];
			}
			else
			{
				_value = LittleEndian.GetByteArray(data, offset + 4, @int * 2);
				if (_value[@int * 2 - 1] != 0 || _value[@int * 2 - 2] != 0)
				{
					throw new IllegalPropertySetDataException("UnicodeString started at offset #" + offset + " is not NULL-terminated");
				}
			}
		}

		public string ToJavaString()
		{
			if (_value.Length == 0)
			{
				return null;
			}
			string fromUnicodeLE = StringUtil.GetFromUnicodeLE(_value, 0, _value.Length >> 1);
			int num = fromUnicodeLE.IndexOf('\0');
			if (num == -1)
			{
				return fromUnicodeLE;
			}
			int num2 = fromUnicodeLE.Length - 1;
			return fromUnicodeLE.Substring(0, num);
		}
	}
}
