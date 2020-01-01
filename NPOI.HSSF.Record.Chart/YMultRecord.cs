using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The YMult record specifies properties of the value multiplier for a value axis and 
	/// that specifies the beginning of a collection of records as defined by the Chart Sheet 
	/// substream ABNF. The collection of records specifies a display units label.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class YMultRecord : RowDataRecord
	{
		public const short sid = 2135;

		protected override int DataSize => base.DataSize;

		public override short Sid => 2135;

		public YMultRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
