using NPOI.OpenXml4Net.Util;
using NPOI.OpenXmlFormats.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Spreadsheet
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:excel")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:excel", IsNullable = true)]
	public class CT_ClientData
	{
		private ST_ObjectType objectTypeField;

		private string anchorField;

		private ST_TrueFalseBlank autoFillField;

		private bool autoFillFieldSpecified;

		private ST_TrueFalseBlank visibleField;

		private bool visibleFieldSpecified;

		private ST_TrueFalseBlank moveWithCellsField;

		private bool moveWithCellsFieldSpecified;

		private ST_TrueFalseBlank sizeWithCellsField;

		private bool sizeWithCellsFieldSpecified;

		private List<int> columnField;

		private List<int> rowField;

		[XmlElement(ElementName = "Anchor")]
		public string anchor
		{
			get
			{
				return anchorField;
			}
			set
			{
				anchorField = value;
			}
		}

		[DefaultValue(ST_TrueFalseBlank.NONE)]
		[XmlElement(ElementName = "AutoFill")]
		public ST_TrueFalseBlank autoFill
		{
			get
			{
				return autoFillField;
			}
			set
			{
				autoFillField = value;
			}
		}

		[XmlIgnore]
		public bool autoFillSpecified
		{
			get
			{
				return autoFillFieldSpecified;
			}
			set
			{
				autoFillFieldSpecified = value;
			}
		}

		[XmlElement(ElementName = "Visible")]
		[DefaultValue(ST_TrueFalseBlank.NONE)]
		public ST_TrueFalseBlank visible
		{
			get
			{
				return visibleField;
			}
			set
			{
				visibleField = value;
			}
		}

		[XmlIgnore]
		public bool visibleSpecified
		{
			get
			{
				return visibleFieldSpecified;
			}
			set
			{
				visibleFieldSpecified = value;
			}
		}

		[XmlElement(ElementName = "MoveWithCells")]
		[DefaultValue(ST_TrueFalseBlank.NONE)]
		public ST_TrueFalseBlank moveWithCells
		{
			get
			{
				return moveWithCellsField;
			}
			set
			{
				moveWithCellsField = value;
			}
		}

		[XmlIgnore]
		public bool moveWithCellsSpecified
		{
			get
			{
				return moveWithCellsFieldSpecified;
			}
			set
			{
				moveWithCellsFieldSpecified = value;
			}
		}

		[XmlElement(ElementName = "SizeWithCells")]
		[DefaultValue(ST_TrueFalseBlank.NONE)]
		public ST_TrueFalseBlank sizeWithCells
		{
			get
			{
				return sizeWithCellsField;
			}
			set
			{
				sizeWithCellsField = value;
			}
		}

		[XmlIgnore]
		public bool sizeWithCellsSpecified
		{
			get
			{
				return sizeWithCellsFieldSpecified;
			}
			set
			{
				sizeWithCellsFieldSpecified = value;
			}
		}

		[XmlElement(ElementName = "Column")]
		public List<int> column
		{
			get
			{
				return columnField;
			}
			set
			{
				columnField = value;
			}
		}

		[XmlElement(ElementName = "Row")]
		public List<int> row
		{
			get
			{
				return rowField;
			}
			set
			{
				rowField = value;
			}
		}

		[XmlAttribute]
		public ST_ObjectType ObjectType
		{
			get
			{
				return objectTypeField;
			}
			set
			{
				objectTypeField = value;
			}
		}

		public CT_ClientData()
		{
			rowField = new List<int>();
			columnField = new List<int>();
		}

		public static CT_ClientData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ClientData cT_ClientData = new CT_ClientData();
			if (node.Attributes["ObjectType"] != null)
			{
				cT_ClientData.ObjectType = (ST_ObjectType)Enum.Parse(typeof(ST_ObjectType), node.Attributes["ObjectType"].Value);
			}
			cT_ClientData.column = new List<int>();
			cT_ClientData.row = new List<int>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "Anchor")
				{
					cT_ClientData.anchor = childNode.InnerText;
				}
				else if (childNode.LocalName == "AutoFill")
				{
					cT_ClientData.autoFill = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalseBlank(childNode.InnerText);
				}
				else if (childNode.LocalName == "Visible")
				{
					cT_ClientData.visible = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalseBlank(childNode.InnerText);
				}
				else if (childNode.LocalName == "MoveWithCells")
				{
					cT_ClientData.moveWithCells = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalseBlank(childNode.InnerText);
				}
				else if (childNode.LocalName == "SizeWithCells")
				{
					cT_ClientData.sizeWithCells = NPOI.OpenXmlFormats.Util.XmlHelper.ReadTrueFalseBlank(childNode.InnerText);
				}
				else if (childNode.LocalName == "Column")
				{
					cT_ClientData.column.Add(int.Parse(childNode.InnerText));
				}
				else if (childNode.LocalName == "Row")
				{
					cT_ClientData.row.Add(int.Parse(childNode.InnerText));
				}
			}
			return cT_ClientData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<x:{0}", nodeName));
			NPOI.OpenXml4Net.Util.XmlHelper.WriteAttribute(sw, "ObjectType", ObjectType.ToString());
			sw.Write(">");
			if (moveWithCells == ST_TrueFalseBlank.t || moveWithCells == ST_TrueFalseBlank.@true)
			{
				sw.Write(string.Format("<x:MoveWithCells/>", moveWithCells));
			}
			if (sizeWithCells == ST_TrueFalseBlank.t || sizeWithCells == ST_TrueFalseBlank.@true)
			{
				sw.Write(string.Format("<x:SizeWithCells/>", sizeWithCells));
			}
			if (anchor != null)
			{
				sw.Write(string.Format("<x:Anchor>{0}</x:Anchor>", anchor));
			}
			if (autoFill != 0)
			{
				sw.Write(string.Format("<x:AutoFill>{0}</x:AutoFill>", autoFill));
			}
			if (visible != 0)
			{
				sw.Write(string.Format("<x:Visible>{0}</x:Visible>", visible));
			}
			if (row != null)
			{
				foreach (int item in row)
				{
					sw.Write(string.Format("<x:Row>{0}</x:Row>", item));
				}
			}
			if (column != null)
			{
				foreach (int item2 in column)
				{
					sw.Write(string.Format("<x:Column>{0}</x:Column>", item2));
				}
			}
			sw.Write(string.Format("</x:{0}>", nodeName));
		}

		public void AddNewRow(int rowNum)
		{
			if (rowField != null)
			{
				rowField.Add(rowNum);
			}
		}

		public void AddNewColumn(int columnNum)
		{
			if (columnField != null)
			{
				columnField.Add(columnNum);
			}
		}

		public void AddNewMoveWithCells()
		{
			moveWithCellsField = ST_TrueFalseBlank.t;
			moveWithCellsFieldSpecified = true;
		}

		public void AddNewSizeWithCells()
		{
			sizeWithCellsField = ST_TrueFalseBlank.t;
			sizeWithCellsFieldSpecified = true;
		}

		public void AddNewAnchor(string name)
		{
			anchorField = name;
		}

		public void AddNewAutoFill(ST_TrueFalseBlank value)
		{
			autoFillField = value;
			autoFillFieldSpecified = true;
		}

		public int SizeOfMoveWithCellsArray()
		{
			if (!moveWithCellsSpecified)
			{
				return 0;
			}
			return 1;
		}

		public int SizeOfSizeWithCellsArray()
		{
			if (!sizeWithCellsFieldSpecified)
			{
				return 0;
			}
			return 1;
		}

		public int GetColumnArray(int index)
		{
			return columnField[index];
		}

		public void SetColumnArray(int index, int value)
		{
			columnField[index] = value;
		}

		public void SetRowArray(int index, int value)
		{
			rowField[index] = value;
		}

		public void SetAnchorArray(int index, string value)
		{
			AddNewAnchor(value);
		}

		public int GetRowArray(int index)
		{
			return rowField[index];
		}
	}
}
