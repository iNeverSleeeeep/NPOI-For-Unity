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
	public class CT_NumFmt
	{
		private ST_NumberFormat valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_NumberFormat val
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

		public static CT_NumFmt Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumFmt cT_NumFmt = new CT_NumFmt();
			if (node.Attributes["w:val"] != null)
			{
				cT_NumFmt.val = (ST_NumberFormat)Enum.Parse(typeof(ST_NumberFormat), node.Attributes["w:val"].Value);
			}
			return cT_NumFmt;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write("/>");
		}
	}
}
