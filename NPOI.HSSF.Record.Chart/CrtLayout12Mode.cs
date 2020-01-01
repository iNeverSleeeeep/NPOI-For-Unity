namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The CrtLayout12Mode specifies a layout mode. Each layout mode specifies a different 
	/// meaning of the x, y, dx, and dy fields of CrtLayout12 and CrtLayout12A.
	/// </summary>
	public enum CrtLayout12Mode : short
	{
		/// <summary>
		/// Position and dimension (2) are determined by the application. x, y, dx and dy MUST be ignored.
		/// </summary>
		L12MAUTO,
		/// <summary>
		/// x and y specify the offset of the top left corner, relative to its default position, 
		/// as a fraction of the chart area. MUST be greater than or equal to -1.0 and MUST be 
		/// less than or equal to 1.0. dx and dy specify the width and height, as a fraction of 
		/// the chart area, MUST be greater than or equal to 0.0, and MUST be less than or equal to 1.0.
		/// </summary>
		L12MFACTOR,
		/// <summary>
		/// x and y specify the offset of the upper-left corner; dx and dy specify the offset of the bottom-right corner. 
		/// x, y, dx and dy are specified relative to the upper-left corner of the chart area as a fraction of the chart area. 
		/// x, y, dx and dy MUST be greater than or equal to 0.0, and MUST be less than or equal to 1.0.
		/// </summary>
		L12MEDGE
	}
}
