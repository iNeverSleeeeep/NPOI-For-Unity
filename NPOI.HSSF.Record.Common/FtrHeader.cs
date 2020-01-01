using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.Common
{
	/// Title: FtrHeader (Future Record Header) common record part
	///
	/// This record part specifies a header for a Ftr (Future)
	///  style record, which includes extra attributes above and
	///  beyond those of a traditional record. 
	public class FtrHeader
	{
		/// This MUST match the type on the Containing record 
		private short recordType;

		/// This is a FrtFlags 
		private short grbitFrt;

		/// MUST be 8 bytes and all zero 
		private byte[] reserved;

		public short RecordType
		{
			get
			{
				return recordType;
			}
			set
			{
				recordType = value;
			}
		}

		public short GrbitFrt
		{
			get
			{
				return grbitFrt;
			}
			set
			{
				grbitFrt = value;
			}
		}

		public byte[] GetReserved
		{
			get
			{
				return reserved;
			}
			set
			{
				reserved = value;
			}
		}

		public FtrHeader()
		{
			reserved = new byte[8];
		}

		public FtrHeader(RecordInputStream in1)
		{
			recordType = in1.ReadShort();
			grbitFrt = in1.ReadShort();
			reserved = new byte[8];
			in1.Read(reserved, 0, 8);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(" [FUTURE HEADER]\n");
			stringBuilder.Append("   type " + recordType);
			stringBuilder.Append("   flags " + grbitFrt);
			stringBuilder.Append(" [/FUTURE HEADER]\n");
			return stringBuilder.ToString();
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(recordType);
			out1.WriteShort(grbitFrt);
			out1.Write(reserved);
		}

		public static int GetDataSize()
		{
			return 12;
		}
	}
}
