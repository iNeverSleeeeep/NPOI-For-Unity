using NPOI.Util;
using System;
using System.Collections;
using System.Text;

namespace NPOI.HPSF
{
	/// <summary>
	/// A property in a {@link Section} of a {@link PropertySet}.
	/// The property's ID gives the property a meaning
	/// in the context of its {@link Section}. Each {@link Section} spans
	/// its own name space of property IDs.
	/// The property's type determines how its
	/// value  is interpreted. For example, if the type Is
	/// {@link Variant#VT_LPSTR} (byte string), the value consists of a
	/// DWord telling how many bytes the string Contains. The bytes follow
	/// immediately, including any null bytes that terminate the
	/// string. The type {@link Variant#VT_I4} denotes a four-byte integer
	/// value, {@link Variant#VT_FILETIME} some DateTime and time (of a
	/// file).
	/// Please note that not all {@link Variant} types yet. This might Change
	/// over time but largely depends on your feedback so that the POI team knows
	/// which variant types are really needed. So please feel free To submit error
	/// reports or patches for the types you need.
	/// Microsoft documentation: 
	/// <a href="http://msdn.microsoft.com/library/en-us/stg/stg/property_Set_display_name_dictionary.asp?frame=true">
	/// Property Set Display Name Dictionary</a>
	/// .
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @author Drew Varner (Drew.Varner InAndAround sc.edu)
	/// @see Section
	/// @see Variant
	/// @since 2002-02-09
	/// </summary>
	public class Property
	{
		/// The property's ID. 
		protected long id;

		/// The property's type. 
		protected long type;

		/// The property's value. 
		protected object value;

