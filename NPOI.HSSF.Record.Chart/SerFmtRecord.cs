using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The SerFmt record specifies properties of the associated data points, data markers, 
	/// or lines of the series. The associated data points, data markers, or lines of the 
	/// series are specified by the preceding DataFormat record. If this record is not 
	/// present in the sequence of records that conforms to the SS rule of the Chart Sheet 
	/// Substream ABNF, then the properties of the associated data points, data markers, 
	/// or lines of the series are specified by the default values of the fields of this record.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class SerFmtRecord : RowDataRecord
	{
		public const short sid = 4189;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4189;

		public SerFmtRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
