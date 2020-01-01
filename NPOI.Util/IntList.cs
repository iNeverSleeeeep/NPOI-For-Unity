using System;

namespace NPOI.Util
{
	/// <summary>
	/// A List of int's; as full an implementation of the java.Util.List interface as possible, with an eye toward minimal creation of objects
	///
	/// the mimicry of List is as follows:
	/// <ul>
	/// <li> if possible, operations designated 'optional' in the List
	///      interface are attempted</li>
	/// <li> wherever the List interface refers to an Object, substitute
	///      int</li>
	/// <li> wherever the List interface refers to a Collection or List,
	///      substitute IntList</li>
	/// </ul>
	///
	/// the mimicry is not perfect, however:
	/// <ul>
	/// <li> operations involving Iterators or ListIterators are not
	///      supported</li>
	/// <li> Remove(Object) becomes RemoveValue to distinguish it from
	///      Remove(int index)</li>
	/// <li> subList is not supported</li>
	/// </ul>
	/// @author Marc Johnson
	/// </summary>
	public class IntList
	{
		private int[] _array;

		private int _limit;

		private int fillval;

		private static int _default_size = 128;

		/// <summary>
		/// the number of elements in this IntList
		/// </summary>
		public int Count => _limit;

		/// <summary>
		/// create an IntList of default size
		/// </summary>
		public IntList()
			: this(_default_size)
		{
		}

		public IntList(int InitialCapacity)
			: this(InitialCapacity, 0)
		{
		}

		/// <summary>
		/// create a copy of an existing IntList
		/// </summary>
		/// <param name="list">the existing IntList</param>
		public IntList(IntList list)
			: this(list._array.Length)
		{
			Array.Copy(list._array, 0, _array, 0, _array.Length);
			_limit = list._limit;
		}

		/// <summary>
		/// create an IntList with a predefined Initial size
		/// </summary>
		/// <param name="initialCapacity">the size for the internal array</param>
		/// <param name="fillvalue"></param>
		public IntList(int initialCapacity, int fillvalue)
		{
			_array = new int[initialCapacity];
			if (fillval != 0)
			{
				fillval = fillvalue;
				FillArray(fillval, _array, 0);
			}
			_limit = 0;
		}

		private void FillArray(int val, int[] array, int index)
		{
			for (int i = index; i < array.Length; i++)
			{
				array[i] = val;
			}
		}

		/// <summary>
		/// add the specfied value at the specified index
		/// </summary>
		/// <param name="index">the index where the new value is to be Added</param>
		/// <param name="value">the new value</param>
		public void Add(int index, int value)
		{
			if (index > _limit)
			{
				throw new IndexOutOfRangeException();
			}
			if (index == _limit)
			{
				Add(value);
			}
			else
			{
				if (_limit == _array.Length)
				{
					growArray(_limit * 2);
				}
				Array.Copy(_array, index, _array, index + 1, _limit - index);
				_array[index] = value;
				_limit++;
			}
		}

		/// <summary>
		/// Appends the specified element to the end of this list
		/// </summary>
		/// <param name="value">element to be Appended to this list.</param>
		/// <returns>return true (as per the general contract of the Collection.add method</returns>
		public bool Add(int value)
		{
			if (_limit == _array.Length)
			{
				growArray(_limit * 2);
			}
			_array[_limit++] = value;
			return true;
		}

		/// <summary>
		/// Appends all of the elements in the specified collection to the
		/// end of this list, in the order that they are returned by the
		/// specified collection's iterator.  The behavior of this
		/// operation is unspecified if the specified collection is
		/// modified while the operation is in progress.  (Note that this
		/// will occur if the specified collection is this list, and it's
		/// nonempty.)
		/// </summary>
		/// <param name="c">collection whose elements are to be Added to this list.</param>
		/// <returns>return true if this list Changed as a result of the call.</returns>
		public bool AddAll(IntList c)
		{
			if (c._limit != 0)
			{
				if (_limit + c._limit > _array.Length)
				{
					growArray(_limit + c._limit);
				}
				Array.Copy(c._array, 0, _array, _limit, c._limit);
				_limit += c._limit;
			}
			return true;
		}

		/// <summary>
		/// Inserts all of the elements in the specified collection into
		/// this list at the specified position.  Shifts the element
		/// currently at that position (if any) and any subsequent elements
		/// to the right (increases their indices).  The new elements will
		/// appear in this list in the order that they are returned by the
		/// specified collection's iterator.  The behavior of this
		/// operation is unspecified if the specified collection is
		/// modified while the operation is in progress.  (Note that this
		/// will occur if the specified collection is this list, and it's
		/// nonempty.)
		/// </summary>
		/// <param name="index">index at which to insert first element from the specified collection.</param>
		/// <param name="c">elements to be inserted into this list.</param>
		/// <returns>return true if this list Changed as a result of the call.</returns>
		public bool AddAll(int index, IntList c)
		{
			if (index > _limit)
			{
				throw new IndexOutOfRangeException();
			}
			if (c._limit != 0)
			{
				if (_limit + c._limit > _array.Length)
				{
					growArray(_limit + c._limit);
				}
				Array.Copy(_array, index, _array, index + c._limit, _limit - index);
				Array.Copy(c._array, 0, _array, index, c._limit);
				_limit += c._limit;
			}
			return true;
		}

