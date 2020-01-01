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
	public class CT_PivotAreaReferences
	{
		private List<CT_PivotAreaReference> referenceField;

		private uint countField;

		private bool countFieldSpecified;

		public List<CT_PivotAreaReference> reference
		{
			get
			{
				return referenceField;
			}
			set
			{
				referenceField = value;
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

		public static CT_PivotAreaReferences Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PivotAreaReferences cT_PivotAreaReferences = new CT_PivotAreaReferences();
			cT_PivotAreaReferences.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_PivotAreaReferences.reference = new List<CT_PivotAreaReference>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "reference")
				{
					cT_PivotAreaReferences.reference.Add(CT_PivotAreaReference.Parse(childNode, namespaceManager));
				}
			}
			return cT_PivotAreaReferences;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (reference != null)
			{
				foreach (CT_PivotAreaReference item in reference)
				{
					item.Write(sw, "reference");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_PivotAreaReferences()
		{
			referenceField = new List<CT_PivotAreaReference>();
		}
	}
}
