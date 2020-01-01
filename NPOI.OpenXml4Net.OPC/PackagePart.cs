using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXml4Net.OPC.Internal;
using System;
using System.IO;

namespace NPOI.OpenXml4Net.OPC
{
	/// Provides a base class for parts stored in a Package.
	///
	/// @author Julien Chable
	/// @version 0.9
	public abstract class PackagePart : RelationshipSource
	{
		/// This part's container.
		internal OPCPackage container;

		/// The part name. (required by the specification [M1.1])
		protected PackagePartName partName;

		/// The type of content of this part. (required by the specification [M1.2])
		internal ContentType contentType;

		/// Flag to know if this part is a relationship.
		private bool isRelationshipPart;

		/// Flag to know if this part has been logically deleted.
		private bool isDeleted;

		/// This part's relationships.
		private PackageRelationshipCollection relationships;

		/// Retrieve all the relationships attached to this part.
		///
		/// @return This part's relationships.
		/// @throws OpenXml4NetException
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#getRelationships()
		public PackageRelationshipCollection Relationships
		{
			get
			{
				return GetRelationshipsCore(null);
			}
		}

		/// Knows if the part have any relationships.
		///
		/// @return <b>true</b> if the part have at least one relationship else
		///         <b>false</b>.
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#hasRelationships()
		public bool HasRelationships
		{
			get
			{
				if (!IsRelationshipPart)
				{
					if (relationships != null)
					{
						return relationships.Size > 0;
					}
					return false;
				}
				return false;
			}
		}

		/// @return the uri
		public PackagePartName PartName
		{
			get
			{
				return partName;
			}
		}

		/// @return the contentType
		public string ContentType
		{
			get
			{
				return contentType.ToString();
			}
			set
			{
				if (container == null)
				{
					contentType = new ContentType(value);
					return;
				}
				throw new InvalidOperationException("You can't change the content type of a part.");
			}
		}

		public OPCPackage Package
		{
			get
			{
				return container;
			}
		}

		/// @return true if this part is a relationship
		public bool IsRelationshipPart
		{
			get
			{
				return isRelationshipPart;
			}
		}

		/// @return true if this part has been logically deleted
		public bool IsDeleted
		{
			get
			{
				return isDeleted;
			}
			set
			{
				isDeleted = value;
			}
		}

		/// Constructor.
		///
		/// @param pack
		///            Parent package.
		/// @param partName
		///            The part name, relative to the parent Package root.
		/// @param contentType
		///            The content type.
		/// @throws InvalidFormatException
		///             If the specified URI is not valid.
		protected PackagePart(OPCPackage pack, PackagePartName partName, ContentType contentType)
			: this(pack, partName, contentType, true)
		{
		}

		/// Constructor.
		///
		/// @param pack
		///            Parent package.
		/// @param partName
		///            The part name, relative to the parent Package root.
		/// @param contentType
		///            The content type.
		/// @param loadRelationships
		///            Specify if the relationships will be loaded
		/// @throws InvalidFormatException
		///             If the specified URI is not valid.
		protected PackagePart(OPCPackage pack, PackagePartName partName, ContentType contentType, bool loadRelationships)
		{
			this.partName = partName;
			this.contentType = contentType;
			container = (ZipPackage)pack;
			isRelationshipPart = this.partName.IsRelationshipPartURI();
			if (loadRelationships)
			{
				LoadRelationships();
			}
		}

		/// Constructor.
		///
		/// @param pack
		///            Parent package.
		/// @param partName
		///            The part name, relative to the parent Package root.
		/// @param contentType
		///            The Multipurpose Internet Mail Extensions (MIME) content type
		///            of the part's data stream.
		public PackagePart(OPCPackage pack, PackagePartName partName, string contentType)
			: this(pack, partName, new ContentType(contentType))
		{
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
		/// @return The newly created and added relationship
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#addExternalRelationship(java.lang.String,
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
		/// @return The newly created and added relationship
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#addExternalRelationship(java.lang.String,
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
			if (relationships == null)
			{
				relationships = new PackageRelationshipCollection();
			}
			Uri targetUri;
			try
			{
				targetUri = PackagingUriHelper.ParseUri(target, UriKind.RelativeOrAbsolute);
			}
			catch (UriFormatException arg)
			{
				throw new ArgumentException("Invalid target - " + arg);
			}
			return relationships.AddRelationship(targetUri, TargetMode.External, relationshipType, id);
		}

