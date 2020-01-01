using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlInclude(typeof(CT_PPr))]
	public class CT_PPrBase
	{
		private CT_String pStyleField;

		private CT_OnOff keepNextField;

		private CT_OnOff keepLinesField;

		private CT_OnOff pageBreakBeforeField;

		private CT_FramePr framePrField;

		private CT_OnOff widowControlField;

		private CT_NumPr numPrField;

		private CT_OnOff suppressLineNumbersField;

		private CT_PBdr pBdrField;

		private CT_Shd shdField;

		private List<CT_TabStop> tabsField;

		private CT_OnOff suppressAutoHyphensField;

		private CT_OnOff kinsokuField;

		private CT_OnOff wordWrapField;

		private CT_OnOff overflowPunctField;

		private CT_OnOff topLinePunctField;

		private CT_OnOff autoSpaceDEField;

		private CT_OnOff autoSpaceDNField;

		private CT_OnOff bidiField;

		private CT_OnOff adjustRightIndField;

		private CT_OnOff snapToGridField;

		private CT_Spacing spacingField;

		private CT_Ind indField;

		private CT_OnOff contextualSpacingField;

		private CT_OnOff mirrorIndentsField;

		private CT_OnOff suppressOverlapField;

		private CT_Jc jcField;

		private CT_TextDirection textDirectionField;

		private CT_TextAlignment textAlignmentField;

		private CT_TextboxTightWrap textboxTightWrapField;

		private CT_DecimalNumber outlineLvlField;

		private CT_DecimalNumber divIdField;

		private CT_Cnf cnfStyleField;

		[XmlElement(Order = 0)]
		public CT_String pStyle
		{
			get
			{
				return pStyleField;
			}
			set
			{
				pStyleField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff keepNext
		{
			get
			{
				return keepNextField;
			}
			set
			{
				keepNextField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff keepLines
		{
			get
			{
				return keepLinesField;
			}
			set
			{
				keepLinesField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff pageBreakBefore
		{
			get
			{
				return pageBreakBeforeField;
			}
			set
			{
				pageBreakBeforeField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_FramePr framePr
		{
			get
			{
				return framePrField;
			}
			set
			{
				framePrField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_OnOff widowControl
		{
			get
			{
				return widowControlField;
			}
			set
			{
				widowControlField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_NumPr numPr
		{
			get
			{
				return numPrField;
			}
			set
			{
				numPrField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OnOff suppressLineNumbers
		{
			get
			{
				return suppressLineNumbersField;
			}
			set
			{
				suppressLineNumbersField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_PBdr pBdr
		{
			get
			{
				return pBdrField;
			}
			set
			{
				pBdrField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_Shd shd
		{
			get
			{
				return shdField;
			}
			set
			{
				shdField = value;
			}
		}

		[XmlArrayItem("tab", IsNullable = false)]
		[XmlArray(Order = 10)]
		public List<CT_TabStop> tabs
		{
			get
			{
				return tabsField;
			}
			set
			{
				tabsField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_OnOff suppressAutoHyphens
		{
			get
			{
				return suppressAutoHyphensField;
			}
			set
			{
				suppressAutoHyphensField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_OnOff kinsoku
		{
			get
			{
				return kinsokuField;
			}
			set
			{
				kinsokuField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_OnOff wordWrap
		{
			get
			{
				return wordWrapField;
			}
			set
			{
				wordWrapField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_OnOff overflowPunct
		{
			get
			{
				return overflowPunctField;
			}
			set
			{
				overflowPunctField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_OnOff topLinePunct
		{
			get
			{
				return topLinePunctField;
			}
			set
			{
				topLinePunctField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_OnOff autoSpaceDE
		{
			get
			{
				return autoSpaceDEField;
			}
			set
			{
				autoSpaceDEField = value;
			}
		}

		[XmlElement(Order = 17)]
		public CT_OnOff autoSpaceDN
		{
			get
			{
				return autoSpaceDNField;
			}
			set
			{
				autoSpaceDNField = value;
			}
		}

		[XmlElement(Order = 18)]
		public CT_OnOff bidi
		{
			get
			{
				return bidiField;
			}
			set
			{
				bidiField = value;
			}
		}

		[XmlElement(Order = 19)]
		public CT_OnOff adjustRightInd
		{
			get
			{
				return adjustRightIndField;
			}
			set
			{
				adjustRightIndField = value;
			}
		}

		[XmlElement(Order = 20)]
		public CT_OnOff snapToGrid
		{
			get
			{
				return snapToGridField;
			}
			set
			{
				snapToGridField = value;
			}
		}

		[XmlElement(Order = 21)]
		public CT_Spacing spacing
		{
			get
			{
				return spacingField;
			}
			set
			{
				spacingField = value;
			}
		}

		[XmlElement(Order = 22)]
		public CT_Ind ind
		{
			get
			{
				return indField;
			}
			set
			{
				indField = value;
			}
		}

		[XmlElement(Order = 23)]
		public CT_OnOff contextualSpacing
		{
			get
			{
				return contextualSpacingField;
			}
			set
			{
				contextualSpacingField = value;
			}
		}

		[XmlElement(Order = 24)]
		public CT_OnOff mirrorIndents
		{
			get
			{
				return mirrorIndentsField;
			}
			set
			{
				mirrorIndentsField = value;
			}
		}

		[XmlElement(Order = 25)]
		public CT_OnOff suppressOverlap
		{
			get
			{
				return suppressOverlapField;
			}
			set
			{
				suppressOverlapField = value;
			}
		}

		[XmlElement(Order = 26)]
		public CT_Jc jc
		{
			get
			{
				return jcField;
			}
			set
			{
				jcField = value;
			}
		}

		[XmlElement(Order = 27)]
		public CT_TextDirection textDirection
		{
			get
			{
				return textDirectionField;
			}
			set
			{
				textDirectionField = value;
			}
		}

		[XmlElement(Order = 28)]
		public CT_TextAlignment textAlignment
		{
			get
			{
				return textAlignmentField;
			}
			set
			{
				textAlignmentField = value;
			}
		}

		[XmlElement(Order = 29)]
		public CT_TextboxTightWrap textboxTightWrap
		{
			get
			{
				return textboxTightWrapField;
			}
			set
			{
				textboxTightWrapField = value;
			}
		}

		[XmlElement(Order = 30)]
		public CT_DecimalNumber outlineLvl
		{
			get
			{
				return outlineLvlField;
			}
			set
			{
				outlineLvlField = value;
			}
		}

		[XmlElement(Order = 31)]
		public CT_DecimalNumber divId
		{
			get
			{
				return divIdField;
			}
			set
			{
				divIdField = value;
			}
		}

		[XmlElement(Order = 32)]
		public CT_Cnf cnfStyle
		{
			get
			{
				return cnfStyleField;
			}
			set
			{
				cnfStyleField = value;
			}
		}

		public static CT_PPrBase Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PPrBase cT_PPrBase = new CT_PPrBase();
			cT_PPrBase.tabs = new List<CT_TabStop>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "pStyle")
				{
					cT_PPrBase.pStyle = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "keepNext")
				{
					cT_PPrBase.keepNext = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "keepLines")
				{
					cT_PPrBase.keepLines = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageBreakBefore")
				{
					cT_PPrBase.pageBreakBefore = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "framePr")
				{
					cT_PPrBase.framePr = CT_FramePr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "widowControl")
				{
					cT_PPrBase.widowControl = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numPr")
				{
					cT_PPrBase.numPr = CT_NumPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressLineNumbers")
				{
					cT_PPrBase.suppressLineNumbers = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pBdr")
				{
					cT_PPrBase.pBdr = CT_PBdr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_PPrBase.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressAutoHyphens")
				{
					cT_PPrBase.suppressAutoHyphens = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "kinsoku")
				{
					cT_PPrBase.kinsoku = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "wordWrap")
				{
					cT_PPrBase.wordWrap = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "overflowPunct")
				{
					cT_PPrBase.overflowPunct = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "topLinePunct")
				{
					cT_PPrBase.topLinePunct = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoSpaceDE")
				{
					cT_PPrBase.autoSpaceDE = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoSpaceDN")
				{
					cT_PPrBase.autoSpaceDN = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bidi")
				{
					cT_PPrBase.bidi = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "adjustRightInd")
				{
					cT_PPrBase.adjustRightInd = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "snapToGrid")
				{
					cT_PPrBase.snapToGrid = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spacing")
				{
					cT_PPrBase.spacing = CT_Spacing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ind")
				{
					cT_PPrBase.ind = CT_Ind.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "contextualSpacing")
				{
					cT_PPrBase.contextualSpacing = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mirrorIndents")
				{
					cT_PPrBase.mirrorIndents = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressOverlap")
				{
					cT_PPrBase.suppressOverlap = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "jc")
				{
					cT_PPrBase.jc = CT_Jc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textDirection")
				{
					cT_PPrBase.textDirection = CT_TextDirection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textAlignment")
				{
					cT_PPrBase.textAlignment = CT_TextAlignment.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textboxTightWrap")
				{
					cT_PPrBase.textboxTightWrap = CT_TextboxTightWrap.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "outlineLvl")
				{
					cT_PPrBase.outlineLvl = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "divId")
				{
					cT_PPrBase.divId = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cnfStyle")
				{
					cT_PPrBase.cnfStyle = CT_Cnf.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tabs")
				{
					foreach (XmlNode childNode2 in childNode.ChildNodes)
					{
						cT_PPrBase.tabs.Add(CT_TabStop.Parse(childNode2, namespaceManager));
					}
				}
			}
			return cT_PPrBase;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (pStyle != null)
			{
				pStyle.Write(sw, "pStyle");
			}
			if (tabs != null && tabs.Count > 0)
			{
				sw.Write("<w:tabs>");
				foreach (CT_TabStop tab in tabs)
				{
					tab.Write(sw, "tab");
				}
				sw.Write("</w:tabs>");
			}
			if (keepNext != null)
			{
				keepNext.Write(sw, "keepNext");
			}
			if (keepLines != null)
			{
				keepLines.Write(sw, "keepLines");
			}
			if (pageBreakBefore != null)
			{
				pageBreakBefore.Write(sw, "pageBreakBefore");
			}
			if (framePr != null)
			{
				framePr.Write(sw, "framePr");
			}
			if (widowControl != null)
			{
				widowControl.Write(sw, "widowControl");
			}
			if (numPr != null)
			{
				numPr.Write(sw, "numPr");
			}
			if (suppressLineNumbers != null)
			{
				suppressLineNumbers.Write(sw, "suppressLineNumbers");
			}
			if (pBdr != null)
			{
				pBdr.Write(sw, "pBdr");
			}
			if (shd != null)
			{
				shd.Write(sw, "shd");
			}
			if (suppressAutoHyphens != null)
			{
				suppressAutoHyphens.Write(sw, "suppressAutoHyphens");
			}
			if (kinsoku != null)
			{
				kinsoku.Write(sw, "kinsoku");
			}
			if (wordWrap != null)
			{
				wordWrap.Write(sw, "wordWrap");
			}
			if (overflowPunct != null)
			{
				overflowPunct.Write(sw, "overflowPunct");
			}
			if (topLinePunct != null)
			{
				topLinePunct.Write(sw, "topLinePunct");
			}
			if (autoSpaceDE != null)
			{
				autoSpaceDE.Write(sw, "autoSpaceDE");
			}
			if (autoSpaceDN != null)
			{
				autoSpaceDN.Write(sw, "autoSpaceDN");
			}
			if (bidi != null)
			{
				bidi.Write(sw, "bidi");
			}
			if (adjustRightInd != null)
			{
				adjustRightInd.Write(sw, "adjustRightInd");
			}
			if (snapToGrid != null)
			{
				snapToGrid.Write(sw, "snapToGrid");
			}
			if (spacing != null)
			{
				spacing.Write(sw, "spacing");
			}
			if (ind != null)
			{
				ind.Write(sw, "ind");
			}
			if (contextualSpacing != null)
			{
				contextualSpacing.Write(sw, "contextualSpacing");
			}
			if (mirrorIndents != null)
			{
				mirrorIndents.Write(sw, "mirrorIndents");
			}
			if (suppressOverlap != null)
			{
				suppressOverlap.Write(sw, "suppressOverlap");
			}
			if (jc != null)
			{
				jc.Write(sw, "jc");
			}
			if (textDirection != null)
			{
				textDirection.Write(sw, "textDirection");
			}
			if (textAlignment != null)
			{
				textAlignment.Write(sw, "textAlignment");
			}
			if (textboxTightWrap != null)
			{
				textboxTightWrap.Write(sw, "textboxTightWrap");
			}
			if (outlineLvl != null)
			{
				outlineLvl.Write(sw, "outlineLvl");
			}
			if (divId != null)
			{
				divId.Write(sw, "divId");
			}
			if (cnfStyle != null)
			{
				cnfStyle.Write(sw, "cnfStyle");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public bool IsSetTextAlignment()
		{
			if (textAlignmentField == null)
			{
				return false;
			}
			return textAlignmentField != null;
		}

		public CT_TextAlignment AddNewTextAlignment()
		{
			if (textAlignmentField == null)
			{
				textAlignmentField = new CT_TextAlignment();
			}
			return textAlignmentField;
		}

		public bool IsSetPStyle()
		{
			if (pStyleField == null)
			{
				return false;
			}
			return !string.IsNullOrEmpty(pStyleField.val);
		}

		public CT_String AddNewPStyle()
		{
			if (pStyleField == null)
			{
				pStyleField = new CT_String();
			}
			return pStyleField;
		}

		public bool IsSetShd()
		{
			return shdField != null;
		}

		public CT_Shd AddNewShd()
		{
			if (shdField == null)
			{
				shdField = new CT_Shd();
			}
			return shdField;
		}

		public bool IsSetJc()
		{
			return jcField != null;
		}

		public CT_Jc AddNewJc()
		{
			if (jcField == null)
			{
				jcField = new CT_Jc();
			}
			return jcField;
		}

		public bool IsSetPBdr()
		{
			return pBdrField != null;
		}

		public CT_Spacing AddNewSpacing()
		{
			if (spacingField == null)
			{
				spacingField = new CT_Spacing();
			}
			return spacingField;
		}

		public bool IsSetPageBreakBefore()
		{
			if (pageBreakBeforeField == null)
			{
				return false;
			}
			return pageBreakBeforeField.val;
		}

		public CT_OnOff AddNewPageBreakBefore()
		{
			if (pageBreakBeforeField == null)
			{
				pageBreakBeforeField = new CT_OnOff();
			}
			return pageBreakBeforeField;
		}

		public CT_PBdr AddNewPBdr()
		{
			if (pBdrField == null)
			{
				pBdrField = new CT_PBdr();
			}
			return pBdrField;
		}

		public bool IsSetWordWrap()
		{
			if (wordWrapField == null)
			{
				return false;
			}
			return wordWrapField.val;
		}

		public CT_OnOff AddNewWordWrap()
		{
			if (wordWrapField == null)
			{
				wordWrapField = new CT_OnOff();
			}
			return wordWrapField;
		}

		public CT_Ind AddNewInd()
		{
			if (indField == null)
			{
				indField = new CT_Ind();
			}
			return indField;
		}

		public CT_Tabs AddNewTabs()
		{
			CT_Tabs cT_Tabs = new CT_Tabs();
			tabsField = cT_Tabs.tab;
			return cT_Tabs;
		}
	}
}
