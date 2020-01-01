using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        File Pass Record
	/// Description:  Indicates that the record after this record are encrypted. HSSF does not support encrypted excel workbooks
	/// and the presence of this record will cause Processing to be aborted.
	/// REFERENCE:  PG 420 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 3.0-pre
	public class FilePassRecord : StandardRecord
	{
		public const short sid = 47;

		private const int ENCRYPTION_XOR = 0;

		private const int ENCRYPTION_OTHER = 1;

		private const int ENCRYPTION_OTHER_RC4 = 1;

		private const int ENCRYPTION_OTHER_CAPI_2 = 2;

		private const int ENCRYPTION_OTHER_CAPI_3 = 3;

		private int _encryptionType;

		private int _encryptionInfo;

		private int _minorVersionNo;

		private byte[] _docId;

		private byte[] _saltData;

		private byte[] _saltHash;

		protected override int DataSize => 54;

		public byte[] DocId
		{
			get
			{
				return _docId;
			}
			set
			{
			}
		}

		public byte[] SaltData => _saltData;

		public byte[] SaltHash => _saltHash;

		public override short Sid => 47;

		public FilePassRecord()
		{
		}

		public FilePassRecord(RecordInputStream in1)
		{
			_encryptionType = in1.ReadUShort();
			switch (_encryptionType)
			{
			case 0:
				throw new RecordFormatException("HSSF does not currently support XOR obfuscation");
			default:
				throw new RecordFormatException("Unknown encryption type " + _encryptionType);
			case 1:
				_encryptionInfo = in1.ReadUShort();
				switch (_encryptionInfo)
				{
				case 2:
				case 3:
					throw new RecordFormatException("HSSF does not currently support CryptoAPI encryption");
				default:
					throw new RecordFormatException("Unknown encryption info " + _encryptionInfo);
				case 1:
					_minorVersionNo = in1.ReadUShort();
					if (_minorVersionNo != 1)
					{
						throw new RecordFormatException("Unexpected VersionInfo number for RC4Header " + _minorVersionNo);
					}
					_docId = Read(in1, 16);
					_saltData = Read(in1, 16);
					_saltHash = Read(in1, 16);
					break;
				}
				break;
			}
		}

		private static byte[] Read(RecordInputStream in1, int size)
		{
			byte[] array = new byte[size];
			in1.ReadFully(array);
			return array;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FILEPASS]\n");
			stringBuilder.Append("    .type = ").Append(HexDump.ShortToHex(_encryptionType)).Append("\n");
			stringBuilder.Append("    .info = ").Append(HexDump.ShortToHex(_encryptionInfo)).Append("\n");
			stringBuilder.Append("    .ver  = ").Append(HexDump.ShortToHex(_minorVersionNo)).Append("\n");
			stringBuilder.Append("    .docId= ").Append(HexDump.ToHex(_docId)).Append("\n");
			stringBuilder.Append("    .salt = ").Append(HexDump.ToHex(_saltData)).Append("\n");
			stringBuilder.Append("    .hash = ").Append(HexDump.ToHex(_saltHash)).Append("\n");
			stringBuilder.Append("[/FILEPASS]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_encryptionType);
			out1.WriteShort(_encryptionInfo);
			out1.WriteShort(_minorVersionNo);
			out1.Write(_docId);
			out1.Write(_saltData);
			out1.Write(_saltHash);
		}

		public override object Clone()
		{
			return this;
		}
	}
}
