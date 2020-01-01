using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CustomSheetViews
	{
		private List<CT_CustomSheetView> customSheetViewField;

		public List<CT_CustomSheetView> customSheetView
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

		public static CT_CustomSheetViews Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomSheetViews cT_CustomSheetViews = new CT_CustomSheetViews();
			cT_CustomSheetViews.customSheetView = new List<CT_CustomSheetView>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customSheetView")
				{
					cT_CustomSheetViews.customSheetView.Add(CT_CustomSheetView.Parse(childNode, namespaceManager));
				}
			}
			return cT_CustomSheetViews;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (customSheetView != null)
			{
				foreach (CT_CustomSheetView item in customSheetView)
				{
					item.Write(sw, "customSheetView");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
