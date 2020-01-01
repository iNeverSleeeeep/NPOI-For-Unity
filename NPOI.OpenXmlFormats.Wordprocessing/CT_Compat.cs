using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Compat
	{
		private CT_OnOff useSingleBorderforContiguousCellsField;

		private CT_OnOff wpJustificationField;

		private CT_OnOff noTabHangIndField;

		private CT_OnOff noLeadingField;

		private CT_OnOff spaceForULField;

		private CT_OnOff noColumnBalanceField;

		private CT_OnOff balanceSingleByteDoubleByteWidthField;

		private CT_OnOff noExtraLineSpacingField;

		private CT_OnOff doNotLeaveBackslashAloneField;

		private CT_OnOff ulTrailSpaceField;

		private CT_OnOff doNotExpandShiftReturnField;

		private CT_OnOff spacingInWholePointsField;

		private CT_OnOff lineWrapLikeWord6Field;

		private CT_OnOff printBodyTextBeforeHeaderField;

		private CT_OnOff printColBlackField;

		private CT_OnOff wpSpaceWidthField;

		private CT_OnOff showBreaksInFramesField;

		private CT_OnOff subFontBySizeField;

		private CT_OnOff suppressBottomSpacingField;

		private CT_OnOff suppressTopSpacingField;

		private CT_OnOff suppressSpacingAtTopOfPageField;

		private CT_OnOff suppressTopSpacingWPField;

		private CT_OnOff suppressSpBfAfterPgBrkField;

		private CT_OnOff swapBordersFacingPagesField;

		private CT_OnOff convMailMergeEscField;

		private CT_OnOff truncateFontHeightsLikeWP6Field;

		private CT_OnOff mwSmallCapsField;

		private CT_OnOff usePrinterMetricsField;

		private CT_OnOff doNotSuppressParagraphBordersField;

		private CT_OnOff wrapTrailSpacesField;

		private CT_OnOff footnoteLayoutLikeWW8Field;

		private CT_OnOff shapeLayoutLikeWW8Field;

		private CT_OnOff alignTablesRowByRowField;

		private CT_OnOff forgetLastTabAlignmentField;

		private CT_OnOff adjustLineHeightInTableField;

		private CT_OnOff autoSpaceLikeWord95Field;

		private CT_OnOff noSpaceRaiseLowerField;

		private CT_OnOff doNotUseHTMLParagraphAutoSpacingField;

		private CT_OnOff layoutRawTableWidthField;

		private CT_OnOff layoutTableRowsApartField;

		private CT_OnOff useWord97LineBreakRulesField;

		private CT_OnOff doNotBreakWrappedTablesField;

		private CT_OnOff doNotSnapToGridInCellField;

		private CT_OnOff selectFldWithFirstOrLastCharField;

		private CT_OnOff applyBreakingRulesField;

		private CT_OnOff doNotWrapTextWithPunctField;

		private CT_OnOff doNotUseEastAsianBreakRulesField;

		private CT_OnOff useWord2002TableStyleRulesField;

		private CT_OnOff growAutofitField;

		private CT_OnOff useFELayoutField;

		private CT_OnOff useNormalStyleForListField;

		private CT_OnOff doNotUseIndentAsNumberingTabStopField;

		private CT_OnOff useAltKinsokuLineBreakRulesField;

		private CT_OnOff allowSpaceOfSameStyleInTableField;

		private CT_OnOff doNotSuppressIndentationField;

		private CT_OnOff doNotAutofitConstrainedTablesField;

		private CT_OnOff autofitToFirstFixedWidthCellField;

		private CT_OnOff underlineTabInNumListField;

		private CT_OnOff displayHangulFixedWidthField;

		private CT_OnOff splitPgBreakAndParaMarkField;

		private CT_OnOff doNotVertAlignCellWithSpField;

		private CT_OnOff doNotBreakConstrainedForcedTableField;

		private CT_OnOff doNotVertAlignInTxbxField;

		private CT_OnOff useAnsiKerningPairsField;

		private CT_OnOff cachedColBalanceField;

		[XmlElement(Order = 0)]
		public CT_OnOff useSingleBorderforContiguousCells
		{
			get
			{
				return useSingleBorderforContiguousCellsField;
			}
			set
			{
				useSingleBorderforContiguousCellsField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OnOff wpJustification
		{
			get
			{
				return wpJustificationField;
			}
			set
			{
				wpJustificationField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_OnOff noTabHangInd
		{
			get
			{
				return noTabHangIndField;
			}
			set
			{
				noTabHangIndField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff noLeading
		{
			get
			{
				return noLeadingField;
			}
			set
			{
				noLeadingField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff spaceForUL
		{
			get
			{
				return spaceForULField;
			}
			set
			{
				spaceForULField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_OnOff noColumnBalance
		{
			get
			{
				return noColumnBalanceField;
			}
			set
			{
				noColumnBalanceField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_OnOff balanceSingleByteDoubleByteWidth
		{
			get
			{
				return balanceSingleByteDoubleByteWidthField;
			}
			set
			{
				balanceSingleByteDoubleByteWidthField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OnOff noExtraLineSpacing
		{
			get
			{
				return noExtraLineSpacingField;
			}
			set
			{
				noExtraLineSpacingField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_OnOff doNotLeaveBackslashAlone
		{
			get
			{
				return doNotLeaveBackslashAloneField;
			}
			set
			{
				doNotLeaveBackslashAloneField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_OnOff ulTrailSpace
		{
			get
			{
				return ulTrailSpaceField;
			}
			set
			{
				ulTrailSpaceField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_OnOff doNotExpandShiftReturn
		{
			get
			{
				return doNotExpandShiftReturnField;
			}
			set
			{
				doNotExpandShiftReturnField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_OnOff spacingInWholePoints
		{
			get
			{
				return spacingInWholePointsField;
			}
			set
			{
				spacingInWholePointsField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_OnOff lineWrapLikeWord6
		{
			get
			{
				return lineWrapLikeWord6Field;
			}
			set
			{
				lineWrapLikeWord6Field = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_OnOff printBodyTextBeforeHeader
		{
			get
			{
				return printBodyTextBeforeHeaderField;
			}
			set
			{
				printBodyTextBeforeHeaderField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_OnOff printColBlack
		{
			get
			{
				return printColBlackField;
			}
			set
			{
				printColBlackField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_OnOff wpSpaceWidth
		{
			get
			{
				return wpSpaceWidthField;
			}
			set
			{
				wpSpaceWidthField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_OnOff showBreaksInFrames
		{
			get
			{
				return showBreaksInFramesField;
			}
			set
			{
				showBreaksInFramesField = value;
			}
		}

		[XmlElement(Order = 17)]
		public CT_OnOff subFontBySize
		{
			get
			{
				return subFontBySizeField;
			}
			set
			{
				subFontBySizeField = value;
			}
		}

		[XmlElement(Order = 18)]
		public CT_OnOff suppressBottomSpacing
		{
			get
			{
				return suppressBottomSpacingField;
			}
			set
			{
				suppressBottomSpacingField = value;
			}
		}

		[XmlElement(Order = 19)]
		public CT_OnOff suppressTopSpacing
		{
			get
			{
				return suppressTopSpacingField;
			}
			set
			{
				suppressTopSpacingField = value;
			}
		}

		[XmlElement(Order = 20)]
		public CT_OnOff suppressSpacingAtTopOfPage
		{
			get
			{
				return suppressSpacingAtTopOfPageField;
			}
			set
			{
				suppressSpacingAtTopOfPageField = value;
			}
		}

		[XmlElement(Order = 21)]
		public CT_OnOff suppressTopSpacingWP
		{
			get
			{
				return suppressTopSpacingWPField;
			}
			set
			{
				suppressTopSpacingWPField = value;
			}
		}

		[XmlElement(Order = 22)]
		public CT_OnOff suppressSpBfAfterPgBrk
		{
			get
			{
				return suppressSpBfAfterPgBrkField;
			}
			set
			{
				suppressSpBfAfterPgBrkField = value;
			}
		}

		[XmlElement(Order = 23)]
		public CT_OnOff swapBordersFacingPages
		{
			get
			{
				return swapBordersFacingPagesField;
			}
			set
			{
				swapBordersFacingPagesField = value;
			}
		}

		[XmlElement(Order = 24)]
		public CT_OnOff convMailMergeEsc
		{
			get
			{
				return convMailMergeEscField;
			}
			set
			{
				convMailMergeEscField = value;
			}
		}

		[XmlElement(Order = 25)]
		public CT_OnOff truncateFontHeightsLikeWP6
		{
			get
			{
				return truncateFontHeightsLikeWP6Field;
			}
			set
			{
				truncateFontHeightsLikeWP6Field = value;
			}
		}

		[XmlElement(Order = 26)]
		public CT_OnOff mwSmallCaps
		{
			get
			{
				return mwSmallCapsField;
			}
			set
			{
				mwSmallCapsField = value;
			}
		}

		[XmlElement(Order = 27)]
		public CT_OnOff usePrinterMetrics
		{
			get
			{
				return usePrinterMetricsField;
			}
			set
			{
				usePrinterMetricsField = value;
			}
		}

		[XmlElement(Order = 28)]
		public CT_OnOff doNotSuppressParagraphBorders
		{
			get
			{
				return doNotSuppressParagraphBordersField;
			}
			set
			{
				doNotSuppressParagraphBordersField = value;
			}
		}

		[XmlElement(Order = 29)]
		public CT_OnOff wrapTrailSpaces
		{
			get
			{
				return wrapTrailSpacesField;
			}
			set
			{
				wrapTrailSpacesField = value;
			}
		}

		[XmlElement(Order = 30)]
		public CT_OnOff footnoteLayoutLikeWW8
		{
			get
			{
				return footnoteLayoutLikeWW8Field;
			}
			set
			{
				footnoteLayoutLikeWW8Field = value;
			}
		}

		[XmlElement(Order = 31)]
		public CT_OnOff shapeLayoutLikeWW8
		{
			get
			{
				return shapeLayoutLikeWW8Field;
			}
			set
			{
				shapeLayoutLikeWW8Field = value;
			}
		}

		[XmlElement(Order = 32)]
		public CT_OnOff alignTablesRowByRow
		{
			get
			{
				return alignTablesRowByRowField;
			}
			set
			{
				alignTablesRowByRowField = value;
			}
		}

		[XmlElement(Order = 33)]
		public CT_OnOff forgetLastTabAlignment
		{
			get
			{
				return forgetLastTabAlignmentField;
			}
			set
			{
				forgetLastTabAlignmentField = value;
			}
		}

		[XmlElement(Order = 34)]
		public CT_OnOff adjustLineHeightInTable
		{
			get
			{
				return adjustLineHeightInTableField;
			}
			set
			{
				adjustLineHeightInTableField = value;
			}
		}

		[XmlElement(Order = 35)]
		public CT_OnOff autoSpaceLikeWord95
		{
			get
			{
				return autoSpaceLikeWord95Field;
			}
			set
			{
				autoSpaceLikeWord95Field = value;
			}
		}

		[XmlElement(Order = 36)]
		public CT_OnOff noSpaceRaiseLower
		{
			get
			{
				return noSpaceRaiseLowerField;
			}
			set
			{
				noSpaceRaiseLowerField = value;
			}
		}

		[XmlElement(Order = 37)]
		public CT_OnOff doNotUseHTMLParagraphAutoSpacing
		{
			get
			{
				return doNotUseHTMLParagraphAutoSpacingField;
			}
			set
			{
				doNotUseHTMLParagraphAutoSpacingField = value;
			}
		}

		[XmlElement(Order = 38)]
		public CT_OnOff layoutRawTableWidth
		{
			get
			{
				return layoutRawTableWidthField;
			}
			set
			{
				layoutRawTableWidthField = value;
			}
		}

		[XmlElement(Order = 39)]
		public CT_OnOff layoutTableRowsApart
		{
			get
			{
				return layoutTableRowsApartField;
			}
			set
			{
				layoutTableRowsApartField = value;
			}
		}

		[XmlElement(Order = 40)]
		public CT_OnOff useWord97LineBreakRules
		{
			get
			{
				return useWord97LineBreakRulesField;
			}
			set
			{
				useWord97LineBreakRulesField = value;
			}
		}

		[XmlElement(Order = 41)]
		public CT_OnOff doNotBreakWrappedTables
		{
			get
			{
				return doNotBreakWrappedTablesField;
			}
			set
			{
				doNotBreakWrappedTablesField = value;
			}
		}

		[XmlElement(Order = 42)]
		public CT_OnOff doNotSnapToGridInCell
		{
			get
			{
				return doNotSnapToGridInCellField;
			}
			set
			{
				doNotSnapToGridInCellField = value;
			}
		}

		[XmlElement(Order = 43)]
		public CT_OnOff selectFldWithFirstOrLastChar
		{
			get
			{
				return selectFldWithFirstOrLastCharField;
			}
			set
			{
				selectFldWithFirstOrLastCharField = value;
			}
		}

		[XmlElement(Order = 44)]
		public CT_OnOff applyBreakingRules
		{
			get
			{
				return applyBreakingRulesField;
			}
			set
			{
				applyBreakingRulesField = value;
			}
		}

		[XmlElement(Order = 45)]
		public CT_OnOff doNotWrapTextWithPunct
		{
			get
			{
				return doNotWrapTextWithPunctField;
			}
			set
			{
				doNotWrapTextWithPunctField = value;
			}
		}

		[XmlElement(Order = 46)]
		public CT_OnOff doNotUseEastAsianBreakRules
		{
			get
			{
				return doNotUseEastAsianBreakRulesField;
			}
			set
			{
				doNotUseEastAsianBreakRulesField = value;
			}
		}

		[XmlElement(Order = 47)]
		public CT_OnOff useWord2002TableStyleRules
		{
			get
			{
				return useWord2002TableStyleRulesField;
			}
			set
			{
				useWord2002TableStyleRulesField = value;
			}
		}

		[XmlElement(Order = 48)]
		public CT_OnOff growAutofit
		{
			get
			{
				return growAutofitField;
			}
			set
			{
				growAutofitField = value;
			}
		}

		[XmlElement(Order = 49)]
		public CT_OnOff useFELayout
		{
			get
			{
				return useFELayoutField;
			}
			set
			{
				useFELayoutField = value;
			}
		}

		[XmlElement(Order = 50)]
		public CT_OnOff useNormalStyleForList
		{
			get
			{
				return useNormalStyleForListField;
			}
			set
			{
				useNormalStyleForListField = value;
			}
		}

		[XmlElement(Order = 51)]
		public CT_OnOff doNotUseIndentAsNumberingTabStop
		{
			get
			{
				return doNotUseIndentAsNumberingTabStopField;
			}
			set
			{
				doNotUseIndentAsNumberingTabStopField = value;
			}
		}

		[XmlElement(Order = 52)]
		public CT_OnOff useAltKinsokuLineBreakRules
		{
			get
			{
				return useAltKinsokuLineBreakRulesField;
			}
			set
			{
				useAltKinsokuLineBreakRulesField = value;
			}
		}

		[XmlElement(Order = 53)]
		public CT_OnOff allowSpaceOfSameStyleInTable
		{
			get
			{
				return allowSpaceOfSameStyleInTableField;
			}
			set
			{
				allowSpaceOfSameStyleInTableField = value;
			}
		}

		[XmlElement(Order = 54)]
		public CT_OnOff doNotSuppressIndentation
		{
			get
			{
				return doNotSuppressIndentationField;
			}
			set
			{
				doNotSuppressIndentationField = value;
			}
		}

		[XmlElement(Order = 55)]
		public CT_OnOff doNotAutofitConstrainedTables
		{
			get
			{
				return doNotAutofitConstrainedTablesField;
			}
			set
			{
				doNotAutofitConstrainedTablesField = value;
			}
		}

		[XmlElement(Order = 56)]
		public CT_OnOff autofitToFirstFixedWidthCell
		{
			get
			{
				return autofitToFirstFixedWidthCellField;
			}
			set
			{
				autofitToFirstFixedWidthCellField = value;
			}
		}

		[XmlElement(Order = 57)]
		public CT_OnOff underlineTabInNumList
		{
			get
			{
				return underlineTabInNumListField;
			}
			set
			{
				underlineTabInNumListField = value;
			}
		}

		[XmlElement(Order = 58)]
		public CT_OnOff displayHangulFixedWidth
		{
			get
			{
				return displayHangulFixedWidthField;
			}
			set
			{
				displayHangulFixedWidthField = value;
			}
		}

		[XmlElement(Order = 59)]
		public CT_OnOff splitPgBreakAndParaMark
		{
			get
			{
				return splitPgBreakAndParaMarkField;
			}
			set
			{
				splitPgBreakAndParaMarkField = value;
			}
		}

		[XmlElement(Order = 60)]
		public CT_OnOff doNotVertAlignCellWithSp
		{
			get
			{
				return doNotVertAlignCellWithSpField;
			}
			set
			{
				doNotVertAlignCellWithSpField = value;
			}
		}

		[XmlElement(Order = 61)]
		public CT_OnOff doNotBreakConstrainedForcedTable
		{
			get
			{
				return doNotBreakConstrainedForcedTableField;
			}
			set
			{
				doNotBreakConstrainedForcedTableField = value;
			}
		}

		[XmlElement(Order = 62)]
		public CT_OnOff doNotVertAlignInTxbx
		{
			get
			{
				return doNotVertAlignInTxbxField;
			}
			set
			{
				doNotVertAlignInTxbxField = value;
			}
		}

		[XmlElement(Order = 63)]
		public CT_OnOff useAnsiKerningPairs
		{
			get
			{
				return useAnsiKerningPairsField;
			}
			set
			{
				useAnsiKerningPairsField = value;
			}
		}

		[XmlElement(Order = 64)]
		public CT_OnOff cachedColBalance
		{
			get
			{
				return cachedColBalanceField;
			}
			set
			{
				cachedColBalanceField = value;
			}
		}

		public static CT_Compat Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Compat cT_Compat = new CT_Compat();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "useSingleBorderforContiguousCells")
				{
					cT_Compat.useSingleBorderforContiguousCells = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "wpJustification")
				{
					cT_Compat.wpJustification = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noTabHangInd")
				{
					cT_Compat.noTabHangInd = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noLeading")
				{
					cT_Compat.noLeading = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spaceForUL")
				{
					cT_Compat.spaceForUL = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noColumnBalance")
				{
					cT_Compat.noColumnBalance = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "balanceSingleByteDoubleByteWidth")
				{
					cT_Compat.balanceSingleByteDoubleByteWidth = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noExtraLineSpacing")
				{
					cT_Compat.noExtraLineSpacing = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotLeaveBackslashAlone")
				{
					cT_Compat.doNotLeaveBackslashAlone = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ulTrailSpace")
				{
					cT_Compat.ulTrailSpace = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotExpandShiftReturn")
				{
					cT_Compat.doNotExpandShiftReturn = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "spacingInWholePoints")
				{
					cT_Compat.spacingInWholePoints = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "lineWrapLikeWord6")
				{
					cT_Compat.lineWrapLikeWord6 = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printBodyTextBeforeHeader")
				{
					cT_Compat.printBodyTextBeforeHeader = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printColBlack")
				{
					cT_Compat.printColBlack = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "wpSpaceWidth")
				{
					cT_Compat.wpSpaceWidth = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showBreaksInFrames")
				{
					cT_Compat.showBreaksInFrames = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "subFontBySize")
				{
					cT_Compat.subFontBySize = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressBottomSpacing")
				{
					cT_Compat.suppressBottomSpacing = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressTopSpacing")
				{
					cT_Compat.suppressTopSpacing = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressSpacingAtTopOfPage")
				{
					cT_Compat.suppressSpacingAtTopOfPage = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressTopSpacingWP")
				{
					cT_Compat.suppressTopSpacingWP = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "suppressSpBfAfterPgBrk")
				{
					cT_Compat.suppressSpBfAfterPgBrk = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "swapBordersFacingPages")
				{
					cT_Compat.swapBordersFacingPages = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "convMailMergeEsc")
				{
					cT_Compat.convMailMergeEsc = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "truncateFontHeightsLikeWP6")
				{
					cT_Compat.truncateFontHeightsLikeWP6 = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mwSmallCaps")
				{
					cT_Compat.mwSmallCaps = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "usePrinterMetrics")
				{
					cT_Compat.usePrinterMetrics = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotSuppressParagraphBorders")
				{
					cT_Compat.doNotSuppressParagraphBorders = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "wrapTrailSpaces")
				{
					cT_Compat.wrapTrailSpaces = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "footnoteLayoutLikeWW8")
				{
					cT_Compat.footnoteLayoutLikeWW8 = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "shapeLayoutLikeWW8")
				{
					cT_Compat.shapeLayoutLikeWW8 = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "alignTablesRowByRow")
				{
					cT_Compat.alignTablesRowByRow = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "forgetLastTabAlignment")
				{
					cT_Compat.forgetLastTabAlignment = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "adjustLineHeightInTable")
				{
					cT_Compat.adjustLineHeightInTable = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoSpaceLikeWord95")
				{
					cT_Compat.autoSpaceLikeWord95 = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noSpaceRaiseLower")
				{
					cT_Compat.noSpaceRaiseLower = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotUseHTMLParagraphAutoSpacing")
				{
					cT_Compat.doNotUseHTMLParagraphAutoSpacing = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "layoutRawTableWidth")
				{
					cT_Compat.layoutRawTableWidth = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "layoutTableRowsApart")
				{
					cT_Compat.layoutTableRowsApart = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "useWord97LineBreakRules")
				{
					cT_Compat.useWord97LineBreakRules = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotBreakWrappedTables")
				{
					cT_Compat.doNotBreakWrappedTables = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotSnapToGridInCell")
				{
					cT_Compat.doNotSnapToGridInCell = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "selectFldWithFirstOrLastChar")
				{
					cT_Compat.selectFldWithFirstOrLastChar = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "applyBreakingRules")
				{
					cT_Compat.applyBreakingRules = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotWrapTextWithPunct")
				{
					cT_Compat.doNotWrapTextWithPunct = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotUseEastAsianBreakRules")
				{
					cT_Compat.doNotUseEastAsianBreakRules = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "useWord2002TableStyleRules")
				{
					cT_Compat.useWord2002TableStyleRules = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "growAutofit")
				{
					cT_Compat.growAutofit = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "useFELayout")
				{
					cT_Compat.useFELayout = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "useNormalStyleForList")
				{
					cT_Compat.useNormalStyleForList = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotUseIndentAsNumberingTabStop")
				{
					cT_Compat.doNotUseIndentAsNumberingTabStop = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "useAltKinsokuLineBreakRules")
				{
					cT_Compat.useAltKinsokuLineBreakRules = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "allowSpaceOfSameStyleInTable")
				{
					cT_Compat.allowSpaceOfSameStyleInTable = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotSuppressIndentation")
				{
					cT_Compat.doNotSuppressIndentation = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotAutofitConstrainedTables")
				{
					cT_Compat.doNotAutofitConstrainedTables = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autofitToFirstFixedWidthCell")
				{
					cT_Compat.autofitToFirstFixedWidthCell = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "underlineTabInNumList")
				{
					cT_Compat.underlineTabInNumList = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "displayHangulFixedWidth")
				{
					cT_Compat.displayHangulFixedWidth = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "splitPgBreakAndParaMark")
				{
					cT_Compat.splitPgBreakAndParaMark = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotVertAlignCellWithSp")
				{
					cT_Compat.doNotVertAlignCellWithSp = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotBreakConstrainedForcedTable")
				{
					cT_Compat.doNotBreakConstrainedForcedTable = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotVertAlignInTxbx")
				{
					cT_Compat.doNotVertAlignInTxbx = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "useAnsiKerningPairs")
				{
					cT_Compat.useAnsiKerningPairs = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "cachedColBalance")
				{
					cT_Compat.cachedColBalance = CT_OnOff.Parse(childNode, namespaceManager);
				}
			}
			return cT_Compat;
		}

		internal void Write(StreamWriter sw, string nodeName)
		{
			sw.Write(string.Format("<w:{0}", nodeName));
			sw.Write(">");
			if (useSingleBorderforContiguousCells != null)
			{
				useSingleBorderforContiguousCells.Write(sw, "useSingleBorderforContiguousCells");
			}
			if (wpJustification != null)
			{
				wpJustification.Write(sw, "wpJustification");
			}
			if (noTabHangInd != null)
			{
				noTabHangInd.Write(sw, "noTabHangInd");
			}
			if (noLeading != null)
			{
				noLeading.Write(sw, "noLeading");
			}
			if (spaceForUL != null)
			{
				spaceForUL.Write(sw, "spaceForUL");
			}
			if (noColumnBalance != null)
			{
				noColumnBalance.Write(sw, "noColumnBalance");
			}
			if (balanceSingleByteDoubleByteWidth != null)
			{
				balanceSingleByteDoubleByteWidth.Write(sw, "balanceSingleByteDoubleByteWidth");
			}
			if (noExtraLineSpacing != null)
			{
				noExtraLineSpacing.Write(sw, "noExtraLineSpacing");
			}
			if (doNotLeaveBackslashAlone != null)
			{
				doNotLeaveBackslashAlone.Write(sw, "doNotLeaveBackslashAlone");
			}
			if (ulTrailSpace != null)
			{
				ulTrailSpace.Write(sw, "ulTrailSpace");
			}
			if (doNotExpandShiftReturn != null)
			{
				doNotExpandShiftReturn.Write(sw, "doNotExpandShiftReturn");
			}
			if (spacingInWholePoints != null)
			{
				spacingInWholePoints.Write(sw, "spacingInWholePoints");
			}
			if (lineWrapLikeWord6 != null)
			{
				lineWrapLikeWord6.Write(sw, "lineWrapLikeWord6");
			}
			if (printBodyTextBeforeHeader != null)
			{
				printBodyTextBeforeHeader.Write(sw, "printBodyTextBeforeHeader");
			}
			if (printColBlack != null)
			{
				printColBlack.Write(sw, "printColBlack");
			}
			if (wpSpaceWidth != null)
			{
				wpSpaceWidth.Write(sw, "wpSpaceWidth");
			}
			if (showBreaksInFrames != null)
			{
				showBreaksInFrames.Write(sw, "showBreaksInFrames");
			}
			if (subFontBySize != null)
			{
				subFontBySize.Write(sw, "subFontBySize");
			}
			if (suppressBottomSpacing != null)
			{
				suppressBottomSpacing.Write(sw, "suppressBottomSpacing");
			}
			if (suppressTopSpacing != null)
			{
				suppressTopSpacing.Write(sw, "suppressTopSpacing");
			}
			if (suppressSpacingAtTopOfPage != null)
			{
				suppressSpacingAtTopOfPage.Write(sw, "suppressSpacingAtTopOfPage");
			}
			if (suppressTopSpacingWP != null)
			{
				suppressTopSpacingWP.Write(sw, "suppressTopSpacingWP");
			}
			if (suppressSpBfAfterPgBrk != null)
			{
				suppressSpBfAfterPgBrk.Write(sw, "suppressSpBfAfterPgBrk");
			}
			if (swapBordersFacingPages != null)
			{
				swapBordersFacingPages.Write(sw, "swapBordersFacingPages");
			}
			if (convMailMergeEsc != null)
			{
				convMailMergeEsc.Write(sw, "convMailMergeEsc");
			}
			if (truncateFontHeightsLikeWP6 != null)
			{
				truncateFontHeightsLikeWP6.Write(sw, "truncateFontHeightsLikeWP6");
			}
			if (mwSmallCaps != null)
			{
				mwSmallCaps.Write(sw, "mwSmallCaps");
			}
			if (usePrinterMetrics != null)
			{
				usePrinterMetrics.Write(sw, "usePrinterMetrics");
			}
			if (doNotSuppressParagraphBorders != null)
			{
				doNotSuppressParagraphBorders.Write(sw, "doNotSuppressParagraphBorders");
			}
			if (wrapTrailSpaces != null)
			{
				wrapTrailSpaces.Write(sw, "wrapTrailSpaces");
			}
			if (footnoteLayoutLikeWW8 != null)
			{
				footnoteLayoutLikeWW8.Write(sw, "footnoteLayoutLikeWW8");
			}
			if (shapeLayoutLikeWW8 != null)
			{
				shapeLayoutLikeWW8.Write(sw, "shapeLayoutLikeWW8");
			}
			if (alignTablesRowByRow != null)
			{
				alignTablesRowByRow.Write(sw, "alignTablesRowByRow");
			}
			if (forgetLastTabAlignment != null)
			{
				forgetLastTabAlignment.Write(sw, "forgetLastTabAlignment");
			}
			if (adjustLineHeightInTable != null)
			{
				adjustLineHeightInTable.Write(sw, "adjustLineHeightInTable");
			}
			if (autoSpaceLikeWord95 != null)
			{
				autoSpaceLikeWord95.Write(sw, "autoSpaceLikeWord95");
			}
			if (noSpaceRaiseLower != null)
			{
				noSpaceRaiseLower.Write(sw, "noSpaceRaiseLower");
			}
			if (doNotUseHTMLParagraphAutoSpacing != null)
			{
				doNotUseHTMLParagraphAutoSpacing.Write(sw, "doNotUseHTMLParagraphAutoSpacing");
			}
			if (layoutRawTableWidth != null)
			{
				layoutRawTableWidth.Write(sw, "layoutRawTableWidth");
			}
			if (layoutTableRowsApart != null)
			{
				layoutTableRowsApart.Write(sw, "layoutTableRowsApart");
			}
			if (useWord97LineBreakRules != null)
			{
				useWord97LineBreakRules.Write(sw, "useWord97LineBreakRules");
			}
			if (doNotBreakWrappedTables != null)
			{
				doNotBreakWrappedTables.Write(sw, "doNotBreakWrappedTables");
			}
			if (doNotSnapToGridInCell != null)
			{
				doNotSnapToGridInCell.Write(sw, "doNotSnapToGridInCell");
			}
			if (selectFldWithFirstOrLastChar != null)
			{
				selectFldWithFirstOrLastChar.Write(sw, "selectFldWithFirstOrLastChar");
			}
			if (applyBreakingRules != null)
			{
				applyBreakingRules.Write(sw, "applyBreakingRules");
			}
			if (doNotWrapTextWithPunct != null)
			{
				doNotWrapTextWithPunct.Write(sw, "doNotWrapTextWithPunct");
			}
			if (doNotUseEastAsianBreakRules != null)
			{
				doNotUseEastAsianBreakRules.Write(sw, "doNotUseEastAsianBreakRules");
			}
			if (useWord2002TableStyleRules != null)
			{
				useWord2002TableStyleRules.Write(sw, "useWord2002TableStyleRules");
			}
			if (growAutofit != null)
			{
				growAutofit.Write(sw, "growAutofit");
			}
			if (useFELayout != null)
			{
				useFELayout.Write(sw, "useFELayout");
			}
			if (useNormalStyleForList != null)
			{
				useNormalStyleForList.Write(sw, "useNormalStyleForList");
			}
			if (doNotUseIndentAsNumberingTabStop != null)
			{
				doNotUseIndentAsNumberingTabStop.Write(sw, "doNotUseIndentAsNumberingTabStop");
			}
			if (useAltKinsokuLineBreakRules != null)
			{
				useAltKinsokuLineBreakRules.Write(sw, "useAltKinsokuLineBreakRules");
			}
			if (allowSpaceOfSameStyleInTable != null)
			{
				allowSpaceOfSameStyleInTable.Write(sw, "allowSpaceOfSameStyleInTable");
			}
			if (doNotSuppressIndentation != null)
			{
				doNotSuppressIndentation.Write(sw, "doNotSuppressIndentation");
			}
			if (doNotAutofitConstrainedTables != null)
			{
				doNotAutofitConstrainedTables.Write(sw, "doNotAutofitConstrainedTables");
			}
			if (autofitToFirstFixedWidthCell != null)
			{
				autofitToFirstFixedWidthCell.Write(sw, "autofitToFirstFixedWidthCell");
			}
			if (underlineTabInNumList != null)
			{
				underlineTabInNumList.Write(sw, "underlineTabInNumList");
			}
			if (displayHangulFixedWidth != null)
			{
				displayHangulFixedWidth.Write(sw, "displayHangulFixedWidth");
			}
			if (splitPgBreakAndParaMark != null)
			{
				splitPgBreakAndParaMark.Write(sw, "splitPgBreakAndParaMark");
			}
			if (doNotVertAlignCellWithSp != null)
			{
				doNotVertAlignCellWithSp.Write(sw, "doNotVertAlignCellWithSp");
			}
			if (doNotBreakConstrainedForcedTable != null)
			{
				doNotBreakConstrainedForcedTable.Write(sw, "doNotBreakConstrainedForcedTable");
			}
			if (doNotVertAlignInTxbx != null)
			{
				doNotVertAlignInTxbx.Write(sw, "doNotVertAlignInTxbx");
			}
			if (useAnsiKerningPairs != null)
			{
				useAnsiKerningPairs.Write(sw, "useAnsiKerningPairs");
			}
			if (cachedColBalance != null)
			{
				cachedColBalance.Write(sw, "cachedColBalance");
			}
			sw.Write(string.Format("</w:{0}>", nodeName));
		}

		public CT_Compat()
		{
			useFELayoutField = new CT_OnOff();
			adjustLineHeightInTableField = new CT_OnOff();
			doNotExpandShiftReturnField = new CT_OnOff();
			ulTrailSpaceField = new CT_OnOff();
			doNotLeaveBackslashAloneField = new CT_OnOff();
			balanceSingleByteDoubleByteWidthField = new CT_OnOff();
			spaceForULField = new CT_OnOff();
		}
	}
}
