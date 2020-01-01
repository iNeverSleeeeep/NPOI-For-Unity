using NPOI.HSSF.Model;
using NPOI.HSSF.Record;
using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.HSSF.UserModel
{
	/// <summary>
	/// Rich text Unicode string.  These strings can have fonts applied to
	/// arbitary parts of the string.
	/// @author Glen Stampoultzis (glens at apache.org)
	/// @author Jason Height (jheight at apache.org)
	/// </summary> 
	[Serializable]
	public class HSSFRichTextString : IComparable<HSSFRichTextString>, IRichTextString
	{
		/// Place holder for indicating that NO_FONT has been applied here 
		public const short NO_FONT = 0;

		[NonSerialized]
		private UnicodeString _string;

		private InternalWorkbook _book;

		private LabelSSTRecord _record;

		/// <summary>
		/// Returns the plain string representation.
		/// </summary>
		/// <value>The string.</value>
		public string String => _string.String;

		/// <summary>
		/// Returns the raw, probably shared Unicode String.
		/// Used when tweaking the styles, eg updating font
		/// positions.
		/// Changes to this string may well effect
		/// other RichTextStrings too!
		/// </summary>
		/// <value>The raw unicode string.</value>
		public UnicodeString RawUnicodeString => _string;

		/// <summary>
		/// Gets or sets the unicode string.
		/// </summary>
		/// <value>The unicode string.</value>
		public UnicodeString UnicodeString
		{
			get
			{
				return CloneStringIfRequired();
			}
			set
			{
				_string = value;
			}
		}

		/// <summary>
		/// Gets the number of Chars in the font..
		/// </summary>
		/// <value>The length.</value>
		public int Length => _string.CharCount;

		/// <summary>
		/// Gets the number of formatting runs used. There will always be at
		/// least one of font NO_FONT.
		/// </summary>
		/// <value>The num formatting runs.</value>
		public int NumFormattingRuns => _string.FormatRunCount;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFRichTextString" /> class.
		/// </summary>
		public HSSFRichTextString()
			: this("")
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFRichTextString" /> class.
		/// </summary>
		/// <param name="str">The string.</param>
		public HSSFRichTextString(string str)
		{
			if (str == null)
			{
				_string = new UnicodeString("");
			}
			else
			{
				_string = new UnicodeString(str);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HSSF.UserModel.HSSFRichTextString" /> class.
		/// </summary>
		/// <param name="book">The workbook.</param>
		/// <param name="record">The record.</param>
		public HSSFRichTextString(InternalWorkbook book, LabelSSTRecord record)
		{
			SetWorkbookReferences(book, record);
			_string = book.GetSSTString(record.SSTIndex);
		}

		/// <summary>
		/// This must be called to Setup the internal work book references whenever
		/// a RichTextString Is Added to a cell
		/// </summary>
		/// <param name="book">The workbook.</param>
		/// <param name="record">The record.</param>
		public void SetWorkbookReferences(InternalWorkbook book, LabelSSTRecord record)
		{
			_book = book;
			_record = record;
		}

		/// <summary>
		/// Called whenever the Unicode string Is modified. When it Is modified
		/// we need to Create a new SST index, so that other LabelSSTRecords will not
		/// be affected by Changes tat we make to this string.
		/// </summary>
		/// <returns></returns>
		private UnicodeString CloneStringIfRequired()
		{
			if (_book == null)
			{
				return _string;
			}
			return (UnicodeString)_string.Clone();
		}

		/// <summary>
		/// Adds to SST if required.
		/// </summary>
		private void AddToSSTIfRequired()
		{
			if (_book != null)
			{
				int num = _book.AddSSTString(_string);
				_record.SSTIndex = num;
				_string = _book.GetSSTString(num);
			}
		}

		/// <summary>
		/// Applies a font to the specified Chars of a string.
		/// </summary>
		/// <param name="startIndex">The start index to apply the font to (inclusive).</param>
		/// <param name="endIndex">The end index to apply the font to (exclusive).</param>
		/// <param name="fontIndex">The font to use.</param>
		public void ApplyFont(int startIndex, int endIndex, short fontIndex)
		{
			if (startIndex > endIndex)
			{
				throw new ArgumentException("Start index must be less than end index.");
			}
			if (startIndex < 0 || endIndex > Length)
			{
				throw new ArgumentException("Start and end index not in range.");
			}
			if (startIndex != endIndex)
			{
				short fontIndex2 = 0;
				if (endIndex != Length)
				{
					fontIndex2 = GetFontAtIndex(endIndex);
				}
				_string = CloneStringIfRequired();
				List<UnicodeString.FormatRun> list = _string.FormatIterator();
				ArrayList arrayList = new ArrayList();
				if (list != null)
				{
					IEnumerator enumerator = list.GetEnumerator();
					while (enumerator.MoveNext())
					{
						UnicodeString.FormatRun formatRun = (UnicodeString.FormatRun)enumerator.Current;
						if (formatRun.CharacterPos >= startIndex && formatRun.CharacterPos < endIndex)
						{
							arrayList.Add(formatRun);
						}
					}
				}
				foreach (UnicodeString.FormatRun item in arrayList)
				{
					_string.RemoveFormatRun(item);
				}
				_string.AddFormatRun(new UnicodeString.FormatRun((short)startIndex, fontIndex));
				if (endIndex != Length)
				{
					_string.AddFormatRun(new UnicodeString.FormatRun((short)endIndex, fontIndex2));
				}
				AddToSSTIfRequired();
			}
		}

		/// <summary>
		/// Applies a font to the specified Chars of a string.
		/// </summary>
		/// <param name="startIndex">The start index to apply the font to (inclusive).</param>
		/// <param name="endIndex"> The end index to apply to font to (exclusive).</param>
		/// <param name="font">The index of the font to use.</param>
		public void ApplyFont(int startIndex, int endIndex, IFont font)
		{
			ApplyFont(startIndex, endIndex, font.Index);
		}

		/// <summary>
		/// Sets the font of the entire string.
		/// </summary>
		/// <param name="font">The font to use.</param>
		public void ApplyFont(IFont font)
		{
			ApplyFont(0, _string.CharCount, font);
		}

		/// <summary>
		/// Removes any formatting that may have been applied to the string.
		/// </summary>
		public void ClearFormatting()
		{
			_string = CloneStringIfRequired();
			_string.ClearFormatting();
			AddToSSTIfRequired();
		}

		/// <summary>
		/// Returns the font in use at a particular index.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns>The font that's currently being applied at that
		/// index or null if no font Is being applied or the
		/// index Is out of range.</returns>
		public short GetFontAtIndex(int index)
		{
			int formatRunCount = _string.FormatRunCount;
			UnicodeString.FormatRun formatRun = null;
			for (int i = 0; i < formatRunCount; i++)
			{
				UnicodeString.FormatRun formatRun2 = _string.GetFormatRun(i);
				if (formatRun2.CharacterPos > index)
				{
					break;
				}
				formatRun = formatRun2;
			}
			return formatRun?.FontIndex ?? 0;
		}

		/// <summary>
		/// The index within the string to which the specified formatting run applies.
		/// </summary>
		/// <param name="index">the index of the formatting run</param>
		/// <returns>the index within the string.</returns>
		public int GetIndexOfFormattingRun(int index)
		{
			UnicodeString.FormatRun formatRun = _string.GetFormatRun(index);
			return formatRun.CharacterPos;
		}

		/// <summary>
		/// Gets the font used in a particular formatting run.
		/// </summary>
		/// <param name="index">the index of the formatting run.</param>
		/// <returns>the font number used.</returns>
		public short GetFontOfFormattingRun(int index)
		{
			UnicodeString.FormatRun formatRun = _string.GetFormatRun(index);
			return formatRun.FontIndex;
		}

		/// <summary>
		/// Compares one rich text string to another.
		/// </summary>
		/// <param name="other">The other rich text string.</param>
		/// <returns></returns>
		public int CompareTo(HSSFRichTextString other)
		{
			return _string.CompareTo(other._string);
		}

		/// <summary>
		/// Equalses the specified o.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns></returns>
		public override bool Equals(object o)
		{
			if (o is HSSFRichTextString)
			{
				return _string.Equals(((HSSFRichTextString)o).String);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return _string.GetHashCode();
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			return _string.ToString();
		}

		/// <summary>
		/// Applies the specified font to the entire string.
		/// </summary>
		/// <param name="fontIndex">Index of the font to apply.</param>
		public void ApplyFont(short fontIndex)
		{
			ApplyFont(0, _string.CharCount, fontIndex);
		}
	}
}
