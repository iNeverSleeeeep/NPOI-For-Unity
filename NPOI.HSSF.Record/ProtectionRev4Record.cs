using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Protection Revision 4 Record
	/// Description:  describes whether this is a protected shared/tracked workbook
	///  ( HSSF does not support encryption because we don't feel like going to jail ) 
	/// REFERENCE:  PG 373 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class ProtectionRev4Record : StandardRecord
	{
		public const short sid = 431;

		private static BitField protectedFlag = BitFieldFactory.GetInstance(1);

		private short _options;

		/// Get whether the this is protected shared/tracked workbook or not
		/// @return whether to protect the workbook or not
		public bool Protect
		{
			get
			{
				return protectedFlag.IsSet(_options);
			}
			set
			{
				_options = (short)protectedFlag.SetBoolean(_options, value);
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 431;

		public ProtectionRev4Record(short options)
		{
			_options = options;
		}

		public ProtectionRev4Record(bool protect)
			: this(0)
		{
			Protect = protect;
		}

		/// Constructs a ProtectionRev4 record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public ProtectionRev4Record(RecordInputStream in1)
			: this(in1.ReadShort())
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[PROT4REV]\n");
			stringBuilder.Append("    .protect         = ").Append(Protect).Append("\n");
			stringBuilder.Append("[/PROT4REV]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_options);
		}
	}
}
