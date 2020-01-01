using NPOI.SS.Formula;
using NPOI.SS.Formula.PTG;
using NPOI.SS.Util;
using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title:        DATAVALIDATION Record (0x01BE)<p />
	/// Description:  This record stores data validation Settings and a list of cell ranges
	///               which contain these Settings. The data validation Settings of a sheet
	///               are stored in a sequential list of DV records. This list Is followed by
	///               DVAL record(s)
	/// @author Dragos Buleandra (dragos.buleandra@trade2b.ro)
	/// @version 2.0-pre
	public class DVRecord : StandardRecord
	{
		public const short sid = 446;

		public const int STRING_PROMPT_TITLE = 0;

		public const int STRING_ERROR_TITLE = 1;

		public const int STRING_PROMPT_TEXT = 2;

		public const int STRING_ERROR_TEXT = 3;

		private static readonly UnicodeString NULL_TEXT_STRING = new UnicodeString("\0");

		/// Option flags 
		private int _option_flags;

		/// Title of the prompt box 
		private UnicodeString _promptTitle;

		/// Title of the error box 
		private UnicodeString _errorTitle;

		/// Text of the prompt box 
		private UnicodeString _promptText;

		/// Text of the error box 
		private UnicodeString _errorText;

		/// Not used - Excel seems to always write 0x3FE0 
		private short _not_used_1 = 16352;

		/// Formula data for first condition (RPN token array without size field) 
		private Formula _formula1;

		/// Not used - Excel seems to always write 0x0000 
		private short _not_used_2;

		/// Formula data for second condition (RPN token array without size field) 
		private Formula _formula2;

		/// Cell range address list with all affected ranges 
		private CellRangeAddressList _regions;

		/// Option flags field
		/// @see org.apache.poi.hssf.util.HSSFDataValidation utility class
		private BitField opt_data_type = new BitField(15);

		private BitField opt_error_style = new BitField(112);

		private BitField opt_string_list_formula = new BitField(128);

		private BitField opt_empty_cell_allowed = new BitField(256);

		private BitField opt_suppress_dropdown_arrow = new BitField(512);

		private BitField opt_show_prompt_on_cell_selected = new BitField(262144);

		private BitField opt_show_error_on_invalid_value = new BitField(524288);

		private BitField opt_condition_operator = new BitField(15728640);

		/// Get the condition data type
		/// @return the condition data type
		/// @see org.apache.poi.hssf.util.HSSFDataValidation utility class
		public int DataType
		{
			get
			{
				return opt_data_type.GetValue(_option_flags);
			}
			set
			{
				_option_flags = opt_data_type.SetValue(_option_flags, value);
			}
		}

		/// Get the condition error style
		/// @return the condition error style
		/// @see org.apache.poi.hssf.util.HSSFDataValidation utility class
		public int ErrorStyle
		{
			get
			{
				return opt_error_style.GetValue(_option_flags);
			}
			set
			{
				_option_flags = opt_error_style.SetValue(_option_flags, value);
			}
		}

		/// return true if in list validations the string list Is explicitly given in the formula, false otherwise
		/// @return true if in list validations the string list Is explicitly given in the formula, false otherwise
		/// @see org.apache.poi.hssf.util.HSSFDataValidation utility class
		public bool ListExplicitFormula
		{
			get
			{
				return opt_string_list_formula.IsSet(_option_flags);
			}
			set
			{
				_option_flags = opt_string_list_formula.SetBoolean(_option_flags, value);
			}
		}

		/// return true if empty values are allowed in cells, false otherwise
		/// @return if empty values are allowed in cells, false otherwise
		/// @see org.apache.poi.hssf.util.HSSFDataValidation utility class
		public bool EmptyCellAllowed
		{
			get
			{
				return opt_empty_cell_allowed.IsSet(_option_flags);
			}
			set
			{
				_option_flags = opt_empty_cell_allowed.SetBoolean(_option_flags, value);
			}
		}

		/// return true if a prompt window should appear when cell Is selected, false otherwise
		/// @return if a prompt window should appear when cell Is selected, false otherwise
		/// @see org.apache.poi.hssf.util.HSSFDataValidation utility class
		public bool ShowPromptOnCellSelected => opt_show_prompt_on_cell_selected.IsSet(_option_flags);

		/// return true if an error window should appear when an invalid value Is entered in the cell, false otherwise
		/// @return if an error window should appear when an invalid value Is entered in the cell, false otherwise
		/// @see org.apache.poi.hssf.util.HSSFDataValidation utility class
		public bool ShowErrorOnInvalidValue
		{
			get
			{
				return opt_show_error_on_invalid_value.IsSet(_option_flags);
			}
			set
			{
				_option_flags = opt_show_error_on_invalid_value.SetBoolean(_option_flags, value);
			}
		}

		/// Get the condition operator
		/// @return the condition operator
		/// @see org.apache.poi.hssf.util.HSSFDataValidation utility class
		public int ConditionOperator
		{
			get
			{
				return opt_condition_operator.GetValue(_option_flags);
			}
			set
			{
				_option_flags = opt_condition_operator.SetValue(_option_flags, value);
			}
		}

		public CellRangeAddressList CellRangeAddress
		{
			get
			{
				return _regions;
			}
			set
			{
				_regions = value;
			}
		}

		/// Gets the option flags field.
		/// @return options - the option flags field
		public int OptionFlags => _option_flags;

		protected override int DataSize
		{
			get
			{
				int num = 12;
				num += GetUnicodeStringSize(_promptTitle);
				num += GetUnicodeStringSize(_errorTitle);
				num += GetUnicodeStringSize(_promptText);
				num += GetUnicodeStringSize(_errorText);
				num += _formula1.EncodedTokenSize;
				num += _formula2.EncodedTokenSize;
				return num + _regions.Size;
			}
		}

		public override short Sid => 446;

		public DVRecord()
		{
		}

		public DVRecord(int validationType, int operator1, int errorStyle, bool emptyCellAllowed, bool suppressDropDownArrow, bool isExplicitList, bool showPromptBox, string promptTitle, string promptText, bool showErrorBox, string errorTitle, string errorText, Ptg[] formula1, Ptg[] formula2, CellRangeAddressList regions)
		{
			int holder = 0;
			holder = opt_data_type.SetValue(holder, validationType);
			holder = opt_condition_operator.SetValue(holder, operator1);
			holder = opt_error_style.SetValue(holder, errorStyle);
			holder = opt_empty_cell_allowed.SetBoolean(holder, emptyCellAllowed);
			holder = opt_suppress_dropdown_arrow.SetBoolean(holder, suppressDropDownArrow);
			holder = opt_string_list_formula.SetBoolean(holder, isExplicitList);
			holder = opt_show_prompt_on_cell_selected.SetBoolean(holder, showPromptBox);
			holder = (_option_flags = opt_show_error_on_invalid_value.SetBoolean(holder, showErrorBox));
			_promptTitle = ResolveTitleText(promptTitle);
			_promptText = ResolveTitleText(promptText);
			_errorTitle = ResolveTitleText(errorTitle);
			_errorText = ResolveTitleText(errorText);
			_formula1 = Formula.Create(formula1);
			_formula2 = Formula.Create(formula2);
			_regions = regions;
		}

		/// Constructs a DV record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public DVRecord(RecordInputStream in1)
		{
			_option_flags = in1.ReadInt();
			_promptTitle = ReadUnicodeString(in1);
			_errorTitle = ReadUnicodeString(in1);
			_promptText = ReadUnicodeString(in1);
			_errorText = ReadUnicodeString(in1);
			int encodedTokenLen = in1.ReadUShort();
			_not_used_1 = in1.ReadShort();
			_formula1 = Formula.Read(encodedTokenLen, in1);
			int encodedTokenLen2 = in1.ReadUShort();
			_not_used_2 = in1.ReadShort();
			_formula2 = Formula.Read(encodedTokenLen2, in1);
			_regions = new CellRangeAddressList(in1);
		}

		/// When entered via the UI, Excel translates empty string into "\0"
		/// While it is possible to encode the title/text as empty string (Excel doesn't exactly crash),
		/// the resulting tool-tip text / message box looks wrong.  It is best to do the same as the 
		/// Excel UI and encode 'not present' as "\0". 
		private static UnicodeString ResolveTitleText(string str)
		{
			if (str == null || str.Length < 1)
			{
				return NULL_TEXT_STRING;
			}
			return new UnicodeString(str);
		}

		private static UnicodeString ReadUnicodeString(RecordInputStream in1)
		{
			return new UnicodeString(in1);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteInt(_option_flags);
			SerializeUnicodeString(_promptTitle, out1);
			SerializeUnicodeString(_errorTitle, out1);
			SerializeUnicodeString(_promptText, out1);
			SerializeUnicodeString(_errorText, out1);
			out1.WriteShort(_formula1.EncodedTokenSize);
			out1.WriteShort(_not_used_1);
			_formula1.SerializeTokens(out1);
			out1.WriteShort(_formula2.EncodedTokenSize);
			out1.WriteShort(_not_used_2);
			_formula2.SerializeTokens(out1);
			_regions.Serialize(out1);
		}

		private static void SerializeUnicodeString(UnicodeString us, ILittleEndianOutput out1)
		{
			StringUtil.WriteUnicodeString(out1, us.String);
		}

		private static int GetUnicodeStringSize(UnicodeString us)
		{
			string @string = us.String;
			return 3 + @string.Length * ((!StringUtil.HasMultibyte(@string)) ? 1 : 2);
		}

		/// Clones the object. Uses serialisation, as the
		///  contents are somewhat complex
		public override object Clone()
		{
			return CloneViaReserialise();
		}
	}
}