		/// Add a relationship to a part (except relationships part).
		///
		/// @param targetPartName
		///            Name of the target part. This one must be relative to the
		///            source root directory of the part.
		/// @param targetMode
		///            Mode [Internal|External].
		/// @param relationshipType
		///            Type of relationship.
		/// @return The newly created and added relationship
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#AddRelationship(org.apache.poi.OpenXml4Net.opc.PackagePartName,
		///      org.apache.poi.OpenXml4Net.opc.TargetMode, java.lang.String)
		public PackageRelationship AddRelationship(PackagePartName targetPartName, TargetMode targetMode, string relationshipType)
		{
			return AddRelationship(targetPartName, targetMode, relationshipType, null);
		}

		/// Add a relationship to a part (except relationships part).
		/// <p>
		/// Check rule M1.25: The Relationships part shall not have relationships to
		/// any other part. Package implementers shall enforce this requirement upon
		/// the attempt to create such a relationship and shall treat any such
		/// relationship as invalid.
		/// </p>
		/// @param targetPartName
		///            Name of the target part. This one must be relative to the
		///            source root directory of the part.
		/// @param targetMode
		///            Mode [Internal|External].
		/// @param relationshipType
		///            Type of relationship.
		/// @param id
		///            Relationship unique id.
		/// @return The newly created and added relationship
		///
		/// @throws InvalidFormatException
		///             If the URI point to a relationship part URI.
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#AddRelationship(org.apache.poi.OpenXml4Net.opc.PackagePartName,
		///      org.apache.poi.OpenXml4Net.opc.TargetMode, java.lang.String, java.lang.String)
		public PackageRelationship AddRelationship(PackagePartName targetPartName, TargetMode targetMode, string relationshipType, string id)
		{
			container.ThrowExceptionIfReadOnly();
			if (targetPartName == null)
			{
				throw new ArgumentException("targetPartName");
			}
			if (relationshipType == null)
			{
				throw new ArgumentException("relationshipType");
			}
			if (IsRelationshipPart || targetPartName.IsRelationshipPartURI())
			{
				throw new InvalidOperationException("Rule M1.25: The Relationships part shall not have relationships to any other part.");
			}
			if (relationships == null)
			{
				relationships = new PackageRelationshipCollection();
			}
			return relationships.AddRelationship(targetPartName.URI, targetMode, relationshipType, id);
		}

		/// Add a relationship to a part (except relationships part).
		///
		/// @param targetURI
		///            URI the target part. Must be relative to the source root
		///            directory of the part.
		/// @param targetMode
		///            Mode [Internal|External].
		/// @param relationshipType
		///            Type of relationship.
		/// @return The newly created and added relationship
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#AddRelationship(org.apache.poi.OpenXml4Net.opc.PackagePartName,
		///      org.apache.poi.OpenXml4Net.opc.TargetMode, java.lang.String)
		public PackageRelationship AddRelationship(Uri targetURI, TargetMode targetMode, string relationshipType)
		{
			return AddRelationship(targetURI, targetMode, relationshipType, null);
		}

