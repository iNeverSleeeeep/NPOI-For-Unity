namespace NPOI.HSSF.Record
{
	public class DrawingSelectionRecord : AbstractEscherHolderRecord
	{
		public const short sid = 237;

		protected override string RecordName => "MSODRAWINGSELECTION";

		public override short Sid => 237;

		public DrawingSelectionRecord()
		{
		}

		public DrawingSelectionRecord(RecordInputStream in1)
			: base(in1)
		{
		}
	}
}
