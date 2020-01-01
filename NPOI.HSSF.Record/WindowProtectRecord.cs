using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Window Protect Record
	/// Description:  flags whether workbook windows are protected
	/// REFERENCE:  PG 424 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class WindowProtectRecord : StandardRecord
	{
		public const short sid = 25;

		private static BitField settingsProtectedFlag = BitFieldFactory.GetInstance(1);

		private int _options;

		/// Is this window protected or not
		///
		/// @return protected or not
		public bool Protect
		{
			get
			{
				return settingsProtectedFlag.IsSet(_options);
			}
			set
			{
				_options = settingsProtectedFlag.SetBoolean(_options, value);
			}
		}

		protected override int DataSize => 2;

		public override short Sid => 25;

		public WindowProtectRecord(int options)
		{
			_options = options;
		}

		/// Constructs a WindowProtect record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public WindowProtectRecord(RecordInputStream in1)
			: this(in1.ReadUShort())
		{
		}

		public WindowProtectRecord(bool protect)
			: this(0)
		{
			Protect = protect;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[WINDOWPROTECT]\n");
			stringBuilder.Append("    .protect         = ").Append(Protect).Append("\n");
			stringBuilder.Append("[/WINDOWPROTECT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_options);
		}
	}
}
