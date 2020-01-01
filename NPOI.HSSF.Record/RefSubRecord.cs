using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	public class RefSubRecord
	{
		public const int ENCODED_SIZE = 6;

		/// index to External Book Block (which starts with a EXTERNALBOOK record) 
		private int _extBookIndex;

		private int _firstSheetIndex;

		private int _lastSheetIndex;

		public int ExtBookIndex => _extBookIndex;

		public int FirstSheetIndex => _firstSheetIndex;

		public int LastSheetIndex => _lastSheetIndex;

		/// a Constructor for making new sub record
		public RefSubRecord(int extBookIndex, int firstSheetIndex, int lastSheetIndex)
		{
			_extBookIndex = extBookIndex;
			_firstSheetIndex = firstSheetIndex;
			_lastSheetIndex = lastSheetIndex;
		}

		/// @param in the RecordInputstream to Read the record from
		public RefSubRecord(RecordInputStream in1)
			: this(in1.ReadShort(), in1.ReadShort(), in1.ReadShort())
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("extBook=").Append(_extBookIndex);
			stringBuilder.Append(" firstSheet=").Append(_firstSheetIndex);
			stringBuilder.Append(" lastSheet=").Append(_lastSheetIndex);
			return stringBuilder.ToString();
		}

		/// called by the class that is responsible for writing this sucker.
		/// Subclasses should implement this so that their data is passed back in a
		/// byte array.
		///
		/// @param offset to begin writing at
		/// @param data byte array containing instance data
		/// @return number of bytes written
		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_extBookIndex);
			out1.WriteShort(_firstSheetIndex);
			out1.WriteShort(_lastSheetIndex);
		}
	}
}
