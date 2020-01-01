using ICSharpCode.SharpZipLib.Zip;
using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.OpenXml4Net.OPC.Internal.Marshallers;
using NPOI.OpenXml4Net.Util;
using NPOI.Util;
using System;
using System.Collections;
using System.IO;

namespace NPOI.OpenXml4Net.OPC
{
	/// Physical zip package.
	///
	/// @author Julien Chable
	public class ZipPackage : OPCPackage
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(ZipPackage));

		/// Zip archive, as either a file on disk,
		///  or a stream
		private ZipEntrySource zipArchive;

		/// Get the zip archive
		///
		/// @return The zip archive.
		public ZipEntrySource ZipArchive
		{
			get
			{
				return zipArchive;
			}
		}

		/// Constructor. Creates a new ZipPackage.
		public ZipPackage()
			: base(OPCPackage.defaultPackageAccess)
		{
			zipArchive = null;
		}

		/// Constructor. <b>Operation not supported.</b>
		///
		/// @param in
		///            Zip input stream to load.
		/// @param access
		/// @throws ArgumentException
		///             If the specified input stream not an instance of
		///             ZipInputStream.
		public ZipPackage(Stream in1, PackageAccess access)
			: base(access)
		{
			zipArchive = new ZipInputStreamZipEntrySource(new ZipInputStream(in1));
		}

		/// Constructor. Opens a Zip based Open XML document.
		///
		/// @param path
		///            The path of the file to open or create.
		/// @param access
		///            The package access mode.
		/// @throws InvalidFormatException
		///             If the content type part parsing encounters an error.
		public ZipPackage(string path, PackageAccess access)
			: base(access)
		{
			ZipFile zipFile = null;
			try
			{
				zipFile = ZipHelper.OpenZipFile(path);
			}
			catch (IOException innerException)
			{
				throw new InvalidOperationException("Can't open the specified file: '" + path + "'", innerException);
			}
			zipArchive = new ZipFileZipEntrySource(zipFile);
		}

		/// Constructor. Opens a Zip based Open XML document.
		///
		/// @param file
		///            The file to open or create.
		/// @param access
		///            The package access mode.
		/// @throws InvalidFormatException
		///             If the content type part parsing encounters an error.
		public ZipPackage(FileInfo file, PackageAccess access)
			: base(access)
		{
			ZipFile zipFile = null;
			try
			{
				zipFile = ZipHelper.OpenZipFile(file);
			}
			catch (IOException innerException)
			{
				throw new InvalidOperationException("Can't open the specified file: '" + file + "'", innerException);
			}
			zipArchive = new ZipFileZipEntrySource(zipFile);
		}

		/// Retrieves the parts from this package. We assume that the package has not
		/// been yet inspect to retrieve all the parts, this method will open the
		/// archive and look for all parts contain inside it. If the package part
		/// list is not empty, it will be emptied.
		///
		/// @return All parts contain in this package.
		/// @throws InvalidFormatException
		///             Throws if the package is not valid.
		protected override PackagePart[] GetPartsImpl()
		{
			if (partList == null)
			{
				partList = new PackagePartCollection();
			}
			if (zipArchive == null)
			{
				PackagePart[] array = new PackagePart[partList.Values.Count];
				partList.Values.CopyTo(array, 0);
				return array;
			}
			IEnumerator entries = zipArchive.Entries;
			while (entries.MoveNext())
			{
				ZipEntry zipEntry = (ZipEntry)entries.Current;
				if (zipEntry.Name.ToLower().Equals("[Content_Types].xml".ToLower()))
				{
					try
					{
						contentTypeManager = new ZipContentTypeManager(ZipArchive.GetInputStream(zipEntry), this);
					}
					catch (IOException ex)
					{
						throw new InvalidFormatException(ex.Message);
					}
					break;
				}
			}
			if (contentTypeManager == null)
			{
				throw new InvalidFormatException("Package should contain a content type part [M1.13]");
			}
			entries = zipArchive.Entries;
			while (entries.MoveNext())
			{
				ZipEntry zipEntry2 = (ZipEntry)entries.Current;
				PackagePartName packagePartName = BuildPartName(zipEntry2);
				if (packagePartName != null)
				{
					string contentType = contentTypeManager.GetContentType(packagePartName);
					if (contentType != null && contentType.Equals(ContentTypes.RELATIONSHIPS_PART))
					{
						try
						{
							partList[packagePartName] = new ZipPackagePart(this, zipEntry2, packagePartName, contentType);
						}
						catch (InvalidOperationException ex2)
						{
							throw new InvalidFormatException(ex2.Message);
						}
					}
				}
			}
			entries = zipArchive.Entries;
			while (entries.MoveNext())
			{
				ZipEntry zipEntry3 = entries.Current as ZipEntry;
				PackagePartName packagePartName2 = BuildPartName(zipEntry3);
				if (packagePartName2 != null)
				{
					string contentType2 = contentTypeManager.GetContentType(packagePartName2);
					if (contentType2 == null || !contentType2.Equals(ContentTypes.RELATIONSHIPS_PART))
					{
						if (contentType2 == null)
						{
							throw new InvalidFormatException("The part " + packagePartName2.URI.OriginalString + " does not have any content type ! Rule: Package require content types when retrieving a part from a package. [M.1.14]");
						}
						try
						{
							partList[packagePartName2] = new ZipPackagePart(this, zipEntry3, packagePartName2, contentType2);
						}
						catch (InvalidOperationException ex3)
						{
							throw new InvalidFormatException(ex3.Message);
						}
					}
				}
			}
			ZipPackagePart[] array2 = new ZipPackagePart[partList.Count];
			partList.Values.CopyTo(array2, 0);
			return array2;
		}

		/// Builds a PackagePartName for the given ZipEntry,
		///  or null if it's the content types / invalid part
		private PackagePartName BuildPartName(ZipEntry entry)
		{
			try
			{
				if (entry.Name.ToLower().Equals("[Content_Types].xml".ToLower()))
				{
					return null;
				}
				return PackagingUriHelper.CreatePartName(ZipHelper.GetOPCNameFromZipItemName(entry.Name));
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// Create a new MemoryPackagePart from the specified URI and content type
		///
		///
		/// aram partName The part URI.
		///
		/// @param contentType
		///            The part content type.
		/// @return The newly created zip package part, else <b>null</b>.
		protected override PackagePart CreatePartImpl(PackagePartName partName, string contentType, bool loadRelationships)
		{
			if (contentType == null)
			{
				throw new ArgumentException("contentType");
			}
			if (partName != null)
			{
				try
				{
					return new MemoryPackagePart(this, partName, contentType, loadRelationships);
				}
				catch (InvalidFormatException)
				{
					return null;
				}
			}
			throw new ArgumentException("partName");
		}

		/// Delete a part from the package
		///
		/// @throws ArgumentException
		///             Throws if the part URI is nulll or invalid.
		protected override void RemovePartImpl(PackagePartName partName)
		{
			if (partName == null)
			{
				throw new ArgumentException("partUri");
			}
		}

		/// Flush the package. Do nothing.
		protected override void FlushImpl()
		{
		}

		/// Close and save the package.
		///
		/// @see #close()
		protected override void CloseImpl()
		{
			Flush();
			if (originalPackagePath != null && !"".Equals(originalPackagePath))
			{
				if (!File.Exists(originalPackagePath))
				{
					throw new InvalidOperationException("Can't close a package not previously open with the open() method !");
				}
				string text = GenerateTempFileName(FileHelper.GetDirectory(originalPackagePath));
				FileInfo fileInfo = TempFile.CreateTempFile(text, ".tmp");
				try
				{
					FileStream fileStream = File.OpenWrite(fileInfo.FullName);
					Save(fileStream);
					if (zipArchive != null)
					{
						zipArchive.Close();
					}
					fileStream.Close();
					FileHelper.CopyFile(fileInfo.FullName, originalPackagePath);
				}
				finally
				{
					File.Delete(text);
					logger.Log(5, "The temporary file: '" + text + "' cannot be deleted ! Make sure that no other application use it.");
				}
			}
		}

		/// Create a unique identifier to be use as a temp file name.
		///
		/// @return A unique identifier use to be use as a temp file name.
		private string GenerateTempFileName(string directory)
		{
			FileInfo fileInfo = null;
			string text = null;
			do
			{
				text = directory + "\\OpenXml4Net" + DateTime.Now.Ticks;
				fileInfo = new FileInfo(text);
			}
			while (File.Exists(text));
			return fileInfo.Name;
		}

		/// Close the package without saving the document. Discard all the changes
		/// made to this package.
		protected override void RevertImpl()
		{
			try
			{
				if (zipArchive != null)
				{
					zipArchive.Close();
				}
			}
			catch (IOException)
			{
			}
		}

		/// Implement the getPart() method to retrieve a part from its URI in the
		/// current package
		///
		///
		/// @see #getPart(PackageRelationship)
		protected override PackagePart GetPartImpl(PackagePartName partName)
		{
			if (partList.ContainsKey(partName))
			{
				return partList[partName];
			}
			return null;
		}

		/// Save this package into the specified stream
		///
		///
		/// @param outputStream
		///            The stream use to save this package.
		///
		/// @see #save(OutputStream)
		protected override void SaveImpl(Stream outputStream)
		{
			ThrowExceptionIfReadOnly();
			ZipOutputStream zipOutputStream = null;
			try
			{
				zipOutputStream = ((outputStream is ZipOutputStream) ? ((ZipOutputStream)outputStream) : new ZipOutputStream(outputStream));
				zipOutputStream.UseZip64 = UseZip64.Off;
				if (GetPartsByRelationshipType("http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties").Count == 0)
				{
					logger.Log(1, "Save core properties part");
					new ZipPackagePropertiesMarshaller().Marshall(packageProperties, zipOutputStream);
					relationships.AddRelationship(packageProperties.PartName.URI, TargetMode.Internal, "http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties", null);
					if (!contentTypeManager.IsContentTypeRegister(ContentTypes.CORE_PROPERTIES_PART))
					{
						contentTypeManager.AddContentType(packageProperties.PartName, ContentTypes.CORE_PROPERTIES_PART);
					}
				}
				logger.Log(1, "Save package relationships");
				ZipPartMarshaller.MarshallRelationshipPart(base.Relationships, PackagingUriHelper.PACKAGE_RELATIONSHIPS_ROOT_PART_NAME, zipOutputStream);
				logger.Log(1, "Save content types part");
				contentTypeManager.Save(zipOutputStream);
				foreach (PackagePart part in GetParts())
				{
					if (!part.IsRelationshipPart)
					{
						logger.Log(1, "Save part '" + ZipHelper.GetZipItemNameFromOPCName(part.PartName.Name) + "'");
						if (partMarshallers.ContainsKey(part.contentType))
						{
							PartMarshaller partMarshaller = partMarshallers[part.contentType];
							if (!partMarshaller.Marshall(part, zipOutputStream))
							{
								throw new OpenXml4NetException("The part " + part.PartName.URI + " fail to be saved in the stream with marshaller " + partMarshaller);
							}
						}
						else if (!defaultPartMarshaller.Marshall(part, zipOutputStream))
						{
							throw new OpenXml4NetException("The part " + part.PartName.URI + " fail to be saved in the stream with marshaller " + defaultPartMarshaller);
						}
					}
				}
				zipOutputStream.Close();
			}
			catch (Exception ex)
			{
				logger.Log(7, "fail to save: an error occurs while saving the package : " + ex.Message);
			}
		}
	}
}
