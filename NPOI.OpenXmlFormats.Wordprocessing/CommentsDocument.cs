using System.IO;
using System.Xml;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	public class CommentsDocument
	{
		private CT_Comments comments;

		public CT_Comments Comments
		{
			get
			{
				return comments;
			}
		}

		public CommentsDocument()
		{
			comments = new CT_Comments();
		}

		public static CommentsDocument Parse(XmlDocument doc, XmlNamespaceManager NameSpaceManager)
		{
			CT_Comments cT_Comments = CT_Comments.Parse(doc.DocumentElement, NameSpaceManager);
			return new CommentsDocument(cT_Comments);
		}

		public CommentsDocument(CT_Comments comments)
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
