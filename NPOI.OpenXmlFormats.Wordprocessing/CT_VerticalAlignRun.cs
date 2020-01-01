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
	public class CT_VerticalAlignRun
	{
		private ST_VerticalAlignRun valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_VerticalAlignRun val
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

		public static CT_VerticalAlignRun Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_VerticalAlignRun cT_VerticalAlignRun = new CT_VerticalAlignRun();
			if (node.Attributes["w:val"] != null)
			{
				cT_VerticalAlignRun.val = (ST_VerticalAlignRun)Enum.Parse(typeof(ST_VerticalAlignRun), node.Attributes["w:val"].Value);
			}
			return cT_VerticalAlignRun;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "w:val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</w:{0}>", nodeName));
		}
	}
}
