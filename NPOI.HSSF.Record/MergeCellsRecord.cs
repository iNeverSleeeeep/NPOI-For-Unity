using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Collections;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: Merged Cells Record
	///
	/// Description:  Optional record defining a square area of cells to "merged" into
	///               one cell. 
	/// REFERENCE:  NONE (UNDOCUMENTED PRESENTLY) 
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class MergeCellsRecord : StandardRecord, ICloneable
	{
		public const short sid = 229;

		/// sometimes the regions array is shared with other MergedCellsRecords 
		private CellRangeAddress[] _regions;

		private int _startIndex;

		private int _numberOfRegions;

		/// Get the number of merged areas.  If this drops down to 0 you should just go
		/// ahead and delete the record.
		/// @return number of areas
		public short NumAreas
		{
			get
			{
				return (short)_numberOfRegions;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		protected override int DataSize => CellRangeAddressList.GetEncodedSize(_numberOfRegions);

		public override short Sid => 229;

		public MergeCellsRecord(CellRangeAddress[] regions, int startIndex, int numberOfRegions)
		{
			_regions = regions;
			_startIndex = startIndex;
			_numberOfRegions = numberOfRegions;
		}

		/// Constructs a MergedCellsRecord and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public MergeCellsRecord(RecordInputStream in1)
		{
			int num = in1.ReadUShort();
			CellRangeAddress[] array = new CellRangeAddress[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = new CellRangeAddress(in1);
			}
			_numberOfRegions = num;
			_startIndex = 0;
			_regions = array;
		}

		public IEnumerator GetEnumerator()
		{
			return _regions.GetEnumerator();
		}

		/// @return MergedRegion at the given index representing the area that is Merged (r1,c1 - r2,c2)
		public CellRangeAddress GetAreaAt(int index)
		{
			return _regions[_startIndex + index];
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			int numberOfRegions = _numberOfRegions;
			out1.WriteShort(numberOfRegions);
			for (int i = 0; i < _numberOfRegions; i++)
			{
				_regions[_startIndex + i].Serialize(out1);
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[MERGEDCELLS]").Append("\n");
			stringBuilder.Append("     .numregions =").Append(NumAreas).Append("\n");
			for (int i = 0; i < _numberOfRegions; i++)
			{
				CellRangeAddress cellRangeAddress = _regions[_startIndex + i];
				stringBuilder.Append("     .rowfrom    =").Append(cellRangeAddress.FirstRow).Append("\n");
				stringBuilder.Append("     .rowto      =").Append(cellRangeAddress.LastRow).Append("\n");
				stringBuilder.Append("     .colfrom    =").Append(cellRangeAddress.FirstColumn).Append("\n");
				stringBuilder.Append("     .colto      =").Append(cellRangeAddress.LastColumn).Append("\n");
			}
			stringBuilder.Append("[MERGEDCELLS]").Append("\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			int numberOfRegions = _numberOfRegions;
			CellRangeAddress[] array = new CellRangeAddress[numberOfRegions];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = _regions[_startIndex + i].Copy();
			}
			return new MergeCellsRecord(array, 0, numberOfRegions);
		}
	}
}
