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
	public class CT_NumFmts
	{
		private List<CT_NumFmt> numFmtField;

		private uint countField;

		private bool countFieldSpecified;

		[XmlElement]
		public List<CT_NumFmt> numFmt
		{
			get
			{
				return numFmtField;
			}
			set
			{
				numFmtField = value;
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

		public static CT_NumFmts Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_NumFmts cT_NumFmts = new CT_NumFmts();
			cT_NumFmts.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_NumFmts.numFmt = new List<CT_NumFmt>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "numFmt")
				{
					cT_NumFmts.numFmt.Add(CT_NumFmt.Parse(childNode, namespaceManager));
				}
			}
			return cT_NumFmts;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", (double)count, true);
			sw.Write(">");
			if (numFmt != null)
			{
				foreach (CT_NumFmt item in numFmt)
				{
					item.Write(sw, "numFmt");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_NumFmt AddNewNumFmt()
		{
			if (numFmtField == null)
			{
				numFmtField = new List<CT_NumFmt>();
			}
			CT_NumFmt cT_NumFmt = new CT_NumFmt();
			numFmtField.Add(cT_NumFmt);
			return cT_NumFmt;
		}
	}
}
