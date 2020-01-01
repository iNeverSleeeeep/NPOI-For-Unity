namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// pecifies positioning mode for position information saved in a Pos record.
	/// </summary>
	public enum PositionMode : short
	{
		/// <summary>
		/// Relative position to the chart, in points.
		/// </summary>
		MDFX = 0,
		/// <summary>
		/// Absolute width and height in points. It can only be applied to the mdBotRt field of Pos.
		/// </summary>
		MDABS = 1,
		/// <summary>
		/// Owner of Pos determines how to interpret the position data.
		/// </summary>
		MDPARENT = 2,
		/// <summary>
		/// Offset to default position, in 1/1000th of the plot area size.
		/// </summary>
		MDKTH = 3,
		/// <summary>
		/// Relative position to the chart, in SPRC.
		/// </summary>
		MDCHART = 5
	}
}
