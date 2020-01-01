using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	public class UnknownSubRecord : SubRecord
	{
		private int _sid;

		private byte[] _data;

		public override int DataSize => _data.Length;

		public override short Sid => (short)_sid;

		public UnknownSubRecord(ILittleEndianInput in1, int sid, int size)
		{
			_sid = sid;
			byte[] array = new byte[size];
			in1.ReadFully(array);
			_data = array;
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_sid);
			out1.WriteShort(_data.Length);
			out1.Write(_data);
		}

		public override object Clone()
		{
			return this;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append("sid=").Append(HexDump.ShortToHex(_sid));
			stringBuilder.Append(" size=").Append(_data.Length);
			stringBuilder.Append(" : ").Append(HexDump.ToHex(_data));
			stringBuilder.Append("]\n");
			return stringBuilder.ToString();
		}
	}
}
