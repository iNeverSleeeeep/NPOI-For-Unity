using NPOI.OpenXml4Net.Exceptions;
using NPOI.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.XPath;

namespace NPOI.OpenXml4Net.OPC
{
	/// Represents a collection of PackageRelationship elements that are owned by a
	/// given PackagePart or the Package.
	///
	/// @author Julien Chable, CDubettier
	/// @version 0.1
	public class PackageRelationshipCollection : IEnumerator<PackageRelationship>, IDisposable, IEnumerator
	{
		private class DuplicateComparer : IComparer<string>
		{
			public int Compare(string x, string y)
			{
				if (x.CompareTo(y) < 0)
				{
					return -1;
				}
				return 1;
			}
		}

		private static POILogger logger = POILogFactory.GetLogger(typeof(PackageRelationshipCollection));

		/// Package relationships ordered by ID.
		private SortedList<string, PackageRelationship> relationshipsByID;

		/// Package relationships ordered by type.
		private SortedList<string, PackageRelationship> relationshipsByType;

		/// This relationshipPart.
		private PackagePart relationshipPart;

		/// Source part.
		private PackagePart sourcePart;

		/// This part name.
		private PackagePartName partName;

		/// Reference to the package.
		private OPCPackage container;

		/// Get the numbe rof relationships in the collection.
		public int Size
		{
			get
			{
				return relationshipsByID.Values.Count;
			}
		}

		PackageRelationship IEnumerator<PackageRelationship>.Current
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		object IEnumerator.Current
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// Constructor.
		public PackageRelationshipCollection()
		{
			relationshipsByID = new SortedList<string, PackageRelationship>();
			relationshipsByType = new SortedList<string, PackageRelationship>(new DuplicateComparer());
		}

		/// Copy constructor.
		///
		/// This collection will contain only elements from the specified collection
		/// for which the type is compatible with the specified relationship type
		/// filter.
		///
		/// @param coll
		///            Collection to import.
		/// @param filter
		///            Relationship type filter.
		public PackageRelationshipCollection(PackageRelationshipCollection coll, string filter)
			: this()
		{
			foreach (PackageRelationship value in coll.relationshipsByID.Values)
			{
				if (filter == null || value.RelationshipType.Equals(filter))
				{
					AddRelationship(value);
				}
			}
		}

		/// Constructor.
		public PackageRelationshipCollection(OPCPackage container)
			: this(container, null)
		{
		}

		/// Constructor.
		///
		/// @throws InvalidFormatException
		///             Throws if the format of the content part is invalid.
		///
		/// @throws InvalidOperationException
		///             Throws if the specified part is a relationship part.
		public PackageRelationshipCollection(PackagePart part)
			: this(part.container, part)
		{
		}

		/// Constructor. Parse the existing package relationship part if one exists.
		///
		/// @param container
		///            The parent package.
		/// @param part
		///            The part that own this relationships collection. If <b>null</b>
		///            then this part is considered as the package root.
		/// @throws InvalidFormatException
		///             If an error occurs during the parsing of the relatinships
		///             part fo the specified part.
		public PackageRelationshipCollection(OPCPackage container, PackagePart part)
			: this()
		{
			if (container == null)
			{
				throw new ArgumentException("container");
			}
			if (part != null && part.IsRelationshipPart)
			{
				throw new ArgumentException("part");
			}
			this.container = container;
			sourcePart = part;
			partName = GetRelationshipPartName(part);
			if (container.GetPackageAccess() != PackageAccess.WRITE && container.ContainPart(partName))
			{
				relationshipPart = container.GetPart(partName);
				ParseRelationshipsPart(relationshipPart);
			}
		}

		/// Get the relationship part name of the specified part.
		///
		/// @param part
		///            The part .
		/// @return The relationship part name of the specified part. Be careful,
		///         only the correct name is returned, this method does not check if
		///         the part really exist in a package !
		/// @throws InvalidOperationException
		///             Throws if the specified part is a relationship part.
		private static PackagePartName GetRelationshipPartName(PackagePart part)
		{
			PackagePartName packagePartName = (part != null) ? part.PartName : PackagingUriHelper.PACKAGE_ROOT_PART_NAME;
			return PackagingUriHelper.GetRelationshipPartName(packagePartName);
		}

