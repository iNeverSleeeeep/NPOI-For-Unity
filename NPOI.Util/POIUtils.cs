using NPOI.POIFS.FileSystem;
using System.Collections;
using System.Collections.Generic;

namespace NPOI.Util
{
	public class POIUtils
	{
		/// Copies an Entry into a target POIFS directory, recursively
		public static void CopyNodeRecursively(Entry entry, DirectoryEntry target)
		{
			DirectoryEntry directoryEntry = null;
			if (entry.IsDirectoryEntry)
			{
				directoryEntry = target.CreateDirectory(entry.Name);
				IEnumerator entries = ((DirectoryEntry)entry).Entries;
				while (entries.MoveNext())
				{
					CopyNodeRecursively((Entry)entries.Current, directoryEntry);
				}
			}
			else
			{
				DocumentEntry documentEntry = (DocumentEntry)entry;
				using (DocumentInputStream stream = new DocumentInputStream(documentEntry))
				{
					target.CreateDocument(documentEntry.Name, stream);
				}
			}
		}

		/// Copies nodes from one POIFS to the other minus the excepts
		///
		/// @param source
		///            is the source POIFS to copy from
		/// @param target
		///            is the target POIFS to copy to
		/// @param excepts
		///            is a list of Strings specifying what nodes NOT to copy
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

		/// Copies nodes from one POIFS to the other minus the excepts
		///
		/// @param source
		///            is the source POIFS to copy from
		/// @param target
		///            is the target POIFS to copy to
		/// @param excepts
		///            is a list of Strings specifying what nodes NOT to copy
		public static void CopyNodes(POIFSFileSystem source, POIFSFileSystem target, List<string> excepts)
		{
			CopyNodes(source.Root, target.Root, excepts);
		}
	}
}
