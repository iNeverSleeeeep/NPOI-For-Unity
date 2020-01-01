using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// ENDBLOCK - Chart Future Record Type End Block (0x0853)<br />
	///
	/// @author Patrick Cheng
	public class ChartEndBlockRecord : StandardRecord
	{
		public const short sid = 2131;

		private short rt;

		private short grbitFrt;

		private short iObjectKind;

		private byte[] unused;

		protected override int DataSize => 6 + unused.Length;

		public override short Sid => 2131;

		public ChartEndBlockRecord()
		{
		}

		public ChartEndBlockRecord(RecordInputStream in1)
		{
			rt = in1.ReadShort();
			grbitFrt = in1.ReadShort();
			iObjectKind = in1.ReadShort();
			if (in1.Available() == 0)
			{
				unused = new byte[0];
			}
			else
			{
				unused = new byte[6];
				in1.ReadFully(unused);
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rt);
			out1.WriteShort(grbitFrt);
			out1.WriteShort(iObjectKind);
			out1.Write(unused);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ENDBLOCK]\n");
			stringBuilder.Append("    .rt         =").Append(HexDump.ShortToHex(rt)).Append('\n');
			stringBuilder.Append("    .grbitFrt   =").Append(HexDump.ShortToHex(grbitFrt)).Append('\n');
			stringBuilder.Append("    .iObjectKind=").Append(HexDump.ShortToHex(iObjectKind)).Append('\n');
			stringBuilder.Append("    .unused     =").Append(HexDump.ToHex(unused)).Append('\n');
			stringBuilder.Append("[/ENDBLOCK]\n");
			return stringBuilder.ToString();
		}

		public ChartEndBlockRecord clone()
		{
			ChartEndBlockRecord chartEndBlockRecord = new ChartEndBlockRecord();
			chartEndBlockRecord.rt = rt;
			chartEndBlockRecord.grbitFrt = grbitFrt;
			chartEndBlockRecord.iObjectKind = iObjectKind;
			chartEndBlockRecord.unused = (byte[])unused.Clone();
			return chartEndBlockRecord;
		}
	}
}
