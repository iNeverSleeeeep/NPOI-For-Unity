using System;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.Properties
{
	/// <summary>
	/// Trivial extension of Property for POIFSDocuments
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class DirectoryProperty : Property, Parent, Child
	{
		/// <summary>
		/// Directory Property Comparer
		/// </summary>
		public class PropertyComparator : IComparer<Property>
		{
			/// <summary>
			/// Object equality, implemented as object identity
			/// </summary>
			/// <param name="o">Object we're being Compared to</param>
			/// <returns>true if identical, else false</returns>
			public override bool Equals(object o)
			{
				return this == o;
			}

			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			/// <summary>
			/// Compare method. Assumes both parameters are non-null
			/// instances of Property. One property is less than another if
			/// its name is shorter than the other property's name. If the
			/// names are the same length, the property whose name comes
			/// before the other property's name, alphabetically, is less
			/// than the other property.
			/// </summary>
			/// <param name="o1">first object to compare, better be a Property</param>
			/// <param name="o2">second object to compare, better be a Property</param>
			/// <returns>negative value if o1 smaller than o2,
			///         zero           if o1 equals o2,
			///        positive value if o1 bigger than  o2.</returns>
			public int Compare(Property o1, Property o2)
			{
				string value = "_VBA_PROJECT";
				string name = o1.Name;
				string name2 = o2.Name;
				int num = name.Length - name2.Length;
				if (num == 0)
				{
					num = (name.Equals(value, StringComparison.CurrentCulture) ? 1 : (name2.Equals(value, StringComparison.CurrentCulture) ? (-1) : ((name.StartsWith("__", StringComparison.Ordinal) && name2.StartsWith("__", StringComparison.Ordinal)) ? string.Compare(name, name2, StringComparison.OrdinalIgnoreCase) : (name.StartsWith("__", StringComparison.Ordinal) ? 1 : ((!name2.StartsWith("__", StringComparison.Ordinal)) ? string.Compare(name, name2, StringComparison.OrdinalIgnoreCase) : (-1))))));
				}
				return num;
			}
		}

		private List<Property> _children;

		private List<string> _children_names;

		/// <summary>
		/// Gets a value indicating whether this instance is directory.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if a directory type Property; otherwise, <c>false</c>.
		/// </value>
		public override bool IsDirectory => true;

		/// <summary>
		/// Get an iterator over the children of this Parent; all elements
		/// are instances of Property.
		/// </summary>
		/// <value>Iterator of children; may refer to an empty collection</value>
		public IEnumerator<Property> Children => _children.GetEnumerator();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Properties.DirectoryProperty" /> class.
		/// </summary>
		/// <param name="name">the name of the directory</param>
		public DirectoryProperty(string name)
		{
			_children = new List<Property>();
			_children_names = new List<string>();
			base.Name = name;
			Size = 0;
			base.PropertyType = 1;
			base.StartBlock = 0;
			base.NodeColor = 1;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.POIFS.Properties.DirectoryProperty" /> class.
		/// </summary>
		/// <param name="index">index number</param>
		/// <param name="array">byte data</param>
		/// <param name="offset">offset into byte data</param>
		public DirectoryProperty(int index, byte[] array, int offset)
			: base(index, array, offset)
		{
			_children = new List<Property>();
			_children_names = new List<string>();
		}

		/// <summary>
		/// Change a Property's name
		/// </summary>
		/// <param name="property">the Property whose name Is being Changed.</param>
		/// <param name="newName">the new name for the Property</param>
		/// <returns>true if the name Change could be made, else false</returns>
		public bool ChangeName(Property property, string newName)
		{
			string name = property.Name;
			property.Name = newName;
			string name2 = property.Name;
			if (_children_names.Contains(name2))
			{
				property.Name = name;
				return false;
			}
			_children_names.Add(name2);
			_children_names.Remove(name);
			return true;
		}

		/// <summary>
		/// Delete a Property
		/// </summary>
		/// <param name="property">the Property being Deleted</param>
		/// <returns>true if the Property could be Deleted, else false</returns>
		public bool DeleteChild(Property property)
		{
			bool flag = _children.Remove(property);
			if (flag)
			{
				_children_names.Remove(property.Name);
			}
			return flag;
		}

		/// <summary>
		/// Perform whatever activities need to be performed prior to
		/// writing
		/// </summary>
		public override void PreWrite()
		{
			if (_children.Count > 0)
			{
				Property[] array = new Property[_children.Count];
				_children.CopyTo(array, 0);
				Array.Sort(array, new PropertyComparator());
				int num = array.Length / 2;
				base.ChildProperty = array[num].Index;
				array[0].PreviousChild = null;
				array[0].NextChild = null;
				for (int i = 1; i < num; i++)
				{
					array[i].PreviousChild = array[i - 1];
					array[i].NextChild = null;
				}
				if (num != 0)
				{
					array[num].PreviousChild = array[num - 1];
				}
				if (num != array.Length - 1)
				{
					array[num].NextChild = array[num + 1];
					for (int j = num + 1; j < array.Length - 1; j++)
					{
						array[j].PreviousChild = null;
						array[j].NextChild = array[j + 1];
					}
					array[array.Length - 1].PreviousChild = null;
					array[array.Length - 1].NextChild = null;
				}
				else
				{
					array[num].NextChild = null;
				}
			}
		}

		/// <summary>
		/// Add a new child to the collection of children
		/// </summary>
		/// <param name="property">the new child to be added; must not be null</param>
		public void AddChild(Property property)
		{
			string name = property.Name;
			if (_children_names.Contains(name))
			{
				throw new IOException("Duplicate name \"" + name + "\"");
			}
			_children_names.Add(name);
			_children.Add(property);
		}
	}
}
