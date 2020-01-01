using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.PivotTable
{
	/// SXPI - Page Item (0x00B6)<br />
	///
	/// @author Patrick Cheng
	public class PageItemRecord : StandardRecord
	{
		private class FieldInfo
		{
			public const int ENCODED_SIZE = 6;

			/// Index to the View Item SXVI(0x00B2) record 
			private int _isxvi;

			/// Index to the {@link ViewFieldsRecord} SXVD(0x00B1) record 
			private int _isxvd;

			/// Object ID for the drop-down arrow 
			private int _idObj;

			public FieldInfo(RecordInputStream in1)
			{
				_isxvi = in1.ReadShort();
				_isxvd = in1.ReadShort();
				_idObj = in1.ReadShort();
			}

			internal void Serialize(ILittleEndianOutput out1)
			{
				out1.WriteShort(_isxvi);
				out1.WriteShort(_isxvd);
				out1.WriteShort(_idObj);
			}

			public void AppendDebugInfo(StringBuilder sb)
			{
				sb.Append('(');
				sb.Append("isxvi=").Append(HexDump.ShortToHex(_isxvi));
				sb.Append(" isxvd=").Append(HexDump.ShortToHex(_isxvd));
				sb.Append(" idObj=").Append(HexDump.ShortToHex(_idObj));
				sb.Append(')');
			}
		}

		public const short sid = 182;

		private FieldInfo[] _fieldInfos;

		protected override int DataSize => _fieldInfos.Length * 6;

		public override short Sid => 182;

		public PageItemRecord(RecordInputStream in1)
		{
			int remaining = in1.Remaining;
			if (remaining % 6 != 0)
			{
				throw new RecordFormatException("Bad data size " + remaining);
			}
			int num = remaining / 6;
			FieldInfo[] array = new FieldInfo[num];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new FieldInfo(in1);
			}
			_fieldInfos = array;
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			for (int i = 0; i < _fieldInfos.Length; i++)
			{
				_fieldInfos[i].Serialize(out1);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SXPI]\n");
			for (int i = 0; i < _fieldInfos.Length; i++)
			{
				stringBuilder.Append("    item[").Append(i).Append("]=");
				_fieldInfos[i].AppendDebugInfo(stringBuilder);
				stringBuilder.Append('\n');
			}
			stringBuilder.Append("[/SXPI]\n");
			return stringBuilder.ToString();
		}
	}
}
