using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Index Record
	/// Description:  Occurs right after BOF, tells you where the DBCELL records are for a sheet
	///               Important for locating cells
	/// NOT USED IN THIS RELEASE
	/// REFERENCE:  PG 323 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class IndexRecord : StandardRecord
	{
		public const short sid = 523;

		public int field_1_zero;

		public int field_2_first_row;

		public int field_3_last_row_add1;

		public int field_4_zero;

		public IntList field_5_dbcells;

		public int FirstRow
		{
			get
			{
				return field_2_first_row;
			}
			set
			{
				field_2_first_row = value;
			}
		}

		public int LastRowAdd1
		{
			get
			{
				return field_3_last_row_add1;
			}
			set
			{
				field_3_last_row_add1 = value;
			}
		}

		public int NumDbcells
		{
			get
			{
				if (field_5_dbcells == null)
				{
					return 0;
				}
				return field_5_dbcells.Count;
			}
		}

		protected override int DataSize => 16 + NumDbcells * 4;

		public override short Sid => 523;

		public IndexRecord()
		{
		}

		/// Constructs an Index record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public IndexRecord(RecordInputStream in1)
		{
			field_1_zero = in1.ReadInt();
			if (field_1_zero != 0)
			{
				throw new RecordFormatException("Expected zero for field 1 but got " + field_1_zero);
			}
			field_2_first_row = in1.ReadInt();
			field_3_last_row_add1 = in1.ReadInt();
			field_4_zero = in1.ReadInt();
			int num = in1.Remaining / 4;
			field_5_dbcells = new IntList(num);
			for (int i = 0; i < num; i++)
			{
				field_5_dbcells.Add(in1.ReadInt());
			}
		}

		public void AddDbcell(int cell)
		{
			if (field_5_dbcells == null)
			{
				field_5_dbcells = new IntList();
			}
			field_5_dbcells.Add(cell);
		}

		public void SetDbcell(int cell, int value)
		{
			field_5_dbcells.Set(cell, value);
		}

		public int GetDbcellAt(int cellnum)
		{
			return field_5_dbcells.Get(cellnum);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[INDEX]\n");
			stringBuilder.Append("    .firstrow       = ").Append(StringUtil.ToHexString(FirstRow)).Append("\n");
			stringBuilder.Append("    .lastrowadd1    = ").Append(StringUtil.ToHexString(LastRowAdd1)).Append("\n");
			for (int i = 0; i < NumDbcells; i++)
			{
				stringBuilder.Append("    .dbcell_" + i + "       = ").Append(StringUtil.ToHexString(GetDbcellAt(i))).Append("\n");
			}
			stringBuilder.Append("[/INDEX]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(0);
			out1.WriteInt(FirstRow);
			out1.WriteInt(LastRowAdd1);
			out1.WriteInt(field_4_zero);
			for (int i = 0; i < NumDbcells; i++)
			{
				out1.WriteInt(GetDbcellAt(i));
			}
		}

		/// Returns the size of an INdexRecord when it needs to index the specified number of blocks
		public static int GetRecordSizeForBlockCount(int blockCount)
		{
			return 20 + 4 * blockCount;
		}

		public override object Clone()
		{
			IndexRecord indexRecord = new IndexRecord();
			indexRecord.field_1_zero = field_1_zero;
			indexRecord.field_2_first_row = field_2_first_row;
			indexRecord.field_3_last_row_add1 = field_3_last_row_add1;
			indexRecord.field_4_zero = field_4_zero;
			indexRecord.field_5_dbcells = new IntList();
			indexRecord.field_5_dbcells.AddAll(field_5_dbcells);
			return indexRecord;
		}
	}
}
