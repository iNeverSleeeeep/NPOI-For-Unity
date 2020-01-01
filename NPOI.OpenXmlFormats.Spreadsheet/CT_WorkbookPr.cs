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
	public class CT_WorkbookPr
	{
		private bool date1904Field;

		private bool date1904FieldSpecifiedField = true;

		private ST_Objects showObjectsField;

		private bool showBorderUnselectedTablesField;

		private bool filterPrivacyField;

		private bool promptedSolutionsField;

		private bool showInkAnnotationField;

		private bool backupFileField;

		private bool saveExternalLinkValuesField;

		private ST_UpdateLinks updateLinksField;

		private string codeNameField;

		private bool hidePivotFieldListField;

		private bool showPivotChartFilterField;

		private bool allowRefreshQueryField;

		private bool publishItemsField;

		private bool checkCompatibilityField;

		private bool autoCompressPicturesField;

		private bool refreshAllConnectionsField;

		private uint defaultThemeVersionField;

		private bool defaultThemeVersionFieldSpecified;

		[DefaultValue(false)]
		[XmlAttribute]
		public bool date1904
		{
			get
			{
				return date1904Field;
			}
			set
			{
				date1904Field = value;
			}
		}

		[XmlIgnore]
		public bool date1904Specified
		{
			get
			{
				return date1904FieldSpecifiedField;
			}
			set
			{
				date1904FieldSpecifiedField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_Objects.all)]
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

		[DefaultValue(true)]
		[XmlAttribute]
		public bool showBorderUnselectedTables
		{
			get
			{
				return showBorderUnselectedTablesField;
			}
			set
			{
				showBorderUnselectedTablesField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool filterPrivacy
		{
			get
			{
				return filterPrivacyField;
			}
			set
			{
				filterPrivacyField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool promptedSolutions
		{
			get
			{
				return promptedSolutionsField;
			}
			set
			{
				promptedSolutionsField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool showInkAnnotation
		{
			get
			{
				return showInkAnnotationField;
			}
			set
			{
				showInkAnnotationField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool backupFile
		{
			get
			{
				return backupFileField;
			}
			set
			{
				backupFileField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool saveExternalLinkValues
		{
			get
			{
				return saveExternalLinkValuesField;
			}
			set
			{
				saveExternalLinkValuesField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_UpdateLinks.userSet)]
		public ST_UpdateLinks updateLinks
		{
			get
			{
				return updateLinksField;
			}
			set
			{
				updateLinksField = value;
			}
		}

		[XmlAttribute]
		public string codeName
		{
			get
			{
				return codeNameField;
			}
			set
			{
				codeNameField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool hidePivotFieldList
		{
			get
			{
				return hidePivotFieldListField;
			}
			set
			{
				hidePivotFieldListField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool showPivotChartFilter
		{
			get
			{
				return showPivotChartFilterField;
			}
			set
			{
				showPivotChartFilterField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool allowRefreshQuery
		{
			get
			{
				return allowRefreshQueryField;
			}
			set
			{
				allowRefreshQueryField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool publishItems
		{
			get
			{
				return publishItemsField;
			}
			set
			{
				publishItemsField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool checkCompatibility
		{
			get
			{
				return checkCompatibilityField;
			}
			set
			{
				checkCompatibilityField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool autoCompressPictures
		{
			get
			{
				return autoCompressPicturesField;
			}
			set
			{
				autoCompressPicturesField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool refreshAllConnections
		{
			get
			{
				return refreshAllConnectionsField;
			}
			set
			{
				refreshAllConnectionsField = value;
			}
		}

		[XmlAttribute]
		public uint defaultThemeVersion
		{
			get
			{
				return defaultThemeVersionField;
			}
			set
			{
				defaultThemeVersionField = value;
			}
		}

		[XmlIgnore]
		public bool defaultThemeVersionSpecified
		{
			get
			{
				return defaultThemeVersionFieldSpecified;
			}
			set
			{
				defaultThemeVersionFieldSpecified = value;
			}
		}

		public bool IsSetDate1904()
		{
			return date1904FieldSpecifiedField;
		}

		public static CT_WorkbookPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_WorkbookPr cT_WorkbookPr = new CT_WorkbookPr();
			cT_WorkbookPr.date1904 = XmlHelper.ReadBool(node.Attributes["date1904"]);
			cT_WorkbookPr.date1904Specified = (node.Attributes["date1904"] != null);
			if (node.Attributes["showObjects"] != null)
			{
				cT_WorkbookPr.showObjects = (ST_Objects)Enum.Parse(typeof(ST_Objects), node.Attributes["showObjects"].Value);
			}
			cT_WorkbookPr.showBorderUnselectedTables = XmlHelper.ReadBool(node.Attributes["showBorderUnselectedTables"], true);
			cT_WorkbookPr.filterPrivacy = XmlHelper.ReadBool(node.Attributes["filterPrivacy"]);
			cT_WorkbookPr.promptedSolutions = XmlHelper.ReadBool(node.Attributes["promptedSolutions"]);
			cT_WorkbookPr.showInkAnnotation = XmlHelper.ReadBool(node.Attributes["showInkAnnotation"], true);
			cT_WorkbookPr.backupFile = XmlHelper.ReadBool(node.Attributes["backupFile"]);
			cT_WorkbookPr.saveExternalLinkValues = XmlHelper.ReadBool(node.Attributes["saveExternalLinkValues"], true);
			if (node.Attributes["updateLinks"] != null)
			{
				cT_WorkbookPr.updateLinks = (ST_UpdateLinks)Enum.Parse(typeof(ST_UpdateLinks), node.Attributes["updateLinks"].Value);
			}
			cT_WorkbookPr.codeName = XmlHelper.ReadString(node.Attributes["codeName"]);
			cT_WorkbookPr.hidePivotFieldList = XmlHelper.ReadBool(node.Attributes["hidePivotFieldList"]);
			cT_WorkbookPr.showPivotChartFilter = XmlHelper.ReadBool(node.Attributes["showPivotChartFilter"]);
			cT_WorkbookPr.allowRefreshQuery = XmlHelper.ReadBool(node.Attributes["allowRefreshQuery"]);
			cT_WorkbookPr.publishItems = XmlHelper.ReadBool(node.Attributes["publishItems"]);
			cT_WorkbookPr.checkCompatibility = XmlHelper.ReadBool(node.Attributes["checkCompatibility"]);
			cT_WorkbookPr.autoCompressPictures = XmlHelper.ReadBool(node.Attributes["autoCompressPictures"], true);
			cT_WorkbookPr.refreshAllConnections = XmlHelper.ReadBool(node.Attributes["refreshAllConnections"]);
			cT_WorkbookPr.defaultThemeVersion = XmlHelper.ReadUInt(node.Attributes["defaultThemeVersion"]);
			return cT_WorkbookPr;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "date1904", date1904, false);
			if (showObjects != 0)
			{
				XmlHelper.WriteAttribute(sw, "showObjects", showObjects.ToString());
			}
			if (!showBorderUnselectedTables)
			{
				XmlHelper.WriteAttribute(sw, "showBorderUnselectedTables", showBorderUnselectedTables);
			}
			XmlHelper.WriteAttribute(sw, "filterPrivacy", filterPrivacy, false);
			XmlHelper.WriteAttribute(sw, "promptedSolutions", promptedSolutions, false);
			if (!showInkAnnotationField)
			{
				XmlHelper.WriteAttribute(sw, "showInkAnnotation", showInkAnnotation, false);
			}
			XmlHelper.WriteAttribute(sw, "backupFile", backupFile, false);
			if (!saveExternalLinkValues)
			{
				XmlHelper.WriteAttribute(sw, "saveExternalLinkValues", saveExternalLinkValues);
			}
			if (updateLinks != 0)
			{
				XmlHelper.WriteAttribute(sw, "updateLinks", updateLinks.ToString());
			}
			XmlHelper.WriteAttribute(sw, "codeName", codeName);
			XmlHelper.WriteAttribute(sw, "hidePivotFieldList", hidePivotFieldList, false);
			XmlHelper.WriteAttribute(sw, "showPivotChartFilter", showPivotChartFilter, false);
			XmlHelper.WriteAttribute(sw, "allowRefreshQuery", allowRefreshQuery, false);
			XmlHelper.WriteAttribute(sw, "publishItems", publishItems, false);
			XmlHelper.WriteAttribute(sw, "checkCompatibility", checkCompatibility, false);
			XmlHelper.WriteAttribute(sw, "autoCompressPictures", autoCompressPictures, false);
			XmlHelper.WriteAttribute(sw, "refreshAllConnections", refreshAllConnections, false);
			XmlHelper.WriteAttribute(sw, "defaultThemeVersion", defaultThemeVersion);
			sw.Write("/>");
		}

		public CT_WorkbookPr()
		{
			date1904Field = false;
			showObjectsField = ST_Objects.all;
			showBorderUnselectedTablesField = true;
			filterPrivacyField = false;
			promptedSolutionsField = false;
			showInkAnnotationField = true;
			backupFileField = false;
			saveExternalLinkValuesField = true;
			updateLinksField = ST_UpdateLinks.userSet;
			hidePivotFieldListField = false;
			showPivotChartFilterField = false;
			allowRefreshQueryField = false;
			publishItemsField = false;
			checkCompatibilityField = false;
			autoCompressPicturesField = true;
			refreshAllConnectionsField = false;
		}
	}
}
