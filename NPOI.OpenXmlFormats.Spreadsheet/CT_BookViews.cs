using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[DesignerCategory("code")]
	public class CT_BookViews
	{
		private List<CT_BookView> workbookViewField;

		[XmlElement("workbookView")]
		public List<CT_BookView> workbookView
		{
			get
			{
				return workbookViewField;
			}
			set
			{
				workbookViewField = value;
			}
		}

		public static CT_BookViews Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BookViews cT_BookViews = new CT_BookViews();
			cT_BookViews.workbookView = new List<CT_BookView>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "workbookView")
				{
					cT_BookViews.workbookView.Add(CT_BookView.Parse(childNode, namespaceManager));
				}
			}
			return cT_BookViews;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			sw.Write(">");
			if (workbookView != null)
			{
				foreach (CT_BookView item in workbookView)
				{
					item.Write(sw, "workbookView");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_BookView AddNewWorkbookView()
		{
			if (workbookViewField == null)
			{
				workbookViewField = new List<CT_BookView>();
			}
			CT_BookView cT_BookView = new CT_BookView();
			workbookViewField.Add(cT_BookView);
			return cT_BookView;
		}

		public CT_BookView GetWorkbookViewArray(int index)
		{
			if (workbookViewField == null)
			{
				return null;
			}
			return workbookViewField[index];
		}
	}
}
