namespace NPOI.HPSF
{
	public class IndirectPropertyName
	{
		private CodePageString _value;

		public int Size => _value.Size;

		public IndirectPropertyName(byte[] data, int offset)
		{
			_value = new CodePageString(data, offset);
		}
	}
}
