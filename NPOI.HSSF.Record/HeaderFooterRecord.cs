using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// The HEADERFOOTER record stores information Added in Office Excel 2007 for headers/footers.
	///
	/// @author Yegor Kozlov
	public class HeaderFooterRecord : StandardRecord
	{
		public const short sid = 2204;

		private static byte[] BLANK_GUID = new byte[16];

		private byte[] _rawData;

		protected override int DataSize => _rawData.Length;

		public override short Sid => 2204;

		/// If this header belongs to a specific sheet view , the sheet view?s GUID will be saved here.
		///
		/// If it is zero, it means the current sheet. Otherwise, this field MUST match the guid field
		/// of the preceding {@link UserSViewBegin} record.
		///
		/// @return the sheet view's GUID
		public byte[] Guid
		{
			get
			{
				byte[] array = new byte[16];
				Array.Copy(_rawData, 12, array, 0, array.Length);
				return array;
			}
		}

		/// @return whether this record belongs to the current sheet 
		public bool IsCurrentSheet => Arrays.Equals(Guid, BLANK_GUID);

		public HeaderFooterRecord(byte[] data)
		{
			_rawData = data;
		}

		/// construct a HeaderFooterRecord record.  No fields are interpreted and the record will
		/// be Serialized in its original form more or less
		/// @param in the RecordInputstream to read the record from
		public HeaderFooterRecord(RecordInputStream in1)
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
			stringBuilder.Append("[").Append("HEADERFOOTER").Append("] (0x");
			stringBuilder.Append(StringUtil.ToHexString((short)2204).ToUpper() + ")\n");
			stringBuilder.Append("  rawData=").Append(HexDump.ToHex(_rawData)).Append("\n");
			stringBuilder.Append("[/").Append("HEADERFOOTER").Append("]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			return CloneViaReserialise();
		}
	}
}
