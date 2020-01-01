using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// The UserSViewBegin record specifies Settings for a custom view associated with the sheet.
	/// This record also marks the start of custom view records, which save custom view Settings.
	/// Records between {@link UserSViewBegin} and {@link UserSViewEnd} contain Settings for the custom view,
	/// not Settings for the sheet itself.
	///
	/// @author Yegor Kozlov
	public class UserSViewBegin : StandardRecord
	{
		public const short sid = 426;

		private byte[] _rawData;

		protected override int DataSize => _rawData.Length;

		public override short Sid => 426;

		/// @return Globally unique identifier for the custom view
		public byte[] Guid
		{
			get
			{
				byte[] array = new byte[16];
				Array.Copy(_rawData, 0, array, 0, array.Length);
				return array;
			}
		}

		public UserSViewBegin(byte[] data)
		{
			_rawData = data;
		}

		/// construct an UserSViewBegin record.  No fields are interpreted and the record will
		/// be Serialized in its original form more or less
		/// @param in the RecordInputstream to read the record from
		public UserSViewBegin(RecordInputStream in1)
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
			stringBuilder.Append("[").Append("USERSVIEWBEGIN").Append("] (0x");
			stringBuilder.Append(StringUtil.ToHexString((short)426).ToUpper() + ")\n");
			stringBuilder.Append("  rawData=").Append(HexDump.ToHex(_rawData)).Append("\n");
			stringBuilder.Append("[/").Append("USERSVIEWBEGIN").Append("]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			return CloneViaReserialise();
		}
	}
}
