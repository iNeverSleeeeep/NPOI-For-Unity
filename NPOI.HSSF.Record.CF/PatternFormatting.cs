using NPOI.SS.UserModel;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record.CF
{
	/// Pattern Formatting Block of the Conditional Formatting Rule Record.
	///
	/// @author Dmitriy Kumshayev
	public class PatternFormatting : ICloneable
	{
		private short field_15_pattern_style;

		private static BitField FillPatternStyle = BitFieldFactory.GetInstance(64512);

		private short field_16_pattern_color_indexes;

		private static BitField patternColorIndex = BitFieldFactory.GetInstance(127);

		private static BitField patternBackgroundColorIndex = BitFieldFactory.GetInstance(16256);

		/// Get the Fill pattern 
		/// @return Fill pattern
		public FillPattern FillPattern
		{
			get
			{
				return (FillPattern)FillPatternStyle.GetShortValue(field_15_pattern_style);
			}
			set
			{
				field_15_pattern_style = FillPatternStyle.SetShortValue(field_15_pattern_style, (short)value);
			}
		}

		/// Get the background Fill color
		/// @see org.apache.poi.hssf.usermodel.HSSFPalette#GetColor(short)
		/// @return Fill color
		public short FillBackgroundColor
		{
			get
			{
				return patternBackgroundColorIndex.GetShortValue(field_16_pattern_color_indexes);
			}
			set
			{
				field_16_pattern_color_indexes = patternBackgroundColorIndex.SetShortValue(field_16_pattern_color_indexes, value);
			}
		}

		/// Get the foreground Fill color
		/// @see org.apache.poi.hssf.usermodel.HSSFPalette#GetColor(short)
		/// @return Fill color
		public short FillForegroundColor
		{
			get
			{
				return patternColorIndex.GetShortValue(field_16_pattern_color_indexes);
			}
			set
			{
				field_16_pattern_color_indexes = patternColorIndex.SetShortValue(field_16_pattern_color_indexes, value);
			}
		}

		public PatternFormatting()
		{
			field_15_pattern_style = 0;
			field_16_pattern_color_indexes = 0;
		}

		/// Creates new FontFormatting 
		public PatternFormatting(RecordInputStream in1)
		{
			field_15_pattern_style = in1.ReadShort();
			field_16_pattern_color_indexes = in1.ReadShort();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("    [Pattern Formatting]\n");
			stringBuilder.Append("          .Fillpattern= ").Append(StringUtil.ToHexString((int)FillPattern)).Append("\n");
			stringBuilder.Append("          .fgcoloridx= ").Append(StringUtil.ToHexString(FillForegroundColor)).Append("\n");
			stringBuilder.Append("          .bgcoloridx= ").Append(StringUtil.ToHexString(FillBackgroundColor)).Append("\n");
			stringBuilder.Append("    [/Pattern Formatting]\n");
			return stringBuilder.ToString();
		}

		public object Clone()
		{
			PatternFormatting patternFormatting = new PatternFormatting();
			patternFormatting.field_15_pattern_style = field_15_pattern_style;
			patternFormatting.field_16_pattern_color_indexes = field_16_pattern_color_indexes;
			return patternFormatting;
		}

		public int Serialize(int offset, byte[] data)
		{
			LittleEndian.PutShort(data, offset, field_15_pattern_style);
			offset += 2;
			LittleEndian.PutShort(data, offset, field_16_pattern_color_indexes);
			offset += 2;
			return 4;
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_15_pattern_style);
			out1.WriteShort(field_16_pattern_color_indexes);
		}
	}
}
