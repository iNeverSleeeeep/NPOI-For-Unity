using NPOI.SS.UserModel;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.CF
{
	/// Font Formatting Block of the Conditional Formatting Rule Record.
	///
	/// @author Dmitriy Kumshayev
	public class FontFormatting
	{
		private const int OFFSET_FONT_NAME = 0;

		private const int OFFSET_FONT_HEIGHT = 64;

		private const int OFFSET_FONT_OPTIONS = 68;

		private const int OFFSET_FONT_WEIGHT = 72;

		private const int OFFSET_ESCAPEMENT_TYPE = 74;

		private const int OFFSET_UNDERLINE_TYPE = 76;

		private const int OFFSET_FONT_COLOR_INDEX = 80;

		private const int OFFSET_OPTION_FLAGS = 88;

		private const int OFFSET_ESCAPEMENT_TYPE_MODIFIED = 92;

		private const int OFFSET_UNDERLINE_TYPE_MODIFIED = 96;

		private const int OFFSET_FONT_WEIGHT_MODIFIED = 100;

		private const int OFFSET_NOT_USED1 = 104;

		private const int OFFSET_NOT_USED2 = 108;

		private const int OFFSET_NOT_USED3 = 112;

		private const int OFFSET_FONT_FORMATING_END = 116;

		private const int RAW_DATA_SIZE = 118;

		public const int FONT_CELL_HEIGHT_PRESERVED = -1;

		/// Normal boldness (not bold) 
		private const short FONT_WEIGHT_NORMAL = 400;

		/// Bold boldness (bold)
		private const short FONT_WEIGHT_BOLD = 700;

		private byte[] _rawData;

		private static BitField posture = BitFieldFactory.GetInstance(2);

		private static BitField outline = BitFieldFactory.GetInstance(8);

		private static BitField shadow = BitFieldFactory.GetInstance(16);

		private static BitField cancellation = BitFieldFactory.GetInstance(128);

		private static BitField styleModified = BitFieldFactory.GetInstance(2);

		private static BitField outlineModified = BitFieldFactory.GetInstance(8);

		private static BitField shadowModified = BitFieldFactory.GetInstance(16);

		private static BitField cancellationModified = BitFieldFactory.GetInstance(128);

		/// Gets the height of the font in 1/20th point Units
		///
		/// @return fontheight (in points/20); or -1 if not modified
		public int FontHeight
		{
			get
			{
				return GetInt(64);
			}
			set
			{
				SetInt(64, value);
			}
		}

		/// Get whether the font Is to be italics or not
		///
		/// @return italics - whether the font Is italics or not
		/// @see #GetAttributes()
		public bool IsItalic
		{
			get
			{
				return GetFontOption(posture);
			}
			set
			{
				SetFontOption(value, posture);
			}
		}

		public bool IsOutlineOn
		{
			get
			{
				return GetFontOption(outline);
			}
			set
			{
				SetFontOption(value, outline);
			}
		}

		public bool IsShadowOn
		{
			get
			{
				return GetFontOption(shadow);
			}
			set
			{
				SetFontOption(value, shadow);
			}
		}

		/// Get whether the font Is to be stricken out or not
		///
		/// @return strike - whether the font Is stricken out or not
		/// @see #GetAttributes()
		public bool IsStruckout
		{
			get
			{
				return GetFontOption(cancellation);
			}
			set
			{
				SetFontOption(value, cancellation);
			}
		}

		/// <summary>
		/// Get or set the font weight for this font (100-1000dec or 0x64-0x3e8).  
		/// Default Is 0x190 for normal and 0x2bc for bold
		/// </summary>
		public short FontWeight
		{
			get
			{
				return GetShort(72);
			}
			set
			{
				short num = value;
				if (num < 100)
				{
					num = 100;
				}
				if (num > 1000)
				{
					num = 1000;
				}
				SetShort(72, num);
			}
		}

		/// <summary>
		///             Get or set whether the font weight is set to bold or not 
		/// </summary>
		public bool IsBold
		{
			get
			{
				return FontWeight == 700;
			}
			set
			{
				FontWeight = (short)(value ? 700 : 400);
			}
		}

		/// Get the type of base or subscript for the font
		///
		/// @return base or subscript option
		/// @see org.apache.poi.hssf.usermodel.HSSFFontFormatting#SS_NONE
		/// @see org.apache.poi.hssf.usermodel.HSSFFontFormatting#SS_SUPER
		/// @see org.apache.poi.hssf.usermodel.HSSFFontFormatting#SS_SUB
		public FontSuperScript EscapementType
		{
			get
			{
				return (FontSuperScript)GetShort(74);
			}
			set
			{
				SetShort(74, (int)value);
			}
		}

		/// Get the type of Underlining for the font
		///
		/// @return font Underlining type
		public FontUnderlineType UnderlineType
		{
			get
			{
				return (FontUnderlineType)GetShort(76);
			}
			set
			{
				SetShort(76, (int)value);
			}
		}

		public short FontColorIndex
		{
			get
			{
				return (short)GetInt(80);
			}
			set
			{
				SetInt(80, value);
			}
		}

		public bool IsFontStyleModified
		{
			get
			{
				return GetOptionFlag(styleModified);
			}
			set
			{
				SetOptionFlag(value, styleModified);
			}
		}

		public bool IsFontOutlineModified
		{
			get
			{
				return GetOptionFlag(outlineModified);
			}
			set
			{
				SetOptionFlag(value, outlineModified);
			}
		}

		public bool IsFontShadowModified
		{
			get
			{
				return GetOptionFlag(shadowModified);
			}
			set
			{
				SetOptionFlag(value, shadowModified);
			}
		}

		public bool IsFontCancellationModified
		{
			get
			{
				return GetOptionFlag(cancellationModified);
			}
			set
			{
				SetOptionFlag(value, cancellationModified);
			}
		}

		public bool IsEscapementTypeModified
		{
			get
			{
				int @int = GetInt(92);
				return @int == 0;
			}
			set
			{
				int value2 = (!value) ? 1 : 0;
				SetInt(92, value2);
			}
		}

		public bool IsUnderlineTypeModified
		{
			get
			{
				int @int = GetInt(96);
				return @int == 0;
			}
			set
			{
				int value2 = (!value) ? 1 : 0;
				SetInt(96, value2);
			}
		}

		public bool IsFontWeightModified
		{
			get
			{
				int @int = GetInt(100);
				return @int == 0;
			}
			set
			{
				int value2 = (!value) ? 1 : 0;
				SetInt(100, value2);
			}
		}

		private FontFormatting(byte[] rawData)
		{
			_rawData = rawData;
		}

		public FontFormatting()
			: this(new byte[118])
		{
			FontHeight = -1;
			IsItalic = false;
			IsFontWeightModified = false;
			IsOutlineOn = false;
			IsShadowOn = false;
			IsStruckout = false;
			EscapementType = FontSuperScript.None;
			UnderlineType = FontUnderlineType.None;
			FontColorIndex = -1;
			IsFontStyleModified = false;
			IsFontOutlineModified = false;
			IsFontShadowModified = false;
			IsFontCancellationModified = false;
			IsEscapementTypeModified = false;
			IsUnderlineTypeModified = false;
			SetShort(0, 0);
			SetInt(104, 1);
			SetInt(108, 0);
			SetInt(112, 2147483647);
			SetShort(116, 1);
		}

		/// Creates new FontFormatting 
		public FontFormatting(RecordInputStream in1)
			: this(new byte[118])
		{
			for (int i = 0; i < _rawData.Length; i++)
			{
				_rawData[i] = (byte)in1.ReadByte();
			}
		}

		private short GetShort(int offset)
		{
			return LittleEndian.GetShort(_rawData, offset);
		}

		private void SetShort(int offset, int value)
		{
			LittleEndian.PutShort(_rawData, offset, (short)value);
		}

		private int GetInt(int offset)
		{
			return LittleEndian.GetInt(_rawData, offset);
		}

		private void SetInt(int offset, int value)
		{
			LittleEndian.PutInt(_rawData, offset, value);
		}

		public byte[] GetRawRecord()
		{
			return _rawData;
		}

		private void SetFontOption(bool option, BitField field)
		{
			int @int = GetInt(68);
			@int = field.SetBoolean(@int, option);
			SetInt(68, @int);
		}

		private bool GetFontOption(BitField field)
		{
			int @int = GetInt(68);
			return field.IsSet(@int);
		}

		private bool GetOptionFlag(BitField field)
		{
			int @int = GetInt(88);
			if (field.GetValue(@int) != 0)
			{
				return false;
			}
			return true;
		}

		private void SetOptionFlag(bool modified, BitField field)
		{
			int value = (!modified) ? 1 : 0;
			int @int = GetInt(88);
			@int = field.SetValue(@int, value);
			SetInt(88, @int);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\t[Font Formatting]\n");
			stringBuilder.Append("\t.font height = ").Append(FontHeight).Append(" twips\n");
			if (IsFontStyleModified)
			{
				stringBuilder.Append("\t.font posture = ").Append(IsItalic ? "Italic" : "Normal").Append("\n");
			}
			else
			{
				stringBuilder.Append("\t.font posture = ]not modified]").Append("\n");
			}
			if (IsFontOutlineModified)
			{
				stringBuilder.Append("\t.font outline = ").Append(IsOutlineOn).Append("\n");
			}
			else
			{
				stringBuilder.Append("\t.font outline Is not modified\n");
			}
			if (IsFontShadowModified)
			{
				stringBuilder.Append("\t.font shadow = ").Append(IsShadowOn).Append("\n");
			}
			else
			{
				stringBuilder.Append("\t.font shadow Is not modified\n");
			}
			if (IsFontCancellationModified)
			{
				stringBuilder.Append("\t.font strikeout = ").Append(IsStruckout).Append("\n");
			}
			else
			{
				stringBuilder.Append("\t.font strikeout Is not modified\n");
			}
			if (IsFontStyleModified)
			{
				stringBuilder.Append("\t.font weight = ").Append(FontWeight).Append((FontWeight == 400) ? "(Normal)" : ((FontWeight == 700) ? "(Bold)" : ("0x" + StringUtil.ToHexString(FontWeight))))
					.Append("\n");
			}
			else
			{
				stringBuilder.Append("\t.font weight = ]not modified]").Append("\n");
			}
			if (IsEscapementTypeModified)
			{
				stringBuilder.Append("\t.escapement type = ").Append(EscapementType).Append("\n");
			}
			else
			{
				stringBuilder.Append("\t.escapement type Is not modified\n");
			}
			if (IsUnderlineTypeModified)
			{
				stringBuilder.Append("\t.underline type = ").Append(UnderlineType).Append("\n");
			}
			else
			{
				stringBuilder.Append("\t.underline type Is not modified\n");
			}
			stringBuilder.Append("\t.color index = ").Append("0x" + StringUtil.ToHexString(FontColorIndex).ToUpper()).Append("\n");
			stringBuilder.Append("\t[/Font Formatting]\n");
			return stringBuilder.ToString();
		}

		public object Clone()
		{
			byte[] rawData = (byte[])_rawData.Clone();
			return new FontFormatting(rawData);
		}
	}
}
