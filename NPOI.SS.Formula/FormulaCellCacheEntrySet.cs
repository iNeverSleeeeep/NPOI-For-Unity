using System;

namespace NPOI.SS.Formula
{
	/// A custom implementation of {@link java.util.HashSet} in order To reduce memory consumption.
	///
	/// Profiling tests (Oct 2008) have shown that each element {@link FormulaCellCacheEntry} takes
	/// around 32 bytes To store in a HashSet, but around 6 bytes To store here.  For Spreadsheets with
	/// thousands of formula cells with multiple interdependencies, the savings can be very significant.
	///
	/// @author Josh Micich
	internal class FormulaCellCacheEntrySet
	{
		private int _size;

		private FormulaCellCacheEntry[] _arr;

		public FormulaCellCacheEntrySet()
		{
			_arr = FormulaCellCacheEntry.EMPTY_ARRAY;
		}

		public FormulaCellCacheEntry[] ToArray()
		{
			int size = _size;
			if (size < 1)
			{
				return FormulaCellCacheEntry.EMPTY_ARRAY;
			}
			FormulaCellCacheEntry[] array = new FormulaCellCacheEntry[size];
			int num = 0;
			for (int i = 0; i < _arr.Length; i++)
			{
				FormulaCellCacheEntry formulaCellCacheEntry = _arr[i];
				if (formulaCellCacheEntry != null)
				{
					array[num++] = formulaCellCacheEntry;
				}
			}
			if (num != size)
			{
				throw new InvalidOperationException("size mismatch");
			}
			return array;
		}

		public void Add(CellCacheEntry cce)
		{
			if (_size * 3 >= _arr.Length * 2)
			{
				FormulaCellCacheEntry[] arr = _arr;
				FormulaCellCacheEntry[] arr2 = new FormulaCellCacheEntry[4 + _arr.Length * 3 / 2];
				for (int i = 0; i < arr.Length; i++)
				{
					FormulaCellCacheEntry formulaCellCacheEntry = _arr[i];
					if (formulaCellCacheEntry != null)
					{
						AddInternal(arr2, formulaCellCacheEntry);
					}
				}
				_arr = arr2;
			}
			if (AddInternal(_arr, cce))
			{
				_size++;
			}
		}

		private static bool AddInternal(CellCacheEntry[] arr, CellCacheEntry cce)
		{
			int num = cce.GetHashCode() % arr.Length;
			for (int i = num; i < arr.Length; i++)
			{
				CellCacheEntry cellCacheEntry = arr[i];
				if (cellCacheEntry == cce)
				{
					return false;
				}
				if (cellCacheEntry == null)
				{
					arr[i] = cce;
					return true;
				}
			}
			for (int j = 0; j < num; j++)
			{
				CellCacheEntry cellCacheEntry2 = arr[j];
				if (cellCacheEntry2 == cce)
				{
					return false;
				}
				if (cellCacheEntry2 == null)
				{
					arr[j] = cce;
					return true;
				}
			}
			throw new InvalidOperationException("No empty space found");
		}

		public bool Remove(CellCacheEntry cce)
		{
			FormulaCellCacheEntry[] arr = _arr;
			if (_size * 3 < _arr.Length && _arr.Length > 8)
			{
				bool result = false;
				FormulaCellCacheEntry[] arr2 = _arr;
				FormulaCellCacheEntry[] arr3 = new FormulaCellCacheEntry[_arr.Length / 2];
				for (int i = 0; i < arr2.Length; i++)
				{
					FormulaCellCacheEntry formulaCellCacheEntry = _arr[i];
					if (formulaCellCacheEntry != null)
					{
						if (formulaCellCacheEntry == cce)
						{
							result = true;
							_size--;
						}
						else
						{
							AddInternal(arr3, formulaCellCacheEntry);
						}
					}
				}
				_arr = arr3;
				return result;
			}
			int num = cce.GetHashCode() % arr.Length;
			for (int j = num; j < arr.Length; j++)
			{
				FormulaCellCacheEntry formulaCellCacheEntry2 = arr[j];
				if (formulaCellCacheEntry2 == cce)
				{
					arr[j] = null;
					_size--;
					return true;
				}
			}
			for (int k = 0; k < num; k++)
			{
				FormulaCellCacheEntry formulaCellCacheEntry3 = arr[k];
				if (formulaCellCacheEntry3 == cce)
				{
					arr[k] = null;
					_size--;
					return true;
				}
			}
			return false;
		}
	}
}
