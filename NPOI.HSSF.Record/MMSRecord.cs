using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: MMS Record
	/// Description: defines how many Add menu and del menu options are stored
	///                    in the file. Should always be Set to 0 for HSSF workbooks
	/// REFERENCE:  PG 328 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class MMSRecord : StandardRecord
	{
		public const short sid = 193;

		private byte field_1_AddMenuCount;

		private byte field_2_delMenuCount;

		/// Set number of Add menu options (Set to 0)
		/// @param am  number of Add menu options
		public byte AddMenuCount
		{
			get
			{
				return field_1_AddMenuCount;
			}
			set
			{
				field_1_AddMenuCount = value;
			}
		}

		/// Set number of del menu options (Set to 0)
		/// @param dm  number of del menu options
		public byte DelMenuCount
		{
			get
			{
				return field_2_delMenuCount;
			}
			set
			{
				field_2_delMenuCount = value;
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 193;

		public MMSRecord()
		{
		}

		/// Constructs a MMS record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public MMSRecord(RecordInputStream in1)
		{
			if (in1.Remaining != 0)
			{
				field_1_AddMenuCount = (byte)in1.ReadByte();
				field_2_delMenuCount = (byte)in1.ReadByte();
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[MMS]\n");
			stringBuilder.Append("    .addMenu        = ").Append(StringUtil.ToHexString(AddMenuCount)).Append("\n");
			stringBuilder.Append("    .delMenu        = ").Append(StringUtil.ToHexString(DelMenuCount)).Append("\n");
			stringBuilder.Append("[/MMS]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte(AddMenuCount);
			out1.WriteByte(DelMenuCount);
		}
	}
}
