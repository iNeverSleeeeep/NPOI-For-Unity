using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CommentsDocument
	{
		private CT_Comments comments;

		public CommentsDocument()
		{
		}

		public CommentsDocument(CT_Comments comments)
		{
			this.comments = comments;
		}

		public static CommentsDocument Parse(XmlDocument xmlDoc, XmlNamespaceManager namespaceManager)
		{
			CommentsDocument commentsDocument = new CommentsDocument();
			commentsDocument.comments = CT_Comments.Parse(xmlDoc.DocumentElement, namespaceManager);
			return commentsDocument;
		}

		public CT_Comments GetComments()
		{
			return comments;
		}

		public void SetComments(CT_Comments comments)
		{
			this.comments = comments;
		}

		public void Save(Stream stream)
		{
			using (StreamWriter sw = new StreamWriter(stream))
			{
				comments.Write(sw);
			}
		}
	}
}
