using NPOI.OpenXml4Net.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public class CT_SheetView
	{
		private CT_Pane paneField;

		private List<CT_Selection> selectionField;

		private List<CT_PivotSelection> pivotSelectionField;

		private CT_ExtensionList extLstField;

		private bool windowProtectionField;

		private bool showFormulasField;

		private bool showGridLinesField;

		private bool showRowColHeadersField;

		private bool showZerosField;

		private bool rightToLeftField;

		private bool tabSelectedField;

		private bool showRulerField;

		private bool showOutlineSymbolsField;

		private bool defaultGridColorField;

		private bool showWhiteSpaceField;

		private ST_SheetViewType viewField;

		private string topLeftCellField;

		private uint colorIdField;

		private uint zoomScaleField;

		private uint zoomScaleNormalField;

		private uint zoomScaleSheetLayoutViewField;

		private uint zoomScalePageLayoutViewField;

		private uint workbookViewIdField;

		[XmlElement]
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

		[XmlElement]
		public List<CT_Selection> selection
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

		[XmlElement]
		public List<CT_PivotSelection> pivotSelection
		{
			get
			{
				return pivotSelectionField;
			}
			set
			{
				pivotSelectionField = value;
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

		[DefaultValue(false)]
		[XmlAttribute]
		public bool windowProtection
		{
			get
			{
				return windowProtectionField;
			}
			set
			{
				windowProtectionField = value;
			}
		}

		[XmlAttribute]
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
		[XmlAttribute]
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

		[XmlAttribute]
		[DefaultValue(true)]
		public bool showRowColHeaders
		{
			get
			{
				return showRowColHeadersField;
			}
			set
			{
				showRowColHeadersField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool showZeros
		{
			get
			{
				return showZerosField;
			}
			set
			{
				showZerosField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool rightToLeft
		{
			get
			{
				return rightToLeftField;
			}
			set
			{
				rightToLeftField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
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

		[DefaultValue(true)]
		[XmlAttribute]
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

		[DefaultValue(true)]
		[XmlAttribute]
		public bool showOutlineSymbols
		{
			get
			{
				return showOutlineSymbolsField;
			}
			set
			{
				showOutlineSymbolsField = value;
			}
		}

		[DefaultValue(true)]
		[XmlAttribute]
		public bool defaultGridColor
		{
			get
			{
				return defaultGridColorField;
			}
			set
			{
				defaultGridColorField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool showWhiteSpace
		{
			get
			{
				return showWhiteSpaceField;
			}
			set
			{
				showWhiteSpaceField = value;
			}
		}

		[XmlAttribute]
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

		[XmlAttribute]
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

		[DefaultValue(typeof(uint), "64")]
		[XmlAttribute]
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

		[DefaultValue(typeof(uint), "100")]
		[XmlAttribute]
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

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint zoomScaleNormal
		{
			get
			{
				return zoomScaleNormalField;
			}
			set
			{
				zoomScaleNormalField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint zoomScaleSheetLayoutView
		{
			get
			{
				return zoomScaleSheetLayoutViewField;
			}
			set
			{
				zoomScaleSheetLayoutViewField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		[XmlAttribute]
		public uint zoomScalePageLayoutView
		{
			get
			{
				return zoomScalePageLayoutViewField;
			}
			set
			{
				zoomScalePageLayoutViewField = value;
			}
		}

		[XmlAttribute]
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

		public static CT_SheetView Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_SheetView cT_SheetView = new CT_SheetView();
			cT_SheetView.windowProtection = XmlHelper.ReadBool(node.Attributes["windowProtection"]);
			cT_SheetView.showFormulas = XmlHelper.ReadBool(node.Attributes["showFormulas"]);
			if (node.Attributes["showGridLines"] == null)
			{
				cT_SheetView.showGridLines = true;
			}
			else
			{
				cT_SheetView.showGridLines = XmlHelper.ReadBool(node.Attributes["showGridLines"]);
			}
			if (node.Attributes["showRowColHeaders"] == null)
			{
				cT_SheetView.showRowColHeaders = true;
			}
			else
			{
				cT_SheetView.showRowColHeaders = XmlHelper.ReadBool(node.Attributes["showRowColHeaders"]);
			}
			if (node.Attributes["showZeros"] == null)
			{
				cT_SheetView.showZeros = true;
			}
			else
			{
				cT_SheetView.showZeros = XmlHelper.ReadBool(node.Attributes["showZeros"]);
			}
			cT_SheetView.rightToLeft = XmlHelper.ReadBool(node.Attributes["rightToLeft"]);
			cT_SheetView.tabSelected = XmlHelper.ReadBool(node.Attributes["tabSelected"]);
			cT_SheetView.showRuler = XmlHelper.ReadBool(node.Attributes["showRuler"]);
			cT_SheetView.showOutlineSymbols = XmlHelper.ReadBool(node.Attributes["showOutlineSymbols"]);
			cT_SheetView.defaultGridColor = XmlHelper.ReadBool(node.Attributes["defaultGridColor"]);
			if (node.Attributes["showWhiteSpace"] == null)
			{
				cT_SheetView.showWhiteSpace = true;
			}
			else
			{
				cT_SheetView.showWhiteSpace = XmlHelper.ReadBool(node.Attributes["showWhiteSpace"]);
			}
			if (node.Attributes["view"] != null)
			{
				cT_SheetView.view = (ST_SheetViewType)Enum.Parse(typeof(ST_SheetViewType), node.Attributes["view"].Value);
			}
			cT_SheetView.topLeftCell = XmlHelper.ReadString(node.Attributes["topLeftCell"]);
			cT_SheetView.colorId = XmlHelper.ReadUInt(node.Attributes["colorId"]);
			cT_SheetView.zoomScale = XmlHelper.ReadUInt(node.Attributes["zoomScale"]);
			cT_SheetView.zoomScaleNormal = XmlHelper.ReadUInt(node.Attributes["zoomScaleNormal"]);
			cT_SheetView.zoomScaleSheetLayoutView = XmlHelper.ReadUInt(node.Attributes["zoomScaleSheetLayoutView"]);
			cT_SheetView.zoomScalePageLayoutView = XmlHelper.ReadUInt(node.Attributes["zoomScalePageLayoutView"]);
			cT_SheetView.workbookViewId = XmlHelper.ReadUInt(node.Attributes["workbookViewId"]);
			cT_SheetView.selection = new List<CT_Selection>();
			cT_SheetView.pivotSelection = new List<CT_PivotSelection>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pane")
				{
					cT_SheetView.pane = CT_Pane.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "extLst")
				{
					cT_SheetView.extLst = CT_ExtensionList.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "selection")
				{
					cT_SheetView.selection.Add(CT_Selection.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "pivotSelection")
				{
					cT_SheetView.pivotSelection.Add(CT_PivotSelection.Parse(childNode, namespaceManager));
				}
			}
			return cT_SheetView;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<{0}", nodeName));
			XmlHelper.WriteAttribute(sw, "windowProtection", windowProtection, false);
			XmlHelper.WriteAttribute(sw, "showFormulas", showFormulas, false);
			if (!showGridLines)
			{
				XmlHelper.WriteAttribute(sw, "showGridLines", showGridLines);
			}
			if (!showRowColHeaders)
			{
				XmlHelper.WriteAttribute(sw, "showRowColHeaders", showRowColHeaders);
			}
			if (!showZeros)
			{
				XmlHelper.WriteAttribute(sw, "showZeros", showZeros);
			}
			XmlHelper.WriteAttribute(sw, "rightToLeft", rightToLeft, false);
			XmlHelper.WriteAttribute(sw, "tabSelected", tabSelected, false);
			XmlHelper.WriteAttribute(sw, "showRuler", showRuler, false);
			XmlHelper.WriteAttribute(sw, "showOutlineSymbols", showOutlineSymbols, false);
			XmlHelper.WriteAttribute(sw, "defaultGridColor", defaultGridColor, false);
			if (!showWhiteSpace)
			{
				XmlHelper.WriteAttribute(sw, "showWhiteSpace", showWhiteSpace);
			}
			if (view != 0)
			{
				XmlHelper.WriteAttribute(sw, "view", view.ToString());
			}
			XmlHelper.WriteAttribute(sw, "topLeftCell", topLeftCell);
			XmlHelper.WriteAttribute(sw, "colorId", colorId);
			XmlHelper.WriteAttribute(sw, "zoomScale", zoomScale);
			XmlHelper.WriteAttribute(sw, "zoomScaleNormal", zoomScaleNormal);
			XmlHelper.WriteAttribute(sw, "zoomScaleSheetLayoutView", zoomScaleSheetLayoutView);
			XmlHelper.WriteAttribute(sw, "zoomScalePageLayoutView", zoomScalePageLayoutView);
			XmlHelper.WriteAttribute(sw, "workbookViewId", (double)workbookViewId, true);
			sw.Write(">");
			if (pane != null)
			{
				pane.Write(sw, "pane");
			}
			if (extLst != null)
			{
				extLst.Write(sw, "extLst");
			}
			if (selection != null)
			{
				foreach (CT_Selection item in selection)
				{
					item.Write(sw, "selection");
				}
			}
			if (pivotSelection != null)
			{
				foreach (CT_PivotSelection item2 in pivotSelection)
				{
					item2.Write(sw, "pivotSelection");
				}
			}
			sw.Write(string.Format("</{0}>", nodeName));
		}

		public CT_SheetView()
		{
			pivotSelectionField = new List<CT_PivotSelection>();
			selectionField = new List<CT_Selection>();
			windowProtectionField = false;
			showFormulasField = false;
			showGridLinesField = true;
			showRowColHeadersField = true;
			showZerosField = true;
			rightToLeftField = false;
			tabSelectedField = false;
			showRulerField = true;
			showOutlineSymbolsField = true;
			defaultGridColorField = true;
			showWhiteSpaceField = true;
			viewField = ST_SheetViewType.normal;
			colorIdField = 64u;
			zoomScaleField = 100u;
			zoomScaleNormalField = 0u;
			zoomScaleSheetLayoutViewField = 0u;
			zoomScalePageLayoutViewField = 0u;
		}

		public bool IsSetPane()
		{
			return paneField != null;
		}

		public CT_Pane AddNewPane()
		{
			paneField = new CT_Pane();
			return paneField;
		}

		public CT_Selection AddNewSelection()
		{
			if (selectionField == null)
			{
				selectionField = new List<CT_Selection>();
			}
			CT_Selection cT_Selection = new CT_Selection();
			selectionField.Add(cT_Selection);
			return cT_Selection;
		}

		public void UnsetPane()
		{
			paneField = null;
		}

		public CT_Selection GetSelectionArray(int index)
		{
			return selectionField[index];
		}

		public CT_Selection InsertNewSelection(int index)
		{
			CT_Selection cT_Selection = new CT_Selection();
			selectionField.Insert(index, cT_Selection);
			return cT_Selection;
		}

		public int SizeOfSelectionArray()
		{
			return selectionField.Count;
		}

		public void SetSelectionArray(List<CT_Selection> selectionArray)
		{
			selectionField = selectionArray;
		}
	}
}
