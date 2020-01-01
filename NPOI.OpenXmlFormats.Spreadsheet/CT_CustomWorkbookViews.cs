using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CustomWorkbookViews
	{
		private List<CT_CustomWorkbookView> customWorkbookViewField;

		[XmlElement]
		public List<CT_CustomWorkbookView> customWorkbookView
		{
			get
			{
				return customWorkbookViewField;
			}
			set
			{
				customWorkbookViewField = value;
			}
		}

		public static CT_CustomWorkbookViews Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomWorkbookViews cT_CustomWorkbookViews = new CT_CustomWorkbookViews();
			cT_CustomWorkbookViews.customWorkbookView = new List<CT_CustomWorkbookView>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "customWorkbookView")
				{
					cT_CustomWorkbookViews.customWorkbookView.Add(CT_CustomWorkbookView.Parse(childNode, namespaceManager));
				}
			}
			return cT_CustomWorkbookViews;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (customWorkbookView != null)
			{
				foreach (CT_CustomWorkbookView item in customWorkbookView)
				{
					item.Write(sw, "customWorkbookView");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
