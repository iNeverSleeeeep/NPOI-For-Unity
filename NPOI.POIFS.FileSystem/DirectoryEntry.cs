using NPOI.POIFS.EventFileSystem;
using NPOI.Util;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// This interface defines methods specific to Directory objects
	/// managed by a Filesystem instance.
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public interface DirectoryEntry : Entry, IEnumerable<Entry>, IEnumerable
	{
		/// <summary>
		/// get an iterator of the Entry instances contained directly in
		/// this instance (in other words, children only; no grandchildren
		/// etc.)
		/// </summary>
		/// <value>The entries.never null, but hasNext() may return false
		/// immediately (i.e., this DirectoryEntry is empty). All
		/// objects retrieved by next() are guaranteed to be
		/// implementations of Entry.</value>
		IEnumerator<Entry> Entries
		{
			get;
		}

		/// <summary>
		///             is this DirectoryEntry empty?
		/// </summary>
		/// <value><c>true</c> if this instance contains no Entry instances; otherwise, <c>false</c>.</value>
		bool IsEmpty
		{
			get;
		}

		/// <summary>
		/// find out how many Entry instances are contained directly within
		/// this DirectoryEntry
		/// </summary>
		/// <value>number of immediately (no grandchildren etc.) contained
		/// Entry instances</value>
		int EntryCount
		{
			get;
		}

		/// <summary>
		/// Gets or sets the storage ClassID.
		/// </summary>
		/// <value>The storage ClassID.</value>
		ClassID StorageClsid
		{
			get;
			set;
		}

		/// <summary>
		/// get a specified Entry by name
		/// </summary>
		/// <param name="name">the name of the Entry to obtain.</param>
		/// <returns>the specified Entry, if it is directly contained in
		/// this DirectoryEntry</returns>
		Entry GetEntry(string name);

		/// <summary>
		/// Create a new DocumentEntry
		/// </summary>
		/// <param name="name">the name of the new DocumentEntry</param>
		/// <param name="stream">the Stream from which to Create the new DocumentEntry</param>
		/// <returns>the new DocumentEntry</returns>
		DocumentEntry CreateDocument(string name, Stream stream);

		/// <summary>
		/// Create a new DocumentEntry; the data will be provided later
		/// </summary>
		/// <param name="name">the name of the new DocumentEntry</param>
		/// <param name="size">the size of the new DocumentEntry</param>
		/// <param name="writer">BeforeWriting event handler</param>
		/// <returns>the new DocumentEntry</returns>
		DocumentEntry CreateDocument(string name, int size, POIFSWriterListener writer);

		/// <summary>
		/// Create a new DirectoryEntry
		/// </summary>
		/// <param name="name">the name of the new DirectoryEntry</param>
		/// <returns>the name of the new DirectoryEntry</returns>
		DirectoryEntry CreateDirectory(string name);

		/// <summary>
		/// Checks if entry with specified name present
		/// </summary>
		/// <param name="name">entry name</param>
		/// <returns>true if have</returns>
		bool HasEntry(string name);
	}
}
