using System.Collections;

namespace NPOI.Util.Collections
{
	/// <summary>
	/// This interface comes from Java
	/// </summary>
	public interface ISet : ICollection, IEnumerable
	{
		/// <summary>
		/// Adds the specified o.
		/// </summary>
		/// <param name="o">The o.</param>
		void Add(object o);

		/// <summary>
		/// Determines whether [contains] [the specified o].
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified o]; otherwise, <c>false</c>.
		/// </returns>
		bool Contains(object o);

		/// <summary>
		/// Removes the specified o.
		/// </summary>
		/// <param name="o">The o.</param>
		void Remove(object o);

		/// <summary>
		/// Removes all of the elements from this set (optional operation).
		/// The set will be empty after this call returns.
		/// </summary>
		void Clear();
	}
}
