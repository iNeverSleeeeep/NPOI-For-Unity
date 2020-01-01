using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.OpenXml4Net.OPC.Internal.Marshallers;
using NPOI.OpenXml4Net.OPC.Internal.Unmarshallers;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NPOI.OpenXml4Net.OPC
{
	/// Represents a container that can store multiple data objects.
	///
	/// @author Julien Chable, CDubet
	/// @version 0.1
	public abstract class OPCPackage : RelationshipSource
	{
		/// Logger.
		private static POILogger logger = POILogFactory.GetLogger(typeof(OPCPackage));

		/// Default package access.
		protected static PackageAccess defaultPackageAccess = PackageAccess.READ_WRITE;

		/// Package access.
		private PackageAccess packageAccess;

		/// Package parts collection.
		protected PackagePartCollection partList;

		/// Package relationships.
		protected PackageRelationshipCollection relationships;

		/// Part marshallers by content type.
		protected SortedList<ContentType, PartMarshaller> partMarshallers;

		/// Default part marshaller.
		protected PartMarshaller defaultPartMarshaller;

		/// Part unmarshallers by content type.
		protected SortedList<ContentType, PartUnmarshaller> partUnmarshallers;

		/// Core package properties.
		protected PackagePropertiesPart packageProperties;

		/// Manage parts content types of this package.
		protected ContentTypeManager contentTypeManager;

		/// Flag if a modification is done to the document.
		protected bool isDirty;

		/// File path of this package.
		protected string originalPackagePath;

		/// Output stream for writing this package.
		protected Stream output;

		/// Retrieves all package relationships.
		///
		/// @return All package relationships of this package.
		/// @throws OpenXml4NetException
		/// @see #GetRelationshipsHelper(String)
		public PackageRelationshipCollection Relationships
		{
			get
			{
				return GetRelationshipsHelper(null);
			}
		}

		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#hasRelationships()
		public bool HasRelationships
		{
			get
			{
				return relationships.Size > 0;
			}
		}

		/// Constructor.
		///
		/// @param access
		///            Package access.
		public OPCPackage(PackageAccess access)
		{
			if (GetType() != typeof(ZipPackage))
			{
				throw new ArgumentException("PackageBase may not be subclassed");
			}
			Init();
			packageAccess = access;
		}

		/// Initialize the package instance.
		private void Init()
		{
			partMarshallers = new SortedList<ContentType, PartMarshaller>(5);
			partUnmarshallers = new SortedList<ContentType, PartUnmarshaller>(2);
			try
			{
				partUnmarshallers.Add(new ContentType(ContentTypes.CORE_PROPERTIES_PART), new PackagePropertiesUnmarshaller());
				defaultPartMarshaller = new DefaultMarshaller();
				partMarshallers.Add(new ContentType(ContentTypes.CORE_PROPERTIES_PART), new ZipPackagePropertiesMarshaller());
			}
			catch (InvalidFormatException)
			{
				throw new OpenXml4NetException("Package.init() : this exception should never happen, if you read this message please send a mail to the developers team.");
			}
		}

		/// Open a package with read/write permission.
		///
		/// @param path
		///            The document path.
		/// @return A Package object, else <b>null</b>.
		/// @throws InvalidFormatException
		///             If the specified file doesn't exist, and a parsing error
		///             occur.
		public static OPCPackage Open(string path)
		{
			return Open(path, defaultPackageAccess);
		}

		/// Open a package with read/write permission.
		///
		/// @param file
		///            The file to open.
		/// @return A Package object, else <b>null</b>.
		/// @throws InvalidFormatException
		///             If the specified file doesn't exist, and a parsing error
		///             occur.
		public static OPCPackage Open(FileInfo file)
		{
			return Open(file, defaultPackageAccess);
		}

		/// Open a package.
		///
		/// @param path
		///            The document path.
		/// @param access
		///            PackageBase access.
		/// @return A PackageBase object, else <b>null</b>.
		/// @throws InvalidFormatException
		///             If the specified file doesn't exist, and a parsing error
		///             occur.
		public static OPCPackage Open(string path, PackageAccess access)
		{
			if (path == null || "".Equals(path.Trim()) || new DirectoryInfo(path).Exists)
			{
				throw new ArgumentException("path");
			}
			OPCPackage oPCPackage = new ZipPackage(path, access);
			if (oPCPackage.partList == null && access != PackageAccess.WRITE)
			{
				oPCPackage.GetParts();
			}
			oPCPackage.originalPackagePath = new DirectoryInfo(path).FullName;
			return oPCPackage;
		}

		/// Open a package.
		///
		/// @param file
		///            The file to open.
		/// @param access
		///            PackageBase access.
		/// @return A PackageBase object, else <b>null</b>.
		/// @throws InvalidFormatException
		///             If the specified file doesn't exist, and a parsing error
		///             occur.
		public static OPCPackage Open(FileInfo file, PackageAccess access)
		{
			if (file == null || new DirectoryInfo(file.FullName).Exists)
			{
				throw new ArgumentException("file");
			}
			OPCPackage oPCPackage = new ZipPackage(file, access);
			if (oPCPackage.partList == null && access != PackageAccess.WRITE)
			{
				oPCPackage.GetParts();
			}
			oPCPackage.originalPackagePath = file.FullName;
			return oPCPackage;
		}

		/// Open a package.
		///
		/// Note - uses quite a bit more memory than {@link #open(String)}, which
		/// doesn't need to hold the whole zip file in memory, and can take advantage
		/// of native methods
		///
		/// @param in
		///            The InputStream to read the package from
		/// @return A PackageBase object
		public static OPCPackage Open(Stream in1)
		{
			OPCPackage oPCPackage = new ZipPackage(in1, PackageAccess.READ_WRITE);
			if (oPCPackage.partList == null)
			{
				oPCPackage.GetParts();
			}
			return oPCPackage;
		}

		/// Opens a package if it exists, else it Creates one.
		///
		/// @param file
		///            The file to open or to Create.
		/// @return A newly Created package if the specified file does not exist,
		///         else the package extract from the file.
		/// @throws InvalidFormatException
		///             Throws if the specified file exist and is not valid.
		public static OPCPackage OpenOrCreate(string path)
		{
			OPCPackage oPCPackage = null;
			if (File.Exists(path))
			{
				return Open(path);
			}
			return Create(path);
		}

		/// Creates a new package.
		///
		/// @param file
		///            Path of the document.
		/// @return A newly Created PackageBase ready to use.
		public static OPCPackage Create(string path)
		{
			if (new DirectoryInfo(path).Exists)
			{
				throw new ArgumentException("file");
			}
			if (File.Exists(path))
			{
				throw new InvalidOperationException("This package (or file) already exists : use the open() method or delete the file.");
			}
			OPCPackage oPCPackage = null;
			oPCPackage = new ZipPackage();
			oPCPackage.originalPackagePath = new FileInfo(path).Name;
			ConfigurePackage(oPCPackage);
			return oPCPackage;
		}

		public static OPCPackage Create(Stream output)
		{
			OPCPackage oPCPackage = null;
			oPCPackage = new ZipPackage();
			oPCPackage.originalPackagePath = null;
			oPCPackage.output = output;
			ConfigurePackage(oPCPackage);
			return oPCPackage;
		}

		/// Configure the package.
		///
		/// @param pkg
		private static void ConfigurePackage(OPCPackage pkg)
		{
			try
			{
				pkg.contentTypeManager = new ZipContentTypeManager(null, pkg);
				pkg.contentTypeManager.AddContentType(PackagingUriHelper.CreatePartName(PackagingUriHelper.PACKAGE_RELATIONSHIPS_ROOT_URI), ContentTypes.RELATIONSHIPS_PART);
				pkg.contentTypeManager.AddContentType(PackagingUriHelper.CreatePartName("/default.xml"), ContentTypes.PLAIN_OLD_XML);
				pkg.packageProperties = new PackagePropertiesPart(pkg, PackagingUriHelper.CORE_PROPERTIES_PART_NAME);
				pkg.packageProperties.SetCreatorProperty("Generated by OpenXml4Net");
				pkg.packageProperties.SetCreatedProperty(DateTime.Now);
			}
			catch (InvalidFormatException)
			{
				throw;
			}
		}

		/// Flush the package : save all.
		///
		/// @see #close()
		public void Flush()
		{
			ThrowExceptionIfReadOnly();
			if (packageProperties != null)
			{
				packageProperties.Flush();
			}
			FlushImpl();
		}

		/// Close the package and save its content.
		///
		/// @throws IOException
		///             If an IO exception occur during the saving process.
		public void Close()
		{
			if (packageAccess == PackageAccess.READ)
			{
				logger.Log(5, "The close() method is intended to SAVE a package. This package is open in READ ONLY mode, use the revert() method instead !");
				Revert();
			}
			else
			{
				if (originalPackagePath != null && !"".Equals(originalPackagePath.Trim()))
				{
					if (!File.Exists(originalPackagePath))
					{
						Save(originalPackagePath);
					}
					else
					{
						CloseImpl();
					}
				}
				else if (output != null)
				{
					Save(output);
				}
				contentTypeManager.ClearAll();
			}
		}

		/// Close the package WITHOUT saving its content. Reinitialize this package
		/// and cancel all changes done to it.
		public void Revert()
		{
			RevertImpl();
		}

		/// Add a thumbnail to the package. This method is provided to make easier
		/// the addition of a thumbnail in a package. You can do the same work by
		/// using the traditionnal relationship and part mechanism.
		///
		/// @param path
		///            The full path to the image file.
		public void AddThumbnail(string path)
		{
			if ("".Equals(path))
			{
				throw new ArgumentException("path");
			}
			string text = path.Substring(path.LastIndexOf('\\') + 1);
			string contentTypeFromFileExtension = ContentTypes.GetContentTypeFromFileExtension(text);
			PackagePartName packagePartName = null;
			try
			{
				packagePartName = PackagingUriHelper.CreatePartName("/docProps/" + text);
			}
			catch (InvalidFormatException)
			{
				try
				{
					packagePartName = PackagingUriHelper.CreatePartName("/docProps/thumbnail" + path.Substring(path.LastIndexOf(".") + 1));
				}
				catch (InvalidFormatException)
				{
					throw new InvalidOperationException("Can't add a thumbnail file named '" + text + "'");
				}
			}
			if (GetPart(packagePartName) != null)
			{
				throw new InvalidOperationException("You already add a thumbnail named '" + text + "'");
			}
			PackagePart packagePart = CreatePart(packagePartName, contentTypeFromFileExtension, false);
			AddRelationship(packagePartName, TargetMode.Internal, "http://schemas.openxmlformats.org/package/2006/relationships/metadata/thumbnail");
			StreamHelper.CopyStream(new FileStream(path, FileMode.Open), packagePart.GetOutputStream());
		}

		/// Throws an exception if the package access mode is in read only mode
		/// (PackageAccess.Read).
		///
		/// @throws InvalidOperationException
		///             Throws if a writing operation is done on a read only package.
		/// @see org.apache.poi.OpenXml4Net.opc.PackageAccess
		internal void ThrowExceptionIfReadOnly()
		{
			if (packageAccess == PackageAccess.READ)
			{
				throw new InvalidOperationException("Operation not allowed, document open in read only mode!");
			}
		}

		/// Throws an exception if the package access mode is in write only mode
		/// (PackageAccess.Write). This method is call when other methods need write
		/// right.
		///
		/// @throws InvalidOperationException
		///             Throws if a read operation is done on a write only package.
		/// @see org.apache.poi.OpenXml4Net.opc.PackageAccess
		internal void ThrowExceptionIfWriteOnly()
		{
			if (packageAccess == PackageAccess.WRITE)
			{
				throw new InvalidOperationException("Operation not allowed, document open in write only mode!");
			}
		}

		/// Retrieves or Creates if none exists, core package property part.
		///
		/// @return The PackageProperties part of this package.
		public PackageProperties GetPackageProperties()
		{
			ThrowExceptionIfWriteOnly();
			if (packageProperties == null)
			{
				packageProperties = new PackagePropertiesPart(this, PackagingUriHelper.CORE_PROPERTIES_PART_NAME);
			}
			return packageProperties;
		}

		public bool PartExists(Uri uri)
		{
			if (uri.IsAbsoluteUri)
			{
				return false;
			}
			PackagePart partImpl = GetPartImpl(new PackagePartName(uri.OriginalString, true));
			return partImpl != null;
		}

		public PackagePart GetPart(Uri uri)
		{
			ThrowExceptionIfWriteOnly();
			PackagePartName packagePartName = new PackagePartName(uri.ToString(), true);
			if (packagePartName == null)
			{
				throw new ArgumentException("PartName");
			}
			if (partList == null)
			{
				try
				{
					GetParts();
				}
				catch (InvalidFormatException)
				{
					return null;
				}
			}
			return GetPartImpl(packagePartName);
		}

		/// Retrieve a part identified by its name.
		///
		/// @param PartName
		///            Part name of the part to retrieve.
		/// @return The part with the specified name, else <code>null</code>.
		public PackagePart GetPart(PackagePartName partName)
		{
			ThrowExceptionIfWriteOnly();
			if (partName == null)
			{
				throw new ArgumentException("PartName");
			}
			if (partList == null)
			{
				try
				{
					GetParts();
				}
				catch (InvalidFormatException)
				{
					return null;
				}
			}
			return GetPartImpl(partName);
		}

		/// Retrieve parts by content type.
		///
		/// @param contentType
		///            The content type criteria.
		/// @return All part associated to the specified content type.
		public List<PackagePart> GetPartsByContentType(string contentType)
		{
			List<PackagePart> list = new List<PackagePart>();
			foreach (PackagePart value in partList.Values)
			{
				if (value.ContentType.Equals(contentType))
				{
					list.Add(value);
				}
			}
			return list;
		}

		/// Retrieve parts by relationship type.
		///
		/// @param relationshipType
		///            Relationship type.
		/// @return All parts which are the target of a relationship with the
		///         specified type, if the method can't retrieve relationships from
		///         the package, then return <code>null</code>.
		public List<PackagePart> GetPartsByRelationshipType(string relationshipType)
		{
			if (relationshipType == null)
			{
				throw new ArgumentException("relationshipType");
			}
			List<PackagePart> list = new List<PackagePart>();
			foreach (PackageRelationship item in GetRelationshipsByType(relationshipType))
			{
				list.Add(GetPart(item));
			}
			return list;
		}

		public List<PackagePart> GetPartsByName(Regex namePattern)
		{
			if (namePattern == null)
			{
				throw new ArgumentException("name pattern must not be null");
			}
			List<PackagePart> list = new List<PackagePart>();
			foreach (PackagePart value in partList.Values)
			{
				PackagePartName partName = value.PartName;
				string name = partName.Name;
				if (namePattern.IsMatch(name))
				{
					list.Add(value);
				}
			}
			return list;
		}

		/// Get the target part from the specified relationship.
		///
		/// @param partRel
		///            The part relationship uses to retrieve the part.
		public PackagePart GetPart(PackageRelationship partRel)
		{
			PackagePart result = null;
			EnsureRelationships();
			foreach (PackageRelationship relationship in relationships)
			{
				if (relationship.RelationshipType.Equals(partRel.RelationshipType))
				{
					try
					{
						result = GetPart(PackagingUriHelper.CreatePartName(relationship.TargetUri));
						return result;
					}
					catch (InvalidFormatException)
					{
					}
				}
			}
			return result;
		}

		/// Load the parts of the archive if it has not been done yet. The
		/// relationships of each part are not loaded.
		/// Note - Rule M4.1 states that there may only ever be one Core
		///  Properties Part, but Office produced files will sometimes
		///  have multiple! As Office ignores all but the first, we relax
		///  Compliance with Rule M4.1, and ignore all others silently too. 
		/// @return All this package's parts.
		public List<PackagePart> GetParts()
		{
			ThrowExceptionIfWriteOnly();
			if (partList == null)
			{
				bool flag = false;
				bool flag2 = true;
				PackagePart[] partsImpl = GetPartsImpl();
				partList = new PackagePartCollection();
				PackagePart[] array = partsImpl;
				foreach (PackagePart packagePart in array)
				{
					bool flag3 = false;
					foreach (PackagePartName key in partList.Keys)
					{
						if (packagePart.PartName.Name.StartsWith(key.Name))
						{
							flag3 = true;
							break;
						}
					}
					if (flag3)
					{
						throw new InvalidFormatException("A part with the name '" + packagePart.PartName + "' already exist : Packages shall not contain equivalent part names and package implementers shall neither create nor recognize packages with equivalent part names. [M1.12]");
					}
					if (packagePart.ContentType.Equals(ContentTypes.CORE_PROPERTIES_PART))
					{
						if (!flag)
						{
							flag = true;
						}
						else
						{
							Console.WriteLine("OPC Compliance error [M4.1]: there is more than one core properties relationship in the package ! POI will use only the first, but other software may reject this file.");
						}
					}
					if (partUnmarshallers.ContainsKey(packagePart.contentType))
					{
						PartUnmarshaller partUnmarshaller = partUnmarshallers[packagePart.contentType];
						UnmarshallContext context = new UnmarshallContext(this, packagePart.PartName);
						try
						{
							PackagePart packagePart2 = partUnmarshaller.Unmarshall(context, packagePart.GetInputStream());
							partList[packagePart2.PartName] = packagePart2;
							if (packagePart2 is PackagePropertiesPart && flag && flag2)
							{
								packageProperties = (PackagePropertiesPart)packagePart2;
								flag2 = false;
							}
						}
						catch (IOException)
						{
							logger.Log(5, "Unmarshall operation : IOException for " + packagePart.PartName);
						}
						catch (InvalidOperationException ex2)
						{
							throw new InvalidFormatException(ex2.Message);
						}
					}
					else
					{
						try
						{
							partList[packagePart.PartName] = packagePart;
						}
						catch (InvalidOperationException ex3)
						{
							throw new InvalidFormatException(ex3.Message);
						}
					}
				}
			}
			return new List<PackagePart>(partList.Values);
		}

		public PackagePart CreatePart(Uri partName, string contentType)
		{
			return CreatePart(new PackagePartName(partName.OriginalString, true), contentType, true);
		}

		/// Create and Add a part, with the specified name and content type, to the
		/// package.
		///
		/// @param PartName
		///            Part name.
		/// @param contentType
		///            Part content type.
		/// @return The newly Created part.
		/// @throws InvalidFormatException
		///             If rule M1.12 is not verified : Packages shall not contain
		///             equivalent part names and package implementers shall neither
		///             Create nor recognize packages with equivalent part names.
		/// @see #CreatePartImpl(PackagePartName, String, bool) 
		public PackagePart CreatePart(PackagePartName partName, string contentType)
		{
			return CreatePart(partName, contentType, true);
		}

		/// Create and Add a part, with the specified name and content type, to the
		/// package. For general purpose, prefer the overload version of this method
		/// without the 'loadRelationships' parameter.
		///
		/// @param PartName
		///            Part name.
		/// @param contentType
		///            Part content type.
		/// @param loadRelationships
		///            Specify if the existing relationship part, if any, logically
		///            associated to the newly Created part will be loaded.
		/// @return The newly Created part.
		/// @throws InvalidFormatException
		///             If rule M1.12 is not verified : Packages shall not contain
		///             equivalent part names and package implementers shall neither
		///             Create nor recognize packages with equivalent part names.
		/// @see {@link#CreatePartImpl(URI, String)}
		public PackagePart CreatePart(PackagePartName partName, string contentType, bool loadRelationships)
		{
			ThrowExceptionIfReadOnly();
			if (partName == null)
			{
				throw new ArgumentException("PartName");
			}
			if (contentType == null || contentType == "")
			{
				throw new ArgumentException("contentType");
			}
			bool flag = false;
			bool flag2 = false;
			foreach (PackagePartName key in partList.Keys)
			{
				if (partName.Name.StartsWith(key.Name))
				{
					flag = true;
					if (partList[key].IsDeleted)
					{
						flag2 = true;
					}
					break;
				}
			}
			if (flag && !flag2)
			{
				throw new PartAlreadyExistsException("A part with the name '" + partName.Name + "' already exists : Packages shall not contain equivalent part names and package implementers shall neither create nor recognize packages with equivalent part names. [M1.12]");
			}
			if (contentType == ContentTypes.CORE_PROPERTIES_PART && packageProperties != null)
			{
				throw new InvalidOperationException("OPC Compliance error [M4.1]: you try to add more than one core properties relationship in the package !");
			}
			PackagePart packagePart = CreatePartImpl(partName, contentType, loadRelationships);
			contentTypeManager.AddContentType(partName, contentType);
			partList[partName] = packagePart;
			isDirty = true;
			return packagePart;
		}

		/// Add a part to the package.
		///
		/// @param PartName
		///            Part name of the part to Create.
		/// @param contentType
		///            type associated with the file
		/// @param content
		///            the contents to Add. In order to have faster operation in
		///            document merge, the data are stored in memory not on a hard
		///            disk
		///
		/// @return The new part.
		/// @see #CreatePart(PackagePartName, String)
		public PackagePart CreatePart(PackagePartName partName, string contentType, MemoryStream content)
		{
			PackagePart packagePart = CreatePart(partName, contentType);
			if (packagePart == null)
			{
				return null;
			}
			if (content != null)
			{
				try
				{
					Stream outputStream = packagePart.GetOutputStream();
					if (outputStream == null)
					{
						return null;
					}
					outputStream.Write(content.ToArray(), 0, (int)content.Length);
					outputStream.Close();
					return packagePart;
				}
				catch (IOException)
				{
					return null;
				}
			}
			return null;
		}

		/// Add the specified part to the package. If a part already exists in the
		/// package with the same name as the one specified, then we replace the old
		/// part by the specified part.
		///
		/// @param part
		///            The part to Add (or replace).
		/// @return The part Added to the package, the same as the one specified.
		/// @throws InvalidFormatException
		///             If rule M1.12 is not verified : Packages shall not contain
		///             equivalent part names and package implementers shall neither
		///             Create nor recognize packages with equivalent part names.
		protected PackagePart AddPackagePart(PackagePart part)
		{
			ThrowExceptionIfReadOnly();
			if (part == null)
			{
				throw new ArgumentException("part");
			}
			if (partList.ContainsKey(part.PartName))
			{
				if (!partList[part.PartName].IsDeleted)
				{
					throw new InvalidOperationException("A part with the name '" + part.PartName.Name + "' already exists : Packages shall not contain equivalent part names and package implementers shall neither Create nor recognize packages with equivalent part names. [M1.12]");
				}
				part.IsDeleted = false;
				partList.Remove(part.PartName);
			}
			partList[part.PartName] = part;
			isDirty = true;
			return part;
		}

		/// Remove the specified part in this package. If this part is relationship
		/// part, then delete all relationships in the source part.
		///
		/// @param part
		///            The part to Remove. If <code>null</code>, skip the action.
		/// @see #RemovePart(PackagePartName)
		public void RemovePart(PackagePart part)
		{
			if (part != null)
			{
				RemovePart(part.PartName);
			}
		}

		/// Remove a part in this package. If this part is relationship part, then
		/// delete all relationships in the source part.
		///
		/// @param PartName
		///            The part name of the part to Remove.
		public void RemovePart(PackagePartName PartName)
		{
			ThrowExceptionIfReadOnly();
			if (PartName == null || !ContainPart(PartName))
			{
				throw new ArgumentException("PartName");
			}
			if (partList.ContainsKey(PartName))
			{
				partList[PartName].IsDeleted = true;
				RemovePartImpl(PartName);
				partList.Remove(PartName);
			}
			else
			{
				RemovePartImpl(PartName);
			}
			contentTypeManager.RemoveContentType(PartName);
			if (PartName.IsRelationshipPartURI())
			{
				Uri sourcePartUriFromRelationshipPartUri = PackagingUriHelper.GetSourcePartUriFromRelationshipPartUri(PartName.URI);
				PackagePartName packagePartName;
				try
				{
					packagePartName = PackagingUriHelper.CreatePartName(sourcePartUriFromRelationshipPartUri);
				}
				catch (InvalidFormatException)
				{
					logger.Log(7, "Part name URI '" + sourcePartUriFromRelationshipPartUri + "' is not valid ! This message is not intended to be displayed !");
					return;
				}
				if (packagePartName.URI.Equals(PackagingUriHelper.PACKAGE_ROOT_URI))
				{
					ClearRelationships();
				}
				else if (ContainPart(packagePartName))
				{
					PackagePart part = GetPart(packagePartName);
					if (part != null)
					{
						part.ClearRelationships();
					}
				}
			}
			isDirty = true;
		}

		/// Remove a part from this package as well as its relationship part, if one
		/// exists, and all parts listed in the relationship part. Be aware that this
		/// do not delete relationships which target the specified part.
		///
		/// @param PartName
		///            The name of the part to delete.
		/// @throws InvalidFormatException
		///             Throws if the associated relationship part of the specified
		///             part is not valid.
		public void RemovePartRecursive(PackagePartName PartName)
		{
			PackagePart packagePart = partList[PackagingUriHelper.GetRelationshipPartName(PartName)];
			PackagePart packagePart2 = partList[PartName];
			if (packagePart != null)
			{
				PackageRelationshipCollection packageRelationshipCollection = new PackageRelationshipCollection(packagePart2);
				foreach (PackageRelationship item in packageRelationshipCollection)
				{
					PackagePartName partName = PackagingUriHelper.CreatePartName(PackagingUriHelper.ResolvePartUri(item.SourceUri, item.TargetUri));
					RemovePart(partName);
				}
				RemovePart(packagePart.PartName);
			}
			RemovePart(packagePart2.PartName);
		}

		public void DeletePart(Uri uri)
		{
			PackagePartName packagePartName = new PackagePartName(uri.ToString(), true);
			if (packagePartName == null)
			{
				throw new ArgumentException("PartName");
			}
			RemovePart(packagePartName);
			RemovePart(PackagingUriHelper.GetRelationshipPartName(packagePartName));
		}

		/// Delete the part with the specified name and its associated relationships
		/// part if one exists. Prefer the use of this method to delete a part in the
		/// package, compare to the Remove() methods that don't Remove associated
		/// relationships part.
		///
		/// @param PartName
		///            Name of the part to delete
		public void DeletePart(PackagePartName partName)
		{
			if (partName == null)
			{
				throw new ArgumentException("PartName");
			}
			RemovePart(partName);
			RemovePart(PackagingUriHelper.GetRelationshipPartName(partName));
		}

		/// Delete the part with the specified name and all part listed in its
		/// associated relationships part if one exists. This process is recursively
		/// apply to all parts in the relationships part of the specified part.
		/// Prefer the use of this method to delete a part in the package, compare to
		/// the Remove() methods that don't Remove associated relationships part.
		///
		/// @param PartName
		///            Name of the part to delete
		public void DeletePartRecursive(PackagePartName partName)
		{
			if (partName == null || !ContainPart(partName))
			{
				throw new ArgumentException("PartName");
			}
			PackagePart part = GetPart(partName);
			RemovePart(partName);
			try
			{
				foreach (PackageRelationship relationship in part.Relationships)
				{
					PackagePartName partName2 = PackagingUriHelper.CreatePartName(PackagingUriHelper.ResolvePartUri(partName.URI, relationship.TargetUri));
					DeletePartRecursive(partName2);
				}
			}
			catch (InvalidFormatException ex)
			{
				logger.Log(5, "An exception occurs while deleting part '" + partName.Name + "'. Some parts may remain in the package. - " + ex.Message);
				return;
			}
			PackagePartName relationshipPartName = PackagingUriHelper.GetRelationshipPartName(partName);
			if (relationshipPartName != null && ContainPart(relationshipPartName))
			{
				RemovePart(relationshipPartName);
			}
		}

		/// Check if a part already exists in this package from its name.
		///
		/// @param PartName
		///            Part name to check.
		/// @return <i>true</i> if the part is logically Added to this package, else
		///         <i>false</i>.
		public bool ContainPart(PackagePartName partName)
		{
			return GetPart(partName) != null;
		}

		/// Add a relationship to the package (except relationships part).
		///
		/// Check rule M4.1 : The format designer shall specify and the format
		/// producer shall Create at most one core properties relationship for a
		/// package. A format consumer shall consider more than one core properties
		/// relationship for a package to be an error. If present, the relationship
		/// shall target the Core Properties part.
		///
		/// Check rule M1.25: The Relationships part shall not have relationships to
		/// any other part. Package implementers shall enforce this requirement upon
		/// the attempt to Create such a relationship and shall treat any such
		/// relationship as invalid.
		///
		/// @param targetPartName
		///            Target part name.
		/// @param targetMode
		///            Target mode, either Internal or External.
		/// @param relationshipType
		///            Relationship type.
		/// @param relID
		///            ID of the relationship.
		/// @see PackageRelationshipTypes
		public PackageRelationship AddRelationship(PackagePartName targetPartName, TargetMode targetMode, string relationshipType, string relID)
		{
			if (relationshipType.Equals("http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties") && packageProperties != null)
			{
				throw new InvalidOperationException("OPC Compliance error [M4.1]: can't add another core properties part ! Use the built-in package method instead.");
			}
			if (targetPartName.IsRelationshipPartURI())
			{
				throw new InvalidOperationException("Rule M1.25: The Relationships part shall not have relationships to any other part.");
			}
			EnsureRelationships();
			PackageRelationship result = relationships.AddRelationship(targetPartName.URI, targetMode, relationshipType, relID);
			isDirty = true;
			return result;
		}

		/// Add a package relationship.
		///
		/// @param targetPartName
		///            Target part name.
		/// @param targetMode
		///            Target mode, either Internal or External.
		/// @param relationshipType
		///            Relationship type.
		/// @see PackageRelationshipTypes
		public PackageRelationship AddRelationship(PackagePartName targetPartName, TargetMode targetMode, string relationshipType)
		{
			return AddRelationship(targetPartName, targetMode, relationshipType, null);
		}

		/// Adds an external relationship to a part (except relationships part).
		///
		/// The targets of external relationships are not subject to the same
		/// validity checks that internal ones are, as the contents is potentially
		/// any file, URL or similar.
		///
		/// @param target
		///            External target of the relationship
		/// @param relationshipType
		///            Type of relationship.
		/// @return The newly Created and Added relationship
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#AddExternalRelationship(java.lang.String,
		///      java.lang.String)
		public PackageRelationship AddExternalRelationship(string target, string relationshipType)
		{
			return AddExternalRelationship(target, relationshipType, null);
		}

		/// Adds an external relationship to a part (except relationships part).
		///
		/// The targets of external relationships are not subject to the same
		/// validity checks that internal ones are, as the contents is potentially
		/// any file, URL or similar.
		///
		/// @param target
		///            External target of the relationship
		/// @param relationshipType
		///            Type of relationship.
		/// @param id
		///            Relationship unique id.
		/// @return The newly Created and Added relationship
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#AddExternalRelationship(java.lang.String,
		///      java.lang.String)
		public PackageRelationship AddExternalRelationship(string target, string relationshipType, string id)
		{
			if (target == null)
			{
				throw new ArgumentException("target");
			}
			if (relationshipType == null)
			{
				throw new ArgumentException("relationshipType");
			}
			Uri targetUri;
			try
			{
				targetUri = PackagingUriHelper.ParseUri(target);
			}
			catch (UriFormatException arg)
			{
				throw new ArgumentException("Invalid target - " + arg);
			}
			EnsureRelationships();
			PackageRelationship result = relationships.AddRelationship(targetUri, TargetMode.External, relationshipType, id);
			isDirty = true;
			return result;
		}

		/// Delete a relationship from this package.
		///
		/// @param id
		///            Id of the relationship to delete.
		public void RemoveRelationship(string id)
		{
			if (relationships != null)
			{
				relationships.RemoveRelationship(id);
				isDirty = true;
			}
		}

		/// Retrieves all relationships with the specified type.
		///
		/// @param relationshipType
		///            The filter specifying the relationship type.
		/// @return All relationships with the specified relationship type.
		public PackageRelationshipCollection GetRelationshipsByType(string relationshipType)
		{
			ThrowExceptionIfWriteOnly();
			if (relationshipType == null)
			{
				throw new ArgumentException("relationshipType");
			}
			return GetRelationshipsHelper(relationshipType);
		}

		/// Retrieves all relationships with specified id (normally just ine because
		/// a relationship id is supposed to be unique).
		///
		/// @param id
		///            Id of the wanted relationship.
		private PackageRelationshipCollection GetRelationshipsHelper(string id)
		{
			ThrowExceptionIfWriteOnly();
			EnsureRelationships();
			return relationships.GetRelationships(id);
		}

		/// Clear package relationships.
		public void ClearRelationships()
		{
			if (relationships != null)
			{
				relationships.Clear();
				isDirty = true;
			}
		}

		/// Ensure that the relationships collection is not null.
		public void EnsureRelationships()
		{
			if (relationships == null)
			{
				try
				{
					relationships = new PackageRelationshipCollection(this);
				}
				catch (InvalidFormatException)
				{
					relationships = new PackageRelationshipCollection();
				}
			}
		}

		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#GetRelationship(java.lang.String)
		public PackageRelationship GetRelationship(string id)
		{
			return relationships.GetRelationshipByID(id);
		}

		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#isRelationshipExists(org.apache.poi.OpenXml4Net.opc.PackageRelationship)
		public bool IsRelationshipExists(PackageRelationship rel)
		{
			foreach (PackageRelationship relationship in Relationships)
			{
				if (relationship == rel)
				{
					return true;
				}
			}
			return false;
		}

		/// Add a marshaller.
		///
		/// @param contentType
		///            The content type to bind to the specified marshaller.
		/// @param marshaller
		///            The marshaller to register with the specified content type.
		public void AddMarshaller(string contentType, PartMarshaller marshaller)
		{
			try
			{
				partMarshallers[new ContentType(contentType)] = marshaller;
			}
			catch (InvalidFormatException ex)
			{
				logger.Log(5, "The specified content type is not valid: '" + ex.Message + "'. The marshaller will not be Added !");
			}
		}

		/// Add an unmarshaller.
		///
		/// @param contentType
		///            The content type to bind to the specified unmarshaller.
		/// @param unmarshaller
		///            The unmarshaller to register with the specified content type.
		public void AddUnmarshaller(string contentType, PartUnmarshaller unmarshaller)
		{
			try
			{
				partUnmarshallers[new ContentType(contentType)] = unmarshaller;
			}
			catch (InvalidFormatException ex)
			{
				logger.Log(5, "The specified content type is not valid: '" + ex.Message + "'. The unmarshaller will not be Added !");
			}
		}

		/// Remove a marshaller by its content type.
		///
		/// @param contentType
		///            The content type associated with the marshaller to Remove.
		public void RemoveMarshaller(string contentType)
		{
			partMarshallers.Remove(new ContentType(contentType));
		}

		/// Remove an unmarshaller by its content type.
		///
		/// @param contentType
		///            The content type associated with the unmarshaller to Remove.
		public void RemoveUnmarshaller(string contentType)
		{
			partUnmarshallers.Remove(new ContentType(contentType));
		}

		/// Get the package access mode.
		///
		/// @return the packageAccess The current package access.
		public PackageAccess GetPackageAccess()
		{
			return packageAccess;
		}

		/// Validates the package compliance with the OPC specifications.
		///
		/// @return <b>true</b> if the package is valid else <b>false</b>
		public bool ValidatePackage(OPCPackage pkg)
		{
			throw new InvalidOperationException("Not implemented yet !!!");
		}

		/// Save the document in the specified file.
		///
		/// @param targetFile
		///            Destination file.
		/// @throws IOException
		///             Throws if an IO exception occur.
		/// @see #save(OutputStream)
		public void Save(string path)
		{
			if (path == null)
			{
				throw new ArgumentException("targetFile");
			}
			ThrowExceptionIfReadOnly();
			FileStream fileStream = null;
			fileStream = new FileStream(path, FileMode.OpenOrCreate);
			Save(fileStream);
			fileStream.Close();
		}

		/// Save the document in the specified output stream.
		///
		/// @param outputStream
		///            The stream to save the package.
		/// @see #saveImpl(OutputStream)
		public void Save(Stream outputStream)
		{
			ThrowExceptionIfReadOnly();
			SaveImpl(outputStream);
		}

		/// Core method to Create a package part. This method must be implemented by
		/// the subclass.
		///
		/// @param PartName
		///            URI of the part to Create.
		/// @param contentType
		///            Content type of the part to Create.
		/// @return The newly Created package part.
		protected abstract PackagePart CreatePartImpl(PackagePartName PartName, string contentType, bool loadRelationships);

		/// Core method to delete a package part. This method must be implemented by
		/// the subclass.
		///
		/// @param PartName
		///            The URI of the part to delete.
		protected abstract void RemovePartImpl(PackagePartName PartName);

		/// Flush the package but not save.
		protected abstract void FlushImpl();

		/// Close the package and cause a save of the package.
		protected abstract void CloseImpl();

		/// Close the package without saving the document. Discard all changes made
		/// to this package.
		protected abstract void RevertImpl();

		/// Save the package into the specified output stream.
		///
		/// @param outputStream
		///            The output stream use to save this package.
		protected abstract void SaveImpl(Stream outputStream);

		/// Get the package part mapped to the specified URI.
		///
		/// @param PartName
		///            The URI of the part to retrieve.
		/// @return The package part located by the specified URI, else <b>null</b>.
		protected abstract PackagePart GetPartImpl(PackagePartName PartName);

		/// Get all parts link to the package.
		///
		/// @return A list of the part owned by the package.
		protected abstract PackagePart[] GetPartsImpl();

		/// Replace a content type in this package.
		///
		/// <p>
		///     A typical scneario to call this method is to rename a template file to the main format, e.g.
		///     ".dotx" to ".docx"
		///     ".dotm" to ".docm"
		///     ".xltx" to ".xlsx"
		///     ".xltm" to ".xlsm"
		///     ".potx" to ".pptx"
		///     ".potm" to ".pptm"
		/// </p>
		/// For example, a code converting  a .xlsm macro workbook to .xlsx would look as follows:
		/// <p>
		///    <pre><code>
		///
		///     OPCPackage pkg = OPCPackage.open(new FileInputStream("macro-workbook.xlsm"));
		///     pkg.replaceContentType(
		///         "application/vnd.ms-excel.sheet.macroEnabled.main+xml",
		///         "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml");
		///
		///     FileOutputStream out = new FileOutputStream("workbook.xlsx");
		///     pkg.save(out);
		///     out.close();
		///
		///    </code></pre>
		/// </p>
		///
		/// @param oldContentType  the content type to be replaced
		/// @param newContentType  the replacement
		/// @return whether replacement was succesfull
		/// @since POI-3.8
		public bool ReplaceContentType(string oldContentType, string newContentType)
		{
			bool result = false;
			List<PackagePart> partsByContentType = GetPartsByContentType(oldContentType);
			foreach (PackagePart item in partsByContentType)
			{
				if (item.ContentType.Equals(oldContentType))
				{
					PackagePartName partName = item.PartName;
					contentTypeManager.AddContentType(partName, newContentType);
					result = true;
				}
			}
			return result;
		}
	}
}
