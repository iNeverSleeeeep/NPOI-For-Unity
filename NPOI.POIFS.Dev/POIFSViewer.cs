using NPOI.POIFS.FileSystem;
using System;
using System.Collections;
using System.IO;
using System.Text;

namespace NPOI.POIFS.Dev
{
	public class POIFSViewer
	{
		public static void ViewFile(string filename, bool printName)
		{
			if (printName)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(".");
				for (int i = 0; i < filename.Length; i++)
				{
					stringBuilder.Append("-");
				}
				stringBuilder.Append(".");
				Console.WriteLine(stringBuilder);
				Console.WriteLine("|" + filename + "|");
				Console.WriteLine(stringBuilder);
			}
			try
			{
				using (Stream stream = File.OpenRead(filename))
				{
					POIFSViewable viewable = new POIFSFileSystem(stream);
					IList list = POIFSViewEngine.InspectViewable(viewable, drilldown: true, 0, "  ");
					IEnumerator enumerator = list.GetEnumerator();
					while (enumerator.MoveNext())
					{
						Console.Write(enumerator.Current);
					}
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
