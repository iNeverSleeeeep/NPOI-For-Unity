using NPOI.OpenXml4Net.Util;
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
	public class CT_PageBreak
	{
		private List<CT_Break> brkField;

		private uint countField;

		private uint manualBreakCountField;

		public List<CT_Break> brk
		{
			get
			{
				return brkField;
			}
			set
			{
				brkField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
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

		[DefaultValue(typeof(uint), "0")]
		public uint manualBreakCount
		{
			get
			{
				return manualBreakCountField;
			}
			set
			{
				manualBreakCountField = value;
			}
		}

		public CT_PageBreak()
		{
			brkField = new List<CT_Break>();
			countField = 0u;
			manualBreakCountField = 0u;
		}

		public int sizeOfBrkArray()
		{
			return brkField.Count;
		}

		public CT_Break AddNewBrk()
		{
			CT_Break cT_Break = new CT_Break();
			brkField.Add(cT_Break);
			return cT_Break;
		}

		public void RemoveBrk(int index)
		{
			brkField.RemoveAt(index);
		}

		public static CT_PageBreak Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PageBreak cT_PageBreak = new CT_PageBreak();
			cT_PageBreak.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_PageBreak.manualBreakCount = XmlHelper.ReadUInt(node.Attributes["manualBreakCount"]);
			cT_PageBreak.brk = new List<CT_Break>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "brk")
				{
					cT_PageBreak.brk.Add(CT_Break.Parse(childNode, namespaceManager));
				}
			}
			return cT_PageBreak;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			XmlHelper.WriteAttribute(sw, "manualBreakCount", manualBreakCount);
			sw.Write(">");
			if (brk != null)
			{
				foreach (CT_Break item in brk)
				{
					item.Write(sw, "brk");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
