using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot("webSettings", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	public class CT_WebSettings
	{
		private CT_Frameset framesetField;

		private CT_Divs divsField;

		private CT_String encodingField;

		private CT_OnOff optimizeForBrowserField;

		private CT_OnOff relyOnVMLField;

		private CT_OnOff allowPNGField;

		private CT_OnOff doNotRelyOnCSSField;

		private CT_OnOff doNotSaveAsSingleFileField;

		private CT_OnOff doNotOrganizeInFolderField;

		private CT_OnOff doNotUseLongFileNamesField;

		private CT_DecimalNumber pixelsPerInchField;

		private CT_TargetScreenSz targetScreenSzField;

		private CT_OnOff saveSmartTagsAsXmlField;

		[XmlElement(Order = 0)]
		public CT_Frameset frameset
		{
			get
			{
				return framesetField;
			}
			set
			{
				framesetField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Divs divs
		{
			get
			{
				return divsField;
			}
			set
			{
				divsField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_String encoding
		{
			get
			{
				return encodingField;
			}
			set
			{
				encodingField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_OnOff optimizeForBrowser
		{
			get
			{
				return optimizeForBrowserField;
			}
			set
			{
				optimizeForBrowserField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff relyOnVML
		{
			get
			{
				return relyOnVMLField;
			}
			set
			{
				relyOnVMLField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_OnOff allowPNG
		{
			get
			{
				return allowPNGField;
			}
			set
			{
				allowPNGField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_OnOff doNotRelyOnCSS
		{
			get
			{
				return doNotRelyOnCSSField;
			}
			set
			{
				doNotRelyOnCSSField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OnOff doNotSaveAsSingleFile
		{
			get
			{
				return doNotSaveAsSingleFileField;
			}
			set
			{
				doNotSaveAsSingleFileField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_OnOff doNotOrganizeInFolder
		{
			get
			{
				return doNotOrganizeInFolderField;
			}
			set
			{
				doNotOrganizeInFolderField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_OnOff doNotUseLongFileNames
		{
			get
			{
				return doNotUseLongFileNamesField;
			}
			set
			{
				doNotUseLongFileNamesField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_DecimalNumber pixelsPerInch
		{
			get
			{
				return pixelsPerInchField;
			}
			set
			{
				pixelsPerInchField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_TargetScreenSz targetScreenSz
		{
			get
			{
				return targetScreenSzField;
			}
			set
			{
				targetScreenSzField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_OnOff saveSmartTagsAsXml
		{
			get
			{
				return saveSmartTagsAsXmlField;
			}
			set
			{
				saveSmartTagsAsXmlField = value;
			}
		}

		public CT_WebSettings()
		{
			allowPNGField = new CT_OnOff();
			optimizeForBrowserField = new CT_OnOff();
		}
	}
}
