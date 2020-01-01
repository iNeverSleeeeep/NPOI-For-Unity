using NPOI.Util;
using System;

namespace NPOI.HSSF.Record
{
	/// Common header/footer base class
	///
	/// @author Josh Micich
	public abstract class HeaderFooterBase : StandardRecord
	{
		private bool field_2_hasMultibyte;

		private string field_3_text;

		/// get the length of the footer string
		///
		/// @return length of the footer string
		private int TextLength => field_3_text.Length;

		public string Text
		{
			get
			{
				return field_3_text;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("text must not be null");
				}
				field_2_hasMultibyte = StringUtil.HasMultibyte(value);
				field_3_text = value;
				if (DataSize > 8224)
				{
					throw new ArgumentException("Header/Footer string too long (limit is " + (short)8224 + " bytes)");
				}
			}
		}

		protected override int DataSize
		{
			get
			{
				if (TextLength < 1)
				{
					return 0;
				}
				return 3 + TextLength * ((!field_2_hasMultibyte) ? 1 : 2);
			}
		}

		protected HeaderFooterBase(string text)
		{
			Text = text;
		}

		protected HeaderFooterBase(RecordInputStream in1)
		{
			if (in1.Remaining > 0)
			{
				int requestedLength = in1.ReadShort();
				field_2_hasMultibyte = (in1.ReadByte() != 0);
				if (field_2_hasMultibyte)
				{
					field_3_text = in1.ReadUnicodeLEString(requestedLength);
				}
				else
				{
					field_3_text = in1.ReadCompressedUnicode(requestedLength);
				}
			}
			else
			{
				field_3_text = "";
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			if (TextLength > 0)
			{
				out1.WriteShort(TextLength);
				out1.WriteByte(field_2_hasMultibyte ? 1 : 0);
				if (field_2_hasMultibyte)
				{
					StringUtil.PutUnicodeLE(field_3_text, out1);
				}
				else
				{
					StringUtil.PutCompressedUnicode(field_3_text, out1);
				}
			}
		}
	}
}
