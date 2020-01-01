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
	public class CT_VerticalJc
	{
		private ST_VerticalJc valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_VerticalJc val
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

		public static CT_VerticalJc Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_VerticalJc cT_VerticalJc = new CT_VerticalJc();
			if (node.Attributes["w:val"] != null)
			{
				cT_VerticalJc.val = (ST_VerticalJc)Enum.Parse(typeof(ST_VerticalJc), node.Attributes["w:val"].Value);
			}
			return cT_VerticalJc;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write("/>");
		}
	}
}
