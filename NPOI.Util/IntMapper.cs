using System.Collections.Generic;

namespace NPOI.Util
{
	/// <summary>
	/// A List of objects that are indexed AND keyed by an int; also allows for Getting
	/// the index of a value in the list
	///
	/// <p>I am happy is someone wants to re-implement this without using the
	/// internal list and hashmap. If so could you please make sure that
	/// you can add elements half way into the list and have the value-key mappings
	/// update</p>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <remarks>@author Jason Height</remarks>
	public class IntMapper<T>
	{
		private List<T> elements;

		private Dictionary<T, int> valueKeyMap;

		private static int _default_size = 10;

		/// <summary>
		/// Gets the size.
		/// </summary>
		public int Size => elements.Count;

		/// <summary>
		/// Gets the T object at the specified index.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public T this[int index]
		{
			get
			{
				return elements[index];
			}
		}

		/// <summary>
		/// create an IntMapper of default size
		/// </summary>
		public IntMapper()
			: this(_default_size)
		{
		}

		public IntMapper(int InitialCapacity)
		{
			elements = new List<T>(InitialCapacity);
			valueKeyMap = new Dictionary<T, int>(InitialCapacity);
		}

		/// <summary>
		/// Appends the specified element to the end of this list
		/// </summary>
		/// <param name="value">element to be Appended to this list.</param>
		/// <returns>return true (as per the general contract of the Collection.add method)</returns>
		public bool Add(T value)
		{
			int count = elements.Count;
			elements.Add(value);
			if (valueKeyMap.ContainsKey(value))
			{
				valueKeyMap[value] = count;
			}
			else
			{
				valueKeyMap.Add(value, count);
			}
			return true;
		}

		/// <summary>
		/// Gets the index of T object.
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns></returns>
		public int GetIndex(T o)
		{
			if (!valueKeyMap.ContainsKey(o))
			{
				return -1;
			}
			return valueKeyMap[o];
		}

		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			return elements.GetEnumerator();
		}

		public void Clear()
		{
			elements.Clear();
			valueKeyMap.Clear();
		}
	}
}
