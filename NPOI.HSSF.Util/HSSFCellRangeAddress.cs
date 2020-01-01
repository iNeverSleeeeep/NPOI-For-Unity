using NPOI.HSSF.Record;
using NPOI.Util;
using System.Collections;

namespace NPOI.HSSF.Util
{
	/// <p>Title: HSSFCellRangeAddress</p>
	/// <p>Description:
	///          Implementation of the cell range Address lists,like Is described in
	///          OpenOffice.org's Excel Documentation .
	///          In BIFF8 there Is a common way to store absolute cell range Address
	///          lists in several records (not formulas). A cell range Address list
	///          consists of a field with the number of ranges and the list of the range
	///          Addresses. Each cell range Address (called an AddR structure) Contains
	///          4 16-bit-values.</p>
	/// <p>Copyright: Copyright (c) 2004</p>
	/// <p>Company: </p>
	/// @author Dragos Buleandra (dragos.buleandra@trade2b.ro)
	/// @version 2.0-pre
	public class HSSFCellRangeAddress
	{
		public class AddrStructure
		{
			private short _first_row;

			private short _first_col;

			private short _last_row;

			private short _last_col;

			/// Get the upper left hand corner column number
			/// @return column number for the upper left hand corner
			public short FirstColumn
			{
				get
				{
					return _first_col;
				}
				set
				{
					_first_col = value;
				}
			}

			/// Get the upper left hand corner row number
			/// @return row number for the upper left hand corner
			public short FirstRow
			{
				get
				{
					return _first_row;
				}
				set
				{
					_first_row = value;
				}
			}

			/// Get the lower right hand corner column number
			/// @return column number for the lower right hand corner
			public short LastColumn
			{
				get
				{
					return _last_col;
				}
				set
				{
					_last_col = value;
				}
			}

			/// Get the lower right hand corner row number
			/// @return row number for the lower right hand corner
			public short LastRow
			{
				get
				{
					return _last_row;
				}
				set
				{
					_last_row = value;
				}
			}

			public AddrStructure(short first_row, short last_row, short first_col, short last_col)
			{
				_first_row = first_row;
				_last_row = last_row;
				_first_col = first_col;
				_last_col = last_col;
			}
		}

		private static POILogger logger = POILogFactory.GetLogger(typeof(HSSFCellRangeAddress));

		/// Number of following AddR structures
		private short field_Addr_number;

		/// List of AddR structures. Each structure represents a cell range
		private ArrayList field_regions_list;

		/// Get the number of following AddR structures.
		/// The number of this structures Is automatically Set when reading an Excel file
		/// and/or increased when you manually Add a new AddR structure .
		/// This Is the reason there Isn't a Set method for this field .
		/// @return number of AddR structures
		public short AddRStructureNumber => field_Addr_number;

		public int Size => 2 + field_Addr_number * 8;

		public HSSFCellRangeAddress()
		{
		}

		/// Construct a new HSSFCellRangeAddress object and Sets its fields appropriately .
		/// Even this Isn't an Excel record , I kept the same behavior for reading/writing
		/// the object's data as for a regular record .
		///
		/// @param in the RecordInputstream to read the record from
		public HSSFCellRangeAddress(RecordInputStream in1)
		{
			FillFields(in1);
		}

		public void FillFields(RecordInputStream in1)
		{
			field_Addr_number = in1.ReadShort();
			field_regions_list = new ArrayList(field_Addr_number);
			for (int i = 0; i < field_Addr_number; i++)
			{
				short num = in1.ReadShort();
				short num2 = in1.ReadShort();
				short first_col = num;
				short last_col = num2;
				if (in1.Remaining >= 4)
				{
					first_col = in1.ReadShort();
					last_col = in1.ReadShort();
				}
				else
				{
					logger.Log(5, "Ran out of data reading cell references for DVRecord");
					i = field_Addr_number;
				}
				AddrStructure value = new AddrStructure(num, num2, first_col, last_col);
				field_regions_list.Add(value);
			}
		}

		/// Add an AddR structure .
		/// @param first_row - the upper left hand corner's row
		/// @param first_col - the upper left hand corner's col
		/// @param last_row  - the lower right hand corner's row
		/// @param last_col  - the lower right hand corner's col
		/// @return the index of this AddR structure
		public int AddAddRStructure(short first_row, short first_col, short last_row, short last_col)
		{
			if (field_regions_list == null)
			{
				field_Addr_number = 0;
				field_regions_list = new ArrayList(10);
			}
			AddrStructure value = new AddrStructure(first_row, last_row, first_col, last_col);
			field_regions_list.Add(value);
			field_Addr_number++;
			return field_Addr_number;
		}

		/// Remove the AddR structure stored at the passed in index
		/// @param index The AddR structure's index
		public void RemoveAddRStructureAt(int index)
		{
			field_regions_list.Remove(index);
			field_Addr_number--;
		}

		/// return the AddR structure at the given index.
		/// @return AddrStructure representing
		public AddrStructure GetAddRStructureAt(int index)
		{
			return (AddrStructure)field_regions_list[index];
		}

		public int Serialize(int offSet, byte[] data)
		{
			int num = 2;
			LittleEndian.PutShort(data, offSet, AddRStructureNumber);
			for (int i = 0; i < AddRStructureNumber; i++)
			{
				AddrStructure addRStructureAt = GetAddRStructureAt(i);
				LittleEndian.PutShort(data, offSet + num, addRStructureAt.FirstRow);
				num += 2;
				LittleEndian.PutShort(data, offSet + num, addRStructureAt.LastRow);
				num += 2;
				LittleEndian.PutShort(data, offSet + num, addRStructureAt.FirstColumn);
				num += 2;
				LittleEndian.PutShort(data, offSet + num, addRStructureAt.LastColumn);
				num += 2;
			}
			return Size;
		}
	}
}
