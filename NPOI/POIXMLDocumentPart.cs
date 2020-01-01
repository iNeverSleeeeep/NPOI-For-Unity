using NPOI.OpenXml4Net.Exceptions;
using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXml4Net.OPC.Internal;
using NPOI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI
{
	/// Represents an entry of a OOXML namespace.
	///
	/// <p>
	/// Each POIXMLDocumentPart keeps a reference to the underlying a {@link org.apache.poi.openxml4j.opc.PackagePart}.
	/// </p>
	///
	/// @author Yegor Kozlov
	public class POIXMLDocumentPart
	{
		private static POILogger logger = POILogFactory.GetLogger(typeof(POIXMLDocumentPart));

		private PackagePart packagePart;

		private PackageRelationship packageRel;

		private POIXMLDocumentPart parent;

		private Dictionary<string, POIXMLDocumentPart> relations = new Dictionary<string, POIXMLDocumentPart>();

		/// Counter that provides the amount of incoming relations from other parts
		/// to this part.
		private int relationCounter;

		private static XmlNamespaceManager nsm = null;

		public static XmlNamespaceManager NamespaceManager
		{
			get
			{
				if (nsm == null)
				{
					nsm = CreateDefaultNSM();
				}
				return nsm;
			}
		}

		private int IncrementRelationCounter()
		{
			relationCounter++;
			return relationCounter;
		}

		private int DecrementRelationCounter()
		{
			relationCounter--;
			return relationCounter;
		}

		private int GetRelationCounter()
		{
			return relationCounter;
		}

		/// Construct POIXMLDocumentPart representing a "core document" namespace part.
		public POIXMLDocumentPart(OPCPackage pkg)
		{
			PackageRelationship relationship = pkg.GetRelationshipsByType("http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument").GetRelationship(0);
			packagePart = pkg.GetPart(relationship);
			packageRel = relationship;
		}

		/// Creates new POIXMLDocumentPart   - called by client code to create new parts from scratch.
		///
		/// @see #CreateRelationship(POIXMLRelation, POIXMLFactory, int, bool)
		public POIXMLDocumentPart()
		{
		}

		/// Creates an POIXMLDocumentPart representing the given namespace part and relationship.
		/// Called by {@link #read(POIXMLFactory, java.util.Map)} when Reading in an exisiting file.
		///
		/// @param part - The namespace part that holds xml data represenring this sheet.
		/// @param rel - the relationship of the given namespace part
		/// @see #read(POIXMLFactory, java.util.Map) 
		public POIXMLDocumentPart(PackagePart part, PackageRelationship rel)
		{
			packagePart = part;
			packageRel = rel;
		}

		/// Creates an POIXMLDocumentPart representing the given namespace part, relationship and parent
		/// Called by {@link #read(POIXMLFactory, java.util.Map)} when Reading in an exisiting file.
		///
		/// @param parent - Parent part
		/// @param part - The namespace part that holds xml data represenring this sheet.
		/// @param rel - the relationship of the given namespace part
		/// @see #read(POIXMLFactory, java.util.Map)
		public POIXMLDocumentPart(POIXMLDocumentPart parent, PackagePart part, PackageRelationship rel)
		{
			packagePart = part;
			packageRel = rel;
			this.parent = parent;
		}

		/// When you open something like a theme, call this to
		///  re-base the XML Document onto the core child of the
		///  current core document 
		protected void Rebase(OPCPackage pkg)
		{
			PackageRelationshipCollection relationshipsByType = packagePart.GetRelationshipsByType("http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument");
			if (relationshipsByType.Size != 1)
			{
				throw new InvalidOperationException("Tried to rebase using http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument but found " + relationshipsByType.Size + " parts of the right type");
			}
			packageRel = relationshipsByType.GetRelationship(0);
			packagePart = packagePart.GetRelatedPart(packageRel);
		}

		internal static XmlNamespaceManager CreateDefaultNSM()
		{
			NameTable nameTable = new NameTable();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(nameTable);
			xmlNamespaceManager.AddNamespace(string.Empty, "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
			xmlNamespaceManager.AddNamespace("d", "http://schemas.openxmlformats.org/spreadsheetml/2006/main");
			xmlNamespaceManager.AddNamespace("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
			xmlNamespaceManager.AddNamespace("xdr", "http://schemas.openxmlformats.org/drawingml/2006/spreadsheetDrawing");
			xmlNamespaceManager.AddNamespace("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
			xmlNamespaceManager.AddNamespace("c", "http://schemas.openxmlformats.org/drawingml/2006/chart");
			xmlNamespaceManager.AddNamespace("vt", "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes");
			xmlNamespaceManager.AddNamespace("w", "http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			xmlNamespaceManager.AddNamespace("wp", "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing");
			xmlNamespaceManager.AddNamespace("m", "http://schemas.openxmlformats.org/officeDocument/2006/math");
			xmlNamespaceManager.AddNamespace("ve", "http://schemas.openxmlformats.org/markup-compatibility/2006");
			xmlNamespaceManager.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
			xmlNamespaceManager.AddNamespace("v", "urn:schemas-microsoft-com:vml");
			xmlNamespaceManager.AddNamespace("wne", "http://schemas.microsoft.com/office/word/2006/wordml");
			xmlNamespaceManager.AddNamespace("xp", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/extended-properties");
			xmlNamespaceManager.AddNamespace("ctp", "http://schemas.openxmlformats.org/officeDocument/2006/relationships/custom-properties");
			xmlNamespaceManager.AddNamespace("cp", PackagePropertiesPart.NAMESPACE_CP_URI);
			xmlNamespaceManager.AddNamespace("dc", PackagePropertiesPart.NAMESPACE_DC_URI);
			xmlNamespaceManager.AddNamespace("dcterms", PackagePropertiesPart.NAMESPACE_DCTERMS_URI);
			xmlNamespaceManager.AddNamespace("dcmitype", "http://purl.org/dc/dcmitype/");
			xmlNamespaceManager.AddNamespace("xsi", PackagePropertiesPart.NAMESPACE_XSI_URI);
			xmlNamespaceManager.AddNamespace("xsd", "http://www.w3.org/2001/XMLSchema");
			return xmlNamespaceManager;
		}

		/// Provides access to the underlying PackagePart
		///
		/// @return the underlying PackagePart
		public PackagePart GetPackagePart()
		{
			return packagePart;
		}

		public static XmlDocument ConvertStreamToXml(Stream xmlStream)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlStream);
			return xmlDocument;
		}

		/// Provides access to the PackageRelationship that identifies this POIXMLDocumentPart
		///
		/// @return the PackageRelationship that identifies this POIXMLDocumentPart
		public PackageRelationship GetPackageRelationship()
		{
			return packageRel;
		}

		/// Returns the list of child relations for this POIXMLDocumentPart
		///
		/// @return child relations
		public List<POIXMLDocumentPart> GetRelations()
		{
			return new List<POIXMLDocumentPart>(relations.Values);
		}

		/// Returns the target {@link POIXMLDocumentPart}, where a
		/// {@link PackageRelationship} is set from the {@link PackagePart} of this
		/// {@link POIXMLDocumentPart} to the {@link PackagePart} of the target
		/// {@link POIXMLDocumentPart} with a {@link PackageRelationship#GetId()}
		/// matching the given parameter value.
		///
		/// @param id
		///            The relation id to look for
		/// @return the target part of the relation, or null, if none exists
		public POIXMLDocumentPart GetRelationById(string id)
		{
			return relations[id];
		}

		/// Returns the {@link PackageRelationship#GetId()} of the
		/// {@link PackageRelationship}, that sources from the {@link PackagePart} of
		/// this {@link POIXMLDocumentPart} to the {@link PackagePart} of the given
		/// parameter value.
		///
		/// @param part
		///            The {@link POIXMLDocumentPart} for which the according
		///            relation-id shall be found.
		/// @return The value of the {@link PackageRelationship#GetId()} or null, if
		///         parts are not related.
		public string GetRelationId(POIXMLDocumentPart part)
		{
			foreach (KeyValuePair<string, POIXMLDocumentPart> relation in relations)
			{
				if (relation.Value == part)
				{
					return relation.Key;
				}
			}
			return null;
		}

		/// Add a new child POIXMLDocumentPart
		///
		/// @param part the child to add
		public void AddRelation(string id, POIXMLDocumentPart part)
		{
			relations[id] = part;
			part.IncrementRelationCounter();
		}

		/// Remove the relation to the specified part in this namespace and remove the
		/// part, if it is no longer needed.
		protected void RemoveRelation(POIXMLDocumentPart part)
		{
			RemoveRelation(part, true);
		}

		/// Remove the relation to the specified part in this namespace and remove the
		/// part, if it is no longer needed and flag is set to true.
		///
		/// @param part
		///            The related part, to which the relation shall be Removed.
		/// @param RemoveUnusedParts
		///            true, if the part shall be Removed from the namespace if not
		///            needed any longer.
		protected bool RemoveRelation(POIXMLDocumentPart part, bool RemoveUnusedParts)
		{
			string relationId = GetRelationId(part);
			if (relationId == null)
			{
				return false;
			}
			part.DecrementRelationCounter();
			GetPackagePart().RemoveRelationship(relationId);
			relations.Remove(relationId);
			if (RemoveUnusedParts && part.GetRelationCounter() == 0)
			{
				try
				{
					part.onDocumentRemove();
				}
				catch (IOException ex)
				{
					throw new POIXMLException(ex);
				}
				GetPackagePart().Package.RemovePart(part.GetPackagePart());
			}
			return true;
		}

		/// Returns the parent POIXMLDocumentPart. All parts except root have not-null parent.
		///
		/// @return the parent POIXMLDocumentPart or <code>null</code> for the root element.
		public POIXMLDocumentPart GetParent()
		{
			return parent;
		}

		public override string ToString()
		{
			if (packagePart != null)
			{
				return packagePart.ToString();
			}
			return null;
		}

		/// Save the content in the underlying namespace part.
		/// Default implementation is empty meaning that the namespace part is left unmodified.
		///
		/// Sub-classes should override and add logic to marshal the "model" into Ooxml4J.
		///
		/// For example, the code saving a generic XML entry may look as follows:
		/// <pre><code>
		/// protected void commit()  {
		///   PackagePart part = GetPackagePart();
		///   Stream out = part.GetStream();
		///   XmlObject bean = GetXmlBean(); //the "model" which holds Changes in memory
		///   bean.save(out, DEFAULT_XML_OPTIONS);
		///   out.close();
		/// }
		///  </code></pre>
		protected virtual void Commit()
		{
		}

		/// Save Changes in the underlying OOXML namespace.
		/// Recursively fires {@link #commit()} for each namespace part
		///
		/// @param alreadySaved    context set Containing already visited nodes
		protected void OnSave(List<PackagePart> alreadySaved)
		{
			Commit();
			alreadySaved.Add(GetPackagePart());
			foreach (POIXMLDocumentPart value in relations.Values)
			{
				if (!alreadySaved.Contains(value.GetPackagePart()))
				{
					value.OnSave(alreadySaved);
				}
			}
		}

		/// Create a new child POIXMLDocumentPart
		///
		/// @param descriptor the part descriptor
		/// @param factory the factory that will create an instance of the requested relation
		/// @return the Created child POIXMLDocumentPart
		public POIXMLDocumentPart CreateRelationship(POIXMLRelation descriptor, POIXMLFactory factory)
		{
			return CreateRelationship(descriptor, factory, -1, false);
		}

		public POIXMLDocumentPart CreateRelationship(POIXMLRelation descriptor, POIXMLFactory factory, int idx)
		{
			return CreateRelationship(descriptor, factory, idx, false);
		}

		/// Create a new child POIXMLDocumentPart
		///
		/// @param descriptor the part descriptor
		/// @param factory the factory that will create an instance of the requested relation
		/// @param idx part number
		/// @param noRelation if true, then no relationship is Added.
		/// @return the Created child POIXMLDocumentPart
		protected POIXMLDocumentPart CreateRelationship(POIXMLRelation descriptor, POIXMLFactory factory, int idx, bool noRelation)
		{
			try
			{
				PackagePartName packagePartName = PackagingUriHelper.CreatePartName(descriptor.GetFileName(idx));
				PackageRelationship packageRelationship = null;
				PackagePart packagePart = this.packagePart.Package.CreatePart(packagePartName, descriptor.ContentType);
				if (!noRelation)
				{
					packageRelationship = this.packagePart.AddRelationship(packagePartName, TargetMode.Internal, descriptor.Relation);
				}
				POIXMLDocumentPart pOIXMLDocumentPart = factory.CreateDocumentPart(descriptor);
				pOIXMLDocumentPart.packageRel = packageRelationship;
				pOIXMLDocumentPart.packagePart = packagePart;
				pOIXMLDocumentPart.parent = this;
				if (!noRelation)
				{
					AddRelation(packageRelationship.Id, pOIXMLDocumentPart);
				}
				return pOIXMLDocumentPart;
			}
			catch (PartAlreadyExistsException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new POIXMLException(ex2);
			}
		}

		/// Iterate through the underlying PackagePart and create child POIXMLFactory instances
		/// using the specified factory
		///
		/// @param factory   the factory object that Creates POIXMLFactory instances
		/// @param context   context map Containing already visited noted keyed by tarGetURI
		protected void Read(POIXMLFactory factory, Dictionary<PackagePart, POIXMLDocumentPart> context)
		{
			try
			{
				PackageRelationshipCollection relationships = this.packagePart.Relationships;
				foreach (PackageRelationship item in relationships)
				{
					TargetMode? targetMode = item.TargetMode;
					if (targetMode.GetValueOrDefault() == TargetMode.Internal && targetMode.HasValue)
					{
						Uri targetUri = item.TargetUri;
						PackagePart packagePart;
						if (targetUri.OriginalString.IndexOf('#') >= 0)
						{
							packagePart = null;
						}
						else
						{
							PackagePartName partName = PackagingUriHelper.CreatePartName(targetUri);
							packagePart = this.packagePart.Package.GetPart(partName);
							if (packagePart == null)
							{
								logger.Log(7, "Skipped invalid entry " + item.TargetUri);
								continue;
							}
						}
						if (packagePart == null || !context.ContainsKey(packagePart))
						{
							POIXMLDocumentPart pOIXMLDocumentPart = factory.CreateDocumentPart(this, item, packagePart);
							pOIXMLDocumentPart.parent = this;
							AddRelation(item.Id, pOIXMLDocumentPart);
							if (packagePart != null)
							{
								context[packagePart] = pOIXMLDocumentPart;
								if (packagePart.HasRelationships)
								{
									pOIXMLDocumentPart.Read(factory, context);
								}
							}
						}
						else
						{
							AddRelation(item.Id, context[packagePart]);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null && ex.InnerException.InnerException != null)
				{
					logger.Log(1, ex.InnerException.InnerException);
				}
				throw;
			}
		}

		/// Get the PackagePart that is the target of a relationship.
		///
		/// @param rel The relationship
		/// @return The target part
		/// @throws InvalidFormatException
		protected PackagePart GetTargetPart(PackageRelationship rel)
		{
			return GetPackagePart().GetRelatedPart(rel);
		}

		/// Fired when a new namespace part is Created
		internal virtual void OnDocumentCreate()
		{
		}

		/// Fired when a namespace part is read
		internal virtual void OnDocumentRead()
		{
		}

		/// Fired when a namespace part is about to be Removed from the namespace
		protected virtual void onDocumentRemove()
		{
		}
	}
}
