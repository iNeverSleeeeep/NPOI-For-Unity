using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CustomChartsheetViews
	{
		private List<CT_CustomChartsheetView> customSheetViewField;

		public List<CT_CustomChartsheetView> customSheetView
		{
			get
			{
				return customSheetViewField;
			}
			set
			{
				customSheetViewField = value;
			}
		}

		public static CT_CustomChartsheetViews Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomChartsheetViews cT_CustomChartsheetViews = new CT_CustomChartsheetViews();
			cT_CustomChartsheetViews.customSheetView = new List<CT_CustomChartsheetView>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customSheetView")
				{
					cT_CustomChartsheetViews.customSheetView.Add(CT_CustomChartsheetView.Parse(childNode, namespaceManager));
				}
			}
			return cT_CustomChartsheetViews;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (customSheetView != null)
			{
				foreach (CT_CustomChartsheetView item in customSheetView)
				{
					item.Write(sw, "customSheetView");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
