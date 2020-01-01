using NPOI.Util;
using System;
using System.Collections;
using System.Text;

namespace NPOI.HPSF
{
	/// <summary>
	/// Maintains the instances of {@link CustomProperty} that belong To a
	/// {@link DocumentSummaryInformation}. The class maintains the names of the
	/// custom properties in a dictionary. It implements the {@link Map} interface
	/// and by this provides a simplified view on custom properties: A property's
	/// name is the key that maps To a typed value. This implementation hides
	/// property IDs from the developer and regards the property names as keys To
	/// typed values.
	/// While this class provides a simple API To custom properties, it ignores
	/// the fact that not names, but IDs are the real keys To properties. Under the
	/// hood this class maintains a 1:1 relationship between IDs and names. Therefore
	/// you should not use this class To process property Sets with several IDs
	/// mapping To the same name or with properties without a name: the result will
	/// contain only a subSet of the original properties. If you really need To deal
	/// such property Sets, use HPSF's low-level access methods.
	/// An application can call the {@link #isPure} method To check whether a
	/// property Set parsed by {@link CustomProperties} is still pure (i.e.
	/// unmodified) or whether one or more properties have been dropped.
	/// This class is not thRead-safe; concurrent access To instances of this
	/// class must be syncronized.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2006-02-09
	/// </summary>
	public class CustomProperties : Hashtable
	{
		/// Maps property IDs To property names.
		private Hashtable dictionaryIDToName = new Hashtable();

		/// Maps property names To property IDs.
		private Hashtable dictionaryNameToID = new Hashtable();

		/// Tells whether this object is pure or not.
		private bool isPure = true;

		/// <summary>
		/// Gets the <see cref="T:System.Object" /> with the specified name.
		/// </summary>
		/// <value>the value or 
		/// <c>null</c>
		///  if a value with the specified
		/// name is not found in the custom properties.</value>
		public object this[string name]
		{
			get
			{
				object obj = dictionaryNameToID[name];
				if (obj == null)
				{
					IEnumerator enumerator = dictionaryNameToID.GetEnumerator();
					while (enumerator.MoveNext())
					{
						string s = ((DictionaryEntry)enumerator.Current).Key as string;
						int num = Codepage;
						if (num < 0)
						{
							num = 1200;
						}
						byte[] bytes = Encoding.GetEncoding(num).GetBytes(s);
						byte[] bytes2 = Encoding.UTF8.GetBytes(name);
						if (Arrays.Equals(bytes, bytes2))
						{
							obj = ((DictionaryEntry)enumerator.Current).Value;
						}
					}
					if (obj == null)
					{
						return null;
					}
				}
				long num2 = (long)obj;
				return ((CustomProperty)base[num2])?.Value;
			}
		}

		/// <summary>
		/// Gets the dictionary which Contains IDs and names of the named custom
		/// properties.
		/// </summary>
		/// <value>The dictionary.</value>
		public IDictionary Dictionary => dictionaryIDToName;

		/// <summary>
		/// Gets or sets the codepage.
		/// </summary>
		/// <value>The codepage.</value>
		public int Codepage
		{
			get
			{
				int num = -1;
				IEnumerator enumerator = Values.GetEnumerator();
				while (num == -1 && enumerator.MoveNext())
				{
					CustomProperty customProperty = (CustomProperty)enumerator.Current;
					if (customProperty.ID == 1)
					{
						num = (int)customProperty.Value;
					}
				}
				return num;
			}
			set
			{
				MutableProperty mutableProperty = new MutableProperty();
				mutableProperty.ID = 1L;
				mutableProperty.Type = 2L;
				mutableProperty.Value = value;
				Put(new CustomProperty(mutableProperty));
			}
		}

