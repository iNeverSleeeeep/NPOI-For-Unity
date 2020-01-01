using NPOI.SS;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        SharedFormulaRecord
	/// Description:  Primarily used as an excel optimization so that multiple similar formulas
	/// 				  are not written out too many times.  We should recognize this record and
	///               Serialize as Is since this Is used when Reading templates.
	///
	/// Note: the documentation says that the SID Is BC where biffviewer reports 4BC.  The hex dump shows
	/// that the two byte sid representation to be 'BC 04' that Is consistent with the other high byte
	/// record types.
	/// @author Danny Mui at apache dot org
	public class SharedFormulaRecord : SharedValueRecordBase
	{
		public const short sid = 1212;

		private int field_5_reserved;

		private Formula field_7_parsed_expr;

		protected override int ExtraDataSize => 2 + field_7_parsed_expr.EncodedSize;

		public override short Sid => 1212;

		public SharedFormulaRecord()
			: this(new CellRangeAddress8Bit(0, 0, 0, 0))
		{
		}

		private SharedFormulaRecord(CellRangeAddress8Bit range)
			: base(range)
		{
			field_7_parsed_expr = Formula.Create(Ptg.EMPTY_PTG_ARRAY);
		}

		/// @param in the RecordInputstream to Read the record from
		public SharedFormulaRecord(RecordInputStream in1)
			: base(in1)
		{
			field_5_reserved = in1.ReadShort();
			int encodedTokenLen = in1.ReadShort();
			int totalEncodedLen = in1.Available();
			field_7_parsed_expr = Formula.Read(encodedTokenLen, in1, totalEncodedLen);
		}

		/// print a sort of string representation ([SHARED FORMULA RECORD] id = x [/SHARED FORMULA RECORD])
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SHARED FORMULA (").Append(HexDump.IntToHex(1212)).Append("]\n");
			stringBuilder.Append("    .range      = ").Append(base.Range.ToString()).Append("\n");
			stringBuilder.Append("    .reserved    = ").Append(HexDump.ShortToHex(field_5_reserved)).Append("\n");
			Ptg[] tokens = field_7_parsed_expr.Tokens;
			for (int i = 0; i < tokens.Length; i++)
			{
				stringBuilder.Append("Formula[").Append(i).Append("]");
				Ptg ptg = tokens[i];
				stringBuilder.Append(ptg.ToString()).Append(ptg.RVAType).Append("\n");
			}
			stringBuilder.Append("[/SHARED FORMULA]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			SharedFormulaRecord sharedFormulaRecord = new SharedFormulaRecord(base.Range);
			sharedFormulaRecord.field_5_reserved = field_5_reserved;
			sharedFormulaRecord.field_7_parsed_expr = field_7_parsed_expr.Copy();
			return sharedFormulaRecord;
		}

		protected override void SerializeExtraData(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_5_reserved);
			field_7_parsed_expr.Serialize(out1);
		}

		/// @return the equivalent {@link Ptg} array that the formula would have, were it not shared.
		public Ptg[] GetFormulaTokens(FormulaRecord formula)
		{
			int row = formula.Row;
			int column = formula.Column;
			if (!IsInRange(row, column))
			{
				throw new Exception("Shared Formula Conversion: Coding Error");
			}
			SharedFormula sharedFormula = new SharedFormula(SpreadsheetVersion.EXCEL97);
			return sharedFormula.ConvertSharedFormulas(field_7_parsed_expr.Tokens, row, column);
		}

		public bool IsFormulaSame(SharedFormulaRecord other)
		{
			return field_7_parsed_expr.IsSame(other.field_7_parsed_expr);
		}
	}
}