		/// Add a relationship to a part (except relationships part).
		/// <p>
		/// Check rule M1.25: The Relationships part shall not have relationships to
		/// any other part. Package implementers shall enforce this requirement upon
		/// the attempt to create such a relationship and shall treat any such
		/// relationship as invalid.
		/// </p>
		/// @param targetURI
		///            URI of the target part. Must be relative to the source root
		///            directory of the part.
		/// @param targetMode
		///            Mode [Internal|External].
		/// @param relationshipType
		///            Type of relationship.
		/// @param id
		///            Relationship unique id.
		/// @return The newly created and added relationship
		///
		/// @throws InvalidFormatException
		///             If the URI point to a relationship part URI.
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#AddRelationship(org.apache.poi.OpenXml4Net.opc.PackagePartName,
		///      org.apache.poi.OpenXml4Net.opc.TargetMode, java.lang.String, java.lang.String)
		public PackageRelationship AddRelationship(Uri targetURI, TargetMode targetMode, string relationshipType, string id)
		{
			container.ThrowExceptionIfReadOnly();
			if (targetURI == null)
			{
				throw new ArgumentException("targetPartName");
			}
			if (relationshipType == null)
			{
				throw new ArgumentException("relationshipType");
			}
			if (IsRelationshipPart || PackagingUriHelper.IsRelationshipPartURI(targetURI))
			{
				throw new InvalidOperationException("Rule M1.25: The Relationships part shall not have relationships to any other part.");
			}
			if (relationships == null)
			{
				relationships = new PackageRelationshipCollection();
			}
			return relationships.AddRelationship(targetURI, targetMode, relationshipType, id);
		}

		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#clearRelationships()
		public void ClearRelationships()
		{
			if (relationships != null)
			{
				relationships.Clear();
			}
		}

		/// Delete the relationship specified by its id.
		///
		/// @param id
		///            The ID identified the part to delete.
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#removeRelationship(java.lang.String)
		public void RemoveRelationship(string id)
		{
			container.ThrowExceptionIfReadOnly();
			if (relationships != null)
			{
				relationships.RemoveRelationship(id);
			}
		}

		/// Retrieves a package relationship from its id.
		///
		/// @param id
		///            ID of the package relationship to retrieve.
		/// @return The package relationship
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#getRelationship(java.lang.String)
		public PackageRelationship GetRelationship(string id)
		{
			return relationships.GetRelationshipByID(id);
		}

		/// Retrieve all relationships attached to this part which have the specified
		/// type.
		///
		/// @param relationshipType
		///            Relationship type filter.
		/// @return All relationships from this part that have the specified type.
		/// @throws InvalidFormatException
		///             If an error occurs while parsing the part.
		/// @throws InvalidOperationException
		///             If the package is open in write only mode.
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#getRelationshipsByType(java.lang.String)
		public PackageRelationshipCollection GetRelationshipsByType(string relationshipType)
		{
			container.ThrowExceptionIfWriteOnly();
			return GetRelationshipsCore(relationshipType);
		}

		/// Implementation of the getRelationships method().
		///
		/// @param filter
		///            Relationship type filter. If <i>null</i> then the filter is
		///            disabled and return all the relationships.
		/// @return All relationships from this part that have the specified type.
		/// @throws InvalidFormatException
		///             Throws if an error occurs during parsing the relationships
		///             part.
		/// @throws InvalidOperationException
		///             Throws if the package is open en write only mode.
		/// @see #getRelationshipsByType(String)
		private PackageRelationshipCollection GetRelationshipsCore(string filter)
		{
			container.ThrowExceptionIfWriteOnly();
			if (relationships == null)
			{
				ThrowExceptionIfRelationship();
				relationships = new PackageRelationshipCollection(this);
			}
			return new PackageRelationshipCollection(relationships, filter);
		}

		/// Checks if the specified relationship is part of this package part.
		///
		/// @param rel
		///            The relationship to check.
		/// @return <b>true</b> if the specified relationship exists in this part,
		///         else returns <b>false</b>
		/// @see org.apache.poi.OpenXml4Net.opc.RelationshipSource#isRelationshipExists(org.apache.poi.OpenXml4Net.opc.PackageRelationship)
		public bool IsRelationshipExists(PackageRelationship rel)
		{
			try
			{
				foreach (PackageRelationship relationship in Relationships)
				{
					if (relationship == rel)
					{
						return true;
					}
				}
			}
			catch (InvalidFormatException)
			{
			}
			return false;
		}

