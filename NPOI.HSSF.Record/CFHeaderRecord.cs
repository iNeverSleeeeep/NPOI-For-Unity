using NPOI.HSSF.Record.CF;
using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Conditional Formatting Header record (CFHEADER)
	///
	/// @author Dmitriy Kumshayev
	public class CFHeaderRecord : StandardRecord
	{
		public const short sid = 432;

		private int field_1_numcf;

		private int field_2_need_recalculation;

		private CellRangeAddress field_3_enclosing_cell_range;

		private CellRangeAddressList field_4_cell_ranges;

		public int NumberOfConditionalFormats
		{
			get
			{
				return field_1_numcf;
			}
			set
			{
				field_1_numcf = value;
			}
		}

		public bool NeedRecalculation
		{
			get
			{
				if (field_2_need_recalculation != 1)
				{
					return false;
				}
				return true;
			}
			set
			{
				field_2_need_recalculation = (value ? 1 : 0);
			}
		}

		public CellRangeAddress EnclosingCellRange
		{
			get
			{
				return field_3_enclosing_cell_range;
			}
			set
			{
				field_3_enclosing_cell_range = value;
			}
		}

		public CellRangeAddress[] CellRanges
		{
			get
			{
				return field_4_cell_ranges.CellRangeAddresses;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentException("cellRanges must not be null");
				}
				CellRangeAddressList cellRangeAddressList = new CellRangeAddressList();
				CellRangeAddress crB = null;
				foreach (CellRangeAddress cellRangeAddress in value)
				{
					crB = CellRangeUtil.CreateEnclosingCellRange(cellRangeAddress, crB);
					cellRangeAddressList.AddCellRangeAddress(cellRangeAddress);
				}
				field_3_enclosing_cell_range = crB;
				field_4_cell_ranges = cellRangeAddressList;
			}
		}

		protected override int DataSize => 12 + field_4_cell_ranges.Size;

		public override short Sid => 432;

		/// Creates new CFHeaderRecord 
		public CFHeaderRecord()
		{
			field_4_cell_ranges = new CellRangeAddressList();
		}

		public CFHeaderRecord(CellRangeAddress[] regions, int nRules)
		{
			CellRangeAddress[] array2 = CellRanges = CellRangeUtil.MergeCellRanges(regions);
			field_1_numcf = nRules;
		}

		public CFHeaderRecord(RecordInputStream in1)
		{
			field_1_numcf = in1.ReadShort();
			field_2_need_recalculation = in1.ReadShort();
			field_3_enclosing_cell_range = new CellRangeAddress(in1);
			field_4_cell_ranges = new CellRangeAddressList(in1);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[CFHEADER]\n");
			stringBuilder.Append("\t.id\t\t= ").Append(StringUtil.ToHexString((short)432)).Append("\n");
			stringBuilder.Append("\t.numCF\t\t\t= ").Append(NumberOfConditionalFormats).Append("\n");
			stringBuilder.Append("\t.needRecalc\t   = ").Append(NeedRecalculation).Append("\n");
			stringBuilder.Append("\t.enclosingCellRange= ").Append(EnclosingCellRange).Append("\n");
			stringBuilder.Append("\t.cfranges=[");
			for (int i = 0; i < field_4_cell_ranges.CountRanges(); i++)
			{
				stringBuilder.Append((i == 0) ? "" : ",").Append(field_4_cell_ranges.GetCellRangeAddress(i).ToString());
			}
			stringBuilder.Append("]\n");
			stringBuilder.Append("[/CFHEADER]\n");
			return stringBuilder.ToString();
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(field_1_numcf);
			out1.WriteShort(field_2_need_recalculation);
			field_3_enclosing_cell_range.Serialize(out1);
			field_4_cell_ranges.Serialize(out1);
		}

		public override object Clone()
		{
			CFHeaderRecord cFHeaderRecord = new CFHeaderRecord();
			cFHeaderRecord.field_1_numcf = field_1_numcf;
			cFHeaderRecord.field_2_need_recalculation = field_2_need_recalculation;
			cFHeaderRecord.field_3_enclosing_cell_range = field_3_enclosing_cell_range;
			cFHeaderRecord.field_4_cell_ranges = field_4_cell_ranges.Copy();
			return cFHeaderRecord;
		}
	}
}
