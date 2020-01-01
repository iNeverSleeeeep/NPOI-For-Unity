namespace NPOI.HSSF.EventUserModel.DummyRecord
{
	/// A dummy record for when we're missing a cell in a row,
	///  but still want to trigger something
	public class MissingCellDummyRecord : DummyRecordBase
	{
		private int row;

		private int column;

		public int Row => row;

		public int Column => column;

		public MissingCellDummyRecord(int row, int column)
		{
			this.row = row;
			this.column = column;
		}
	}
}
