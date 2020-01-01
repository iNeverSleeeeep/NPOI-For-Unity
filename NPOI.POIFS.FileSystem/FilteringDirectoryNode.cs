using NPOI.POIFS.EventFileSystem;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// A DirectoryEntry filter, which exposes another  DirectoryEntry less certain parts.
	/// This is typically used when copying or comparing  Filesystems.
	/// </summary>
	public class FilteringDirectoryNode : DirectoryEntry, Entry, IEnumerable<Entry>, IEnumerable
	{
		private class FilteringIterator : IEnumerator<Entry>, IDisposable, IEnumerator
		{
			private IEnumerator<Entry> parent;

			private Entry next;

			private DirectoryEntry directory;

			private FilteringDirectoryNode filtering;

			public Entry Current => next;

			object IEnumerator.Current
			{
				get
				{
					return next;
				}
			}

			public FilteringIterator(FilteringDirectoryNode filtering)
			{
				this.filtering = filtering;
				directory = filtering.directory;
				parent = directory.Entries;
			}

			public void Remove()
			{
				throw new InvalidOperationException("Remove not supported");
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				next = null;
				while (parent.MoveNext())
				{
					Entry current = parent.Current;
					if (!filtering.excludes.Contains(current.Name))
					{
						next = filtering.WrapEntry(current);
						break;
					}
				}
				return next != null;
			}

			public void Reset()
			{
				throw new NotImplementedException();
			}
		}

		private List<string> excludes;

		private Dictionary<string, List<string>> childExcludes;

		private DirectoryEntry directory;

		public IEnumerator<Entry> Entries => GetEntries();

		public bool IsEmpty => EntryCount == 0;

		public int EntryCount
		{
			get
			{
				int num = directory.EntryCount;
				foreach (string exclude in excludes)
				{
					if (directory.HasEntry(exclude))
					{
						num--;
					}
				}
				return num;
			}
		}

		public ClassID StorageClsid
		{
			get
			{
				return directory.StorageClsid;
			}
			set
			{
				directory.StorageClsid = value;
			}
		}

		public string Name => directory.Name;

		public bool IsDirectoryEntry => true;

		public bool IsDocumentEntry => false;

		public DirectoryEntry Parent => directory.Parent;

		/// <summary>
		/// Creates a filter round the specified directory, which will exclude entries such as 
		/// "MyNode" and "MyDir/IgnoreNode". The excludes can stretch into children, if they contain a /.
		/// </summary>
		/// <param name="directory">The Directory to filter</param>
		/// <param name="excludes">The Entries to exclude</param>
		public FilteringDirectoryNode(DirectoryEntry directory, ICollection<string> excludes)
		{
			this.directory = directory;
			this.excludes = new List<string>();
			childExcludes = new Dictionary<string, List<string>>();
			foreach (string exclude in excludes)
			{
				int num = exclude.IndexOf('/');
				if (num == -1)
				{
					this.excludes.Add(exclude);
				}
				else
				{
					string key = exclude.Substring(0, num);
					string item = exclude.Substring(num + 1);
					if (!childExcludes.ContainsKey(key))
					{
						childExcludes.Add(key, new List<string>());
					}
					childExcludes[key].Add(item);
				}
			}
		}

		public bool HasEntry(string name)
		{
			if (excludes.Contains(name))
			{
				return false;
			}
			return directory.HasEntry(name);
		}

		public IEnumerator<Entry> GetEntries()
		{
			return new FilteringIterator(this);
		}

		public Entry GetEntry(string name)
		{
			if (excludes.Contains(name))
			{
				throw new FileNotFoundException(name);
			}
			Entry entry = directory.GetEntry(name);
			return WrapEntry(entry);
		}

		private Entry WrapEntry(Entry entry)
		{
			string name = entry.Name;
			if (childExcludes.ContainsKey(name) && entry is DirectoryEntry)
			{
				return new FilteringDirectoryNode((DirectoryEntry)entry, childExcludes[name]);
			}
			return entry;
		}

		public DocumentEntry CreateDocument(string name, Stream stream)
		{
			return directory.CreateDocument(name, stream);
		}

		public DocumentEntry CreateDocument(string name, int size, POIFSWriterListener writer)
		{
			return directory.CreateDocument(name, size, writer);
		}

		public DirectoryEntry CreateDirectory(string name)
		{
			return directory.CreateDirectory(name);
		}

		public bool Delete()
		{
			return directory.Delete();
		}

		public bool RenameTo(string newName)
		{
			return directory.RenameTo(newName);
		}

		public IEnumerator<Entry> GetEnumerator()
		{
			return new FilteringIterator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new FilteringIterator(this);
		}
	}
}
