using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_PivotCaches
	{
		private List<CT_PivotCache> pivotCacheField;

		[XmlElement]
		public List<CT_PivotCache> pivotCache
		{
			get
			{
				return pivotCacheField;
			}
			set
			{
				pivotCacheField = value;
			}
		}

		public static CT_PivotCaches Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PivotCaches cT_PivotCaches = new CT_PivotCaches();
			cT_PivotCaches.pivotCache = new List<CT_PivotCache>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pivotCache")
				{
					cT_PivotCaches.pivotCache.Add(CT_PivotCache.Parse(childNode, namespaceManager));
				}
			}
			return cT_PivotCaches;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (pivotCache != null)
			{
				foreach (CT_PivotCache item in pivotCache)
				{
					item.Write(sw, "pivotCache");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
