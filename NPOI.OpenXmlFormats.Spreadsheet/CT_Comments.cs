using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", ElementName = "comments")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Comments
	{
		private CT_Authors authorsField = new CT_Authors();

		private CT_CommentList commentListField = new CT_CommentList();

		private CT_ExtensionList extLstField;

		[XmlElement("authors", Order = 0)]
		public CT_Authors authors
		{
			get
			{
				return authorsField;
			}
			set
			{
				authorsField = value;
			}
		}

		[XmlElement("commentList", Order = 1)]
		public CT_CommentList commentList
		{
			get
			{
				return commentListField;
			}
			set
			{
				commentListField = value;
			}
		}

		[XmlElement("extLst", Order = 2)]
		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public static CT_Comments Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Comments cT_Comments = new CT_Comments();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "authors")
				{
					cT_Comments.authors = CT_Authors.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "commentList")
				{
					cT_Comments.commentList = CT_CommentList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Comments.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Comments;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\" ?>");
			sw.Write("<comments xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\">");
			if (authors != null)
			{
				authors.Write(sw, "authors");
			}
			if (commentList != null)
			{
				commentList.Write(sw, "commentList");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write("</comments>");
		}

		public CT_Authors AddNewAuthors()
		{
			authorsField = new CT_Authors();
			return authorsField;
		}

		public void AddNewCommentList()
		{
			commentListField = new CT_CommentList();
		}
	}
}