		/// <summary>
		/// Tells whether this {@link CustomProperties} instance is pure or one or
		/// more properties of the underlying low-level property Set has been
		/// dropped.
		/// </summary>
		/// <value><c>true</c> if this instance is pure; otherwise, <c>false</c>.</value>
		public bool IsPure
		{
			get
			{
				return isPure;
			}
			set
			{
				isPure = value;
			}
		}

		/// <summary>
		/// Puts a {@link CustomProperty} into this map. It is assumed that the
		/// {@link CustomProperty} alReady has a valid ID. Otherwise use
		/// {@link #Put(CustomProperty)}.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="cp">The custom property.</param>
		/// <returns></returns>
		public CustomProperty Put(string name, CustomProperty cp)
		{
			if (string.IsNullOrEmpty(name))
			{
				isPure = false;
				return null;
			}
			if (name == null)
			{
				throw new ArgumentException("The name of a custom property must be a String, but it is a " + name.GetType().Name);
			}
			if (!name.Equals(cp.Name))
			{
				throw new ArgumentException("Parameter \"name\" (" + name + ") and custom property's name (" + cp.Name + ") do not match.");
			}
			long iD = cp.ID;
			object obj = dictionaryNameToID[name];
			if (obj != null)
			{
				dictionaryIDToName.Remove(obj);
			}
			dictionaryNameToID[name] = iD;
			dictionaryIDToName[iD] = name;
			if (obj != null)
			{
				base.Remove(obj);
			}
			base[iD] = cp;
			return cp;
		}

		/// Returns a set of all the names of our
		///  custom properties. Equivalent to 
		///  {@link #nameSet()}
		public ICollection KeySet()
		{
			return dictionaryNameToID.Keys;
		}

		/// Returns a set of all the names of our
		///  custom properties
		public ICollection NameSet()
		{
			return dictionaryNameToID.Keys;
		}

		/// Returns a set of all the IDs of our
		///  custom properties
		public ICollection IdSet()
		{
			return dictionaryNameToID.Keys;
		}

		/// <summary>
		/// Puts a {@link CustomProperty} that has not yet a valid ID into this
		/// map. The method will allocate a suitable ID for the custom property:
		/// <ul>
		/// 	<li>If there is alReady a property with the same name, take the ID
		/// of that property.</li>
		/// 	<li>Otherwise Find the highest ID and use its value plus one.</li>
		/// </ul>
		/// </summary>
		/// <param name="customProperty">The custom property.</param>
		/// <returns>If the was alReady a property with the same name, the</returns>
		private object Put(CustomProperty customProperty)
		{
			string name = customProperty.Name;
			object obj = dictionaryNameToID[name];
			if (obj != null)
			{
				customProperty.ID = (long)obj;
			}
			else
			{
				long num = 1L;
				IEnumerator enumerator = dictionaryIDToName.Keys.GetEnumerator();
				while (enumerator.MoveNext())
				{
					long num2 = (long)enumerator.Current;
					if (num2 > num)
					{
						num = num2;
					}
				}
				customProperty.ID = num + 1;
			}
			return Put(name, customProperty);
		}

		/// <summary>
		/// Removes a custom property.
		/// </summary>
		/// <param name="name">The name of the custom property To Remove</param>
		/// <returns>The Removed property or 
		/// <c>null</c>
		///  if the specified property was not found.</returns>
		public object Remove(string name)
		{
			if (dictionaryNameToID[name] == null)
			{
				return null;
			}
			long num = (long)dictionaryNameToID[name];
			dictionaryIDToName.Remove(num);
			dictionaryNameToID.Remove(name);
			CustomProperty result = (CustomProperty)this[num];
			Remove(num);
			return result;
		}

		/// <summary>
		/// Adds a named string property.
		/// </summary>
		/// <param name="name">The property's name.</param>
		/// <param name="value">The property's value.</param>
		/// <returns>the property that was stored under the specified name before, or
		/// <c>null</c>
		///  if there was no such property before.</returns>
		public object Put(string name, string value)
		{
			MutableProperty mutableProperty = new MutableProperty();
			mutableProperty.ID = -1L;
			mutableProperty.Type = 31L;
			mutableProperty.Value = value;
			CustomProperty customProperty = new CustomProperty(mutableProperty, name);
			return Put(customProperty);
		}

