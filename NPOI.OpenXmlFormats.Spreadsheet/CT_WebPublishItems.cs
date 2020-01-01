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
	public class CT_WebPublishItems
	{
		private List<CT_WebPublishItem> webPublishItemField;

		private uint countField;

		private bool countFieldSpecified;

		public List<CT_WebPublishItem> webPublishItem
		{
			get
			{
				return webPublishItemField;
			}
			set
			{
				webPublishItemField = value;
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

		public static CT_WebPublishItems Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_WebPublishItems cT_WebPublishItems = new CT_WebPublishItems();
			if (node.Attributes["count"] != null)
			{
				cT_WebPublishItems.count = XmlHelper.ReadUInt(node.Attributes["count"]);
			}
			cT_WebPublishItems.webPublishItem = new List<CT_WebPublishItem>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "webPublishItem")
				{
					cT_WebPublishItems.webPublishItem.Add(CT_WebPublishItem.Parse(childNode, namespaceManager));
				}
			}
			return cT_WebPublishItems;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "count", (double)count, true);
			sw.Write(">");
			if (webPublishItem != null)
			{
				foreach (CT_WebPublishItem item in webPublishItem)
				{
					item.Write(sw, "webPublishItem");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
