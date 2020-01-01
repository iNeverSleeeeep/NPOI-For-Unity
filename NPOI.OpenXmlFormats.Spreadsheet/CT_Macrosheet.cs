using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Macrosheet
	{
		private CT_SheetPr sheetPrField;

		private CT_SheetDimension dimensionField;

		private CT_SheetViews sheetViewsField;

		private CT_SheetFormatPr sheetFormatPrField;

		private List<CT_Col> colsField;

		private List<CT_Row> sheetDataField;

		private CT_SheetProtection sheetProtectionField;

		private CT_AutoFilter autoFilterField;

		private CT_SortState sortStateField;

		private CT_DataConsolidate dataConsolidateField;

		private List<CT_CustomSheetView> customSheetViewsField;

		private CT_PhoneticPr phoneticPrField;

		private List<CT_ConditionalFormatting> conditionalFormattingField;

		private CT_PrintOptions printOptionsField;

		private CT_PageMargins pageMarginsField;

		private CT_PageSetup pageSetupField;

		private CT_HeaderFooter headerFooterField;

		private CT_PageBreak rowBreaksField;

		private CT_PageBreak colBreaksField;

		private List<CT_CustomProperty> customPropertiesField;

		private CT_Drawing drawingField;

		private CT_LegacyDrawing legacyDrawingField;

		private CT_LegacyDrawing legacyDrawingHFField;

		private CT_SheetBackgroundPicture pictureField;

		private List<CT_OleObject> oleObjectsField;

		private CT_ExtensionList extLstField;

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

		[XmlArray(Order = 4)]
		[XmlArrayItem("col", typeof(CT_Col), IsNullable = false)]
		public List<CT_Col> cols
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

		[XmlArrayItem("row", IsNullable = false)]
		[XmlArray(Order = 5)]
		public List<CT_Row> sheetData
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

		[XmlArray(Order = 10)]
		[XmlArrayItem("customSheetView", IsNullable = false)]
		public List<CT_CustomSheetView> customSheetViews
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

		[XmlArray(Order = 19)]
		[XmlArrayItem("customPr", IsNullable = false)]
		public List<CT_CustomProperty> customProperties
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

		[XmlArray(Order = 24)]
		[XmlArrayItem("oleObject", IsNullable = false)]
		public List<CT_OleObject> oleObjects
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

		public CT_Macrosheet()
		{
			extLstField = new CT_ExtensionList();
			oleObjectsField = new List<CT_OleObject>();
			pictureField = new CT_SheetBackgroundPicture();
			legacyDrawingHFField = new CT_LegacyDrawing();
			legacyDrawingField = new CT_LegacyDrawing();
			drawingField = new CT_Drawing();
			customPropertiesField = new List<CT_CustomProperty>();
			colBreaksField = new CT_PageBreak();
			rowBreaksField = new CT_PageBreak();
			headerFooterField = new CT_HeaderFooter();
			pageSetupField = new CT_PageSetup();
			pageMarginsField = new CT_PageMargins();
			printOptionsField = new CT_PrintOptions();
			conditionalFormattingField = new List<CT_ConditionalFormatting>();
			phoneticPrField = new CT_PhoneticPr();
			customSheetViewsField = new List<CT_CustomSheetView>();
			dataConsolidateField = new CT_DataConsolidate();
			sortStateField = new CT_SortState();
			autoFilterField = new CT_AutoFilter();
			sheetProtectionField = new CT_SheetProtection();
			sheetDataField = new List<CT_Row>();
			colsField = new List<CT_Col>();
			sheetFormatPrField = new CT_SheetFormatPr();
			sheetViewsField = new CT_SheetViews();
			dimensionField = new CT_SheetDimension();
			sheetPrField = new CT_SheetPr();
		}
	}
}
