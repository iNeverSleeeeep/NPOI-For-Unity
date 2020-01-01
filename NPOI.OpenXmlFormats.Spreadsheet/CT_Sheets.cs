using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot("sheets", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = false)]
	[DesignerCategory("code")]
	public class CT_Sheets
	{
		private List<CT_Sheet> sheetField;

		[XmlElement("sheet")]
		public List<CT_Sheet> sheet
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

		public CT_Sheets()
		{
			sheetField = new List<CT_Sheet>();
		}

		public static CT_Sheets Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Sheets cT_Sheets = new CT_Sheets();
			cT_Sheets.sheet = new List<CT_Sheet>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sheet")
				{
					cT_Sheets.sheet.Add(CT_Sheet.Parse(childNode, namespaceManager));
				}
			}
			return cT_Sheets;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (sheet != null)
			{
				foreach (CT_Sheet item in sheet)
				{
					item.Write(sw, "sheet");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_Sheet AddNewSheet()
		{
			if (sheetField == null)
			{
				sheetField = new List<CT_Sheet>();
			}
			CT_Sheet cT_Sheet = new CT_Sheet();
			sheetField.Add(cT_Sheet);
			return cT_Sheet;
		}

		public void RemoveSheet(int index)
		{
			if (sheetField != null)
			{
				sheetField.RemoveAt(index);
			}
		}

		public CT_Sheet InsertNewSheet(int index)
		{
			if (sheetField == null)
			{
				sheetField = new List<CT_Sheet>();
			}
			CT_Sheet cT_Sheet = new CT_Sheet();
			sheetField.Insert(index, cT_Sheet);
			return cT_Sheet;
		}

		public CT_Sheet GetSheetArray(int index)
		{
			if (sheetField == null)
			{
				return null;
			}
			return sheetField[index];
		}
	}
}
