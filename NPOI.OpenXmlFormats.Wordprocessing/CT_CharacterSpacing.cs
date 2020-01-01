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
	public class CT_CharacterSpacing
	{
		private ST_CharacterSpacing valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_CharacterSpacing val
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

		public static CT_CharacterSpacing Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CharacterSpacing cT_CharacterSpacing = new CT_CharacterSpacing();
			if (node.Attributes["w:val"] != null)
			{
				cT_CharacterSpacing.val = (ST_CharacterSpacing)Enum.Parse(typeof(ST_CharacterSpacing), node.Attributes["w:val"].Value);
			}
			return cT_CharacterSpacing;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write("/>");
		}
	}
}
