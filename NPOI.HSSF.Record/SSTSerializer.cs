using NPOI.HSSF.Record.Cont;
using NPOI.Util;

namespace NPOI.HSSF.Record
{
	/// This class handles serialization of SST records.  It utilizes the record processor
	/// class write individual records. This has been refactored from the SSTRecord class.
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	public class SSTSerializer
	{
		private int _numStrings;

		private int _numUniqueStrings;

		private IntMapper<UnicodeString> strings;

		/// OffSets from the beginning of the SST record (even across continuations) 
		private int[] bucketAbsoluteOffsets;

		/// OffSets relative the start of the current SST or continue record 
		private int[] bucketRelativeOffsets;

		public int[] BucketAbsoluteOffsets => bucketAbsoluteOffsets;

		public int[] BucketRelativeOffsets => bucketRelativeOffsets;

		public SSTSerializer(IntMapper<UnicodeString> strings, int numStrings, int numUniqueStrings)
		{
			this.strings = strings;
			_numStrings = numStrings;
			_numUniqueStrings = numUniqueStrings;
			int numberOfInfoRecsForStrings = ExtSSTRecord.GetNumberOfInfoRecsForStrings(strings.Size);
			bucketAbsoluteOffsets = new int[numberOfInfoRecsForStrings];
			bucketRelativeOffsets = new int[numberOfInfoRecsForStrings];
		}

		public void Serialize(ContinuableRecordOutput out1)
		{
			out1.WriteInt(_numStrings);
			out1.WriteInt(_numUniqueStrings);
			for (int i = 0; i < strings.Size; i++)
			{
				if (i % 8 == 0)
				{
					int totalSize = out1.TotalSize;
					int num = i / 8;
					if (num < 128)
					{
						bucketAbsoluteOffsets[num] = totalSize;
						bucketRelativeOffsets[num] = totalSize;
					}
				}
				UnicodeString unicodeString = GetUnicodeString(i);
				unicodeString.Serialize(out1);
			}
		}

		private UnicodeString GetUnicodeString(int index)
		{
			return GetUnicodeString(strings, index);
		}

		private static UnicodeString GetUnicodeString(IntMapper<UnicodeString> strings, int index)
		{
			return strings[index];
		}
	}
}
