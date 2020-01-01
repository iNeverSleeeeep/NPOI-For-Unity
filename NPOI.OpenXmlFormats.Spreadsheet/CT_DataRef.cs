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
	public class CT_DataRef
	{
		private string refField;

		private string nameField;

		private string sheetField;

		private string idField;

		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
			}
		}

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

		public string sheet
		{
			get
			{
				return sheetField;
			}
			set
			{
				sheetField = value;
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

		public static CT_DataRef Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DataRef cT_DataRef = new CT_DataRef();
			cT_DataRef.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			cT_DataRef.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_DataRef.sheet = XmlHelper.ReadString(node.Attributes["sheet"]);
			cT_DataRef.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			return cT_DataRef;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "sheet", sheet);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
