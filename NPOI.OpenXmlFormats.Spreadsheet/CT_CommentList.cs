using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	public class CT_CommentList
	{
		private List<CT_Comment> commentField;

		[XmlElement("comment")]
		public List<CT_Comment> comment
		{
			get
			{
				return commentField;
			}
			set
			{
				commentField = value;
			}
		}

		public static CT_CommentList Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CommentList cT_CommentList = new CT_CommentList();
			cT_CommentList.comment = new List<CT_Comment>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "comment")
				{
					cT_CommentList.comment.Add(CT_Comment.Parse(childNode, namespaceManager));
				}
			}
			return cT_CommentList;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}>", nodeName));
			if (comment != null)
			{
				foreach (CT_Comment item in comment)
				{
					item.Write(sw, "comment");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_Comment GetCommentArray(int index)
		{
			return comment[index];
		}

		public void RemoveComment(int index)
		{
			comment.RemoveAt(index);
		}

		public int SizeOfCommentArray()
		{
			return comment.Count;
		}

		public CT_Comment InsertNewComment(int index)
		{
			if (commentField == null)
			{
				commentField = new List<CT_Comment>();
			}
			CT_Comment cT_Comment = new CT_Comment();
			commentField.Insert(index, cT_Comment);
			return cT_Comment;
		}

		public CT_Comment AddNewComment()
		{
			if (commentField == null)
			{
				commentField = new List<CT_Comment>();
			}
			CT_Comment cT_Comment = new CT_Comment();
			commentField.Add(cT_Comment);
			return cT_Comment;
		}
	}
}
