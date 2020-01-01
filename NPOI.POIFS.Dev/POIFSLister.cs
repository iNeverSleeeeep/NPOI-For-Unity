using NPOI.POIFS.FileSystem;
using System;
using System.Collections;
using System.IO;

namespace NPOI.POIFS.Dev
{
	public class POIFSLister
	{
		public static void ViewFile(string filename)
		{
			using (Stream stream = new FileStream(filename, FileMode.Open))
			{
				POIFSFileSystem pOIFSFileSystem = new POIFSFileSystem(stream);
				DisplayDirectory(pOIFSFileSystem.Root, "");
			}
		}

		public static void DisplayDirectory(DirectoryNode dir, string indent)
		{
			Console.WriteLine(indent + dir.Name + " -");
			string text = indent + "  ";
			IEnumerator entries = dir.Entries;
			while (entries.MoveNext())
			{
				object current = entries.Current;
				if (current is DirectoryNode)
				{
					DisplayDirectory((DirectoryNode)current, text);
				}
				else
				{
					DocumentNode documentNode = (DocumentNode)current;
					string text2 = documentNode.Name;
					if (text2[0] < '\n')
					{
						string str = "(0x0" + (int)text2[0] + ")" + text2.Substring(1);
						text2 = text2.Substring(1) + " <" + str + ">";
					}
					Console.WriteLine(text + text2);
				}
			}
		}
	}
}
