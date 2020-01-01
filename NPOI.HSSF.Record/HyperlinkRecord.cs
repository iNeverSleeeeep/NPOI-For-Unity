using NPOI.HSSF.Util;
using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// The <c>HyperlinkRecord</c> wraps an HLINK-record 
	///  from the Excel-97 format.
	/// Supports only external links for now (eg http://) 
	///
	/// @author      Mark Hissink Muller <a href="mailto:mark@hissinkmuller.nl">mark@hissinkmuller.nl</a>
	/// @author      Yegor Kozlov (yegor at apache dot org)
	public class HyperlinkRecord : StandardRecord
	{
		/// Link flags
		public const int HLINK_URL = 1;

		public const int HLINK_ABS = 2;

		public const int HLINK_LABEL = 20;

		public const int HLINK_PLACE = 8;

		private const int HLINK_TARGET_FRAME = 128;

		private const int HLINK_UNC_PATH = 256;

		public const short sid = 440;

		private static POILogger logger = POILogFactory.GetLogger(typeof(HyperlinkRecord));

		public static readonly GUID STD_MONIKER = GUID.Parse("79EAC9D0-BAF9-11CE-8C82-00AA004BA90B");

		public static readonly GUID URL_MONIKER = GUID.Parse("79EAC9E0-BAF9-11CE-8C82-00AA004BA90B");

		public static readonly GUID FILE_MONIKER = GUID.Parse("00000303-0000-0000-C000-000000000046");

		/// Tail of a URL link
		public static readonly byte[] URL_uninterpretedTail = HexRead.ReadFromString("79 58 81 F4  3B 1D 7F 48   AF 2C 82 5D  C4 85 27 63   00 00 00 00  A5 AB 00 00");

		/// Tail of a file link
		public static readonly byte[] FILE_uninterpretedTail = HexRead.ReadFromString("FF FF AD DE  00 00 00 00   00 00 00 00  00 00 00 00   00 00 00 00  00 00 00 00");

		private static readonly int TAIL_SIZE = FILE_uninterpretedTail.Length;

		/// cell range of this hyperlink 
		private CellRangeAddress _range;

		/// 16-byte GUID
		private GUID _guid;

		/// Some sort of options for file links.
		private short _fileOpts;

		/// Link options. Can include any of HLINK_* flags.
		private int _linkOpts;

		/// Test label
		private string _label = string.Empty;

		private string _targetFrame = string.Empty;

		/// Moniker. Makes sense only for URL and file links
		private GUID _moniker;

		/// in 8:3 DOS format No Unicode string header,
		/// always 8-bit characters, zero-terminated 
		private string _shortFilename = string.Empty;

		/// Link 
		private string _address = string.Empty;

		/// Text describing a place in document.  In Excel UI, this is appended to the
		/// address, (after a '#' delimiter).<br />
		/// This field is optional.  If present, the {@link #HLINK_PLACE} must be set.
		private string _textMark = string.Empty;

		/// Remaining bytes
		private byte[] _uninterpretedTail;

		/// Return the column of the first cell that Contains the hyperlink
		///
		/// @return the 0-based column of the first cell that Contains the hyperlink
		public int FirstColumn
		{
			get
			{
				return _range.FirstColumn;
			}
			set
			{
				_range.FirstColumn = value;
			}
		}

		/// Set the column of the last cell that Contains the hyperlink
		///
		/// @return the 0-based column of the last cell that Contains the hyperlink
		public int LastColumn
		{
			get
			{
				return _range.LastColumn;
			}
			set
			{
				_range.LastColumn = value;
			}
		}

		/// Return the row of the first cell that Contains the hyperlink
		///
		/// @return the 0-based row of the first cell that Contains the hyperlink
		public int FirstRow
		{
			get
			{
				return _range.FirstRow;
			}
			set
			{
				_range.FirstRow = value;
			}
		}

		/// Return the row of the last cell that Contains the hyperlink
		///
		/// @return the 0-based row of the last cell that Contains the hyperlink
		public int LastRow
		{
			get
			{
				return _range.LastRow;
			}
			set
			{
				_range.LastRow = value;
			}
		}

		/// Returns a 16-byte guid identifier. Seems to always equal {@link STD_MONIKER}
		///
		/// @return 16-byte guid identifier
		public GUID Guid => _guid;

		/// Returns a 16-byte moniker.
		///
		/// @return 16-byte moniker
		public GUID Moniker => _moniker;

		/// Return text label for this hyperlink
		///
		/// @return  text to Display
		public string Label
		{
			get
			{
				return CleanString(_label);
			}
			set
			{
				_label = AppendNullTerm(value);
			}
		}

		/// Hypelink Address. Depending on the hyperlink type it can be URL, e-mail, patrh to a file, etc.
		///
		/// @return  the Address of this hyperlink
		public string Address
		{
			get
			{
				if ((_linkOpts & 1) != 0 && _moniker != null && FILE_MONIKER.Equals(_moniker))
				{
					return CleanString((_address != null) ? _address : _shortFilename);
				}
				if ((_linkOpts & 8) != 0)
				{
					return CleanString(_textMark);
				}
				return CleanString(_address);
			}
			set
			{
				if ((_linkOpts & 1) != 0 && _moniker != null && FILE_MONIKER.Equals(_moniker))
				{
					_shortFilename = AppendNullTerm(value);
				}
				else if ((_linkOpts & 8) != 0)
				{
					_textMark = AppendNullTerm(value);
				}
				else
				{
					_address = AppendNullTerm(value);
				}
			}
		}

		public string TextMark
		{
			get
			{
				return CleanString(_textMark);
			}
			set
			{
				_textMark = AppendNullTerm(value);
			}
		}

		/// Link options. Must be a combination of HLINK_* constants.
		public int LinkOptions => _linkOpts;

		public string TargetFrame => CleanString(_targetFrame);

		public string ShortFilename
		{
			get
			{
				return CleanString(_shortFilename);
			}
			set
			{
				_shortFilename = AppendNullTerm(value);
			}
		}

		/// Label options
		public int LabelOptions => 2;

		/// Options for a file link
		public int FileOptions => _fileOpts;

		public override short Sid => 440;

		protected override int DataSize
		{
			get
			{
				int num = 0;
				num += 8;
				num += 16;
				num += 4;
				num += 4;
				if ((_linkOpts & 0x14) != 0)
				{
					num += 4;
					num += _label.Length * 2;
				}
				if ((_linkOpts & 0x80) != 0)
				{
					num += 4;
					num += _targetFrame.Length * 2;
				}
				if ((_linkOpts & 1) != 0 && (_linkOpts & 0x100) != 0)
				{
					num += 4;
					num += _address.Length * 2;
				}
				if ((_linkOpts & 1) != 0 && (_linkOpts & 0x100) == 0)
				{
					num += 16;
					if (_moniker != null && URL_MONIKER.Equals(_moniker))
					{
						num += 4;
						num += _address.Length * 2;
						if (_uninterpretedTail != null)
						{
							num += TAIL_SIZE;
						}
					}
					else if (_moniker != null && FILE_MONIKER.Equals(_moniker))
					{
						num += 2;
						num += 4;
						num += ((_shortFilename != null) ? _shortFilename.Length : 0);
						num += TAIL_SIZE;
						num += 4;
						if (!string.IsNullOrEmpty(_address))
						{
							num += 6;
							num += _address.Length * 2;
						}
					}
				}
				if ((_linkOpts & 8) != 0)
				{
					num += 4;
					num += _textMark.Length * 2;
				}
				return num;
			}
		}

		/// Create a new hyperlink
		public HyperlinkRecord()
		{
		}

		/// Read hyperlink from input stream
		///
		/// @param in the stream to Read from
		public HyperlinkRecord(RecordInputStream in1)
		{
			_range = new CellRangeAddress(in1);
			_guid = new GUID(in1);
			int num = in1.ReadInt();
			if (num != 2)
			{
				throw new RecordFormatException("Stream Version must be 0x2 but found " + num);
			}
			_linkOpts = in1.ReadInt();
			if ((_linkOpts & 0x14) != 0)
			{
				int requestedLength = in1.ReadInt();
				_label = in1.ReadUnicodeLEString(requestedLength);
			}
			if ((_linkOpts & 0x80) != 0)
			{
				int requestedLength2 = in1.ReadInt();
				_targetFrame = in1.ReadUnicodeLEString(requestedLength2);
			}
			if ((_linkOpts & 1) != 0 && (_linkOpts & 0x100) != 0)
			{
				_moniker = null;
				int requestedLength3 = in1.ReadInt();
				_address = in1.ReadUnicodeLEString(requestedLength3);
			}
			if ((_linkOpts & 1) != 0 && (_linkOpts & 0x100) == 0)
			{
				_moniker = new GUID(in1);
				if (URL_MONIKER.Equals(_moniker))
				{
					int num2 = in1.ReadInt();
					int remaining = in1.Remaining;
					if (num2 == remaining)
					{
						int requestedLength4 = num2 / 2;
						_address = in1.ReadUnicodeLEString(requestedLength4);
					}
					else
					{
						int requestedLength5 = (num2 - TAIL_SIZE) / 2;
						_address = in1.ReadUnicodeLEString(requestedLength5);
						_uninterpretedTail = ReadTail(URL_uninterpretedTail, in1);
					}
				}
				else if (FILE_MONIKER.Equals(_moniker))
				{
					_fileOpts = in1.ReadShort();
					int nChars = in1.ReadInt();
					_shortFilename = StringUtil.ReadCompressedUnicode(in1, nChars);
					_uninterpretedTail = ReadTail(FILE_uninterpretedTail, in1);
					int num3 = in1.ReadInt();
					if (num3 > 0)
					{
						int num4 = in1.ReadInt();
						in1.ReadUShort();
						_address = StringUtil.ReadUnicodeLE(in1, num4 / 2);
					}
					else
					{
						_address = null;
					}
				}
				else if (STD_MONIKER.Equals(_moniker))
				{
					_fileOpts = in1.ReadShort();
					int num5 = in1.ReadInt();
					byte[] array = new byte[num5];
					in1.ReadFully(array);
					_address = Encoding.UTF8.GetString(array);
				}
			}
			if ((_linkOpts & 8) != 0)
			{
				int requestedLength6 = in1.ReadInt();
				_textMark = in1.ReadUnicodeLEString(requestedLength6);
			}
			if (in1.Remaining > 0)
			{
				Console.WriteLine(HexDump.ToHex(in1.ReadRemainder()));
			}
		}

		private static byte[] ReadTail(byte[] expectedTail, ILittleEndianInput in1)
		{
			byte[] array = new byte[TAIL_SIZE];
			in1.ReadFully(array);
			return array;
		}

		private static void WriteTail(byte[] tail, ILittleEndianOutput out1)
		{
			out1.Write(tail);
		}

		private static string CleanString(string s)
		{
			if (s == null)
			{
				return null;
			}
			int num = s.IndexOf('\0');
			if (num < 0)
			{
				return s;
			}
			return s.Substring(0, num);
		}

		private static string AppendNullTerm(string s)
		{
			if (s == null)
			{
				return null;
			}
			return s + '\0';
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			_range.Serialize(out1);
			_guid.Serialize(out1);
			out1.WriteInt(2);
			out1.WriteInt(_linkOpts);
			if ((_linkOpts & 0x14) != 0)
			{
				out1.WriteInt(_label.Length);
				StringUtil.PutUnicodeLE(_label, out1);
			}
			if ((_linkOpts & 0x80) != 0)
			{
				out1.WriteInt(_targetFrame.Length);
				StringUtil.PutUnicodeLE(_targetFrame, out1);
			}
			if ((_linkOpts & 1) != 0 && (_linkOpts & 0x100) != 0)
			{
				out1.WriteInt(_address.Length);
				StringUtil.PutUnicodeLE(_address, out1);
			}
			if ((_linkOpts & 1) != 0 && (_linkOpts & 0x100) == 0)
			{
				_moniker.Serialize(out1);
				if (_moniker != null && URL_MONIKER.Equals(_moniker))
				{
					if (_uninterpretedTail == null)
					{
						out1.WriteInt(_address.Length * 2);
						StringUtil.PutUnicodeLE(_address, out1);
					}
					else
					{
						out1.WriteInt(_address.Length * 2 + TAIL_SIZE);
						StringUtil.PutUnicodeLE(_address, out1);
						WriteTail(_uninterpretedTail, out1);
					}
				}
				else if (_moniker != null && FILE_MONIKER.Equals(_moniker))
				{
					out1.WriteShort(_fileOpts);
					out1.WriteInt(_shortFilename.Length);
					StringUtil.PutCompressedUnicode(_shortFilename, out1);
					WriteTail(_uninterpretedTail, out1);
					if (string.IsNullOrEmpty(_address))
					{
						out1.WriteInt(0);
					}
					else
					{
						int num = _address.Length * 2;
						out1.WriteInt(num + 6);
						out1.WriteInt(num);
						out1.WriteShort(3);
						StringUtil.PutUnicodeLE(_address, out1);
					}
				}
			}
			if ((_linkOpts & 8) != 0)
			{
				out1.WriteInt(_textMark.Length);
				StringUtil.PutUnicodeLE(_textMark, out1);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[HYPERLINK RECORD]\n");
			stringBuilder.Append("    .range            = ").Append(_range.FormatAsString()).Append("\n");
			stringBuilder.Append("    .guid        = ").Append(_guid.FormatAsString()).Append("\n");
			stringBuilder.Append("    .linkOpts          = ").Append(HexDump.IntToHex(_linkOpts)).Append("\n");
			stringBuilder.Append("    .label          = ").Append(Label).Append("\n");
			if ((_linkOpts & 0x80) != 0)
			{
				stringBuilder.Append("    .targetFrame= ").Append(TargetFrame).Append("\n");
			}
			if ((_linkOpts & 1) != 0 && _moniker != null)
			{
				stringBuilder.Append("    .moniker          = ").Append(_moniker.FormatAsString()).Append("\n");
			}
			if ((_linkOpts & 8) != 0)
			{
				stringBuilder.Append("    .targetFrame= ").Append(TextMark).Append("\n");
			}
			stringBuilder.Append("    .address            = ").Append(Address).Append("\n");
			stringBuilder.Append("[/HYPERLINK RECORD]\n");
			return stringBuilder.ToString();
		}

		/// <summary>
		/// Initialize a new url link
		/// </summary>        
		public void CreateUrlLink()
		{
			_range = new CellRangeAddress(0, 0, 0, 0);
			_guid = STD_MONIKER;
			_linkOpts = 23;
			Label = "";
			_moniker = URL_MONIKER;
			Address = "";
			_uninterpretedTail = URL_uninterpretedTail;
		}

		/// <summary>
		/// Initialize a new file link
		/// </summary>
		public void CreateFileLink()
		{
			_range = new CellRangeAddress(0, 0, 0, 0);
			_guid = STD_MONIKER;
			_linkOpts = 21;
			_fileOpts = 0;
			Label = "";
			_moniker = FILE_MONIKER;
			Address = null;
			ShortFilename = "";
			_uninterpretedTail = FILE_uninterpretedTail;
		}

		/// <summary>
		/// Initialize a new document link
		/// </summary>
		public void CreateDocumentLink()
		{
			_range = new CellRangeAddress(0, 0, 0, 0);
			_guid = STD_MONIKER;
			_linkOpts = 28;
			Label = "";
			_moniker = FILE_MONIKER;
			Address = "";
			TextMark = "";
		}

		public override object Clone()
		{
			HyperlinkRecord hyperlinkRecord = new HyperlinkRecord();
			hyperlinkRecord._range = _range.Copy();
			hyperlinkRecord._guid = _guid;
			hyperlinkRecord._linkOpts = _linkOpts;
			hyperlinkRecord._fileOpts = _fileOpts;
			hyperlinkRecord._label = _label;
			hyperlinkRecord._address = _address;
			hyperlinkRecord._moniker = _moniker;
			hyperlinkRecord._shortFilename = _shortFilename;
			hyperlinkRecord._targetFrame = _targetFrame;
			hyperlinkRecord._textMark = _textMark;
			hyperlinkRecord._uninterpretedTail = _uninterpretedTail;
			return hyperlinkRecord;
		}
	}
}
