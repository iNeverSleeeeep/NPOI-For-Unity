using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.Chart
{
	/// <summary>
	/// The BRAI record specifies a reference to data in a sheet (1) that is used by a part of a series, 
	/// legend entry, trendline or error bars.
	/// </summary>
	public class BRAIRecord : StandardRecord
	{
		public const short sid = 4177;

		public const byte LINK_TYPE_TITLE_OR_TEXT = 0;

		public const byte LINK_TYPE_VALUES = 1;

		public const byte LINK_TYPE_CATEGORIES = 2;

		public const byte LINK_TYPE_BUBBLESIZE_VALUE = 3;

		public const byte REFERENCE_TYPE_DEFAULT_CATEGORIES = 0;

		public const byte REFERENCE_TYPE_DIRECT = 1;

		public const byte REFERENCE_TYPE_WORKSHEET = 2;

		public const byte REFERENCE_TYPE_NOT_USED = 3;

		public const byte REFERENCE_TYPE_ERROR_REPORTED = 4;

		private BitField customNumberFormat = BitFieldFactory.GetInstance(1);

		private byte field_1_linkType;

		private byte field_2_referenceType;

		private short field_3_options;

		private short field_4_indexNumberFmtRecord;

		/// <summary>
		/// A ChartParsedFormula structure that specifies the formula (section 2.2.2) that specifies the reference.
		/// </summary>
		private Formula field_5_formulaOfLink;

		/// Size of record (exluding 4 byte header)
		protected override int DataSize => 6 + field_5_formulaOfLink.EncodedSize;

		public override short Sid => 4177;

		/// <summary>
		/// specifies the part of the series, trendline, or error bars the referenced data specifies.
		/// </summary>
		public byte LinkType
		{
			get
			{
				return field_1_linkType;
			}
			set
			{
				field_1_linkType = value;
			}
		}

		public byte ReferenceType
		{
			get
			{
				return field_2_referenceType;
			}
			set
			{
				field_2_referenceType = value;
			}
		}

		public short Options
		{
			get
			{
				return field_3_options;
			}
			set
			{
				field_3_options = value;
			}
		}

		/// <summary>
		/// specifies the number format to use for the data.
		/// </summary>
		public short IndexNumberFmtRecord
		{
			get
			{
				return field_4_indexNumberFmtRecord;
			}
			set
			{
				field_4_indexNumberFmtRecord = value;
			}
		}

		public Ptg[] FormulaOfLink
		{
			get
			{
				return field_5_formulaOfLink.Tokens;
			}
			set
			{
				field_5_formulaOfLink = Formula.Create(value);
			}
		}

		public bool IsCustomNumberFormat
		{
			get
			{
				return customNumberFormat.IsSet(field_3_options);
			}
			set
			{
				field_3_options = customNumberFormat.SetShortBoolean(field_3_options, value);
			}
		}

		public BRAIRecord()
		{
		}

		/// Constructs a LinkedData record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public BRAIRecord(RecordInputStream in1)
		{
			field_1_linkType = (byte)in1.ReadByte();
			field_2_referenceType = (byte)in1.ReadByte();
			field_3_options = in1.ReadShort();
			field_4_indexNumberFmtRecord = in1.ReadShort();
			int encodedTokenLen = in1.ReadUShort();
			field_5_formulaOfLink = Formula.Read(encodedTokenLen, in1);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[AI]\n");
			stringBuilder.Append("    .linkType             = ").Append(HexDump.ByteToHex(LinkType)).Append('\n');
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .referenceType        = ").Append(HexDump.ByteToHex(ReferenceType)).Append('\n');
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .options              = ").Append(HexDump.ShortToHex(Options)).Append('\n');
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("         .customNumberFormat       = ").Append(IsCustomNumberFormat).Append('\n');
			stringBuilder.Append("    .indexNumberFmtRecord = ").Append(HexDump.ShortToHex(IndexNumberFmtRecord)).Append('\n');
			stringBuilder.Append(Environment.NewLine);
			stringBuilder.Append("    .formulaOfLink        = ");
			Ptg[] tokens = field_5_formulaOfLink.Tokens;
			foreach (Ptg ptg in tokens)
			{
				stringBuilder.Append(ptg.ToString()).Append(ptg.RVAType).Append('\n');
			}
			stringBuilder.Append("[/AI]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteByte(field_1_linkType);
			out1.WriteByte(field_2_referenceType);
			out1.WriteShort(field_3_options);
			out1.WriteShort(field_4_indexNumberFmtRecord);
			field_5_formulaOfLink.Serialize(out1);
		}

		public override object Clone()
		{
			BRAIRecord bRAIRecord = new BRAIRecord();
			bRAIRecord.field_1_linkType = field_1_linkType;
			bRAIRecord.field_2_referenceType = field_2_referenceType;
			bRAIRecord.field_3_options = field_3_options;
			bRAIRecord.field_4_indexNumberFmtRecord = field_4_indexNumberFmtRecord;
			bRAIRecord.field_5_formulaOfLink = field_5_formulaOfLink.Copy();
			return bRAIRecord;
		}
	}
}
