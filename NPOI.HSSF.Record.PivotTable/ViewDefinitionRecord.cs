using NPOI.Util;
using System.Text;

namespace NPOI.HSSF.Record.PivotTable
{
	/// SXVIEW - View Definition (0x00B0)<br />
	///
	/// @author Patrick Cheng
	public class ViewDefinitionRecord : StandardRecord
	{
		public const short sid = 176;

		private int rwFirst;

		private int rwLast;

		private int colFirst;

		private int colLast;

		private int rwFirstHead;

		private int rwFirstData;

		private int colFirstData;

		private int iCache;

		private int reserved;

		private int sxaxis4Data;

		private int ipos4Data;

		private int cDim;

		private int cDimRw;

		private int cDimCol;

		private int cDimPg;

		private int cDimData;

		private int cRw;

		private int cCol;

		private int grbit;

		private int itblAutoFmt;

		private string dataField;

		private string name;

		protected override int DataSize => 40 + StringUtil.GetEncodedSize(name) + StringUtil.GetEncodedSize(dataField);

		public override short Sid => 176;

		public ViewDefinitionRecord(RecordInputStream in1)
		{
			rwFirst = in1.ReadUShort();
			rwLast = in1.ReadUShort();
			colFirst = in1.ReadUShort();
			colLast = in1.ReadUShort();
			rwFirstHead = in1.ReadUShort();
			rwFirstData = in1.ReadUShort();
			colFirstData = in1.ReadUShort();
			iCache = in1.ReadUShort();
			reserved = in1.ReadUShort();
			sxaxis4Data = in1.ReadUShort();
			ipos4Data = in1.ReadUShort();
			cDim = in1.ReadUShort();
			cDimRw = in1.ReadUShort();
			cDimCol = in1.ReadUShort();
			cDimPg = in1.ReadUShort();
			cDimData = in1.ReadUShort();
			cRw = in1.ReadUShort();
			cCol = in1.ReadUShort();
			grbit = in1.ReadUShort();
			itblAutoFmt = in1.ReadUShort();
			int nChars = in1.ReadUShort();
			int nChars2 = in1.ReadUShort();
			name = StringUtil.ReadUnicodeString(in1, nChars);
			dataField = StringUtil.ReadUnicodeString(in1, nChars2);
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(rwFirst);
			out1.WriteShort(rwLast);
			out1.WriteShort(colFirst);
			out1.WriteShort(colLast);
			out1.WriteShort(rwFirstHead);
			out1.WriteShort(rwFirstData);
			out1.WriteShort(colFirstData);
			out1.WriteShort(iCache);
			out1.WriteShort(reserved);
			out1.WriteShort(sxaxis4Data);
			out1.WriteShort(ipos4Data);
			out1.WriteShort(cDim);
			out1.WriteShort(cDimRw);
			out1.WriteShort(cDimCol);
			out1.WriteShort(cDimPg);
			out1.WriteShort(cDimData);
			out1.WriteShort(cRw);
			out1.WriteShort(cCol);
			out1.WriteShort(grbit);
			out1.WriteShort(itblAutoFmt);
			out1.WriteShort(name.Length);
			out1.WriteShort(dataField.Length);
			StringUtil.WriteUnicodeStringFlagAndData(out1, name);
			StringUtil.WriteUnicodeStringFlagAndData(out1, dataField);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[SXVIEW]\n");
			stringBuilder.Append("    .rwFirst      =").Append(HexDump.ShortToHex(rwFirst)).Append('\n');
			stringBuilder.Append("    .rwLast       =").Append(HexDump.ShortToHex(rwLast)).Append('\n');
			stringBuilder.Append("    .colFirst     =").Append(HexDump.ShortToHex(colFirst)).Append('\n');
			stringBuilder.Append("    .colLast      =").Append(HexDump.ShortToHex(colLast)).Append('\n');
			stringBuilder.Append("    .rwFirstHead  =").Append(HexDump.ShortToHex(rwFirstHead)).Append('\n');
			stringBuilder.Append("    .rwFirstData  =").Append(HexDump.ShortToHex(rwFirstData)).Append('\n');
			stringBuilder.Append("    .colFirstData =").Append(HexDump.ShortToHex(colFirstData)).Append('\n');
			stringBuilder.Append("    .iCache       =").Append(HexDump.ShortToHex(iCache)).Append('\n');
			stringBuilder.Append("    .reserved     =").Append(HexDump.ShortToHex(reserved)).Append('\n');
			stringBuilder.Append("    .sxaxis4Data  =").Append(HexDump.ShortToHex(sxaxis4Data)).Append('\n');
			stringBuilder.Append("    .ipos4Data    =").Append(HexDump.ShortToHex(ipos4Data)).Append('\n');
			stringBuilder.Append("    .cDim         =").Append(HexDump.ShortToHex(cDim)).Append('\n');
			stringBuilder.Append("    .cDimRw       =").Append(HexDump.ShortToHex(cDimRw)).Append('\n');
			stringBuilder.Append("    .cDimCol      =").Append(HexDump.ShortToHex(cDimCol)).Append('\n');
			stringBuilder.Append("    .cDimPg       =").Append(HexDump.ShortToHex(cDimPg)).Append('\n');
			stringBuilder.Append("    .cDimData     =").Append(HexDump.ShortToHex(cDimData)).Append('\n');
			stringBuilder.Append("    .cRw          =").Append(HexDump.ShortToHex(cRw)).Append('\n');
			stringBuilder.Append("    .cCol         =").Append(HexDump.ShortToHex(cCol)).Append('\n');
			stringBuilder.Append("    .grbit        =").Append(HexDump.ShortToHex(grbit)).Append('\n');
			stringBuilder.Append("    .itblAutoFmt  =").Append(HexDump.ShortToHex(itblAutoFmt)).Append('\n');
			stringBuilder.Append("    .name         =").Append(name).Append('\n');
			stringBuilder.Append("    .dataField    =").Append(dataField).Append('\n');
			stringBuilder.Append("[/SXVIEW]\n");
			return stringBuilder.ToString();
		}
	}
}
