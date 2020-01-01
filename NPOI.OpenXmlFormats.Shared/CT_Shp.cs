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
	public class CT_Shp
	{
		private ST_Shp valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Shp val
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

		public static CT_Shp Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Shp cT_Shp = new CT_Shp();
			if (node.Attributes["m:val"] != null)
			{
				cT_Shp.val = (ST_Shp)Enum.Parse(typeof(ST_Shp), node.Attributes["m:val"].Value);
			}
			return cT_Shp;
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
