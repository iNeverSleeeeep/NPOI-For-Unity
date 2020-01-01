using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot("sheet", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	public class CT_Sheet
	{
		private string nameField;

		private uint sheetIdField;

		private ST_SheetState stateField;

		private string idField;

		[XmlAttribute("name")]
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

		[XmlAttribute("sheetId")]
		public uint sheetId
		{
			get
			{
				return sheetIdField;
			}
			set
			{
				sheetIdField = value;
			}
		}

		[XmlAttribute("state")]
		[DefaultValue(ST_SheetState.visible)]
		public ST_SheetState state
		{
			get
			{
				return stateField;
			}
			set
			{
				stateField = value;
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

		public static CT_Sheet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Sheet cT_Sheet = new CT_Sheet();
			cT_Sheet.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_Sheet.sheetId = XmlHelper.ReadUInt(node.Attributes["sheetId"]);
			if (node.Attributes["state"] != null)
			{
				cT_Sheet.state = (ST_SheetState)Enum.Parse(typeof(ST_SheetState), node.Attributes["state"].Value);
			}
			cT_Sheet.id = XmlHelper.ReadString(node.Attributes["id", "http://schemas.openxmlformats.org/officeDocument/2006/relationships"]);
			return cT_Sheet;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "sheetId", sheetId);
			if (state != 0)
			{
				XmlHelper.WriteAttribute(sw, "state", state.ToString());
			}
			XmlHelper.WriteAttribute(sw, "r:id", id);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_Sheet()
		{
			stateField = ST_SheetState.visible;
		}

		public void Set(CT_Sheet sheet)
		{
			nameField = sheet.nameField;
			sheetIdField = sheet.sheetIdField;
			stateField = sheet.stateField;
			idField = sheet.idField;
		}

		public CT_Sheet Copy()
		{
			CT_Sheet cT_Sheet = new CT_Sheet();
			cT_Sheet.idField = idField;
			cT_Sheet.sheetIdField = sheetIdField;
			cT_Sheet.nameField = nameField;
			cT_Sheet.stateField = stateField;
			return cT_Sheet;
		}
	}
}
