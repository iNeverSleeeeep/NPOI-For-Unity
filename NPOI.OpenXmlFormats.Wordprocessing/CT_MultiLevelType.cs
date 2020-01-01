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
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_MultiLevelType
	{
		private ST_MultiLevelType valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_MultiLevelType val
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

		public static CT_MultiLevelType Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MultiLevelType cT_MultiLevelType = new CT_MultiLevelType();
			if (node.Attributes["w:val"] != null)
			{
				cT_MultiLevelType.val = (ST_MultiLevelType)Enum.Parse(typeof(ST_MultiLevelType), node.Attributes["w:val"].Value);
			}
			return cT_MultiLevelType;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write("/>");
		}
	}
}
