using NPOI.SS.Formula.PTG;
using NPOI.Util;
using System;
using System.Globalization;
using System.Text;

namespace NPOI.HSSF.Record
{
	public class LbsDataSubRecord : SubRecord
	{
		public const int sid = 19;

		/// From [MS-XLS].pdf 2.5.147 FtLbsData:
		///
		/// An unsigned integer that indirectly specifies whether
		/// some of the data in this structure appear in a subsequent Continue record.
		/// If _cbFContinued is 0x00, all of the fields in this structure except sid and _cbFContinued
		///  MUST NOT exist. If this entire structure is Contained within the same record,
		/// then _cbFContinued MUST be greater than or equal to the size, in bytes,
		/// of this structure, not including the four bytes for the ft and _cbFContinued fields
		private int _cbFContinued;

		/// a formula that specifies the range of cell values that are the items in this list.
		private int _unknownPreFormulaInt;

		private Ptg _linkPtg;

		private byte? _unknownPostFormulaByte;

		/// An unsigned integer that specifies the number of items in the list.
		private int _cLines;

		/// An unsigned integer that specifies the one-based index of the first selected item in this list.
		/// A value of 0x00 specifies there is no currently selected item.
		private int _iSel;

		/// flags that tell what data follows
		private int _flags;

		/// An ObjId that specifies the edit box associated with this list.
		/// A value of 0x00 specifies that there is no edit box associated with this list.
		private int _idEdit;

		/// An optional LbsDropData that specifies properties for this dropdown control.
		/// This field MUST exist if and only if the Containing Obj?s cmo.ot is equal to 0x14.
		private LbsDropData _dropData;

		/// An optional array of strings where each string specifies an item in the list.
		/// The number of elements in this array, if it exists, MUST be {@link #_cLines}
		private string[] _rgLines;

		/// An optional array of bools that specifies
		/// which items in the list are part of a multiple selection
		private bool[] _bsels;

		public override bool IsTerminating => true;

		/// @return the formula that specifies the range of cell values that are the items in this list.
		public Ptg Formula => _linkPtg;

		/// @return the number of items in the list
		public int NumberOfItems => _cLines;

		public override short Sid => 19;

		public override int DataSize
		{
			get
			{
				int num = 2;
				if (_linkPtg != null)
				{
					num += 2;
					num += 4;
					num += _linkPtg.Size;
					if (((int?)_unknownPostFormulaByte).HasValue)
					{
						num++;
					}
				}
				num += 8;
				if (_dropData != null)
				{
					num += _dropData.DataSize;
				}
				if (_rgLines != null)
				{
					string[] rgLines = _rgLines;
					foreach (string value in rgLines)
					{
						num += StringUtil.GetEncodedSize(value);
					}
				}
				if (_bsels != null)
				{
					num += _bsels.Length;
				}
				return num;
			}
		}

		private LbsDataSubRecord()
		{
		}

