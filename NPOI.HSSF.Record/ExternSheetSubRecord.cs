using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        A sub Record for Extern Sheet 
	/// Description:  Defines a named range within a workbook. 
	/// REFERENCE:  
	/// @author Libin Roman (Vista Portal LDT. Developer)
	/// @version 1.0-pre
	public class ExternSheetSubRecord : Record
	{
		public const short sid = 4095;

		private short field_1_index_to_supbook;

		private short field_2_index_to_first_supbook_sheet;

		private short field_3_index_to_last_supbook_sheet;

		/// returns the record size
		public override int RecordSize => 6;

		/// return the non static version of the id for this record.
		public override short Sid => 4095;

		/// a Constractor for making new sub record
		public ExternSheetSubRecord()
		{
		}

		/// Constructs a Extern Sheet Sub Record record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public ExternSheetSubRecord(RecordInputStream in1)
		{
			field_1_index_to_supbook = in1.ReadShort();
			field_2_index_to_first_supbook_sheet = in1.ReadShort();
			field_3_index_to_last_supbook_sheet = in1.ReadShort();
		}

		/// Sets the Index to the sup book
		/// @param index sup book index
		public void SetIndexToSupBook(short index)
		{
			field_1_index_to_supbook = index;
		}

		/// Gets the index to sup book
		/// @return sup book index
		public short GetIndexToSupBook()
		{
			return field_1_index_to_supbook;
		}

		/// Sets the index to first sheet in supbook
		/// @param index index to first sheet
		public void SetIndexToFirstSupBook(short index)
		{
			field_2_index_to_first_supbook_sheet = index;
		}

		/// Gets the index to first sheet from supbook
		/// @return index to first supbook
		public short GetIndexToFirstSupBook()
		{
			return field_2_index_to_first_supbook_sheet;
		}

		/// Sets the index to last sheet in supbook
		/// @param index index to last sheet
		public void SetIndexToLastSupBook(short index)
		{
			field_3_index_to_last_supbook_sheet = index;
		}

		/// Gets the index to last sheet in supbook
		/// @return index to last supbook
		public short GetIndexToLastSupBook()
		{
			return field_3_index_to_last_supbook_sheet;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("   supbookindex =").Append(GetIndexToSupBook()).Append('\n');
			stringBuilder.Append("   1stsbindex   =").Append(GetIndexToFirstSupBook()).Append('\n');
			stringBuilder.Append("   lastsbindex  =").Append(GetIndexToLastSupBook()).Append('\n');
			return stringBuilder.ToString();
		}

		/// called by the class that Is responsible for writing this sucker.
		/// Subclasses should implement this so that their data Is passed back in a
		/// byte array.
		///
		/// @param offset to begin writing at
		/// @param data byte array containing instance data
		/// @return number of bytes written
		public override int Serialize(int offset, byte[] data)
		{
			LittleEndian.PutShort(data, offset, GetIndexToSupBook());
			LittleEndian.PutShort(data, 2 + offset, GetIndexToFirstSupBook());
			LittleEndian.PutShort(data, 4 + offset, GetIndexToLastSupBook());
			return RecordSize;
		}
	}
}
