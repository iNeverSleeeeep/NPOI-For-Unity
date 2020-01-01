using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_LimLoc
	{
		private ST_LimLoc valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_LimLoc val
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

		public static CT_LimLoc Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_LimLoc cT_LimLoc = new CT_LimLoc();
			if (node.Attributes["m:val"] != null)
			{
				cT_LimLoc.val = (ST_LimLoc)Enum.Parse(typeof(ST_LimLoc), node.Attributes["m:val"].Value);
			}
			return cT_LimLoc;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<m:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "m:val", val.ToString());
			sw.Write(">");
			sw.Write(string.Format("</m:{0}>", nodeName));
		}
	}
}
