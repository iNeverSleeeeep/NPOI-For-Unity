using NPOI.POIFS.Dev;
using NPOI.POIFS.EventFileSystem;
using NPOI.POIFS.Properties;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// Simple implementation of DirectoryEntry
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	[Serializable]
	public class DirectoryNode : EntryNode, DirectoryEntry, Entry, POIFSViewable, IEnumerable<Entry>, IEnumerable
	{
		private Dictionary<string, Entry> _byname;

		private List<Entry> _entries;

		private POIFSFileSystem _oFilesSystem;

		private NPOIFSFileSystem _nFilesSystem;

		private POIFSDocumentPath _path;

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <value>this directory's path representation</value>
		public POIFSDocumentPath Path => _path;

		public POIFSFileSystem FileSystem => _oFilesSystem;

		public NPOIFSFileSystem NFileSystem => _nFilesSystem;

		/// <summary>
		/// get an iterator of the Entry instances contained directly in
		/// this instance (in other words, children only; no grandchildren
		/// etc.)
		/// </summary>
		/// <value>
		/// The entries.never null, but hasNext() may return false
		/// immediately (i.e., this DirectoryEntry is empty). All
		/// objects retrieved by next() are guaranteed to be
		/// implementations of Entry.
		/// </value>
		public IEnumerator<Entry> Entries => _entries.GetEnumerator();

		/// <summary>
		/// is this DirectoryEntry empty?
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance contains no Entry instances; otherwise, <c>false</c>.
		/// </value>
		public bool IsEmpty => _entries.Count == 0;

		/// <summary>
		/// find out how many Entry instances are contained directly within
		/// this DirectoryEntry
		/// </summary>
		/// <value>
		/// number of immediately (no grandchildren etc.) contained
		/// Entry instances
		/// </value>
		public int EntryCount => _entries.Count;

		/// <summary>
		/// Gets or Sets the storage clsid for the directory entry
		/// </summary>
		/// <value>The storage ClassID.</value>
		public ClassID StorageClsid
		{
			get
			{
				return base.Property.StorageClsid;
			}
			set
			{
				base.Property.StorageClsid = value;
			}
		}

		/// <summary>
		/// Is this a DirectoryEntry?
		/// </summary>
		/// <value>true if the Entry Is a DirectoryEntry, else false</value>
		public override bool IsDirectoryEntry => true;

		/// <summary>
		/// extensions use this method to verify internal rules regarding
		/// deletion of the underlying store.
		/// </summary>
		/// <value> true if it's ok to Delete the underlying store, else
		/// false</value>
		protected override bool IsDeleteOK => IsEmpty;

		/// <summary>
		/// Get an array of objects, some of which may implement POIFSViewable
		/// </summary>
		/// <value>an array of Object; may not be null, but may be empty</value>
		public Array ViewableArray => new object[0];

		/// <summary>
		/// Get an Iterator of objects, some of which may implement
		/// POIFSViewable
		/// </summary>
		/// <value>an Iterator; may not be null, but may have an empty
		/// back end store</value>
		public IEnumerator ViewableIterator
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				arrayList.Add(base.Property);
				arrayList.AddRange(_entries);
				return arrayList.GetEnumerator();
			}
		}

		/// <summary>
		/// Give viewers a hint as to whether to call GetViewableArray or
		/// GetViewableIterator
		/// </summary>
		/// <value><c>true</c> if a viewer should call GetViewableArray; otherwise, <c>false</c>if
		/// a viewer should call GetViewableIterator</value>
		public bool PreferArray => false;

		/// <summary>
		/// Provides a short description of the object, to be used when a
		/// POIFSViewable object has not provided its contents.
		/// </summary>
		/// <value>The short description.</value>
		public string ShortDescription => base.Name;

		public bool CanRead
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool CanSeek
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool CanWrite
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public DirectoryNode(DirectoryProperty property, POIFSFileSystem fileSystem, DirectoryNode parent)
			: this(property, parent, fileSystem, null)
		{
		}

		/// <summary>
		/// Create a DirectoryNode. This method Is not public by design; it
		/// Is intended strictly for the internal use of this package
		/// </summary>
		/// <param name="property">the DirectoryProperty for this DirectoryEntry</param>
		/// <param name="nFileSystem">the POIFSFileSystem we belong to</param>
		/// <param name="parent">the parent of this entry</param>
		public DirectoryNode(DirectoryProperty property, NPOIFSFileSystem nFileSystem, DirectoryNode parent)
			: this(property, parent, null, nFileSystem)
		{
		}

		private DirectoryNode(DirectoryProperty property, DirectoryNode parent, POIFSFileSystem oFileSystem, NPOIFSFileSystem nFileSystem)
			: base(property, parent)
		{
			_oFilesSystem = oFileSystem;
			_nFilesSystem = nFileSystem;
			if (parent == null)
			{
				_path = new POIFSDocumentPath();
			}
			else
			{
				_path = new POIFSDocumentPath(parent._path, new string[1]
				{
					property.Name
				});
			}
			_byname = new Dictionary<string, Entry>();
			_entries = new List<Entry>();
			IEnumerator<Property> children = property.Children;
			while (children.MoveNext())
			{
				Property current = children.Current;
				Entry entry = null;
				if (current.IsDirectory)
				{
					DirectoryProperty property2 = (DirectoryProperty)current;
					entry = ((_oFilesSystem == null) ? new DirectoryNode(property2, _nFilesSystem, this) : new DirectoryNode(property2, _oFilesSystem, this));
				}
				else
				{
					entry = new DocumentNode((DocumentProperty)current, this);
				}
				_entries.Add(entry);
				_byname.Add(entry.Name, entry);
			}
		}

		/// <summary>
		/// open a document in the directory's entry's list of entries
		/// </summary>
		/// <param name="documentName">the name of the document to be opened</param>
		/// <returns>a newly opened DocumentStream</returns>
		public DocumentInputStream CreatePOIFSDocumentReader(string documentName)
		{
			Entry entry = GetEntry(documentName);
			if (!entry.IsDocumentEntry)
			{
				throw new IOException("Entry '" + documentName + "' Is not a DocumentEntry");
			}
			return new DocumentInputStream((DocumentEntry)entry);
		}

		/// <summary>
		/// Create a new DocumentEntry; the data will be provided later
		/// </summary>
		/// <param name="document">the name of the new documentEntry</param>
		/// <returns>the new DocumentEntry</returns>
		public DocumentEntry CreateDocument(POIFSDocument document)
		{
			DocumentProperty documentProperty = document.DocumentProperty;
			DocumentNode documentNode = new DocumentNode(documentProperty, this);
			((DirectoryProperty)base.Property).AddChild(documentProperty);
			_oFilesSystem.AddDocument(document);
			_entries.Add(documentNode);
			_byname.Add(documentProperty.Name, documentNode);
			return documentNode;
		}

		/// <summary>
		/// Change a contained Entry's name
		/// </summary>
		/// <param name="oldName">the original name</param>
		/// <param name="newName">the new name</param>
		/// <returns>true if the operation succeeded, else false</returns>
		public bool ChangeName(string oldName, string newName)
		{
			bool flag = false;
			EntryNode entryNode = (EntryNode)_byname[oldName];
			if (entryNode != null)
			{
				flag = ((DirectoryProperty)base.Property).ChangeName(entryNode.Property, newName);
				if (flag)
				{
					_byname.Remove(oldName);
					_byname[entryNode.Property.Name] = entryNode;
				}
			}
			return flag;
		}

		/// <summary>
		/// Deletes the entry.
		/// </summary>
		/// <param name="entry">the EntryNode to be Deleted</param>
		/// <returns>true if the entry was Deleted, else false</returns>
		public bool DeleteEntry(EntryNode entry)
		{
			bool flag = ((DirectoryProperty)base.Property).DeleteChild(entry.Property);
			if (flag)
			{
				_entries.Remove(entry);
				_byname.Remove(entry.Name);
				if (_oFilesSystem != null)
				{
					_oFilesSystem.Remove(entry);
				}
				else
				{
					_nFilesSystem.Remove(entry);
				}
			}
			return flag;
		}

		internal Entry GetEntry(int index)
		{
			return _entries[index];
		}

		public bool HasEntry(string name)
		{
			if (name != null)
			{
				return _byname.ContainsKey(name);
			}
			return false;
		}

		/// <summary>
		/// get a specified Entry by name
		/// </summary>
		/// <param name="name">the name of the Entry to obtain.</param>
		/// <returns>
		/// the specified Entry, if it is directly contained in
		/// this DirectoryEntry
		/// </returns>
		public Entry GetEntry(string name)
		{
			Entry entry = null;
			if (name != null)
			{
				try
				{
					entry = _byname[name];
				}
				catch (KeyNotFoundException)
				{
					throw new FileNotFoundException("no such entry: \"" + name + "\"");
				}
			}
			if (entry == null)
			{
				throw new FileNotFoundException("no such entry: \"" + name + "\"");
			}
			return entry;
		}

		public DocumentInputStream CreateDocumentInputStream(Entry document)
		{
			if (!document.IsDocumentEntry)
			{
				throw new IOException("Entry '" + document.Name + "' is not a DocumentEntry");
			}
			DocumentEntry document2 = (DocumentEntry)document;
			return new DocumentInputStream(document2);
		}

		public DocumentInputStream CreateDocumentInputStream(string documentName)
		{
			return CreateDocumentInputStream(GetEntry(documentName));
		}

		public DocumentEntry CreateDocument(NPOIFSDocument document)
		{
			try
			{
				DocumentProperty documentProperty = document.DocumentProperty;
				DocumentNode documentNode = new DocumentNode(documentProperty, this);
				((DirectoryProperty)base.Property).AddChild(documentProperty);
				_nFilesSystem.AddDocument(document);
				_entries.Add(documentNode);
				_byname[documentProperty.Name] = documentNode;
				return documentNode;
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		/// <summary>
		/// Create a new DirectoryEntry
		/// </summary>
		/// <param name="name">the name of the new DirectoryEntry</param>
		/// <returns>the name of the new DirectoryEntry</returns>
		public DirectoryEntry CreateDirectory(string name)
		{
			DirectoryProperty directoryProperty = new DirectoryProperty(name);
			DirectoryNode directoryNode;
			if (_oFilesSystem != null)
			{
				directoryNode = new DirectoryNode(directoryProperty, _oFilesSystem, this);
				_oFilesSystem.AddDirectory(directoryProperty);
			}
			else
			{
				directoryNode = new DirectoryNode(directoryProperty, _nFilesSystem, this);
				_nFilesSystem.AddDirectory(directoryProperty);
			}
			((DirectoryProperty)base.Property).AddChild(directoryProperty);
			_entries.Add(directoryNode);
			_byname[name] = directoryNode;
			return directoryNode;
		}

		public DocumentEntry CreateDocument(string name, Stream stream)
		{
			try
			{
				if (_nFilesSystem != null)
				{
					return CreateDocument(new NPOIFSDocument(name, _nFilesSystem, stream));
				}
				return CreateDocument(new POIFSDocument(name, stream));
			}
			catch (IOException ex)
			{
				throw ex;
			}
		}

		public DocumentEntry CreateDocument(string name, int size, POIFSWriterListener writer)
		{
			return CreateDocument(new POIFSDocument(name, size, _path, writer));
		}

		public IEnumerator<Entry> GetEnumerator()
		{
			return _entries.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _entries.GetEnumerator();
		}

		public void Flush()
		{
			throw new NotImplementedException();
		}

		public int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public void SetLength(long value)
		{
			throw new NotImplementedException();
		}
	}
}
