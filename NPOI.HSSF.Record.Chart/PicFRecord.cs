using NPOI.Util;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The PicF record specifies the layout of a picture that is attached to a picture-filled chart element.
	/// </summary>
	/// <remarks>
	/// author: Antony liu (antony.apollo at gmail.com)
	/// </remarks>
	public class PicFRecord : RowDataRecord
	{
		public const short sid = 4156;

		protected override int DataSize => base.DataSize;

		public override short Sid => 4156;

		public PicFRecord(RecordInputStream ris)
			: base(ris)
		{
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			base.Serialize(out1);
		}
	}
}
