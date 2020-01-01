namespace NPOI.HSSF
{
	internal struct XmlTkDWord
	{
		/// <summary>
		/// The xtHeader.drType field MUST be equal to 0x04.
		/// </summary>
		public XmlTkHeader xtHeader;

		public int dValue;
	}
}
