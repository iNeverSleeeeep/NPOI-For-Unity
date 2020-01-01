using NPOI.HSSF.Record;
using NPOI.HSSF.Record.CF;
using NPOI.SS.UserModel;

namespace NPOI.HSSF.UserModel
{
	/// High level representation for Font Formatting component
	/// of Conditional Formatting Settings
	///
	/// @author Dmitriy Kumshayev
	public class HSSFFontFormatting : IFontFormatting
	{
		private FontFormatting fontFormatting;

		/// Get the type of base or subscript for the font
		///
		/// @return base or subscript option
		public FontSuperScript EscapementType
		{
			get
			{
				return fontFormatting.EscapementType;
			}
			set
			{
				switch (value)
				{
				case FontSuperScript.Super:
				case FontSuperScript.Sub:
					fontFormatting.EscapementType = value;
					fontFormatting.IsEscapementTypeModified = true;
					break;
				case FontSuperScript.None:
					fontFormatting.EscapementType = value;
					fontFormatting.IsEscapementTypeModified = false;
					break;
				}
			}
		}

		/// @return font color index
		public short FontColorIndex
		{
			get
			{
				return fontFormatting.FontColorIndex;
			}
			set
			{
				fontFormatting.FontColorIndex = value;
			}
		}

		/// Gets the height of the font in 1/20th point Units
		///
		/// @return fontheight (in points/20); or -1 if not modified
		public int FontHeight
		{
			get
			{
				return fontFormatting.FontHeight;
			}
			set
			{
				fontFormatting.FontHeight = value;
			}
		}

		/// Get the font weight for this font (100-1000dec or 0x64-0x3e8).  Default Is
		/// 0x190 for normal and 0x2bc for bold
		///
		/// @return bw - a number between 100-1000 for the fonts "boldness"
		public short FontWeight => fontFormatting.FontWeight;

		/// Get the type of Underlining for the font
		///
		/// @return font Underlining type
		///
		/// @see #U_NONE
		/// @see #U_SINGLE
		/// @see #U_DOUBLE
		/// @see #U_SINGLE_ACCOUNTING
		/// @see #U_DOUBLE_ACCOUNTING
		public FontUnderlineType UnderlineType
		{
			get
			{
				return fontFormatting.UnderlineType;
			}
			set
			{
				switch (value)
				{
				case FontUnderlineType.Single:
				case FontUnderlineType.Double:
				case FontUnderlineType.SingleAccounting:
				case FontUnderlineType.DoubleAccounting:
					fontFormatting.UnderlineType = value;
					IsUnderlineTypeModified = true;
					break;
				case FontUnderlineType.None:
					fontFormatting.UnderlineType = value;
					IsUnderlineTypeModified = false;
					break;
				}
			}
		}

		/// Get whether the font weight Is Set to bold or not
		///
		/// @return bold - whether the font Is bold or not
		public bool IsBold
		{
			get
			{
				if (fontFormatting.IsFontWeightModified)
				{
					return fontFormatting.IsBold;
				}
				return false;
			}
		}

		/// @return true if escapement type was modified from default   
		public bool IsEscapementTypeModified
		{
			get
			{
				return fontFormatting.IsEscapementTypeModified;
			}
			set
			{
				fontFormatting.IsEscapementTypeModified = value;
			}
		}

		/// @return true if font cancellation was modified from default   
		public bool IsFontCancellationModified
		{
			get
			{
				return fontFormatting.IsFontCancellationModified;
			}
			set
			{
				fontFormatting.IsFontCancellationModified = value;
			}
		}

		/// @return true if font outline type was modified from default   
		public bool IsFontOutlineModified
		{
			get
			{
				return fontFormatting.IsFontOutlineModified;
			}
			set
			{
				fontFormatting.IsFontOutlineModified = value;
			}
		}

		/// @return true if font shadow type was modified from default   
		public bool IsFontShadowModified
		{
			get
			{
				return fontFormatting.IsFontShadowModified;
			}
			set
			{
				fontFormatting.IsFontShadowModified = value;
			}
		}

		/// @return true if font style was modified from default   
		public bool IsFontStyleModified
		{
			get
			{
				return fontFormatting.IsFontStyleModified;
			}
			set
			{
				fontFormatting.IsFontStyleModified = value;
			}
		}

		/// @return true if font style was Set to <i>italic</i> 
		public bool IsItalic
		{
			get
			{
				if (fontFormatting.IsFontStyleModified)
				{
					return fontFormatting.IsItalic;
				}
				return false;
			}
		}

		/// @return true if font outline Is on
		public bool IsOutlineOn
		{
			get
			{
				if (fontFormatting.IsFontOutlineModified)
				{
					return fontFormatting.IsOutlineOn;
				}
				return false;
			}
			set
			{
				fontFormatting.IsOutlineOn = value;
				fontFormatting.IsFontOutlineModified = value;
			}
		}

		/// @return true if font shadow Is on
		public bool IsShadowOn
		{
			get
			{
				if (fontFormatting.IsFontOutlineModified)
				{
					return fontFormatting.IsShadowOn;
				}
				return false;
			}
			set
			{
				fontFormatting.IsShadowOn = value;
				fontFormatting.IsFontShadowModified = value;
			}
		}

		/// @return true if font strikeout Is on
		public bool IsStrikeout
		{
			get
			{
				if (fontFormatting.IsFontCancellationModified)
				{
					return fontFormatting.IsStruckout;
				}
				return false;
			}
			set
			{
				fontFormatting.IsStruckout = value;
				fontFormatting.IsFontCancellationModified = value;
			}
		}

		/// @return true if font Underline type was modified from default   
		public bool IsUnderlineTypeModified
		{
			get
			{
				return fontFormatting.IsUnderlineTypeModified;
			}
			set
			{
				fontFormatting.IsUnderlineTypeModified = value;
			}
		}

		/// @return true if font weight was modified from default   
		public bool IsFontWeightModified => fontFormatting.IsFontWeightModified;

		public HSSFFontFormatting(CFRuleRecord cfRuleRecord)
		{
			fontFormatting = cfRuleRecord.FontFormatting;
		}

		protected FontFormatting GetFontFormattingBlock()
		{
			return fontFormatting;
		}

		/// @return
		/// @see org.apache.poi.hssf.record.cf.FontFormatting#GetRawRecord()
		protected byte[] GetRawRecord()
		{
			return fontFormatting.GetRawRecord();
		}

		/// Set font style options.
		///
		/// @param italic - if true, Set posture style to italic, otherwise to normal 
		/// @param bold- if true, Set font weight to bold, otherwise to normal
		public void SetFontStyle(bool italic, bool bold)
		{
			bool flag = italic || bold;
			fontFormatting.IsItalic = italic;
			fontFormatting.IsBold = bold;
			fontFormatting.IsFontStyleModified = flag;
			fontFormatting.IsFontWeightModified = flag;
		}

		/// Set font style options to default values (non-italic, non-bold)
		public void ResetFontStyle()
		{
			SetFontStyle(italic: false, bold: false);
		}
	}
}
