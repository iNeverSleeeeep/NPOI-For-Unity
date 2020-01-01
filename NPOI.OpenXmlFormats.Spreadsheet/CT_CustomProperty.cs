using NPOI.OpenXml4Net.Util;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CustomProperty
	{
		private string nameField;

		private string idField;

		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

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

		public static CT_CustomProperty Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomProperty cT_CustomProperty = new CT_CustomProperty();
			cT_CustomProperty.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_CustomProperty.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			return cT_CustomProperty;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
