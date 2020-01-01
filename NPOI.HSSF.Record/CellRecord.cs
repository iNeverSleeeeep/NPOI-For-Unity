using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	public abstract class CellRecord : StandardRecord, CellValueRecordInterface, IComparable
	{
		private int _rowIndex;

		private int _columnIndex;

		private int _formatIndex;

		public int Row
		{
			get
			{
				return _rowIndex;
			}
			set
			{
				_rowIndex = value;
			}
		}

		public int Column
		{
			get
			{
				return _columnIndex;
			}
			set
			{
				_columnIndex = value;
			}
		}

		/// get the index to the ExtendedFormat
		///
		/// @see org.apache.poi.hssf.record.ExtendedFormatRecord
		/// @return index to the XF record
		public short XFIndex
		{
			get
			{
				return (short)_formatIndex;
			}
			set
			{
				_formatIndex = value;
			}
		}

		/// Gets the debug info BIFF record type name (used by {@link #toString()}.
		protected abstract string RecordName
		{
			get;
		}

		/// @return the size (in bytes) of the value data for this cell record
		protected abstract int ValueDataSize
		{
			get;
		}

		protected override int DataSize => 6 + ValueDataSize;

		protected CellRecord()
		{
		}

		protected CellRecord(RecordInputStream in1)
		{
			_rowIndex = in1.ReadUShort();
			_columnIndex = in1.ReadUShort();
			_formatIndex = in1.ReadUShort();
		}

		public int CompareTo(object obj)
		{
			CellValueRecordInterface cellValueRecordInterface = (CellValueRecordInterface)obj;
			if (Row == cellValueRecordInterface.Row && Column == cellValueRecordInterface.Column)
			{
				return 0;
			}
			if (Row < cellValueRecordInterface.Row)
			{
				return -1;
			}
			if (Row > cellValueRecordInterface.Row)
			{
				return 1;
			}
			if (Column < cellValueRecordInterface.Column)
			{
				return -1;
			}
			if (Column > cellValueRecordInterface.Column)
			{
				return 1;
			}
			return -1;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string recordName = RecordName;
			stringBuilder.Append("[").Append(recordName).Append("]\n");
			stringBuilder.Append("    .row    = ").Append(HexDump.ShortToHex(Row)).Append("\n");
			stringBuilder.Append("    .col    = ").Append(HexDump.ShortToHex(Column)).Append("\n");
			stringBuilder.Append("    .xfindex= ").Append(HexDump.ShortToHex(XFIndex)).Append("\n");
			AppendValueText(stringBuilder);
			stringBuilder.Append("\n");
			stringBuilder.Append("[/").Append(recordName).Append("]\n");
			return stringBuilder.ToString();
		}

		/// Append specific debug info (used by {@link #toString()} for the value
		/// contained in this record. Trailing new-line should not be Appended
		/// (superclass does that).
		protected abstract void AppendValueText(StringBuilder sb);

		/// writes out the value data for this cell record
		protected abstract void SerializeValue(ILittleEndianOutput out1);

		public override void Serialize(ILittleEndianOutput out1)
		{
			out1.WriteShort(Row);
			out1.WriteShort(Column);
			out1.WriteShort(XFIndex);
			SerializeValue(out1);
		}

		protected void CopyBaseFields(CellRecord rec)
		{
			rec._rowIndex = _rowIndex;
			rec._columnIndex = _columnIndex;
			rec._formatIndex = _formatIndex;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is CellValueRecordInterface))
			{
				return false;
			}
			CellValueRecordInterface cellValueRecordInterface = (CellValueRecordInterface)obj;
			if (Row == cellValueRecordInterface.Row && Column == cellValueRecordInterface.Column)
			{
				return true;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Row ^ Column;
		}
	}
}
