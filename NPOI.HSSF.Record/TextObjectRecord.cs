using NPOI.HSSF.Record.Cont;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.PTG;
using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.HSSF.Record
{
	public class TextObjectRecord : ContinuableRecord
	{
		public const short sid = 438;

		private const int FORMAT_RUN_ENCODED_SIZE = 8;

		private IRichTextString _text;

		private BitField _HorizontalTextAlignment = BitFieldFactory.GetInstance(14);

		private BitField _VerticalTextAlignment = BitFieldFactory.GetInstance(112);

		private BitField textLocked = BitFieldFactory.GetInstance(512);

		private int field_1_options;

		private int field_2_textOrientation;

		private int field_3_reserved4;

		private int field_4_reserved5;

		private int field_5_reserved6;

		private int field_8_reserved7;

		private int _unknownPreFormulaInt;

		/// expect tRef, tRef3D, tArea, tArea3D or tName 
		private OperandPtg _linkRefPtg;

		/// Not clear if needed .  Excel seems to be OK if this byte is not present. 
		/// Value is often the same as the earlier firstColumn byte. 
		private byte? _unknownPostFormulaByte;

		private int TrailingRecordsSize
		{
			get
			{
				if (_text.Length < 1)
				{
					return 0;
				}
				int num = 0;
				int num2 = _text.Length * 2;
				while (num2 > 0)
				{
					int num3 = Math.Min(8222, num2);
					num2 -= num3;
					num += 4;
					num += 1 + num3;
				}
				int num4 = (_text.NumFormattingRuns + 1) * 8 + 4;
				return num + num4;
			}
		}

		private int FormattingDataLength
		{
			get
			{
				if (_text.Length < 1)
				{
					return 0;
				}
				return (_text.NumFormattingRuns + 1) * 8;
			}
		}

		public IRichTextString Str
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}

		public override short Sid => 438;

		/// Get the text orientation field for the TextObjectBase record.
		///
		/// @return a TextOrientation
		public TextOrientation TextOrientation
		{
			get
			{
				return (TextOrientation)field_2_textOrientation;
			}
			set
			{
				field_2_textOrientation = (int)value;
			}
		}

		/// @return the Horizontal text alignment field value.
		public HorizontalTextAlignment HorizontalTextAlignment
		{
			get
			{
				return (HorizontalTextAlignment)_HorizontalTextAlignment.GetValue(field_1_options);
			}
			set
			{
				field_1_options = _HorizontalTextAlignment.SetValue(field_1_options, (int)value);
			}
		}

		/// @return the Vertical text alignment field value.
		public VerticalTextAlignment VerticalTextAlignment
		{
			get
			{
				return (VerticalTextAlignment)_VerticalTextAlignment.GetValue(field_1_options);
			}
			set
			{
				field_1_options = _VerticalTextAlignment.SetValue(field_1_options, (int)value);
			}
		}

		/// Text has been locked
		/// @return  the text locked field value.
		public bool IsTextLocked
		{
			get
			{
				return textLocked.IsSet(field_1_options);
			}
			set
			{
				field_1_options = textLocked.SetBoolean(field_1_options, value);
			}
		}

		public Ptg LinkRefPtg => _linkRefPtg;

		public TextObjectRecord()
		{
		}

		public TextObjectRecord(RecordInputStream in1)
		{
			field_1_options = in1.ReadUShort();
			field_2_textOrientation = in1.ReadUShort();
			field_3_reserved4 = in1.ReadUShort();
			field_4_reserved5 = in1.ReadUShort();
			field_5_reserved6 = in1.ReadUShort();
			int num = in1.ReadUShort();
			int num2 = in1.ReadUShort();
			field_8_reserved7 = in1.ReadInt();
			if (in1.Remaining > 0)
			{
				if (in1.Remaining < 11)
				{
					throw new RecordFormatException("Not enough remaining data for a link formula");
				}
				int size = in1.ReadUShort();
				_unknownPreFormulaInt = in1.ReadInt();
				Ptg[] array = Ptg.ReadTokens(size, in1);
				if (array.Length != 1)
				{
					throw new RecordFormatException("Read " + array.Length + " tokens but expected exactly 1");
				}
				_linkRefPtg = (OperandPtg)array[0];
				if (in1.Remaining > 0)
				{
					_unknownPostFormulaByte = (byte)in1.ReadByte();
				}
				else
				{
					_unknownPostFormulaByte = null;
				}
			}
			else
			{
				_linkRefPtg = null;
			}
			if (in1.Remaining > 0)
			{
				throw new RecordFormatException("Unused " + in1.Remaining + " bytes at end of record");
			}
			string str = (num <= 0) ? "" : ReadRawString(in1, num);
			_text = new HSSFRichTextString(str);
			if (num2 > 0)
			{
				ProcessFontRuns(in1, _text, num2);
			}
		}

		private static void ProcessFontRuns(RecordInputStream in1, IRichTextString str, int formattingRunDataLength)
		{
			if (formattingRunDataLength % 8 != 0)
			{
				throw new RecordFormatException("Bad format run data length " + formattingRunDataLength + ")");
			}
			int num = formattingRunDataLength / 8;
			for (int i = 0; i < num; i++)
			{
				short startIndex = in1.ReadShort();
				short fontIndex = in1.ReadShort();
				in1.ReadInt();
				str.ApplyFont(startIndex, str.Length, fontIndex);
			}
		}

		private static byte[] CreateFormatData(IRichTextString str)
		{
			int numFormattingRuns = str.NumFormattingRuns;
			byte[] array = new byte[(numFormattingRuns + 1) * 8];
			int num = 0;
			for (int i = 0; i < numFormattingRuns; i++)
			{
				LittleEndian.PutUShort(array, num, str.GetIndexOfFormattingRun(i));
				num += 2;
				int fontOfFormattingRun = ((HSSFRichTextString)str).GetFontOfFormattingRun(i);
				LittleEndian.PutUShort(array, num, (fontOfFormattingRun != 0) ? fontOfFormattingRun : 0);
				num += 2;
				num += 4;
			}
			LittleEndian.PutUShort(array, num, str.Length);
			num += 2;
			LittleEndian.PutUShort(array, num, 0);
			num += 2;
			num += 4;
			return array;
		}

		private void SerializeTrailingRecords(ContinuableRecordOutput out1)
		{
			out1.WriteContinue();
			out1.WriteStringData(_text.String);
			out1.WriteContinue();
			WriteFormatData(out1, _text);
		}

		private void WriteFormatData(ContinuableRecordOutput out1, IRichTextString str)
		{
			int numFormattingRuns = str.NumFormattingRuns;
			for (int i = 0; i < numFormattingRuns; i++)
			{
				out1.WriteShort(str.GetIndexOfFormattingRun(i));
				int fontOfFormattingRun = ((HSSFRichTextString)str).GetFontOfFormattingRun(i);
				out1.WriteShort((fontOfFormattingRun != 0) ? fontOfFormattingRun : 0);
				out1.WriteInt(0);
			}
			out1.WriteShort(str.Length);
			out1.WriteShort(0);
			out1.WriteInt(0);
		}

		private void SerializeTXORecord(ContinuableRecordOutput out1)
		{
			out1.WriteShort(field_1_options);
			out1.WriteShort(field_2_textOrientation);
			out1.WriteShort(field_3_reserved4);
			out1.WriteShort(field_4_reserved5);
			out1.WriteShort(field_5_reserved6);
			out1.WriteShort(_text.Length);
			out1.WriteShort(FormattingDataLength);
			out1.WriteInt(field_8_reserved7);
			if (_linkRefPtg != null)
			{
				int size = _linkRefPtg.Size;
				out1.WriteShort(size);
				out1.WriteInt(_unknownPreFormulaInt);
				_linkRefPtg.Write(out1);
				if (((int?)_unknownPostFormulaByte).HasValue)
				{
					out1.WriteByte(Convert.ToByte(_unknownPostFormulaByte, CultureInfo.InvariantCulture));
				}
			}
		}

		protected override void Serialize(ContinuableRecordOutput out1)
		{
			SerializeTXORecord(out1);
			if (_text.String.Length > 0)
			{
				SerializeTrailingRecords(out1);
			}
		}

		private void ProcessFontRuns(RecordInputStream in1)
		{
			while (in1.Remaining > 0)
			{
				short startIndex = in1.ReadShort();
				short fontIndex = in1.ReadShort();
				in1.ReadInt();
				_text.ApplyFont(startIndex, _text.Length, fontIndex);
			}
		}

		private static string ReadRawString(RecordInputStream in1, int textLength)
		{
			byte b = (byte)in1.ReadByte();
			if ((b & 1) == 0)
			{
				return in1.ReadCompressedUnicode(textLength);
			}
			return in1.ReadUnicodeLEString(textLength);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[TXO]\n");
			stringBuilder.Append("    .options        = ").Append(HexDump.ShortToHex(field_1_options)).Append("\n");
			stringBuilder.Append("         .IsHorizontal = ").Append(HorizontalTextAlignment).Append('\n');
			stringBuilder.Append("         .IsVertical   = ").Append(VerticalTextAlignment).Append('\n');
			stringBuilder.Append("         .textLocked   = ").Append(IsTextLocked).Append('\n');
			stringBuilder.Append("    .textOrientation= ").Append(HexDump.ShortToHex((int)TextOrientation)).Append("\n");
			stringBuilder.Append("    .reserved4      = ").Append(HexDump.ShortToHex(field_3_reserved4)).Append("\n");
			stringBuilder.Append("    .reserved5      = ").Append(HexDump.ShortToHex(field_4_reserved5)).Append("\n");
			stringBuilder.Append("    .reserved6      = ").Append(HexDump.ShortToHex(field_5_reserved6)).Append("\n");
			stringBuilder.Append("    .textLength     = ").Append(HexDump.ShortToHex(_text.Length)).Append("\n");
			stringBuilder.Append("    .reserved7      = ").Append(HexDump.IntToHex(field_8_reserved7)).Append("\n");
			stringBuilder.Append("    .string = ").Append(_text).Append('\n');
			for (int i = 0; i < _text.NumFormattingRuns; i++)
			{
				stringBuilder.Append("    .textrun = ").Append(((HSSFRichTextString)_text).GetFontOfFormattingRun(i)).Append('\n');
			}
			stringBuilder.Append("[/TXO]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			TextObjectRecord textObjectRecord = new TextObjectRecord();
			textObjectRecord._text = _text;
			textObjectRecord.field_1_options = field_1_options;
			textObjectRecord.field_2_textOrientation = field_2_textOrientation;
			textObjectRecord.field_3_reserved4 = field_3_reserved4;
			textObjectRecord.field_4_reserved5 = field_4_reserved5;
			textObjectRecord.field_5_reserved6 = field_5_reserved6;
			textObjectRecord.field_8_reserved7 = field_8_reserved7;
			textObjectRecord._text = _text;
			if (_linkRefPtg != null)
			{
				textObjectRecord._unknownPreFormulaInt = _unknownPreFormulaInt;
				textObjectRecord._linkRefPtg = _linkRefPtg.Copy();
				textObjectRecord._unknownPostFormulaByte = _unknownPostFormulaByte;
			}
			return textObjectRecord;
		}
	}
}
