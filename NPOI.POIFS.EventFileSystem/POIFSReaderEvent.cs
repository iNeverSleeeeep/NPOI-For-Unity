using NPOI.POIFS.FileSystem;

namespace NPOI.POIFS.EventFileSystem
{
	/// Class POIFSReaderEvent
	///
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @version %I%, %G%
	public class POIFSReaderEvent
	{
		private DocumentInputStream stream;

		private POIFSDocumentPath path;

		private string documentName;

		/// @return the DocumentInputStream, freshly opened
		public DocumentInputStream Stream => stream;

		/// @return the document's path
		public POIFSDocumentPath Path => path;

		/// @return the document's name
		public string Name => documentName;

		/// package scoped constructor
		///
		/// @param stream the DocumentInputStream, freshly opened
		/// @param path the path of the document
		/// @param documentName the name of the document
		public POIFSReaderEvent(DocumentInputStream stream, POIFSDocumentPath path, string documentName)
		{
			this.stream = stream;
			this.path = path;
			this.documentName = documentName;
		}
	}
}
