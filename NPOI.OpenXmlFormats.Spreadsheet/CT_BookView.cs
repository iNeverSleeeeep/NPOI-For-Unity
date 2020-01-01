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
	public class CT_BookView
	{
		private CT_ExtensionList extLstField;

		private ST_Visibility visibilityField;

		private bool minimizedField;

		private bool showHorizontalScrollField;

		private bool showVerticalScrollField;

		private bool showSheetTabsField;

		private int xWindowField;

		private bool xWindowFieldSpecified;

		private int yWindowField;

		private bool yWindowFieldSpecified;

		private uint windowWidthField;

		private bool windowWidthFieldSpecified;

		private uint windowHeightField;

		private bool windowHeightFieldSpecified;

		private uint tabRatioField;

		private uint firstSheetField;

		private uint activeTabField;

		private bool autoFilterDateGroupingField;

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

		[XmlIgnore]
		public bool extLstSpecified
		{
			get
			{
				return null != extLstField;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_Visibility.visible)]
		public ST_Visibility visibility
		{
			get
			{
				return visibilityField;
			}
			set
			{
				visibilityField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool minimized
		{
			get
			{
				return minimizedField;
			}
			set
			{
				minimizedField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool showHorizontalScroll
		{
			get
			{
				return showHorizontalScrollField;
			}
			set
			{
				showHorizontalScrollField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool showVerticalScroll
		{
			get
			{
				return showVerticalScrollField;
			}
			set
			{
				showVerticalScrollField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool showSheetTabs
		{
			get
			{
				return showSheetTabsField;
			}
			set
			{
				showSheetTabsField = value;
			}
		}

		[XmlAttribute]
		public int xWindow
		{
			get
			{
				return xWindowField;
			}
			set
			{
				xWindowField = value;
				xWindowFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool xWindowSpecified
		{
			get
			{
				return xWindowFieldSpecified;
			}
			set
			{
				xWindowFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public int yWindow
		{
			get
			{
				return yWindowField;
			}
			set
			{
				yWindowField = value;
				yWindowFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool yWindowSpecified
		{
			get
			{
				return yWindowFieldSpecified;
			}
			set
			{
				yWindowFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint windowWidth
		{
			get
			{
				return windowWidthField;
			}
			set
			{
				windowWidthField = value;
				windowWidthFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool windowWidthSpecified
		{
			get
			{
				return windowWidthFieldSpecified;
			}
			set
			{
				windowWidthFieldSpecified = value;
			}
		}

		[XmlAttribute]
		public uint windowHeight
		{
			get
			{
				return windowHeightField;
			}
			set
			{
				windowHeightField = value;
				windowHeightFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool windowHeightSpecified
		{
			get
			{
				return windowHeightFieldSpecified;
			}
			set
			{
				windowHeightFieldSpecified = value;
			}
		}

		[DefaultValue(typeof(uint), "600")]
		[XmlAttribute]
		public uint tabRatio
		{
			get
			{
				return tabRatioField;
			}
			set
			{
				tabRatioField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint firstSheet
		{
			get
			{
				return firstSheetField;
			}
			set
			{
				firstSheetField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "0")]
		public uint activeTab
		{
			get
			{
				return activeTabField;
			}
			set
			{
				activeTabField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool autoFilterDateGrouping
		{
			get
			{
				return autoFilterDateGroupingField;
			}
			set
			{
				autoFilterDateGroupingField = value;
			}
		}

		public CT_BookView()
		{
			visibilityField = ST_Visibility.visible;
			minimizedField = false;
			showHorizontalScrollField = true;
			showVerticalScrollField = true;
			showSheetTabsField = true;
			tabRatioField = 600u;
			firstSheetField = 0u;
			activeTabField = 0u;
			autoFilterDateGroupingField = true;
		}

		public static CT_BookView Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_BookView cT_BookView = new CT_BookView();
			if (node.Attributes["visibility"] != null)
			{
				cT_BookView.visibility = (ST_Visibility)Enum.Parse(typeof(ST_Visibility), node.Attributes["visibility"].Value);
			}
			cT_BookView.minimized = XmlHelper.ReadBool(node.Attributes["minimized"]);
			cT_BookView.showHorizontalScroll = XmlHelper.ReadBool(node.Attributes["showHorizontalScroll"], true);
			cT_BookView.showVerticalScroll = XmlHelper.ReadBool(node.Attributes["showVerticalScroll"], true);
			cT_BookView.showSheetTabs = XmlHelper.ReadBool(node.Attributes["showSheetTabs"], true);
			cT_BookView.xWindow = XmlHelper.ReadInt(node.Attributes["xWindow"]);
			cT_BookView.yWindow = XmlHelper.ReadInt(node.Attributes["yWindow"]);
			cT_BookView.windowWidth = XmlHelper.ReadUInt(node.Attributes["windowWidth"]);
			cT_BookView.windowHeight = XmlHelper.ReadUInt(node.Attributes["windowHeight"]);
			cT_BookView.tabRatio = XmlHelper.ReadUInt(node.Attributes["tabRatio"]);
			cT_BookView.firstSheet = XmlHelper.ReadUInt(node.Attributes["firstSheet"]);
			cT_BookView.activeTab = XmlHelper.ReadUInt(node.Attributes["activeTab"]);
			cT_BookView.autoFilterDateGrouping = XmlHelper.ReadBool(node.Attributes["autoFilterDateGrouping"], true);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_BookView.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_BookView;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			if (visibility != 0)
			{
				XmlHelper.WriteAttribute(sw, "visibility", visibility.ToString());
			}
			XmlHelper.WriteAttribute(sw, "minimized", minimized, false);
			if (!showHorizontalScroll)
			{
				XmlHelper.WriteAttribute(sw, "showHorizontalScroll", showHorizontalScroll);
			}
			if (!showVerticalScroll)
			{
				XmlHelper.WriteAttribute(sw, "showVerticalScroll", showVerticalScroll);
			}
			if (!showSheetTabs)
			{
				XmlHelper.WriteAttribute(sw, "showSheetTabs", showSheetTabs);
			}
			XmlHelper.WriteAttribute(sw, "xWindow", xWindow);
			XmlHelper.WriteAttribute(sw, "yWindow", yWindow);
			XmlHelper.WriteAttribute(sw, "windowWidth", windowWidth);
			XmlHelper.WriteAttribute(sw, "windowHeight", windowHeight);
			XmlHelper.WriteAttribute(sw, "tabRatio", tabRatio);
			XmlHelper.WriteAttribute(sw, "firstSheet", firstSheet);
			XmlHelper.WriteAttribute(sw, "activeTab", activeTab);
			if (!autoFilterDateGrouping)
			{
				XmlHelper.WriteAttribute(sw, "autoFilterDateGrouping", autoFilterDateGrouping);
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
