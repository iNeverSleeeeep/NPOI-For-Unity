using NPOI.HSSF.Record.Cont;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Supports the STRING record structure.
	///
	/// @author Glen Stampoultzis (glens at apache.org)
	[Serializable]
	public class StringRecord : ContinuableRecord
	{
		public const short sid = 519;

		private bool _is16bitUnicode;

		private string _text;

		/// return the non static version of the id for this record.
		public override short Sid => 519;

		/// @return The string represented by this record.
		public string String
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				_is16bitUnicode = StringUtil.HasMultibyte(value);
			}
		}

		public StringRecord()
		{
		}

		/// Constructs a String record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public StringRecord(RecordInputStream in1)
		{
			int requestedLength = in1.ReadShort();
			_is16bitUnicode = (in1.ReadByte() != 0);
			if (_is16bitUnicode)
			{
				_text = in1.ReadUnicodeLEString(requestedLength);
			}
			else
			{
				_text = in1.ReadCompressedUnicode(requestedLength);
			}
		}

		/// called by the class that Is responsible for writing this sucker.
		/// Subclasses should implement this so that their data Is passed back in a
		/// byte array.
		///
		/// @param offset to begin writing at
		/// @param data byte array containing instance data
		/// @return number of bytes written
		protected override void Serialize(ContinuableRecordOutput out1)
		{
			out1.WriteShort(_text.Length);
			out1.WriteStringData(_text);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[STRING]\n");
			stringBuilder.Append("    .string            = ").Append(_text).Append("\n");
			stringBuilder.Append("[/STRING]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			StringRecord stringRecord = new StringRecord();
			stringRecord._is16bitUnicode = _is16bitUnicode;
			stringRecord._text = _text;
			return stringRecord;
		}
	}
}
