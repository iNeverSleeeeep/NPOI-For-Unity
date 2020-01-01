using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_MergeCells
	{
		private List<CT_MergeCell> mergeCellField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_MergeCell> mergeCell
		{
			get
			{
				return mergeCellField;
			}
			set
			{
				mergeCellField = value;
			}
		}

		[XmlAttribute]
		public uint count
		{
			get
			{
				return countField;
			}
			set
			{
				countField = value;
			}
		}

		[XmlIgnore]
		public bool countSpecified
		{
			get
			{
				return countFieldSpecified;
			}
			set
			{
				countFieldSpecified = value;
			}
		}

		public static CT_MergeCells Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_MergeCells cT_MergeCells = new CT_MergeCells();
			cT_MergeCells.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_MergeCells.mergeCell = new List<CT_MergeCell>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "mergeCell")
				{
					cT_MergeCells.mergeCell.Add(CT_MergeCell.Parse(childNode, namespaceManager));
				}
			}
			return cT_MergeCells;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (mergeCell != null)
			{
				foreach (CT_MergeCell item in mergeCell)
				{
					item.Write(sw, "mergeCell");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_MergeCells()
		{
			mergeCellField = new List<CT_MergeCell>();
		}

		public CT_MergeCell GetMergeCellArray(int index)
		{
			return mergeCellField[index];
		}

		public void SetMergeCellArray(CT_MergeCell[] array)
		{
			mergeCell = new List<CT_MergeCell>(array);
		}

		public int sizeOfMergeCellArray()
		{
			return mergeCell.Count;
		}

		public CT_MergeCell AddNewMergeCell()
		{
			CT_MergeCell cT_MergeCell = new CT_MergeCell();
			mergeCell.Add(cT_MergeCell);
			return cT_MergeCell;
		}
	}
}
