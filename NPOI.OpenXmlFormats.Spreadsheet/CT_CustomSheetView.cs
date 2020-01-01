using NPOI.OpenXml4Net.Util;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType("customSheetView", Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_CustomSheetView
	{
		private CT_Pane paneField;

		private CT_Selection selectionField;

		private CT_PageBreak rowBreaksField;

		private CT_PageBreak colBreaksField;

		private CT_PageMargins pageMarginsField;

		private CT_PrintOptions printOptionsField;

		private CT_PageSetup pageSetupField;

		private CT_HeaderFooter headerFooterField;

		private CT_AutoFilter autoFilterField;

		private CT_ExtensionList extLstField;

		private string guidField = string.Empty;

		private uint scaleField;

		private uint colorIdField;

		private bool showPageBreaksField;

		private bool showFormulasField;

		private bool showGridLinesField;

		private bool showRowColField;

		private bool outlineSymbolsField;

		private bool zeroValuesField;

		private bool fitToPageField;

		private bool printAreaField;

		private bool filterField;

		private bool showAutoFilterField;

		private bool hiddenRowsField;

		private bool hiddenColumnsField;

		private ST_SheetState stateField;

		private bool filterUniqueField;

		private ST_SheetViewType viewField;

		private bool showRulerField;

		private string topLeftCellField;

		public CT_Pane pane
		{
			get
			{
				return paneField;
			}
			set
			{
				paneField = value;
			}
		}

		public CT_Selection selection
		{
			get
			{
				return selectionField;
			}
			set
			{
				selectionField = value;
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

		[DefaultValue(typeof(uint), "100")]
		public uint scale
		{
			get
			{
				return scaleField;
			}
			set
			{
				scaleField = value;
			}
		}

		[DefaultValue(typeof(uint), "64")]
		public uint colorId
		{
			get
			{
				return colorIdField;
			}
			set
			{
				colorIdField = value;
			}
		}

		[DefaultValue(false)]
		public bool showPageBreaks
		{
			get
			{
				return showPageBreaksField;
			}
			set
			{
				showPageBreaksField = value;
			}
		}

		[DefaultValue(false)]
		public bool showFormulas
		{
			get
			{
				return showFormulasField;
			}
			set
			{
				showFormulasField = value;
			}
		}

		[DefaultValue(true)]
		public bool showGridLines
		{
			get
			{
				return showGridLinesField;
			}
			set
			{
				showGridLinesField = value;
			}
		}

		[DefaultValue(true)]
		public bool showRowCol
		{
			get
			{
				return showRowColField;
			}
			set
			{
				showRowColField = value;
			}
		}

		[DefaultValue(true)]
		public bool outlineSymbols
		{
			get
			{
				return outlineSymbolsField;
			}
			set
			{
				outlineSymbolsField = value;
			}
		}

		[DefaultValue(true)]
		public bool zeroValues
		{
			get
			{
				return zeroValuesField;
			}
			set
			{
				zeroValuesField = value;
			}
		}

		[DefaultValue(false)]
		public bool fitToPage
		{
			get
			{
				return fitToPageField;
			}
			set
			{
				fitToPageField = value;
			}
		}

		[DefaultValue(false)]
		public bool printArea
		{
			get
			{
				return printAreaField;
			}
			set
			{
				printAreaField = value;
			}
		}

		[DefaultValue(false)]
		public bool filter
		{
			get
			{
				return filterField;
			}
			set
			{
				filterField = value;
			}
		}

		[DefaultValue(false)]
		public bool showAutoFilter
		{
			get
			{
				return showAutoFilterField;
			}
			set
			{
				showAutoFilterField = value;
			}
		}

		[DefaultValue(false)]
		public bool hiddenRows
		{
			get
			{
				return hiddenRowsField;
			}
			set
			{
				hiddenRowsField = value;
			}
		}

		[DefaultValue(false)]
		public bool hiddenColumns
		{
			get
			{
				return hiddenColumnsField;
			}
			set
			{
				hiddenColumnsField = value;
			}
		}

		[DefaultValue(ST_SheetState.visible)]
		public ST_SheetState state
		{
			get
			{
				return stateField;
			}
			set
			{
				stateField = value;
			}
		}

		[DefaultValue(false)]
		public bool filterUnique
		{
			get
			{
				return filterUniqueField;
			}
			set
			{
				filterUniqueField = value;
			}
		}

		[DefaultValue(ST_SheetViewType.normal)]
		public ST_SheetViewType view
		{
			get
			{
				return viewField;
			}
			set
			{
				viewField = value;
			}
		}

		[DefaultValue(true)]
		public bool showRuler
		{
			get
			{
				return showRulerField;
			}
			set
			{
				showRulerField = value;
			}
		}

		public string topLeftCell
		{
			get
			{
				return topLeftCellField;
			}
			set
			{
				topLeftCellField = value;
			}
		}

		public static CT_CustomSheetView Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_CustomSheetView cT_CustomSheetView = new CT_CustomSheetView();
			cT_CustomSheetView.guid = XmlHelper.ReadString(node.Attributes["guid"]);
			cT_CustomSheetView.scale = XmlHelper.ReadUInt(node.Attributes["scale"]);
			cT_CustomSheetView.colorId = XmlHelper.ReadUInt(node.Attributes["colorId"]);
			cT_CustomSheetView.showPageBreaks = XmlHelper.ReadBool(node.Attributes["showPageBreaks"]);
			cT_CustomSheetView.showFormulas = XmlHelper.ReadBool(node.Attributes["showFormulas"]);
			if (node.Attributes["showGridLines"] == null)
			{
				cT_CustomSheetView.showGridLines = true;
			}
			else
			{
				cT_CustomSheetView.showGridLines = XmlHelper.ReadBool(node.Attributes["showGridLines"]);
			}
			cT_CustomSheetView.showRowCol = XmlHelper.ReadBool(node.Attributes["showRowCol"]);
			cT_CustomSheetView.outlineSymbols = XmlHelper.ReadBool(node.Attributes["outlineSymbols"]);
			cT_CustomSheetView.zeroValues = XmlHelper.ReadBool(node.Attributes["zeroValues"]);
			cT_CustomSheetView.fitToPage = XmlHelper.ReadBool(node.Attributes["fitToPage"]);
			cT_CustomSheetView.printArea = XmlHelper.ReadBool(node.Attributes["printArea"]);
			cT_CustomSheetView.filter = XmlHelper.ReadBool(node.Attributes["filter"]);
			cT_CustomSheetView.showAutoFilter = XmlHelper.ReadBool(node.Attributes["showAutoFilter"]);
			cT_CustomSheetView.hiddenRows = XmlHelper.ReadBool(node.Attributes["hiddenRows"]);
			cT_CustomSheetView.hiddenColumns = XmlHelper.ReadBool(node.Attributes["hiddenColumns"]);
			if (node.Attributes["state"] != null)
			{
				cT_CustomSheetView.state = (ST_SheetState)Enum.Parse(typeof(ST_SheetState), node.Attributes["state"].Value);
			}
			cT_CustomSheetView.filterUnique = XmlHelper.ReadBool(node.Attributes["filterUnique"]);
			if (node.Attributes["view"] != null)
			{
				cT_CustomSheetView.view = (ST_SheetViewType)Enum.Parse(typeof(ST_SheetViewType), node.Attributes["view"].Value);
			}
			cT_CustomSheetView.showRuler = XmlHelper.ReadBool(node.Attributes["showRuler"]);
			cT_CustomSheetView.topLeftCell = XmlHelper.ReadString(node.Attributes["topLeftCell"]);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pane")
				{
					cT_CustomSheetView.pane = CT_Pane.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "selection")
				{
					cT_CustomSheetView.selection = CT_Selection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rowBreaks")
				{
					cT_CustomSheetView.rowBreaks = CT_PageBreak.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "colBreaks")
				{
					cT_CustomSheetView.colBreaks = CT_PageBreak.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageMargins")
				{
					cT_CustomSheetView.pageMargins = CT_PageMargins.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printOptions")
				{
					cT_CustomSheetView.printOptions = CT_PrintOptions.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageSetup")
				{
					cT_CustomSheetView.pageSetup = CT_PageSetup.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "headerFooter")
				{
					cT_CustomSheetView.headerFooter = CT_HeaderFooter.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoFilter")
				{
					cT_CustomSheetView.autoFilter = CT_AutoFilter.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_CustomSheetView.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
			}
			return cT_CustomSheetView;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "guid", guid);
			XmlHelper.WriteAttribute(sw, "scale", scale);
			XmlHelper.WriteAttribute(sw, "colorId", colorId);
			XmlHelper.WriteAttribute(sw, "showPageBreaks", showPageBreaks, false);
			XmlHelper.WriteAttribute(sw, "showFormulas", showFormulas, false);
			XmlHelper.WriteAttribute(sw, "showGridLines", showGridLines, false);
			XmlHelper.WriteAttribute(sw, "showRowCol", showRowCol, false);
			XmlHelper.WriteAttribute(sw, "outlineSymbols", outlineSymbols);
			XmlHelper.WriteAttribute(sw, "zeroValues", zeroValues);
			XmlHelper.WriteAttribute(sw, "fitToPage", fitToPage, false);
			XmlHelper.WriteAttribute(sw, "printArea", printArea, false);
			XmlHelper.WriteAttribute(sw, "filter", filter, false);
			XmlHelper.WriteAttribute(sw, "showAutoFilter", showAutoFilter, false);
			XmlHelper.WriteAttribute(sw, "hiddenRows", hiddenRows, false);
			XmlHelper.WriteAttribute(sw, "hiddenColumns", hiddenColumns, false);
			XmlHelper.WriteAttribute(sw, "state", state.ToString());
			XmlHelper.WriteAttribute(sw, "filterUnique", filterUnique, false);
			XmlHelper.WriteAttribute(sw, "view", view.ToString());
			XmlHelper.WriteAttribute(sw, "showRuler", showRuler, false);
			XmlHelper.WriteAttribute(sw, "topLeftCell", topLeftCell);
			sw.Write(">");
			if (pane != null)
			{
				pane.Write(sw, "pane");
			}
			if (selection != null)
			{
				selection.Write(sw, "selection");
			}
			if (rowBreaks != null)
			{
				rowBreaks.Write(sw, "rowBreaks");
			}
			if (colBreaks != null)
			{
				colBreaks.Write(sw, "colBreaks");
			}
			if (pageMargins != null)
			{
				pageMargins.Write(sw, "pageMargins");
			}
			if (printOptions != null)
			{
				printOptions.Write(sw, "printOptions");
			}
			if (pageSetup != null)
			{
				pageSetup.Write(sw, "pageSetup");
			}
			if (headerFooter != null)
			{
				headerFooter.Write(sw, "headerFooter");
			}
			if (autoFilter != null)
			{
				autoFilter.Write(sw, "autoFilter");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}
	}
}
