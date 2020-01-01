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
	public class CT_FtnPos
	{
		private ST_FtnPos valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_FtnPos val
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

		public static CT_FtnPos Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_FtnPos cT_FtnPos = new CT_FtnPos();
			if (node.Attributes["w:val"] != null)
			{
				cT_FtnPos.val = (ST_FtnPos)Enum.Parse(typeof(ST_FtnPos), node.Attributes["w:val"].Value);
			}
			return cT_FtnPos;
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
