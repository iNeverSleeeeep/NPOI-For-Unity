using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// ENDBLOCK - Chart Future Record Type End Block (0x0853)<br />
	///
	/// @author Patrick Cheng
	public class EndBlockRecord : StandardRecord
	{
		public const short sid = 2131;

		private short rt;

		private short grbitFrt;

		private byte[] unused;

		public ObjectKind ObjectKind
		{
			get;
			set;
		}

		protected override int DataSize => 6 + unused.Length;

		public override short Sid => 2131;

		public EndBlockRecord()
		{
			rt = 2131;
			grbitFrt = 0;
		}

		public EndBlockRecord(RecordInputStream in1)
		{
			rt = in1.ReadShort();
			grbitFrt = in1.ReadShort();
			ObjectKind = (ObjectKind)in1.ReadShort();
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
			out1.WriteShort((int)ObjectKind);
			out1.Write(unused);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[ENDBLOCK]\n");
			stringBuilder.Append("    .rt         =").Append(HexDump.ShortToHex(rt)).Append('\n');
			stringBuilder.Append("    .grbitFrt   =").Append(HexDump.ShortToHex(grbitFrt)).Append('\n');
			stringBuilder.Append("    .iObjectKind=").Append(HexDump.ShortToHex((int)ObjectKind)).Append('\n');
			stringBuilder.Append("[/ENDBLOCK]\n");
			return stringBuilder.ToString();
		}

		public EndBlockRecord clone()
		{
			EndBlockRecord endBlockRecord = new EndBlockRecord();
			endBlockRecord.rt = rt;
			endBlockRecord.grbitFrt = grbitFrt;
			endBlockRecord.ObjectKind = ObjectKind;
			endBlockRecord.unused = (byte[])unused.Clone();
			return endBlockRecord;
		}

		public static EndBlockRecord CreateEndBlock(ObjectKind objectKind)
		{
			EndBlockRecord endBlockRecord = new EndBlockRecord();
			endBlockRecord.ObjectKind = objectKind;
			return endBlockRecord;
		}
	}
}