		/// <summary>
		/// Adds a named long property
		/// </summary>
		/// <param name="name">The property's name.</param>
		/// <param name="value">The property's value.</param>
		/// <returns>the property that was stored under the specified name before, or
		/// <c>null</c>
		///  if there was no such property before.</returns>
		public object Put(string name, long value)
		{
			MutableProperty mutableProperty = new MutableProperty();
			mutableProperty.ID = -1L;
			mutableProperty.Type = 20L;
			mutableProperty.Value = value;
			CustomProperty customProperty = new CustomProperty(mutableProperty, name);
			return Put(customProperty);
		}

		/// <summary>
		/// Adds a named double property.
		/// </summary>
		/// <param name="name">The property's name.</param>
		/// <param name="value">The property's value.</param>
		/// <returns>the property that was stored under the specified name before, or
		/// <c>null</c>
		///  if there was no such property before.</returns>
		public object Put(string name, double value)
		{
			MutableProperty mutableProperty = new MutableProperty();
			mutableProperty.ID = -1L;
			mutableProperty.Type = 5L;
			mutableProperty.Value = value;
			CustomProperty customProperty = new CustomProperty(mutableProperty, name);
			return Put(customProperty);
		}

		/// <summary>
		/// Adds a named integer property.
		/// </summary>
		/// <param name="name">The property's name.</param>
		/// <param name="value">The property's value.</param>
		/// <returns>the property that was stored under the specified name before, or
		/// <c>null</c>
		///  if there was no such property before.</returns>
		public object Put(string name, int value)
		{
			MutableProperty mutableProperty = new MutableProperty();
			mutableProperty.ID = -1L;
			mutableProperty.Type = 3L;
			mutableProperty.Value = value;
			CustomProperty customProperty = new CustomProperty(mutableProperty, name);
			return Put(customProperty);
		}

		/// <summary>
		/// Adds a named bool property.
		/// </summary>
		/// <param name="name">The property's name.</param>
		/// <param name="value">The property's value.</param>
		/// <returns>the property that was stored under the specified name before, or
		/// <c>null</c>
		///  if there was no such property before.</returns>
		public object Put(string name, bool value)
		{
			MutableProperty mutableProperty = new MutableProperty();
			mutableProperty.ID = -1L;
			mutableProperty.Type = 11L;
			mutableProperty.Value = value;
			CustomProperty customProperty = new CustomProperty(mutableProperty, name);
			return Put(customProperty);
		}

		/// <summary>
		/// Adds a named date property.
		/// </summary>
		/// <param name="name">The property's name.</param>
		/// <param name="value">The property's value.</param>
		/// <returns>the property that was stored under the specified name before, or
		/// <c>null</c>
		///  if there was no such property before.</returns>
		public object Put(string name, DateTime value)
		{
			MutableProperty mutableProperty = new MutableProperty();
			mutableProperty.ID = -1L;
			mutableProperty.Type = 64L;
			mutableProperty.Value = value;
			CustomProperty customProperty = new CustomProperty(mutableProperty, name);
			return Put(customProperty);
		}

		/// Checks against both String Name and Long ID
		public override bool ContainsKey(object key)
		{
			if (key is long)
			{
				return base.ContainsKey((long)key);
			}
			if (key is string)
			{
				return base.ContainsKey((long)dictionaryNameToID[key]);
			}
			return false;
		}

		/// Checks against both the property, and its values. 
		public override bool ContainsValue(object value)
		{
			if (value is CustomProperty)
			{
				return base.ContainsValue(value);
			}
			foreach (object value2 in base.Values)
			{
				if ((value2 as CustomProperty).Value == value)
				{
					return true;
				}
			}
			return false;
		}
	}
}
