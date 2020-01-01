using NPOI.HSSF.Record.Cont;
using NPOI.Util;
using System;
using System.Collections;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Static String Table Record
	///
	/// Description:  This holds all the strings for LabelSSTRecords.
	///
	/// REFERENCE:    PG 389 Microsoft Excel 97 Developer's Kit (ISBN:
	///               1-57231-498-2)
	///
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @author Glen Stampoultzis (glens at apache.org)
	///
	/// @see org.apache.poi.hssf.record.LabelSSTRecord
	/// @see org.apache.poi.hssf.record.ContinueRecord
	public class SSTRecord : ContinuableRecord
	{
		public const short sid = 252;

		/// how big can an SST record be? As big as any record can be: 8228 bytes 
		public const int MAX_RECORD_SIZE = 8228;

		/// standard record overhead: two shorts (record id plus data space size)
		public const int STD_RECORD_OVERHEAD = 4;

		/// SST overhead: the standard record overhead, plus the number of strings and the number of Unique strings -- two ints 
		public const int SST_RECORD_OVERHEAD = 12;

		/// how much data can we stuff into an SST record? That would be _max minus the standard SST record overhead 
		public const int MAX_DATA_SPACE = 8216;

		private static readonly UnicodeString EMPTY_STRING = new UnicodeString("");

		/// Union of strings in the SST and EXTSST 
		private int field_1_num_strings;

		/// according to docs ONLY SST 
		private int field_2_num_unique_strings;

		private IntMapper<UnicodeString> field_3_strings;

		private SSTDeserializer deserializer;

		/// Offsets from the beginning of the SST record (even across continuations) 
		private int[] bucketAbsoluteOffsets;

		/// Offsets relative the start of the current SST or continue record 
		private int[] bucketRelativeOffsets;

		/// @return number of strings
		public int NumStrings
		{
			get
			{
				return field_1_num_strings;
			}
			set
			{
				field_1_num_strings = value;
			}
		}

		/// @return number of Unique strings
		public int NumUniqueStrings
		{
			get
			{
				return field_2_num_unique_strings;
			}
			set
			{
				field_2_num_unique_strings = value;
			}
		}

		/// @return sid
		public override short Sid => 252;

		/// @return count of the strings we hold.
		public int CountStrings => field_3_strings.Size;

		/// default constructor
		public SSTRecord()
		{
			field_1_num_strings = 0;
			field_2_num_unique_strings = 0;
			field_3_strings = new IntMapper<UnicodeString>();
			deserializer = new SSTDeserializer(field_3_strings);
		}

		/// Constructs an SST record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public SSTRecord(RecordInputStream in1)
		{
			field_1_num_strings = in1.ReadInt();
			field_2_num_unique_strings = in1.ReadInt();
			field_3_strings = new IntMapper<UnicodeString>();
			deserializer = new SSTDeserializer(field_3_strings);
			deserializer.ManufactureStrings(field_2_num_unique_strings, in1);
		}

		/// Add a string.
		///
		/// @param string string to be Added
		///
		/// @return the index of that string in the table
		public int AddString(UnicodeString str)
		{
			field_1_num_strings++;
			UnicodeString unicodeString = (str == null) ? EMPTY_STRING : str;
			int index = field_3_strings.GetIndex(unicodeString);
			int result;
			if (index != -1)
			{
				result = index;
			}
			else
			{
				result = field_3_strings.Size;
				field_2_num_unique_strings++;
				SSTDeserializer.AddToStringTable(field_3_strings, unicodeString);
			}
			return result;
		}

		/// Get a particular string by its index
		///
		/// @param id index into the array of strings
		///
		/// @return the desired string
		public UnicodeString GetString(int id)
		{
			return field_3_strings[id];
		}

		public bool IsString16bit(int id)
		{
			UnicodeString unicodeString = field_3_strings[id];
			return (unicodeString.OptionFlags & 1) == 1;
		}

		/// Return a debugging string representation
		///
		/// @return string representation
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SST]\n");
			stringBuilder.Append("    .numstrings     = ").Append(StringUtil.ToHexString(NumStrings)).Append("\n");
			stringBuilder.Append("    .uniquestrings  = ").Append(StringUtil.ToHexString(NumUniqueStrings)).Append("\n");
			for (int i = 0; i < field_3_strings.Size; i++)
			{
				UnicodeString unicodeString = field_3_strings[i];
				stringBuilder.Append("    .string_" + i + "      = ").Append(unicodeString.GetDebugInfo()).Append("\n");
			}
			stringBuilder.Append("[/SST]\n");
			return stringBuilder.ToString();
		}

		/// @return hashcode
		public override int GetHashCode()
		{
			return field_2_num_unique_strings;
		}

		public override bool Equals(object o)
		{
			if (o == null || o.GetType() != GetType())
			{
				return false;
			}
			SSTRecord sSTRecord = (SSTRecord)o;
			if (field_1_num_strings == sSTRecord.field_1_num_strings && field_2_num_unique_strings == sSTRecord.field_2_num_unique_strings)
			{
				return field_3_strings.Equals(sSTRecord.field_3_strings);
			}
			return false;
		}

		/// @return an iterator of the strings we hold. All instances are
		///         UnicodeStrings
		public IEnumerator GetStrings()
		{
			return field_3_strings.GetEnumerator();
		}

		/// called by the class that Is responsible for writing this sucker.
		/// Subclasses should implement this so that their data Is passed back in a
		/// byte array.
		///
		/// @return size
		protected override void Serialize(ContinuableRecordOutput out1)
		{
			SSTSerializer sSTSerializer = new SSTSerializer(field_3_strings, NumStrings, NumUniqueStrings);
			sSTSerializer.Serialize(out1);
			bucketAbsoluteOffsets = sSTSerializer.BucketAbsoluteOffsets;
			bucketRelativeOffsets = sSTSerializer.BucketRelativeOffsets;
		}

		private SSTDeserializer GetDeserializer()
		{
			return deserializer;
		}

		/// Creates an extended string record based on the current contents of
		/// the current SST record.  The offset within the stream to the SST record
		/// Is required because the extended string record points directly to the
		/// strings in the SST record.
		///
		/// NOTE: THIS FUNCTION MUST ONLY BE CALLED AFTER THE SST RECORD HAS BEEN
		///       SERIALIZED.
		///
		/// @param sstOffset     The offset in the stream to the start of the
		///                      SST record.
		/// @return  The new SST record.
		public ExtSSTRecord CreateExtSSTRecord(int sstOffset)
		{
			if (bucketAbsoluteOffsets == null || bucketAbsoluteOffsets == null)
			{
				throw new InvalidOperationException("SST record has not yet been Serialized.");
			}
			ExtSSTRecord extSSTRecord = new ExtSSTRecord();
			extSSTRecord.NumStringsPerBucket = 8;
			int[] array = (int[])bucketAbsoluteOffsets.Clone();
			int[] array2 = (int[])bucketRelativeOffsets.Clone();
			for (int i = 0; i < array.Length; i++)
			{
				array[i] += sstOffset;
			}
			extSSTRecord.SetBucketOffsets(array, array2);
			return extSSTRecord;
		}

		/// Calculates the size in bytes of the EXTSST record as it would be if the
		/// record was Serialized.
		///
		/// @return  The size of the ExtSST record in bytes.
		public int CalcExtSSTRecordSize()
		{
			return ExtSSTRecord.GetRecordSizeForStrings(field_3_strings.Size);
		}
	}
}
