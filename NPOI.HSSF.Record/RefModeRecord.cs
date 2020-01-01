using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        RefMode Record
	/// Description:  Describes which reference mode to use
	/// REFERENCE:  PG 376 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class RefModeRecord : StandardRecord
	{
		public const short sid = 15;

		public const short USE_A1_MODE = 1;

		public const short USE_R1C1_MODE = 0;

		private short field_1_mode;

		/// Get the reference mode to use (HSSF uses/assumes A1)
		/// @return mode to use
		/// @see #USE_A1_MODE
		/// @see #USE_R1C1_MODE
		public short Mode
		{
			get
			{
				return field_1_mode;
			}
			set
			{
				field_1_mode = value;
			}
		}

		public override short Sid => 15;

		protected override int DataSize => 2;

		public RefModeRecord()
		{
		}

		/// Constructs a RefMode record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public RefModeRecord(RecordInputStream in1)
		{
			field_1_mode = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[REFMODE]\n");
			stringBuilder.Append("    .mode           = ").Append(StringUtil.ToHexString(Mode)).Append("\n");
			stringBuilder.Append("[/REFMODE]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			RefModeRecord refModeRecord = new RefModeRecord();
			refModeRecord.field_1_mode = field_1_mode;
			return refModeRecord;
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Mode);
		}
	}
}
