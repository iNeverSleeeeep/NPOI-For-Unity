using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Formula Record.
	/// REFERENCE:  PG 317/444 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	[Serializable]
	public class FormulaRecord : CellRecord
	{
		public const short sid = 6;

		private const int FIXED_SIZE = 14;

		private double field_4_value;

		private short field_5_options;

		private BitField alwaysCalc = BitFieldFactory.GetInstance(1);

		private BitField calcOnLoad = BitFieldFactory.GetInstance(2);

		private BitField sharedFormula = BitFieldFactory.GetInstance(8);

		private int field_6_zero;

		[NonSerialized]
		private Formula field_8_parsed_expr;

		/// Since the NaN support seems sketchy (different constants) we'll store and spit it out directly
		[NonSerialized]
		private SpecialCachedValue specialCachedValue;

		/// @return <c>true</c> if this {@link FormulaRecord} is followed by a
		///  {@link StringRecord} representing the cached text result of the formula
		///  evaluation.
		public bool HasCachedResultString
		{
			get
			{
				if (specialCachedValue == null)
				{
					return false;
				}
				return specialCachedValue.GetTypeCode() == 0;
			}
		}

		/// Get the calculated value of the formula
		///
		/// @return calculated value
		public double Value
		{
			get
			{
				return field_4_value;
			}
			set
			{
				field_4_value = value;
				specialCachedValue = null;
			}
		}

		/// Get the option flags
		///
		/// @return bitmask
		public short Options
		{
			get
			{
				return field_5_options;
			}
			set
			{
				field_5_options = value;
			}
		}

		public bool IsSharedFormula
		{
			get
			{
				return sharedFormula.IsSet(field_5_options);
			}
			set
			{
				field_5_options = sharedFormula.SetShortBoolean(field_5_options, value);
			}
		}

		public bool IsAlwaysCalc
		{
			get
			{
				return alwaysCalc.IsSet(field_5_options);
			}
			set
			{
				field_5_options = alwaysCalc.SetShortBoolean(field_5_options, value);
			}
		}

		public bool IsCalcOnLoad
		{
			get
			{
				return calcOnLoad.IsSet(field_5_options);
			}
			set
			{
				field_5_options = calcOnLoad.SetShortBoolean(field_5_options, value);
			}
		}

		/// Get the stack as a list
		///
		/// @return list of tokens (casts stack to a list and returns it!)
		/// this method can return null Is we are Unable to Create Ptgs from 
		///     existing excel file
		/// callers should Check for null!
		public Ptg[] ParsedExpression
		{
			get
			{
				return field_8_parsed_expr.Tokens;
			}
			set
			{
				field_8_parsed_expr = Formula.Create(value);
			}
		}

		public Formula Formula => field_8_parsed_expr;

		protected override string RecordName => "FORMULA";

		protected override int ValueDataSize => 14 + field_8_parsed_expr.EncodedSize;

		public override short Sid => 6;

		public bool CachedBooleanValue => specialCachedValue.GetBooleanValue();

		public int CachedErrorValue => specialCachedValue.GetErrorValue();

		public CellType CachedResultType
		{
			get
			{
				if (specialCachedValue == null)
				{
					return CellType.Numeric;
				}
				return specialCachedValue.GetValueType();
			}
		}

		/// Creates new FormulaRecord 
		public FormulaRecord()
		{
			field_8_parsed_expr = Formula.Create(Ptg.EMPTY_PTG_ARRAY);
		}

		/// Constructs a Formula record and Sets its fields appropriately.
		/// Note - id must be 0x06 (NOT 0x406 see MSKB #Q184647 for an 
		/// "explanation of this bug in the documentation) or an exception
		///  will be throw upon validation
		///
		/// @param in the RecordInputstream to Read the record from
		public FormulaRecord(RecordInputStream in1)
			: base(in1)
		{
			long num = in1.ReadLong();
			field_5_options = in1.ReadShort();
			specialCachedValue = SpecialCachedValue.Create(num);
			if (specialCachedValue == null)
			{
				field_4_value = BitConverter.Int64BitsToDouble(num);
			}
			field_6_zero = in1.ReadInt();
			int encodedTokenLen = in1.ReadShort();
			field_8_parsed_expr = Formula.Read(encodedTokenLen, in1, in1.Available());
		}

		public void SetParsedExpression(Ptg[] ptgs)
		{
			field_8_parsed_expr = Formula.Create(ptgs);
		}

		public void SetSharedFormula(bool flag)
		{
			field_5_options = sharedFormula.SetShortBoolean(field_5_options, flag);
		}

		public void SetCachedResultTypeEmptyString()
		{
			specialCachedValue = SpecialCachedValue.CreateCachedEmptyValue();
		}

		public void SetCachedResultTypeString()
		{
			specialCachedValue = SpecialCachedValue.CreateForString();
		}

		public void SetCachedResultErrorCode(int errorCode)
		{
			specialCachedValue = SpecialCachedValue.CreateCachedErrorCode(errorCode);
		}

		public void SetCachedResultBoolean(bool value)
		{
			specialCachedValue = SpecialCachedValue.CreateCachedBoolean(value);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CellValueRecordInterface))
			{
				return false;
			}
			CellValueRecordInterface cellValueRecordInterface = (CellValueRecordInterface)obj;
			if (base.Row == cellValueRecordInterface.Row && base.Column == cellValueRecordInterface.Column)
			{
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.Row ^ base.Column;
		}

		protected override void SerializeValue(ILittleEndianOutput out1)
		{
			if (specialCachedValue == null)
			{
				out1.WriteDouble(field_4_value);
			}
			else
			{
				specialCachedValue.Serialize(out1);
			}
			out1.WriteShort(Options);
			out1.WriteInt(field_6_zero);
			field_8_parsed_expr.Serialize(out1);
		}

		protected override void AppendValueText(StringBuilder buffer)
		{
			buffer.Append("    .value           = ");
			if (specialCachedValue == null)
			{
				buffer.Append(field_4_value).Append("\n");
			}
			else
			{
				buffer.Append(specialCachedValue.FormatDebugString).Append("\n");
			}
			buffer.Append("    .options         = ").Append(Options).Append("\n");
			buffer.Append("      .alwaysCalc         = ").Append(alwaysCalc.IsSet(Options)).Append("\n");
			buffer.Append("      .CalcOnLoad         = ").Append(calcOnLoad.IsSet(Options)).Append("\n");
			buffer.Append("      .sharedFormula         = ").Append(sharedFormula.IsSet(Options)).Append("\n");
			buffer.Append("    .zero            = ").Append(field_6_zero).Append("\n");
			Ptg[] tokens = field_8_parsed_expr.Tokens;
			for (int i = 0; i < tokens.Length; i++)
			{
				buffer.Append("\t Ptg[").Append(i).Append("]=");
				Ptg ptg = tokens[i];
				buffer.Append(ptg.ToString()).Append(ptg.RVAType).Append("\n");
			}
		}

		public override object Clone()
		{
			FormulaRecord formulaRecord = new FormulaRecord();
			CopyBaseFields(formulaRecord);
			formulaRecord.field_4_value = field_4_value;
			formulaRecord.field_5_options = field_5_options;
			formulaRecord.field_6_zero = field_6_zero;
			formulaRecord.field_8_parsed_expr = field_8_parsed_expr.Copy();
			formulaRecord.specialCachedValue = specialCachedValue;
			return formulaRecord;
		}
	}
}
