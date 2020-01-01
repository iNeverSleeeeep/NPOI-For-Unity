namespace NPOI.HSSF.Record
{
	/// The cell value record interface Is implemented by all classes of type Record that
	/// contain cell values.  It allows the containing sheet to move through them and Compare
	/// them.
	///
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	///
	/// @see org.apache.poi.hssf.model.Sheet
	/// @see org.apache.poi.hssf.record.Record
	/// @see org.apache.poi.hssf.record.RecordFactory
	public interface CellValueRecordInterface
	{
		/// Get the row this cell occurs on
		///
		/// @return the row
		int Row
		{
			get;
			set;
		}

		/// Get the column this cell defines within the row
		///
		/// @return the column
		int Column
		{
			get;
			set;
		}

		short XFIndex
		{
			get;
			set;
		}
	}
}
