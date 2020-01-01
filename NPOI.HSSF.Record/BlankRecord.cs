using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Blank cell record 
	/// Description:  Represents a column in a row with no value but with styling.
	/// REFERENCE:  PG 287 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class BlankRecord : StandardRecord, CellValueRecordInterface, IComparable
	{
		public const short sid = 513;

		private int field_1_row;

		private int field_2_col;

		private short field_3_xf;

		/// Get the row this cell occurs on
		///
		/// @return the row
		public int Row
		{
			get
			{
				return field_1_row;
			}
			set
			{
				field_1_row = value;
			}
		}

		/// Get the column this cell defines within the row
		///
		/// @return the column
		public int Column
		{
			get
			{
				return field_2_col;
			}
			set
			{
				field_2_col = value;
			}
		}

		/// Set the index of the extended format record to style this cell with
		///
		/// @param xf - the 0-based index of the extended format
		/// @see org.apache.poi.hssf.record.ExtendedFormatRecord
		public short XFIndex
		{
			get
			{
				return field_3_xf;
			}
			set
			{
				field_3_xf = value;
			}
		}

		/// return the non static version of the id for this record.
		public override short Sid => 513;

		protected override int DataSize => 6;

		/// Creates a new instance of BlankRecord 
		public BlankRecord()
		{
		}

		/// Constructs a BlankRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public BlankRecord(RecordInputStream in1)
		{
			field_1_row = in1.ReadUShort();
			field_2_col = in1.ReadShort();
			field_3_xf = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BLANK]\n");
			stringBuilder.Append("row       = ").Append(HexDump.ShortToHex(Row)).Append("\n");
			stringBuilder.Append("col       = ").Append(HexDump.ShortToHex(Column)).Append("\n");
			stringBuilder.Append("xf        = ").Append(HexDump.ShortToHex(XFIndex)).Append("\n");
			stringBuilder.Append("[/BLANK]\n");
			return stringBuilder.ToString();
		}

		/// called by the class that is responsible for writing this sucker.
		/// Subclasses should implement this so that their data is passed back in a
		/// byte array.
		///
		/// @return byte array containing instance data
		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Row);
			out1.WriteShort(Column);
			out1.WriteShort(XFIndex);
		}

		public int CompareTo(object obj)
		{
			CellValueRecordInterface cellValueRecordInterface = (CellValueRecordInterface)obj;
			if (Row == cellValueRecordInterface.Row && Column == cellValueRecordInterface.Column)
			{
				return 0;
			}
			if (Row < cellValueRecordInterface.Row)
			{
				return -1;
			}
			if (Row > cellValueRecordInterface.Row)
			{
				return 1;
			}
			if (Column < cellValueRecordInterface.Column)
			{
				return -1;
			}
			if (Column > cellValueRecordInterface.Column)
			{
				return 1;
			}
			return -1;
		}

		public override object Clone()
		{
			BlankRecord blankRecord = new BlankRecord();
			blankRecord.field_1_row = field_1_row;
			blankRecord.field_2_col = field_2_col;
			blankRecord.field_3_xf = field_3_xf;
			return blankRecord;
		}
	}
}
