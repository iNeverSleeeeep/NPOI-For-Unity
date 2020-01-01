using System.Collections.Generic;

namespace NPOI.POIFS.Properties
{
	/// <summary>
	/// Behavior for parent (directory) properties
	/// @author Marc Johnson27591@hotmail.com
	/// </summary>
	public interface Parent : Child
	{
		/// <summary>
		/// Get an iterator over the children of this Parent
		/// all elements are instances of Property.
		/// </summary>
		/// <returns></returns>
		IEnumerator<Property> Children
		{
			get;
		}

		/// <summary>
		/// Sets the previous child.
		/// </summary>
		new Child PreviousChild
		{
			get;
			set;
		}

		/// <summary>
		/// Sets the next child.
		/// </summary>
		new Child NextChild
		{
			get;
			set;
		}

		/// <summary>
		/// Add a new child to the collection of children
		/// </summary>
		/// <param name="property">the new child to be added; must not be null</param>
		void AddChild(Property property);
	}
}
