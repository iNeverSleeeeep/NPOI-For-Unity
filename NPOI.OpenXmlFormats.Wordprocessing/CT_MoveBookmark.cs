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
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_MoveBookmark : CT_Bookmark
	{
		private string authorField;

		private string dateField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string author
		{
			get
			{
				return authorField;
			}
			set
			{
				authorField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string date
		{
			get
			{
				return dateField;
			}
			set
			{
				dateField = value;
			}
		}

		public new static CT_MoveBookmark Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MoveBookmark cT_MoveBookmark = new CT_MoveBookmark();
			cT_MoveBookmark.author = XmlHelper.ReadString(node.Attributes["w:author"]);
			cT_MoveBookmark.date = XmlHelper.ReadString(node.Attributes["w:date"]);
			cT_MoveBookmark.name = XmlHelper.ReadString(node.Attributes["w:name"]);
			cT_MoveBookmark.colFirst = XmlHelper.ReadString(node.Attributes["w:colFirst"]);
			cT_MoveBookmark.colLast = XmlHelper.ReadString(node.Attributes["w:colLast"]);
			if (node.Attributes["w:displacedByCustomXml"] != null)
			{
				cT_MoveBookmark.displacedByCustomXml = (ST_DisplacedByCustomXml)Enum.Parse(typeof(ST_DisplacedByCustomXml), node.Attributes["w:displacedByCustomXml"].Value);
			}
			cT_MoveBookmark.id = XmlHelper.ReadString(node.Attributes["w:id"]);
			return cT_MoveBookmark;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:author", author);
			XmlHelper.WriteAttribute(sw, "w:date", date);
			XmlHelper.WriteAttribute(sw, "w:name", base.name);
			XmlHelper.WriteAttribute(sw, "w:colFirst", base.colFirst);
			XmlHelper.WriteAttribute(sw, "w:colLast", base.colLast);
			XmlHelper.WriteAttribute(sw, "w:displacedByCustomXml", base.displacedByCustomXml.ToString());
			XmlHelper.WriteAttribute(sw, "w:id", base.id);
			sw.Write("/>");
		}
	}
}
