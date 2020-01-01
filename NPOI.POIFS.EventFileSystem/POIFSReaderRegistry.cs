using NPOI.POIFS.FileSystem;
using System.Collections;

namespace NPOI.POIFS.EventFileSystem
{
	/// A registry for POIFSReaderListeners and the DocumentDescriptors of
	/// the documents those listeners are interested in
	///
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @version %I%, %G%
	public class POIFSReaderRegistry
	{
		private ArrayList omnivorousListeners;

		private Hashtable selectiveListeners;

		private Hashtable chosenDocumentDescriptors;

		/// Construct the registry
		public POIFSReaderRegistry()
		{
			omnivorousListeners = new ArrayList();
			selectiveListeners = new Hashtable();
			chosenDocumentDescriptors = new Hashtable();
		}

		/// Register a POIFSReaderListener for a particular document
		///
		/// @param listener the listener
		/// @param path the path of the document of interest
		/// @param documentName the name of the document of interest
		public void RegisterListener(POIFSReaderListener listener, POIFSDocumentPath path, string documentName)
		{
			if (!omnivorousListeners.Contains(listener))
			{
				ArrayList arrayList = (ArrayList)selectiveListeners[listener];
				if (arrayList == null)
				{
					arrayList = new ArrayList();
					selectiveListeners[listener] = arrayList;
				}
				DocumentDescriptor documentDescriptor = new DocumentDescriptor(path, documentName);
				if (arrayList.Add(documentDescriptor) >= 0)
				{
					ArrayList arrayList2 = (ArrayList)chosenDocumentDescriptors[documentDescriptor];
					if (arrayList2 == null)
					{
						arrayList2 = new ArrayList();
						chosenDocumentDescriptors[documentDescriptor] = arrayList2;
					}
					arrayList2.Add(listener);
				}
			}
		}

		/// Register for all documents
		///
		/// @param listener the listener who wants to Get all documents
		public void RegisterListener(POIFSReaderListener listener)
		{
			if (!omnivorousListeners.Contains(listener))
			{
				RemoveSelectiveListener(listener);
				omnivorousListeners.Add(listener);
			}
		}

		/// Get am iterator of listeners for a particular document
		///
		/// @param path the document path
		/// @param name the name of the document
		///
		/// @return an Iterator POIFSReaderListeners; may be empty
		public IEnumerator GetListeners(POIFSDocumentPath path, string name)
		{
			ArrayList arrayList = new ArrayList(omnivorousListeners);
			ArrayList arrayList2 = (ArrayList)chosenDocumentDescriptors[new DocumentDescriptor(path, name)];
			if (arrayList2 != null)
			{
				arrayList.AddRange(arrayList2);
			}
			return arrayList.GetEnumerator();
		}

		private void RemoveSelectiveListener(POIFSReaderListener listener)
		{
			ArrayList arrayList = (ArrayList)selectiveListeners[listener];
			if (arrayList != null)
			{
				selectiveListeners.Remove(listener);
				IEnumerator enumerator = arrayList.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DropDocument(listener, (DocumentDescriptor)enumerator.Current);
				}
			}
		}

		private void DropDocument(POIFSReaderListener listener, DocumentDescriptor descriptor)
		{
			ArrayList arrayList = (ArrayList)chosenDocumentDescriptors[descriptor];
			arrayList.Remove(listener);
			if (arrayList.Count == 0)
			{
				chosenDocumentDescriptors.Remove(descriptor);
			}
		}
	}
}
