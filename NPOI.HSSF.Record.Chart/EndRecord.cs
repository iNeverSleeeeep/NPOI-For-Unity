using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// The end record defines the end of a block of records for a (Graphing)
	/// data object. This record is matched with a corresponding BeginRecord.
	///
	/// @see BeginRecord
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	public class EndRecord : StandardRecord
	{
		public const short sid = 4148;

		public static EndRecord instance = new EndRecord();

		protected override int DataSize => 0;

		public override short Sid => 4148;

		public EndRecord()
		{
		}

		/// Constructs a EndRecord record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public EndRecord(RecordInputStream in1)
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[END]\n");
			stringBuilder.Append("[/END]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
		}

		public override object Clone()
		{
			return new EndRecord();
		}
	}
}
