using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: double Stream Flag Record
	/// Description:  tells if this Is a double stream file. (always no for HSSF generated files)
	///               double Stream files contain both BIFF8 and BIFF7 workbooks.
	/// REFERENCE:  PG 305 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class DSFRecord : StandardRecord
	{
		public const short sid = 353;

		private int _options;

		private static BitField biff5BookStreamFlag = BitFieldFactory.GetInstance(1);

		public bool IsBiff5BookStreamPresent => biff5BookStreamFlag.IsSet(_options);

		protected override int DataSize => 2;

		public override short Sid => 353;

		private DSFRecord(int options)
		{
			_options = options;
		}

		public DSFRecord(bool isBiff5BookStreamPresent)
			: this(0)
		{
			_options = biff5BookStreamFlag.SetBoolean(0, isBiff5BookStreamPresent);
		}

		/// Constructs a DBCellRecord and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public DSFRecord(RecordInputStream in1)
			: this(in1.ReadShort())
		{
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[DSF]\n");
			stringBuilder.Append("    .IsDSF           = ").Append(StringUtil.ToHexString(_options)).Append("\n");
			stringBuilder.Append("[/DSF]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_options);
		}
	}
}
