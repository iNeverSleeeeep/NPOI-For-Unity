using NPOI.OpenXmlFormats.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot("settings", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Settings
	{
		private CT_WriteProtection writeProtectionField;

		private CT_View viewField;

		private CT_Zoom zoomField;

		private CT_OnOff removePersonalInformationField;

		private CT_OnOff removeDateAndTimeField;

		private CT_OnOff doNotDisplayPageBoundariesField;

		private CT_OnOff displayBackgroundShapeField;

		private CT_OnOff printPostScriptOverTextField;

		private CT_OnOff printFractionalCharacterWidthField;

		private CT_OnOff printFormsDataField;

		private CT_OnOff embedTrueTypeFontsField;

		private CT_OnOff embedSystemFontsField;

		private CT_OnOff saveSubsetFontsField;

		private CT_OnOff saveFormsDataField;

		private CT_OnOff mirrorMarginsField;

		private CT_OnOff alignBordersAndEdgesField;

		private CT_OnOff bordersDoNotSurroundHeaderField;

		private CT_OnOff bordersDoNotSurroundFooterField;

		private CT_OnOff gutterAtTopField;

		private CT_OnOff hideSpellingErrorsField;

		private CT_OnOff hideGrammaticalErrorsField;

		private List<CT_WritingStyle> activeWritingStyleField;

		private CT_Proof proofStateField;

		private CT_OnOff formsDesignField;

		private CT_Rel attachedTemplateField;

		private CT_OnOff linkStylesField;

		private CT_ShortHexNumber stylePaneFormatFilterField;

		private CT_ShortHexNumber stylePaneSortMethodField;

		private CT_DocType documentTypeField;

		private CT_MailMerge mailMergeField;

		private CT_TrackChangesView revisionViewField;

		private CT_OnOff trackRevisionsField;

		private CT_OnOff doNotTrackMovesField;

		private CT_OnOff doNotTrackFormattingField;

		private CT_DocProtect documentProtectionField;

		private CT_OnOff autoFormatOverrideField;

		private CT_OnOff styleLockThemeField;

		private CT_OnOff styleLockQFSetField;

		private CT_TwipsMeasure defaultTabStopField;

		private CT_OnOff autoHyphenationField;

		private CT_DecimalNumber consecutiveHyphenLimitField;

		private CT_TwipsMeasure hyphenationZoneField;

		private CT_OnOff doNotHyphenateCapsField;

		private CT_OnOff showEnvelopeField;

		private CT_DecimalNumber summaryLengthField;

		private CT_String clickAndTypeStyleField;

		private CT_String defaultTableStyleField;

		private CT_OnOff evenAndOddHeadersField;

		private CT_OnOff bookFoldRevPrintingField;

		private CT_OnOff bookFoldPrintingField;

		private CT_DecimalNumber bookFoldPrintingSheetsField;

		private CT_TwipsMeasure drawingGridHorizontalSpacingField;

		private CT_TwipsMeasure drawingGridVerticalSpacingField;

		private CT_DecimalNumber displayHorizontalDrawingGridEveryField;

		private CT_DecimalNumber displayVerticalDrawingGridEveryField;

		private CT_OnOff doNotUseMarginsForDrawingGridOriginField;

		private CT_TwipsMeasure drawingGridHorizontalOriginField;

		private CT_TwipsMeasure drawingGridVerticalOriginField;

		private CT_OnOff doNotShadeFormDataField;

		private CT_OnOff noPunctuationKerningField;

		private CT_CharacterSpacing characterSpacingControlField;

		private CT_OnOff printTwoOnOneField;

		private CT_OnOff strictFirstAndLastCharsField;

		private CT_Kinsoku noLineBreaksAfterField;

		private CT_Kinsoku noLineBreaksBeforeField;

		private CT_OnOff savePreviewPictureField;

		private CT_OnOff doNotValidateAgainstSchemaField;

		private CT_OnOff saveInvalidXmlField;

		private CT_OnOff ignoreMixedContentField;

		private CT_OnOff alwaysShowPlaceholderTextField;

		private CT_OnOff doNotDemarcateInvalidXmlField;

		private CT_OnOff saveXmlDataOnlyField;

		private CT_OnOff useXSLTWhenSavingField;

		private CT_SaveThroughXslt saveThroughXsltField;

		private CT_OnOff showXMLTagsField;

		private CT_OnOff alwaysMergeEmptyNamespaceField;

		private CT_OnOff updateFieldsField;

		private XmlElement[] hdrShapeDefaultsField;

		private CT_FtnDocProps footnotePrField;

		private CT_EdnDocProps endnotePrField;

		private CT_Compat compatField;

		private List<CT_DocVar> docVarsField;

		private CT_DocRsids rsidsField;

		private CT_MathPr mathPrField;

		private CT_OnOff uiCompat97To2003Field;

		private List<CT_String> attachedSchemaField;

		private CT_Language themeFontLangField;

		private CT_ColorSchemeMapping clrSchemeMappingField;

		private CT_OnOff doNotIncludeSubdocsInStatsField;

		private CT_OnOff doNotAutoCompressPicturesField;

		private CT_Empty forceUpgradeField;

		private CT_Captions captionsField;

		private CT_ReadingModeInkLockDown readModeInkLockDownField;

		private List<CT_SmartTagType> smartTagTypeField;

		private List<CT_Schema> schemaLibraryField;

		private XmlElement[] shapeDefaultsField;

		private CT_OnOff doNotEmbedSmartTagsField;

		private CT_String decimalSymbolField;

		private CT_String listSeparatorField;

		[XmlElement(Order = 0)]
		public CT_WriteProtection writeProtection
		{
			get
			{
				return writeProtectionField;
			}
			set
			{
				writeProtectionField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_View view
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

		[XmlElement(Order = 2)]
		public CT_Zoom zoom
		{
			get
			{
				return zoomField;
			}
			set
			{
				zoomField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff removePersonalInformation
		{
			get
			{
				return removePersonalInformationField;
			}
			set
			{
				removePersonalInformationField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff removeDateAndTime
		{
			get
			{
				return removeDateAndTimeField;
			}
			set
			{
				removeDateAndTimeField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_OnOff doNotDisplayPageBoundaries
		{
			get
			{
				return doNotDisplayPageBoundariesField;
			}
			set
			{
				doNotDisplayPageBoundariesField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_OnOff displayBackgroundShape
		{
			get
			{
				return displayBackgroundShapeField;
			}
			set
			{
				displayBackgroundShapeField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OnOff printPostScriptOverText
		{
			get
			{
				return printPostScriptOverTextField;
			}
			set
			{
				printPostScriptOverTextField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_OnOff printFractionalCharacterWidth
		{
			get
			{
				return printFractionalCharacterWidthField;
			}
			set
			{
				printFractionalCharacterWidthField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_OnOff printFormsData
		{
			get
			{
				return printFormsDataField;
			}
			set
			{
				printFormsDataField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_OnOff embedTrueTypeFonts
		{
			get
			{
				return embedTrueTypeFontsField;
			}
			set
			{
				embedTrueTypeFontsField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_OnOff embedSystemFonts
		{
			get
			{
				return embedSystemFontsField;
			}
			set
			{
				embedSystemFontsField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_OnOff saveSubsetFonts
		{
			get
			{
				return saveSubsetFontsField;
			}
			set
			{
				saveSubsetFontsField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_OnOff saveFormsData
		{
			get
			{
				return saveFormsDataField;
			}
			set
			{
				saveFormsDataField = value;
			}
		}

		[XmlElement(Order = 14)]
		public CT_OnOff mirrorMargins
		{
			get
			{
				return mirrorMarginsField;
			}
			set
			{
				mirrorMarginsField = value;
			}
		}

		[XmlElement(Order = 15)]
		public CT_OnOff alignBordersAndEdges
		{
			get
			{
				return alignBordersAndEdgesField;
			}
			set
			{
				alignBordersAndEdgesField = value;
			}
		}

		[XmlElement(Order = 16)]
		public CT_OnOff bordersDoNotSurroundHeader
		{
			get
			{
				return bordersDoNotSurroundHeaderField;
			}
			set
			{
				bordersDoNotSurroundHeaderField = value;
			}
		}

		[XmlElement(Order = 17)]
		public CT_OnOff bordersDoNotSurroundFooter
		{
			get
			{
				return bordersDoNotSurroundFooterField;
			}
			set
			{
				bordersDoNotSurroundFooterField = value;
			}
		}

		[XmlElement(Order = 18)]
		public CT_OnOff gutterAtTop
		{
			get
			{
				return gutterAtTopField;
			}
			set
			{
				gutterAtTopField = value;
			}
		}

		[XmlElement(Order = 19)]
		public CT_OnOff hideSpellingErrors
		{
			get
			{
				return hideSpellingErrorsField;
			}
			set
			{
				hideSpellingErrorsField = value;
			}
		}

		[XmlElement(Order = 20)]
		public CT_OnOff hideGrammaticalErrors
		{
			get
			{
				return hideGrammaticalErrorsField;
			}
			set
			{
				hideGrammaticalErrorsField = value;
			}
		}

		[XmlElement("activeWritingStyle", Order = 21)]
		public List<CT_WritingStyle> activeWritingStyle
		{
			get
			{
				return activeWritingStyleField;
			}
			set
			{
				activeWritingStyleField = value;
			}
		}

		[XmlElement(Order = 22)]
		public CT_Proof proofState
		{
			get
			{
				return proofStateField;
			}
			set
			{
				proofStateField = value;
			}
		}

		[XmlElement(Order = 23)]
		public CT_OnOff formsDesign
		{
			get
			{
				return formsDesignField;
			}
			set
			{
				formsDesignField = value;
			}
		}

		[XmlElement(Order = 24)]
		public CT_Rel attachedTemplate
		{
			get
			{
				return attachedTemplateField;
			}
			set
			{
				attachedTemplateField = value;
			}
		}

		[XmlElement(Order = 25)]
		public CT_OnOff linkStyles
		{
			get
			{
				return linkStylesField;
			}
			set
			{
				linkStylesField = value;
			}
		}

		[XmlElement(Order = 26)]
		public CT_ShortHexNumber stylePaneFormatFilter
		{
			get
			{
				return stylePaneFormatFilterField;
			}
			set
			{
				stylePaneFormatFilterField = value;
			}
		}

		[XmlElement(Order = 27)]
		public CT_ShortHexNumber stylePaneSortMethod
		{
			get
			{
				return stylePaneSortMethodField;
			}
			set
			{
				stylePaneSortMethodField = value;
			}
		}

		[XmlElement(Order = 28)]
		public CT_DocType documentType
		{
			get
			{
				return documentTypeField;
			}
			set
			{
				documentTypeField = value;
			}
		}

		[XmlElement(Order = 29)]
		public CT_MailMerge mailMerge
		{
			get
			{
				return mailMergeField;
			}
			set
			{
				mailMergeField = value;
			}
		}

		[XmlElement(Order = 30)]
		public CT_TrackChangesView revisionView
		{
			get
			{
				return revisionViewField;
			}
			set
			{
				revisionViewField = value;
			}
		}

		[XmlElement(Order = 31)]
		public CT_OnOff trackRevisions
		{
			get
			{
				return trackRevisionsField;
			}
			set
			{
				trackRevisionsField = value;
			}
		}

		[XmlElement(Order = 32)]
		public CT_OnOff doNotTrackMoves
		{
			get
			{
				return doNotTrackMovesField;
			}
			set
			{
				doNotTrackMovesField = value;
			}
		}

		[XmlElement(Order = 33)]
		public CT_OnOff doNotTrackFormatting
		{
			get
			{
				return doNotTrackFormattingField;
			}
			set
			{
				doNotTrackFormattingField = value;
			}
		}

		[XmlElement(Order = 34)]
		public CT_DocProtect documentProtection
		{
			get
			{
				return documentProtectionField;
			}
			set
			{
				documentProtectionField = value;
			}
		}

		[XmlElement(Order = 35)]
		public CT_OnOff autoFormatOverride
		{
			get
			{
				return autoFormatOverrideField;
			}
			set
			{
				autoFormatOverrideField = value;
			}
		}

		[XmlElement(Order = 36)]
		public CT_OnOff styleLockTheme
		{
			get
			{
				return styleLockThemeField;
			}
			set
			{
				styleLockThemeField = value;
			}
		}

		[XmlElement(Order = 37)]
		public CT_OnOff styleLockQFSet
		{
			get
			{
				return styleLockQFSetField;
			}
			set
			{
				styleLockQFSetField = value;
			}
		}

		[XmlElement(Order = 38)]
		public CT_TwipsMeasure defaultTabStop
		{
			get
			{
				return defaultTabStopField;
			}
			set
			{
				defaultTabStopField = value;
			}
		}

		[XmlElement(Order = 39)]
		public CT_OnOff autoHyphenation
		{
			get
			{
				return autoHyphenationField;
			}
			set
			{
				autoHyphenationField = value;
			}
		}

		[XmlElement(Order = 40)]
		public CT_DecimalNumber consecutiveHyphenLimit
		{
			get
			{
				return consecutiveHyphenLimitField;
			}
			set
			{
				consecutiveHyphenLimitField = value;
			}
		}

		[XmlElement(Order = 41)]
		public CT_TwipsMeasure hyphenationZone
		{
			get
			{
				return hyphenationZoneField;
			}
			set
			{
				hyphenationZoneField = value;
			}
		}

		[XmlElement(Order = 42)]
		public CT_OnOff doNotHyphenateCaps
		{
			get
			{
				return doNotHyphenateCapsField;
			}
			set
			{
				doNotHyphenateCapsField = value;
			}
		}

		[XmlElement(Order = 43)]
		public CT_OnOff showEnvelope
		{
			get
			{
				return showEnvelopeField;
			}
			set
			{
				showEnvelopeField = value;
			}
		}

		[XmlElement(Order = 44)]
		public CT_DecimalNumber summaryLength
		{
			get
			{
				return summaryLengthField;
			}
			set
			{
				summaryLengthField = value;
			}
		}

		[XmlElement(Order = 45)]
		public CT_String clickAndTypeStyle
		{
			get
			{
				return clickAndTypeStyleField;
			}
			set
			{
				clickAndTypeStyleField = value;
			}
		}

		[XmlElement(Order = 46)]
		public CT_String defaultTableStyle
		{
			get
			{
				return defaultTableStyleField;
			}
			set
			{
				defaultTableStyleField = value;
			}
		}

		[XmlElement(Order = 47)]
		public CT_OnOff evenAndOddHeaders
		{
			get
			{
				return evenAndOddHeadersField;
			}
			set
			{
				evenAndOddHeadersField = value;
			}
		}

		[XmlElement(Order = 48)]
		public CT_OnOff bookFoldRevPrinting
		{
			get
			{
				return bookFoldRevPrintingField;
			}
			set
			{
				bookFoldRevPrintingField = value;
			}
		}

		[XmlElement(Order = 49)]
		public CT_OnOff bookFoldPrinting
		{
			get
			{
				return bookFoldPrintingField;
			}
			set
			{
				bookFoldPrintingField = value;
			}
		}

		[XmlElement(Order = 50)]
		public CT_DecimalNumber bookFoldPrintingSheets
		{
			get
			{
				return bookFoldPrintingSheetsField;
			}
			set
			{
				bookFoldPrintingSheetsField = value;
			}
		}

		[XmlElement(Order = 51)]
		public CT_TwipsMeasure drawingGridHorizontalSpacing
		{
			get
			{
				return drawingGridHorizontalSpacingField;
			}
			set
			{
				drawingGridHorizontalSpacingField = value;
			}
		}

		[XmlElement(Order = 52)]
		public CT_TwipsMeasure drawingGridVerticalSpacing
		{
			get
			{
				return drawingGridVerticalSpacingField;
			}
			set
			{
				drawingGridVerticalSpacingField = value;
			}
		}

		[XmlElement(Order = 53)]
		public CT_DecimalNumber displayHorizontalDrawingGridEvery
		{
			get
			{
				return displayHorizontalDrawingGridEveryField;
			}
			set
			{
				displayHorizontalDrawingGridEveryField = value;
			}
		}

		[XmlElement(Order = 54)]
		public CT_DecimalNumber displayVerticalDrawingGridEvery
		{
			get
			{
				return displayVerticalDrawingGridEveryField;
			}
			set
			{
				displayVerticalDrawingGridEveryField = value;
			}
		}

		[XmlElement(Order = 55)]
		public CT_OnOff doNotUseMarginsForDrawingGridOrigin
		{
			get
			{
				return doNotUseMarginsForDrawingGridOriginField;
			}
			set
			{
				doNotUseMarginsForDrawingGridOriginField = value;
			}
		}

		[XmlElement(Order = 56)]
		public CT_TwipsMeasure drawingGridHorizontalOrigin
		{
			get
			{
				return drawingGridHorizontalOriginField;
			}
			set
			{
				drawingGridHorizontalOriginField = value;
			}
		}

		[XmlElement(Order = 57)]
		public CT_TwipsMeasure drawingGridVerticalOrigin
		{
			get
			{
				return drawingGridVerticalOriginField;
			}
			set
			{
				drawingGridVerticalOriginField = value;
			}
		}

		[XmlElement(Order = 58)]
		public CT_OnOff doNotShadeFormData
		{
			get
			{
				return doNotShadeFormDataField;
			}
			set
			{
				doNotShadeFormDataField = value;
			}
		}

		[XmlElement(Order = 59)]
		public CT_OnOff noPunctuationKerning
		{
			get
			{
				return noPunctuationKerningField;
			}
			set
			{
				noPunctuationKerningField = value;
			}
		}

		[XmlElement(Order = 60)]
		public CT_CharacterSpacing characterSpacingControl
		{
			get
			{
				return characterSpacingControlField;
			}
			set
			{
				characterSpacingControlField = value;
			}
		}

		[XmlElement(Order = 61)]
		public CT_OnOff printTwoOnOne
		{
			get
			{
				return printTwoOnOneField;
			}
			set
			{
				printTwoOnOneField = value;
			}
		}

		[XmlElement(Order = 62)]
		public CT_OnOff strictFirstAndLastChars
		{
			get
			{
				return strictFirstAndLastCharsField;
			}
			set
			{
				strictFirstAndLastCharsField = value;
			}
		}

		[XmlElement(Order = 63)]
		public CT_Kinsoku noLineBreaksAfter
		{
			get
			{
				return noLineBreaksAfterField;
			}
			set
			{
				noLineBreaksAfterField = value;
			}
		}

		[XmlElement(Order = 64)]
		public CT_Kinsoku noLineBreaksBefore
		{
			get
			{
				return noLineBreaksBeforeField;
			}
			set
			{
				noLineBreaksBeforeField = value;
			}
		}

		[XmlElement(Order = 65)]
		public CT_OnOff savePreviewPicture
		{
			get
			{
				return savePreviewPictureField;
			}
			set
			{
				savePreviewPictureField = value;
			}
		}

		[XmlElement(Order = 66)]
		public CT_OnOff doNotValidateAgainstSchema
		{
			get
			{
				return doNotValidateAgainstSchemaField;
			}
			set
			{
				doNotValidateAgainstSchemaField = value;
			}
		}

		[XmlElement(Order = 67)]
		public CT_OnOff saveInvalidXml
		{
			get
			{
				return saveInvalidXmlField;
			}
			set
			{
				saveInvalidXmlField = value;
			}
		}

		[XmlElement(Order = 68)]
		public CT_OnOff ignoreMixedContent
		{
			get
			{
				return ignoreMixedContentField;
			}
			set
			{
				ignoreMixedContentField = value;
			}
		}

		[XmlElement(Order = 69)]
		public CT_OnOff alwaysShowPlaceholderText
		{
			get
			{
				return alwaysShowPlaceholderTextField;
			}
			set
			{
				alwaysShowPlaceholderTextField = value;
			}
		}

		[XmlElement(Order = 70)]
		public CT_OnOff doNotDemarcateInvalidXml
		{
			get
			{
				return doNotDemarcateInvalidXmlField;
			}
			set
			{
				doNotDemarcateInvalidXmlField = value;
			}
		}

		[XmlElement(Order = 71)]
		public CT_OnOff saveXmlDataOnly
		{
			get
			{
				return saveXmlDataOnlyField;
			}
			set
			{
				saveXmlDataOnlyField = value;
			}
		}

		[XmlElement(Order = 72)]
		public CT_OnOff useXSLTWhenSaving
		{
			get
			{
				return useXSLTWhenSavingField;
			}
			set
			{
				useXSLTWhenSavingField = value;
			}
		}

		[XmlElement(Order = 73)]
		public CT_SaveThroughXslt saveThroughXslt
		{
			get
			{
				return saveThroughXsltField;
			}
			set
			{
				saveThroughXsltField = value;
			}
		}

		[XmlElement(Order = 74)]
		public CT_OnOff showXMLTags
		{
			get
			{
				return showXMLTagsField;
			}
			set
			{
				showXMLTagsField = value;
			}
		}

		[XmlElement(Order = 75)]
		public CT_OnOff alwaysMergeEmptyNamespace
		{
			get
			{
				return alwaysMergeEmptyNamespaceField;
			}
			set
			{
				alwaysMergeEmptyNamespaceField = value;
			}
		}

		[XmlElement(Order = 76)]
		public CT_OnOff updateFields
		{
			get
			{
				return updateFieldsField;
			}
			set
			{
				updateFieldsField = value;
			}
		}

		[XmlArray(Order = 77)]
		[XmlArrayItem("", Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
		public XmlElement[] hdrShapeDefaults
		{
			get
			{
				return hdrShapeDefaultsField;
			}
			set
			{
				hdrShapeDefaultsField = value;
			}
		}

		[XmlElement(Order = 78)]
		public CT_FtnDocProps footnotePr
		{
			get
			{
				return footnotePrField;
			}
			set
			{
				footnotePrField = value;
			}
		}

		[XmlElement(Order = 79)]
		public CT_EdnDocProps endnotePr
		{
			get
			{
				return endnotePrField;
			}
			set
			{
				endnotePrField = value;
			}
		}

		[XmlElement(Order = 80)]
		public CT_Compat compat
		{
			get
			{
				return compatField;
			}
			set
			{
				compatField = value;
			}
		}

		[XmlArray(Order = 81)]
		[XmlArrayItem("docVar", IsNullable = false)]
		public List<CT_DocVar> docVars
		{
			get
			{
				return docVarsField;
			}
			set
			{
				docVarsField = value;
			}
		}

		[XmlElement(Order = 82)]
		public CT_DocRsids rsids
		{
			get
			{
				return rsidsField;
			}
			set
			{
				rsidsField = value;
			}
		}

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", Order = 83)]
		public CT_MathPr mathPr
		{
			get
			{
				return mathPrField;
			}
			set
			{
				mathPrField = value;
			}
		}

		[XmlElement(Order = 84)]
		public CT_OnOff uiCompat97To2003
		{
			get
			{
				return uiCompat97To2003Field;
			}
			set
			{
				uiCompat97To2003Field = value;
			}
		}

		[XmlElement("attachedSchema", Order = 85)]
		public List<CT_String> attachedSchema
		{
			get
			{
				return attachedSchemaField;
			}
			set
			{
				attachedSchemaField = value;
			}
		}

		[XmlElement(Order = 86)]
		public CT_Language themeFontLang
		{
			get
			{
				return themeFontLangField;
			}
			set
			{
				themeFontLangField = value;
			}
		}

		[XmlElement(Order = 87)]
		public CT_ColorSchemeMapping clrSchemeMapping
		{
			get
			{
				return clrSchemeMappingField;
			}
			set
			{
				clrSchemeMappingField = value;
			}
		}

		[XmlElement(Order = 88)]
		public CT_OnOff doNotIncludeSubdocsInStats
		{
			get
			{
				return doNotIncludeSubdocsInStatsField;
			}
			set
			{
				doNotIncludeSubdocsInStatsField = value;
			}
		}

		[XmlElement(Order = 89)]
		public CT_OnOff doNotAutoCompressPictures
		{
			get
			{
				return doNotAutoCompressPicturesField;
			}
			set
			{
				doNotAutoCompressPicturesField = value;
			}
		}

		[XmlElement(Order = 90)]
		public CT_Empty forceUpgrade
		{
			get
			{
				return forceUpgradeField;
			}
			set
			{
				forceUpgradeField = value;
			}
		}

		[XmlElement(Order = 91)]
		public CT_Captions captions
		{
			get
			{
				return captionsField;
			}
			set
			{
				captionsField = value;
			}
		}

		[XmlElement(Order = 92)]
		public CT_ReadingModeInkLockDown readModeInkLockDown
		{
			get
			{
				return readModeInkLockDownField;
			}
			set
			{
				readModeInkLockDownField = value;
			}
		}

		[XmlElement("smartTagType", Order = 93)]
		public List<CT_SmartTagType> smartTagType
		{
			get
			{
				return smartTagTypeField;
			}
			set
			{
				smartTagTypeField = value;
			}
		}

		[XmlArray(Namespace = "http://schemas.openxmlformats.org/schemaLibrary/2006/main", Order = 94)]
		[XmlArrayItem("schema", IsNullable = false)]
		public List<CT_Schema> schemaLibrary
		{
			get
			{
				return schemaLibraryField;
			}
			set
			{
				schemaLibraryField = value;
			}
		}

		[XmlArray(Order = 95)]
		[XmlArrayItem("", Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
		public XmlElement[] shapeDefaults
		{
			get
			{
				return shapeDefaultsField;
			}
			set
			{
				shapeDefaultsField = value;
			}
		}

		[XmlElement(Order = 96)]
		public CT_OnOff doNotEmbedSmartTags
		{
			get
			{
				return doNotEmbedSmartTagsField;
			}
			set
			{
				doNotEmbedSmartTagsField = value;
			}
		}

		[XmlElement(Order = 97)]
		public CT_String decimalSymbol
		{
			get
			{
				return decimalSymbolField;
			}
			set
			{
				decimalSymbolField = value;
			}
		}

		[XmlElement(Order = 98)]
		public CT_String listSeparator
		{
			get
			{
				return listSeparatorField;
			}
			set
			{
				listSeparatorField = value;
			}
		}

		public static CT_Settings Parse(XmlNode node, XmlNamespaceManager namespaceManager)
		{
			if (node == null)
			{
				return null;
			}
			CT_Settings cT_Settings = new CT_Settings();
			cT_Settings.activeWritingStyle = new List<CT_WritingStyle>();
			cT_Settings.docVars = new List<CT_DocVar>();
			cT_Settings.attachedSchema = new List<CT_String>();
			cT_Settings.smartTagType = new List<CT_SmartTagType>();
			cT_Settings.schemaLibrary = new List<CT_Schema>();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.LocalName == "writeProtection")
				{
					cT_Settings.writeProtection = CT_WriteProtection.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "view")
				{
					cT_Settings.view = CT_View.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "zoom")
				{
					cT_Settings.zoom = CT_Zoom.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "removePersonalInformation")
				{
					cT_Settings.removePersonalInformation = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "removeDateAndTime")
				{
					cT_Settings.removeDateAndTime = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotDisplayPageBoundaries")
				{
					cT_Settings.doNotDisplayPageBoundaries = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "displayBackgroundShape")
				{
					cT_Settings.displayBackgroundShape = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printPostScriptOverText")
				{
					cT_Settings.printPostScriptOverText = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printFractionalCharacterWidth")
				{
					cT_Settings.printFractionalCharacterWidth = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printFormsData")
				{
					cT_Settings.printFormsData = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "embedTrueTypeFonts")
				{
					cT_Settings.embedTrueTypeFonts = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "embedSystemFonts")
				{
					cT_Settings.embedSystemFonts = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "saveSubsetFonts")
				{
					cT_Settings.saveSubsetFonts = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "saveFormsData")
				{
					cT_Settings.saveFormsData = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mirrorMargins")
				{
					cT_Settings.mirrorMargins = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "alignBordersAndEdges")
				{
					cT_Settings.alignBordersAndEdges = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bordersDoNotSurroundHeader")
				{
					cT_Settings.bordersDoNotSurroundHeader = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bordersDoNotSurroundFooter")
				{
					cT_Settings.bordersDoNotSurroundFooter = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "gutterAtTop")
				{
					cT_Settings.gutterAtTop = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hideSpellingErrors")
				{
					cT_Settings.hideSpellingErrors = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hideGrammaticalErrors")
				{
					cT_Settings.hideGrammaticalErrors = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "proofState")
				{
					cT_Settings.proofState = CT_Proof.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "formsDesign")
				{
					cT_Settings.formsDesign = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "attachedTemplate")
				{
					cT_Settings.attachedTemplate = CT_Rel.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "linkStyles")
				{
					cT_Settings.linkStyles = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "stylePaneFormatFilter")
				{
					cT_Settings.stylePaneFormatFilter = CT_ShortHexNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "stylePaneSortMethod")
				{
					cT_Settings.stylePaneSortMethod = CT_ShortHexNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "documentType")
				{
					cT_Settings.documentType = CT_DocType.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mailMerge")
				{
					cT_Settings.mailMerge = CT_MailMerge.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "revisionView")
				{
					cT_Settings.revisionView = CT_TrackChangesView.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "trackRevisions")
				{
					cT_Settings.trackRevisions = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotTrackMoves")
				{
					cT_Settings.doNotTrackMoves = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotTrackFormatting")
				{
					cT_Settings.doNotTrackFormatting = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "documentProtection")
				{
					cT_Settings.documentProtection = CT_DocProtect.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoFormatOverride")
				{
					cT_Settings.autoFormatOverride = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "styleLockTheme")
				{
					cT_Settings.styleLockTheme = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "styleLockQFSet")
				{
					cT_Settings.styleLockQFSet = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "defaultTabStop")
				{
					cT_Settings.defaultTabStop = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "autoHyphenation")
				{
					cT_Settings.autoHyphenation = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "consecutiveHyphenLimit")
				{
					cT_Settings.consecutiveHyphenLimit = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "hyphenationZone")
				{
					cT_Settings.hyphenationZone = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotHyphenateCaps")
				{
					cT_Settings.doNotHyphenateCaps = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showEnvelope")
				{
					cT_Settings.showEnvelope = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "summaryLength")
				{
					cT_Settings.summaryLength = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "clickAndTypeStyle")
				{
					cT_Settings.clickAndTypeStyle = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "defaultTableStyle")
				{
					cT_Settings.defaultTableStyle = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "evenAndOddHeaders")
				{
					cT_Settings.evenAndOddHeaders = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bookFoldRevPrinting")
				{
					cT_Settings.bookFoldRevPrinting = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bookFoldPrinting")
				{
					cT_Settings.bookFoldPrinting = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "bookFoldPrintingSheets")
				{
					cT_Settings.bookFoldPrintingSheets = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "drawingGridHorizontalSpacing")
				{
					cT_Settings.drawingGridHorizontalSpacing = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "drawingGridVerticalSpacing")
				{
					cT_Settings.drawingGridVerticalSpacing = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "displayHorizontalDrawingGridEvery")
				{
					cT_Settings.displayHorizontalDrawingGridEvery = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "displayVerticalDrawingGridEvery")
				{
					cT_Settings.displayVerticalDrawingGridEvery = CT_DecimalNumber.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotUseMarginsForDrawingGridOrigin")
				{
					cT_Settings.doNotUseMarginsForDrawingGridOrigin = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "drawingGridHorizontalOrigin")
				{
					cT_Settings.drawingGridHorizontalOrigin = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "drawingGridVerticalOrigin")
				{
					cT_Settings.drawingGridVerticalOrigin = CT_TwipsMeasure.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotShadeFormData")
				{
					cT_Settings.doNotShadeFormData = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noPunctuationKerning")
				{
					cT_Settings.noPunctuationKerning = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "characterSpacingControl")
				{
					cT_Settings.characterSpacingControl = CT_CharacterSpacing.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "printTwoOnOne")
				{
					cT_Settings.printTwoOnOne = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "strictFirstAndLastChars")
				{
					cT_Settings.strictFirstAndLastChars = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noLineBreaksAfter")
				{
					cT_Settings.noLineBreaksAfter = CT_Kinsoku.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "noLineBreaksBefore")
				{
					cT_Settings.noLineBreaksBefore = CT_Kinsoku.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "savePreviewPicture")
				{
					cT_Settings.savePreviewPicture = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotValidateAgainstSchema")
				{
					cT_Settings.doNotValidateAgainstSchema = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "saveInvalidXml")
				{
					cT_Settings.saveInvalidXml = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "ignoreMixedContent")
				{
					cT_Settings.ignoreMixedContent = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "alwaysShowPlaceholderText")
				{
					cT_Settings.alwaysShowPlaceholderText = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotDemarcateInvalidXml")
				{
					cT_Settings.doNotDemarcateInvalidXml = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "saveXmlDataOnly")
				{
					cT_Settings.saveXmlDataOnly = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "useXSLTWhenSaving")
				{
					cT_Settings.useXSLTWhenSaving = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "saveThroughXslt")
				{
					cT_Settings.saveThroughXslt = CT_SaveThroughXslt.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "showXMLTags")
				{
					cT_Settings.showXMLTags = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "alwaysMergeEmptyNamespace")
				{
					cT_Settings.alwaysMergeEmptyNamespace = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "updateFields")
				{
					cT_Settings.updateFields = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "footnotePr")
				{
					cT_Settings.footnotePr = CT_FtnDocProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "endnotePr")
				{
					cT_Settings.endnotePr = CT_EdnDocProps.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "compat")
				{
					cT_Settings.compat = CT_Compat.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "rsids")
				{
					cT_Settings.rsids = CT_DocRsids.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "mathPr")
				{
					cT_Settings.mathPr = CT_MathPr.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "uiCompat97To2003")
				{
					cT_Settings.uiCompat97To2003 = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "themeFontLang")
				{
					cT_Settings.themeFontLang = CT_Language.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "clrSchemeMapping")
				{
					cT_Settings.clrSchemeMapping = CT_ColorSchemeMapping.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotIncludeSubdocsInStats")
				{
					cT_Settings.doNotIncludeSubdocsInStats = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotAutoCompressPictures")
				{
					cT_Settings.doNotAutoCompressPictures = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "forceUpgrade")
				{
					cT_Settings.forceUpgrade = new CT_Empty();
				}
				else if (childNode.LocalName == "captions")
				{
					cT_Settings.captions = CT_Captions.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "readModeInkLockDown")
				{
					cT_Settings.readModeInkLockDown = CT_ReadingModeInkLockDown.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "doNotEmbedSmartTags")
				{
					cT_Settings.doNotEmbedSmartTags = CT_OnOff.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "decimalSymbol")
				{
					cT_Settings.decimalSymbol = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "listSeparator")
				{
					cT_Settings.listSeparator = CT_String.Parse(childNode, namespaceManager);
				}
				else if (childNode.LocalName == "activeWritingStyle")
				{
					cT_Settings.activeWritingStyle.Add(CT_WritingStyle.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "docVars")
				{
					cT_Settings.docVars.Add(CT_DocVar.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "attachedSchema")
				{
					cT_Settings.attachedSchema.Add(CT_String.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "smartTagType")
				{
					cT_Settings.smartTagType.Add(CT_SmartTagType.Parse(childNode, namespaceManager));
				}
				else if (childNode.LocalName == "schemaLibrary")
				{
					cT_Settings.schemaLibrary.Add(CT_Schema.Parse(childNode, namespaceManager));
				}
			}
			return cT_Settings;
		}

		internal void Write(StreamWriter sw)
		{
			sw.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
			sw.Write("<w:settings xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:r=\"http://schemas.openxmlformats.org/officeDocument/2006/relationships\" xmlns:m=\"http://schemas.openxmlformats.org/officeDocument/2006/math\" ");
			sw.Write("xmlns:v=\"urn:schemas-microsoft-com:vml\" xmlns:w10=\"urn:schemas-microsoft-com:office:word\" xmlns:w=\"http://schemas.openxmlformats.org/wordprocessingml/2006/main\" xmlns:sl=\"http://schemas.openxmlformats.org/schemaLibrary/2006/main\">");
			if (writeProtection != null)
			{
				writeProtection.Write(sw, "writeProtection");
			}
			if (view != null)
			{
				view.Write(sw, "view");
			}
			if (zoom != null)
			{
				zoom.Write(sw, "zoom");
			}
			if (removePersonalInformation != null)
			{
				removePersonalInformation.Write(sw, "removePersonalInformation");
			}
			if (removeDateAndTime != null)
			{
				removeDateAndTime.Write(sw, "removeDateAndTime");
			}
			if (doNotDisplayPageBoundaries != null)
			{
				doNotDisplayPageBoundaries.Write(sw, "doNotDisplayPageBoundaries");
			}
			if (displayBackgroundShape != null)
			{
				displayBackgroundShape.Write(sw, "displayBackgroundShape");
			}
			if (printPostScriptOverText != null)
			{
				printPostScriptOverText.Write(sw, "printPostScriptOverText");
			}
			if (printFractionalCharacterWidth != null)
			{
				printFractionalCharacterWidth.Write(sw, "printFractionalCharacterWidth");
			}
			if (printFormsData != null)
			{
				printFormsData.Write(sw, "printFormsData");
			}
			if (embedTrueTypeFonts != null)
			{
				embedTrueTypeFonts.Write(sw, "embedTrueTypeFonts");
			}
			if (embedSystemFonts != null)
			{
				embedSystemFonts.Write(sw, "embedSystemFonts");
			}
			if (saveSubsetFonts != null)
			{
				saveSubsetFonts.Write(sw, "saveSubsetFonts");
			}
			if (saveFormsData != null)
			{
				saveFormsData.Write(sw, "saveFormsData");
			}
			if (mirrorMargins != null)
			{
				mirrorMargins.Write(sw, "mirrorMargins");
			}
			if (alignBordersAndEdges != null)
			{
				alignBordersAndEdges.Write(sw, "alignBordersAndEdges");
			}
			if (bordersDoNotSurroundHeader != null)
			{
				bordersDoNotSurroundHeader.Write(sw, "bordersDoNotSurroundHeader");
			}
			if (bordersDoNotSurroundFooter != null)
			{
				bordersDoNotSurroundFooter.Write(sw, "bordersDoNotSurroundFooter");
			}
			if (gutterAtTop != null)
			{
				gutterAtTop.Write(sw, "gutterAtTop");
			}
			if (hideSpellingErrors != null)
			{
				hideSpellingErrors.Write(sw, "hideSpellingErrors");
			}
			if (hideGrammaticalErrors != null)
			{
				hideGrammaticalErrors.Write(sw, "hideGrammaticalErrors");
			}
			if (proofState != null)
			{
				proofState.Write(sw, "proofState");
			}
			if (formsDesign != null)
			{
				formsDesign.Write(sw, "formsDesign");
			}
			if (attachedTemplate != null)
			{
				attachedTemplate.Write(sw, "attachedTemplate");
			}
			if (linkStyles != null)
			{
				linkStyles.Write(sw, "linkStyles");
			}
			if (stylePaneFormatFilter != null)
			{
				stylePaneFormatFilter.Write(sw, "stylePaneFormatFilter");
			}
			if (stylePaneSortMethod != null)
			{
				stylePaneSortMethod.Write(sw, "stylePaneSortMethod");
			}
			if (documentType != null)
			{
				documentType.Write(sw, "documentType");
			}
			if (mailMerge != null)
			{
				mailMerge.Write(sw, "mailMerge");
			}
			if (revisionView != null)
			{
				revisionView.Write(sw, "revisionView");
			}
			if (trackRevisions != null)
			{
				trackRevisions.Write(sw, "trackRevisions");
			}
			if (doNotTrackMoves != null)
			{
				doNotTrackMoves.Write(sw, "doNotTrackMoves");
			}
			if (doNotTrackFormatting != null)
			{
				doNotTrackFormatting.Write(sw, "doNotTrackFormatting");
			}
			if (documentProtection != null)
			{
				documentProtection.Write(sw, "documentProtection");
			}
			if (autoFormatOverride != null)
			{
				autoFormatOverride.Write(sw, "autoFormatOverride");
			}
			if (styleLockTheme != null)
			{
				styleLockTheme.Write(sw, "styleLockTheme");
			}
			if (styleLockQFSet != null)
			{
				styleLockQFSet.Write(sw, "styleLockQFSet");
			}
			if (defaultTabStop != null)
			{
				defaultTabStop.Write(sw, "defaultTabStop");
			}
			if (autoHyphenation != null)
			{
				autoHyphenation.Write(sw, "autoHyphenation");
			}
			if (consecutiveHyphenLimit != null)
			{
				consecutiveHyphenLimit.Write(sw, "consecutiveHyphenLimit");
			}
			if (hyphenationZone != null)
			{
				hyphenationZone.Write(sw, "hyphenationZone");
			}
			if (doNotHyphenateCaps != null)
			{
				doNotHyphenateCaps.Write(sw, "doNotHyphenateCaps");
			}
			if (showEnvelope != null)
			{
				showEnvelope.Write(sw, "showEnvelope");
			}
			if (summaryLength != null)
			{
				summaryLength.Write(sw, "summaryLength");
			}
			if (clickAndTypeStyle != null)
			{
				clickAndTypeStyle.Write(sw, "clickAndTypeStyle");
			}
			if (defaultTableStyle != null)
			{
				defaultTableStyle.Write(sw, "defaultTableStyle");
			}
			if (evenAndOddHeaders != null)
			{
				evenAndOddHeaders.Write(sw, "evenAndOddHeaders");
			}
			if (bookFoldRevPrinting != null)
			{
				bookFoldRevPrinting.Write(sw, "bookFoldRevPrinting");
			}
			if (bookFoldPrinting != null)
			{
				bookFoldPrinting.Write(sw, "bookFoldPrinting");
			}
			if (bookFoldPrintingSheets != null)
			{
				bookFoldPrintingSheets.Write(sw, "bookFoldPrintingSheets");
			}
			if (drawingGridHorizontalSpacing != null)
			{
				drawingGridHorizontalSpacing.Write(sw, "drawingGridHorizontalSpacing");
			}
			if (drawingGridVerticalSpacing != null)
			{
				drawingGridVerticalSpacing.Write(sw, "drawingGridVerticalSpacing");
			}
			if (displayHorizontalDrawingGridEvery != null)
			{
				displayHorizontalDrawingGridEvery.Write(sw, "displayHorizontalDrawingGridEvery");
			}
			if (displayVerticalDrawingGridEvery != null)
			{
				displayVerticalDrawingGridEvery.Write(sw, "displayVerticalDrawingGridEvery");
			}
			if (doNotUseMarginsForDrawingGridOrigin != null)
			{
				doNotUseMarginsForDrawingGridOrigin.Write(sw, "doNotUseMarginsForDrawingGridOrigin");
			}
			if (drawingGridHorizontalOrigin != null)
			{
				drawingGridHorizontalOrigin.Write(sw, "drawingGridHorizontalOrigin");
			}
			if (drawingGridVerticalOrigin != null)
			{
				drawingGridVerticalOrigin.Write(sw, "drawingGridVerticalOrigin");
			}
			if (doNotShadeFormData != null)
			{
				doNotShadeFormData.Write(sw, "doNotShadeFormData");
			}
			if (noPunctuationKerning != null)
			{
				noPunctuationKerning.Write(sw, "noPunctuationKerning");
			}
			if (characterSpacingControl != null)
			{
				characterSpacingControl.Write(sw, "characterSpacingControl");
			}
			if (printTwoOnOne != null)
			{
				printTwoOnOne.Write(sw, "printTwoOnOne");
			}
			if (strictFirstAndLastChars != null)
			{
				strictFirstAndLastChars.Write(sw, "strictFirstAndLastChars");
			}
			if (noLineBreaksAfter != null)
			{
				noLineBreaksAfter.Write(sw, "noLineBreaksAfter");
			}
			if (noLineBreaksBefore != null)
			{
				noLineBreaksBefore.Write(sw, "noLineBreaksBefore");
			}
			if (savePreviewPicture != null)
			{
				savePreviewPicture.Write(sw, "savePreviewPicture");
			}
			if (doNotValidateAgainstSchema != null)
			{
				doNotValidateAgainstSchema.Write(sw, "doNotValidateAgainstSchema");
			}
			if (saveInvalidXml != null)
			{
				saveInvalidXml.Write(sw, "saveInvalidXml");
			}
			if (ignoreMixedContent != null)
			{
				ignoreMixedContent.Write(sw, "ignoreMixedContent");
			}
			if (alwaysShowPlaceholderText != null)
			{
				alwaysShowPlaceholderText.Write(sw, "alwaysShowPlaceholderText");
			}
			if (doNotDemarcateInvalidXml != null)
			{
				doNotDemarcateInvalidXml.Write(sw, "doNotDemarcateInvalidXml");
			}
			if (saveXmlDataOnly != null)
			{
				saveXmlDataOnly.Write(sw, "saveXmlDataOnly");
			}
			if (useXSLTWhenSaving != null)
			{
				useXSLTWhenSaving.Write(sw, "useXSLTWhenSaving");
			}
			if (saveThroughXslt != null)
			{
				saveThroughXslt.Write(sw, "saveThroughXslt");
			}
			if (showXMLTags != null)
			{
				showXMLTags.Write(sw, "showXMLTags");
			}
			if (alwaysMergeEmptyNamespace != null)
			{
				alwaysMergeEmptyNamespace.Write(sw, "alwaysMergeEmptyNamespace");
			}
			if (updateFields != null)
			{
				updateFields.Write(sw, "updateFields");
			}
			if (footnotePr != null)
			{
				footnotePr.Write(sw, "footnotePr");
			}
			if (endnotePr != null)
			{
				endnotePr.Write(sw, "endnotePr");
			}
			if (compat != null)
			{
				compat.Write(sw, "compat");
			}
			if (rsids != null)
			{
				rsids.Write(sw, "rsids");
			}
			if (mathPr != null)
			{
				mathPr.Write(sw, "mathPr");
			}
			if (uiCompat97To2003 != null)
			{
				uiCompat97To2003.Write(sw, "uiCompat97To2003");
			}
			if (themeFontLang != null)
			{
				themeFontLang.Write(sw, "themeFontLang");
			}
			if (clrSchemeMapping != null)
			{
				clrSchemeMapping.Write(sw, "clrSchemeMapping");
			}
			if (doNotIncludeSubdocsInStats != null)
			{
				doNotIncludeSubdocsInStats.Write(sw, "doNotIncludeSubdocsInStats");
			}
			if (doNotAutoCompressPictures != null)
			{
				doNotAutoCompressPictures.Write(sw, "doNotAutoCompressPictures");
			}
			if (forceUpgrade != null)
			{
				sw.Write("<w:forceUpgrade/>");
			}
			if (captions != null)
			{
				captions.Write(sw, "captions");
			}
			if (readModeInkLockDown != null)
			{
				readModeInkLockDown.Write(sw, "readModeInkLockDown");
			}
			if (doNotEmbedSmartTags != null)
			{
				doNotEmbedSmartTags.Write(sw, "doNotEmbedSmartTags");
			}
			if (decimalSymbol != null)
			{
				decimalSymbol.Write(sw, "decimalSymbol");
			}
			if (listSeparator != null)
			{
				listSeparator.Write(sw, "listSeparator");
			}
			if (activeWritingStyle != null)
			{
				foreach (CT_WritingStyle item in activeWritingStyle)
				{
					item.Write(sw, "activeWritingStyle");
				}
			}
			if (docVars != null)
			{
				foreach (CT_DocVar docVar in docVars)
				{
					docVar.Write(sw, "docVars");
				}
			}
			if (attachedSchema != null)
			{
				foreach (CT_String item2 in attachedSchema)
				{
					item2.Write(sw, "attachedSchema");
				}
			}
			if (smartTagType != null)
			{
				foreach (CT_SmartTagType item3 in smartTagType)
				{
					item3.Write(sw, "smartTagType");
				}
			}
			if (schemaLibrary != null)
			{
				foreach (CT_Schema item4 in schemaLibrary)
				{
					item4.Write(sw, "schemaLibrary");
				}
			}
			sw.Write("</w:settings>");
		}

		public CT_Settings()
		{
			listSeparatorField = new CT_String();
			listSeparator.val = ",";
			decimalSymbolField = new CT_String();
			decimalSymbol.val = ".";
			shapeDefaultsField = new XmlElement[0];
			clrSchemeMappingField = new CT_ColorSchemeMapping();
			clrSchemeMapping.bg1 = ST_ColorSchemeIndex.light1;
			clrSchemeMapping.t1 = ST_ColorSchemeIndex.dark1;
			clrSchemeMapping.bg2 = ST_ColorSchemeIndex.light2;
			clrSchemeMapping.t2 = ST_ColorSchemeIndex.dark2;
			clrSchemeMapping.accent1 = ST_ColorSchemeIndex.accent1;
			clrSchemeMapping.accent2 = ST_ColorSchemeIndex.accent2;
			clrSchemeMapping.accent3 = ST_ColorSchemeIndex.accent3;
			clrSchemeMapping.accent4 = ST_ColorSchemeIndex.accent4;
			clrSchemeMapping.accent5 = ST_ColorSchemeIndex.accent5;
			clrSchemeMapping.accent6 = ST_ColorSchemeIndex.accent6;
			clrSchemeMapping.hyperlink = ST_ColorSchemeIndex.hyperlink;
			clrSchemeMapping.followedHyperlink = ST_ColorSchemeIndex.followedHyperlink;
			themeFontLangField = new CT_Language();
			themeFontLang.val = "en-US";
			themeFontLang.eastAsia = "zh-CN";
			mathPrField = new CT_MathPr();
			rsidsField = new CT_DocRsids();
			compatField = new CT_Compat();
			characterSpacingControlField = new CT_CharacterSpacing();
			characterSpacingControl.val = ST_CharacterSpacing.compressPunctuation;
			displayVerticalDrawingGridEveryField = new CT_DecimalNumber();
			displayVerticalDrawingGridEvery.val = "2";
			displayHorizontalDrawingGridEveryField = new CT_DecimalNumber();
			displayHorizontalDrawingGridEvery.val = "0";
			drawingGridVerticalSpacingField = new CT_TwipsMeasure();
			drawingGridVerticalSpacing.val = 156uL;
			defaultTabStopField = new CT_TwipsMeasure();
			defaultTabStopField.val = 420uL;
			bordersDoNotSurroundFooterField = new CT_OnOff();
			bordersDoNotSurroundHeaderField = new CT_OnOff();
			zoomField = new CT_Zoom();
		}

		public bool IsSetZoom()
		{
			return zoom != null;
		}

		public CT_Zoom AddNewZoom()
		{
			zoom = new CT_Zoom();
			return zoom;
		}

		public bool IsSetUpdateFields()
		{
			return updateFieldsField != null;
		}
	}
}
