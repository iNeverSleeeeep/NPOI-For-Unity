using System.IO;

namespace NPOI.OpenXml4Net.OPC.Internal
{
	/// Provide useful method to manage file.
	///
	/// @author Julien Chable
	/// @version 0.1
	public class FileHelper
	{
		/// Get the directory part of the specified file path.
		///
		/// @param f
		///            File to process.
		/// @return The directory path from the specified
		public static string GetDirectory(string filepath)
		{
			return Path.GetDirectoryName(filepath).Replace("\\", "/");
		}

		/// Copy a file.
		///
		/// @param in
		///            The source file.
		/// @param out
		///            The target location.
		/// @throws IOException
		///             If an I/O error occur.
		public static void CopyFile(string inpath, string outpath)
		{
			File.Copy(inpath, outpath, true);
		}

		/// Get file name from the specified File object.
		public static string GetFilename(string filepath)
		{
			int length = filepath.Length;
			int num = length;
			while (--num >= 0)
			{
				char c = filepath[num];
				if (c == '\\')
				{
					return filepath.Substring(num + 1, length);
				}
			}
			return "";
		}
	}
}
