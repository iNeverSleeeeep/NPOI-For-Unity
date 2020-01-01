using NPOI.Util;
using System;
using System.Text;

namespace NPOI.HSSF.Record
{
	/// Creates new BoolErrRecord. 
	/// REFERENCE:  PG ??? Microsoft Excel 97 Developer's Kit (ISBN: 1-57231-498-2)
	/// @author Michael P. Harhen
	/// @author Jason Height (jheight at chariot dot net dot au)
	/// @version 2.0-pre
	public class BoolErrRecord : CellRecord
	{
		public const short sid = 517;

		private int _value;

		/// If <code>true</code>, this record represents an error cell value, otherwise this record represents a boolean cell value
		private bool _isError;

		/// Get the value for the cell
		///
		/// @return bool representing the bool value
		public bool BooleanValue => _value != 0;

		/// Get the error value for the cell
		///
		/// @return byte representing the error value
		public byte ErrorValue => (byte)_value;

		/// Indicates whether the call holds a boolean value
		///
		/// @return boolean true if the cell holds a boolean value
		public bool IsBoolean => !_isError;

		/// Indicates whether the call holds an error value
		///
		/// @return bool true if the cell holds an error value
		public bool IsError => _isError;

		protected override string RecordName => "BOOLERR";

		protected override int ValueDataSize => 2;

		public override short Sid => 517;

		/// Creates new BoolErrRecord 
		public BoolErrRecord()
		{
		}

		/// Constructs a BoolErr record and Sets its fields appropriately.
		///
		/// @param in the RecordInputstream to Read the record from
		public BoolErrRecord(RecordInputStream in1)
			: base(in1)
		{
			switch (in1.Remaining)
			{
			case 2:
				_value = in1.ReadByte();
				break;
			case 3:
				_value = in1.ReadUShort();
				break;
			default:
				throw new RecordFormatException("Unexpected size (" + in1.Remaining + ") for BOOLERR record.");
			}
			int num = in1.ReadUByte();
			switch (num)
			{
			case 0:
				_isError = false;
				break;
			case 1:
				_isError = true;
				break;
			default:
				throw new RecordFormatException("Unexpected isError flag (" + num + ") for BOOLERR record.");
			}
		}

		/// Set the bool value for the cell
		///
		/// @param value   representing the bool value
		public void SetValue(bool value)
		{
			_value = (value ? 1 : 0);
			_isError = false;
		}

		/// Set the error value for the cell
		///
		/// @param value     error representing the error value
		///                  this value can only be 0,7,15,23,29,36 or 42
		///                  see bugzilla bug 16560 for an explanation
		public void SetValue(byte value)
		{
			switch (value)
			{
			case 0:
			case 7:
			case 15:
			case 23:
			case 29:
			case 36:
			case 42:
				_value = value;
				_isError = true;
				break;
			default:
				throw new ArgumentException("Error Value can only be 0,7,15,23,29,36 or 42. It cannot be " + value);
			}
		}

		protected override void AppendValueText(StringBuilder buffer)
		{
			if (IsBoolean)
			{
				buffer.Append("    .boolValue   = ").Append(BooleanValue).Append("\n");
			}
			else
			{
				buffer.Append("    .errCode     = ").Append(ErrorValue).Append("\n");
			}
		}

		protected override void SerializeValue(ILittleEndianOutput out1)
		{
			out1.WriteByte(_value);
			out1.WriteByte(_isError ? 1 : 0);
		}

		public override object Clone()
		{
			BoolErrRecord boolErrRecord = new BoolErrRecord();
			CopyBaseFields(boolErrRecord);
			boolErrRecord._value = _value;
			boolErrRecord._isError = _isError;
			return boolErrRecord;
		}
	}
}
