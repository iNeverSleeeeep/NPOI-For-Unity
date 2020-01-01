using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The DropBar record specifies the attributes of the up bars or the down bars between multiple 
	/// series of a line chart group and specifies the beginning of a collection of records as 
	/// defined by the Chart Sheet Substream ABNF. The first of these collections in the line chart 
	/// group specifies the attributes of the up bars. The second specifies the attributes of the 
	/// down bars. If this record exists, then the chart group type MUST be line and the field cSer 
	/// in the record SeriesList MUST be greater than 1.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class DropBarRecord : RowDataRecord
	{
		public const short sid = 4157;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4157;

		public DropBarRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
