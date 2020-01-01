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
	public class CT_DataRefs
	{
		private List<CT_DataRef> dataRefField;

		private uint countField;

		private bool countFieldSpecified;

		public List<CT_DataRef> dataRef
		{
			get
			{
				return dataRefField;
			}
			set
			{
				dataRefField = value;
			}
		}

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

		public CT_DataRefs()
		{
			dataRefField = new List<CT_DataRef>();
		}

		public static CT_DataRefs Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_DataRefs cT_DataRefs = new CT_DataRefs();
			cT_DataRefs.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			cT_DataRefs.dataRef = new List<CT_DataRef>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "dataRef")
				{
					cT_DataRefs.dataRef.Add(CT_DataRef.Parse(childNode, namespaceManager));
				}
			}
			return cT_DataRefs;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", count);
			sw.Write(">");
			if (dataRef != null)
			{
				foreach (CT_DataRef item in dataRef)
				{
					item.Write(sw, "dataRef");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
