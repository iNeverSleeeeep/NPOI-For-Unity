using NPOI.HSSF.Record.Common;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: FeatHdr (Feature Header) Record
	///
	/// This record specifies common information for Shared Features, and 
	///  specifies the beginning of a collection of records to define them. 
	/// The collection of data (Globals Substream ABNF, macro sheet substream 
	///  ABNF or worksheet substream ABNF) specifies Shared Feature data.
	public class FeatHdrRecord : StandardRecord
	{
		/// Specifies the enhanced protection type. Used to protect a 
		/// shared workbook by restricting access to some areas of it 
		public const int SHAREDFEATURES_ISFPROTECTION = 2;

		/// Specifies that formula errors should be ignored 
		public const int SHAREDFEATURES_ISFFEC2 = 3;

		/// Specifies the smart tag type. Recognises certain
		/// types of entries (proper names, dates/times etc) and
		/// flags them for action 
		public const int SHAREDFEATURES_ISFFACTOID = 4;

		/// Specifies the shared list type. Used for a table
		/// within a sheet
		public const int SHAREDFEATURES_ISFLIST = 5;

		public const short sid = 2151;

		private FtrHeader futureHeader;

		private int isf_sharedFeatureType;

		private byte reserved;

		/// 0x00000000 = rgbHdrData not present
		/// 0xffffffff = rgbHdrData present
		private long cbHdrData;

		/// We need a BOFRecord to make sense of this... 
		private byte[] rgbHdrData;

		public override short Sid => 2151;

		protected override int DataSize => 19 + rgbHdrData.Length;

		public FeatHdrRecord()
		{
			futureHeader = new FtrHeader();
			futureHeader.RecordType = 2151;
		}

		public FeatHdrRecord(RecordInputStream in1)
		{
			futureHeader = new FtrHeader(in1);
			isf_sharedFeatureType = in1.ReadShort();
			reserved = (byte)in1.ReadByte();
			cbHdrData = in1.ReadInt();
			rgbHdrData = in1.ReadRemainder();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FEATURE HEADER]\n");
			stringBuilder.Append("[/FEATURE HEADER]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			futureHeader.Serialize(out1);
			out1.WriteShort(isf_sharedFeatureType);
			out1.WriteByte(reserved);
			out1.WriteInt((int)cbHdrData);
			out1.Write(rgbHdrData);
		}

		public override object Clone()
		{
			return CloneViaReserialise();
		}
	}
}
