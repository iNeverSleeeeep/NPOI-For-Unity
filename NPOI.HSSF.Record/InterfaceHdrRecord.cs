using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Interface Header Record
	/// Description: Defines the beginning of Interface records (MMS)
	/// REFERENCE:  PG 324 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class InterfaceHdrRecord : StandardRecord
	{
		public const short sid = 225;

		/// suggested (and probably correct) default
		public const short CODEPAGE = 1200;

		private int _codepage;

		protected override int DataSize => 2;

		public override short Sid => 225;

		public InterfaceHdrRecord(int codePage)
		{
			_codepage = codePage;
		}

		/// Constructs an Codepage record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public InterfaceHdrRecord(RecordInputStream in1)
		{
			_codepage = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[INTERFACEHDR]\n");
			stringBuilder.Append("    .codepage        = ").Append(StringUtil.ToHexString(_codepage)).Append("\n");
			stringBuilder.Append("[/INTERFACEHDR]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_codepage);
		}
	}
}
