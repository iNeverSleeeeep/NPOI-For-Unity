using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections;
using System.IO;

namespace NPOI.OpenXml4Net.OPC.Internal
{
	public class ZipHelper
	{
		/// Forward slash use to convert part name between OPC and zip item naming
		/// conventions.
		private static string FORWARD_SLASH = "/";

		/// Buffer to read data from file. Use big buffer to improve performaces. the
		/// InputStream class is reading only 8192 bytes per read call (default value
		/// set by sun)
		public static int READ_WRITE_FILE_BUFFER_SIZE = 8192;

		/// Prevent this class to be instancied.
		private ZipHelper()
		{
		}

		/// Retrieve the zip entry of the core properties part.
		///
		/// @throws OpenXml4NetException
		///             Throws if internal error occurs.
		public static ZipEntry GetCorePropertiesZipEntry(ZipPackage pkg)
		{
			PackageRelationship relationship = pkg.GetRelationshipsByType("http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties").GetRelationship(0);
			if (relationship == null)
			{
				return null;
			}
			return new ZipEntry(relationship.TargetUri.OriginalString);
		}

		/// Retrieve the Zip entry of the content types part.
		public static ZipEntry GetContentTypeZipEntry(ZipPackage pkg)
		{
			IEnumerator entries = pkg.ZipArchive.Entries;
			while (entries.MoveNext())
			{
				ZipEntry zipEntry = (ZipEntry)entries.Current;
				if (zipEntry.Name.Equals("[Content_Types].xml"))
				{
					return zipEntry;
				}
			}
			return null;
		}

		/// Convert a zip name into an OPC name by adding a leading forward slash to
		/// the specified item name.
		///
		/// @param zipItemName
		///            Zip item name to convert.
		/// @return An OPC compliant name.
		public static string GetOPCNameFromZipItemName(string zipItemName)
		{
			if (zipItemName == null)
			{
				throw new ArgumentException("zipItemName");
			}
			if (zipItemName.StartsWith(FORWARD_SLASH))
			{
				return zipItemName;
			}
			return FORWARD_SLASH + zipItemName;
		}

		/// Convert an OPC item name into a zip item name by removing any leading
		/// forward slash if it exist.
		///
		/// @param opcItemName
		///            The OPC item name to convert.
		/// @return A zip item name without any leading slashes.
		public static string GetZipItemNameFromOPCName(string opcItemName)
		{
			if (opcItemName == null)
			{
				throw new ArgumentException("opcItemName");
			}
			string text = opcItemName;
			while (text.StartsWith(FORWARD_SLASH))
			{
				text = text.Substring(1);
			}
			return text;
		}

		/// Convert an OPC item name into a zip URI by removing any leading forward
		/// slash if it exist.
		///
		/// @param opcItemName
		///            The OPC item name to convert.
		/// @return A zip URI without any leading slashes.
		public static Uri GetZipURIFromOPCName(string opcItemName)
		{
			if (opcItemName != null)
			{
				string text = opcItemName;
				while (text.StartsWith(FORWARD_SLASH))
				{
					text = text.Substring(1);
				}
				try
				{
					return PackagingUriHelper.ParseUri(text, UriKind.RelativeOrAbsolute);
				}
				catch (UriFormatException)
				{
					return null;
				}
			}
			throw new ArgumentException("opcItemName");
		}

		/// Opens the specified file as a zip, or returns null if no such file exists
		///
		/// @param file
		///            The file to open.
		/// @return The zip archive freshly open.
		public static ZipFile OpenZipFile(FileInfo file)
		{
			if (!file.Exists)
			{
				return null;
			}
			return new ZipFile(File.OpenRead(file.FullName));
		}

		/// Retrieve and open a zip file with the specified path.
		///
		/// @param path
		///            The file path.
		/// @return The zip archive freshly open.
		public static ZipFile OpenZipFile(string path)
		{
			if (!File.Exists(path))
			{
				return null;
			}
			return new ZipFile(File.OpenRead(path));
		}
	}
}
