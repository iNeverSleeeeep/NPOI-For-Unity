using NPOI.HSSF.Record.Cont;
using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        Name Record (aka Named Range) 
	/// Description:  Defines a named range within a workbook. 
	/// REFERENCE:  
	/// @author Libin Roman (Vista Portal LDT. Developer)
	/// @author  Sergei Kozello (sergeikozello at mail.ru)
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @version 1.0-pre
	public class NameRecord : ContinuableRecord
	{
		private enum Option : short
		{
			OPT_HIDDEN_NAME = 1,
			OPT_FUNCTION_NAME = 2,
			OPT_COMMAND_NAME = 4,
			OPT_MACRO = 8,
			OPT_COMPLEX = 0x10,
			OPT_BUILTIN = 0x20,
			OPT_BINDATA = 0x1000
		}

		public const short sid = 24;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_CONSOLIDATE_AREA = 0;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_AUTO_OPEN = 1;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_AUTO_CLOSE = 2;

		public const byte BUILTIN_EXTRACT = 3;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_DATABASE = 4;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_CRITERIA = 5;

		public const byte BUILTIN_PRINT_AREA = 6;

		public const byte BUILTIN_PRINT_TITLE = 7;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_RECORDER = 8;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_DATA_FORM = 9;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_AUTO_ACTIVATE = 10;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_AUTO_DEACTIVATE = 11;

		/// Included for completeness sake, not implemented
		public const byte BUILTIN_SHEET_TITLE = 12;

		public const byte BUILTIN_FILTER_DB = 13;

		private short field_1_option_flag;

		private byte field_2_keyboard_shortcut;

		/// One-based extern index of sheet (resolved via LinkTable). Zero if this is a global name  
		private short field_5_externSheetIndex_plus1;

		/// the one based sheet number.  
		private int field_6_sheetNumber;

		private bool field_11_nameIsMultibyte;

		private byte field_12_built_in_code;

		private string field_12_name_text;

		private Formula field_13_name_definition;

		private string field_14_custom_menu_text;

		private string field_15_description_text;

		private string field_16_help_topic_text;

		private string field_17_status_bar_text;

		protected int DataSize => 13 + NameRawSize + field_14_custom_menu_text.Length + field_15_description_text.Length + field_16_help_topic_text.Length + field_17_status_bar_text.Length + field_13_name_definition.EncodedSize;

		/// @return function Group
		/// @see FnGroupCountRecord
		public byte FnGroup
		{
			get
			{
				int num = field_1_option_flag & 0xFC0;
				return (byte)(num >> 4);
			}
		}

		/// Gets the option flag
		/// @return option flag
		public short OptionFlag
		{
			get
			{
				return field_1_option_flag;
			}
			set
			{
				field_1_option_flag = value;
			}
		}

		/// returns the keyboard shortcut
		/// @return keyboard shortcut
		public byte KeyboardShortcut
		{
			get
			{
				return field_2_keyboard_shortcut;
			}
			set
			{
				field_2_keyboard_shortcut = value;
			}
		}

		/// ** 
		public byte NameTextLength
		{
			get
			{
				if (IsBuiltInName)
				{
					return 1;
				}
				return (byte)field_12_name_text.Length;
			}
		}

		private int NameRawSize
		{
			get
			{
				if (IsBuiltInName)
				{
					return 1;
				}
				int length = field_12_name_text.Length;
				if (field_11_nameIsMultibyte)
				{
					return 2 * length;
				}
				return length;
			}
		}

		/// @return <c>true</c> if name has a formula (named range or defined value)
		public bool HasFormula
		{
			get
			{
				if (IsFormula(field_1_option_flag))
				{
					return field_13_name_definition.EncodedTokenSize > 0;
				}
				return false;
			}
		}

		/// @return true if name Is hidden
		public bool IsHiddenName
		{
			get
			{
				return (field_1_option_flag & 1) != 0;
			}
			set
			{
				if (value)
				{
					field_1_option_flag |= 1;
				}
				else
				{
					field_1_option_flag &= -2;
				}
			}
		}

		/// @return true if name Is a function
		public bool IsFunctionName
		{
			get
			{
				return (field_1_option_flag & 2) != 0;
			}
			set
			{
				if (value)
				{
					field_1_option_flag |= 2;
				}
				else
				{
					field_1_option_flag &= -3;
				}
			}
		}

		/// @return true if name Is a command
		public bool IsCommandName => (field_1_option_flag & 4) != 0;

		/// @return true if function macro or command macro
		public bool IsMacro => (field_1_option_flag & 8) != 0;

		/// @return true if array formula or user defined
		public bool IsComplexFunction => (field_1_option_flag & 0x10) != 0;

		/// Convenience Function to determine if the name Is a built-in name
		public bool IsBuiltInName => (OptionFlag & 0x20) != 0;

		/// Gets the name
		/// @return name
		public string NameText
		{
			get
			{
				if (!IsBuiltInName)
				{
					return field_12_name_text;
				}
				return TranslateBuiltInName(BuiltInName);
			}
			set
			{
				field_12_name_text = value;
				field_11_nameIsMultibyte = StringUtil.HasMultibyte(value);
			}
		}

		/// Gets the Built In Name
		/// @return the built in Name
		public byte BuiltInName => field_12_built_in_code;

		/// Gets the definition, reference (Formula)
		/// @return definition -- can be null if we cant Parse ptgs
		public Ptg[] NameDefinition
		{
			get
			{
				return field_13_name_definition.Tokens;
			}
			set
			{
				field_13_name_definition = Formula.Create(value);
			}
		}

		/// Get the custom menu text
		/// @return custom menu text
		public string CustomMenuText
		{
			get
			{
				return field_14_custom_menu_text;
			}
			set
			{
				field_14_custom_menu_text = value;
			}
		}

		/// Gets the description text
		/// @return description text
		public string DescriptionText
		{
			get
			{
				return field_15_description_text;
			}
			set
			{
				field_15_description_text = value;
			}
		}

		/// Get the help topic text
		/// @return gelp topic text
		public string HelpTopicText
		{
			get
			{
				return field_16_help_topic_text;
			}
			set
			{
				field_16_help_topic_text = value;
			}
		}

		/// Gets the status bar text
		/// @return status bar text
		public string StatusBarText
		{
			get
			{
				return field_17_status_bar_text;
			}
			set
			{
				field_17_status_bar_text = value;
			}
		}

		/// For named ranges, and built-in names
		/// @return the 1-based sheet number. 
		public int SheetNumber
		{
			get
			{
				return field_6_sheetNumber;
			}
			set
			{
				field_6_sheetNumber = value;
			}
		}

		/// Gets the extern sheet number
		/// @return extern sheet index
		public int ExternSheetNumber
		{
			get
			{
				if (field_13_name_definition.EncodedSize < 1)
				{
					return 0;
				}
				Ptg ptg = field_13_name_definition.Tokens[0];
				if (ptg.GetType() == typeof(Area3DPtg))
				{
					return ((Area3DPtg)ptg).ExternSheetIndex;
				}
				if (ptg.GetType() == typeof(Ref3DPtg))
				{
					return ((Ref3DPtg)ptg).ExternSheetIndex;
				}
				return 0;
			}
		}

		/// return the non static version of the id for this record.
		public override short Sid => 24;

		public static bool IsFormula(int optValue)
		{
			return (optValue & 0xF) == 0;
		}

		/// Creates new NameRecord 
		public NameRecord()
		{
			field_13_name_definition = Formula.Create(Ptg.EMPTY_PTG_ARRAY);
			field_12_name_text = "";
			field_14_custom_menu_text = "";
			field_15_description_text = "";
			field_16_help_topic_text = "";
			field_17_status_bar_text = "";
		}

		/// Constructs a Name record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public NameRecord(RecordInputStream ris)
		{
			byte[] buf = ris.ReadAllContinuedRemainder();
			ILittleEndianInput littleEndianInput = new LittleEndianByteArrayInputStream(buf);
			field_1_option_flag = littleEndianInput.ReadShort();
			field_2_keyboard_shortcut = (byte)littleEndianInput.ReadByte();
			int nChars = littleEndianInput.ReadByte();
			int encodedTokenLen = littleEndianInput.ReadShort();
			field_5_externSheetIndex_plus1 = littleEndianInput.ReadShort();
			field_6_sheetNumber = littleEndianInput.ReadUShort();
			int num = littleEndianInput.ReadUByte();
			int num2 = littleEndianInput.ReadUByte();
			int num3 = littleEndianInput.ReadUByte();
			int num4 = littleEndianInput.ReadUByte();
			field_11_nameIsMultibyte = (littleEndianInput.ReadByte() != 0);
			if (IsBuiltInName)
			{
				field_12_built_in_code = (byte)littleEndianInput.ReadByte();
			}
			else if (field_11_nameIsMultibyte)
			{
				field_12_name_text = StringUtil.ReadUnicodeLE(littleEndianInput, nChars);
			}
			else
			{
				field_12_name_text = StringUtil.ReadCompressedUnicode(littleEndianInput, nChars);
			}
			int totalEncodedLen = littleEndianInput.Available() - (num + num2 + num3 + num4);
			field_13_name_definition = Formula.Read(encodedTokenLen, littleEndianInput, totalEncodedLen);
			field_14_custom_menu_text = StringUtil.ReadCompressedUnicode(littleEndianInput, num);
			field_15_description_text = StringUtil.ReadCompressedUnicode(littleEndianInput, num2);
			field_16_help_topic_text = StringUtil.ReadCompressedUnicode(littleEndianInput, num3);
			field_17_status_bar_text = StringUtil.ReadCompressedUnicode(littleEndianInput, num4);
		}

		/// Constructor to Create a built-in named region
		/// @param builtin Built-in byte representation for the name record, use the public constants
		/// @param index 
		public NameRecord(byte builtin, int sheetNumber)
			: this()
		{
			field_12_built_in_code = builtin;
			OptionFlag = (short)(field_1_option_flag | 0x20);
			field_6_sheetNumber = sheetNumber;
		}

		/// Indicates that the defined name refers to a user-defined function.
		/// This attribute is used when there is an add-in or other code project associated with the file.
		///
		/// @param function <c>true</c> indicates the name refers to a function.
		public void SetFunction(bool function)
		{
			if (function)
			{
				field_1_option_flag |= 2;
			}
			else
			{
				field_1_option_flag &= -3;
			}
		}

		/// called by the class that Is responsible for writing this sucker.
		/// Subclasses should implement this so that their data Is passed back in a
		/// @param offset to begin writing at
		/// @param data byte array containing instance data
		/// @return number of bytes written
		protected override void Serialize(ContinuableRecordOutput out1)
		{
			int length = field_14_custom_menu_text.Length;
			int length2 = field_15_description_text.Length;
			int length3 = field_16_help_topic_text.Length;
			int length4 = field_17_status_bar_text.Length;
			out1.WriteShort(OptionFlag);
			out1.WriteByte(KeyboardShortcut);
			out1.WriteByte(NameTextLength);
			out1.WriteShort(field_13_name_definition.EncodedTokenSize);
			out1.WriteShort(field_5_externSheetIndex_plus1);
			out1.WriteShort(field_6_sheetNumber);
			out1.WriteByte(length);
			out1.WriteByte(length2);
			out1.WriteByte(length3);
			out1.WriteByte(length4);
			out1.WriteByte(field_11_nameIsMultibyte ? 1 : 0);
			if (IsBuiltInName)
			{
				out1.WriteByte(field_12_built_in_code);
			}
			else
			{
				string input = field_12_name_text;
				if (field_11_nameIsMultibyte)
				{
					StringUtil.PutUnicodeLE(input, out1);
				}
				else
				{
					StringUtil.PutCompressedUnicode(input, out1);
				}
			}
			field_13_name_definition.SerializeTokens(out1);
			field_13_name_definition.SerializeArrayConstantData(out1);
			StringUtil.PutCompressedUnicode(CustomMenuText, out1);
			StringUtil.PutCompressedUnicode(DescriptionText, out1);
			StringUtil.PutCompressedUnicode(HelpTopicText, out1);
			StringUtil.PutCompressedUnicode(StatusBarText, out1);
		}

		private Ptg CreateNewPtg()
		{
			return new Area3DPtg("A1:A1", 0);
		}

		/// @see Object#ToString()
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[NAME]\n");
			stringBuilder.Append("    .option flags         = ").Append(HexDump.ToHex(field_1_option_flag)).Append("\n");
			stringBuilder.Append("    .keyboard shortcut    = ").Append(HexDump.ToHex(field_2_keyboard_shortcut)).Append("\n");
			stringBuilder.Append("    .Length of the name   = ").Append(NameTextLength).Append("\n");
			stringBuilder.Append("    .extSheetIx(1-based, 0=Global)= ").Append(field_5_externSheetIndex_plus1).Append("\n");
			stringBuilder.Append("    .sheetTabIx           = ").Append(field_6_sheetNumber).Append("\n");
			stringBuilder.Append("    .Length of menu text (Char count)        = ").Append(field_14_custom_menu_text.Length).Append("\n");
			stringBuilder.Append("    .Length of description text (Char count) = ").Append(field_15_description_text.Length).Append("\n");
			stringBuilder.Append("    .Length of help topic text (Char count)  = ").Append(field_16_help_topic_text.Length).Append("\n");
			stringBuilder.Append("    .Length of status bar text (Char count)  = ").Append(field_17_status_bar_text.Length).Append("\n");
			stringBuilder.Append("    .Name (Unicode flag)  = ").Append(field_11_nameIsMultibyte).Append("\n");
			stringBuilder.Append("    .Name (Unicode text)  = ").Append(NameText).Append("\n");
			Ptg[] tokens = field_13_name_definition.Tokens;
			stringBuilder.AppendLine("    .Formula (nTokens=" + tokens.Length + "):");
			foreach (Ptg ptg in tokens)
			{
				stringBuilder.Append("       " + ptg.ToString()).Append(ptg.RVAType).Append("\n");
			}
			stringBuilder.Append("    .Menu text (Unicode string without Length field)        = ").Append(field_14_custom_menu_text).Append("\n");
			stringBuilder.Append("    .Description text (Unicode string without Length field) = ").Append(field_15_description_text).Append("\n");
			stringBuilder.Append("    .Help topic text (Unicode string without Length field)  = ").Append(field_16_help_topic_text).Append("\n");
			stringBuilder.Append("    .Status bar text (Unicode string without Length field)  = ").Append(field_17_status_bar_text).Append("\n");
			stringBuilder.Append("[/NAME]\n");
			return stringBuilder.ToString();
		}

		/// Creates a human Readable name for built in types
		/// @return Unknown if the built-in name cannot be translated
		protected string TranslateBuiltInName(byte name)
		{
			switch (name)
			{
			case 10:
				return "Auto_Activate";
			case 2:
				return "Auto_Close";
			case 11:
				return "Auto_Deactivate";
			case 1:
				return "Auto_Open";
			case 0:
				return "Consolidate_Area";
			case 5:
				return "Criteria";
			case 4:
				return "Database";
			case 9:
				return "Data_Form";
			case 6:
				return "Print_Area";
			case 7:
				return "Print_Titles";
			case 8:
				return "Recorder";
			case 12:
				return "Sheet_Title";
			case 13:
				return "_FilterDatabase";
			case 3:
				return "Extract";
			default:
				return "Unknown";
			}
		}
	}
}
