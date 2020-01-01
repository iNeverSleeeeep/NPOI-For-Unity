using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlInclude(typeof(CT_BookmarkRange))]
	[XmlInclude(typeof(CT_Bookmark))]
	[XmlInclude(typeof(CT_MoveBookmark))]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_MarkupRange : CT_Markup
	{
		private ST_DisplacedByCustomXml displacedByCustomXmlField;

		private bool displacedByCustomXmlFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_DisplacedByCustomXml displacedByCustomXml
		{
			get
			{
				return displacedByCustomXmlField;
			}
			set
			{
				displacedByCustomXmlField = value;
			}
		}

		[XmlIgnore]
		public bool displacedByCustomXmlSpecified
		{
			get
			{
				return displacedByCustomXmlFieldSpecified;
			}
			set
			{
				displacedByCustomXmlFieldSpecified = value;
			}
		}

		public new static CT_MarkupRange Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MarkupRange cT_MarkupRange = new CT_MarkupRange();
			if (node.Attributes["w:displacedByCustomXml"] != null)
			{
				cT_MarkupRange.displacedByCustomXml = (ST_DisplacedByCustomXml)Enum.Parse(typeof(ST_DisplacedByCustomXml), node.Attributes["w:displacedByCustomXml"].Value);
			}
			cT_MarkupRange.id = XmlHelper.ReadString(node.Attributes["w:id"]);
			return cT_MarkupRange;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			if (displacedByCustomXml != 0)
			{
				XmlHelper.WriteAttribute(sw, "w:displacedByCustomXml", displacedByCustomXml.ToString());
			}
			XmlHelper.WriteAttribute(sw, "w:id", base.id);
			sw.Write("/>");
		}
	}
}
