namespace NPOI.HSSF
{
	internal struct XmlTkString
	{
		/// <summary>
		/// The xtHeader.drType field MUST be equal to 0x05.
		/// </summary>
		public XmlTkHeader xtHeader;

		public uint cchValue;

		/// <summary>
		/// An array of Unicode characters. The size of the array, in characters, is specified 
		/// by the cchValue field. The size of the field, in bytes, MUST equal the result of 
		/// the following formula:cchValue * 2.
		/// </summary>
		public char[] rgbValue;
	}
}
