using NPOI.HPSF.Wellknown;
using NPOI.Util;
using System;
using System.Collections;
using System.Text;

namespace NPOI.HPSF
{
	/// <summary>
	/// Represents a section in a {@link PropertySet}.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @author Drew Varner (Drew.Varner allUpIn sc.edu)
	/// @since 2002-02-09
	/// </summary>
	public class Section
	{
		/// Represents an entry in the property list and holds a property's ID and
		/// its offset from the section's beginning.
		private class PropertyListEntry : IComparable
		{
			public int id;

			public int offset;

			public int Length;

			/// Compares this {@link PropertyListEntry} with another one by their
			/// offsets. A {@link PropertyListEntry} is "smaller" than another one if
			/// its offset from the section's begin is smaller.
			///
			/// @see Comparable#CompareTo(java.lang.Object)
			public int CompareTo(object o)
			{
				if (!(o is PropertyListEntry))
				{
					throw new InvalidCastException(o.ToString());
				}
				int num = ((PropertyListEntry)o).offset;
				if (offset < num)
				{
					return -1;
				}
				if (offset == num)
				{
					return 0;
				}
				return 1;
			}

			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(GetType().Name);
				stringBuilder.Append("[id=");
				stringBuilder.Append(id);
				stringBuilder.Append(", offset=");
				stringBuilder.Append(offset);
				stringBuilder.Append(", Length=");
				stringBuilder.Append(Length);
				stringBuilder.Append(']');
				return stringBuilder.ToString();
			}
		}

		/// Maps property IDs To section-private PID strings. These
		/// strings can be found in the property with ID 0.
		protected IDictionary dictionary;

		/// The section's format ID, {@link #GetFormatID}.
		protected ClassID formatID;

		protected long offset;

		protected int size;

		protected Property[] properties;

		/// This member is <c>true</c> if the last call To {@link
		/// #GetPropertyIntValue} or {@link #GetProperty} tried To access a
		/// property that was not available, else <c>false</c>.
		private bool wasNull;

		/// <summary>
		/// Returns the format ID. The format ID is the "type" of the
		/// section. For example, if the format ID of the first {@link
		/// Section} Contains the bytes specified by 
		/// <c>org.apache.poi.hpsf.wellknown.SectionIDMap.SUMMARY_INFORMATION_ID</c>
		/// the section (and thus the property Set) is a SummaryInformation.
		/// </summary>
		/// <value>The format ID.</value>
		public ClassID FormatID => formatID;

		/// <summary>
		/// Gets the offset of the section in the stream.
		/// </summary>
		/// <value>The offset of the section in the stream</value>
		public long OffSet => offset;

		/// <summary>
		/// Returns the section's size in bytes.
		/// </summary>
		/// <value>The section's size in bytes.</value>
		public virtual int Size => size;

		/// <summary>
		/// Returns the number of properties in this section.
		/// </summary>
		/// <value>The number of properties in this section.</value> 
		public virtual int PropertyCount => properties.Length;

		/// <summary>
		/// Returns this section's properties.
		/// </summary>
		/// <value>This section's properties.</value>
		public virtual Property[] Properties => properties;

		/// <summary>
		/// Checks whether the property which the last call To {@link
		/// #GetPropertyIntValue} or {@link #GetProperty} tried To access
		/// was available or not. This information might be important for
		/// callers of {@link #GetPropertyIntValue} since the latter
		/// returns 0 if the property does not exist. Using {@link
		/// #wasNull} the caller can distiguish this case from a property's
		/// real value of 0.
		/// </summary>
		/// <value><c>true</c> if the last call To {@link
		/// #GetPropertyIntValue} or {@link #GetProperty} tried To access a
		/// property that was not available; otherwise, <c>false</c>.</value>
		public virtual bool WasNull => wasNull;

		/// <summary>
		/// Gets the section's dictionary. A dictionary allows an application To
		/// use human-Readable property names instead of numeric property IDs. It
		/// Contains mappings from property IDs To their associated string
		/// values. The dictionary is stored as the property with ID 0. The codepage
		/// for the strings in the dictionary is defined by property with ID 1.
		/// </summary>
		/// <value>the dictionary or null
		///  if the section does not have
		/// a dictionary.</value>
		public virtual IDictionary Dictionary
		{
			get
			{
				if (dictionary == null)
				{
					dictionary = new Hashtable();
				}
				return dictionary;
			}
			set
			{
				dictionary = value;
			}
		}

		/// <summary>
		/// Gets the section's codepage, if any.
		/// </summary>
		/// <value>The section's codepage if one is defined, else -1.</value>
		public int Codepage
		{
			get
			{
				if (GetProperty(1L) == null)
				{
					return -1;
				}
				return (int)GetProperty(1L);
			}
		}

