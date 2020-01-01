using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ChartsheetViews
	{
		private List<CT_ChartsheetView> sheetViewField;

		private CT_ExtensionList extLstField;

		public List<CT_ChartsheetView> sheetView
		{
			get
			{
				return sheetViewField;
			}
			set
			{
				sheetViewField = value;
			}
		}

		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		public static CT_ChartsheetViews Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ChartsheetViews cT_ChartsheetViews = new CT_ChartsheetViews();
			cT_ChartsheetViews.sheetView = new List<CT_ChartsheetView>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_ChartsheetViews.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetView")
				{
					cT_ChartsheetViews.sheetView.Add(CT_ChartsheetView.Parse(childNode, namespaceManager));
				}
			}
			return cT_ChartsheetViews;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (sheetView != null)
			{
				foreach (CT_ChartsheetView item in sheetView)
				{
					item.Write(sw, "sheetView");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
