using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlRoot("chartsheet", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = false)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_Chartsheet
	{
		private CT_ChartsheetPr sheetPrField;

		private CT_ChartsheetViews sheetViewsField;

		private CT_ChartsheetProtection sheetProtectionField;

		private CT_CustomChartsheetViews customSheetViewsField;

		private CT_PageMargins pageMarginsField;

		private CT_CsPageSetup pageSetupField;

		private CT_HeaderFooter headerFooterField;

		private CT_Drawing drawingField;

		private CT_LegacyDrawing legacyDrawingField;

		private CT_LegacyDrawing legacyDrawingHFField;

		private CT_SheetBackgroundPicture pictureField;

		private CT_WebPublishItems webPublishItemsField;

		private CT_ExtensionList extLstField;

		[XmlElement]
		public CT_ChartsheetPr sheetPr
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
		public CT_ChartsheetViews sheetViews
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
		public CT_ChartsheetProtection sheetProtection
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
		public CT_CustomChartsheetViews customSheetViews
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
		public CT_CsPageSetup pageSetup
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

		public static CT_Chartsheet Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Chartsheet cT_Chartsheet = new CT_Chartsheet();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "sheetPr")
				{
					cT_Chartsheet.sheetPr = CT_ChartsheetPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetViews")
				{
					cT_Chartsheet.sheetViews = CT_ChartsheetViews.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sheetProtection")
				{
					cT_Chartsheet.sheetProtection = CT_ChartsheetProtection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "customSheetViews")
				{
					cT_Chartsheet.customSheetViews = CT_CustomChartsheetViews.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageMargins")
				{
					cT_Chartsheet.pageMargins = CT_PageMargins.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageSetup")
				{
					cT_Chartsheet.pageSetup = CT_CsPageSetup.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "headerFooter")
				{
					cT_Chartsheet.headerFooter = CT_HeaderFooter.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "drawing")
				{
					cT_Chartsheet.drawing = CT_Drawing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "legacyDrawing")
				{
					cT_Chartsheet.legacyDrawing = CT_LegacyDrawing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "legacyDrawingHF")
				{
					cT_Chartsheet.legacyDrawingHF = CT_LegacyDrawing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "picture")
				{
					cT_Chartsheet.picture = CT_SheetBackgroundPicture.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "webPublishItems")
				{
					cT_Chartsheet.webPublishItems = CT_WebPublishItems.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_Chartsheet.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_Chartsheet;
		}

		internal void Write(Stream stream)
		{
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				streamWriter.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
				streamWriter.Write("<chartsheet xmlns=\"http://schemas.openxmlformats.org/spreadsheetml/2006/main\"");
				streamWriter.Write(" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\"");
				streamWriter.Write(">");
				if (sheetPr != null)
				{
					sheetPr.Write(streamWriter, "sheetPr");
				}
				if (sheetViews != null)
				{
					sheetViews.Write(streamWriter, "sheetViews");
				}
				if (sheetProtection != null)
				{
					sheetProtection.Write(streamWriter, "sheetProtection");
				}
				if (customSheetViews != null)
				{
					customSheetViews.Write(streamWriter, "customSheetViews");
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
				if (webPublishItems != null)
				{
					webPublishItems.Write(streamWriter, "webPublishItems");
				}
				if (extLst != null)
				{
					extLst.Write(streamWriter, "extLst");
				}
				streamWriter.Write("</chartsheet>");
			}
		}
	}
}
