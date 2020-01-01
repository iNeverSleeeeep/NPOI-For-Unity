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
	public class CT_CustomWorkbookView
	{
		private CT_ExtensionList extLstField;

		private string nameField;

		private string guidField;

		private bool autoUpdateField;

		private uint mergeIntervalField;

		private bool mergeIntervalFieldSpecified;

		private bool changesSavedWinField;

		private bool onlySyncField;

		private bool personalViewField;

		private bool includePrintSettingsField;

		private bool includeHiddenRowColField;

		private bool maximizedField;

		private bool minimizedField;

		private bool showHorizontalScrollField;

		private bool showVerticalScrollField;

		private bool showSheetTabsField;

		private int xWindowField;

		private int yWindowField;

		private uint windowWidthField;

		private uint windowHeightField;

		private uint tabRatioField;

		private uint activeSheetIdField;

		private bool showFormulaBarField;

		private bool showStatusbarField;

		private ST_Comments showCommentsField;

		private ST_Objects showObjectsField;

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

		[XmlAttribute]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlAttribute]
		public string guid
		{
			get
			{
				return guidField;
			}
			set
			{
				guidField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool autoUpdate
		{
			get
			{
				return autoUpdateField;
			}
			set
			{
				autoUpdateField = value;
			}
		}

		[XmlAttribute]
		public uint mergeInterval
		{
			get
			{
				return mergeIntervalField;
			}
			set
			{
				mergeIntervalField = value;
			}
		}

		[XmlIgnore]
		public bool mergeIntervalSpecified
		{
			get
			{
				return mergeIntervalFieldSpecified;
			}
			set
			{
				mergeIntervalFieldSpecified = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool changesSavedWin
		{
			get
			{
				return changesSavedWinField;
			}
			set
			{
				changesSavedWinField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool onlySync
		{
			get
			{
				return onlySyncField;
			}
			set
			{
				onlySyncField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool personalView
		{
			get
			{
				return personalViewField;
			}
			set
			{
				personalViewField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool includePrintSettings
		{
			get
			{
				return includePrintSettingsField;
			}
			set
			{
				includePrintSettingsField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool includeHiddenRowCol
		{
			get
			{
				return includeHiddenRowColField;
			}
			set
			{
				includeHiddenRowColField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool maximized
		{
			get
			{
				return maximizedField;
			}
			set
			{
				maximizedField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
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

		[DefaultValue(true)]
		[XmlAttribute]
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

		[DefaultValue(0)]
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
			}
		}

		[DefaultValue(0)]
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
			}
		}

		[XmlAttribute]
		[DefaultValue(typeof(uint), "600")]
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

		[XmlAttribute]
		public uint activeSheetId
		{
			get
			{
				return activeSheetIdField;
			}
			set
			{
				activeSheetIdField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool showFormulaBar
		{
			get
			{
				return showFormulaBarField;
			}
			set
			{
				showFormulaBarField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool showStatusbar
		{
			get
			{
				return showStatusbarField;
			}
			set
			{
				showStatusbarField = value;
			}
		}

		[DefaultValue(ST_Comments.commIndicator)]
		[XmlAttribute]
		public ST_Comments showComments
		{
			get
			{
				return showCommentsField;
			}
			set
			{
				showCommentsField = value;
			}
		}

		[DefaultValue(ST_Objects.all)]
		[XmlAttribute]
		public ST_Objects showObjects
		{
			get
			{
				return showObjectsField;
			}
			set
			{
				showObjectsField = value;
			}
		}

		public CT_CustomWorkbookView()
		{
			autoUpdateField = false;
			changesSavedWinField = false;
			onlySyncField = false;
			personalViewField = false;
			includePrintSettingsField = true;
			includeHiddenRowColField = true;
			maximizedField = false;
			minimizedField = false;
			showHorizontalScrollField = true;
			showVerticalScrollField = true;
			showSheetTabsField = true;
			xWindowField = 0;
			yWindowField = 0;
			tabRatioField = 600u;
			showFormulaBarField = true;
			showStatusbarField = true;
			showCommentsField = ST_Comments.commIndicator;
			showObjectsField = ST_Objects.all;
		}

		public static CT_CustomWorkbookView Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomWorkbookView cT_CustomWorkbookView = new CT_CustomWorkbookView();
			cT_CustomWorkbookView.name = XmlHelper.ReadString(node.Attributes["name"]);
			cT_CustomWorkbookView.guid = XmlHelper.ReadString(node.Attributes["guid"]);
			cT_CustomWorkbookView.autoUpdate = XmlHelper.ReadBool(node.Attributes["autoUpdate"]);
			cT_CustomWorkbookView.mergeInterval = XmlHelper.ReadUInt(node.Attributes["mergeInterval"]);
			cT_CustomWorkbookView.mergeIntervalSpecified = (node.Attributes["mergeInterval"] != null);
			cT_CustomWorkbookView.changesSavedWin = XmlHelper.ReadBool(node.Attributes["changesSavedWin"]);
			cT_CustomWorkbookView.onlySync = XmlHelper.ReadBool(node.Attributes["onlySync"]);
			cT_CustomWorkbookView.personalView = XmlHelper.ReadBool(node.Attributes["personalView"]);
			cT_CustomWorkbookView.includePrintSettings = XmlHelper.ReadBool(node.Attributes["includePrintSettings"]);
			cT_CustomWorkbookView.includeHiddenRowCol = XmlHelper.ReadBool(node.Attributes["includeHiddenRowCol"]);
			cT_CustomWorkbookView.maximized = XmlHelper.ReadBool(node.Attributes["maximized"]);
			cT_CustomWorkbookView.minimized = XmlHelper.ReadBool(node.Attributes["minimized"]);
			cT_CustomWorkbookView.showHorizontalScroll = XmlHelper.ReadBool(node.Attributes["showHorizontalScroll"]);
			cT_CustomWorkbookView.showVerticalScroll = XmlHelper.ReadBool(node.Attributes["showVerticalScroll"]);
			cT_CustomWorkbookView.showSheetTabs = XmlHelper.ReadBool(node.Attributes["showSheetTabs"]);
			cT_CustomWorkbookView.xWindow = XmlHelper.ReadInt(node.Attributes["xWindow"]);
			cT_CustomWorkbookView.yWindow = XmlHelper.ReadInt(node.Attributes["yWindow"]);
			cT_CustomWorkbookView.windowWidth = XmlHelper.ReadUInt(node.Attributes["windowWidth"]);
			cT_CustomWorkbookView.windowHeight = XmlHelper.ReadUInt(node.Attributes["windowHeight"]);
			cT_CustomWorkbookView.tabRatio = XmlHelper.ReadUInt(node.Attributes["tabRatio"]);
			cT_CustomWorkbookView.activeSheetId = XmlHelper.ReadUInt(node.Attributes["activeSheetId"]);
			cT_CustomWorkbookView.showFormulaBar = XmlHelper.ReadBool(node.Attributes["showFormulaBar"]);
			cT_CustomWorkbookView.showStatusbar = XmlHelper.ReadBool(node.Attributes["showStatusbar"]);
			if (node.Attributes["showComments"] != null)
			{
				cT_CustomWorkbookView.showComments = (ST_Comments)Enum.Parse(typeof(ST_Comments), node.Attributes["showComments"].Value);
			}
			if (node.Attributes["showObjects"] != null)
			{
				cT_CustomWorkbookView.showObjects = (ST_Objects)Enum.Parse(typeof(ST_Objects), node.Attributes["showObjects"].Value);
			}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "extLst")
				{
					cT_CustomWorkbookView.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_CustomWorkbookView;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "name", name);
			XmlHelper.WriteAttribute(sw, "guid", guid);
			XmlHelper.WriteAttribute(sw, "autoUpdate", autoUpdate);
			XmlHelper.WriteAttribute(sw, "mergeInterval", mergeInterval);
			XmlHelper.WriteAttribute(sw, "changesSavedWin", changesSavedWin);
			XmlHelper.WriteAttribute(sw, "onlySync", onlySync);
			XmlHelper.WriteAttribute(sw, "personalView", personalView);
			XmlHelper.WriteAttribute(sw, "includePrintSettings", includePrintSettings);
			XmlHelper.WriteAttribute(sw, "includeHiddenRowCol", includeHiddenRowCol);
			XmlHelper.WriteAttribute(sw, "maximized", maximized);
			XmlHelper.WriteAttribute(sw, "minimized", minimized);
			XmlHelper.WriteAttribute(sw, "showHorizontalScroll", showHorizontalScroll);
			XmlHelper.WriteAttribute(sw, "showVerticalScroll", showVerticalScroll);
			XmlHelper.WriteAttribute(sw, "showSheetTabs", showSheetTabs);
			XmlHelper.WriteAttribute(sw, "xWindow", xWindow);
			XmlHelper.WriteAttribute(sw, "yWindow", yWindow);
			XmlHelper.WriteAttribute(sw, "windowWidth", windowWidth);
			XmlHelper.WriteAttribute(sw, "windowHeight", windowHeight);
			XmlHelper.WriteAttribute(sw, "tabRatio", tabRatio);
			XmlHelper.WriteAttribute(sw, "activeSheetId", activeSheetId);
			XmlHelper.WriteAttribute(sw, "showFormulaBar", showFormulaBar);
			XmlHelper.WriteAttribute(sw, "showStatusbar", showStatusbar);
			XmlHelper.WriteAttribute(sw, "showComments", showComments.ToString());
			XmlHelper.WriteAttribute(sw, "showObjects", showObjects.ToString());
			sw.Write(">");
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
