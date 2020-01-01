using NPOI.OpenXml4Net.OPC;
using NPOI.OpenXmlFormats.Spreadsheet;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace NPOI.XSSF.Model
{
	public class CommentsTable : POIXMLDocumentPart
	{
		private CT_Comments comments;

		/// XML Beans uses a list, which is very slow
		///  to search, so we wrap things with our own
		///  map for fast Lookup.
		private Dictionary<string, CT_Comment> commentRefs;

		public CommentsTable()
		{
			comments = new CT_Comments();
			comments.AddNewCommentList();
			comments.AddNewAuthors().AddAuthor("");
		}

		internal CommentsTable(PackagePart part, PackageRelationship rel)
			: base(part, rel)
		{
			XmlDocument xmlDoc = POIXMLDocumentPart.ConvertStreamToXml(part.GetInputStream());
			ReadFrom(xmlDoc);
		}

		public void ReadFrom(XmlDocument xmlDoc)
		{
			try
			{
				CommentsDocument commentsDocument = CommentsDocument.Parse(xmlDoc, POIXMLDocumentPart.NamespaceManager);
				comments = commentsDocument.GetComments();
			}
			catch (XmlException ex)
			{
				throw new IOException(ex.Message);
			}
		}

		public void WriteTo(Stream out1)
		{
			CommentsDocument commentsDocument = new CommentsDocument();
			commentsDocument.SetComments(comments);
			commentsDocument.Save(out1);
		}

		protected override void Commit()
		{
			PackagePart packagePart = GetPackagePart();
			Stream outputStream = packagePart.GetOutputStream();
			WriteTo(outputStream);
			outputStream.Close();
		}

		/// Called after the reference is updated, so that
		///  we can reflect that in our cache
		public void ReferenceUpdated(string oldReference, CT_Comment comment)
		{
			if (commentRefs != null)
			{
				commentRefs.Remove(oldReference);
				commentRefs[comment.@ref] = comment;
			}
		}

		public void RecreateReference()
		{
			commentRefs.Clear();
			foreach (CT_Comment item in comments.commentList.comment)
			{
				commentRefs.Add(item.@ref, item);
			}
		}

		public int GetNumberOfComments()
		{
			return comments.commentList.SizeOfCommentArray();
		}

		public int GetNumberOfAuthors()
		{
			return comments.authors.SizeOfAuthorArray();
		}

		public string GetAuthor(long authorId)
		{
			return comments.authors.GetAuthorArray((int)authorId);
		}

		/// <summary>
		/// Searches the author. If not found he is added to the list of authors.
		/// </summary>
		/// <param name="author">author to search</param>
		/// <returns>index of the author</returns>
		public int FindAuthor(string author)
		{
			for (int i = 0; i < comments.authors.SizeOfAuthorArray(); i++)
			{
				if (comments.authors.GetAuthorArray(i).Equals(author))
				{
					return i;
				}
			}
			return AddNewAuthor(author);
		}

		public XSSFComment FindCellComment(string cellRef)
		{
			CT_Comment cTComment = GetCTComment(cellRef);
			if (cTComment != null)
			{
				return new XSSFComment(this, cTComment, null);
			}
			return null;
		}

		public CT_Comment GetCTComment(string cellRef)
		{
			if (commentRefs == null)
			{
				commentRefs = new Dictionary<string, CT_Comment>();
				foreach (CT_Comment item in comments.commentList.comment)
				{
					commentRefs.Add(item.@ref, item);
				}
			}
			if (!commentRefs.ContainsKey(cellRef))
			{
				return null;
			}
			return commentRefs[cellRef];
		}

		public CT_Comment CreateComment()
		{
			CT_Comment cT_Comment = comments.commentList.AddNewComment();
			cT_Comment.@ref = "A1";
			cT_Comment.authorId = 0u;
			if (commentRefs != null)
			{
				if (commentRefs.ContainsKey(cT_Comment.@ref))
				{
					commentRefs[cT_Comment.@ref] = cT_Comment;
				}
				else
				{
					commentRefs.Add(cT_Comment.@ref, cT_Comment);
				}
			}
			return cT_Comment;
		}

		public bool RemoveComment(string cellRef)
		{
			CT_CommentList commentList = comments.commentList;
			if (commentList != null)
			{
				for (int i = 0; i < commentList.SizeOfCommentArray(); i++)
				{
					CT_Comment commentArray = commentList.GetCommentArray(i);
					if (cellRef.Equals(commentArray.@ref))
					{
						commentList.RemoveComment(i);
						if (commentRefs != null)
						{
							commentRefs.Remove(cellRef);
						}
						return true;
					}
				}
			}
			return false;
		}

		private int AddNewAuthor(string author)
		{
			int num = comments.authors.SizeOfAuthorArray();
			comments.authors.Insert(num, author);
			return num;
		}

		public CT_Comments GetCTComments()
		{
			return comments;
		}
	}
}
