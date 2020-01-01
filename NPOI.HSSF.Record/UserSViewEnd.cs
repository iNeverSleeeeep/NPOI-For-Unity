using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// The UserSViewEnd record marks the end of the Settings for a custom view associated with the sheet
	///
	/// @author Yegor Kozlov
	public class UserSViewEnd : StandardRecord
	{
		public const short sid = 427;

		private byte[] _rawData;

		protected override int DataSize => _rawData.Length;

		public override short Sid => 427;

		public UserSViewEnd(byte[] data)
		{
			_rawData = data;
		}

		/// construct an UserSViewEnd record.  No fields are interpreted and the record will
		/// be Serialized in its original form more or less
		/// @param in the RecordInputstream to read the record from
		public UserSViewEnd(RecordInputStream in1)
		{
			_rawData = in1.ReadRemainder();
		}

		/// spit the record out AS IS. no interpretation or identification
		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.Write(_rawData);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[").Append("USERSVIEWEND").Append("] (0x");
			stringBuilder.Append(StringUtil.ToHexString((short)427).ToUpper() + ")\n");
			stringBuilder.Append("  rawData=").Append(HexDump.ToHex(_rawData)).Append("\n");
			stringBuilder.Append("[/").Append("USERSVIEWEND").Append("]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			return CloneViaReserialise();
		}
	}
}
