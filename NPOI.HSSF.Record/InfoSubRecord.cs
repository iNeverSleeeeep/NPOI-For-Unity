using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Extended SST table info subrecord
	/// Contains the elements of "info" in the SST's array field
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	/// @see org.apache.poi.hssf.record.ExtSSTRecord
	public class InfoSubRecord
	{
		public const int ENCODED_SIZE = 8;

		public const short sid = 4095;

		private int field_1_stream_pos;

		private int field_2_bucket_sst_offset;

		private short field_3_zero;

		public int StreamPos
		{
			get
			{
				return field_1_stream_pos;
			}
			set
			{
				field_1_stream_pos = value;
			}
		}

		public short BucketRecordOffset
		{
			set
			{
				field_2_bucket_sst_offset = value;
			}
		}

		public int BucketSSTOffset => field_2_bucket_sst_offset;

		/// Creates new ExtSSTInfoSubRecord 
		public InfoSubRecord(int streamPos, int bucketSstOffset)
		{
			field_1_stream_pos = streamPos;
			field_2_bucket_sst_offset = bucketSstOffset;
		}

		public InfoSubRecord(RecordInputStream in1)
		{
			field_1_stream_pos = in1.ReadInt();
			field_2_bucket_sst_offset = in1.ReadShort();
			field_3_zero = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[EXTSST]\n");
			stringBuilder.Append("    .streampos      = ").Append(StringUtil.ToHexString(StreamPos)).Append("\n");
			stringBuilder.Append("    .bucketsstoffset= ").Append(StringUtil.ToHexString(BucketSSTOffset)).Append("\n");
			stringBuilder.Append("    .zero           = ").Append(StringUtil.ToHexString(field_3_zero)).Append("\n");
			stringBuilder.Append("[/EXTSST]\n");
			return stringBuilder.ToString();
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(field_1_stream_pos);
			out1.WriteShort(field_2_bucket_sst_offset);
			out1.WriteShort(field_3_zero);
		}
	}
}