		/// Returns the property's ID.
		///
		/// @return The ID value
		public virtual long ID
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		/// Returns the property's type.
		///
		/// @return The type value
		public virtual long Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		/// <summary>
		/// Gets the property's value.
		/// </summary>
		/// <value>The property's value</value>
		public virtual object Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>
		/// Gets the property's size in bytes. This is always a multiple of
		/// 4.
		/// </summary>
		/// <value>the property's size in bytes</value>
		public int Count
		{
			get
			{
				int num = Variant.GetVariantLength(type);
				if (num >= 0)
				{
					return num;
				}
				if (num == -2)
				{
					throw new WritingNotSupportedException(type, null);
				}
				int num2 = 4;
				switch (type)
				{
				case 30L:
				{
					int num3 = ((string)value).Length + 1;
					int num4 = num3 % num2;
					if (num4 > 0)
					{
						num3 += num2 - num4;
					}
					num += num3;
					break;
				}
				default:
					throw new WritingNotSupportedException(type, value);
				case 0L:
					break;
				}
				return num;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.Property" /> class.
		/// </summary>
		/// <param name="id">the property's ID.</param>
		/// <param name="type">the property's type, see {@link Variant}.</param>
		/// <param name="value">the property's value. Only certain types are allowed, see
		/// {@link Variant}.</param>
		public Property(long id, long type, object value)
		{
			this.id = id;
			this.type = type;
			this.value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.Property" /> class.
		/// </summary>
		/// <param name="id">The property's ID.</param>
		/// <param name="src">The bytes the property Set stream consists of.</param>
		/// <param name="offset">The property's type/value pair's offset in the
		/// section.</param>
		/// <param name="Length">The property's type/value pair's Length in bytes.</param>
		/// <param name="codepage">The section's and thus the property's
		/// codepage. It is needed only when Reading string values</param>
		public Property(long id, byte[] src, long offset, int Length, int codepage)
		{
			this.id = id;
			if (id == 0)
			{
				value = ReadDictionary(src, offset, Length, codepage);
			}
			else
			{
				int num = (int)offset;
				type = LittleEndian.GetUInt(src, num);
				num += 4;
				try
				{
					value = VariantSupport.Read(src, num, Length, (int)type, codepage);
				}
				catch (UnsupportedVariantTypeException ex)
				{
					VariantSupport.WriteUnsupportedTypeMessage(ex);
					value = ex.Value;
				}
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.Property" /> class.
		/// </summary>
		protected Property()
		{
		}

		/// <summary>
		/// Reads the dictionary.
		/// </summary>
		/// <param name="src">The byte array containing the bytes making out the dictionary.</param>
		/// <param name="offset">At this offset within src the dictionary starts.</param>
		/// <param name="Length">The dictionary Contains at most this many bytes.</param>
		/// <param name="codepage">The codepage of the string values.</param>
		/// <returns>The dictonary</returns>
		protected IDictionary ReadDictionary(byte[] src, long offset, int Length, int codepage)
		{
			if (offset >= 0 && offset <= src.Length)
			{
				int num = (int)offset;
				long uInt = LittleEndian.GetUInt(src, num);
				num += 4;
				Hashtable hashtable = new Hashtable((int)uInt, 1f);
				try
				{
					for (int i = 0; i < uInt; i++)
					{
						long uInt2 = LittleEndian.GetUInt(src, num);
						num += 4;
						long num2 = LittleEndian.GetUInt(src, num);
						num += 4;
						StringBuilder stringBuilder = new StringBuilder();
						switch (codepage)
						{
						case -1:
							stringBuilder.Append(Encoding.UTF8.GetString(src, num, (int)num2));
							break;
						case 1200:
						{
							int num3 = (int)(num2 * 2);
							byte[] array = new byte[num3];
							for (int j = 0; j < num3; j++)
							{
								array[j] = src[num + j];
							}
							stringBuilder.Append(Encoding.GetEncoding(codepage).GetString(array, 0, num3 - 2));
							break;
						}
						default:
							stringBuilder.Append(Encoding.GetEncoding(codepage).GetString(src, num, (int)num2));
							break;
						}
						while (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] == '\0')
						{
							stringBuilder.Length--;
						}
						if (codepage == 1200)
						{
							if (num2 % 2 == 1)
							{
								num2++;
							}
							num += (int)(num2 + num2);
						}
						else
						{
							num += (int)num2;
						}
						hashtable[uInt2] = stringBuilder.ToString();
					}
					return hashtable;
				}
				catch (Exception exception)
				{
					POILogger logger = POILogFactory.GetLogger(typeof(Property));
					logger.Log(5, "The property Set's dictionary Contains bogus data. All dictionary entries starting with the one with ID " + id + " will be ignored.", exception);
					return hashtable;
				}
			}
			throw new HPSFRuntimeException("Illegal offset " + offset + " while HPSF stream Contains " + Length + " bytes.");
		}

		/// <summary>
		/// Compares two properties.
		/// Please beware that a property with
		/// ID == 0 is a special case: It does not have a type, and its value is the
		/// section's dictionary. Another special case are strings: Two properties
		/// may have the different types Variant.VT_LPSTR and Variant.VT_LPWSTR;
		/// </summary>
		/// <param name="o">The o.</param>
		/// <returns></returns>
		public override bool Equals(object o)
		{
			if (!(o is Property))
			{
				return false;
			}
			Property property = (Property)o;
			object obj = property.Value;
			long iD = property.ID;
			if (id != iD || (id != 0 && !TypesAreEqual(this.type, property.Type)))
			{
				return false;
			}
			if (value == null && obj == null)
			{
				return true;
			}
			if (value == null || obj == null)
			{
				return false;
			}
			Type type = value.GetType();
			Type type2 = obj.GetType();
			if (!type.IsAssignableFrom(type2) && !type2.IsAssignableFrom(type))
			{
				return false;
			}
			if (value is byte[])
			{
				return Arrays.Equals((byte[])value, (byte[])obj);
			}
			return value.Equals(obj);
		}

		/// <summary>
		/// Typeses the are equal.
		/// </summary>
		/// <param name="t1">The t1.</param>
		/// <param name="t2">The t2.</param>
		/// <returns></returns>
		private bool TypesAreEqual(long t1, long t2)
		{
			if (t1 == t2 || (t1 == 30 && t2 == 31) || (t2 == 30 && t1 == 31))
			{
				return true;
			}
			return false;
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
			num += id;
			num += type;
			if (value != null)
			{
				num += value.GetHashCode();
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
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append('[');
			stringBuilder.Append("id: ");
			stringBuilder.Append(ID);
			stringBuilder.Append(", type: ");
			stringBuilder.Append(GetType());
			object obj = Value;
			stringBuilder.Append(", value: ");
			if (obj is string)
			{
				stringBuilder.Append(obj.ToString());
				string text = obj.ToString();
				int length = text.Length;
				byte[] array = new byte[length * 2];
				for (int i = 0; i < length; i++)
				{
					char c = text[i];
					byte b = (byte)((c & 0xFF00) >> 8);
					byte b2 = (byte)(c & 0xFF);
					array[i * 2] = b;
					array[i * 2 + 1] = b2;
				}
				stringBuilder.Append(" [");
				if (array.Length > 0)
				{
					string text2 = HexDump.Dump(array, 0L, 0);
					stringBuilder.Append(text2);
				}
				stringBuilder.Append("]");
			}
			else if (obj is byte[])
			{
				byte[] array2 = (byte[])obj;
				if (array2.Length > 0)
				{
					string text3 = HexDump.Dump(array2, 0L, 0);
					stringBuilder.Append(text3);
				}
			}
			else
			{
				stringBuilder.Append(obj.ToString());
			}
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}
	}
}
