using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot("table", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Table
	{
		private CT_AutoFilter autoFilterField;

		private CT_SortState sortStateField;

		private CT_TableColumns tableColumnsField;

		private CT_TableStyleInfo tableStyleInfoField;

		private CT_ExtensionList extLstField;

		private uint idField;

		private string nameField;

		private string displayNameField;

		private string commentField;

		private string refField;

		private ST_TableType tableTypeField;

		private uint headerRowCountField;

		private bool insertRowField;

		private bool insertRowShiftField;

		private uint totalsRowCountField;

		private bool totalsRowShownField;

		private bool publishedField;

		private uint headerRowDxfIdField;

		private bool headerRowDxfIdFieldSpecified;

		private uint dataDxfIdField;

		private bool dataDxfIdFieldSpecified;

		private uint totalsRowDxfIdField;

		private bool totalsRowDxfIdFieldSpecified;

		private uint headerRowBorderDxfIdField;

		private bool headerRowBorderDxfIdFieldSpecified;

		private uint tableBorderDxfIdField;

		private bool tableBorderDxfIdFieldSpecified;

		private uint totalsRowBorderDxfIdField;

		private bool totalsRowBorderDxfIdFieldSpecified;

		private string headerRowCellStyleField;

		private string dataCellStyleField;

		private string totalsRowCellStyleField;

		private uint connectionIdField;

		private bool connectionIdFieldSpecified;

		[XmlElement]
		public CT_AutoFilter autoFilter
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

		[XmlElement]
		public CT_SortState sortState
		{
			get
			{
				return sortStateField;
			}
			set
			{
				sortStateField = value;
			}
		}

		[XmlElement]
		public CT_TableColumns tableColumns
		{
			get
			{
				return tableColumnsField;
			}
			set
			{
				tableColumnsField = value;
			}
		}

		[XmlElement]
		public CT_TableStyleInfo tableStyleInfo
		{
			get
			{
				return tableStyleInfoField;
			}
			set
			{
				tableStyleInfoField = value;
			}
		}

		[XmlElement]
		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		public uint id
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

		[XmlAttribute]
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

		[XmlAttribute]
		public string displayName
		{
			get
			{
				return displayNameField;
			}
			set
			{
				displayNameField = value;
			}
		}

		[XmlAttribute]
		public string comment
		{
			get
			{
				return commentField;
			}
			set
			{
				commentField = value;
			}
		}

		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(ST_TableType.worksheet)]
		public ST_TableType tableType
		{
			get
			{
				return tableTypeField;
			}
			set
			{
				tableTypeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "1")]
		public uint headerRowCount
		{
			get
			{
				return headerRowCountField;
			}
			set
			{
				headerRowCountField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool insertRow
		{
			get
			{
				return insertRowField;
			}
			set
			{
				insertRowField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool insertRowShift
		{
			get
			{
				return insertRowShiftField;
			}
			set
			{
				insertRowShiftField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint totalsRowCount
		{
			get
			{
				return totalsRowCountField;
			}
			set
			{
				totalsRowCountField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool totalsRowShown
		{
			get
			{
				return totalsRowShownField;
			}
			set
			{
				totalsRowShownField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool published
		{
			get
			{
				return publishedField;
			}
			set
			{
				publishedField = value;
			}
		}

		[XmlAttribute]
		public uint headerRowDxfId
		{
			get
			{
				return headerRowDxfIdField;
			}
			set
			{
				headerRowDxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool headerRowDxfIdSpecified
		{
			get
			{
				return headerRowDxfIdFieldSpecified;
			}
			set
			{
				headerRowDxfIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint dataDxfId
		{
			get
			{
				return dataDxfIdField;
			}
			set
			{
				dataDxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool dataDxfIdSpecified
		{
			get
			{
				return dataDxfIdFieldSpecified;
			}
			set
			{
				dataDxfIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint totalsRowDxfId
		{
			get
			{
				return totalsRowDxfIdField;
			}
			set
			{
				totalsRowDxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool totalsRowDxfIdSpecified
		{
			get
			{
				return totalsRowDxfIdFieldSpecified;
			}
			set
			{
				totalsRowDxfIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint headerRowBorderDxfId
		{
			get
			{
				return headerRowBorderDxfIdField;
			}
			set
			{
				headerRowBorderDxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool headerRowBorderDxfIdSpecified
		{
			get
			{
				return headerRowBorderDxfIdFieldSpecified;
			}
			set
			{
				headerRowBorderDxfIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint tableBorderDxfId
		{
			get
			{
				return tableBorderDxfIdField;
			}
			set
			{
				tableBorderDxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool tableBorderDxfIdSpecified
		{
			get
			{
				return tableBorderDxfIdFieldSpecified;
			}
			set
			{
				tableBorderDxfIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint totalsRowBorderDxfId
		{
			get
			{
				return totalsRowBorderDxfIdField;
			}
			set
			{
				totalsRowBorderDxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool totalsRowBorderDxfIdSpecified
		{
			get
			{
				return totalsRowBorderDxfIdFieldSpecified;
			}
			set
			{
				totalsRowBorderDxfIdFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public string headerRowCellStyle
		{
			get
			{
				return headerRowCellStyleField;
			}
			set
			{
				headerRowCellStyleField = value;
			}
		}

		[XmlAttribute]
		public string dataCellStyle
		{
			get
			{
				return dataCellStyleField;
			}
			set
			{
				dataCellStyleField = value;
			}
		}

		[XmlAttribute]
		public string totalsRowCellStyle
		{
			get
			{
				return totalsRowCellStyleField;
			}
			set
			{
				totalsRowCellStyleField = value;
			}
		}

		[XmlAttribute]
		public uint connectionId
		{
			get
			{
				return connectionIdField;
			}
			set
			{
				connectionIdField = value;
			}
		}

		[XmlIgnore]
		public bool connectionIdSpecified
		{
			get
			{
				return connectionIdFieldSpecified;
			}
			set
			{
				connectionIdFieldSpecified = value;
			}
		}

		public CT_Table()
		{
			tableTypeField = ST_TableType.worksheet;
			headerRowCountField = 1u;
			insertRowField = false;
			insertRowShiftField = false;
			totalsRowCountField = 0u;
			totalsRowShownField = true;
			publishedField = false;
		}

		public static CT_Table Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Table cT_Table = new CT_Table();
			if (node.Attributes["id"] != null)
			{
				cT_Table.id = XmlHelper.ReadUInt(node.Attributes["id"]);
			}
			cT_Table.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_Table.displayName = XmlHelper.ReadString(node.Attributes["displayName"]);
			cT_Table.comment = XmlHelper.ReadString(node.Attributes["comment"]);
			cT_Table.@ref = XmlHelper.ReadString(node.Attributes["ref"]);
			if (node.Attributes["tableType"] != null)
			{
				cT_Table.tableType = (ST_TableType)Enum.Parse(typeof(ST_TableType), node.Attributes["tableType"].Value);
			}
			if (node.Attributes["headerRowCount"] != null)
			{
				cT_Table.headerRowCount = XmlHelper.ReadUInt(node.Attributes["headerRowCount"]);
			}
			if (node.Attributes["insertRow"] != null)
			{
				cT_Table.insertRow = XmlHelper.ReadBool(node.Attributes["insertRow"]);
			}
			if (node.Attributes["insertRowShift"] != null)
			{
				cT_Table.insertRowShift = XmlHelper.ReadBool(node.Attributes["insertRowShift"]);
			}
			if (node.Attributes["totalsRowCount"] != null)
			{
				cT_Table.totalsRowCount = XmlHelper.ReadUInt(node.Attributes["totalsRowCount"]);
			}
			if (node.Attributes["totalsRowShown"] != null)
			{
				cT_Table.totalsRowShown = XmlHelper.ReadBool(node.Attributes["totalsRowShown"]);
			}
			if (node.Attributes["published"] != null)
			{
				cT_Table.published = XmlHelper.ReadBool(node.Attributes["published"]);
			}
			if (node.Attributes["headerRowDxfId"] != null)
			{
				cT_Table.headerRowDxfId = XmlHelper.ReadUInt(node.Attributes["headerRowDxfId"]);
			}
			if (node.Attributes["dataDxfId"] != null)
			{
				cT_Table.dataDxfId = XmlHelper.ReadUInt(node.Attributes["dataDxfId"]);
			}
			if (node.Attributes["totalsRowDxfId"] != null)
			{
				cT_Table.totalsRowDxfId = XmlHelper.ReadUInt(node.Attributes["totalsRowDxfId"]);
			}
			if (node.Attributes["headerRowBorderDxfId"] != null)
			{
				cT_Table.headerRowBorderDxfId = XmlHelper.ReadUInt(node.Attributes["headerRowBorderDxfId"]);
			}
			if (node.Attributes["tableBorderDxfId"] != null)
			{
				cT_Table.tableBorderDxfId = XmlHelper.ReadUInt(node.Attributes["tableBorderDxfId"]);
			}
			if (node.Attributes["totalsRowBorderDxfId"] != null)
			{
				cT_Table.totalsRowBorderDxfId = XmlHelper.ReadUInt(node.Attributes["totalsRowBorderDxfId"]);
			}
			cT_Table.headerRowCellStyle = XmlHelper.ReadString(node.Attributes["headerRowCellStyle"]);
			cT_Table.dataCellStyle = XmlHelper.ReadString(node.Attributes["dataCellStyle"]);
			cT_Table.totalsRowCellStyle = XmlHelper.ReadString(node.Attributes["totalsRowCellStyle"]);
			if (node.Attributes["connectionId"] != null)
			{
				cT_Table.connectionId = XmlHelper.ReadUInt(node.Attributes["connectionId"]);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "autoFilter")
				{
					cT_Table.autoFilter = CT_AutoFilter.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sortState")
				{
					cT_Table.sortState = CT_SortState.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tableColumns")
				{
					cT_Table.tableColumns = CT_TableColumns.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tableStyleInfo")
				{
					cT_Table.tableStyleInfo = CT_TableStyleInfo.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Table.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Table;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<table xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\"");
			XmlHelper.WriteAttribute(sw, "id", id);
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "displayName", displayName);
			XmlHelper.WriteAttribute(sw, "comment", comment);
			XmlHelper.WriteAttribute(sw, "ref", @ref);
			XmlHelper.WriteAttribute(sw, "tableType", tableType.ToString());
			XmlHelper.WriteAttribute(sw, "headerRowCount", headerRowCount);
			XmlHelper.WriteAttribute(sw, "insertRow", insertRow);
			XmlHelper.WriteAttribute(sw, "insertRowShift", insertRowShift);
			XmlHelper.WriteAttribute(sw, "totalsRowCount", totalsRowCount);
			XmlHelper.WriteAttribute(sw, "totalsRowShown", totalsRowShown);
			XmlHelper.WriteAttribute(sw, "published", published);
			XmlHelper.WriteAttribute(sw, "headerRowDxfId", headerRowDxfId);
			XmlHelper.WriteAttribute(sw, "dataDxfId", dataDxfId);
			XmlHelper.WriteAttribute(sw, "totalsRowDxfId", totalsRowDxfId);
			XmlHelper.WriteAttribute(sw, "headerRowBorderDxfId", headerRowBorderDxfId);
			XmlHelper.WriteAttribute(sw, "tableBorderDxfId", tableBorderDxfId);
			XmlHelper.WriteAttribute(sw, "totalsRowBorderDxfId", totalsRowBorderDxfId);
			XmlHelper.WriteAttribute(sw, "headerRowCellStyle", headerRowCellStyle);
			XmlHelper.WriteAttribute(sw, "dataCellStyle", dataCellStyle);
			XmlHelper.WriteAttribute(sw, "totalsRowCellStyle", totalsRowCellStyle);
			XmlHelper.WriteAttribute(sw, "connectionId", connectionId);
			sw.Write(">");
			if (autoFilter != null)
			{
				autoFilter.Write(sw, "autoFilter");
			}
			if (sortState != null)
			{
				sortState.Write(sw, "sortState");
			}
			if (tableColumns != null)
			{
				tableColumns.Write(sw, "tableColumns");
			}
			if (tableStyleInfo != null)
			{
				tableStyleInfo.Write(sw, "tableStyleInfo");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write("</table>");
		}
	}
}
