namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// specifies the text elements that are formatted using the position and appearance information 
	/// specified by the Text record immediately following this record.
	/// </summary>
	public enum TextFormatInfo : short
	{
		/// <summary>
		/// Format all Text records in the chart group where fShowPercent is equal to 0 or fShowValue is equal to 0.
		/// </summary>
		ShowPercentOrValueNotSet,
		/// <summary>
		/// Format all Text records in the chart group where fShowPercent is equal to 1 or fShowValue is equal to 1.
		/// </summary>
		ShowPercentOrValueSet,
		/// <summary>
		/// Format all Text records in the chart where the value of fScaled of the associated FontInfo structure is equal to 0.
		/// </summary>
		FontScaleNotSet,
		/// <summary>
		/// Format all Text records in the chart where the value of fScaled of the associated FontInfo structure is equal to 1.
		/// </summary>
		FontScaleSet
	}
}
