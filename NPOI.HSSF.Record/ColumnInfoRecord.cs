using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Title: COLINFO Record<p />
	/// Description:  Defines with width and formatting for a range of columns<p />
	/// REFERENCE:  PG 293 Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)<p />
	/// @author Andrew C. Oliver (acoliver at apache dot org)
	/// @version 2.0-pre
	public class ColumnInfoRecord : StandardRecord
	{
		public const short sid = 125;

		private int _first_col;

		private int _last_col;

		private int _col_width;

		private int _xf_index;

		private int _options;

		private static BitField hidden = BitFieldFactory.GetInstance(1);

		private static BitField outlevel = BitFieldFactory.GetInstance(1792);

		private static BitField collapsed = BitFieldFactory.GetInstance(4096);

		private int field_6_reserved;

		/// Get the first column this record defines formatting info for
		/// @return the first column index (0-based)
		public int FirstColumn
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

		/// Get the last column this record defines formatting info for
		/// @return the last column index (0-based)
		public int LastColumn
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

		/// Get the columns' width in 1/256 of a Char width
		/// @return column width
		public int ColumnWidth
		{
			get
			{
				return _col_width;
			}
			set
			{
				_col_width = value;
			}
		}

		/// Get the columns' default format info
		/// @return the extended format index
		/// @see org.apache.poi.hssf.record.ExtendedFormatRecord
		public int XFIndex
		{
			get
			{
				return _xf_index;
			}
			set
			{
				_xf_index = value;
			}
		}

		/// Get the options bitfield - use the bitSetters instead
		/// @return the bitfield raw value
		public int Options
		{
			get
			{
				return _options;
			}
			set
			{
				_options = value;
			}
		}

		/// Get whether or not these cells are hidden
		/// @return whether the cells are hidden.
		/// @see #SetOptions(short)
		public bool IsHidden
		{
			get
			{
				return hidden.IsSet(_options);
			}
			set
			{
				_options = hidden.SetBoolean(_options, value);
			}
		}

		/// Get the outline level for the cells
		/// @see #SetOptions(short)
		/// @return outline level for the cells
		public int OutlineLevel
		{
			get
			{
				return outlevel.GetValue(_options);
			}
			set
			{
				_options = outlevel.SetValue(_options, value);
			}
		}

		/// Get whether the cells are collapsed
		/// @return wether the cells are collapsed
		/// @see #SetOptions(short)
		public bool IsCollapsed
		{
			get
			{
				return collapsed.IsSet(_options);
			}
			set
			{
				_options = collapsed.SetBoolean(_options, value);
			}
		}

		public override short Sid => 125;

		protected override int DataSize => 12;

		public ColumnInfoRecord()
		{
			ColumnWidth = 2275;
			_options = 2;
			_xf_index = 15;
			field_6_reserved = 2;
		}

		/// Constructs a ColumnInfo record and Sets its fields appropriately
		/// @param in the RecordInputstream to Read the record from
		public ColumnInfoRecord(RecordInputStream in1)
		{
			_first_col = in1.ReadUShort();
			_last_col = in1.ReadUShort();
			_col_width = in1.ReadUShort();
			_xf_index = in1.ReadUShort();
			_options = in1.ReadUShort();
			switch (in1.Remaining)
			{
			case 2:
				field_6_reserved = in1.ReadUShort();
				break;
			case 1:
				field_6_reserved = in1.ReadByte();
				break;
			case 0:
				field_6_reserved = 0;
				break;
			default:
				throw new Exception("Unusual record size remaining=(" + in1.Remaining + ")");
			}
		}

		/// @return true if the format, options and column width match
		public bool FormatMatches(ColumnInfoRecord other)
		{
			if (_xf_index != other._xf_index)
			{
				return false;
			}
			if (_options != other._options)
			{
				return false;
			}
			if (_col_width != other._col_width)
			{
				return false;
			}
			return true;
		}

		public bool ContainsColumn(int columnIndex)
		{
			if (_first_col <= columnIndex)
			{
				return columnIndex <= _last_col;
			}
			return false;
		}

		public bool IsAdjacentBefore(ColumnInfoRecord other)
		{
			return _last_col == other._first_col - 1;
		}

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(FirstColumn);
			out1.WriteShort(LastColumn);
			out1.WriteShort(ColumnWidth);
			out1.WriteShort(XFIndex);
			out1.WriteShort(_options);
			out1.WriteShort(field_6_reserved);
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[COLINFO]\n");
			stringBuilder.Append("colfirst       = ").Append(FirstColumn).Append("\n");
			stringBuilder.Append("collast        = ").Append(LastColumn).Append("\n");
			stringBuilder.Append("colwidth       = ").Append(ColumnWidth).Append("\n");
			stringBuilder.Append("xFindex        = ").Append(XFIndex).Append("\n");
			stringBuilder.Append("options        = ").Append(Options).Append("\n");
			stringBuilder.Append("  hidden       = ").Append(IsHidden).Append("\n");
			stringBuilder.Append("  olevel       = ").Append(OutlineLevel).Append("\n");
			stringBuilder.Append("  collapsed    = ").Append(IsCollapsed).Append("\n");
			stringBuilder.Append("[/COLINFO]\n");
			return stringBuilder.ToString();
		}

		public override object Clone()
		{
			ColumnInfoRecord columnInfoRecord = new ColumnInfoRecord();
			columnInfoRecord._first_col = _first_col;
			columnInfoRecord._last_col = _last_col;
			columnInfoRecord._col_width = _col_width;
			columnInfoRecord._xf_index = _xf_index;
			columnInfoRecord._options = _options;
			columnInfoRecord.field_6_reserved = field_6_reserved;
			return columnInfoRecord;
		}
	}
}
