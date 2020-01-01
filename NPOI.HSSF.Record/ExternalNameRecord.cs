using NPOI.SS.Formula;
using NPOI.SS.Formula.Constant;
using NPOI.SS.Formula.PTG;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// EXTERNALNAME<p />
	///
	/// @author Josh Micich
	public class ExternalNameRecord : StandardRecord
	{
		public const short sid = 35;

		private const int OPT_BUILTIN_NAME = 1;

		private const int OPT_AUTOMATIC_LINK = 2;

		private const int OPT_PICTURE_LINK = 4;

		private const int OPT_STD_DOCUMENT_NAME = 8;

		private const int OPT_OLE_LINK = 16;

		private const int OPT_ICONIFIED_PICTURE_LINK = 32768;

		private short field_1_option_flag;

		private short field_2_ixals;

		private short field_3_not_used;

		private string field_4_name;

		private Formula field_5_name_definition;

		/// 'rgoper' / 'Last received results of the DDE link'
		/// (seems to be only applicable to DDE links)<br />
		/// Logically this is a 2-D array, which has been flattened into 1-D array here.
		private object[] _ddeValues;

		/// (logical) number of columns in the {@link #_ddeValues} array
		private int _nColumns;

		/// (logical) number of rows in the {@link #_ddeValues} array
		private int _nRows;

		/// Convenience Function to determine if the name Is a built-in name
		public bool IsBuiltInName => (field_1_option_flag & 1) != 0;

		/// For OLE and DDE, links can be either 'automatic' or 'manual'
		public bool IsAutomaticLink => (field_1_option_flag & 2) != 0;

		/// only for OLE and DDE
		public bool IsPicureLink => (field_1_option_flag & 4) != 0;

		/// DDE links only. If <c>true</c>, this denotes the 'StdDocumentName'
		public bool IsStdDocumentNameIdentifier => (field_1_option_flag & 8) != 0;

		public bool IsOLELink => (field_1_option_flag & 0x10) != 0;

		public bool IsIconifiedPictureLink => (field_1_option_flag & 0x8000) != 0;

		public short Ix
		{
			get
			{
				return field_2_ixals;
			}
			set
			{
				field_2_ixals = value;
			}
		}

		/// @return the standard String representation of this name
		public string Text
		{
			get
			{
				return field_4_name;
			}
			set
			{
				field_4_name = value;
			}
		}

		protected override int DataSize
		{
			get
			{
				int num = 6;
				num += StringUtil.GetEncodedSize(field_4_name) - 1;
				if (!IsOLELink && !IsStdDocumentNameIdentifier)
				{
					if (IsAutomaticLink)
					{
						num += 3;
						num += ConstantValueParser.GetEncodedSize(_ddeValues);
					}
					else
					{
						num += field_5_name_definition.EncodedSize;
					}
				}
				return num;
			}
		}

		private bool HasFormula
		{
			get
			{
				if (IsAutomaticLink)
				{
					return false;
				}
				return true;
			}
		}

		public override short Sid => 35;

		public ExternalNameRecord()
		{
			field_2_ixals = 0;
		}

		public ExternalNameRecord(RecordInputStream in1)
		{
			field_1_option_flag = in1.ReadShort();
			field_2_ixals = in1.ReadShort();
			field_3_not_used = in1.ReadShort();
			int nChars = in1.ReadUByte();
			field_4_name = StringUtil.ReadUnicodeString(in1, nChars);
			if (!IsOLELink && !IsStdDocumentNameIdentifier)
			{
				if (IsAutomaticLink)
				{
					if (in1.Available() > 0)
					{
						int num = in1.ReadUByte() + 1;
						int num2 = in1.ReadShort() + 1;
						int nValues = num2 * num;
						_ddeValues = ConstantValueParser.Parse(in1, nValues);
						_nColumns = num;
						_nRows = num2;
					}
				}
				else
				{
					int encodedTokenLen = in1.ReadUShort();
					field_5_name_definition = Formula.Read(encodedTokenLen, in1);
				}
			}
		}

		public Ptg[] GetParsedExpression()
		{
			return Formula.GetTokens(field_5_name_definition);
		}

		public void SetParsedExpression(Ptg[] ptgs)
		{
			field_5_name_definition = Formula.Create(ptgs);
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_option_flag);
			out1.WriteShort(field_2_ixals);
			out1.WriteShort(field_3_not_used);
			out1.WriteByte(field_4_name.Length);
			StringUtil.WriteUnicodeStringFlagAndData(out1, field_4_name);
			if (!IsOLELink && !IsStdDocumentNameIdentifier)
			{
				if (IsAutomaticLink)
				{
					out1.WriteByte(_nColumns - 1);
					out1.WriteShort(_nRows - 1);
					ConstantValueParser.Encode(out1, _ddeValues);
				}
				else
				{
					field_5_name_definition.Serialize(out1);
				}
			}
		}

		private Exception ReadFail(string msg)
		{
			string message = msg + " fields: (option=" + field_1_option_flag + " index=" + field_2_ixals + " not_used=" + field_3_not_used + " name='" + field_4_name + "')";
			return new Exception(message);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[EXTERNALNAME]\n");
			stringBuilder.Append("    .options      = ").Append(field_1_option_flag).Append("\n");
			stringBuilder.Append("    .ix      = ").Append(field_2_ixals).Append("\n");
			stringBuilder.Append("    .name    = ").Append(field_4_name).Append("\n");
			if (field_5_name_definition != null)
			{
				Ptg[] tokens = field_5_name_definition.Tokens;
				foreach (Ptg ptg in tokens)
				{
					stringBuilder.Append(ptg.ToString()).Append(ptg.RVAType).Append("\n");
				}
			}
			stringBuilder.Append("[/EXTERNALNAME]\n");
			return stringBuilder.ToString();
		}
	}
}
