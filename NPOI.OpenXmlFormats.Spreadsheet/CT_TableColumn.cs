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
	public class CT_TableColumn
	{
		private CT_TableFormula calculatedColumnFormulaField;

		private CT_TableFormula totalsRowFormulaField;

		private CT_XmlColumnPr xmlColumnPrField;

		private CT_ExtensionList extLstField;

		private uint idField;

		private string uniqueNameField;

		private string nameField;

		private ST_TotalsRowFunction totalsRowFunctionField;

		private string totalsRowLabelField;

		private uint queryTableFieldIdField;

		private bool queryTableFieldIdFieldSpecified;

		private uint headerRowDxfIdField;

		private bool headerRowDxfIdFieldSpecified;

		private uint dataDxfIdField;

		private bool dataDxfIdFieldSpecified;

		private uint totalsRowDxfIdField;

		private bool totalsRowDxfIdFieldSpecified;

		private string headerRowCellStyleField;

		private string dataCellStyleField;

		private string totalsRowCellStyleField;

		[XmlElement]
		public CT_TableFormula calculatedColumnFormula
		{
			get
			{
				return calculatedColumnFormulaField;
			}
			set
			{
				calculatedColumnFormulaField = value;
			}
		}

		[XmlElement]
		public CT_TableFormula totalsRowFormula
		{
			get
			{
				return totalsRowFormulaField;
			}
			set
			{
				totalsRowFormulaField = value;
			}
		}

		public CT_XmlColumnPr xmlColumnPr
		{
			get
			{
				return xmlColumnPrField;
			}
			set
			{
				xmlColumnPrField = value;
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
		public string uniqueName
		{
			get
			{
				return uniqueNameField;
			}
			set
			{
				uniqueNameField = value;
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
		[DefaultValue(ST_TotalsRowFunction.none)]
		public ST_TotalsRowFunction totalsRowFunction
		{
			get
			{
				return totalsRowFunctionField;
			}
			set
			{
				totalsRowFunctionField = value;
			}
		}

		[XmlAttribute]
		public string totalsRowLabel
		{
			get
			{
				return totalsRowLabelField;
			}
			set
			{
				totalsRowLabelField = value;
			}
		}

		[XmlAttribute]
		public uint queryTableFieldId
		{
			get
			{
				return queryTableFieldIdField;
			}
			set
			{
				queryTableFieldIdField = value;
			}
		}

		[XmlIgnore]
		public bool queryTableFieldIdSpecified
		{
			get
			{
				return queryTableFieldIdFieldSpecified;
			}
			set
			{
				queryTableFieldIdFieldSpecified = value;
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

		public CT_TableColumn()
		{
			totalsRowFunctionField = ST_TotalsRowFunction.none;
		}

		public static CT_TableColumn Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_TableColumn cT_TableColumn = new CT_TableColumn();
			if (node.Attributes["id"] != null)
			{
				cT_TableColumn.id = XmlHelper.ReadUInt(node.Attributes["id"]);
			}
			cT_TableColumn.uniqueName = XmlHelper.ReadString(node.Attributes["uniqueName"]);
			cT_TableColumn.name = XmlHelper.ReadString(node.Attributes["name"]);
			if (node.Attributes["totalsRowFunction"] != null)
			{
				cT_TableColumn.totalsRowFunction = (ST_TotalsRowFunction)Enum.Parse(typeof(ST_TotalsRowFunction), node.Attributes["totalsRowFunction"].Value);
			}
			cT_TableColumn.totalsRowLabel = XmlHelper.ReadString(node.Attributes["totalsRowLabel"]);
			if (node.Attributes["queryTableFieldId"] != null)
			{
				cT_TableColumn.queryTableFieldId = XmlHelper.ReadUInt(node.Attributes["queryTableFieldId"]);
			}
			if (node.Attributes["headerRowDxfId"] != null)
			{
				cT_TableColumn.headerRowDxfId = XmlHelper.ReadUInt(node.Attributes["headerRowDxfId"]);
			}
			if (node.Attributes["dataDxfId"] != null)
			{
				cT_TableColumn.dataDxfId = XmlHelper.ReadUInt(node.Attributes["dataDxfId"]);
			}
			if (node.Attributes["totalsRowDxfId"] != null)
			{
				cT_TableColumn.totalsRowDxfId = XmlHelper.ReadUInt(node.Attributes["totalsRowDxfId"]);
			}
			cT_TableColumn.headerRowCellStyle = XmlHelper.ReadString(node.Attributes["headerRowCellStyle"]);
			cT_TableColumn.dataCellStyle = XmlHelper.ReadString(node.Attributes["dataCellStyle"]);
			cT_TableColumn.totalsRowCellStyle = XmlHelper.ReadString(node.Attributes["totalsRowCellStyle"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "calculatedColumnFormula")
				{
					cT_TableColumn.calculatedColumnFormula = CT_TableFormula.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "totalsRowFormula")
				{
					cT_TableColumn.totalsRowFormula = CT_TableFormula.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "xmlColumnPr")
				{
					cT_TableColumn.xmlColumnPr = CT_XmlColumnPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_TableColumn.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_TableColumn;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "id", id);
			XmlHelper.WriteAttribute(sw, "uniqueName", uniqueName);
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "totalsRowFunction", totalsRowFunction.ToString());
			XmlHelper.WriteAttribute(sw, "totalsRowLabel", totalsRowLabel);
			XmlHelper.WriteAttribute(sw, "queryTableFieldId", queryTableFieldId);
			XmlHelper.WriteAttribute(sw, "headerRowDxfId", headerRowDxfId);
			XmlHelper.WriteAttribute(sw, "dataDxfId", dataDxfId);
			XmlHelper.WriteAttribute(sw, "totalsRowDxfId", totalsRowDxfId);
			XmlHelper.WriteAttribute(sw, "headerRowCellStyle", headerRowCellStyle);
			XmlHelper.WriteAttribute(sw, "dataCellStyle", dataCellStyle);
			XmlHelper.WriteAttribute(sw, "totalsRowCellStyle", totalsRowCellStyle);
			sw.Write(">");
			if (calculatedColumnFormula != null)
			{
				calculatedColumnFormula.Write(sw, "calculatedColumnFormula");
			}
			if (totalsRowFormula != null)
			{
				totalsRowFormula.Write(sw, "totalsRowFormula");
			}
			if (xmlColumnPr != null)
			{
				xmlColumnPr.Write(sw, "xmlColumnPr");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
