using NPOI.HSSF.Util;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Used to store multiple RK numbers on a row.  1 MulRk = Multiple Cell values.
	/// HSSF just Converts this into multiple NUMBER records.  Read-ONLY SUPPORT!
	/// REFERENCE:  PG 330 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class MulRKRecord : StandardRecord
	{
		private class RkRec
		{
			public const int ENCODED_SIZE = 6;

			public short xf;

			public int rk;

			private RkRec(RecordInputStream in1)
			{
				xf = in1.ReadShort();
				rk = in1.ReadInt();
			}

			public static RkRec[] ParseRKs(RecordInputStream in1)
			{
				int num = (in1.Remaining - 2) / 6;
				RkRec[] array = new RkRec[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = new RkRec(in1);
				}
				return array;
			}
		}

		public const short sid = 189;

		private int field_1_row;

		private short field_2_first_col;

		private RkRec[] field_3_rks;

		private short field_4_last_col;

		public int Row => field_1_row;

		/// starting column (first cell this holds in the row)
		/// @return first column number
		public short FirstColumn => field_2_first_col;

		/// ending column (last cell this holds in the row)
		/// @return first column number
		public short LastColumn => field_4_last_col;

		/// Get the number of columns this Contains (last-first +1)
		/// @return number of columns (last - first +1)
		public int NumColumns => field_4_last_col - field_2_first_col + 1;

		public override short Sid => 189;

		protected override int DataSize
		{
			get
			{
				throw new RecordFormatException("Sorry, you can't serialize MulRK in this release");
			}
		}

		/// Creates new MulRKRecord 
		public MulRKRecord()
		{
		}

		/// Constructs a MulRK record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public MulRKRecord(RecordInputStream in1)
		{
			field_1_row = in1.ReadUShort();
			field_2_first_col = in1.ReadShort();
			field_3_rks = RkRec.ParseRKs(in1);
			field_4_last_col = in1.ReadShort();
		}

		/// returns the xf index for column (coffset = column - field_2_first_col)
		/// @return the XF index for the column
		public short GetXFAt(int coffset)
		{
			return field_3_rks[coffset].xf;
		}

		/// returns the rk number for column (coffset = column - field_2_first_col)
		/// @return the value (decoded into a double)
		public double GetRKNumberAt(int coffset)
		{
			return RKUtil.DecodeNumber(field_3_rks[coffset].rk);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[MULRK]\n");
			stringBuilder.Append("\t.row\t = ").Append(HexDump.ShortToHex(Row)).Append("\n");
			stringBuilder.Append(" .firstcol= ").Append(StringUtil.ToHexString(FirstColumn)).Append("\n");
			stringBuilder.Append(" .lastcol = ").Append(StringUtil.ToHexString(LastColumn)).Append("\n");
			for (int i = 0; i < NumColumns; i++)
			{
				stringBuilder.Append(" xf[").Append(i).Append("] = ")
					.Append(StringUtil.ToHexString(GetXFAt(i)))
					.Append("\n");
				stringBuilder.Append(" rk[").Append(i).Append("] = ")
					.Append(GetRKNumberAt(i))
					.Append("\n");
			}
			stringBuilder.Append("[/MULRK]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			throw new RecordFormatException("Sorry, you can't serialize MulRK in this release");
		}
	}
}
