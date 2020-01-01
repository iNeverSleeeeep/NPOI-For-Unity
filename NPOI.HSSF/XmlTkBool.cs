namespace NPOI.HSSF
{
	internal struct XmlTkBool
	{
		/// <summary>
		/// The xtHeader.drType field MUST be equal to 0x02.
		/// </summary>
		public XmlTkHeader xtHeader;

		public byte dValue;

		public byte unused;
	}
}
