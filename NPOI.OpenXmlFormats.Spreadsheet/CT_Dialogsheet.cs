using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Dialogsheet
	{
		private CT_SheetPr sheetPrField;

		private CT_SheetViews sheetViewsField;

		private CT_SheetFormatPr sheetFormatPrField;

		private CT_SheetProtection sheetProtectionField;

		private List<CT_CustomSheetView> customSheetViewsField;

		private CT_PrintOptions printOptionsField;

		private CT_PageMargins pageMarginsField;

		private CT_PageSetup pageSetupField;

		private CT_HeaderFooter headerFooterField;

		private CT_Drawing drawingField;

		private CT_LegacyDrawing legacyDrawingField;

		private CT_LegacyDrawing legacyDrawingHFField;

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

		[XmlArray(Order = 4)]
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

		[XmlArrayItem("oleObject", IsNullable = false)]
		[XmlArray(Order = 12)]
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
	}
}
