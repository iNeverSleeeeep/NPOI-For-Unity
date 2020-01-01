using NPOI.HPSF.Wellknown;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NPOI.HPSF
{
	/// <summary>
	/// Represents a property Set in the Horrible Property Set Format
	/// (HPSF). These are usually metadata of a Microsoft Office
	/// document.
	/// An application that wants To access these metadata should Create
	/// an instance of this class or one of its subclasses by calling the
	/// factory method {@link PropertySetFactory#Create} and then retrieve
	/// the information its needs by calling appropriate methods.
	/// {@link PropertySetFactory#Create} does its work by calling one
	/// of the constructors {@link PropertySet#PropertySet(InputStream)} or
	/// {@link PropertySet#PropertySet(byte[])}. If the constructor's
	/// argument is not in the Horrible Property Set Format, i.e. not a
	/// property Set stream, or if any other error occurs, an appropriate
	/// exception is thrown.
	/// A {@link PropertySet} has a list of {@link Section}s, and each
	/// {@link Section} has a {@link Property} array. Use {@link
	/// #GetSections} To retrieve the {@link Section}s, then call {@link
	/// Section#GetProperties} for each {@link Section} To Get hold of the
	/// {@link Property} arrays. Since the vast majority of {@link
	/// PropertySet}s Contains only a single {@link Section}, the
	/// convenience method {@link #GetProperties} returns the properties of
	/// a {@link PropertySet}'s {@link Section} (throwing a {@link
	/// NoSingleSectionException} if the {@link PropertySet} Contains more
	/// (or less) than exactly one {@link Section}).
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @author Drew Varner (Drew.Varner hanginIn sc.edu)
	/// @since 2002-02-09
	/// </summary>
	[Serializable]
	public class PropertySet
	{
		/// If the OS version field holds this value the property Set stream Was
		/// Created on a 16-bit Windows system.
		public const int OS_WIN16 = 0;

		/// If the OS version field holds this value the property Set stream Was
		/// Created on a Macintosh system.
		public const int OS_MACINTOSH = 1;

		/// If the OS version field holds this value the property Set stream Was
		/// Created on a 32-bit Windows system.
		public const int OS_WIN32 = 2;

		/// The "byteOrder" field must equal this value.
		protected static byte[] BYTE_ORDER_ASSERTION = new byte[2]
		{
			254,
			byte.MaxValue
		};

		/// Specifies this {@link PropertySet}'s byte order. See the
		/// HPFS documentation for details!
		protected int byteOrder;

		/// The "format" field must equal this value.
		protected static byte[] FORMAT_ASSERTION;

		/// Specifies this {@link PropertySet}'s format. See the HPFS
		/// documentation for details!
		protected int format;

		/// Specifies the version of the operating system that Created
		/// this {@link PropertySet}. See the HPFS documentation for
		/// details!
		protected int osVersion;

		/// Specifies this {@link PropertySet}'s "classID" field. See
		/// the HPFS documentation for details!
		[NonSerialized]
		protected ClassID classID;

		/// The sections in this {@link PropertySet}.
		protected List<Section> sections;

		/// <summary>
		/// Gets or sets the property Set stream's low-level "byte order"
		/// field. It is always <c>0xFFFE</c>
		/// </summary>
		/// <value>The property Set stream's low-level "byte order" field..</value>
		public virtual int ByteOrder
		{
			get
			{
				return byteOrder;
			}
			set
			{
				byteOrder = value;
			}
		}

		/// <summary>
		/// Gets or sets the property Set stream's low-level "format"
		/// field. It is always <c>0x0000</c>
		/// </summary>
		/// <value>The property Set stream's low-level "format" field.</value>
		public virtual int Format
		{
			get
			{
				return format;
			}
			set
			{
				format = value;
			}
		}

		/// <summary>
		/// Returns the property Set stream's low-level "OS version"
		/// field.
		/// </summary>
		/// <value>The property Set stream's low-level "OS version" field.</value>
		public virtual int OSVersion
		{
			get
			{
				return osVersion;
			}
			set
			{
				osVersion = value;
			}
		}

		/// <summary>
		/// Gets or sets the property Set stream's low-level "class ID"
		/// </summary>
		/// <value>The property Set stream's low-level "class ID" field.</value>
		public virtual ClassID ClassID
		{
			get
			{
				return classID;
			}
			set
			{
				classID = value;
			}
		}

		/// <summary>
		/// Returns the number of {@link Section}s in the property
		/// Set.
		/// </summary>
		/// <value>The number of {@link Section}s in the property Set.</value>
		public virtual int SectionCount => sections.Count;

		/// <summary>
		/// Returns the {@link Section}s in the property Set.
		/// </summary>
		/// <value>{@link Section}s in the property Set.</value>
		public virtual List<Section> Sections => sections;

		/// <summary>
		/// Checks whether this {@link PropertySet} represents a Summary
		/// Information.
		/// </summary>
		/// <value>
		/// 	<c>true</c> Checks whether this {@link PropertySet} represents a Summary
		/// Information; otherwise, <c>false</c>.
		/// </value>
		public virtual bool IsSummaryInformation
		{
			get
			{
				if (sections.Count <= 0)
				{
					return false;
				}
				return Arrays.Equals(sections[0].FormatID.Bytes, SectionIDMap.SUMMARY_INFORMATION_ID);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is document summary information.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is document summary information; otherwise, <c>false</c>.
		/// </value>
		/// Checks whether this {@link PropertySet} is a Document
		/// Summary Information.
		/// @return 
		/// <c>true</c>
		///  if this {@link PropertySet}
		/// represents a Document Summary Information, else 
		/// <c>false</c>
		public virtual bool IsDocumentSummaryInformation
		{
			get
			{
				if (sections.Count <= 0)
				{
					return false;
				}
				return Arrays.Equals(sections[0].FormatID.Bytes, SectionIDMap.DOCUMENT_SUMMARY_INFORMATION_ID1);
			}
		}

		/// <summary>
		/// Convenience method returning the {@link Property} array
		/// contained in this property Set. It is a shortcut for Getting
		/// the {@link PropertySet}'s {@link Section}s list and then
		/// Getting the {@link Property} array from the first {@link
		/// Section}.
		/// </summary>
		/// <value>The properties of the only {@link Section} of this
		/// {@link PropertySet}.</value>
		public virtual Property[] Properties => FirstSection.Properties;

		/// <summary>
		/// Checks whether the property which the last call To {@link
		/// #GetPropertyIntValue} or {@link #GetProperty} tried To access
		/// Was available or not. This information might be important for
		/// callers of {@link #GetPropertyIntValue} since the latter
		/// returns 0 if the property does not exist. Using {@link
		/// #WasNull}, the caller can distiguish this case from a
		/// property's real value of 0.
		/// </summary>
		/// <value><c>true</c> if the last call To {@link
		/// #GetPropertyIntValue} or {@link #GetProperty} tried To access a
		/// property that Was not available; otherwise, <c>false</c>.</value>
		public virtual bool WasNull => FirstSection.WasNull;

		/// <summary>
		/// Gets the first section.
		/// </summary>
		/// <value>The first section.</value>
		public virtual Section FirstSection
		{
			get
			{
				if (SectionCount < 1)
				{
					throw new MissingSectionException("Property Set does not contain any sections.");
				}
				return sections[0];
			}
		}

		/// <summary>
		/// If the {@link PropertySet} has only a single section this
		/// method returns it.
		/// </summary>
		/// <value>The singleSection value</value>
		public Section SingleSection
		{
			get
			{
				int sectionCount = SectionCount;
				if (sectionCount != 1)
				{
					throw new NoSingleSectionException("Property Set Contains " + sectionCount + " sections.");
				}
				return sections[0];
			}
		}

		/// <summary>
		/// Creates an empty (uninitialized) {@link PropertySet}
		/// Please note: For the time being this
		/// constructor is protected since it is used for internal purposes
		/// only, but expect it To become public once the property Set's
		/// writing functionality is implemented.
		/// </summary>
		protected PropertySet()
		{
		}

		/// <summary>
		/// Creates a {@link PropertySet} instance from an {@link
		/// InputStream} in the Horrible Property Set Format.
		/// The constructor Reads the first few bytes from the stream
		/// and determines whether it is really a property Set stream. If
		/// it Is, it parses the rest of the stream. If it is not, it
		/// Resets the stream To its beginning in order To let other
		/// components mess around with the data and throws an
		/// exception.
		/// </summary>
		/// <param name="stream">Holds the data making out the property Set
		/// stream.</param>
		public PropertySet(Stream stream)
		{
			if (IsPropertySetStream(stream))
			{
				int num = (stream as ByteArrayInputStream).Available();
				byte[] array = new byte[num];
				stream.Read(array, 0, array.Length);
				init(array, 0, array.Length);
				return;
			}
			throw new NoPropertySetStreamException("this stream may not be a valid property set stream");
		}

		/// <summary>
		/// Creates a {@link PropertySet} instance from a byte array
		/// that represents a stream in the Horrible Property Set
		/// Format.
		/// </summary>
		/// <param name="stream">The byte array holding the stream data.</param>
		/// <param name="offset">The offset in stream where the stream data begin. 
		/// If the stream data begin with the first byte in the
		/// array, the offset is 0.</param>
		/// <param name="Length"> The Length of the stream data.</param>
		public PropertySet(byte[] stream, int offset, int Length)
		{
			if (IsPropertySetStream(stream, offset, Length))
			{
				init(stream, offset, Length);
				return;
			}
			throw new NoPropertySetStreamException();
		}

		/// <summary>
		/// Creates a {@link PropertySet} instance from a byte array
		/// that represents a stream in the Horrible Property Set
		/// Format.
		/// </summary>
		/// <param name="stream">The byte array holding the stream data. The
		/// complete byte array contents is the stream data.</param>
		public PropertySet(byte[] stream)
			: this(stream, 0, stream.Length)
		{
		}

		/// <summary>
		/// Checks whether an {@link InputStream} is in the Horrible
		/// Property Set Format.
		/// </summary>
		/// <param name="stream">The {@link InputStream} To check. In order To
		/// perform the check, the method Reads the first bytes from the
		/// stream. After Reading, the stream is Reset To the position it
		/// had before Reading. The {@link InputStream} must support the
		/// {@link InputStream#mark} method.</param>
		/// <returns>
		/// 	<c>true</c> if the stream is a property Set
		/// stream; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsPropertySetStream(Stream stream)
		{
			ByteArrayInputStream byteArrayInputStream = stream as ByteArrayInputStream;
			int num = 50;
			if (byteArrayInputStream == null || !byteArrayInputStream.MarkSupported())
			{
				throw new MarkUnsupportedException(stream.GetType().Name);
			}
			byteArrayInputStream.Mark(num);
			byte[] array = new byte[num];
			int length = stream.Read(array, 0, Math.Min(array.Length, byteArrayInputStream.Available()));
			bool result = IsPropertySetStream(array, 0, length);
			byteArrayInputStream.Reset();
			return result;
		}

		/// <summary>
		/// Checks whether a byte array is in the Horrible Property Set
		/// Format.
		/// </summary>
		/// <param name="src">The byte array To check.</param>
		/// <param name="offset">The offset in the byte array.</param>
		/// <param name="Length">The significant number of bytes in the byte
		/// array. Only this number of bytes will be checked.</param>
		/// <returns>
		/// 	<c>true</c> if the byte array is a property Set
		/// stream; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsPropertySetStream(byte[] src, int offset, int Length)
		{
			int uShort = LittleEndian.GetUShort(src, offset);
			int num = offset + 2;
			byte[] array = new byte[2];
			LittleEndian.PutShort(array, 0, (short)uShort);
			if (!Arrays.Equals(array, BYTE_ORDER_ASSERTION))
			{
				return false;
			}
			int uShort2 = LittleEndian.GetUShort(src, num);
			num += 2;
			array = new byte[2];
			LittleEndian.PutShort(array, 0, (short)uShort2);
			if (!Arrays.Equals(array, FORMAT_ASSERTION))
			{
				return false;
			}
			LittleEndian.GetUInt(src, offset);
			num += 4;
			new ClassID(src, offset);
			num += 16;
			long uInt = LittleEndian.GetUInt(src, num);
			num += 4;
			if (uInt < 0)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Initializes this {@link PropertySet} instance from a byte
		/// array. The method assumes that it has been checked alReady that
		/// the byte array indeed represents a property Set stream. It does
		/// no more checks on its own.
		/// </summary>
		/// <param name="src">Byte array containing the property Set stream</param>
		/// <param name="offset">The property Set stream starts at this offset</param>
		/// <param name="Length">Length of the property Set stream.</param>
		private void init(byte[] src, int offset, int Length)
		{
			byteOrder = LittleEndian.GetUShort(src, offset);
			int num = offset + 2;
			format = LittleEndian.GetUShort(src, num);
			num += 2;
			osVersion = (int)LittleEndian.GetUInt(src, num);
			num += 4;
			classID = new ClassID(src, num);
			num += 16;
			int @int = LittleEndian.GetInt(src, num);
			num += 4;
			if (@int < 0)
			{
				throw new HPSFRuntimeException("Section count " + @int + " is negative.");
			}
			sections = new List<Section>(@int);
			for (int i = 0; i < @int; i++)
			{
				Section item = new Section(src, num);
				num += ClassID.Length + 4;
				sections.Add(item);
			}
		}

		/// <summary>
		/// Convenience method returning the value of the property with
		/// the specified ID. If the property is not available,
		/// <c>null</c> is returned and a subsequent call To {@link
		/// #WasNull} will return <c>true</c> .
		/// </summary>
		/// <param name="id">The property ID</param>
		/// <returns>The property value</returns>
		public virtual object GetProperty(int id)
		{
			return FirstSection.GetProperty(id);
		}

		/// <summary>
		/// Convenience method returning the value of a bool property
		/// with the specified ID. If the property is not available,
		/// <c>false</c> is returned. A subsequent call To {@link
		/// #WasNull} will return <c>true</c> To let the caller
		/// distinguish that case from a real property value of
		/// <c>false</c>.
		/// </summary>
		/// <param name="id">The property ID</param>
		/// <returns>The property value</returns>
		public virtual bool GetPropertyBooleanValue(int id)
		{
			return FirstSection.GetPropertyBooleanValue(id);
		}

		/// <summary>
		/// Convenience method returning the value of the numeric
		/// property with the specified ID. If the property is not
		/// available, 0 is returned. A subsequent call To {@link #WasNull}
		/// will return <c>true</c> To let the caller distinguish
		/// that case from a real property value of 0.
		/// </summary>
		/// <param name="id">The property ID</param>
		/// <returns>The propertyIntValue value</returns>
		public virtual int GetPropertyIntValue(int id)
		{
			return FirstSection.GetPropertyIntValue(id);
		}

		/// <summary>
		/// Returns <c>true</c> if the <c>PropertySet</c> is equal
		/// To the specified parameter, else <c>false</c>.
		/// </summary>
		/// <param name="o">the object To Compare this 
		/// <c>PropertySet</c>
		///  with</param>
		/// <returns><c>true</c>
		///  if the objects are equal, 
		/// <c>false</c>
		/// if not</returns>
		public override bool Equals(object o)
		{
			if (o == null || !(o is PropertySet))
			{
				return false;
			}
			PropertySet propertySet = (PropertySet)o;
			int num = propertySet.ByteOrder;
			int num2 = ByteOrder;
			ClassID classID = propertySet.ClassID;
			ClassID obj = ClassID;
			int num3 = propertySet.Format;
			int num4 = Format;
			int oSVersion = propertySet.OSVersion;
			int oSVersion2 = OSVersion;
			int sectionCount = propertySet.SectionCount;
			int sectionCount2 = SectionCount;
			if (num != num2 || !classID.Equals(obj) || num3 != num4 || oSVersion != oSVersion2 || sectionCount != sectionCount2)
			{
				return false;
			}
			return Util.AreEqual(Sections, propertySet.Sections);
		}

		/// <summary>
		/// Serves as a hash function for a particular type.
		/// </summary>
		/// <returns>
		/// A hash code for the current <see cref="T:System.Object" />.
		/// </returns>
		public override int GetHashCode()
		{
			throw new InvalidOperationException("FIXME: Not yet implemented.");
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
			int sectionCount = SectionCount;
			stringBuilder.Append(GetType().Name);
			stringBuilder.Append('[');
			stringBuilder.Append("byteOrder: ");
			stringBuilder.Append(ByteOrder);
			stringBuilder.Append(", classID: ");
			stringBuilder.Append(ClassID);
			stringBuilder.Append(", format: ");
			stringBuilder.Append(Format);
			stringBuilder.Append(", OSVersion: ");
			stringBuilder.Append(OSVersion);
			stringBuilder.Append(", sectionCount: ");
			stringBuilder.Append(sectionCount);
			stringBuilder.Append(", sections: [\n");
			foreach (Section section in Sections)
			{
				stringBuilder.Append(section.ToString());
			}
			stringBuilder.Append(']');
			stringBuilder.Append(']');
			return stringBuilder.ToString();
		}

		static PropertySet()
		{
			byte[] array = FORMAT_ASSERTION = new byte[2];
		}
	}
}
