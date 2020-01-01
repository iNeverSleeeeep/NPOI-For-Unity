using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", ElementName = "comment")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Comment
	{
		private CT_Rst textField = new CT_Rst();

		private string refField = string.Empty;

		private uint authorIdField;

		private string guidField;

		[XmlElement("text")]
		public CT_Rst text
		{
			get
			{
				return textField;
			}
			set
			{
				textField = value;
			}
		}

		[XmlAttribute("ref")]
		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
			}
		}

		[XmlAttribute("authorId")]
		public uint authorId
		{
			get
			{
				return authorIdField;
			}
			set
			{
				authorIdField = value;
			}
		}

		[XmlAttribute("guid")]
		public string guid
		{
			get
			{
				return guidField;
			}
			set
			{
				guidField = value;
			}
		}

		public static CT_Comment Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Comment cT_Comment = new CT_Comment();
			cT_Comment.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			if (node.Attributes["authorId"] != null)
			{
				cT_Comment.authorId = XmlHelper.ReadUInt(node.Attributes["authorId"]);
			}
			cT_Comment.guid = XmlHelper.ReadString(node.Attributes["guid"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "text")
				{
					cT_Comment.text = CT_Rst.Parse(childNode, namespaceManager);
				}
			}
			return cT_Comment;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			XmlHelper.WriteAttribute(sw, "authorId", (double)authorId, true);
			XmlHelper.WriteAttribute(sw, "guid", guid);
			sw.Write(">");
			if (text != null)
			{
				text.Write(sw, "text");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
