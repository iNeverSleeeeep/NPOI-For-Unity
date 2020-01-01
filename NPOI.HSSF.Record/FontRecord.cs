using NPOI.SS.UserModel;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Font Record - descrbes a font in the workbook (index = 0-3,5-infinity - skip 4)
	/// Description:  An element in the Font Table
	/// REFERENCE:  PG 315 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class FontRecord : StandardRecord
	{
		public const short sid = 49;

		private short field_1_font_height;

		private short field_2_attributes;

		private static BitField italic = BitFieldFactory.GetInstance(2);

		private static BitField strikeout = BitFieldFactory.GetInstance(8);

		private static BitField macoutline = BitFieldFactory.GetInstance(16);

		private static BitField macshadow = BitFieldFactory.GetInstance(32);

		private short field_3_color_palette_index;

		private short field_4_bold_weight;

		private short field_5_base_sub_script;

		private byte field_6_underline;

		private byte field_7_family;

		private byte field_8_charset;

		private byte field_9_zero;

		private string field_11_font_name;

		/// Set the font to be italics or not
		///
		/// @param italics - whether the font Is italics or not
		/// @see #SetAttributes(short)
		public bool IsItalic
		{
			get
			{
				return italic.IsSet(field_2_attributes);
			}
			set
			{
				field_2_attributes = italic.SetShortBoolean(field_2_attributes, value);
			}
		}

		/// Set the font to be stricken out or not
		///
		/// @param strike - whether the font Is stricken out or not
		/// @see #SetAttributes(short)
		public bool IsStrikeout
		{
			get
			{
				return strikeout.IsSet(field_2_attributes);
			}
			set
			{
				field_2_attributes = strikeout.SetShortBoolean(field_2_attributes, value);
			}
		}

		/// whether to use the mac outline font style thing (mac only) - Some mac person
		/// should comment this instead of me doing it (since I have no idea)
		///
		/// @param mac - whether to do that mac font outline thing or not
		/// @see #SetAttributes(short)
		public bool IsMacoutlined
		{
			get
			{
				return macoutline.IsSet(field_2_attributes);
			}
			set
			{
				field_2_attributes = macoutline.SetShortBoolean(field_2_attributes, value);
			}
		}

		/// whether to use the mac shado font style thing (mac only) - Some mac person
		/// should comment this instead of me doing it (since I have no idea)
		///
		/// @param mac - whether to do that mac font shadow thing or not
		/// @see #SetAttributes(short)
		public bool IsMacshadowed
		{
			get
			{
				return macshadow.IsSet(field_2_attributes);
			}
			set
			{
				field_2_attributes = macshadow.SetShortBoolean(field_2_attributes, value);
			}
		}

		/// Set the type of Underlining for the font
		public FontUnderlineType Underline
		{
			get
			{
				return (FontUnderlineType)field_6_underline;
			}
			set
			{
				field_6_underline = (byte)value;
			}
		}

		/// Set the font family (TODO)
		///
		/// @param f family
		public byte Family
		{
			get
			{
				return field_7_family;
			}
			set
			{
				field_7_family = value;
			}
		}

		/// Set the Char Set
		///
		/// @param charSet - CharSet
		public byte Charset
		{
			get
			{
				return field_8_charset;
			}
			set
			{
				field_8_charset = value;
			}
		}

		/// Set the name of the font
		///
		/// @param fn - name of the font (i.e. "Arial")
		public string FontName
		{
			get
			{
				return field_11_font_name;
			}
			set
			{
				field_11_font_name = value;
			}
		}

		/// Gets the height of the font in 1/20th point Units
		///
		/// @return fontheight (in points/20)
		public short FontHeight
		{
			get
			{
				return field_1_font_height;
			}
			set
			{
				field_1_font_height = value;
			}
		}

		/// Get the font attributes (see individual bit Getters that reference this method)
		///
		/// @return attribute - the bitmask
		public short Attributes
		{
			get
			{
				return field_2_attributes;
			}
			set
			{
				field_2_attributes = value;
			}
		}

		/// Get the font's color palette index
		///
		/// @return cpi - font color index
		public short ColorPaletteIndex
		{
			get
			{
				return field_3_color_palette_index;
			}
			set
			{
				field_3_color_palette_index = value;
			}
		}

		/// Get the bold weight for this font (100-1000dec or 0x64-0x3e8).  Default Is
		/// 0x190 for normal and 0x2bc for bold
		///
		/// @return bw - a number between 100-1000 for the fonts "boldness"
		public short BoldWeight
		{
			get
			{
				return field_4_bold_weight;
			}
			set
			{
				field_4_bold_weight = value;
			}
		}

		/// Get the type of base or subscript for the font
		///
		/// @return base or subscript option
		public FontSuperScript SuperSubScript
		{
			get
			{
				return (FontSuperScript)field_5_base_sub_script;
			}
			set
			{
				field_5_base_sub_script = (short)value;
			}
		}

		protected override int DataSize
		{
			get
			{
				int num = 16;
				int length = field_11_font_name.Length;
				if (length < 1)
				{
					return num;
				}
				bool flag = StringUtil.HasMultibyte(field_11_font_name);
				return num + length * ((!flag) ? 1 : 2);
			}
		}

		public override short Sid => 49;

		public FontRecord()
		{
		}

		/// Constructs a Font record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public FontRecord(RecordInputStream in1)
		{
			field_1_font_height = in1.ReadShort();
			field_2_attributes = in1.ReadShort();
			field_3_color_palette_index = in1.ReadShort();
			field_4_bold_weight = in1.ReadShort();
			field_5_base_sub_script = in1.ReadShort();
			field_6_underline = (byte)in1.ReadByte();
			field_7_family = (byte)in1.ReadByte();
			field_8_charset = (byte)in1.ReadByte();
			field_9_zero = (byte)in1.ReadByte();
			int num = (byte)in1.ReadByte();
			int num2 = in1.ReadUByte();
			if (num > 0)
			{
				if (num2 == 0)
				{
					field_11_font_name = in1.ReadCompressedUnicode(num);
				}
				else
				{
					field_11_font_name = in1.ReadUnicodeLEString(num);
				}
			}
			else
			{
				field_11_font_name = "";
			}
		}

		/// Clones all the font style information from another
		///  FontRecord, onto this one. This 
		///  will then hold all the same font style options.
		public void CloneStyleFrom(FontRecord source)
		{
			field_1_font_height = source.field_1_font_height;
			field_2_attributes = source.field_2_attributes;
			field_3_color_palette_index = source.field_3_color_palette_index;
			field_4_bold_weight = source.field_4_bold_weight;
			field_5_base_sub_script = source.field_5_base_sub_script;
			field_6_underline = source.field_6_underline;
			field_7_family = source.field_7_family;
			field_8_charset = source.field_8_charset;
			field_9_zero = source.field_9_zero;
			field_11_font_name = source.field_11_font_name;
		}

		/// Does this FontRecord have all the same font
		///  properties as the supplied FontRecord?
		/// Note that {@link #equals(Object)} will check
		///  for exact objects, while this will check
		///  for exact contents, because normally the
		///  font record's position makes a big
		///  difference too.  
		public bool SameProperties(FontRecord other)
		{
			if (field_1_font_height == other.field_1_font_height && field_2_attributes == other.field_2_attributes && field_3_color_palette_index == other.field_3_color_palette_index && field_4_bold_weight == other.field_4_bold_weight && field_5_base_sub_script == other.field_5_base_sub_script && field_6_underline == other.field_6_underline && field_7_family == other.field_7_family && field_8_charset == other.field_8_charset && field_9_zero == other.field_9_zero)
			{
				return field_11_font_name.Equals(other.field_11_font_name);
			}
			return false;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[FONT]\n");
			stringBuilder.Append("    .fontheight      = ").Append(StringUtil.ToHexString(FontHeight)).Append("\n");
			stringBuilder.Append("    .attributes      = ").Append(StringUtil.ToHexString(Attributes)).Append("\n");
			stringBuilder.Append("         .italic     = ").Append(IsItalic).Append("\n");
			stringBuilder.Append("         .strikout   = ").Append(IsStrikeout).Append("\n");
			stringBuilder.Append("         .macoutlined= ").Append(IsMacoutlined).Append("\n");
			stringBuilder.Append("         .macshadowed= ").Append(IsMacshadowed).Append("\n");
			stringBuilder.Append("    .colorpalette    = ").Append(StringUtil.ToHexString(ColorPaletteIndex)).Append("\n");
			stringBuilder.Append("    .boldweight      = ").Append(StringUtil.ToHexString(BoldWeight)).Append("\n");
			stringBuilder.Append("    .basesubscript  = ").Append(StringUtil.ToHexString((short)SuperSubScript)).Append("\n");
			stringBuilder.Append("    .underline       = ").Append(StringUtil.ToHexString((short)Underline)).Append("\n");
			stringBuilder.Append("    .family          = ").Append(StringUtil.ToHexString(Family)).Append("\n");
			stringBuilder.Append("    .charset         = ").Append(StringUtil.ToHexString(Charset)).Append("\n");
			stringBuilder.Append("    .fontname        = ").Append(FontName).Append("\n");
			stringBuilder.Append("[/FONT]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(FontHeight);
			out1.WriteShort(Attributes);
			out1.WriteShort(ColorPaletteIndex);
			out1.WriteShort(BoldWeight);
			out1.WriteShort((int)SuperSubScript);
			out1.WriteByte((int)Underline);
			out1.WriteByte(Family);
			out1.WriteByte(Charset);
			out1.WriteByte(field_9_zero);
			int length = field_11_font_name.Length;
			out1.WriteByte(length);
			bool flag = StringUtil.HasMultibyte(field_11_font_name);
			out1.WriteByte(flag ? 1 : 0);
			if (length > 0)
			{
				if (flag)
				{
					StringUtil.PutUnicodeLE(field_11_font_name, out1);
				}
				else
				{
					StringUtil.PutCompressedUnicode(field_11_font_name, out1);
				}
			}
		}

		public override int GetHashCode()
		{
			int num = 31;
			int num2 = 1;
			num2 = num * num2 + ((field_11_font_name != null) ? field_11_font_name.GetHashCode() : 0);
			num2 = num * num2 + field_1_font_height;
			num2 = num * num2 + field_2_attributes;
			num2 = num * num2 + field_3_color_palette_index;
			num2 = num * num2 + field_4_bold_weight;
			num2 = num * num2 + field_5_base_sub_script;
			num2 = num * num2 + field_6_underline;
			num2 = num * num2 + field_7_family;
			num2 = num * num2 + field_8_charset;
			return num * num2 + field_9_zero;
		}

		/// Only returns two for the same exact object -
		///  creating a second FontRecord with the same
		///  properties won't be considered equal, as 
		///  the record's position in the record stream
		///  matters.
		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			return false;
		}
	}
}
