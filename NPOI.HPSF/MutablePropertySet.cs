using NPOI.POIFS.FileSystem;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.HPSF
{
	/// <summary>
	/// Adds writing support To the {@link PropertySet} class.
	/// Please be aware that this class' functionality will be merged into the
	/// {@link PropertySet} class at a later time, so the API will Change.
	/// @author Rainer Klute 
	/// <a href="mailto:klute@rainer-klute.de">&lt;klute@rainer-klute.de&gt;</a>
	/// @since 2003-02-19
	/// </summary>
	[Serializable]
	public class MutablePropertySet : PropertySet
	{
		/// The Length of the property Set stream header.
		private int OFFSET_HEADER = PropertySet.BYTE_ORDER_ASSERTION.Length + PropertySet.FORMAT_ASSERTION.Length + 4 + 16 + 4;

		/// <summary>
		/// Gets or sets the "byteOrder" property.
		/// </summary>
		/// <value>the byteOrder value To Set</value>
		public override int ByteOrder
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
		/// Gets or sets the "format" property.
		/// </summary>
		/// <value>the format value To Set</value>
		public override int Format
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
		/// Gets or sets the "osVersion" property
		/// </summary>
		/// <value>the osVersion value To Set.</value>
		public override int OSVersion
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
		public override ClassID ClassID
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
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.MutablePropertySet" /> class.
		/// Its primary task is To initialize the immutable field with their proper
		/// values. It also Sets fields that might Change To reasonable defaults.
		/// </summary>
		public MutablePropertySet()
		{
			byteOrder = LittleEndian.GetUShort(PropertySet.BYTE_ORDER_ASSERTION);
			format = LittleEndian.GetUShort(PropertySet.FORMAT_ASSERTION);
			osVersion = 133636;
			classID = new ClassID();
			sections = new List<Section>();
			sections.Add(new MutableSection());
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NPOI.HPSF.MutablePropertySet" /> class.
		/// All nested elements, i.e.<c>Section</c>s and <c>Property</c> instances, will be their
		/// mutable counterparts in the new <c>MutablePropertySet</c>.
		/// </summary>
		/// <param name="ps">The property Set To copy</param>
		public MutablePropertySet(PropertySet ps)
		{
			byteOrder = ps.ByteOrder;
			format = ps.Format;
			osVersion = ps.OSVersion;
			ClassID = ps.ClassID;
			ClearSections();
			if (sections == null)
			{
				sections = new List<Section>();
			}
			IEnumerator enumerator = ps.Sections.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MutableSection section = new MutableSection((Section)enumerator.Current);
				AddSection(section);
			}
		}

		/// <summary>
		/// Removes all sections from this property Set.
		/// </summary>
		public virtual void ClearSections()
		{
			sections = null;
		}

		/// <summary>
		/// Adds a section To this property Set.
		/// </summary>
		/// <param name="section">section The {@link Section} To Add. It will be Appended
		/// after any sections that are alReady present in the property Set
		/// and thus become the last section.</param>
		public virtual void AddSection(Section section)
		{
			if (sections == null)
			{
				sections = new List<Section>();
			}
			sections.Add(section);
		}

		/// <summary>
		/// Writes the property Set To an output stream.
		/// </summary>
		/// <param name="out1">the output stream To Write the section To</param>
		public virtual void Write(Stream out1)
		{
			int count = sections.Count;
			int num = 0;
			num += TypeWriter.WriteToStream(out1, (short)ByteOrder);
			num += TypeWriter.WriteToStream(out1, (short)Format);
			num += TypeWriter.WriteToStream(out1, OSVersion);
			num += TypeWriter.WriteToStream(out1, ClassID);
			num += TypeWriter.WriteToStream(out1, count);
			int oFFSET_HEADER = OFFSET_HEADER;
			oFFSET_HEADER += count * (ClassID.Length + 4);
			int num2 = oFFSET_HEADER;
			IEnumerator enumerator = sections.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MutableSection mutableSection = (MutableSection)enumerator.Current;
				ClassID formatID = mutableSection.FormatID;
				if (formatID == null)
				{
					throw new NoFormatIDException();
				}
				num += TypeWriter.WriteToStream(out1, mutableSection.FormatID);
				num += TypeWriter.WriteUIntToStream(out1, (uint)oFFSET_HEADER);
				oFFSET_HEADER += mutableSection.Size;
			}
			oFFSET_HEADER = num2;
			IEnumerator enumerator2 = sections.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				MutableSection mutableSection2 = (MutableSection)enumerator2.Current;
				oFFSET_HEADER += mutableSection2.Write(out1);
			}
		}

		/// <summary>
		/// Returns the contents of this property Set stream as an input stream.
		/// The latter can be used for example To Write the property Set into a POIFS
		/// document. The input stream represents a snapshot of the property Set.
		/// If the latter is modified while the input stream is still being
		/// Read, the modifications will not be reflected in the input stream but in
		/// the {@link MutablePropertySet} only.
		/// </summary>
		/// <returns>the contents of this property Set stream</returns>
		public virtual Stream GetStream()
		{
			MemoryStream memoryStream = new MemoryStream();
			Write(memoryStream);
			memoryStream.Position = 0L;
			return memoryStream;
		}

		/// <summary>
		/// Returns the contents of this property set stream as an input stream.
		/// The latter can be used for example to write the property set into a POIFS
		/// document. The input stream represents a snapshot of the property set.
		/// If the latter is modified while the input stream is still being
		/// read, the modifications will not be reflected in the input stream but in
		/// the {@link MutablePropertySet} only.
		/// </summary>
		/// <returns>the contents of this property set stream</returns>
		public virtual Stream ToStream()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Write(memoryStream);
				memoryStream.Flush();
				byte[] buffer = memoryStream.ToArray();
				return new MemoryStream(buffer);
			}
		}

		/// <summary>
		/// Writes a property Set To a document in a POI filesystem directory
		/// </summary>
		/// <param name="dir">The directory in the POI filesystem To Write the document To.</param>
		/// <param name="name">The document's name. If there is alReady a document with the
		/// same name in the directory the latter will be overwritten.</param>
		public virtual void Write(DirectoryEntry dir, string name)
		{
			try
			{
				Entry entry = dir.GetEntry(name);
				entry.Delete();
			}
			catch (FileNotFoundException)
			{
			}
			dir.CreateDocument(name, GetStream());
		}
	}
}
