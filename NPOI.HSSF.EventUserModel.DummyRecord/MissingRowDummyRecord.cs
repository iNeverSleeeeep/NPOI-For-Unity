namespace NPOI.HSSF.EventUserModel.DummyRecord
{
	/// A dummy record for when we're missing a row, but still
	///  want to trigger something
	public class MissingRowDummyRecord : DummyRecordBase
	{
		private int rowNumber;

		public int RowNumber => rowNumber;

		public MissingRowDummyRecord(int rowNumber)
		{
			this.rowNumber = rowNumber;
		}
	}
}
