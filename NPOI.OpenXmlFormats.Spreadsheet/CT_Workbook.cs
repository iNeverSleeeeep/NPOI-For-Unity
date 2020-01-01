using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", ElementName = "workbook", IsNullable = false)]
	public class CT_Workbook
	{
		private CT_FileVersion fileVersionField;

		private CT_FileSharing fileSharingField;

		private CT_WorkbookPr workbookPrField;

		private CT_WorkbookProtection workbookProtectionField;

		private CT_BookViews bookViewsField;

		private CT_Sheets sheetsField = new CT_Sheets();

		private CT_FunctionGroups functionGroupsField;

		private CT_ExternalReferences externalReferencesField;

		private CT_DefinedNames definedNamesField;

		private CT_CalcPr calcPrField;

		private CT_OleSize oleSizeField;

		private CT_CustomWorkbookViews customWorkbookViewsField;

		private CT_PivotCaches pivotCachesField;

		private CT_SmartTagPr smartTagPrField;

		private CT_SmartTagTypes smartTagTypesField;

		private CT_WebPublishing webPublishingField;

		private List<CT_FileRecoveryPr> fileRecoveryPrField;

		private CT_WebPublishObjects webPublishObjectsField;

		private CT_ExtensionList extLstField;

		[XmlElement]
		public CT_FileVersion fileVersion
		{
			get
			{
				return fileVersionField;
			}
			set
			{
				fileVersionField = value;
			}
		}

		[XmlElement]
		public CT_FileSharing fileSharing
		{
			get
			{
				return fileSharingField;
			}
			set
			{
				fileSharingField = value;
			}
		}

		[XmlElement]
		public CT_WorkbookPr workbookPr
		{
			get
			{
				return workbookPrField;
			}
			set
			{
				workbookPrField = value;
			}
		}

		[XmlElement]
		public CT_WorkbookProtection workbookProtection
		{
			get
			{
				return workbookProtectionField;
			}
			set
			{
				workbookProtectionField = value;
			}
		}

		[XmlElement("bookViews", IsNullable = false)]
		public CT_BookViews bookViews
		{
			get
			{
				return bookViewsField;
			}
			set
			{
				bookViewsField = value;
			}
		}

		[XmlElement("sheets", IsNullable = false)]
		public CT_Sheets sheets
		{
			get
			{
				return sheetsField;
			}
			set
			{
				sheetsField = value;
			}
		}

		[XmlElement]
		public CT_FunctionGroups functionGroups
		{
			get
			{
				return functionGroupsField;
			}
			set
			{
				functionGroupsField = value;
			}
		}

		[XmlElement]
		public CT_ExternalReferences externalReferences
		{
			get
			{
				return externalReferencesField;
			}
			set
			{
				externalReferencesField = value;
			}
		}

		[XmlElement]
		public CT_DefinedNames definedNames
		{
			get
			{
				return definedNamesField;
			}
			set
			{
				definedNamesField = value;
			}
		}

		[XmlElement]
		public CT_CalcPr calcPr
		{
			get
			{
				return calcPrField;
			}
			set
			{
				calcPrField = value;
			}
		}

		[XmlElement]
		public CT_OleSize oleSize
		{
			get
			{
				return oleSizeField;
			}
			set
			{
				oleSizeField = value;
			}
		}

		[XmlElement]
		public CT_CustomWorkbookViews customWorkbookViews
		{
			get
			{
				return customWorkbookViewsField;
			}
			set
			{
				customWorkbookViewsField = value;
			}
		}

		[XmlElement]
		public CT_PivotCaches pivotCaches
		{
			get
			{
				return pivotCachesField;
			}
			set
			{
				pivotCachesField = value;
			}
		}

		[XmlElement]
		public CT_SmartTagPr smartTagPr
		{
			get
			{
				return smartTagPrField;
			}
			set
			{
				smartTagPrField = value;
			}
		}

		[XmlElement]
		public CT_SmartTagTypes smartTagTypes
		{
			get
			{
				return smartTagTypesField;
			}
			set
			{
				smartTagTypesField = value;
			}
		}

		[XmlElement]
		public CT_WebPublishing webPublishing
		{
			get
			{
				return webPublishingField;
			}
			set
			{
				webPublishingField = value;
			}
		}

		[XmlElement]
		public List<CT_FileRecoveryPr> fileRecoveryPr
		{
			get
			{
				return fileRecoveryPrField;
			}
			set
			{
				fileRecoveryPrField = value;
			}
		}

		[XmlElement]
		public CT_WebPublishObjects webPublishObjects
		{
			get
			{
				return webPublishObjectsField;
			}
			set
			{
				webPublishObjectsField = value;
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

		public static CT_Workbook Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Workbook cT_Workbook = new CT_Workbook();
			cT_Workbook.fileRecoveryPr = new List<CT_FileRecoveryPr>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "fileVersion")
				{
					cT_Workbook.fileVersion = CT_FileVersion.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fileSharing")
				{
					cT_Workbook.fileSharing = CT_FileSharing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "workbookPr")
				{
					cT_Workbook.workbookPr = CT_WorkbookPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "workbookProtection")
				{
					cT_Workbook.workbookProtection = CT_WorkbookProtection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bookViews")
				{
					cT_Workbook.bookViews = CT_BookViews.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheets")
				{
					cT_Workbook.sheets = CT_Sheets.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "functionGroups")
				{
					cT_Workbook.functionGroups = CT_FunctionGroups.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "externalReferences")
				{
					cT_Workbook.externalReferences = CT_ExternalReferences.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "definedNames")
				{
					cT_Workbook.definedNames = CT_DefinedNames.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "calcPr")
				{
					cT_Workbook.calcPr = CT_CalcPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "oleSize")
				{
					cT_Workbook.oleSize = CT_OleSize.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "customWorkbookViews")
				{
					cT_Workbook.customWorkbookViews = CT_CustomWorkbookViews.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pivotCaches")
				{
					cT_Workbook.pivotCaches = CT_PivotCaches.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "smartTagPr")
				{
					cT_Workbook.smartTagPr = CT_SmartTagPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "smartTagTypes")
				{
					cT_Workbook.smartTagTypes = CT_SmartTagTypes.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "webPublishing")
				{
					cT_Workbook.webPublishing = CT_WebPublishing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "webPublishObjects")
				{
					cT_Workbook.webPublishObjects = CT_WebPublishObjects.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Workbook.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "fileRecoveryPr")
				{
					cT_Workbook.fileRecoveryPr.Add(CT_FileRecoveryPr.Parse(childNode, namespaceManager));
				}
			}
			return cT_Workbook;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<workbook xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">");
			if (fileVersion != null)
			{
				fileVersion.Write(sw, "fileVersion");
			}
			if (fileSharing != null)
			{
				fileSharing.Write(sw, "fileSharing");
			}
			if (workbookPr != null)
			{
				workbookPr.Write(sw, "workbookPr");
			}
			if (workbookProtection != null)
			{
				workbookProtection.Write(sw, "workbookProtection");
			}
			if (bookViews != null)
			{
				bookViews.Write(sw, "bookViews");
			}
			if (sheets != null)
			{
				sheets.Write(sw, "sheets");
			}
			if (functionGroups != null)
			{
				functionGroups.Write(sw, "functionGroups");
			}
			if (externalReferences != null)
			{
				externalReferences.Write(sw, "externalReferences");
			}
			if (definedNames != null)
			{
				definedNames.Write(sw, "definedNames");
			}
			if (calcPr != null)
			{
				calcPr.Write(sw, "calcPr");
			}
			if (oleSize != null)
			{
				oleSize.Write(sw, "oleSize");
			}
			if (customWorkbookViews != null)
			{
				customWorkbookViews.Write(sw, "customWorkbookViews");
			}
			if (pivotCaches != null)
			{
				pivotCaches.Write(sw, "pivotCaches");
			}
			if (smartTagPr != null)
			{
				smartTagPr.Write(sw, "smartTagPr");
			}
			if (smartTagTypes != null)
			{
				smartTagTypes.Write(sw, "smartTagTypes");
			}
			if (webPublishing != null)
			{
				webPublishing.Write(sw, "webPublishing");
			}
			if (webPublishObjects != null)
			{
				webPublishObjects.Write(sw, "webPublishObjects");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (fileRecoveryPr != null)
			{
				foreach (CT_FileRecoveryPr item in fileRecoveryPr)
				{
					item.Write(sw, "fileRecoveryPr");
				}
			}
			sw.Write("</workbook>");
		}

		public CT_WorkbookPr AddNewWorkbookPr()
		{
			workbookPrField = new CT_WorkbookPr();
			return workbookPrField;
		}

		public CT_CalcPr AddNewCalcPr()
		{
			calcPrField = new CT_CalcPr();
			return calcPrField;
		}

		public CT_Sheets AddNewSheets()
		{
			sheetsField = new CT_Sheets();
			return sheetsField;
		}

		public CT_BookViews AddNewBookViews()
		{
			bookViewsField = new CT_BookViews();
			return bookViewsField;
		}

		public bool IsSetWorkbookPr()
		{
			return workbookPrField != null;
		}

		public bool IsSetCalcPr()
		{
			return calcPrField != null;
		}

		public bool IsSetSheets()
		{
			return sheetsField != null;
		}

		public bool IsSetBookViews()
		{
			return bookViewsField != null;
		}

		public bool IsSetDefinedNames()
		{
			return definedNamesField != null;
		}

		public CT_DefinedNames AddNewDefinedNames()
		{
			definedNamesField = new CT_DefinedNames();
			return definedNamesField;
		}

		public void SetDefinedNames(CT_DefinedNames definedNames)
		{
			definedNamesField = definedNames;
		}

		public void unsetDefinedNames()
		{
			definedNamesField = null;
		}
	}
}
