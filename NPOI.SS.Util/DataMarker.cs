using NPOI.SS.UserModel;

namespace NPOI.SS.Util
{
	/// <summary>
	/// Represents data marker used in charts.
	/// @author Roman Kashitsyn
	/// </summary>
	public class DataMarker
	{
		private ISheet sheet;

		private CellRangeAddress range;

		/// <summary>
		/// get or set the sheet marker points to.
		/// </summary>
		public ISheet Sheet
		{
			get
			{
				return sheet;
			}
			set
			{
				sheet = value;
			}
		}

		/// <summary>
		/// get or set range of the marker.
		/// </summary>
		public CellRangeAddress Range
		{
			get
			{
				return range;
			}
			set
			{
				range = value;
			}
		}

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="sheet">the sheet where data located.</param>
		/// <param name="range">the range within that sheet.</param>
		public DataMarker(ISheet sheet, CellRangeAddress range)
		{
			this.sheet = sheet;
			this.range = range;
		}

		/// <summary>
		/// Formats data marker using canonical format, for example
		/// 'SheetName!$A$1:$A$5'.
		/// </summary>
		/// <returns>formatted data marker</returns>
		public string FormatAsString()
		{
			string sheetName = (sheet == null) ? null : sheet.SheetName;
			if (range == null)
			{
				return null;
			}
			return range.FormatAsString(sheetName, useAbsoluteAddress: true);
		}
	}
}
