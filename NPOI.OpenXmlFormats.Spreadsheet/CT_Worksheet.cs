using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", ElementName = "worksheet", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Worksheet
	{
		private CT_SheetPr sheetPrField;

		private CT_SheetDimension dimensionField;

		private CT_SheetViews sheetViewsField;

		private CT_SheetFormatPr sheetFormatPrField;

		private List<CT_Cols> colsField;

		private CT_SheetData sheetDataField = new CT_SheetData();

		private CT_SheetCalcPr sheetCalcPrField;

		private CT_SheetProtection sheetProtectionField;

		private CT_ProtectedRanges protectedRangesField;

		private CT_Scenarios scenariosField;

		private CT_AutoFilter autoFilterField;

		private CT_SortState sortStateField;

		private CT_DataConsolidate dataConsolidateField;

		private CT_CustomSheetViews customSheetViewsField;

		private CT_MergeCells mergeCellsField;

		private CT_PhoneticPr phoneticPrField;

		private List<CT_ConditionalFormatting> conditionalFormattingField;

		private CT_DataValidations dataValidationsField;

		private CT_Hyperlinks hyperlinksField;

		private CT_PrintOptions printOptionsField;

		private CT_PageMargins pageMarginsField;

		private CT_PageSetup pageSetupField;

		private CT_HeaderFooter headerFooterField;

		private CT_PageBreak rowBreaksField;

		private CT_PageBreak colBreaksField;

		private CT_CustomProperties customPropertiesField;

		private CT_CellWatches cellWatchesField;

		private CT_IgnoredErrors ignoredErrorsField;

		private CT_CellSmartTags smartTagsField;

		private CT_Drawing drawingField;

		private CT_LegacyDrawing legacyDrawingField;

		private CT_LegacyDrawing legacyDrawingHFField;

		private CT_SheetBackgroundPicture pictureField;

		private CT_OleObjects oleObjectsField;

		private CT_Controls controlsField;

		private CT_WebPublishItems webPublishItemsField;

		private CT_TableParts tablePartsField;

		private CT_ExtensionList extLstField;

		[XmlElement("sheetPr")]
		public CT_SheetPr sheetPr
		{
			get
			{
				return sheetPrField;
			}
			set
			{
				sheetPrField = value;
			}
		}

		[XmlElement]
		public CT_SheetDimension dimension
		{
			get
			{
				return dimensionField;
			}
			set
			{
				dimensionField = value;
			}
		}

		[XmlElement]
		public CT_SheetViews sheetViews
		{
			get
			{
				return sheetViewsField;
			}
			set
			{
				sheetViewsField = value;
			}
		}

		[XmlElement]
		public CT_SheetFormatPr sheetFormatPr
		{
			get
			{
				return sheetFormatPrField;
			}
			set
			{
				sheetFormatPrField = value;
			}
		}

		[XmlElement("cols")]
		public List<CT_Cols> cols
		{
			get
			{
				return colsField;
			}
			set
			{
				colsField = value;
			}
		}

		[XmlElement("sheetData", IsNullable = false)]
		public CT_SheetData sheetData
		{
			get
			{
				return sheetDataField;
			}
			set
			{
				sheetDataField = value;
			}
		}

		[XmlElement]
		public CT_SheetCalcPr sheetCalcPr
		{
			get
			{
				return sheetCalcPrField;
			}
			set
			{
				sheetCalcPrField = value;
			}
		}

		[XmlElement]
		public CT_SheetProtection sheetProtection
		{
			get
			{
				return sheetProtectionField;
			}
			set
			{
				sheetProtectionField = value;
			}
		}

		[XmlElement]
		public CT_ProtectedRanges protectedRanges
		{
			get
			{
				return protectedRangesField;
			}
			set
			{
				protectedRangesField = value;
			}
		}

		public CT_Scenarios scenarios
		{
			get
			{
				return scenariosField;
			}
			set
			{
				scenariosField = value;
			}
		}

		public CT_AutoFilter autoFilter
		{
			get
			{
				return autoFilterField;
			}
			set
			{
				autoFilterField = value;
			}
		}

		public CT_SortState sortState
		{
			get
			{
				return sortStateField;
			}
			set
			{
				sortStateField = value;
			}
		}

		public CT_DataConsolidate dataConsolidate
		{
			get
			{
				return dataConsolidateField;
			}
			set
			{
				dataConsolidateField = value;
			}
		}

		[XmlElement]
		public CT_CustomSheetViews customSheetViews
		{
			get
			{
				return customSheetViewsField;
			}
			set
			{
				customSheetViewsField = value;
			}
		}

		[XmlElement]
		public CT_MergeCells mergeCells
		{
			get
			{
				return mergeCellsField;
			}
			set
			{
				mergeCellsField = value;
			}
		}

		[XmlElement]
		public CT_PhoneticPr phoneticPr
		{
			get
			{
				return phoneticPrField;
			}
			set
			{
				phoneticPrField = value;
			}
		}

		[XmlElement]
		public List<CT_ConditionalFormatting> conditionalFormatting
		{
			get
			{
				return conditionalFormattingField;
			}
			set
			{
				conditionalFormattingField = value;
			}
		}

		[XmlElement]
		public CT_DataValidations dataValidations
		{
			get
			{
				return dataValidationsField;
			}
			set
			{
				dataValidationsField = value;
			}
		}

		[XmlElement]
		public CT_Hyperlinks hyperlinks
		{
			get
			{
				return hyperlinksField;
			}
			set
			{
				hyperlinksField = value;
			}
		}

		[XmlElement]
		public CT_PrintOptions printOptions
		{
			get
			{
				return printOptionsField;
			}
			set
			{
				printOptionsField = value;
			}
		}

		[XmlElement]
		public CT_PageMargins pageMargins
		{
			get
			{
				return pageMarginsField;
			}
			set
			{
				pageMarginsField = value;
			}
		}

		[XmlElement]
		public CT_PageSetup pageSetup
		{
			get
			{
				return pageSetupField;
			}
			set
			{
				pageSetupField = value;
			}
		}

		[XmlElement]
		public CT_HeaderFooter headerFooter
		{
			get
			{
				return headerFooterField;
			}
			set
			{
				headerFooterField = value;
			}
		}

		[XmlElement]
		public CT_PageBreak rowBreaks
		{
			get
			{
				return rowBreaksField;
			}
			set
			{
				rowBreaksField = value;
			}
		}

		[XmlElement]
		public CT_PageBreak colBreaks
		{
			get
			{
				return colBreaksField;
			}
			set
			{
				colBreaksField = value;
			}
		}

		[XmlElement]
		public CT_CustomProperties customProperties
		{
			get
			{
				return customPropertiesField;
			}
			set
			{
				customPropertiesField = value;
			}
		}

		[XmlElement]
		public CT_CellWatches cellWatches
		{
			get
			{
				return cellWatchesField;
			}
			set
			{
				cellWatchesField = value;
			}
		}

		[XmlElement]
		public CT_IgnoredErrors ignoredErrors
		{
			get
			{
				return ignoredErrorsField;
			}
			set
			{
				ignoredErrorsField = value;
			}
		}

		[XmlElement]
		public CT_CellSmartTags smartTags
		{
			get
			{
				return smartTagsField;
			}
			set
			{
				smartTagsField = value;
			}
		}

		[XmlElement]
		public CT_Drawing drawing
		{
			get
			{
				return drawingField;
			}
			set
			{
				drawingField = value;
			}
		}

		[XmlElement]
		public CT_LegacyDrawing legacyDrawing
		{
			get
			{
				return legacyDrawingField;
			}
			set
			{
				legacyDrawingField = value;
			}
		}

		[XmlElement]
		public CT_LegacyDrawing legacyDrawingHF
		{
			get
			{
				return legacyDrawingHFField;
			}
			set
			{
				legacyDrawingHFField = value;
			}
		}

		[XmlElement]
		public CT_SheetBackgroundPicture picture
		{
			get
			{
				return pictureField;
			}
			set
			{
				pictureField = value;
			}
		}

		[XmlElement]
		public CT_OleObjects oleObjects
		{
			get
			{
				return oleObjectsField;
			}
			set
			{
				oleObjectsField = value;
			}
		}

		[XmlElement]
		public CT_Controls controls
		{
			get
			{
				return controlsField;
			}
			set
			{
				controlsField = value;
			}
		}

		[XmlElement]
		public CT_WebPublishItems webPublishItems
		{
			get
			{
				return webPublishItemsField;
			}
			set
			{
				webPublishItemsField = value;
			}
		}

		[XmlElement]
		public CT_TableParts tableParts
		{
			get
			{
				return tablePartsField;
			}
			set
			{
				tablePartsField = value;
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

		public static CT_Worksheet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Worksheet cT_Worksheet = new CT_Worksheet();
			cT_Worksheet.cols = new List<CT_Cols>();
			cT_Worksheet.conditionalFormatting = new List<CT_ConditionalFormatting>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sheetPr")
				{
					cT_Worksheet.sheetPr = CT_SheetPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dimension")
				{
					cT_Worksheet.dimension = CT_SheetDimension.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetViews")
				{
					cT_Worksheet.sheetViews = CT_SheetViews.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetFormatPr")
				{
					cT_Worksheet.sheetFormatPr = CT_SheetFormatPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetData")
				{
					cT_Worksheet.sheetData = CT_SheetData.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetCalcPr")
				{
					cT_Worksheet.sheetCalcPr = CT_SheetCalcPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetProtection")
				{
					cT_Worksheet.sheetProtection = CT_SheetProtection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "protectedRanges")
				{
					cT_Worksheet.protectedRanges = CT_ProtectedRanges.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "scenarios")
				{
					cT_Worksheet.scenarios = CT_Scenarios.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoFilter")
				{
					cT_Worksheet.autoFilter = CT_AutoFilter.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sortState")
				{
					cT_Worksheet.sortState = CT_SortState.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dataConsolidate")
				{
					cT_Worksheet.dataConsolidate = CT_DataConsolidate.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "customSheetViews")
				{
					cT_Worksheet.customSheetViews = CT_CustomSheetViews.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mergeCells")
				{
					cT_Worksheet.mergeCells = CT_MergeCells.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "phoneticPr")
				{
					cT_Worksheet.phoneticPr = CT_PhoneticPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "dataValidations")
				{
					cT_Worksheet.dataValidations = CT_DataValidations.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hyperlinks")
				{
					cT_Worksheet.hyperlinks = CT_Hyperlinks.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printOptions")
				{
					cT_Worksheet.printOptions = CT_PrintOptions.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageMargins")
				{
					cT_Worksheet.pageMargins = CT_PageMargins.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageSetup")
				{
					cT_Worksheet.pageSetup = CT_PageSetup.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "headerFooter")
				{
					cT_Worksheet.headerFooter = CT_HeaderFooter.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rowBreaks")
				{
					cT_Worksheet.rowBreaks = CT_PageBreak.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "colBreaks")
				{
					cT_Worksheet.colBreaks = CT_PageBreak.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "customProperties")
				{
					cT_Worksheet.customProperties = CT_CustomProperties.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cellWatches")
				{
					cT_Worksheet.cellWatches = CT_CellWatches.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ignoredErrors")
				{
					cT_Worksheet.ignoredErrors = CT_IgnoredErrors.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "smartTags")
				{
					cT_Worksheet.smartTags = CT_CellSmartTags.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "drawing")
				{
					cT_Worksheet.drawing = CT_Drawing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "legacyDrawing")
				{
					cT_Worksheet.legacyDrawing = CT_LegacyDrawing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "legacyDrawingHF")
				{
					cT_Worksheet.legacyDrawingHF = CT_LegacyDrawing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "picture")
				{
					cT_Worksheet.picture = CT_SheetBackgroundPicture.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "oleObjects")
				{
					cT_Worksheet.oleObjects = CT_OleObjects.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "controls")
				{
					cT_Worksheet.controls = CT_Controls.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "webPublishItems")
				{
					cT_Worksheet.webPublishItems = CT_WebPublishItems.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tableParts")
				{
					cT_Worksheet.tableParts = CT_TableParts.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Worksheet.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cols")
				{
					cT_Worksheet.cols.Add(CT_Cols.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "conditionalFormatting")
				{
					cT_Worksheet.conditionalFormatting.Add(CT_ConditionalFormatting.Parse(childNode, namespaceManager));
				}
			}
			return cT_Worksheet;
		}

		internal void Write(Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
				streamWriter.Write("<worksheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\">");
				if (sheetPr != null)
				{
					sheetPr.Write(streamWriter, "sheetPr");
				}
				if (dimension != null)
				{
					dimension.Write(streamWriter, "dimension");
				}
				if (sheetViews != null)
				{
					sheetViews.Write(streamWriter, "sheetViews");
				}
				if (sheetFormatPr != null)
				{
					sheetFormatPr.Write(streamWriter, "sheetFormatPr");
				}
				if (cols != null)
				{
					foreach (CT_Cols col in cols)
					{
						col.Write(streamWriter, "cols");
					}
				}
				if (sheetData != null)
				{
					sheetData.Write(streamWriter, "sheetData");
				}
				if (sheetCalcPr != null)
				{
					sheetCalcPr.Write(streamWriter, "sheetCalcPr");
				}
				if (sheetProtection != null)
				{
					sheetProtection.Write(streamWriter, "sheetProtection");
				}
				if (protectedRanges != null)
				{
					protectedRanges.Write(streamWriter, "protectedRanges");
				}
				if (scenarios != null)
				{
					scenarios.Write(streamWriter, "scenarios");
				}
				if (autoFilter != null)
				{
					autoFilter.Write(streamWriter, "autoFilter");
				}
				if (sortState != null)
				{
					sortState.Write(streamWriter, "sortState");
				}
				if (dataConsolidate != null)
				{
					dataConsolidate.Write(streamWriter, "dataConsolidate");
				}
				if (customSheetViews != null)
				{
					customSheetViews.Write(streamWriter, "customSheetViews");
				}
				if (mergeCells != null)
				{
					mergeCells.Write(streamWriter, "mergeCells");
				}
				if (phoneticPr != null)
				{
					phoneticPr.Write(streamWriter, "phoneticPr");
				}
				if (conditionalFormatting != null)
				{
					foreach (CT_ConditionalFormatting item in conditionalFormatting)
					{
						item.Write(streamWriter, "conditionalFormatting");
					}
				}
				if (dataValidations != null)
				{
					dataValidations.Write(streamWriter, "dataValidations");
				}
				if (hyperlinks != null)
				{
					hyperlinks.Write(streamWriter, "hyperlinks");
				}
				if (printOptions != null)
				{
					printOptions.Write(streamWriter, "printOptions");
				}
				if (pageMargins != null)
				{
					pageMargins.Write(streamWriter, "pageMargins");
				}
				if (pageSetup != null)
				{
					pageSetup.Write(streamWriter, "pageSetup");
				}
				if (headerFooter != null)
				{
					headerFooter.Write(streamWriter, "headerFooter");
				}
				if (rowBreaks != null)
				{
					rowBreaks.Write(streamWriter, "rowBreaks");
				}
				if (colBreaks != null)
				{
					colBreaks.Write(streamWriter, "colBreaks");
				}
				if (customProperties != null)
				{
					customProperties.Write(streamWriter, "customProperties");
				}
				if (cellWatches != null)
				{
					cellWatches.Write(streamWriter, "cellWatches");
				}
				if (ignoredErrors != null)
				{
					ignoredErrors.Write(streamWriter, "ignoredErrors");
				}
				if (smartTags != null)
				{
					smartTags.Write(streamWriter, "smartTags");
				}
				if (drawing != null)
				{
					drawing.Write(streamWriter, "drawing");
				}
				if (legacyDrawing != null)
				{
					legacyDrawing.Write(streamWriter, "legacyDrawing");
				}
				if (legacyDrawingHF != null)
				{
					legacyDrawingHF.Write(streamWriter, "legacyDrawingHF");
				}
				if (picture != null)
				{
					picture.Write(streamWriter, "picture");
				}
				if (oleObjects != null)
				{
					oleObjects.Write(streamWriter, "oleObjects");
				}
				if (controls != null)
				{
					controls.Write(streamWriter, "controls");
				}
				if (webPublishItems != null)
				{
					webPublishItems.Write(streamWriter, "webPublishItems");
				}
				if (tableParts != null)
				{
					tableParts.Write(streamWriter, "tableParts");
				}
				if (extLst != null)
				{
					extLst.Write(streamWriter, "extLst");
				}
				streamWriter.Write("</worksheet>");
			}
		}

		public CT_AutoFilter AddNewAutoFilter()
		{
			autoFilterField = new CT_AutoFilter();
			return autoFilterField;
		}

		public bool IsSetRowBreaks()
		{
			return rowBreaksField != null;
		}

		public CT_Drawing AddNewDrawing()
		{
			drawingField = new CT_Drawing();
			return drawingField;
		}

		public CT_LegacyDrawing AddNewLegacyDrawing()
		{
			legacyDrawing = new CT_LegacyDrawing();
			return legacyDrawing;
		}

		public CT_PageBreak AddNewRowBreaks()
		{
			rowBreaksField = new CT_PageBreak();
			return rowBreaksField;
		}

		public CT_PageBreak AddNewColBreaks()
		{
			colBreaksField = new CT_PageBreak();
			return colBreaksField;
		}

		public bool IsSetSheetFormatPr()
		{
			return sheetFormatPrField != null;
		}

		public bool IsSetPrintOptions()
		{
			return printOptionsField != null;
		}

		public void UnsetMergeCells()
		{
			mergeCellsField = null;
		}

		public CT_PrintOptions AddNewPrintOptions()
		{
			printOptionsField = new CT_PrintOptions();
			return printOptionsField;
		}

		public CT_DataValidations AddNewDataValidations()
		{
			dataValidationsField = new CT_DataValidations();
			return dataValidationsField;
		}

		public CT_SheetViews AddNewSheetViews()
		{
			sheetViewsField = new CT_SheetViews();
			return sheetViewsField;
		}

		public CT_Hyperlinks AddNewHyperlinks()
		{
			hyperlinksField = new CT_Hyperlinks();
			return hyperlinksField;
		}

		public CT_ConditionalFormatting AddNewConditionalFormatting()
		{
			if (conditionalFormattingField == null)
			{
				conditionalFormattingField = new List<CT_ConditionalFormatting>();
			}
			CT_ConditionalFormatting cT_ConditionalFormatting = new CT_ConditionalFormatting();
			conditionalFormattingField.Add(cT_ConditionalFormatting);
			return cT_ConditionalFormatting;
		}

		public CT_ConditionalFormatting GetConditionalFormattingArray(int index)
		{
			if (conditionalFormattingField != null)
			{
				return conditionalFormattingField[index];
			}
			return null;
		}

		public CT_MergeCells AddNewMergeCells()
		{
			mergeCellsField = new CT_MergeCells();
			return mergeCellsField;
		}

		public bool IsSetColBreaks()
		{
			return colBreaksField != null;
		}

		public bool IsSetHyperlinks()
		{
			return hyperlinksField != null;
		}

		public bool IsSetMergeCells()
		{
			return mergeCellsField != null;
		}

		public bool IsSetSheetProtection()
		{
			return sheetProtectionField != null;
		}

		public bool IsSetDrawing()
		{
			return drawingField != null;
		}

		public void UnsetDrawing()
		{
			drawingField = null;
		}

		public bool IsSetLegacyDrawing()
		{
			return legacyDrawingField != null;
		}

		public void UnsetLegacyDrawing()
		{
			legacyDrawingField = null;
		}

		public bool IsSetPageSetup()
		{
			return pageSetupField != null;
		}

		public bool IsSetTableParts()
		{
			return tablePartsField != null;
		}

		public bool IsSetSheetCalcPr()
		{
			return sheetCalcPrField != null;
		}

		public CT_SheetProtection AddNewSheetProtection()
		{
			sheetProtectionField = new CT_SheetProtection();
			return sheetProtectionField;
		}

		public CT_TableParts AddNewTableParts()
		{
			tablePartsField = new CT_TableParts();
			return tablePartsField;
		}

		public CT_PageMargins AddNewPageMargins()
		{
			pageMarginsField = new CT_PageMargins();
			return pageMarginsField;
		}

		public CT_PageSetup AddNewPageSetup()
		{
			pageSetupField = new CT_PageSetup();
			return pageSetupField;
		}

		public void SetColsArray(List<CT_Cols> a)
		{
			colsField = a;
		}

		public int sizeOfColsArray()
		{
			if (colsField != null)
			{
				return colsField.Count;
			}
			return 0;
		}

		public void RemoveCols(int index)
		{
			colsField.RemoveAt(index);
		}

		public CT_Cols AddNewCols()
		{
			if (colsField == null)
			{
				colsField = new List<CT_Cols>();
			}
			CT_Cols cT_Cols = new CT_Cols();
			colsField.Add(cT_Cols);
			return cT_Cols;
		}

		public void SetColsArray(int index, CT_Cols newCols)
		{
			if (colsField == null)
			{
				colsField = new List<CT_Cols>();
			}
			else
			{
				colsField.Clear();
			}
			colsField.Insert(index, newCols);
		}

		public CT_Cols GetColsArray(int index)
		{
			if (colsField == null)
			{
				colsField = new List<CT_Cols>();
			}
			return colsField[index];
		}

		public List<CT_Cols> GetColsArray()
		{
			return colsField;
		}

		public bool IsSetPageMargins()
		{
			return pageMarginsField != null;
		}

		public bool IsSetHyperLinks()
		{
			return hyperlinksField != null;
		}

		public bool IsSetSheetPr()
		{
			return sheetPrField != null;
		}

		public int SizeOfConditionalFormattingArray()
		{
			if (conditionalFormattingField != null)
			{
				return conditionalFormatting.Count;
			}
			return 0;
		}

		public void UnsetSheetProtection()
		{
			sheetProtectionField = null;
		}

		public CT_SheetFormatPr AddNewSheetFormatPr()
		{
			sheetFormatPrField = new CT_SheetFormatPr();
			return sheetFormatPrField;
		}

		public CT_SheetCalcPr AddNewSheetCalcPr()
		{
			sheetCalcPrField = new CT_SheetCalcPr();
			return sheetCalcPrField;
		}

		public CT_SheetPr AddNewSheetPr()
		{
			sheetPrField = new CT_SheetPr();
			return sheetPrField;
		}

		public CT_SheetDimension AddNewDimension()
		{
			dimensionField = new CT_SheetDimension();
			return dimensionField;
		}

		public CT_SheetData AddNewSheetData()
		{
			sheetDataField = new CT_SheetData();
			return sheetDataField;
		}

		public void UnsetPageSetup()
		{
			pageSetup = null;
		}
	}
}
