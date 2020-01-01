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
	public class CT_PPr : CT_PPrBase
	{
		private CT_ParaRPr rPrField;

		private CT_SectPr sectPrField;

		private CT_PPrChange pPrChangeField;

		[XmlElement(Order = 0)]
		public CT_ParaRPr rPr
		{
			get
			{
				return rPrField;
			}
			set
			{
				rPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_SectPr sectPr
		{
			get
			{
				return sectPrField;
			}
			set
			{
				sectPrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_PPrChange pPrChange
		{
			get
			{
				return pPrChangeField;
			}
			set
			{
				pPrChangeField = value;
			}
		}

		public new static CT_PPr Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_PPr cT_PPr = new CT_PPr();
			cT_PPr.tabs = new List<CT_TabStop>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "rPr")
				{
					cT_PPr.rPr = CT_ParaRPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "sectPr")
				{
					cT_PPr.sectPr = CT_SectPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pPrChange")
				{
					cT_PPr.pPrChange = CT_PPrChange.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pStyle")
				{
					cT_PPr.pStyle = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "keepNext")
				{
					cT_PPr.keepNext = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "keepLines")
				{
					cT_PPr.keepLines = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pageBreakBefore")
				{
					cT_PPr.pageBreakBefore = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "framePr")
				{
					cT_PPr.framePr = CT_FramePr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "widowControl")
				{
					cT_PPr.widowControl = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "numPr")
				{
					cT_PPr.numPr = CT_NumPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressLineNumbers")
				{
					cT_PPr.suppressLineNumbers = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "pBdr")
				{
					cT_PPr.pBdr = CT_PBdr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shd")
				{
					cT_PPr.shd = CT_Shd.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressAutoHyphens")
				{
					cT_PPr.suppressAutoHyphens = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "kinsoku")
				{
					cT_PPr.kinsoku = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "wordWrap")
				{
					cT_PPr.wordWrap = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "overflowPunct")
				{
					cT_PPr.overflowPunct = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "topLinePunct")
				{
					cT_PPr.topLinePunct = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoSpaceDE")
				{
					cT_PPr.autoSpaceDE = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoSpaceDN")
				{
					cT_PPr.autoSpaceDN = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bidi")
				{
					cT_PPr.bidi = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "adjustRightInd")
				{
					cT_PPr.adjustRightInd = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "snapToGrid")
				{
					cT_PPr.snapToGrid = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spacing")
				{
					cT_PPr.spacing = CT_Spacing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ind")
				{
					cT_PPr.ind = CT_Ind.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "contextualSpacing")
				{
					cT_PPr.contextualSpacing = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mirrorIndents")
				{
					cT_PPr.mirrorIndents = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressOverlap")
				{
					cT_PPr.suppressOverlap = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "jc")
				{
					cT_PPr.jc = CT_Jc.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textDirection")
				{
					cT_PPr.textDirection = CT_TextDirection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textAlignment")
				{
					cT_PPr.textAlignment = CT_TextAlignment.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "textboxTightWrap")
				{
					cT_PPr.textboxTightWrap = CT_TextboxTightWrap.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "outlineLvl")
				{
					cT_PPr.outlineLvl = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "divId")
				{
					cT_PPr.divId = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cnfStyle")
				{
					cT_PPr.cnfStyle = CT_Cnf.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "tabs")
				{
					foreach (XmlNode childNode2 in childNode.ChildNodes)
					{
						cT_PPr.tabs.Add(CT_TabStop.Parse(childNode2, namespaceManager));
					}
				}
			}
			return cT_PPr;
		}

		internal new void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (sectPr != null)
			{
				sectPr.Write(sw, "sectPr");
			}
			if (pPrChange != null)
			{
				pPrChange.Write(sw, "pPrChange");
			}
			if (base.pStyle != null)
			{
				base.pStyle.Write(sw, "pStyle");
			}
			if (base.keepNext != null)
			{
				base.keepNext.Write(sw, "keepNext");
			}
			if (base.keepLines != null)
			{
				base.keepLines.Write(sw, "keepLines");
			}
			if (base.pageBreakBefore != null)
			{
				base.pageBreakBefore.Write(sw, "pageBreakBefore");
			}
			if (base.framePr != null)
			{
				base.framePr.Write(sw, "framePr");
			}
			if (base.widowControl != null)
			{
				base.widowControl.Write(sw, "widowControl");
			}
			if (base.numPr != null)
			{
				base.numPr.Write(sw, "numPr");
			}
			if (base.pBdr != null)
			{
				base.pBdr.Write(sw, "pBdr");
			}
			if (base.tabs != null && base.tabs.Count > 0)
			{
				sw.Write("<w:tabs>");
				foreach (CT_TabStop tab in base.tabs)
				{
					tab.Write(sw, "tab");
				}
				sw.Write("</w:tabs>");
			}
			if (base.suppressLineNumbers != null)
			{
				base.suppressLineNumbers.Write(sw, "suppressLineNumbers");
			}
			if (base.shd != null)
			{
				base.shd.Write(sw, "shd");
			}
			if (base.suppressAutoHyphens != null)
			{
				base.suppressAutoHyphens.Write(sw, "suppressAutoHyphens");
			}
			if (base.kinsoku != null)
			{
				base.kinsoku.Write(sw, "kinsoku");
			}
			if (base.wordWrap != null)
			{
				base.wordWrap.Write(sw, "wordWrap");
			}
			if (base.overflowPunct != null)
			{
				base.overflowPunct.Write(sw, "overflowPunct");
			}
			if (base.topLinePunct != null)
			{
				base.topLinePunct.Write(sw, "topLinePunct");
			}
			if (base.autoSpaceDE != null)
			{
				base.autoSpaceDE.Write(sw, "autoSpaceDE");
			}
			if (base.autoSpaceDN != null)
			{
				base.autoSpaceDN.Write(sw, "autoSpaceDN");
			}
			if (base.bidi != null)
			{
				base.bidi.Write(sw, "bidi");
			}
			if (base.adjustRightInd != null)
			{
				base.adjustRightInd.Write(sw, "adjustRightInd");
			}
			if (base.snapToGrid != null)
			{
				base.snapToGrid.Write(sw, "snapToGrid");
			}
			if (base.spacing != null)
			{
				base.spacing.Write(sw, "spacing");
			}
			if (base.ind != null)
			{
				base.ind.Write(sw, "ind");
			}
			if (base.contextualSpacing != null)
			{
				base.contextualSpacing.Write(sw, "contextualSpacing");
			}
			if (base.mirrorIndents != null)
			{
				base.mirrorIndents.Write(sw, "mirrorIndents");
			}
			if (base.suppressOverlap != null)
			{
				base.suppressOverlap.Write(sw, "suppressOverlap");
			}
			if (base.jc != null)
			{
				base.jc.Write(sw, "jc");
			}
			if (base.outlineLvl != null)
			{
				base.outlineLvl.Write(sw, "outlineLvl");
			}
			if (rPr != null)
			{
				rPr.Write(sw, "rPr");
			}
			if (base.textDirection != null)
			{
				base.textDirection.Write(sw, "textDirection");
			}
			if (base.textAlignment != null)
			{
				base.textAlignment.Write(sw, "textAlignment");
			}
			if (base.textboxTightWrap != null)
			{
				base.textboxTightWrap.Write(sw, "textboxTightWrap");
			}
			if (base.divId != null)
			{
				base.divId.Write(sw, "divId");
			}
			if (base.cnfStyle != null)
			{
				base.cnfStyle.Write(sw, "cnfStyle");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_ParaRPr AddNewRPr()
		{
			if (rPrField == null)
			{
				rPrField = new CT_ParaRPr();
			}
			return rPrField;
		}

		public CT_NumPr AddNewNumPr()
		{
			if (base.numPr == null)
			{
				base.numPr = new CT_NumPr();
			}
			return base.numPr;
		}
	}
}