		/// Add the specified relationship to the collection.
		///
		/// @param relPart
		///            The relationship to add.
		public void AddRelationship(PackageRelationship relPart)
		{
			relationshipsByID.Add(relPart.Id, relPart);
			relationshipsByType.Add(relPart.RelationshipType, relPart);
		}

		/// Add a relationship to the collection.
		///
		/// @param targetUri
		///            Target URI.
		/// @param targetMode
		///            The target mode : INTERNAL or EXTERNAL
		/// @param relationshipType
		///            Relationship type.
		/// @param id
		///            Relationship ID.
		/// @return The newly created relationship.
		/// @see PackageAccess
		public PackageRelationship AddRelationship(Uri targetUri, TargetMode targetMode, string relationshipType, string id)
		{
			if (id == null)
			{
				int num = 0;
				do
				{
					id = "rId" + ++num;
				}
				while (relationshipsByID.ContainsKey(id));
			}
			PackageRelationship packageRelationship = new PackageRelationship(container, sourcePart, targetUri, targetMode, relationshipType, id);
			relationshipsByID.Add(packageRelationship.Id, packageRelationship);
			relationshipsByType.Add(packageRelationship.RelationshipType, packageRelationship);
			return packageRelationship;
		}

		/// Remove a relationship by its ID.
		///
		/// @param id
		///            The relationship ID to Remove.
		public void RemoveRelationship(string id)
		{
			if (relationshipsByID != null && relationshipsByType != null)
			{
				PackageRelationship packageRelationship = relationshipsByID[id];
				if (packageRelationship != null)
				{
					relationshipsByID.Remove(packageRelationship.Id);
					for (int i = 0; i < relationshipsByType.Count; i++)
					{
						if (relationshipsByType.Values[i] == packageRelationship)
						{
							relationshipsByType.RemoveAt(i);
						}
					}
				}
			}
		}

		/// Remove a relationship by its reference.
		///
		/// @param rel
		///            The relationship to delete.
		public void RemoveRelationship(PackageRelationship rel)
		{
			if (rel == null)
			{
				throw new ArgumentException("rel");
			}
			relationshipsByID.Values.Remove(rel);
			relationshipsByType.Values.Remove(rel);
		}

		/// Retrieves a relationship by its index in the collection.
		///
		/// @param index
		///            Must be a value between [0-relationships_count-1]
		public PackageRelationship GetRelationship(int index)
		{
			if (index < 0 || index > relationshipsByID.Values.Count)
			{
				throw new ArgumentException("index");
			}
			PackageRelationship result = null;
			int num = 0;
			foreach (PackageRelationship value in relationshipsByID.Values)
			{
				if (index == num++)
				{
					return value;
				}
			}
			return result;
		}

		/// Retrieves a package relationship based on its id.
		///
		/// @param id
		///            ID of the package relationship to retrieve.
		/// @return The package relationship identified by the specified id.
		public PackageRelationship GetRelationshipByID(string id)
		{
			if (!relationshipsByID.ContainsKey(id))
			{
				return null;
			}
			return relationshipsByID[id];
		}

