using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Presentation
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:powerpoint")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:powerpoint", IsNullable = true)]
	public class CT_Rel
	{
		private string idField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		public static CT_Rel Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Rel cT_Rel = new CT_Rel();
			cT_Rel.id = XmlHelper.ReadString(node.Attributes["r:id"]);
			return cT_Rel;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<p:{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			sw.Write(string.Format("</p:{0}>", nodeName));
		}
	}
}
