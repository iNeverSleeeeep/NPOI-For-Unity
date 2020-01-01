using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Common class for HSSFHeader and HSSFFooter
	/// </summary>
	public abstract class HeaderFooter : IHeaderFooter
	{
		/// <summary>
		/// Represents a special field in a header or footer,
		/// eg the page number
		/// </summary>
		public class Field
		{
			/// The character sequence that marks this field 
			public string sequence;

			[Obsolete("Use the generic list Fields.AllFields instead.")]
			public static ArrayList ALL_FIELDS
			{
				get
				{
					return new ArrayList(Fields.AllFields);
				}
			}

			public Field(Fields fields, string sequence)
			{
				this.sequence = sequence;
				fields.Add(this);
			}
		}

		/// <summary>
		/// A special field that normally comes in a pair, eg
		/// turn on underline / turn off underline
		/// </summary>
		public class PairField : Field
		{
			public PairField(Fields fields, string sequence)
				: base(fields, sequence)
			{
			}
		}

		public class Fields
		{
			private List<Field> allFields = new List<Field>();

			private Field _sheetnamefield;

			private Field _filefield;

			private Field _fullfilefield;

			private Field _pagefield;

			private Field _datefield;

			private Field _timefield;

			private Field _numpagesfield;

			private Field _picturefield;

			private PairField _boldfield;

			private PairField _italicfield;

			private PairField _strikethroughfield;

			private PairField _subscriptfield;

			private PairField _superscriptfield;

			private PairField _underlinefield;

			private PairField _doubleunderlinefield;

			/// <summary>
			/// Instance to this class.
			/// </summary>
			private static readonly Fields instance;

			public static ReadOnlyCollection<Field> AllFields => Instance.allFields.AsReadOnly();

			public Field SHEET_NAME_FIELD => _sheetnamefield;

			public Field DATE_FIELD => _datefield;

			public Field FILE_FIELD => _filefield;

			public Field FULL_FILE_FIELD => _fullfilefield;

			public Field PAGE_FIELD => _pagefield;

			public Field TIME_FIELD => _timefield;

			public Field NUM_PAGES_FIELD => _numpagesfield;

			public Field PICTURE_FIELD => _picturefield;

			public PairField BOLD_FIELD => _boldfield;

			public PairField ITALIC_FIELD => _italicfield;

			public PairField STRIKETHROUGH_FIELD => _strikethroughfield;

			public PairField SUBSCRIPT_FIELD => _subscriptfield;

			public PairField SUPERSCRIPT_FIELD => _superscriptfield;

			public PairField UNDERLINE_FIELD => _underlinefield;

			public PairField DOUBLE_UNDERLINE_FIELD => _doubleunderlinefield;

			/// <summary>
			/// Accessing the initialized instance.
			/// </summary>
			public static Fields Instance => instance;

			/// <summary>
			/// Explicit static constructor to tell C# compiler not to mark type as beforefieldinit.
			/// </summary>
			static Fields()
			{
				instance = new Fields();
			}

			/// <summary>
			/// Initialize AllFields.
			/// </summary>
			private Fields()
			{
				_sheetnamefield = new Field(this, "&A");
				_datefield = new Field(this, "&D");
				_filefield = new Field(this, "&F");
				_fullfilefield = new Field(this, "&Z");
				_pagefield = new Field(this, "&P");
				_timefield = new Field(this, "&T");
				_numpagesfield = new Field(this, "&N");
				_picturefield = new Field(this, "&G");
				_boldfield = new PairField(this, "&B");
				_italicfield = new PairField(this, "&I");
				_strikethroughfield = new PairField(this, "&S");
				_subscriptfield = new PairField(this, "&Y");
				_superscriptfield = new PairField(this, "&X");
				_underlinefield = new PairField(this, "&U");
				_doubleunderlinefield = new PairField(this, "&E");
			}

			internal void Add(Field field)
			{
				allFields.Add(field);
			}
		}

		protected bool stripFields;

		/// @return the internal text representation (combining center, left and right parts).
		/// Possibly empty string if no header or footer is set.  Never <c>null</c>.
		public abstract string RawText
		{
			get;
		}

		/// <summary>
		/// Get the left side of the header or footer.
		/// </summary>
		/// <value>The string representing the left side.</value>
		public string Left
		{
			get
			{
				return SplitParts()[0];
			}
			set
			{
				UpdatePart(0, value);
			}
		}

		/// <summary>
		/// Get the center of the header or footer.
		/// </summary>
		/// <value>The string representing the center.</value>
		public string Center
		{
			get
			{
				return SplitParts()[1];
			}
			set
			{
				UpdatePart(1, value);
			}
		}

		/// <summary>
		/// Get the right side of the header or footer.
		/// </summary>
		/// <value>The string representing the right side..</value>
		public string Right
		{
			get
			{
				return SplitParts()[2];
			}
			set
			{
				UpdatePart(2, value);
			}
		}

		/// <summary>
		/// Returns the string representing the current page number
		/// </summary>
		/// <value>The special string for page number.</value>
		public static string Page => PAGE_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the number of pages.
		/// </summary>
		/// <value>The special string for the number of pages.</value>
		public static string NumPages => NUM_PAGES_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the current date
		/// </summary>
		/// <value>The special string for the date</value>
		public static string Date => DATE_FIELD.sequence;

		/// <summary>
		/// Gets the time.
		/// </summary>
		/// <value>The time.</value>
		/// Returns the string representing the current time
		/// @return The special string for the time
		public static string Time => TIME_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the current file name
		/// </summary>
		/// <value>The special string for the file name.</value>
		public static string File => FILE_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the current tab (sheet) name
		/// </summary>
		/// <value>The special string for tab name.</value>
		public static string Tab => SHEET_NAME_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the start bold
		/// </summary>
		/// <returns>The special string for start bold</returns>
		public static string StartBold => BOLD_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the end bold
		/// </summary>
		/// <value>The special string for end bold.</value>
		public static string EndBold => BOLD_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the start underline
		/// </summary>
		/// <value>The special string for start underline.</value>
		public static string StartUnderline => UNDERLINE_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the end underline
		/// </summary>
		/// <value>The special string for end underline.</value>
		public static string EndUnderline => UNDERLINE_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the start double underline
		/// </summary>
		/// <value>The special string for start double underline.</value>
		public static string StartDoubleUnderline => DOUBLE_UNDERLINE_FIELD.sequence;

		/// <summary>
		/// Returns the string representing the end double underline
		/// </summary>
		/// <value>The special string for end double underline.</value>
		public static string EndDoubleUnderline => DOUBLE_UNDERLINE_FIELD.sequence;

		/// <summary>
		/// Are fields currently being Stripped from
		/// the text that this {@link HeaderStories} returns?
		/// Default is false, but can be changed
		/// </summary>
		/// <value><c>true</c> if [are fields stripped]; otherwise, <c>false</c>.</value>
		public bool AreFieldsStripped
		{
			get
			{
				return stripFields;
			}
			set
			{
				stripFields = value;
			}
		}

		internal static Field SHEET_NAME_FIELD => Fields.Instance.SHEET_NAME_FIELD;

		internal static Field DATE_FIELD => Fields.Instance.DATE_FIELD;

		internal static Field FILE_FIELD => Fields.Instance.FILE_FIELD;

		public static Field FULL_FILE_FIELD => Fields.Instance.FULL_FILE_FIELD;

		internal static Field PAGE_FIELD => Fields.Instance.PAGE_FIELD;

		internal static Field TIME_FIELD => Fields.Instance.TIME_FIELD;

		internal static Field NUM_PAGES_FIELD => Fields.Instance.NUM_PAGES_FIELD;

		public static Field PICTURE_FIELD => Fields.Instance.PICTURE_FIELD;

		internal static PairField BOLD_FIELD => Fields.Instance.BOLD_FIELD;

		public static PairField ITALIC_FIELD => Fields.Instance.ITALIC_FIELD;

		public static PairField STRIKETHROUGH_FIELD => Fields.Instance.STRIKETHROUGH_FIELD;

		public static PairField SUBSCRIPT_FIELD => Fields.Instance.SUBSCRIPT_FIELD;

		public static PairField SUPERSCRIPT_FIELD => Fields.Instance.SUPERSCRIPT_FIELD;

		internal static PairField UNDERLINE_FIELD => Fields.Instance.UNDERLINE_FIELD;

		internal static PairField DOUBLE_UNDERLINE_FIELD => Fields.Instance.DOUBLE_UNDERLINE_FIELD;

		private string[] SplitParts()
		{
			string text = RawText;
			string text2 = "";
			string text3 = "";
			string text4 = "";
			while (text.Length > 1)
			{
				if (text[0] != '&')
				{
					text3 = text;
					break;
				}
				int num = text.Length;
				switch (text[1])
				{
				case 'L':
					if (text.IndexOf("&C", StringComparison.Ordinal) >= 0)
					{
						num = Math.Min(num, text.IndexOf("&C", StringComparison.Ordinal));
					}
					if (text.IndexOf("&R", StringComparison.Ordinal) >= 0)
					{
						num = Math.Min(num, text.IndexOf("&R", StringComparison.Ordinal));
					}
					text2 = text.Substring(2, num - 2);
					text = text.Substring(num);
					break;
				case 'C':
					if (text.IndexOf("&L", StringComparison.Ordinal) >= 0)
					{
						num = Math.Min(num, text.IndexOf("&L", StringComparison.Ordinal));
					}
					if (text.IndexOf("&R", StringComparison.Ordinal) >= 0)
					{
						num = Math.Min(num, text.IndexOf("&R", StringComparison.Ordinal));
					}
					text3 = text.Substring(2, num - 2);
					text = text.Substring(num);
					break;
				case 'R':
					if (text.IndexOf("&C", StringComparison.Ordinal) >= 0)
					{
						num = Math.Min(num, text.IndexOf("&C", StringComparison.Ordinal));
					}
					if (text.IndexOf("&L", StringComparison.Ordinal) >= 0)
					{
						num = Math.Min(num, text.IndexOf("&L", StringComparison.Ordinal));
					}
					text4 = text.Substring(2, num - 2);
					text = text.Substring(num);
					break;
				default:
					text3 = text;
					break;
				}
			}
			return new string[3]
			{
				text2,
				text3,
				text4
			};
		}

		private void UpdatePart(int partIndex, string newValue)
		{
			string[] array = SplitParts();
			array[partIndex] = ((newValue == null) ? "" : newValue);
			UpdateHeaderFooterText(array);
		}

		/// <summary>
		/// Creates the complete footer string based on the left, center, and middle
		/// strings.
		/// </summary>
		/// <param name="parts">The parts.</param>
		private void UpdateHeaderFooterText(string[] parts)
		{
			string text = parts[0];
			string text2 = parts[1];
			string text3 = parts[2];
			if (text2.Length < 1 && text.Length < 1 && text3.Length < 1)
			{
				SetHeaderFooterText(string.Empty);
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder(64);
				stringBuilder.Append("&C");
				stringBuilder.Append(text2);
				stringBuilder.Append("&L");
				stringBuilder.Append(text);
				stringBuilder.Append("&R");
				stringBuilder.Append(text3);
				string headerFooterText = stringBuilder.ToString();
				SetHeaderFooterText(headerFooterText);
			}
		}

		/// <summary>
		/// Sets the header footer text.
		/// </summary>
		/// <param name="text">the new header footer text (contains mark-up tags). Possibly
		/// empty string never </param>
		protected abstract void SetHeaderFooterText(string text);

		/// <summary>
		/// Returns the string that represents the change in font size.
		/// </summary>
		/// <param name="size">the new font size.</param>
		/// <returns>The special string to represent a new font size</returns>
		public static string FontSize(short size)
		{
			return "&" + size;
		}

		/// <summary>
		/// Returns the string that represents the change in font.
		/// </summary>
		/// <param name="font">the new font.</param>
		/// <param name="style">the fonts style, one of regular, italic, bold, italic bold or bold italic.</param>
		/// <returns>The special string to represent a new font size</returns>
		public static string Font(string font, string style)
		{
			return "&\"" + font + "," + style + "\"";
		}

		/// <summary>
		/// Removes any fields (eg macros, page markers etc)
		/// from the string.
		/// Normally used to make some text suitable for showing
		/// to humans, and the resultant text should not normally
		/// be saved back into the document!
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static string StripFields(string text)
		{
			if (text == null || text.Length == 0)
			{
				return text;
			}
			foreach (Field allField in Fields.AllFields)
			{
				string sequence = allField.sequence;
				int num;
				while ((num = text.IndexOf(sequence, StringComparison.CurrentCulture)) > -1)
				{
					text = text.Substring(0, num) + text.Substring(num + sequence.Length);
				}
			}
			text = Regex.Replace(text, "\\&\\d+", "");
			text = Regex.Replace(text, "\\&\".*?,.*?\"", "");
			return text;
		}
	}
}
