using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// The begin record defines the start of a block of records for a (grpahing
	/// data object. This record is matched with a corresponding EndRecord.
	///
	/// @see EndRecord
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	public class BeginRecord : StandardRecord
	{
		public const short sid = 4147;

		public static BeginRecord instance = new BeginRecord();

		protected override int DataSize => 0;

		public override short Sid => 4147;

		public BeginRecord()
		{
		}

		/// Constructs a BeginRecord record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public BeginRecord(RecordInputStream in1)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[BEGIN]\n");
			stringBuilder.Append("[/BEGIN]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
		}

		public override object Clone()
		{
			return new BeginRecord();
		}
	}
}
