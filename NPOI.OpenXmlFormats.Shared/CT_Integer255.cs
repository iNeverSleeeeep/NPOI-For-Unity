using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_Integer255
	{
		private string valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "integer")]
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

		public static CT_Integer255 Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Integer255 cT_Integer = new CT_Integer255();
			cT_Integer.val = XmlHelper.ReadString(node.Attributes["m:val"]);
			return cT_Integer;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "m:val", val);
			sw.Write(">");
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
