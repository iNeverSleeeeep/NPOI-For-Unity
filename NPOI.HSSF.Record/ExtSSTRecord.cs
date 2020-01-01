using NPOI.HSSF.Record.Cont;
using NPOI.Util;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Extended Static String Table
	/// Description: This record Is used for a quick Lookup into the SST record. This
	///              record breaks the SST table into a Set of buckets. The offsets
	///              to these buckets within the SST record are kept as well as the
	///              position relative to the start of the SST record.
	/// REFERENCE:  PG 313 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at apache dot org)
	/// @version 2.0-pre
	/// @see org.apache.poi.hssf.record.ExtSSTInfoSubRecord
	public class ExtSSTRecord : ContinuableRecord
	{
		public const short DEFAULT_BUCKET_SIZE = 8;

		public const int MAX_BUCKETS = 128;

		public const short sid = 255;

		private short field_1_strings_per_bucket = 8;

		private InfoSubRecord[] _sstInfos;

		public short NumStringsPerBucket
		{
			get
			{
				return field_1_strings_per_bucket;
			}
			set
			{
				field_1_strings_per_bucket = value;
			}
		}

		/// Returns the size of this record 
		internal int DataSize => 2 + 8 * _sstInfos.Length;

		internal InfoSubRecord[] InfoSubRecords => _sstInfos;

		public override short Sid => 255;

		public ExtSSTRecord()
		{
			_sstInfos = new InfoSubRecord[0];
		}

		/// Constructs a EOFRecord record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public ExtSSTRecord(RecordInputStream in1)
		{
			field_1_strings_per_bucket = in1.ReadShort();
			int capacity = in1.Remaining / 8;
			List<InfoSubRecord> list = new List<InfoSubRecord>(capacity);
			while (in1.Available() > 0)
			{
				InfoSubRecord item = new InfoSubRecord(in1);
				list.Add(item);
				if (in1.Available() == 0 && in1.HasNextRecord && in1.GetNextSid() == 60)
				{
					in1.NextRecord();
				}
			}
			_sstInfos = list.ToArray();
		}

		public IEnumerator GetEnumerator()
		{
			return _sstInfos.GetEnumerator();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[EXTSST]\n");
			stringBuilder.Append("    .dsst           = ").Append(StringUtil.ToHexString(NumStringsPerBucket)).Append("\n");
			stringBuilder.Append("    .numInfoRecords = ").Append(_sstInfos.Length).Append("\n");
			for (int i = 0; i < _sstInfos.Length; i++)
			{
				stringBuilder.Append("    .inforecord     = ").Append(i).Append("\n");
				stringBuilder.Append("    .streampos      = ").Append(StringUtil.ToHexString(_sstInfos[i].StreamPos)).Append("\n");
				stringBuilder.Append("    .sstoffset      = ").Append(StringUtil.ToHexString(_sstInfos[i].BucketSSTOffset)).Append("\n");
			}
			stringBuilder.Append("[/EXTSST]\n");
			return stringBuilder.ToString();
		}

		protected override void Serialize(ContinuableRecordOutput out1)
		{
			out1.WriteShort(field_1_strings_per_bucket);
			for (int i = 0; i < _sstInfos.Length; i++)
			{
				_sstInfos[i].Serialize(out1);
			}
		}

		public static int GetNumberOfInfoRecsForStrings(int numStrings)
		{
			int num = numStrings / 8;
			if (numStrings % 8 != 0)
			{
				num++;
			}
			if (num > 128)
			{
				num = 128;
			}
			return num;
		}

		/// Given a number of strings (in the sst), returns the size of the extsst record
		public static int GetRecordSizeForStrings(int numStrings)
		{
			return 6 + GetNumberOfInfoRecsForStrings(numStrings) * 8;
		}

		public void SetBucketOffsets(int[] bucketAbsoluteOffsets, int[] bucketRelativeOffsets)
		{
			_sstInfos = new InfoSubRecord[bucketAbsoluteOffsets.Length];
			for (int i = 0; i < bucketAbsoluteOffsets.Length; i++)
			{
				_sstInfos[i] = new InfoSubRecord(bucketAbsoluteOffsets[i], bucketRelativeOffsets[i]);
			}
		}
	}
}
