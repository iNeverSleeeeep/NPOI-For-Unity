using System.IO;

namespace NPOI.HSSF.Record
{
	/// This Is purely for the biff viewer.  During normal operations we don't want
	/// to be seeing this.
	public class DrawingRecordForBiffViewer : AbstractEscherHolderRecord
	{
		public const short sid = 236;

		protected override string RecordName => "MSODRAWING";

		public override short Sid => 236;

		public DrawingRecordForBiffViewer()
		{
		}

		public DrawingRecordForBiffViewer(RecordInputStream in1)
			: base(in1)
		{
		}

		public DrawingRecordForBiffViewer(DrawingRecord r)
			: base(ConvertToInputStream(r))
		{
			ConvertRawBytesToEscherRecords();
		}

		private static RecordInputStream ConvertToInputStream(DrawingRecord r)
		{
			byte[] buffer = r.Serialize();
			using (MemoryStream @in = new MemoryStream(buffer))
			{
				RecordInputStream recordInputStream = new RecordInputStream(@in);
				recordInputStream.NextRecord();
				return recordInputStream;
			}
		}
	}
}
