using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace NPOI.POIFS.FileSystem
{
	public class EntryUtils
	{
		/// Copies an Entry into a target POIFS directory, recursively
		public static void CopyNodeRecursively(Entry entry, DirectoryEntry target)
		{
			DirectoryEntry directoryEntry = null;
			if (entry.IsDirectoryEntry)
			{
				directoryEntry = target.CreateDirectory(entry.Name);
				IEnumerator<Entry> entries = ((DirectoryEntry)entry).Entries;
				while (entries.MoveNext())
				{
					CopyNodeRecursively(entries.Current, directoryEntry);
				}
			}
			else
			{
				DocumentEntry documentEntry = (DocumentEntry)entry;
				DocumentInputStream documentInputStream = new DocumentInputStream(documentEntry);
				target.CreateDocument(documentEntry.Name, documentInputStream);
				documentInputStream.Close();
			}
		}

		/// Copies all the nodes from one POIFS Directory to another
		///
		/// @param sourceRoot
		///            is the source Directory to copy from
		/// @param targetRoot
		///            is the target Directory to copy to
		public static void CopyNodes(DirectoryEntry sourceRoot, DirectoryEntry targetRoot)
		{
			foreach (Entry item in sourceRoot)
			{
				CopyNodeRecursively(item, targetRoot);
			}
		}

		/// Copies nodes from one Directory to the other minus the excepts
		///
		/// @param filteredSource The filtering source Directory to copy from
		/// @param filteredTarget The filtering target Directory to copy to
		public static void CopyNodes(FilteringDirectoryNode filteredSource, FilteringDirectoryNode filteredTarget)
		{
			CopyNodes((DirectoryEntry)filteredSource, (DirectoryEntry)filteredTarget);
		}

		/// Copies nodes from one Directory to the other minus the excepts
		///
		/// @param sourceRoot
		///            is the source Directory to copy from
		/// @param targetRoot
		///            is the target Directory to copy to
		/// @param excepts
		///            is a list of Strings specifying what nodes NOT to copy
		/// @deprecated use {@link FilteringDirectoryNode} instead
		[Obsolete]
		public static void CopyNodes(DirectoryEntry sourceRoot, DirectoryEntry targetRoot, List<string> excepts)
		{
			IEnumerator entries = sourceRoot.Entries;
			while (entries.MoveNext())
			{
				Entry entry = (Entry)entries.Current;
				if (!excepts.Contains(entry.Name))
				{
					CopyNodeRecursively(entry, targetRoot);
				}
			}
		}

		/// Copies all nodes from one POIFS to the other
		///
		/// @param source
		///            is the source POIFS to copy from
		/// @param target
		///            is the target POIFS to copy to
		public static void CopyNodes(POIFSFileSystem source, POIFSFileSystem target)
		{
			CopyNodes(source.Root, target.Root);
		}

		/// Copies nodes from one POIFS to the other, minus the excepts.
		/// This delegates the filtering work to {@link FilteringDirectoryNode},
		///  so excepts can be of the form "NodeToExclude" or
		///  "FilteringDirectory/ExcludedChildNode"
		///
		/// @param source is the source POIFS to copy from
		/// @param target is the target POIFS to copy to
		/// @param excepts is a list of Entry Names to be excluded from the copy
		public static void CopyNodes(POIFSFileSystem source, POIFSFileSystem target, List<string> excepts)
		{
			CopyNodes(new FilteringDirectoryNode(source.Root, excepts), new FilteringDirectoryNode(target.Root, excepts));
		}

		/// Checks to see if the two Directories hold the same contents.
		/// For this to be true, they must have entries with the same names,
		///  no entries in one but not the other, and the size+contents
		///  of each entry must match, and they must share names.
		/// To exclude certain parts of the Directory from being checked,
		///  use a {@link FilteringDirectoryNode}
		public static bool AreDirectoriesIdentical(DirectoryEntry dirA, DirectoryEntry dirB)
		{
			if (!dirA.Name.Equals(dirB.Name))
			{
				return false;
			}
			if (dirA.EntryCount != dirB.EntryCount)
			{
				return false;
			}
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			int num = -12345;
			foreach (Entry item in dirA)
			{
				string name = item.Name;
				if (item.IsDirectoryEntry)
				{
					dictionary.Add(name, num);
				}
				else
				{
					dictionary.Add(name, ((DocumentNode)item).Size);
				}
			}
			foreach (Entry item2 in dirB)
			{
				string name2 = item2.Name;
				if (!dictionary.ContainsKey(name2))
				{
					return false;
				}
				int num2 = (!item2.IsDirectoryEntry) ? ((DocumentNode)item2).Size : num;
				if (num2 != dictionary[name2])
				{
					return false;
				}
				dictionary.Remove(name2);
			}
			if (dictionary.Count != 0)
			{
				return false;
			}
			foreach (Entry item3 in dirA)
			{
				try
				{
					Entry entry = dirB.GetEntry(item3.Name);
					if (!((!item3.IsDirectoryEntry) ? AreDocumentsIdentical((DocumentEntry)item3, (DocumentEntry)entry) : AreDirectoriesIdentical((DirectoryEntry)item3, (DirectoryEntry)entry)))
					{
						return false;
					}
				}
				catch (FileNotFoundException)
				{
					return false;
				}
				catch (IOException)
				{
					return false;
				}
			}
			return true;
		}

		/// Checks to see if two Documents have the same name
		///  and the same contents. (Their parent directories are
		///  not checked)
		public static bool AreDocumentsIdentical(DocumentEntry docA, DocumentEntry docB)
		{
			if (!docA.Name.Equals(docB.Name))
			{
				return false;
			}
			if (docA.Size == docB.Size)
			{
				bool result = true;
				DocumentInputStream documentInputStream = null;
				DocumentInputStream documentInputStream2 = null;
				try
				{
					documentInputStream = new DocumentInputStream(docA);
					documentInputStream2 = new DocumentInputStream(docB);
					while (true)
					{
						int num = documentInputStream.Read();
						int num2 = documentInputStream2.Read();
						if (num != num2)
						{
							return false;
						}
						if (num == -1)
						{
							break;
						}
						if (num2 == -1)
						{
							return result;
						}
					}
					return result;
				}
				finally
				{
					documentInputStream?.Close();
					documentInputStream2?.Close();
				}
			}
			return false;
		}
	}
}
