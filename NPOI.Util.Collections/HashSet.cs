using System;
using System.Collections;

namespace NPOI.Util.Collections
{
	/// <summary>
	/// This class comes from Java
	/// </summary>
	public class HashSet : ISet, ICollection, IEnumerable
	{
		private readonly Hashtable impl = new Hashtable();

		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// The number of elements contained in the <see cref="T:System.Collections.ICollection" />.
		/// </returns>
		public int Count => impl.Count;

		/// <summary>
		/// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
		/// </summary>
		/// <value></value>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.
		/// </returns>
		public bool IsSynchronized => impl.IsSynchronized;

		/// <summary>
		/// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
		/// </summary>
		/// <value></value>
		/// <returns>
		/// An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
		/// </returns>
		public object SyncRoot => impl.SyncRoot;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.Util.Collections.HashSet" /> class.
		/// </summary>
		public HashSet()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.Util.Collections.HashSet" /> class.
		/// </summary>
		/// <param name="s">The s.</param>
		public HashSet(ISet s)
		{
			foreach (object o in s)
			{
				Add(o);
			}
		}

		/// <summary>
		/// Adds the specified o.
		/// </summary>
		/// <param name="o">The o.</param>
		public void Add(object o)
		{
			impl[o] = null;
		}

		/// <summary>
		/// Determines whether [contains] [the specified o].
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified o]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(object o)
		{
			return impl.ContainsKey(o);
		}

		/// <summary>
		/// Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// 	<paramref name="array" /> is null.
		/// </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		/// 	<paramref name="index" /> is less than zero.
		/// </exception>
		/// <exception cref="T:System.ArgumentException">
		/// 	<paramref name="array" /> is multidimensional.
		/// -or-
		/// <paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.
		/// -or-
		/// The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.
		/// </exception>
		/// <exception cref="T:System.ArgumentException">
		/// The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.
		/// </exception>
		public void CopyTo(Array array, int index)
		{
			impl.Keys.CopyTo(array, index);
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator GetEnumerator()
		{
			return impl.Keys.GetEnumerator();
		}

		/// <summary>
		/// Removes the specified o.
		/// </summary>
		/// <param name="o">The o.</param>
		public void Remove(object o)
		{
			impl.Remove(o);
		}

		/// <summary>
		/// Removes all of the elements from this set.
		/// The set will be empty after this call returns.
		/// </summary>
		public void Clear()
		{
			impl.Clear();
		}
	}
}
