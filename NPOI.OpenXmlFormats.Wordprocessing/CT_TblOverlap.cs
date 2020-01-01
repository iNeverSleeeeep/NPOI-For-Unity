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
	public class CT_TblOverlap
	{
		private ST_TblOverlap valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_TblOverlap val
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

		public static CT_TblOverlap Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TblOverlap cT_TblOverlap = new CT_TblOverlap();
			if (node.Attributes["w:val"] != null)
			{
				cT_TblOverlap.val = (ST_TblOverlap)Enum.Parse(typeof(ST_TblOverlap), node.Attributes["w:val"].Value);
			}
			return cT_TblOverlap;
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
