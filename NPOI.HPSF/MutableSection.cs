using NPOI.Util;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace NPOI.HPSF
{
	/// <summary>
	/// Adds writing capability To the {@link Section} class.
	/// Please be aware that this class' functionality will be merged into the
	/// {@link Section} class at a later time, so the API will Change.
	/// @since 2002-02-20
	/// </summary>
	public class MutableSection : Section
	{
		private class PropertyComparer : IComparer
		{
			int IComparer.Compare(object o1, object o2)
			{
				Property property = (Property)o1;
				Property property2 = (Property)o2;
				if (property.ID < property2.ID)
				{
					return -1;
				}
				if (property.ID == property2.ID)
				{
					return 0;
				}
				return 1;
			}
		}

		/// If the "dirty" flag is true, the section's size must be
		/// (re-)calculated before the section is written.
		private bool dirty = true;

		/// List To assemble the properties. Unfortunately a wrong
		/// decision has been taken when specifying the "properties" field
		/// as an Property[]. It should have been a {@link java.util.List}.
		private ArrayList preprops;

		/// Contains the bytes making out the section. This byte array is
		/// established when the section's size is calculated and can be reused
		/// later. It is valid only if the "dirty" flag is false.
		private byte[] sectionBytes;

		/// <summary>
		/// Returns the section's size in bytes.
		/// </summary>
		/// <value>The section's size in bytes.</value>
		public override int Size
		{
			get
			{
				if (dirty)
				{
					try
					{
						size = CalcSize();
						dirty = false;
					}
					catch (Exception)
					{
						throw;
					}
				}
				return size;
			}
		}

		/// <summary>
		/// OverWrites the base class' method To cope with a redundancy:
		/// the property count is maintained in a separate member variable, but
		/// shouldn't.
		/// </summary>
		/// <value>The number of properties in this section.</value>
		public override int PropertyCount => preprops.Count;

		/// <summary>
		/// Returns this section's properties.
		/// </summary>
		/// <value>This section's properties.</value>
		public override Property[] Properties
		{
			get
			{
				EnsureProperties();
				return properties;
			}
		}

		/// <summary>
		/// Sets the section's dictionary. All keys in the dictionary must be
		/// {@link java.lang.long} instances, all values must be
		/// {@link java.lang.String}s. This method overWrites the properties with IDs
		/// 0 and 1 since they are reserved for the dictionary and the dictionary's
		/// codepage. Setting these properties explicitly might have surprising
		/// effects. An application should never do this but always use this
		/// method.
		/// </summary>
		/// <value>
		/// the dictionary
		/// </value>
		public override IDictionary Dictionary
		{
			get
			{
				return dictionary;
			}
			set
			{
				if (value != null)
				{
					IEnumerator enumerator = value.Keys.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (!(enumerator.Current is long) && !(enumerator.Current is int))
						{
							throw new IllegalPropertySetDataException("Dictionary keys must be of type long. but it's " + enumerator.Current + "," + enumerator.Current.GetType().Name + " now");
						}
					}
					dictionary = value;
					SetProperty(0, -1L, value);
					if (GetProperty(1L) == null)
					{
						SetProperty(1, 2L, 1200);
					}
				}
				else
				{
					RemoveProperty(0L);
				}
			}
		}

		/// <summary>
		/// Gets the section's codepage, if any.
		/// </summary>
		/// <value>The section's codepage if one is defined, else -1.</value>
		public new int Codepage
		{
			get
			{
				return base.Codepage;
			}
			set
			{
				SetProperty(1, 2L, value);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.MutableSection" /> class.
		/// </summary>
		public MutableSection()
		{
			dirty = true;
			formatID = null;
			offset = -1L;
			preprops = new ArrayList();
		}

		/// <summary>
		/// Constructs a <c>MutableSection</c> by doing a deep copy of an
		/// existing <c>Section</c>. All nested <c>Property</c>
		/// instances, will be their mutable counterparts in the new
		/// <c>MutableSection</c>.
		/// </summary>
		/// <param name="s">The section Set To copy</param>
		public MutableSection(Section s)
		{
			SetFormatID(s.FormatID);
			Property[] properties = s.Properties;
			MutableProperty[] array = new MutableProperty[properties.Length];
			for (int i = 0; i < properties.Length; i++)
			{
				array[i] = new MutableProperty(properties[i]);
			}
			SetProperties(array);
			Dictionary = s.Dictionary;
		}

		/// <summary>
		/// Sets the section's format ID.
		/// </summary>
		/// <param name="formatID">The section's format ID</param>
		public void SetFormatID(ClassID formatID)
		{
			base.formatID = formatID;
		}

		/// <summary>
		/// Sets the section's format ID.
		/// </summary>
		/// <param name="formatID">The section's format ID as a byte array. It components
		/// are in big-endian format.</param>
		public void SetFormatID(byte[] formatID)
		{
			ClassID classID = base.FormatID;
			if (classID == null)
			{
				classID = new ClassID();
				SetFormatID(classID);
			}
			classID.Bytes = formatID;
		}

		/// <summary>
		/// Sets this section's properties. Any former values are overwritten.
		/// </summary>
		/// <param name="properties">This section's new properties.</param>
		public void SetProperties(Property[] properties)
		{
			base.properties = properties;
			preprops = new ArrayList();
			for (int i = 0; i < properties.Length; i++)
			{
				preprops.Add(properties[i]);
			}
			dirty = true;
		}

		/// <summary>
		/// Sets the string value of the property with the specified ID.
		/// </summary>
		/// <param name="id">The property's ID</param>
		/// <param name="value">The property's value. It will be written as a Unicode
		/// string.</param>
		public void SetProperty(int id, string value)
		{
			SetProperty(id, 31L, value);
			dirty = true;
		}

		/// <summary>
		/// Sets the int value of the property with the specified ID.
		/// </summary>
		/// <param name="id">The property's ID</param>
		/// <param name="value">The property's value.</param>
		public void SetProperty(int id, int value)
		{
			SetProperty(id, 3L, value);
			dirty = true;
		}

		/// <summary>
		/// Sets the long value of the property with the specified ID.
		/// </summary>
		/// <param name="id">The property's ID</param>
		/// <param name="value">The property's value.</param>
		public void SetProperty(int id, long value)
		{
			SetProperty(id, 20L, value);
			dirty = true;
		}

		/// <summary>
		/// Sets the bool value of the property with the specified ID.
		/// </summary>
		/// <param name="id">The property's ID</param>
		/// <param name="value">The property's value.</param>
		public void SetProperty(int id, bool value)
		{
			SetProperty(id, 11L, value);
			dirty = true;
		}

		/// <summary>
		/// Sets the value and the variant type of the property with the
		/// specified ID. If a property with this ID is not yet present in
		/// the section, it will be Added. An alReady present property with
		/// the specified ID will be overwritten. A default mapping will be
		/// used To choose the property's type.
		/// </summary>
		/// <param name="id">The property's ID.</param>
		/// <param name="variantType">The property's variant type.</param>
		/// <param name="value">The property's value.</param>
		public void SetProperty(int id, long variantType, object value)
		{
			MutableProperty mutableProperty = new MutableProperty();
			mutableProperty.ID = id;
			mutableProperty.Type = variantType;
			mutableProperty.Value = value;
			SetProperty(mutableProperty);
			dirty = true;
		}

		/// <summary>
		/// Sets the property.
		/// </summary>
		/// <param name="p">The property To be Set.</param>
		public void SetProperty(Property p)
		{
			long iD = p.ID;
			RemoveProperty(iD);
			preprops.Add(p);
			dirty = true;
		}

		/// <summary>
		/// Removes the property.
		/// </summary>
		/// <param name="id">The ID of the property To be Removed</param>
		public void RemoveProperty(long id)
		{
			IEnumerator enumerator = preprops.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((Property)enumerator.Current).ID == id)
				{
					preprops.Remove(enumerator.Current);
					break;
				}
			}
			dirty = true;
		}

		/// <summary>
		/// Sets the value of the bool property with the specified
		/// ID.
		/// </summary>
		/// <param name="id">The property's ID</param>
		/// <param name="value">The property's value</param>
		protected void SetPropertyBooleanValue(int id, bool value)
		{
			SetProperty(id, 11L, value);
		}

		/// <summary>
		/// Calculates the section's size. It is the sum of the Lengths of the
		/// section's header (8), the properties list (16 times the number of
		/// properties) and the properties themselves.
		/// </summary>
		/// <returns>the section's Length in bytes.</returns>
		private int CalcSize()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Write(memoryStream);
				sectionBytes = Util.Pad4(memoryStream.ToArray());
				return sectionBytes.Length;
			}
		}

		/// <summary>
		/// Writes this section into an output stream.
		/// Internally this is done by writing into three byte array output
		/// streams: one for the properties, one for the property list and one for
		/// the section as such. The two former are Appended To the latter when they
		/// have received all their data.
		/// </summary>
		/// <param name="out1">The stream To Write into.</param>
		/// <returns>The number of bytes written, i.e. the section's size.</returns>
		public int Write(Stream out1)
		{
			if (!dirty && sectionBytes != null)
			{
				out1.Write(sectionBytes, 0, sectionBytes.Length);
				return sectionBytes.Length;
			}
			using (MemoryStream memoryStream2 = new MemoryStream())
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					int num = 0;
					num += 8 + PropertyCount * 2 * 4;
					int num2 = -1;
					if (GetProperty(0L) != null)
					{
						object property = GetProperty(1L);
						if (property != null)
						{
							if (!(property is int))
							{
								throw new IllegalPropertySetDataException("The codepage property (ID = 1) must be an Integer object.");
							}
						}
						else
						{
							SetProperty(1, 2L, 1200);
						}
						num2 = Codepage;
					}
					preprops.Sort(new PropertyComparer());
					for (int i = 0; i < preprops.Count; i++)
					{
						MutableProperty mutableProperty = (MutableProperty)preprops[i];
						long iD = mutableProperty.ID;
						TypeWriter.WriteUIntToStream(memoryStream, (uint)mutableProperty.ID);
						TypeWriter.WriteUIntToStream(memoryStream, (uint)num);
						if (iD != 0)
						{
							num += mutableProperty.Write(memoryStream2, Codepage);
						}
						else
						{
							if (num2 == -1)
							{
								throw new IllegalPropertySetDataException("Codepage (property 1) is undefined.");
							}
							num += WriteDictionary(memoryStream2, dictionary, num2);
						}
					}
					memoryStream2.Flush();
					memoryStream.Flush();
					byte[] array = memoryStream.ToArray();
					byte[] array2 = memoryStream2.ToArray();
					TypeWriter.WriteToStream(out1, 8 + array.Length + array2.Length);
					TypeWriter.WriteToStream(out1, PropertyCount);
					out1.Write(array, 0, array.Length);
					out1.Write(array2, 0, array2.Length);
					return 8 + array.Length + array2.Length;
				}
			}
		}

		/// <summary>
		/// Writes the section's dictionary
		/// </summary>
		/// <param name="out1">The output stream To Write To.</param>
		/// <param name="dictionary">The dictionary.</param>
		/// <param name="codepage">The codepage to be used to Write the dictionary items.</param>
		/// <returns>The number of bytes written</returns>
		/// <remarks>
		/// see MSDN KB: http://msdn.microsoft.com/en-us/library/aa380065(VS.85).aspx
		/// </remarks>
		private static int WriteDictionary(Stream out1, IDictionary dictionary, int codepage)
		{
			int num = TypeWriter.WriteUIntToStream(out1, (uint)dictionary.Count);
			IEnumerator enumerator = dictionary.Keys.GetEnumerator();
			while (enumerator.MoveNext())
			{
				long num2 = Convert.ToInt64(enumerator.Current, CultureInfo.InvariantCulture);
				string text = (string)dictionary[num2];
				if (text == null)
				{
					text = (string)dictionary[(int)num2];
				}
				if (codepage == 1200)
				{
					int num3 = text.Length + 1;
					if (num3 % 2 == 1)
					{
						num3++;
					}
					num += TypeWriter.WriteUIntToStream(out1, (uint)num2);
					num += TypeWriter.WriteUIntToStream(out1, (uint)num3);
					byte[] bytes = Encoding.GetEncoding(codepage).GetBytes(text);
					for (int i = 0; i < bytes.Length; i++)
					{
						out1.WriteByte(bytes[i]);
						num++;
					}
					for (num3 -= text.Length; num3 > 0; num3--)
					{
						out1.WriteByte(0);
						out1.WriteByte(0);
						num += 2;
					}
				}
				else
				{
					num += TypeWriter.WriteUIntToStream(out1, (uint)num2);
					num += TypeWriter.WriteUIntToStream(out1, (uint)(text.Length + 1));
					try
					{
						byte[] bytes2 = Encoding.GetEncoding(codepage).GetBytes(text);
						for (int j = 0; j < bytes2.Length; j++)
						{
							out1.WriteByte(bytes2[j]);
							num++;
						}
					}
					catch (Exception reason)
					{
						throw new IllegalPropertySetDataException(reason);
					}
					out1.WriteByte(0);
					num++;
				}
			}
			return num;
		}

		/// <summary>
		/// Ensures the properties.
		/// </summary>
		public void EnsureProperties()
		{
			properties = (Property[])preprops.ToArray(typeof(Property));
		}

		/// <summary>
		/// Gets a property.
		/// </summary>
		/// <param name="id">The ID of the property To Get</param>
		/// <returns>The property or null  if there is no such property</returns>
		public override object GetProperty(long id)
		{
			EnsureProperties();
			return base.GetProperty(id);
		}

		/// <summary>
		/// Sets the property.
		/// </summary>
		/// <param name="id">The property ID.</param>
		/// <param name="value">The property's value. The value's class must be one of those
		/// supported by HPSF.</param>
		public void SetProperty(int id, object value)
		{
			if (value is string)
			{
				SetProperty(id, (string)value);
			}
			else if (value is long)
			{
				SetProperty(id, (long)value);
			}
			else if (value is int)
			{
				SetProperty(id, value);
			}
			else if (value is short)
			{
				SetProperty(id, (short)value);
			}
			else if (value is bool)
			{
				SetProperty(id, (bool)value);
			}
			else
			{
				if (!(value is DateTime))
				{
					throw new HPSFRuntimeException("HPSF does not support properties of type " + value.GetType().Name + ".");
				}
				SetProperty(id, 64L, value);
			}
		}

		/// <summary>
		/// Removes all properties from the section including 0 (dictionary) and
		/// 1 (codepage).
		/// </summary>
		public void Clear()
		{
			Property[] properties = Properties;
			foreach (Property property in properties)
			{
				RemoveProperty(property.ID);
			}
		}
	}
}
