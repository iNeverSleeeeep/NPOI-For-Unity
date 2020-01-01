using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlInclude(typeof(CT_MoveBookmark))]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Bookmark : CT_BookmarkRange
	{
		private string nameField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public new static CT_Bookmark Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Bookmark cT_Bookmark = new CT_Bookmark();
			cT_Bookmark.name = XmlHelper.ReadString(node.Attributes["w:name"]);
			cT_Bookmark.colFirst = XmlHelper.ReadString(node.Attributes["w:colFirst"]);
			cT_Bookmark.colLast = XmlHelper.ReadString(node.Attributes["w:colLast"]);
			if (node.Attributes["w:displacedByCustomXml"] != null)
			{
				cT_Bookmark.displacedByCustomXml = (ST_DisplacedByCustomXml)Enum.Parse(typeof(ST_DisplacedByCustomXml), node.Attributes["w:displacedByCustomXml"].Value);
			}
			cT_Bookmark.id = XmlHelper.ReadString(node.Attributes["w:id"]);
			return cT_Bookmark;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:id", base.id);
			XmlHelper.WriteAttribute(sw, "w:name", name);
			XmlHelper.WriteAttribute(sw, "w:colFirst", base.colFirst);
			XmlHelper.WriteAttribute(sw, "w:colLast", base.colLast);
			if (base.displacedByCustomXml != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:displacedByCustomXml", base.displacedByCustomXml.ToString());
			}
			sw.Write("/>");
		}
	}
}
