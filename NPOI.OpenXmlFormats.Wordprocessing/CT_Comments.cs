using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot("comments", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Comments
	{
		private List<CT_Comment> commentField;

		[XmlElement("comment", Order = 0)]
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

		public static CT_Comments Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Comments cT_Comments = new CT_Comments();
			cT_Comments.comment = new List<CT_Comment>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "comment")
				{
					cT_Comments.comment.Add(CT_Comment.Parse(childNode, namespaceManager));
				}
			}
			return cT_Comments;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write(string.Format("<w:comments "));
			sw.Write("xmlns:ve=\"http://schemas.openxmlformats.org/markup-compatibility/2006\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
			sw.Write("xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" ");
			sw.Write("xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:wp=\"http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing\" ");
			sw.Write("xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" ");
			sw.Write("xmlns:wne=\"http://schemas.microsoft.com/office/word/2006/wordml\">");
			sw.Write(">");
			if (comment != null)
			{
				foreach (CT_Comment item in comment)
				{
					item.Write(sw, "comment");
				}
			}
			sw.Write("</w:comments>");
		}
	}
}
