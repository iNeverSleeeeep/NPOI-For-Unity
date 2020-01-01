using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The BopPopCustom record specifies which data points in the series are contained 
	/// in the secondary bar/pie instead of the primary pie. MUST follow a BopPop record 
	/// that has its split field set to Custom (0x0003).
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class BopPopCustomRecord : RowDataRecord
	{
		public const short sid = 4199;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4199;

		public BopPopCustomRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
