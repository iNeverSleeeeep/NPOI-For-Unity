using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// ARRAY (0x0221)<p />
	///
	/// Treated in a similar way to SharedFormulaRecord
	///
	/// @author Josh Micich
	public class ArrayRecord : SharedValueRecordBase
	{
		public const short sid = 545;

		private const int OPT_ALWAYS_RECALCULATE = 1;

		private const int OPT_CALCULATE_ON_OPEN = 2;

		private int _options;

		private int _field3notUsed;

		private Formula _formula;

		public bool IsAlwaysRecalculate => (_options & 1) != 0;

		public bool IsCalculateOnOpen => (_options & 2) != 0;

		public Ptg[] FormulaTokens => _formula.Tokens;

		protected override int ExtraDataSize => 6 + _formula.EncodedSize;

		public override short Sid => 545;

		public ArrayRecord(RecordInputStream in1)
			: base(in1)
		{
			_options = in1.ReadUShort();
			_field3notUsed = in1.ReadInt();
			int encodedTokenLen = in1.ReadUShort();
			int totalEncodedLen = in1.Available();
			_formula = Formula.Read(encodedTokenLen, in1, totalEncodedLen);
		}

		public ArrayRecord(Formula formula, CellRangeAddress8Bit range)
			: base(range)
		{
			_options = 0;
			_field3notUsed = 0;
			_formula = formula;
		}

		protected override void SerializeExtraData(ILittleEndianOutput out1)
		{
			out1.WriteShort(_options);
			out1.WriteInt(_field3notUsed);
			_formula.Serialize(out1);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(GetType().Name).Append(" [ARRAY]\n");
			stringBuilder.Append(" range=").Append(base.Range.ToString()).Append("\n");
			stringBuilder.Append(" options=").Append(HexDump.ShortToHex(_options)).Append("\n");
			stringBuilder.Append(" notUsed=").Append(HexDump.IntToHex(_field3notUsed)).Append("\n");
			stringBuilder.Append(" formula:").Append("\n");
			Ptg[] tokens = _formula.Tokens;
			foreach (Ptg ptg in tokens)
			{
				stringBuilder.Append(ptg.ToString()).Append(ptg.RVAType).Append("\n");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