		/// @param in the stream to read data from
		/// @param cbFContinued the seconf short in the record header
		/// @param cmoOt the Containing Obj's {@link CommonObjectDataSubRecord#field_1_objectType}
		public LbsDataSubRecord(ILittleEndianInput in1, int cbFContinued, int cmoOt)
		{
			_cbFContinued = cbFContinued;
			int num = in1.ReadUShort();
			if (num > 0)
			{
				int num2 = in1.ReadUShort();
				_unknownPreFormulaInt = in1.ReadInt();
				Ptg[] array = Ptg.ReadTokens(num2, in1);
				if (array.Length != 1)
				{
					throw new RecordFormatException("Read " + array.Length + " tokens but expected exactly 1");
				}
				_linkPtg = array[0];
				switch (num - num2)
				{
				case 7:
					_unknownPostFormulaByte = (byte)in1.ReadByte();
					break;
				case 6:
					_unknownPostFormulaByte = null;
					break;
				default:
					throw new RecordFormatException("Unexpected leftover bytes");
				}
			}
			_cLines = in1.ReadUShort();
			_iSel = in1.ReadUShort();
			_flags = in1.ReadUShort();
			_idEdit = in1.ReadUShort();
			if (cmoOt == 20)
			{
				_dropData = new LbsDropData(in1);
			}
			if ((_flags & 2) != 0)
			{
				_rgLines = new string[_cLines];
				for (int i = 0; i < _cLines; i++)
				{
					_rgLines[i] = StringUtil.ReadUnicodeString(in1);
				}
			}
			if (((_flags >> 4) & 2) != 0)
			{
				_bsels = new bool[_cLines];
				for (int j = 0; j < _cLines; j++)
				{
					_bsels[j] = (in1.ReadByte() == 1);
				}
			}
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(19);
			out1.WriteShort(_cbFContinued);
			if (_linkPtg == null)
			{
				out1.WriteShort(0);
			}
			else
			{
				int size = _linkPtg.Size;
				int num = size + 6;
				if (((int?)_unknownPostFormulaByte).HasValue)
				{
					num++;
				}
				out1.WriteShort(num);
				out1.WriteShort(size);
				out1.WriteInt(_unknownPreFormulaInt);
				_linkPtg.Write(out1);
				if (((int?)_unknownPostFormulaByte).HasValue)
				{
					out1.WriteByte(Convert.ToByte(_unknownPostFormulaByte, CultureInfo.InvariantCulture));
				}
			}
			out1.WriteShort(_cLines);
			out1.WriteShort(_iSel);
			out1.WriteShort(_flags);
			out1.WriteShort(_idEdit);
			if (_dropData != null)
			{
				_dropData.Serialize(out1);
			}
			if (_rgLines != null)
			{
				string[] rgLines = _rgLines;
				foreach (string value in rgLines)
				{
					StringUtil.WriteUnicodeString(out1, value);
				}
			}
			if (_bsels != null)
			{
				bool[] bsels = _bsels;
				for (int j = 0; j < bsels.Length; j++)
				{
					out1.WriteByte(bsels[j] ? 1 : 0);
				}
			}
		}

		private static Ptg ReadRefPtg(byte[] formulaRawBytes)
		{
			ILittleEndianInput littleEndianInput = new LittleEndianByteArrayInputStream(formulaRawBytes);
			switch ((byte)littleEndianInput.ReadByte())
			{
			case 37:
				return new AreaPtg(littleEndianInput);
			case 59:
				return new Area3DPtg(littleEndianInput);
			case 36:
				return new RefPtg(littleEndianInput);
			case 58:
				return new Ref3DPtg(littleEndianInput);
			default:
				return null;
			}
		}

		public override object Clone()
		{
			return this;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(256);
			stringBuilder.Append("[ftLbsData]\n");
			stringBuilder.Append("    .unknownshort1 =").Append(HexDump.ShortToHex(_cbFContinued)).Append("\n");
			stringBuilder.Append("    .formula        = ").Append('\n');
			stringBuilder.Append(_linkPtg.ToString()).Append(_linkPtg.RVAType).Append('\n');
			stringBuilder.Append("    .nEntryCount   =").Append(HexDump.ShortToHex(_cLines)).Append("\n");
			stringBuilder.Append("    .selEntryIx    =").Append(HexDump.ShortToHex(_iSel)).Append("\n");
			stringBuilder.Append("    .style         =").Append(HexDump.ShortToHex(_flags)).Append("\n");
			stringBuilder.Append("    .unknownshort10=").Append(HexDump.ShortToHex(_idEdit)).Append("\n");
			if (_dropData != null)
			{
				stringBuilder.Append('\n').Append(_dropData.ToString());
			}
			stringBuilder.Append("[/ftLbsData]\n");
			return stringBuilder.ToString();
		}

		/// @return a new instance of LbsDataSubRecord to construct auto-filters
		/// @see org.apache.poi.hssf.model.ComboboxShape#createObjRecord(org.apache.poi.hssf.usermodel.HSSFSimpleShape, int)
		public static LbsDataSubRecord CreateAutoFilterInstance()
		{
			LbsDataSubRecord lbsDataSubRecord = new LbsDataSubRecord();
			lbsDataSubRecord._cbFContinued = 8174;
			lbsDataSubRecord._iSel = 0;
			lbsDataSubRecord._flags = 769;
			lbsDataSubRecord._dropData = new LbsDropData();
			lbsDataSubRecord._dropData._wStyle = 2;
			lbsDataSubRecord._dropData._cLine = 8;
			return lbsDataSubRecord;
		}
	}
}
