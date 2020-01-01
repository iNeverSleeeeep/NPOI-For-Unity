using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Mulitple Blank cell record 
	/// Description:  Represents a  Set of columns in a row with no value but with styling.
	///               In this release we have Read-only support for this record type.
	///               The RecordFactory Converts this to a Set of BlankRecord objects.
	/// REFERENCE:  PG 329 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @version 2.0-pre
	/// @see org.apache.poi.hssf.record.BlankRecord
	public class MulBlankRecord : StandardRecord
	{
		public const short sid = 190;

		private int _row;

		private int _first_col;

		private short[] _xfs;

		private int _last_col;

		/// Get the row number of the cells this represents
		///
		/// @return row number
		public int Row => _row;

		/// starting column (first cell this holds in the row)
		/// @return first column number
		public int FirstColumn => _first_col;

		/// ending column (last cell this holds in the row)
		/// @return first column number
		public int LastColumn => _last_col;

		/// Get the number of columns this Contains (last-first +1)
		/// @return number of columns (last - first +1)
		public int NumColumns => _last_col - _first_col + 1;

		public override short Sid => 190;

		protected override int DataSize => 6 + _xfs.Length * 2;

		/// Creates new MulBlankRecord 
		public MulBlankRecord()
		{
		}

		public MulBlankRecord(int row, int firstCol, short[] xfs)
		{
			_row = row;
			_first_col = firstCol;
			_xfs = xfs;
			_last_col = firstCol + xfs.Length - 1;
		}

		/// Constructs a MulBlank record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public MulBlankRecord(RecordInputStream in1)
		{
			_row = in1.ReadUShort();
			_first_col = in1.ReadShort();
			_xfs = ParseXFs(in1);
			_last_col = in1.ReadShort();
		}

		/// returns the xf index for column (coffset = column - field_2_first_col)
		/// @param coffset  the column (coffset = column - field_2_first_col)
		/// @return the XF index for the column
		public short GetXFAt(int coffset)
		{
			return _xfs[coffset];
		}

		private short[] ParseXFs(RecordInputStream in1)
		{
			short[] array = new short[(in1.Remaining - 2) / 2];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = in1.ReadShort();
			}
			return array;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[MULBLANK]\n");
			stringBuilder.Append("row  = ").Append(StringUtil.ToHexString(Row)).Append("\n");
			stringBuilder.Append("firstcol  = ").Append(StringUtil.ToHexString(FirstColumn)).Append("\n");
			stringBuilder.Append(" lastcol  = ").Append(StringUtil.ToHexString(LastColumn)).Append("\n");
			for (int i = 0; i < NumColumns; i++)
			{
				stringBuilder.Append("xf").Append(i).Append("        = ")
					.Append(StringUtil.ToHexString(GetXFAt(i)))
					.Append("\n");
			}
			stringBuilder.Append("[/MULBLANK]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_row);
			out1.WriteShort(_first_col);
			int num = _xfs.Length;
			for (int i = 0; i < num; i++)
			{
				out1.WriteShort(_xfs[i]);
			}
			out1.WriteShort(_last_col);
		}

		public override object Clone()
		{
			return this;
		}
	}
}
