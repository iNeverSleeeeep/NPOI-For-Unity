using NPOI.POIFS.Dev;
using NPOI.POIFS.Properties;
using System;
using System.Collections;

namespace NPOI.POIFS.FileSystem
{
	/// <summary>
	/// Simple implementation of DocumentEntry
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// </summary>
	public class DocumentNode : EntryNode, POIFSViewable, DocumentEntry, Entry
	{
		private POIFSDocument _document;

		/// get the POIFSDocument
		///
		/// @return the internal POIFSDocument
		public POIFSDocument Document => _document;

		/// get the zize of the document, in bytes
		///
		/// @return size in bytes
		public int Size => base.Property.Size;

		/// Is this a DocumentEntry?
		///
		/// @return true if the Entry Is a DocumentEntry, else false
		public override bool IsDocumentEntry => true;

		/// extensions use this method to verify internal rules regarding
		/// deletion of the underlying store.
		///
		/// @return true if it's ok to delete the underlying store, else
		///         false
		protected override bool IsDeleteOK => true;

		/// Get an array of objects, some of which may implement
		/// POIFSViewable
		///
		/// @return an array of Object; may not be null, but may be empty
		public Array ViewableArray => new object[0];

		/// Get an Iterator of objects, some of which may implement
		/// POIFSViewable
		///
		/// @return an Iterator; may not be null, but may have an empty
		/// back end store
		public IEnumerator ViewableIterator
		{
			get
			{
				IList list = new ArrayList();
				list.Add(base.Property);
				list.Add(_document);
				return list.GetEnumerator();
			}
		}

		/// Give viewers a hint as to whether to call getViewableArray or
		/// getViewableIterator
		///
		/// @return true if a viewer should call getViewableArray, false if
		///         a viewer should call getViewableIterator
		public bool PreferArray => false;

		/// Provides a short description of the object, to be used when a
		/// POIFSViewable object has not provided its contents.
		///
		/// @return short description
		public string ShortDescription => base.Name;

		/// create a DocumentNode. This method Is not public by design; it
		/// Is intended strictly for the internal use of this package
		///
		/// @param property the DocumentProperty for this DocumentEntry
		/// @param parent the parent of this entry
		public DocumentNode(DocumentProperty property, DirectoryNode parent)
			: base(property, parent)
		{
			_document = property.Document;
		}
	}
}
