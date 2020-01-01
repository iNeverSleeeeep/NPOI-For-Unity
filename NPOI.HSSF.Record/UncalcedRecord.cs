using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Uncalced Record
	///
	/// If this record occurs in the Worksheet Substream, it indicates that the formulas have not 
	/// been recalculated before the document was saved.
	///
	/// @author Olivier Leprince
	public class UncalcedRecord : StandardRecord
	{
		public const short sid = 94;

		private short _reserved;

		public override short Sid => 94;

		protected override int DataSize => 2;

		public static int StaticRecordSize => 6;

		/// Default constructor
		public UncalcedRecord()
		{
			_reserved = 0;
		}

		/// Read constructor
		public UncalcedRecord(RecordInputStream in1)
		{
			_reserved = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[UNCALCED]\n");
			stringBuilder.Append("[/UNCALCED]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_reserved);
		}
	}
}
