using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The SerAuxErrBar record specifies properties of an error bar.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class SerAuxErrBarRecord : RowDataRecord
	{
		public const short sid = 4187;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4187;

		public SerAuxErrBarRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
