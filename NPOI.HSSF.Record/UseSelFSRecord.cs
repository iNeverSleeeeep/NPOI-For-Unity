using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Use Natural Language Formulas Flag
	/// Description:  Tells the GUI if this was written by something that can use
	///               "natural language" formulas. HSSF can't.
	/// REFERENCE:  PG 420 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class UseSelFSRecord : StandardRecord
	{
		public const short sid = 352;

		private static BitField useNaturalLanguageFormulasFlag = BitFieldFactory.GetInstance(1);

		private int _options;

		protected override int DataSize => 2;

		public override short Sid => 352;

		public UseSelFSRecord(int options)
		{
			_options = options;
		}

		/// Constructs a UseSelFS record and Sets its fields appropriately.
		/// @param in the RecordInputstream to Read the record from
		public UseSelFSRecord(RecordInputStream in1)
			: this(in1.ReadUShort())
		{
		}

		public UseSelFSRecord(bool b)
			: this(0)
		{
			_options = useNaturalLanguageFormulasFlag.SetBoolean(_options, b);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[USESELFS]\n");
			stringBuilder.Append("    .flag            = ").Append(HexDump.ShortToHex(_options)).Append("\n");
			stringBuilder.Append("[/USESELFS]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(_options);
		}
	}
}
