using System;
using System.IO;
using System.Threading;

namespace NPOI.Util
{
	public class TempFile
	{
		/// Creates a temporary file.  Files are collected into one directory and by default are
		/// deleted on exit from the VM.  Files can be kept by defining the system property
		/// <c>poi.keep.tmp.files</c>.
		///
		/// Dont forget to close all files or it might not be possible to delete them.
		public static FileInfo CreateTempFile(string prefix, string suffix)
		{
			Random random = new Random(DateTime.Now.Millisecond);
			string text = prefix + random.Next() + suffix;
			FileStream fileStream = File.Create(text);
			fileStream.Close();
			return new FileInfo(text);
		}

		public static string GetTempFilePath(string prefix, string suffix)
		{
			Random random = new Random(DateTime.Now.Millisecond);
			Thread.Sleep(10);
			return prefix + random.Next() + suffix;
		}
	}
}
