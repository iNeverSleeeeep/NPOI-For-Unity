using NPOI.Util;
using System.Collections;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Extern Sheet 
	/// Description:  A List of Inndexes to SupBook 
	/// REFERENCE:  
	/// @author Libin Roman (Vista Portal LDT. Developer)
	/// @version 1.0-pre
	public class ExternSheetRecord : StandardRecord
	{
		public const short sid = 23;

		private IList _list;

		/// returns the number of REF Records, which is in model
		/// @return number of REF records
		public int NumOfREFRecords => _list.Count;

		/// @return number of REF structures
		public int NumOfRefs => _list.Count;

		protected override int DataSize => 2 + _list.Count * 6;

		/// return the non static version of the id for this record.
		public override short Sid => 23;

		public ExternSheetRecord()
		{
			_list = new ArrayList();
		}

		/// Constructs a Extern Sheet record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public ExternSheetRecord(RecordInputStream in1)
		{
			_list = new ArrayList();
			int num = in1.ReadShort();
			for (int i = 0; i < num; i++)
			{
				RefSubRecord value = new RefSubRecord(in1);
				_list.Add(value);
			}
		}

		/// @return index of newly added ref
		public int AddRef(int extBookIndex, int firstSheetIndex, int lastSheetIndex)
		{
			_list.Add(new RefSubRecord(extBookIndex, firstSheetIndex, lastSheetIndex));
			return _list.Count - 1;
		}

		public int GetRefIxForSheet(int externalBookIndex, int sheetIndex)
		{
			int count = _list.Count;
			for (int i = 0; i < count; i++)
			{
				RefSubRecord @ref = GetRef(i);
				if (@ref.ExtBookIndex == externalBookIndex && @ref.FirstSheetIndex == sheetIndex && @ref.LastSheetIndex == sheetIndex)
				{
					return i;
				}
			}
			return -1;
		}

		/// Adds REF struct (ExternSheetSubRecord)
		/// @param rec REF struct
		public void AddREFRecord(RefSubRecord rec)
		{
			_list.Add(rec);
		}

		private RefSubRecord GetRef(int i)
		{
			return (RefSubRecord)_list[i];
		}

		public int GetExtbookIndexFromRefIndex(int refIndex)
		{
			return GetRef(refIndex).ExtBookIndex;
		}

		/// @return -1 if not found
		public int FindRefIndexFromExtBookIndex(int extBookIndex)
		{
			int count = _list.Count;
			for (int i = 0; i < count; i++)
			{
				if (GetRef(i).ExtBookIndex == extBookIndex)
				{
					return i;
				}
			}
			return -1;
		}

		public static ExternSheetRecord Combine(ExternSheetRecord[] esrs)
		{
			ExternSheetRecord externSheetRecord = new ExternSheetRecord();
			foreach (ExternSheetRecord externSheetRecord2 in esrs)
			{
				int numOfREFRecords = externSheetRecord2.NumOfREFRecords;
				for (int j = 0; j < numOfREFRecords; j++)
				{
					externSheetRecord.AddREFRecord(externSheetRecord2.GetRef(j));
				}
			}
			return externSheetRecord;
		}

		public int GetFirstSheetIndexFromRefIndex(int extRefIndex)
		{
			return GetRef(extRefIndex).FirstSheetIndex;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = _list.Count;
			stringBuilder.Append("[EXTERNSHEET]\n");
			stringBuilder.Append("   numOfRefs     = ").Append(count).Append("\n");
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append("refrec         #").Append(i).Append(": ");
				stringBuilder.Append(GetRef(i).ToString());
				stringBuilder.Append('\n');
			}
			stringBuilder.Append("[/EXTERNSHEET]\n");
			return stringBuilder.ToString();
		}

		/// called by the class that Is responsible for writing this sucker.
		/// Subclasses should implement this so that their data Is passed back in a
		/// byte array.
		///
		/// @param offset to begin writing at
		/// @param data byte array containing instance data
		/// @return number of bytes written
		public override void Serialize(ILittleEndianOutput out1)
		{
			int count = _list.Count;
			out1.WriteShort(count);
			for (int i = 0; i < count; i++)
			{
				GetRef(i).Serialize(out1);
			}
		}
	}
}