		/// <summary>
		/// Creates an empty and uninitialized {@link Section}.
		/// </summary>
		protected Section()
		{
		}

		/// <summary>
		/// Creates a {@link Section} instance from a byte array.
		/// </summary>
		/// <param name="src">Contains the complete property Set stream.</param>
		/// <param name="offset">The position in the stream that points To the
		/// section's format ID.</param>
		public Section(byte[] src, int offset)
		{
			formatID = new ClassID(src, offset);
			int num = offset + 16;
			this.offset = LittleEndian.GetUInt(src, num);
			num = (int)this.offset;
			size = (int)LittleEndian.GetUInt(src, num);
			num += 4;
			int num2 = (int)LittleEndian.GetUInt(src, num);
			num += 4;
			properties = new Property[num2];
			int num3 = num;
			ArrayList arrayList = new ArrayList(num2);
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyListEntry propertyListEntry = new PropertyListEntry
				{
					id = (int)LittleEndian.GetUInt(src, num3)
				};
				num3 += 4;
				propertyListEntry.offset = (int)LittleEndian.GetUInt(src, num3);
				num3 += 4;
				arrayList.Add(propertyListEntry);
			}
			arrayList.Sort();
			for (int j = 0; j < num2 - 1; j++)
			{
				PropertyListEntry propertyListEntry2 = (PropertyListEntry)arrayList[j];
				PropertyListEntry propertyListEntry3 = (PropertyListEntry)arrayList[j + 1];
				propertyListEntry2.Length = propertyListEntry3.offset - propertyListEntry2.offset;
			}
			if (num2 > 0)
			{
				PropertyListEntry propertyListEntry = (PropertyListEntry)arrayList[num2 - 1];
				propertyListEntry.Length = size - propertyListEntry.offset;
			}
			int num4 = -1;
			IEnumerator enumerator = arrayList.GetEnumerator();
			while (num4 == -1 && enumerator.MoveNext())
			{
				PropertyListEntry propertyListEntry = (PropertyListEntry)enumerator.Current;
				if (propertyListEntry.id == 1)
				{
					int num5 = (int)(this.offset + propertyListEntry.offset);
					long uInt = LittleEndian.GetUInt(src, num5);
					num5 += 4;
					if (uInt != 2)
					{
						throw new HPSFRuntimeException("Value type of property ID 1 is not VT_I2 but " + uInt + ".");
					}
					num4 = LittleEndian.GetUShort(src, num5);
				}
			}
			int num6 = 0;
			IEnumerator enumerator2 = arrayList.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				PropertyListEntry propertyListEntry = (PropertyListEntry)enumerator2.Current;
				Property property = new Property(propertyListEntry.id, src, this.offset + propertyListEntry.offset, propertyListEntry.Length, num4);
				if (property.ID == 1)
				{
					property = new Property(property.ID, property.Type, num4);
				}
				properties[num6++] = property;
			}
			dictionary = (IDictionary)GetProperty(0L);
		}

		/// Returns the value of the property with the specified ID. If
		/// the property is not available, <c>null</c> is returned
		/// and a subsequent call To {@link #wasNull} will return
		/// <c>true</c>.
		///
		/// @param id The property's ID
		///
		/// @return The property's value
		public virtual object GetProperty(long id)
		{
			wasNull = false;
			for (int i = 0; i < properties.Length; i++)
			{
				if (id == properties[i].ID)
				{
					return properties[i].Value;
				}
			}
			wasNull = true;
			return null;
		}

		/// Returns the value of the numeric property with the specified
		/// ID. If the property is not available, 0 is returned. A
		/// subsequent call To {@link #wasNull} will return
		/// <c>true</c> To let the caller distinguish that case from
		/// a real property value of 0.
		///
		/// @param id The property's ID
		///
		/// @return The property's value
		public virtual int GetPropertyIntValue(long id)
		{
			object property = GetProperty(id);
			if (property == null)
			{
				return 0;
			}
			if (!(property is long) && !(property is int))
			{
				throw new HPSFRuntimeException("This property is not an integer type, but " + property.GetType().Name + ".");
			}
			return (int)property;
		}

		/// Returns the value of the bool property with the specified
		/// ID. If the property is not available, <c>false</c> Is
		/// returned. A subsequent call To {@link #wasNull} will return
		/// <c>true</c> To let the caller distinguish that case from
		/// a real property value of <c>false</c>.
		///
		/// @param id The property's ID
		///
		/// @return The property's value
		public virtual bool GetPropertyBooleanValue(int id)
		{
			object property = GetProperty(id);
			if (property != null)
			{
				return (bool)GetProperty(id);
			}
			return false;
		}

		/// <summary>
		/// Returns the PID string associated with a property ID. The ID
		/// is first looked up in the {@link Section}'s private
		/// dictionary. If it is not found there, the method calls {@link
		/// SectionIDMap#GetPIDString}.
		/// </summary>
		/// <param name="pid">The property ID.</param>
		/// <returns>The property ID's string value</returns>
		public string GetPIDString(long pid)
		{
			string text = null;
			if (dictionary != null)
			{
				text = (string)dictionary[pid];
			}
			if (text == null)
			{
				text = SectionIDMap.GetPIDString(FormatID.Bytes, pid);
			}
			if (text == null)
			{
				text = "[undefined]";
			}
			return text;
		}

		/// Checks whether this section is equal To another object. The result Is
		/// <c>false</c> if one of the the following conditions holds:
		///
		/// <ul>
		///
		/// <li>The other object is not a {@link Section}.</li>
		///
		/// <li>The format IDs of the two sections are not equal.</li>
		///
		/// <li>The sections have a different number of properties. However,
		/// properties with ID 1 (codepage) are not counted.</li>
		///
		/// <li>The other object is not a {@link Section}.</li>
		///
		/// <li>The properties have different values. The order of the properties
		/// is irrelevant.</li>
		///
		/// </ul>
		///
		/// @param o The object To Compare this section with
		/// @return <c>true</c> if the objects are equal, <c>false</c> if
		/// not
		public override bool Equals(object o)
		{
			if (o == null || !(o is Section))
			{
				return false;
			}
			Section section = (Section)o;
			if (!section.FormatID.Equals(FormatID))
			{
				return false;
			}
			Property[] array = new Property[Properties.Length];
			Property[] array2 = new Property[section.Properties.Length];
			System.Array.Copy(Properties, 0, array, 0, array.Length);
			System.Array.Copy(section.Properties, 0, array2, 0, array2.Length);
			Property property = null;
			Property property2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				long iD = array[i].ID;
				if (iD == 0)
				{
					property = array[i];
					array = Remove(array, i);
					i--;
				}
				if (iD == 1)
				{
					array = Remove(array, i);
					i--;
				}
			}
			for (int j = 0; j < array2.Length; j++)
			{
				long iD2 = array2[j].ID;
				if (iD2 == 0)
				{
					property2 = array2[j];
					array2 = Remove(array2, j);
					j--;
				}
				if (iD2 == 1)
				{
					array2 = Remove(array2, j);
					j--;
				}
			}
			if (array.Length != array2.Length)
			{
				return false;
			}
			bool flag = true;
			if (property != null && property2 != null)
			{
				Hashtable hashtable = (Hashtable)property.Value;
				Hashtable hashtable2 = (Hashtable)property2.Value;
				flag = (hashtable.Count == hashtable2.Count);
			}
			else if (property != null || property2 != null)
			{
				flag = false;
			}
			if (!flag)
			{
				return false;
			}
			return Util.AreEqual(array, array2);
		}

		/// <summary>
		/// Removes a field from a property array. The resulting array Is
		/// compactified and returned.
		/// </summary>
		/// <param name="pa">The property array.</param>
		/// <param name="i">The index of the field To be Removed.</param>
		/// <returns>the compactified array.</returns>
		private Property[] Remove(Property[] pa, int i)
		{
			Property[] array = new Property[pa.Length - 1];
			if (i > 0)
			{
				System.Array.Copy(pa, 0, array, 0, i);
			}
			System.Array.Copy(pa, i + 1, array, i, array.Length - i);
			return array;
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		public override int GetHashCode()
		{
			long num = 0L;
			num += FormatID.GetHashCode();
			Property[] array = Properties;
			for (int i = 0; i < array.Length; i++)
			{
				num += array[i].GetHashCode();
			}
			return (int)(num & uint.MaxValue);
		}

		/// <summary>
		/// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
		/// </returns>
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Property[] array = Properties;
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append('[');
			stringBuilder.Append("formatID: ");
			stringBuilder.Append(FormatID);
			stringBuilder.Append(", offset: ");
			stringBuilder.Append(OffSet);
			stringBuilder.Append(", propertyCount: ");
			stringBuilder.Append(PropertyCount);
			stringBuilder.Append(", size: ");
			stringBuilder.Append(Size);
			stringBuilder.Append(", properties: [\n");
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString());
				stringBuilder.Append(",\n");
			}
			stringBuilder.Append(']');
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
