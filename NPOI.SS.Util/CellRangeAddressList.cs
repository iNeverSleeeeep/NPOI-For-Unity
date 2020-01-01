using NPOI.HSSF.Record;
using NPOI.Util;
using System;
using System.Collections;

namespace NPOI.SS.Util
{
	public class CellRangeAddressList
	{
		/// List of <c>CellRangeAddress</c>es. Each structure represents a cell range
		private ArrayList _list;

		public int Size => GetEncodedSize(_list.Count);

		public CellRangeAddress[] CellRangeAddresses => (CellRangeAddress[])_list.ToArray(typeof(CellRangeAddress));

		public CellRangeAddressList()
		{
			_list = new ArrayList();
		}

		/// Convenience constructor for creating a <c>CellRangeAddressList</c> with a single 
		/// <c>CellRangeAddress</c>.  Other <c>CellRangeAddress</c>es may be Added later.
		public CellRangeAddressList(int firstRow, int lastRow, int firstCol, int lastCol)
			: this()
		{
			AddCellRangeAddress(firstRow, firstCol, lastRow, lastCol);
		}

		/// @param in the RecordInputstream to read the record from
		public CellRangeAddressList(RecordInputStream in1)
		{
			int num = in1.ReadUShort();
			_list = new ArrayList(num);
			for (int i = 0; i < num; i++)
			{
				_list.Add(new CellRangeAddress(in1));
			}
		}

		/// Get the number of following ADDR structures. The number of this
		/// structures is automatically set when reading an Excel file and/or
		/// increased when you manually Add a new ADDR structure . This is the reason
		/// there isn't a set method for this field .
		///
		/// @return number of ADDR structures
		public int CountRanges()
		{
			return _list.Count;
		}

		/// Add a cell range structure.
		///
		/// @param firstRow - the upper left hand corner's row
		/// @param firstCol - the upper left hand corner's col
		/// @param lastRow - the lower right hand corner's row
		/// @param lastCol - the lower right hand corner's col
		/// @return the index of this ADDR structure
		public void AddCellRangeAddress(int firstRow, int firstCol, int lastRow, int lastCol)
		{
			CellRangeAddress cra = new CellRangeAddress(firstRow, lastRow, firstCol, lastCol);
			AddCellRangeAddress(cra);
		}

		public void AddCellRangeAddress(CellRangeAddress cra)
		{
			_list.Add(cra);
		}

		public CellRangeAddress Remove(int rangeIndex)
		{
			if (_list.Count == 0)
			{
				throw new Exception("List is empty");
			}
			if (rangeIndex < 0 || rangeIndex >= _list.Count)
			{
				throw new Exception("Range index (" + rangeIndex + ") is outside allowable range (0.." + (_list.Count - 1) + ")");
			}
			CellRangeAddress result = (CellRangeAddress)_list[rangeIndex];
			_list.Remove(rangeIndex);
			return result;
		}

		/// @return <c>CellRangeAddress</c> at the given index
		public CellRangeAddress GetCellRangeAddress(int index)
		{
			return (CellRangeAddress)_list[index];
		}

		public int Serialize(int offset, byte[] data)
		{
			int size = Size;
			Serialize(new LittleEndianByteArrayOutputStream(data, offset, size));
			return size;
		}

		public void Serialize(ILittleEndianOutput out1)
		{
			int count = _list.Count;
			out1.WriteShort(count);
			for (int i = 0; i < count; i++)
			{
				CellRangeAddress cellRangeAddress = (CellRangeAddress)_list[i];
				cellRangeAddress.Serialize(out1);
			}
		}

		/// @return the total size of for the specified number of ranges,
		///  including the initial 2 byte range count
		public static int GetEncodedSize(int numberOfRanges)
		{
			return 2 + CellRangeAddress.GetEncodedSize(numberOfRanges);
		}

		public CellRangeAddressList Copy()
		{
			CellRangeAddressList cellRangeAddressList = new CellRangeAddressList();
			int count = _list.Count;
			for (int i = 0; i < count; i++)
			{
				CellRangeAddress cellRangeAddress = (CellRangeAddress)_list[i];
				cellRangeAddressList.AddCellRangeAddress(cellRangeAddress.Copy());
			}
			return cellRangeAddressList;
		}
	}
}
