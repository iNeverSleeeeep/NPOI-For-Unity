using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CellWatches
	{
		private List<CT_CellWatch> cellWatchField;

		public List<CT_CellWatch> cellWatch
		{
			get
			{
				return cellWatchField;
			}
			set
			{
				cellWatchField = value;
			}
		}

		public static CT_CellWatches Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CellWatches cT_CellWatches = new CT_CellWatches();
			cT_CellWatches.cellWatch = new List<CT_CellWatch>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "cellWatch")
				{
					cT_CellWatches.cellWatch.Add(CT_CellWatch.Parse(childNode, namespaceManager));
				}
			}
			return cT_CellWatches;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (cellWatch != null)
			{
				foreach (CT_CellWatch item in cellWatch)
				{
					item.Write(sw, "cellWatch");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
