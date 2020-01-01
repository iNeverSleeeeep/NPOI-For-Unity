using NPOI.SS.Formula.Constant;
using NPOI.SS.Util;
using NPOI.Util;
using System;
using System.Text;

namespace NPOI.SS.Formula.PTG
{
	/// ArrayPtg - handles arrays
	///
	/// The ArrayPtg is a little weird, the size of the Ptg when parsing initially only
	/// includes the Ptg sid and the reserved bytes. The next Ptg in the expression then follows.
	/// It is only after the "size" of all the Ptgs is met, that the ArrayPtg data is actually
	/// held after this. So Ptg.CreateParsedExpression keeps track of the number of 
	/// ArrayPtg elements and need to Parse the data upto the FORMULA record size.
	///
	/// @author Jason Height (jheight at chariot dot net dot au)
	public class ArrayPtg : Ptg
	{
		/// Represents the initial plain tArray token (without the constant data that trails the whole
		/// formula).  Objects of this class are only temporary and cannot be used as {@link Ptg}s.
		/// These temporary objects get converted to {@link ArrayPtg} by the
		/// {@link #finishReading(LittleEndianInput)} method.
		public class Initial : Ptg
		{
			private int _reserved0;

			private int _reserved1;

			private int _reserved2;

			public override byte DefaultOperandClass
			{
				get
				{
					throw Invalid();
				}
			}

			public override int Size => 8;

			public override bool IsBaseToken => false;

			public Initial(ILittleEndianInput in1)
			{
				_reserved0 = in1.ReadInt();
				_reserved1 = in1.ReadUShort();
				_reserved2 = in1.ReadUByte();
			}

			private static Exception Invalid()
			{
				throw new InvalidOperationException("This object is a partially initialised tArray, and cannot be used as a Ptg");
			}

			public override string ToFormulaString()
			{
				throw Invalid();
			}

			public override void Write(ILittleEndianOutput out1)
			{
				throw Invalid();
			}

			/// Read in the actual token (array) values. This occurs
			/// AFTER the last Ptg in the expression.
			/// See page 304-305 of Excel97-2007BinaryFileFormat(xls)Specification.pdf
			public ArrayPtg FinishReading(ILittleEndianInput in1)
			{
				int num = in1.ReadUByte();
				short num2 = in1.ReadShort();
				num++;
				num2 = (short)(num2 + 1);
				int nValues = num2 * num;
				object[] arrayValues = ConstantValueParser.Parse(in1, nValues);
				ArrayPtg arrayPtg = new ArrayPtg(_reserved0, _reserved1, _reserved2, num, num2, arrayValues);
				arrayPtg.PtgClass = base.PtgClass;
				return arrayPtg;
			}
		}

		public const byte sid = 32;

		private const int RESERVED_FIELD_LEN = 7;

		/// The size of the plain tArray token written within the standard formula tokens
		/// (not including the data which comes after all formula tokens)
		public const int PLAIN_TOKEN_SIZE = 8;

		private int _reserved0Int;

		private int _reserved1Short;

		private int _reserved2Byte;

		private int _nColumns;

		private int _nRows;

		private object[] _arrayValues;

		public override bool IsBaseToken => false;

		public int RowCount => _nRows;

		public int ColumnCount => _nColumns;

		/// This size includes the size of the array Ptg plus the Array Ptg Token value size
		public override int Size
		{
			get
			{
				int num = 11;
				return num + ConstantValueParser.GetEncodedSize(_arrayValues);
			}
		}

		public override byte DefaultOperandClass => 64;

		private ArrayPtg(int reserved0, int reserved1, int reserved2, int nColumns, int nRows, object[] arrayValues)
		{
			_reserved0Int = reserved0;
			_reserved1Short = reserved1;
			_reserved2Byte = reserved2;
			_nColumns = nColumns;
			_nRows = nRows;
			_arrayValues = arrayValues;
		}

		/// @param values2d array values arranged in rows
		public ArrayPtg(object[][] values2d)
		{
			int num = values2d[0].Length;
			int num2 = values2d.Length;
			_nColumns = (short)num;
			_nRows = (short)num2;
			object[] array = new object[_nColumns * _nRows];
			for (int i = 0; i < num2; i++)
			{
				object[] array2 = values2d[i];
				for (int j = 0; j < num; j++)
				{
					array[GetValueIndex(j, i)] = array2[j];
				}
			}
			_arrayValues = array;
			_reserved0Int = 0;
			_reserved1Short = 0;
			_reserved2Byte = 0;
		}

		public object[][] GetTokenArrayValues()
		{
			if (_arrayValues == null)
			{
				throw new InvalidOperationException("array values not read yet");
			}
			object[][] array = new object[_nRows][];
			for (int i = 0; i < _nRows; i++)
			{
				array[i] = new object[_nColumns];
				for (int j = 0; j < _nColumns; j++)
				{
					array[i][j] = _arrayValues[GetValueIndex(j, i)];
				}
			}
			return array;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("[ArrayPtg]\n");
			stringBuilder.Append("columns = ").Append(ColumnCount).Append("\n");
			stringBuilder.Append("rows = ").Append(RowCount).Append("\n");
			for (int i = 0; i < ColumnCount; i++)
			{
				for (int j = 0; j < RowCount; j++)
				{
					object value = _arrayValues.GetValue(GetValueIndex(i, j));
					stringBuilder.Append("[").Append(i).Append("][")
						.Append(j)
						.Append("] = ")
						.Append(value)
						.Append("\n");
				}
			}
			return stringBuilder.ToString();
		}

		/// Note - (2D) array elements are stored column by column 
		/// @return the index into the internal 1D array for the specified column and row
		public int GetValueIndex(int colIx, int rowIx)
		{
			if (colIx < 0 || colIx >= _nColumns)
			{
				throw new ArgumentException("Specified colIx (" + colIx + ") is outside the allowed range (0.." + (_nColumns - 1) + ")");
			}
			if (rowIx < 0 || rowIx >= _nRows)
			{
				throw new ArgumentException("Specified rowIx (" + rowIx + ") is outside the allowed range (0.." + (_nRows - 1) + ")");
			}
			return rowIx * _nColumns + colIx;
		}

		public override void Write(ILittleEndianOutput out1)
		{
			out1.WriteByte(32 + base.PtgClass);
			out1.WriteInt(_reserved0Int);
			out1.WriteShort(_reserved1Short);
			out1.WriteByte(_reserved2Byte);
		}

		public int WriteTokenValueBytes(ILittleEndianOutput out1)
		{
			out1.WriteByte(_nColumns - 1);
			out1.WriteShort(_nRows - 1);
			ConstantValueParser.Encode(out1, _arrayValues);
			return 3 + ConstantValueParser.GetEncodedSize(_arrayValues);
		}

		public override string ToFormulaString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{");
			for (int i = 0; i < RowCount; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(";");
				}
				for (int j = 0; j < ColumnCount; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(",");
					}
					object value = _arrayValues.GetValue(GetValueIndex(j, i));
					stringBuilder.Append(GetConstantText(value));
				}
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		private static string GetConstantText(object o)
		{
			if (o == null)
			{
				return "";
			}
			if (o is string)
			{
				return "\"" + (string)o + "\"";
			}
			if (o is double || o is double)
			{
				return NumberToTextConverter.ToText((double)o);
			}
			if (o is bool || o is bool)
			{
				return ((bool)o).ToString().ToUpper();
			}
			if (o is ErrorConstant)
			{
				return ((ErrorConstant)o).Text;
			}
			throw new ArgumentException("Unexpected constant class (" + o.GetType().Name + ")");
		}
	}
}
