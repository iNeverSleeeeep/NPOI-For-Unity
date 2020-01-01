namespace NPOI.HSSF
{
	internal struct XmlTkDouble
	{
		/// <summary>
		/// The xtHeader.drType field MUST be equal to 0x03.
		/// </summary>
		public XmlTkHeader xtHeader;

		public int unused;

		public double dValue;
	}
}
