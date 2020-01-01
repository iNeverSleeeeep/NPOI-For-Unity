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
	public class CT_Control
	{
		private uint shapeIdField;

		private string idField;

		private string nameField;

		public uint shapeId
		{
			get
			{
				return shapeIdField;
			}
			set
			{
				shapeIdField = value;
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

		public static CT_Control Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Control cT_Control = new CT_Control();
			cT_Control.shapeId = XmlHelper.ReadUInt(node.Attributes["shapeId"]);
			cT_Control.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			cT_Control.name = XmlHelper.ReadString(node.Attributes["name"]);
			return cT_Control;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "shapeId", shapeId);
			XmlHelper.WriteAttribute(sw, "r:id", id);
			XmlHelper.WriteAttribute(sw, "name", name);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
