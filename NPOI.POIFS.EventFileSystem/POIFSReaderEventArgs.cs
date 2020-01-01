using NPOI.POIFS.FileSystem;
using System;

namespace NPOI.POIFS.EventFileSystem
{
	/// <summary>
	/// EventArgs for POIFSReader
	/// author: Tony Qu
	/// </summary>
	public class POIFSReaderEventArgs : EventArgs
	{
		private POIFSDocumentPath path;

		private POIFSDocument document;

		private string name;

		public virtual POIFSDocumentPath Path => path;

		public virtual POIFSDocument Document => document;

		public virtual DocumentInputStream Stream => new DocumentInputStream(document);

		public virtual string Name => name;

		public POIFSReaderEventArgs(string name, POIFSDocumentPath path, POIFSDocument document)
		{
			this.name = name;
			this.path = path;
			this.document = document;
		}
	}
}
