using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties")]
	[XmlRoot("Properties", Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/extended-properties", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_ExtendedProperties
	{
		private string templateField;

		private string managerField;

		private string companyField;

		private int pagesField;

		private bool pagesFieldSpecified;

		private int wordsField;

		private bool wordsFieldSpecified;

		private int charactersField;

		private bool charactersFieldSpecified;

		private string presentationFormatField;

		private int linesField;

		private bool linesFieldSpecified;

		private int paragraphsField;

		private bool paragraphsFieldSpecified;

		private int slidesField;

		private bool slidesFieldSpecified;

		private int notesField;

		private bool notesFieldSpecified;

		private int totalTimeField;

		private bool totalTimeFieldSpecified;

		private int hiddenSlidesField;

		private bool hiddenSlidesFieldSpecified;

		private int mMClipsField;

		private bool mMClipsFieldSpecified;

		private bool scaleCropField;

		private bool scaleCropFieldSpecified;

		private CT_VectorVariant headingPairsField;

		private CT_VectorLpstr titlesOfPartsField;

		private bool linksUpToDateField;

		private bool linksUpToDateFieldSpecified;

		private int charactersWithSpacesField;

		private bool charactersWithSpacesFieldSpecified;

		private bool sharedDocField;

		private bool sharedDocFieldSpecified;

		private string hyperlinkBaseField;

		private CT_VectorVariant hLinksField;

		private bool hyperlinksChangedField;

		private bool hyperlinksChangedFieldSpecified;

		private CT_DigSigBlob digSigField;

		private string applicationField;

		private string appVersionField;

		private int docSecurityField;

		private bool docSecurityFieldSpecified;

		[XmlElement]
		public string Template
		{
			get
			{
				return templateField;
			}
			set
			{
				templateField = value;
			}
		}

		[XmlElement]
		public string Manager
		{
			get
			{
				return managerField;
			}
			set
			{
				managerField = value;
			}
		}

		[XmlElement]
		public string Company
		{
			get
			{
				return companyField;
			}
			set
			{
				companyField = value;
			}
		}

		[XmlElement]
		public int Pages
		{
			get
			{
				return pagesField;
			}
			set
			{
				pagesField = value;
			}
		}

		[XmlIgnore]
		public bool PagesSpecified
		{
			get
			{
				return pagesFieldSpecified;
			}
			set
			{
				pagesFieldSpecified = value;
			}
		}

		[XmlElement]
		public int Words
		{
			get
			{
				return wordsField;
			}
			set
			{
				wordsField = value;
			}
		}

		[XmlIgnore]
		public bool WordsSpecified
		{
			get
			{
				return wordsFieldSpecified;
			}
			set
			{
				wordsFieldSpecified = value;
			}
		}

		[XmlElement]
		public int Characters
		{
			get
			{
				return charactersField;
			}
			set
			{
				charactersField = value;
			}
		}

		[XmlIgnore]
		public bool CharactersSpecified
		{
			get
			{
				return charactersFieldSpecified;
			}
			set
			{
				charactersFieldSpecified = value;
			}
		}

		[XmlElement]
		public string PresentationFormat
		{
			get
			{
				return presentationFormatField;
			}
			set
			{
				presentationFormatField = value;
			}
		}

		[XmlElement]
		public int Lines
		{
			get
			{
				return linesField;
			}
			set
			{
				linesField = value;
			}
		}

		[XmlIgnore]
		public bool LinesSpecified
		{
			get
			{
				return linesFieldSpecified;
			}
			set
			{
				linesFieldSpecified = value;
			}
		}

		[XmlElement]
		public int Paragraphs
		{
			get
			{
				return paragraphsField;
			}
			set
			{
				paragraphsField = value;
			}
		}

		[XmlIgnore]
		public bool ParagraphsSpecified
		{
			get
			{
				return paragraphsFieldSpecified;
			}
			set
			{
				paragraphsFieldSpecified = value;
			}
		}

		[XmlElement]
		public int Slides
		{
			get
			{
				return slidesField;
			}
			set
			{
				slidesField = value;
				slidesFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool SlidesSpecified
		{
			get
			{
				return slidesFieldSpecified;
			}
			set
			{
				slidesFieldSpecified = value;
			}
		}

		[XmlElement]
		public int Notes
		{
			get
			{
				return notesField;
			}
			set
			{
				notesField = value;
				notesFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool NotesSpecified
		{
			get
			{
				return notesFieldSpecified;
			}
			set
			{
				notesFieldSpecified = value;
			}
		}

		[XmlElement]
		public int TotalTime
		{
			get
			{
				return totalTimeField;
			}
			set
			{
				totalTimeField = value;
			}
		}

		[XmlIgnore]
		public bool TotalTimeSpecified
		{
			get
			{
				return totalTimeFieldSpecified;
			}
			set
			{
				totalTimeFieldSpecified = value;
			}
		}

		[XmlElement]
		public int HiddenSlides
		{
			get
			{
				return hiddenSlidesField;
			}
			set
			{
				hiddenSlidesField = value;
				hiddenSlidesFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool HiddenSlidesSpecified
		{
			get
			{
				return hiddenSlidesFieldSpecified;
			}
			set
			{
				hiddenSlidesFieldSpecified = value;
			}
		}

		[XmlElement]
		public int MMClips
		{
			get
			{
				return mMClipsField;
			}
			set
			{
				mMClipsField = value;
				mMClipsFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool MMClipsSpecified
		{
			get
			{
				return mMClipsFieldSpecified;
			}
			set
			{
				mMClipsFieldSpecified = value;
			}
		}

		[XmlElement]
		public bool ScaleCrop
		{
			get
			{
				return scaleCropField;
			}
			set
			{
				scaleCropField = value;
				scaleCropFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool ScaleCropSpecified
		{
			get
			{
				return scaleCropFieldSpecified;
			}
			set
			{
				scaleCropFieldSpecified = value;
			}
		}

		[XmlElement]
		public CT_VectorVariant HeadingPairs
		{
			get
			{
				return headingPairsField;
			}
			set
			{
				headingPairsField = value;
			}
		}

		[XmlElement]
		public CT_VectorLpstr TitlesOfParts
		{
			get
			{
				return titlesOfPartsField;
			}
			set
			{
				titlesOfPartsField = value;
			}
		}

		[XmlElement]
		public bool LinksUpToDate
		{
			get
			{
				return linksUpToDateField;
			}
			set
			{
				linksUpToDateField = value;
				linksUpToDateFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool LinksUpToDateSpecified
		{
			get
			{
				return linksUpToDateFieldSpecified;
			}
			set
			{
				linksUpToDateFieldSpecified = value;
			}
		}

		[XmlElement]
		public int CharactersWithSpaces
		{
			get
			{
				return charactersWithSpacesField;
			}
			set
			{
				charactersWithSpacesField = value;
			}
		}

		[XmlIgnore]
		public bool CharactersWithSpacesSpecified
		{
			get
			{
				return charactersWithSpacesFieldSpecified;
			}
			set
			{
				charactersWithSpacesFieldSpecified = value;
			}
		}

		[XmlElement]
		public bool SharedDoc
		{
			get
			{
				return sharedDocField;
			}
			set
			{
				sharedDocField = value;
				sharedDocFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool SharedDocSpecified
		{
			get
			{
				return sharedDocFieldSpecified;
			}
			set
			{
				sharedDocFieldSpecified = value;
			}
		}

		[XmlElement]
		public string HyperlinkBase
		{
			get
			{
				return hyperlinkBaseField;
			}
			set
			{
				hyperlinkBaseField = value;
			}
		}

		public CT_VectorVariant HLinks
		{
			get
			{
				return hLinksField;
			}
			set
			{
				hLinksField = value;
			}
		}

		[XmlElement]
		public bool HyperlinksChanged
		{
			get
			{
				return hyperlinksChangedField;
			}
			set
			{
				hyperlinksChangedField = value;
				hyperlinksChangedFieldSpecified = true;
			}
		}

		[XmlIgnore]
		public bool HyperlinksChangedSpecified
		{
			get
			{
				return hyperlinksChangedFieldSpecified;
			}
			set
			{
				hyperlinksChangedFieldSpecified = value;
			}
		}

		[XmlElement]
		public CT_DigSigBlob DigSig
		{
			get
			{
				return digSigField;
			}
			set
			{
				digSigField = value;
			}
		}

		[XmlElement]
		public string Application
		{
			get
			{
				return applicationField;
			}
			set
			{
				applicationField = value;
			}
		}

		[XmlElement]
		public string AppVersion
		{
			get
			{
				return appVersionField;
			}
			set
			{
				appVersionField = value;
			}
		}

		[XmlElement]
		public int DocSecurity
		{
			get
			{
				return docSecurityField;
			}
			set
			{
				docSecurityField = value;
			}
		}

		[XmlIgnore]
		public bool DocSecuritySpecified
		{
			get
			{
				return docSecurityFieldSpecified;
			}
			set
			{
				docSecurityFieldSpecified = value;
			}
		}

		public CT_ExtendedProperties Copy()
		{
			CT_ExtendedProperties cT_ExtendedProperties = new CT_ExtendedProperties();
			cT_ExtendedProperties.applicationField = applicationField;
			cT_ExtendedProperties.appVersionField = appVersionField;
			cT_ExtendedProperties.charactersField = charactersField;
			cT_ExtendedProperties.charactersWithSpacesField = charactersWithSpacesField;
			cT_ExtendedProperties.companyField = companyField;
			cT_ExtendedProperties.digSigField = digSigField;
			cT_ExtendedProperties.docSecurityField = docSecurityField;
			cT_ExtendedProperties.headingPairsField = headingPairsField;
			cT_ExtendedProperties.hiddenSlidesField = hiddenSlidesField;
			cT_ExtendedProperties.hLinksField = hLinksField;
			cT_ExtendedProperties.hyperlinkBaseField = hyperlinkBaseField;
			cT_ExtendedProperties.hyperlinksChangedField = hyperlinksChangedField;
			cT_ExtendedProperties.hyperlinksChangedFieldSpecified = hyperlinksChangedFieldSpecified;
			cT_ExtendedProperties.linesField = linesField;
			cT_ExtendedProperties.linksUpToDateFieldSpecified = linksUpToDateFieldSpecified;
			cT_ExtendedProperties.linksUpToDateField = linksUpToDateField;
			cT_ExtendedProperties.managerField = managerField;
			cT_ExtendedProperties.mMClipsField = mMClipsField;
			cT_ExtendedProperties.mMClipsFieldSpecified = mMClipsFieldSpecified;
			cT_ExtendedProperties.notesField = notesField;
			cT_ExtendedProperties.notesFieldSpecified = notesFieldSpecified;
			cT_ExtendedProperties.pagesField = pagesField;
			cT_ExtendedProperties.pagesFieldSpecified = pagesFieldSpecified;
			cT_ExtendedProperties.paragraphsField = paragraphsField;
			cT_ExtendedProperties.paragraphsFieldSpecified = paragraphsFieldSpecified;
			cT_ExtendedProperties.presentationFormatField = presentationFormatField;
			cT_ExtendedProperties.scaleCropField = scaleCropField;
			cT_ExtendedProperties.scaleCropFieldSpecified = scaleCropFieldSpecified;
			cT_ExtendedProperties.sharedDocField = sharedDocField;
			cT_ExtendedProperties.sharedDocFieldSpecified = sharedDocFieldSpecified;
			cT_ExtendedProperties.slidesField = slidesField;
			cT_ExtendedProperties.templateField = templateField;
			cT_ExtendedProperties.titlesOfPartsField = titlesOfPartsField;
			cT_ExtendedProperties.totalTimeField = totalTimeField;
			cT_ExtendedProperties.totalTimeFieldSpecified = totalTimeFieldSpecified;
			cT_ExtendedProperties.wordsField = wordsField;
			return cT_ExtendedProperties;
		}

		public CT_VectorVariant GetHeadingPairs()
		{
			throw new NotImplementedException();
		}

		public bool IsSetHiddenSlides()
		{
			return hiddenSlidesFieldSpecified;
		}

		public bool IsSetHLinks()
		{
			return hLinksField != null;
		}

		public bool IsSetHyperlinksChanged()
		{
			return hyperlinksChangedFieldSpecified;
		}

		public bool IsSetLinksUpToDate()
		{
			return linksUpToDateFieldSpecified;
		}

		public bool IsSetMMClips()
		{
			return mMClipsFieldSpecified;
		}

		public bool IsSetNotes()
		{
			return notesFieldSpecified;
		}

		public bool IsSetScaleCrop()
		{
			return scaleCropFieldSpecified;
		}

		public bool IsSetSharedDoc()
		{
			return sharedDocFieldSpecified;
		}

		public bool IsSetSlides()
		{
			return slidesFieldSpecified;
		}
	}
}
