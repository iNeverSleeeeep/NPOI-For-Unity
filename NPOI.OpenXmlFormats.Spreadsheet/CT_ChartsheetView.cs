using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_ChartsheetView
	{
		private CT_ExtensionList extLstField;

		private bool tabSelectedField;

		private uint zoomScaleField;

		private uint workbookViewIdField;

		private bool zoomToFitField;

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

		[DefaultValue(false)]
		public bool tabSelected
		{
			get
			{
				return tabSelectedField;
			}
			set
			{
				tabSelectedField = value;
			}
		}

		[DefaultValue(typeof(uint), "100")]
		public uint zoomScale
		{
			get
			{
				return zoomScaleField;
			}
			set
			{
				zoomScaleField = value;
			}
		}

		public uint workbookViewId
		{
			get
			{
				return workbookViewIdField;
			}
			set
			{
				workbookViewIdField = value;
			}
		}

		[DefaultValue(false)]
		public bool zoomToFit
		{
			get
			{
				return zoomToFitField;
			}
			set
			{
				zoomToFitField = value;
			}
		}

		public CT_ChartsheetView()
		{
			tabSelectedField = false;
			zoomScaleField = 100u;
			zoomToFitField = false;
		}

		public static CT_ChartsheetView Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_ChartsheetView cT_ChartsheetView = new CT_ChartsheetView();
			if (node.Attributes["tabSelected"] != null)
			{
				cT_ChartsheetView.tabSelected = XmlHelper.ReadBool(node.Attributes["tabSelected"]);
			}
			if (node.Attributes["zoomScale"] != null)
			{
				cT_ChartsheetView.zoomScale = XmlHelper.ReadUInt(node.Attributes["zoomScale"]);
			}
			if (node.Attributes["workbookViewId"] != null)
			{
				cT_ChartsheetView.workbookViewId = XmlHelper.ReadUInt(node.Attributes["workbookViewId"]);
			}
			if (node.Attributes["zoomToFit"] != null)
			{
				cT_ChartsheetView.zoomToFit = XmlHelper.ReadBool(node.Attributes["zoomToFit"]);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_ChartsheetView.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_ChartsheetView;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (tabSelected)
			{
				XmlHelper.WriteAttribute(sw, "tabSelected", tabSelected);
			}
			if (zoomScale != 100)
			{
				XmlHelper.WriteAttribute(sw, "zoomScale", zoomScale);
			}
			XmlHelper.WriteAttribute(sw, "workbookViewId", workbookViewId);
			if (zoomToFit)
			{
				XmlHelper.WriteAttribute(sw, "zoomToFit", zoomToFit);
			}
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
