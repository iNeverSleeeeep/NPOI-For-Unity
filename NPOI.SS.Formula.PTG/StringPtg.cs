using NPOI.Util;
using System;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// String Stores a String value in a formula value stored in the format
	/// &lt;Length 2 bytes&gt;char[]
	///
	/// @author Werner Froidevaux
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @author Bernard Chesnoy
	public class StringPtg : ScalarConstantPtg
	{
		public const byte sid = 23;

		/// the Char (")used in formulas to delimit string literals 
		private const char FORMULA_DELIMITER = '"';

		private static BitField fHighByte = BitFieldFactory.GetInstance(1);

		/// NOTE: OO doc says 16bit Length, but BiffViewer says 8 Book says something
		/// totally different, so don't look there!
		private int field_1_Length;

		private byte field_2_options;

		private bool _is16bitUnicode;

		private string field_3_string;

		public string Value => field_3_string;

		public override int Size => field_3_string.Length * ((!_is16bitUnicode) ? 1 : 2) + 3;

		/// Create a StringPtg from a stream 
		public StringPtg(ILittleEndianInput in1)
		{
			int nChars = in1.ReadUByte();
			field_2_options = (byte)in1.ReadByte();
			_is16bitUnicode = ((field_2_options & 1) != 0);
			if (_is16bitUnicode)
			{
				field_3_string = StringUtil.ReadUnicodeLE(in1, nChars);
			}
			else
			{
				field_3_string = StringUtil.ReadCompressedUnicode(in1, nChars);
			}
		}

		/// Create a StringPtg from a string representation of the number Number
		/// format Is not Checked, it Is expected to be Validated in the Parser that
		/// calls this method.
		///
		/// @param value :
		///            String representation of a floating point number
		public StringPtg(string value)
		{
			if (value.Length > 255)
			{
				throw new ArgumentException("String literals in formulas can't be bigger than 255 Chars ASCII");
			}
			_is16bitUnicode = StringUtil.HasMultibyte(value);
			field_3_string = value;
			field_1_Length = value.Length;
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(23 + base.PtgClass);
			out1.WriteByte(field_3_string.Length);
			out1.WriteByte(_is16bitUnicode ? 1 : 0);
			if (_is16bitUnicode)
			{
				StringUtil.PutUnicodeLE(field_3_string, out1);
			}
			else
			{
				StringUtil.PutCompressedUnicode(field_3_string, out1);
			}
		}

		public override string ToFormulaString()
		{
			string text = field_3_string;
			int length = text.Length;
			StringBuilder stringBuilder = new StringBuilder(length + 4);
			stringBuilder.Append('"');
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c == '"')
				{
					stringBuilder.Append('"');
				}
				stringBuilder.Append(c);
			}
			stringBuilder.Append('"');
			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			stringBuilder.Append(GetType().Name).Append(" [");
			stringBuilder.Append(field_3_string);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}
	}
}
