using NPOI.Util;

namespace NPOI.HPSF
{
	public class VariantBool
	{
		public const int SIZE = 2;

		private bool _value;

		public bool Value
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

		public VariantBool(byte[] data, int offset)
		{
			_value = (LittleEndian.GetShort(data, offset) != 0);
		}
	}
}
