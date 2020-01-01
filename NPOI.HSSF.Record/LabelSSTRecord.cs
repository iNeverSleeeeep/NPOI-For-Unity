using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Label SST Record
	/// Description:  Refers to a string in the shared string table and Is a column
	///               value.  
	/// REFERENCE:  PG 325 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	[Serializable]
	public class LabelSSTRecord : CellRecord
	{
		public const short sid = 253;

		private int field_4_sst_index;

		protected override string RecordName => "LABELSST";

		/// Get the index to the string in the SSTRecord
		///
		/// @return index of string in the SST Table
		/// @see org.apache.poi.hssf.record.SSTRecord
		public int SSTIndex
		{
			get
			{
				return field_4_sst_index;
			}
			set
			{
				field_4_sst_index = value;
			}
		}

		protected override int ValueDataSize => 4;

		public override short Sid => 253;

		public LabelSSTRecord()
		{
		}

		/// Constructs an LabelSST record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public LabelSSTRecord(RecordInputStream in1)
			: base(in1)
		{
			field_4_sst_index = in1.ReadInt();
		}

		protected override void AppendValueText(StringBuilder sb)
		{
			sb.Append("  .sstIndex = ");
			sb.Append(HexDump.ShortToHex(SSTIndex));
		}

		protected override void SerializeValue(ILittleEndianOutput out1)
		{
			out1.WriteInt(SSTIndex);
		}

		public override object Clone()
		{
			LabelSSTRecord labelSSTRecord = new LabelSSTRecord();
			CopyBaseFields(labelSSTRecord);
			labelSSTRecord.field_4_sst_index = field_4_sst_index;
			return labelSSTRecord;
		}
	}
}