		/// Get the PackagePart that is the target of a relationship.
		///
		/// @param rel A relationship from this part to another one 
		/// @return The target part of the relationship
		public PackagePart GetRelatedPart(PackageRelationship rel)
		{
			if (!IsRelationshipExists(rel))
			{
				throw new ArgumentException("Relationship " + rel + " doesn't start with this part " + partName);
			}
			Uri uri = rel.TargetUri;
			if (uri.OriginalString.IndexOf('#') >= 0)
			{
				string text = uri.ToString();
				try
				{
					uri = PackagingUriHelper.ParseUri(text.Substring(0, text.IndexOf('#')));
				}
				catch (UriFormatException)
				{
					throw new InvalidFormatException("Invalid target URI: " + text);
				}
			}
			PackagePartName packagePartName = PackagingUriHelper.CreatePartName(uri);
			PackagePart part = container.GetPart(packagePartName);
			if (part == null)
			{
				throw new ArgumentException("No part found for relationship " + rel);
			}
			return part;
		}

		public Stream GetStream(FileMode mode)
		{
			return GetStream(mode, FileAccess.Write);
		}

		public Stream GetStream(FileMode mode, FileAccess access)
		{
			if (mode == FileMode.Create && access == FileAccess.Write)
			{
				return GetOutputStream();
			}
			return GetInputStream();
		}

		/// Get the input stream of this part to read its content.
		///
		/// @return The input stream of the content of this part, else
		///         <code>null</code>.
		public Stream GetInputStream()
		{
			Stream inputStreamImpl = GetInputStreamImpl();
			if (inputStreamImpl == null)
			{
				throw new IOException("Can't obtain the input stream from " + partName.Name);
			}
			return inputStreamImpl;
		}

		/// Get the output stream of this part. If the part is originally embedded in
		/// Zip package, it'll be transform intot a <i>MemoryPackagePart</i> in
		/// order to write inside (the standard Java API doesn't allow to write in
		/// the file)
		///
		/// @see org.apache.poi.openxml4j.opc.internal.MemoryPackagePart
		public Stream GetOutputStream()
		{
			if (this is ZipPackagePart)
			{
				container.RemovePart(partName);
				PackagePart packagePart = container.CreatePart(partName, contentType.ToString(), false);
				packagePart.relationships = relationships;
				if (packagePart == null)
				{
					throw new InvalidOperationException("Can't create a temporary part !");
				}
				return packagePart.GetOutputStreamImpl();
			}
			return GetOutputStreamImpl();
		}

		/// Throws an exception if this package part is a relationship part.
		///
		/// @throws InvalidOperationException
		///             If this part is a relationship part.
		private void ThrowExceptionIfRelationship()
		{
			if (IsRelationshipPart)
			{
				throw new InvalidOperationException("Can do this operation on a relationship part !");
			}
		}

		/// Ensure the package relationships collection instance is built.
		///
		/// @throws InvalidFormatException
		///             Throws if
		private void LoadRelationships()
		{
			if (relationships == null && !IsRelationshipPart)
			{
				ThrowExceptionIfRelationship();
				relationships = new PackageRelationshipCollection(this);
			}
		}

		public override string ToString()
		{
			return "Name: " + partName + " - Content Type: " + contentType.ToString();
		}

		/// Abtract method that get the input stream of this part.
		///
		/// @exception IOException
		///                Throws if an IO Exception occur in the implementation
		///                method.
		protected abstract Stream GetInputStreamImpl();

		/// Abstract method that get the output stream of this part.
		protected abstract Stream GetOutputStreamImpl();

		/// Save the content of this part and the associated relationships part (if
		/// this part own at least one relationship) into the specified output
		/// stream.
		///
		/// @param zos
		///            Output stream to save this part.
		/// @throws OpenXml4NetException
		///             If any exception occur.
		public abstract bool Save(Stream zos);

		/// Load the content of this part.
		///
		/// @param ios
		///            The input stream of the content to load.
		/// @return <b>true</b> if the content has been successfully loaded, else
		///         <b>false</b>.
		/// @throws InvalidFormatException
		///             Throws if the content format is invalid.
		public abstract bool Load(Stream ios);

		/// Close this part : flush this part, close the input stream and output
		/// stream. After this method call, the part must be available for packaging.
		public abstract void Close();

		/// Flush the content of this part. If the input stream and/or output stream
		/// as in a waiting state to read or write, the must to empty their
		/// respective buffer.
		public abstract void Flush();
	}
}
