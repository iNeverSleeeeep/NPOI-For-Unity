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
	public class CT_LevelSuffix
	{
		private ST_LevelSuffix valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_LevelSuffix val
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

		public static CT_LevelSuffix Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LevelSuffix cT_LevelSuffix = new CT_LevelSuffix();
			if (node.Attributes["w:val"] != null)
			{
				cT_LevelSuffix.val = (ST_LevelSuffix)Enum.Parse(typeof(ST_LevelSuffix), node.Attributes["w:val"].Value);
			}
			return cT_LevelSuffix;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write("/>");
		}
	}
}
