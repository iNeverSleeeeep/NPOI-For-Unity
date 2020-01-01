namespace NPOI.HSSF
{
	internal struct XmlTkBlob
	{
		/// <summary>
		/// The xtHeader.drType field MUST be equal to 0x07.
		/// </summary>
		public XmlTkHeader xtHeader;

		public uint cbBlob;

		public byte[] rgbBlob;
	}
}