		/// Parse the relationship part and add all relationship in this collection.
		///
		/// @param relPart
		///            The package part to parse.
		/// @throws InvalidFormatException
		///             Throws if the relationship part is invalid.
		private void ParseRelationshipsPart(PackagePart relPart)
		{
			try
			{
				logger.Log(1, "Parsing relationship: " + relPart.PartName);
				XPathDocument xPathDocument = new XPathDocument(relPart.GetInputStream());
				bool flag = false;
				XPathNavigator xPathNavigator = xPathDocument.CreateNavigator();
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xPathNavigator.NameTable);
				xmlNamespaceManager.AddNamespace("x", "http://schemas.openxmlformats.org/package/2006/relationships");
				XPathNodeIterator xPathNodeIterator = xPathNavigator.Select("//x:" + PackageRelationship.RELATIONSHIP_TAG_NAME, xmlNamespaceManager);
				while (true)
				{
					if (!xPathNodeIterator.MoveNext())
					{
						return;
					}
					string attribute = xPathNodeIterator.Current.GetAttribute(PackageRelationship.ID_ATTRIBUTE_NAME, xPathNavigator.NamespaceURI);
					string attribute2 = xPathNodeIterator.Current.GetAttribute(PackageRelationship.TYPE_ATTRIBUTE_NAME, xPathNavigator.NamespaceURI);
					if (attribute2.Equals("http://schemas.openxmlformats.org/package/2006/relationships/metadata/core-properties"))
					{
						if (flag)
						{
							break;
						}
						flag = true;
					}
					string attribute3 = xPathNodeIterator.Current.GetAttribute(PackageRelationship.TARGET_MODE_ATTRIBUTE_NAME, xPathNavigator.NamespaceURI);
					TargetMode targetMode = TargetMode.Internal;
					if (attribute3 != string.Empty)
					{
						targetMode = ((!attribute3.ToLower().Equals("internal")) ? TargetMode.External : TargetMode.Internal);
					}
					string text = string.Empty;
					Uri targetUri;
					try
					{
						text = xPathNodeIterator.Current.GetAttribute(PackageRelationship.TARGET_ATTRIBUTE_NAME, xPathNavigator.NamespaceURI);
						targetUri = PackagingUriHelper.ToUri(text);
					}
					catch (UriFormatException exception)
					{
						logger.Log(7, "Cannot convert " + text + " in a valid relationship URI-> ignored", exception);
						continue;
					}
					AddRelationship(targetUri, targetMode, attribute2, attribute);
				}
				throw new InvalidFormatException("OPC Compliance error [M4.1]: there is more than one core properties relationship in the package !");
			}
			catch (Exception ex)
			{
				logger.Log(7, ex);
				throw new InvalidFormatException(ex.Message);
			}
		}

		/// Retrieves all relations with the specified type.
		///
		/// @param typeFilter
		///            Relationship type filter. If <b>null</b> then all
		///            relationships are returned.
		/// @return All relationships of the type specified by the filter.
		public PackageRelationshipCollection GetRelationships(string typeFilter)
		{
			return new PackageRelationshipCollection(this, typeFilter);
		}

		/// Get this collection's iterator.
		public IEnumerator<PackageRelationship> GetEnumerator()
		{
			return relationshipsByID.Values.GetEnumerator();
		}

		/// Get an iterator of a collection with all relationship with the specified
		/// type.
		///
		/// @param typeFilter
		///            Type filter.
		/// @return An iterator to a collection containing all relationships with the
		///         specified type contain in this collection.
		public IEnumerator<PackageRelationship> Iterator(string typeFilter)
		{
			List<PackageRelationship> list = new List<PackageRelationship>();
			foreach (PackageRelationship value in relationshipsByID.Values)
			{
				if (value.RelationshipType.Equals(typeFilter))
				{
					list.Add(value);
				}
			}
			return list.GetEnumerator();
		}

		/// Clear all relationships.
		public void Clear()
		{
			relationshipsByID.Clear();
			relationshipsByType.Clear();
		}

		public override string ToString()
		{
			string text = (relationshipsByID != null) ? (relationshipsByID.Count + " relationship(s) = [") : "relationshipsByID=null";
			text = ((relationshipPart == null || relationshipPart.PartName == null) ? (text + ",relationshipPart=null") : (text + "," + relationshipPart.PartName));
			text = ((sourcePart == null || sourcePart.PartName == null) ? (text + ",sourcePart=null") : (text + "," + sourcePart.PartName));
			text = ((partName == null) ? (text + ",uri=null)") : (text + "," + partName));
			return text + "]";
		}

		void IDisposable.Dispose()
		{
		}

		bool IEnumerator.MoveNext()
		{
			throw new NotImplementedException();
		}

		void IEnumerator.Reset()
		{
			Clear();
		}
	}
}
