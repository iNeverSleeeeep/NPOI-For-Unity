using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SheetData
	{
		private List<CT_Row> rowField;

		[XmlElement("row")]
		public List<CT_Row> row
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

		[XmlIgnore]
		public bool rowSpecified
		{
			get
			{
				return null != rowField;
			}
		}

		public static CT_SheetData Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SheetData cT_SheetData = new CT_SheetData();
			cT_SheetData.row = new List<CT_Row>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "row")
				{
					cT_SheetData.row.Add(CT_Row.Parse(childNode, namespaceManager));
				}
			}
			return cT_SheetData;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (row != null)
			{
				foreach (CT_Row item in row)
				{
					item.Write(sw, "row");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_Row AddNewRow()
		{
			if (rowField == null)
			{
				rowField = new List<CT_Row>();
			}
			CT_Row cT_Row = new CT_Row();
			rowField.Add(cT_Row);
			return cT_Row;
		}

		public CT_Row InsertNewRow(int index)
		{
			if (rowField == null)
			{
				rowField = new List<CT_Row>();
			}
			CT_Row cT_Row = new CT_Row();
			rowField.Insert(index, cT_Row);
			return cT_Row;
		}

		public void RemoveRow(int rowNum)
		{
			if (rowField != null)
			{
				CT_Row item = null;
				foreach (CT_Row item2 in rowField)
				{
					if (item2.r == rowNum)
					{
						item = item2;
						break;
					}
				}
				rowField.Remove(item);
			}
		}

		public int SizeOfRowArray()
		{
			if (rowField != null)
			{
				return rowField.Count;
			}
			return 0;
		}

		public CT_Row GetRowArray(int index)
		{
			if (rowField != null)
			{
				return rowField[index];
			}
			return null;
		}
	}
}