		/// <summary>
		/// Removes all of the elements from this list.  This list will be
		/// empty After this call returns (unless it throws an exception).
		/// </summary>
		public void Clear()
		{
			_limit = 0;
		}

		/// <summary>
		/// Returns true if this list Contains the specified element.  More
		/// formally, returns true if and only if this list Contains at
		/// least one element e such that o == e
		/// </summary>
		/// <param name="o">element whose presence in this list is to be Tested.</param>
		/// <returns>return true if this list Contains the specified element.</returns>
		public bool Contains(int o)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < _limit)
			{
				if (_array[num] == o)
				{
					flag = true;
				}
				num++;
			}
			return flag;
		}

		/// <summary>
		/// Returns true if this list Contains all of the elements of the
		/// specified collection.
		/// </summary>
		/// <param name="c">collection to be Checked for Containment in this list.</param>
		/// <returns>return true if this list Contains all of the elements of the specified collection.</returns>
		public bool ContainsAll(IntList c)
		{
			bool flag = true;
			if (this != c)
			{
				int num = 0;
				while (flag && num < c._limit)
				{
					if (!Contains(c._array[num]))
					{
						flag = false;
					}
					num++;
				}
			}
			return flag;
		}

		/// <summary>
		/// Compares the specified object with this list for Equality.
		/// Returns true if and only if the specified object is also a
		/// list, both lists have the same size, and all corresponding
		/// pairs of elements in the two lists are Equal.  (Two elements e1
		/// and e2 are equal if e1 == e2.)  In other words, two lists are
		/// defined to be equal if they contain the same elements in the
		/// same order.  This defInition ensures that the Equals method
		/// works properly across different implementations of the List
		/// interface.
		/// </summary>
		/// <param name="o">the object to be Compared for Equality with this list.</param>
		/// <returns>return true if the specified object is equal to this list.</returns>
		public override bool Equals(object o)
		{
			bool flag = this == o;
			if (!flag && o != null && o.GetType() == GetType())
			{
				IntList intList = (IntList)o;
				if (intList._limit == _limit)
				{
					flag = true;
					int num = 0;
					while (flag && num < _limit)
					{
						flag = (_array[num] == intList._array[num]);
						num++;
					}
				}
			}
			return flag;
		}

		/// <summary>
		/// Returns the element at the specified position in this list.
		/// </summary>
		/// <param name="index">index of element to return.</param>
		/// <returns>return the element at the specified position in this list.</returns>
		public int Get(int index)
		{
			if (index >= _limit)
			{
				throw new IndexOutOfRangeException(index + " not accessible in a list of length " + _limit);
			}
			return _array[index];
		}

		/// <summary>
		/// Returns the hash code value for this list.  The hash code of a
		/// list is defined to be the result of the following calculation:
		///
		///  <code>
		///  hashCode = 1;
		///  Iterator i = list.Iterator();
		///  while (i.HasNext()) {
		///       Object obj = i.Next();
		///       hashCode = 31*hashCode + (obj==null ? 0 : obj.HashCode());
		///  }
		///  </code>
		///
		///  This ensures that list1.Equals(list2) implies that
		///  list1.HashCode()==list2.HashCode() for any two lists, list1 and
		///  list2, as required by the general contract of Object.HashCode.
		///
		/// </summary>
		/// <returns>return the hash code value for this list.</returns>
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < _limit; i++)
			{
				num = 31 * num + _array[i];
			}
			return num;
		}

		/// <summary>
		/// Returns the index in this list of the first occurrence of the
		/// specified element, or -1 if this list does not contain this
		/// element.  More formally, returns the lowest index i such that
		/// (o == Get(i)), or -1 if there is no such index.
		/// </summary>
		/// <param name="o">element to search for.</param>
		/// <returns>return the index in this list of the first occurrence of the
		/// specified element, or -1 if this list does not contain
		/// this element.</returns>
		public int IndexOf(int o)
		{
			int i;
			for (i = 0; i < _limit && o != _array[i]; i++)
			{
			}
			if (i == _limit)
			{
				i = -1;
			}
			return i;
		}

		/// <summary>
		/// Returns true if this list Contains no elements.
		/// </summary>
		/// <returns>return true if this list Contains no elements.</returns>
		public bool IsEmpty()
		{
			return _limit == 0;
		}

		/// <summary>
		/// Returns the index in this list of the last occurrence of the
		/// specified element, or -1 if this list does not contain this
		/// element.  More formally, returns the highest index i such that
		/// (o == Get(i)), or -1 if there is no such index.
		/// </summary>
		/// <param name="o">element to search for.</param>
		/// <returns>the index in this list of the last occurrence of the
		/// specified element, or -1 if this list does not contain
		/// this element.
		/// </returns>
		public int LastIndexOf(int o)
		{
			int num = _limit - 1;
			while (num >= 0 && o != _array[num])
			{
				num--;
			}
			return num;
		}

		/// <summary>
		/// Removes the element at the specified position in this list.
		/// Shifts any subsequent elements to the left (subtracts one from
		/// their indices).  Returns the element that was Removed from the
		/// list.
		/// </summary>
		/// <param name="index">the index of the element to Removed.</param>
		/// <returns>return the element previously at the specified position.</returns>
		public int Remove(int index)
		{
			if (index >= _limit)
			{
				throw new IndexOutOfRangeException();
			}
			int result = _array[index];
			Array.Copy(_array, index + 1, _array, index, _limit - index);
			_limit--;
			return result;
		}

		/// <summary>
		/// Removes the first occurrence in this list of the specified
		/// element (optional operation).  If this list does not contain
		/// the element, it is unChanged.  More formally, Removes the
		/// element with the lowest index i such that (o.Equals(get(i)))
		/// (if such an element exists).
		/// </summary>
		/// <param name="o">element to be Removed from this list, if present.</param>
		/// <returns>return true if this list Contained the specified element.</returns>
		public bool RemoveValue(int o)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < _limit)
			{
				if (o == _array[num])
				{
					if (num + 1 < _limit)
					{
						Array.Copy(_array, num + 1, _array, num, _limit - num);
					}
					_limit--;
					flag = true;
				}
				num++;
			}
			return flag;
		}

		/// <summary>
		/// Removes from this list all the elements that are Contained in
		/// the specified collection
		/// </summary>
		/// <param name="c">collection that defines which elements will be Removed from the list.</param>
		/// <returns>return true if this list Changed as a result of the call.</returns>
		public bool RemoveAll(IntList c)
		{
			bool result = false;
			for (int i = 0; i < c._limit; i++)
			{
				if (RemoveValue(c._array[i]))
				{
					result = true;
				}
			}
			return result;
		}

		/// <summary>
		/// Retains only the elements in this list that are Contained in
		/// the specified collection.  In other words, Removes from this
		/// list all the elements that are not Contained in the specified
		/// collection.
		/// </summary>
		/// <param name="c">collection that defines which elements this Set will retain.</param>
		/// <returns>return true if this list Changed as a result of the call.</returns>
		public bool RetainAll(IntList c)
		{
			bool result = false;
			int num = 0;
			while (num < _limit)
			{
				if (!c.Contains(_array[num]))
				{
					Remove(num);
					result = true;
				}
				else
				{
					num++;
				}
			}
			return result;
		}

		/// <summary>
		/// Replaces the element at the specified position in this list with the specified element
		/// </summary>
		/// <param name="index">index of element to Replace.</param>
		/// <param name="element">element to be stored at the specified position.</param>
		/// <returns>the element previously at the specified position.</returns>
		public int Set(int index, int element)
		{
			if (index >= _limit)
			{
				throw new IndexOutOfRangeException();
			}
			int result = _array[index];
			_array[index] = element;
			return result;
		}

		/// <summary>
		/// Returns the number of elements in this list. If this list
		/// Contains more than Int32.MaxValue elements, returns
		/// Int32.MaxValue.
		/// </summary>
		/// <returns>the number of elements in this IntList</returns>
		public int Size()
		{
			return _limit;
		}

		/// <summary>
		/// Returns an array Containing all of the elements in this list in
		/// proper sequence.  Obeys the general contract of the
		/// Collection.ToArray method.
		/// </summary>
		/// <returns>an array Containing all of the elements in this list in proper sequence.</returns>
		public int[] ToArray()
		{
			int[] array = new int[_limit];
			Array.Copy(_array, 0, array, 0, _limit);
			return array;
		}

		/// <summary>
		/// Returns an array Containing all of the elements in this list in
		/// proper sequence.  Obeys the general contract of the
		/// Collection.ToArray(Object[]) method.
		/// </summary>
		/// <param name="a">the array into which the elements of this list are to
		/// be stored, if it is big enough; otherwise, a new array
		/// is allocated for this purpose.</param>
		/// <returns>return an array Containing the elements of this list.</returns>
		public int[] ToArray(int[] a)
		{
			if (a.Length == _limit)
			{
				Array.Copy(_array, 0, a, 0, _limit);
				return a;
			}
			return ToArray();
		}

		private void growArray(int new_size)
		{
			int num = (new_size == _array.Length) ? (new_size + 1) : new_size;
			int[] array = new int[num];
			if (fillval != 0)
			{
				FillArray(fillval, array, _array.Length);
			}
			Array.Copy(_array, 0, array, 0, _limit);
			_array = array;
		}
	}
}
