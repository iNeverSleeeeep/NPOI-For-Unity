using NPOI.POIFS.FileSystem;

namespace NPOI.POIFS.EventFileSystem
{
	/// Class POIFSWriterEvent
	///
	/// @author Marc Johnson (mjohnson at apache dot org)
	/// @version %I%, %G%
	public class POIFSWriterEvent
	{
		private DocumentOutputStream stream;

		private POIFSDocumentPath path;

		private string documentName;

		private int limit;

		/// @return the DocumentOutputStream, freshly opened
		public DocumentOutputStream Stream => stream;

		/// @return the document's path
		public POIFSDocumentPath Path => path;

		/// @return the document's name
		public string Name => documentName;

		/// @return the limit on writing, in bytes
		public int Limit => limit;

		/// namespace scoped constructor
		///
		/// @param stream the DocumentOutputStream, freshly opened
		/// @param path the path of the document
		/// @param documentName the name of the document
		/// @param limit the limit, in bytes, that can be written to the
		///              stream
		public POIFSWriterEvent(DocumentOutputStream stream, POIFSDocumentPath path, string documentName, int limit)
		{
			this.stream = stream;
			this.path = path;
			this.documentName = documentName;
			this.limit = limit;
		}
	}
}
