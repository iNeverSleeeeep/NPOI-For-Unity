using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Cols
	{
		private List<CT_Col> colField = new List<CT_Col>();

		[XmlElement]
		public List<CT_Col> col
		{
			get
			{
				return colField;
			}
			set
			{
				colField = value;
			}
		}

		public void SetColArray(List<CT_Col> array)
		{
			colField = array;
		}

		public CT_Col AddNewCol()
		{
			CT_Col cT_Col = new CT_Col();
			colField.Add(cT_Col);
			return cT_Col;
		}

		public CT_Col InsertNewCol(int index)
		{
			CT_Col cT_Col = new CT_Col();
			colField.Insert(index, cT_Col);
			return cT_Col;
		}

		public void RemoveCol(int index)
		{
			colField.RemoveAt(index);
		}

		public int sizeOfColArray()
		{
			return col.Count;
		}

		public CT_Col GetColArray(int index)
		{
			return colField[index];
		}

		public List<CT_Col> GetColArray()
		{
			return colField;
		}

		public static CT_Cols Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Cols cT_Cols = new CT_Cols();
			cT_Cols.col = new List<CT_Col>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "col")
				{
					cT_Cols.col.Add(CT_Col.Parse(childNode, namespaceManager));
				}
			}
			return cT_Cols;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (col != null)
			{
				foreach (CT_Col item in col)
				{
					item.Write(sw, "col");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
