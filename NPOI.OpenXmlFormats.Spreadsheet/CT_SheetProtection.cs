using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SheetProtection
	{
		private string passwordField;

		private bool sheetField;

		private bool objectsField;

		private bool scenariosField;

		private bool formatCellsField;

		private bool formatColumnsField;

		private bool formatRowsField;

		private bool insertColumnsField;

		private bool insertRowsField;

		private bool insertHyperlinksField;

		private bool deleteColumnsField;

		private bool deleteRowsField;

		private bool selectLockedCellsField;

		private bool sortField;

		private bool autoFilterField;

		private bool pivotTablesField;

		private bool selectUnlockedCellsField;

		[XmlAttribute]
		public string password
		{
			get
			{
				return passwordField;
			}
			set
			{
				passwordField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool sheet
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

		[XmlAttribute]
		[DefaultValue(false)]
		public bool objects
		{
			get
			{
				return objectsField;
			}
			set
			{
				objectsField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool scenarios
		{
			get
			{
				return scenariosField;
			}
			set
			{
				scenariosField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool formatCells
		{
			get
			{
				return formatCellsField;
			}
			set
			{
				formatCellsField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool formatColumns
		{
			get
			{
				return formatColumnsField;
			}
			set
			{
				formatColumnsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool formatRows
		{
			get
			{
				return formatRowsField;
			}
			set
			{
				formatRowsField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool insertColumns
		{
			get
			{
				return insertColumnsField;
			}
			set
			{
				insertColumnsField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool insertRows
		{
			get
			{
				return insertRowsField;
			}
			set
			{
				insertRowsField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool insertHyperlinks
		{
			get
			{
				return insertHyperlinksField;
			}
			set
			{
				insertHyperlinksField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool deleteColumns
		{
			get
			{
				return deleteColumnsField;
			}
			set
			{
				deleteColumnsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool deleteRows
		{
			get
			{
				return deleteRowsField;
			}
			set
			{
				deleteRowsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool selectLockedCells
		{
			get
			{
				return selectLockedCellsField;
			}
			set
			{
				selectLockedCellsField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool sort
		{
			get
			{
				return sortField;
			}
			set
			{
				sortField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool autoFilter
		{
			get
			{
				return autoFilterField;
			}
			set
			{
				autoFilterField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool pivotTables
		{
			get
			{
				return pivotTablesField;
			}
			set
			{
				pivotTablesField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool selectUnlockedCells
		{
			get
			{
				return selectUnlockedCellsField;
			}
			set
			{
				selectUnlockedCellsField = value;
			}
		}

		public CT_SheetProtection()
		{
			formatCellsField = true;
			formatColumnsField = true;
			formatRowsField = true;
			insertColumnsField = true;
			insertRowsField = true;
			insertHyperlinksField = true;
			deleteColumnsField = true;
			deleteRowsField = true;
			selectLockedCellsField = false;
			sortField = true;
			autoFilterField = true;
			pivotTablesField = true;
			selectUnlockedCellsField = false;
		}

		public static CT_SheetProtection Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SheetProtection cT_SheetProtection = new CT_SheetProtection();
			cT_SheetProtection.password = XmlHelper.ReadString(node.Attributes["password"]);
			cT_SheetProtection.sheet = XmlHelper.ReadBool(node.Attributes["sheet"]);
			cT_SheetProtection.objects = XmlHelper.ReadBool(node.Attributes["objects"]);
			cT_SheetProtection.scenarios = XmlHelper.ReadBool(node.Attributes["scenarios"]);
			cT_SheetProtection.formatCells = XmlHelper.ReadBool(node.Attributes["formatCells"]);
			cT_SheetProtection.formatColumns = XmlHelper.ReadBool(node.Attributes["formatColumns"]);
			cT_SheetProtection.formatRows = XmlHelper.ReadBool(node.Attributes["formatRows"]);
			cT_SheetProtection.insertColumns = XmlHelper.ReadBool(node.Attributes["insertColumns"]);
			cT_SheetProtection.insertRows = XmlHelper.ReadBool(node.Attributes["insertRows"]);
			cT_SheetProtection.insertHyperlinks = XmlHelper.ReadBool(node.Attributes["insertHyperlinks"]);
			cT_SheetProtection.deleteColumns = XmlHelper.ReadBool(node.Attributes["deleteColumns"]);
			cT_SheetProtection.deleteRows = XmlHelper.ReadBool(node.Attributes["deleteRows"]);
			cT_SheetProtection.selectLockedCells = XmlHelper.ReadBool(node.Attributes["selectLockedCells"]);
			cT_SheetProtection.sort = XmlHelper.ReadBool(node.Attributes["sort"]);
			cT_SheetProtection.autoFilter = XmlHelper.ReadBool(node.Attributes["autoFilter"]);
			cT_SheetProtection.pivotTables = XmlHelper.ReadBool(node.Attributes["pivotTables"]);
			cT_SheetProtection.selectUnlockedCells = XmlHelper.ReadBool(node.Attributes["selectUnlockedCells"]);
			return cT_SheetProtection;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "password", password);
			XmlHelper.WriteAttribute(sw, "sheet", sheet);
			XmlHelper.WriteAttribute(sw, "objects", objects);
			XmlHelper.WriteAttribute(sw, "scenarios", scenarios);
			XmlHelper.WriteAttribute(sw, "formatCells", formatCells);
			XmlHelper.WriteAttribute(sw, "formatColumns", formatColumns);
			XmlHelper.WriteAttribute(sw, "formatRows", formatRows);
			XmlHelper.WriteAttribute(sw, "insertColumns", insertColumns);
			XmlHelper.WriteAttribute(sw, "insertRows", insertRows);
			XmlHelper.WriteAttribute(sw, "insertHyperlinks", insertHyperlinks);
			XmlHelper.WriteAttribute(sw, "deleteColumns", deleteColumns);
			XmlHelper.WriteAttribute(sw, "deleteRows", deleteRows);
			XmlHelper.WriteAttribute(sw, "selectLockedCells", selectLockedCells);
			XmlHelper.WriteAttribute(sw, "sort", sort);
			XmlHelper.WriteAttribute(sw, "autoFilter", autoFilter);
			XmlHelper.WriteAttribute(sw, "pivotTables", pivotTables);
			XmlHelper.WriteAttribute(sw, "selectUnlockedCells", selectUnlockedCells);
			sw.Write(">");
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
