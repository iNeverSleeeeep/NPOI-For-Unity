using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        GridSet Record.
	/// Description:  flag denoting whether the user specified that gridlines are used when
	///               printing.
	/// REFERENCE:  PG 320 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	///
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author  Glen Stampoultzis (glens at apache.org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	///
	/// @version 2.0-pre
	public class GridsetRecord : StandardRecord
	{
		public const short sid = 130;

		public short field_1_gridset_flag;

		/// Get whether the gridlines are shown during printing.
		///
		/// @return gridSet - true if gridlines are NOT printed, false if they are.
		public bool Gridset
		{
			get
			{
				return field_1_gridset_flag == 1;
			}
			set
			{
				if (value)
				{
					field_1_gridset_flag = 1;
				}
				else
				{
					field_1_gridset_flag = 0;
				}
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 130;

		public GridsetRecord()
		{
		}

		/// Constructs a GridSet record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public GridsetRecord(RecordInputStream in1)
		{
			field_1_gridset_flag = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[GRIDSET]\n");
			stringBuilder.Append("    .gridset        = ").Append(Gridset).Append("\n");
			stringBuilder.Append("[/GRIDSET]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_gridset_flag);
		}

		public override object Clone()
		{
			GridsetRecord gridsetRecord = new GridsetRecord();
			gridsetRecord.field_1_gridset_flag = field_1_gridset_flag;
			return gridsetRecord;
		}
	}
}
