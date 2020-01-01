using NPOI.HSSF.Model;
using NPOI.HSSF.Record.CF;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Conditional Formatting Rule Record.
	/// @author Dmitriy Kumshayev
	public class CFRuleRecord : StandardRecord
	{
		public const short sid = 433;

		public const byte CONDITION_TYPE_CELL_VALUE_IS = 1;

		public const byte CONDITION_TYPE_FORMULA = 2;

		private byte field_1_condition_type;

		private byte field_2_comparison_operator;

		private int field_5_options;

		private static BitField modificationBits = bf(4194303);

		private static BitField alignHor = bf(1);

		private static BitField alignVer = bf(2);

		private static BitField alignWrap = bf(4);

		private static BitField alignRot = bf(8);

		private static BitField alignJustLast = bf(16);

		private static BitField alignIndent = bf(32);

		private static BitField alignShrin = bf(64);

		private static BitField notUsed1 = bf(128);

		private static BitField protLocked = bf(256);

		private static BitField protHidden = bf(512);

		private static BitField bordLeft = bf(1024);

		private static BitField bordRight = bf(2048);

		private static BitField bordTop = bf(4096);

		private static BitField bordBot = bf(8192);

		private static BitField bordTlBr = bf(16384);

		private static BitField bordBlTr = bf(32768);

		private static BitField pattStyle = bf(65536);

		private static BitField pattCol = bf(131072);

		private static BitField pattBgCol = bf(262144);

		private static BitField notUsed2 = bf(3670016);

		private static BitField Undocumented = bf(62914560);

		private static BitField fmtBlockBits = bf(2080374784);

		private static BitField font = bf(67108864);

		private static BitField align = bf(134217728);

		private static BitField bord = bf(268435456);

		private static BitField patt = bf(536870912);

		private static BitField prot = bf(1073741824);

		private static BitField alignTextDir = bf(-2147483648);

		private short field_6_not_used;

		private FontFormatting fontFormatting;

		private BorderFormatting borderFormatting;

		private PatternFormatting patternFormatting;

		private Formula field_17_formula1;

		private Formula field_18_formula2;

		/// get the stack of the 1st expression as a list
		///
		/// @return list of tokens (casts stack to a list and returns it!)
		/// this method can return null is we are unable to create Ptgs from 
		///             	 existing excel file
		/// callers should check for null!
		public Ptg[] ParsedExpression1
		{
			get
			{
				return field_17_formula1.Tokens;
			}
			set
			{
				field_17_formula1 = Formula.Create(value);
			}
		}

		/// get the stack of the 2nd expression as a list
		///
		/// @return list of tokens (casts stack to a list and returns it!)
		/// this method can return null is we are unable to create Ptgs from 
		///             	 existing excel file
		/// callers should check for null!
		public Ptg[] ParsedExpression2
		{
			get
			{
				return field_18_formula2.Tokens;
			}
			set
			{
				field_18_formula2 = Formula.Create(value);
			}
		}

		public byte ConditionType => field_1_condition_type;

		public bool ContainsFontFormattingBlock => GetOptionFlag(font);

		public FontFormatting FontFormatting
		{
			get
			{
				if (ContainsFontFormattingBlock)
				{
					return fontFormatting;
				}
				return null;
			}
			set
			{
				fontFormatting = value;
				SetOptionFlag(fontFormatting != null, font);
			}
		}

		public bool ContainsAlignFormattingBlock => GetOptionFlag(align);

		public bool ContainsBorderFormattingBlock => GetOptionFlag(bord);

		public BorderFormatting BorderFormatting
		{
			get
			{
				if (ContainsBorderFormattingBlock)
				{
					return borderFormatting;
				}
				return null;
			}
			set
			{
				borderFormatting = value;
				SetOptionFlag(borderFormatting != null, bord);
			}
		}

		public bool ContainsPatternFormattingBlock => GetOptionFlag(patt);

		public PatternFormatting PatternFormatting
		{
			get
			{
				if (ContainsPatternFormattingBlock)
				{
					return patternFormatting;
				}
				return null;
			}
			set
			{
				patternFormatting = value;
				SetOptionFlag(patternFormatting != null, patt);
			}
		}

		public bool ContainsProtectionFormattingBlock => GetOptionFlag(prot);

		public byte ComparisonOperation
		{
			get
			{
				return field_2_comparison_operator;
			}
			set
			{
				field_2_comparison_operator = value;
			}
		}

		/// Get the option flags
		///
		/// @return bit mask
		public int Options => field_5_options;

		public bool IsLeftBorderModified
		{
			get
			{
				return IsModified(bordLeft);
			}
			set
			{
				SetModified(value, bordLeft);
			}
		}

		public bool IsRightBorderModified
		{
			get
			{
				return IsModified(bordRight);
			}
			set
			{
				SetModified(value, bordRight);
			}
		}

		public bool IsTopBorderModified
		{
			get
			{
				return IsModified(bordTop);
			}
			set
			{
				SetModified(value, bordTop);
			}
		}

		public bool IsBottomBorderModified
		{
			get
			{
				return IsModified(bordBot);
			}
			set
			{
				SetModified(value, bordBot);
			}
		}

		public bool IsTopLeftBottomRightBorderModified
		{
			get
			{
				return IsModified(bordTlBr);
			}
			set
			{
				SetModified(value, bordTlBr);
			}
		}

		public bool IsBottomLeftTopRightBorderModified
		{
			get
			{
				return IsModified(bordBlTr);
			}
			set
			{
				SetModified(value, bordBlTr);
			}
		}

		public bool IsPatternStyleModified
		{
			get
			{
				return IsModified(pattStyle);
			}
			set
			{
				SetModified(value, pattStyle);
			}
		}

		public bool IsPatternColorModified
		{
			get
			{
				return IsModified(pattCol);
			}
			set
			{
				SetModified(value, pattCol);
			}
		}

		public bool IsPatternBackgroundColorModified
		{
			get
			{
				return IsModified(pattBgCol);
			}
			set
			{
				SetModified(value, pattBgCol);
			}
		}

		public override short Sid => 433;

		protected override int DataSize => 12 + (ContainsFontFormattingBlock ? fontFormatting.GetRawRecord().Length : 0) + (ContainsBorderFormattingBlock ? 8 : 0) + (ContainsPatternFormattingBlock ? 4 : 0) + GetFormulaSize(field_17_formula1) + GetFormulaSize(field_18_formula2);

		private static BitField bf(int i)
		{
			return BitFieldFactory.GetInstance(i);
		}

		/// Creates new CFRuleRecord 
		private CFRuleRecord(byte conditionType, ComparisonOperator comparisonOperation)
		{
			field_1_condition_type = conditionType;
			field_2_comparison_operator = (byte)comparisonOperation;
			field_5_options = modificationBits.SetValue(field_5_options, -1);
			field_5_options = fmtBlockBits.SetValue(field_5_options, 0);
			field_5_options = Undocumented.Clear(field_5_options);
			field_6_not_used = -32766;
			fontFormatting = null;
			borderFormatting = null;
			patternFormatting = null;
			field_17_formula1 = Formula.Create(Ptg.EMPTY_PTG_ARRAY);
			field_18_formula2 = Formula.Create(Ptg.EMPTY_PTG_ARRAY);
		}

		private CFRuleRecord(byte conditionType, ComparisonOperator comparisonOperation, Ptg[] formula1, Ptg[] formula2)
			: this(conditionType, comparisonOperation)
		{
			field_17_formula1 = Formula.Create(formula1);
			field_18_formula2 = Formula.Create(formula2);
		}

		/// Creates a new comparison operation rule
		[Obsolete]
		public static CFRuleRecord Create(HSSFWorkbook workbook, string formulaText)
		{
			Ptg[] formula = ParseFormula(formulaText, workbook);
			return new CFRuleRecord(2, ComparisonOperator.NoComparison, formula, null);
		}

		/// Creates a new comparison operation rule
		[Obsolete]
		public static CFRuleRecord Create(HSSFWorkbook workbook, ComparisonOperator comparisonOperation, string formulaText1, string formulaText2)
		{
			Ptg[] formula = ParseFormula(formulaText1, workbook);
			Ptg[] formula2 = ParseFormula(formulaText2, workbook);
			return new CFRuleRecord(1, comparisonOperation, formula, formula2);
		}

		public static CFRuleRecord Create(HSSFSheet sheet, string formulaText)
		{
			Ptg[] formula = ParseFormula(formulaText, sheet);
			return new CFRuleRecord(2, ComparisonOperator.NoComparison, formula, null);
		}

		/// Creates a new comparison operation rule
		public static CFRuleRecord Create(HSSFSheet sheet, byte comparisonOperation, string formulaText1, string formulaText2)
		{
			Ptg[] formula = ParseFormula(formulaText1, sheet);
			Ptg[] formula2 = ParseFormula(formulaText2, sheet);
			return new CFRuleRecord(1, (ComparisonOperator)comparisonOperation, formula, formula2);
		}

		public CFRuleRecord(RecordInputStream in1)
		{
			field_1_condition_type = (byte)in1.ReadByte();
			field_2_comparison_operator = (byte)in1.ReadByte();
			int encodedTokenLen = in1.ReadUShort();
			int encodedTokenLen2 = in1.ReadUShort();
			field_5_options = in1.ReadInt();
			field_6_not_used = in1.ReadShort();
			if (ContainsFontFormattingBlock)
			{
				fontFormatting = new FontFormatting(in1);
			}
			if (ContainsBorderFormattingBlock)
			{
				borderFormatting = new BorderFormatting(in1);
			}
			if (ContainsPatternFormattingBlock)
			{
				patternFormatting = new PatternFormatting(in1);
			}
			field_17_formula1 = Formula.Read(encodedTokenLen, in1);
			field_18_formula2 = Formula.Read(encodedTokenLen2, in1);
		}

		public void SetAlignFormattingUnChanged()
		{
			SetOptionFlag(flag: false, align);
		}

		public void SetProtectionFormattingUnChanged()
		{
			SetOptionFlag(flag: false, prot);
		}

		private bool IsModified(BitField field)
		{
			return !field.IsSet(field_5_options);
		}

		private void SetModified(bool modified, BitField field)
		{
			field_5_options = field.SetBoolean(field_5_options, !modified);
		}

		private bool GetOptionFlag(BitField field)
		{
			return field.IsSet(field_5_options);
		}

		private void SetOptionFlag(bool flag, BitField field)
		{
			field_5_options = field.SetBoolean(field_5_options, flag);
		}

		/// @param ptgs may be <c>null</c>
		/// @return encoded size of the formula
		private int GetFormulaSize(Formula formula)
		{
			return formula.EncodedTokenSize;
		}

		/// called by the class that Is responsible for writing this sucker.
		/// Subclasses should implement this so that their data Is passed back in a
		/// byte array.
		///
		/// @param offset to begin writing at
		/// @param data byte array containing instance data
		/// @return number of bytes written
		public override void Serialize(ILittleEndianOutput out1)
		{
			int formulaSize = GetFormulaSize(field_17_formula1);
			int formulaSize2 = GetFormulaSize(field_18_formula2);
			out1.WriteByte(field_1_condition_type);
			out1.WriteByte(field_2_comparison_operator);
			out1.WriteShort(formulaSize);
			out1.WriteShort(formulaSize2);
			out1.WriteInt(field_5_options);
			out1.WriteShort(field_6_not_used);
			if (ContainsFontFormattingBlock)
			{
				byte[] rawRecord = fontFormatting.GetRawRecord();
				out1.Write(rawRecord);
			}
			if (ContainsBorderFormattingBlock)
			{
				borderFormatting.Serialize(out1);
			}
			if (ContainsPatternFormattingBlock)
			{
				patternFormatting.Serialize(out1);
			}
			field_17_formula1.SerializeTokens(out1);
			field_18_formula2.SerializeTokens(out1);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CFRULE]\n");
			stringBuilder.Append("    OPTION FLAGS=0x" + StringUtil.ToHexString(Options));
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			CFRuleRecord cFRuleRecord = new CFRuleRecord(field_1_condition_type, (ComparisonOperator)field_2_comparison_operator);
			cFRuleRecord.field_5_options = field_5_options;
			cFRuleRecord.field_6_not_used = field_6_not_used;
			if (ContainsFontFormattingBlock)
			{
				cFRuleRecord.fontFormatting = (FontFormatting)fontFormatting.Clone();
			}
			if (ContainsBorderFormattingBlock)
			{
				cFRuleRecord.borderFormatting = (BorderFormatting)borderFormatting.Clone();
			}
			if (ContainsPatternFormattingBlock)
			{
				cFRuleRecord.patternFormatting = (PatternFormatting)patternFormatting.Clone();
			}
			if (field_17_formula1 != null)
			{
				cFRuleRecord.field_17_formula1 = field_17_formula1.Copy();
			}
			if (field_18_formula2 != null)
			{
				cFRuleRecord.field_18_formula2 = field_18_formula2.Copy();
			}
			return cFRuleRecord;
		}

		/// TODO - Parse conditional format formulas properly i.e. produce tRefN and tAreaN instead of tRef and tArea
		/// this call will produce the wrong results if the formula Contains any cell references
		/// One approach might be to apply the inverse of SharedFormulaRecord.ConvertSharedFormulas(Stack, int, int)
		/// Note - two extra parameters (rowIx &amp;colIx) will be required. They probably come from one of the Region objects.
		///
		/// @return <c>null</c> if <c>formula</c> was null.
		private static Ptg[] ParseFormula(string formula, HSSFWorkbook workbook)
		{
			if (formula == null)
			{
				return null;
			}
			return HSSFFormulaParser.Parse(formula, workbook);
		}

		/// TODO - parse conditional format formulas properly i.e. produce tRefN and tAreaN instead of tRef and tArea
		/// this call will produce the wrong results if the formula contains any cell references
		/// One approach might be to apply the inverse of SharedFormulaRecord.convertSharedFormulas(Stack, int, int)
		/// Note - two extra parameters (rowIx &amp; colIx) will be required. They probably come from one of the Region objects.
		///
		/// @return <code>null</code> if <c>formula</c> was null.
		private static Ptg[] ParseFormula(string formula, HSSFSheet sheet)
		{
			if (formula == null)
			{
				return null;
			}
			int sheetIndex = sheet.Workbook.GetSheetIndex(sheet);
			return HSSFFormulaParser.Parse(formula, (HSSFWorkbook)sheet.Workbook, FormulaType.Cell, sheetIndex);
		}
	}
}
