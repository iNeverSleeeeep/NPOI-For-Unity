using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SheetViews
	{
		private List<CT_SheetView> sheetViewField;

		private CT_ExtensionList extLstField;

		[XmlElement]
		public List<CT_SheetView> sheetView
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

		[XmlElement]
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

		public static CT_SheetViews Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SheetViews cT_SheetViews = new CT_SheetViews();
			cT_SheetViews.sheetView = new List<CT_SheetView>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_SheetViews.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetView")
				{
					cT_SheetViews.sheetView.Add(CT_SheetView.Parse(childNode, namespaceManager));
				}
			}
			return cT_SheetViews;
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
				foreach (CT_SheetView item in sheetView)
				{
					item.Write(sw, "sheetView");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_SheetViews()
		{
			sheetViewField = new List<CT_SheetView>();
		}

		public CT_SheetView AddNewSheetView()
		{
			CT_SheetView cT_SheetView = new CT_SheetView();
			sheetViewField.Add(cT_SheetView);
			return cT_SheetView;
		}

		public CT_SheetView GetSheetViewArray(int index)
		{
			return sheetViewField[index];
		}

		public void SetSheetViewArray(int index, CT_SheetView array)
		{
			sheetViewField[index] = array;
		}

		public int sizeOfSheetViewArray()
		{
			return sheetViewField.Count;
		}
	}
}
