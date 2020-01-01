using NPOI.SS.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace NPOI.SS.Util
{
	/// For POI internal use only
	///
	/// @author Josh Micich
	public class SSCellRange<K> : ICellRange<K>, IEnumerable<K>, IEnumerable where K : ICell
	{
		internal class ArrayIterator<D> : IEnumerator<D>, IDisposable, IEnumerator
		{
			private D[] _array;

			private int _index;

			public D Current
			{
				get
				{
					if (_index >= _array.Length)
					{
						throw new ArgumentNullException(_index.ToString(CultureInfo.CurrentCulture));
					}
					return _array[_index++];
				}
			}

			object IEnumerator.Current
			{
				get
				{
					return Current;
				}
			}

			public ArrayIterator(D[] array)
			{
				_array = array;
				_index = 0;
			}

			public bool MoveNext()
			{
				return _index < _array.Length;
			}

			public void Remove()
			{
				throw new NotSupportedException("Cannot remove cells from this CellRange.");
			}

			public void Reset()
			{
			}

			public void Dispose()
			{
			}
		}

		private int _height;

		private int _width;

		private K[] _flattenedArray;

		private int _firstRow;

		private int _firstColumn;

		public K TopLeftCell => _flattenedArray[0];

		public K[] FlattenedCells => (K[])_flattenedArray.Clone();

		public K[][] Cells
		{
			get
			{
				Type type = _flattenedArray.GetType();
				K[][] result = (K[][])Array.CreateInstance(type, _height);
				type = type.GetElementType();
				for (int num = _height - 1; num >= 0; num--)
				{
					K[] destinationArray = (K[])Array.CreateInstance(type, _width);
					int sourceIndex = _width * num;
					Array.Copy(_flattenedArray, sourceIndex, destinationArray, 0, _width);
				}
				return result;
			}
		}

		public int Height => _height;

		public int Width => _width;

		public int Size => _height * _width;

		public string ReferenceText
		{
			get
			{
				CellRangeAddress cellRangeAddress = new CellRangeAddress(_firstRow, _firstRow + _height - 1, _firstColumn, _firstColumn + _width - 1);
				return cellRangeAddress.FormatAsString();
			}
		}

		private SSCellRange(int firstRow, int firstColumn, int height, int width, K[] flattenedArray)
		{
			_firstRow = firstRow;
			_firstColumn = firstColumn;
			_height = height;
			_width = width;
			_flattenedArray = flattenedArray;
		}

		public static SSCellRange<K> Create(int firstRow, int firstColumn, int height, int width, List<K> flattenedList, Type cellClass)
		{
			int count = flattenedList.Count;
			if (height * width != count)
			{
				throw new ArgumentException("Array size mismatch.");
			}
			K[] array = (K[])Array.CreateInstance(cellClass, count);
			array = flattenedList.ToArray();
			return new SSCellRange<K>(firstRow, firstColumn, height, width, array);
		}

		public K GetCell(int relativeRowIndex, int relativeColumnIndex)
		{
			if (relativeRowIndex < 0 || relativeRowIndex >= _height)
			{
				throw new IndexOutOfRangeException("Specified row " + relativeRowIndex + " is outside the allowable range (0.." + (_height - 1) + ").");
			}
			if (relativeColumnIndex < 0 || relativeColumnIndex >= _width)
			{
				throw new IndexOutOfRangeException("Specified colummn " + relativeColumnIndex + " is outside the allowable range (0.." + (_width - 1) + ").");
			}
			int num = _width * relativeRowIndex + relativeColumnIndex;
			return _flattenedArray[num];
		}

		public IEnumerator<K> GetEnumerator()
		{
			return new ArrayIterator<K>(_flattenedArray);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
