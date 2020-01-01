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
	public class CT_Language
	{
		private string valField;

		private string eastAsiaField;

		private string bidiField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string eastAsia
		{
			get
			{
				return eastAsiaField;
			}
			set
			{
				eastAsiaField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string bidi
		{
			get
			{
				return bidiField;
			}
			set
			{
				bidiField = value;
			}
		}

		public static CT_Language Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Language cT_Language = new CT_Language();
			cT_Language.val = XmlHelper.ReadString(node.Attributes["w:val"]);
			cT_Language.eastAsia = XmlHelper.ReadString(node.Attributes["w:eastAsia"]);
			cT_Language.bidi = XmlHelper.ReadString(node.Attributes["w:bidi"]);
			return cT_Language;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val);
			XmlHelper.WriteAttribute(sw, "w:eastAsia", eastAsia);
			XmlHelper.WriteAttribute(sw, "w:bidi", bidi);
			sw.Write("/>");
		}
	}
}
