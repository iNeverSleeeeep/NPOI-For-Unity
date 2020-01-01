using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The CrtMlFrtContinue record specifies additional data for a CrtMlFrt record, as specified in the CrtMlFrt record.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class CrtMlFrtContinueRecord : RowDataRecord
	{
		public const short sid = 2207;

		protected override int DataSize => base.DataSize;

		public override short Sid => 2207;

		public CrtMlFrtContinueRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
