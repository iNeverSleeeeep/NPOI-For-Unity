using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Protect Record
	/// Description:  defines whether a sheet or workbook is protected (HSSF DOES NOT SUPPORT ENCRYPTION)
	/// (kindly ask the US government to stop having arcane stupid encryption laws and we'll support it) 
	/// (after all terrorists will all use US-legal encrypton right??)
	/// HSSF now supports the simple "protected" sheets (where they are not encrypted and open office et al
	/// ignore the password record entirely).
	/// REFERENCE:  PG 373 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	public class ProtectRecord : StandardRecord
	{
		public const short sid = 18;

		private static BitField protectFlag = BitFieldFactory.GetInstance(1);

		private short _options;

		/// Get whether the sheet is protected or not
		/// @return whether to protect the sheet or not
		public bool Protect
		{
			get
			{
				return protectFlag.IsSet(_options);
			}
			set
			{
				_options = (short)protectFlag.SetBoolean(_options, value);
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 18;

		public ProtectRecord(short options)
		{
			_options = options;
		}

		/// Constructs a Protect record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public ProtectRecord(RecordInputStream in1)
			: this(in1.ReadShort())
		{
		}

		public ProtectRecord(bool isProtected)
			: this(0)
		{
			Protect = isProtected;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PROTECT]\n");
			stringBuilder.Append("    .options = ").Append(HexDump.ShortToHex(_options)).Append("\n");
			stringBuilder.Append("[/PROTECT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_options);
		}

		public override object Clone()
		{
			return new ProtectRecord(_options);
		}
	}
}
