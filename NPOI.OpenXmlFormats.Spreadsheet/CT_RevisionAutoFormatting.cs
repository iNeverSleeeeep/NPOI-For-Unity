using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionAutoFormatting
	{
		private uint sheetIdField;

		private uint autoFormatIdField;

		private bool autoFormatIdFieldSpecified;

		private bool applyNumberFormatsField;

		private bool applyNumberFormatsFieldSpecified;

		private bool applyBorderFormatsField;

		private bool applyBorderFormatsFieldSpecified;

		private bool applyFontFormatsField;

		private bool applyFontFormatsFieldSpecified;

		private bool applyPatternFormatsField;

		private bool applyPatternFormatsFieldSpecified;

		private bool applyAlignmentFormatsField;

		private bool applyAlignmentFormatsFieldSpecified;

		private bool applyWidthHeightFormatsField;

		private bool applyWidthHeightFormatsFieldSpecified;

		private string refField;

		public uint sheetId
		{
			get
			{
				return sheetIdField;
			}
			set
			{
				sheetIdField = value;
			}
		}

		public uint autoFormatId
		{
			get
			{
				return autoFormatIdField;
			}
			set
			{
				autoFormatIdField = value;
			}
		}

		[XmlIgnore]
		public bool autoFormatIdSpecified
		{
			get
			{
				return autoFormatIdFieldSpecified;
			}
			set
			{
				autoFormatIdFieldSpecified = value;
			}
		}

		public bool applyNumberFormats
		{
			get
			{
				return applyNumberFormatsField;
			}
			set
			{
				applyNumberFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyNumberFormatsSpecified
		{
			get
			{
				return applyNumberFormatsFieldSpecified;
			}
			set
			{
				applyNumberFormatsFieldSpecified = value;
			}
		}

		public bool applyBorderFormats
		{
			get
			{
				return applyBorderFormatsField;
			}
			set
			{
				applyBorderFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyBorderFormatsSpecified
		{
			get
			{
				return applyBorderFormatsFieldSpecified;
			}
			set
			{
				applyBorderFormatsFieldSpecified = value;
			}
		}

		public bool applyFontFormats
		{
			get
			{
				return applyFontFormatsField;
			}
			set
			{
				applyFontFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyFontFormatsSpecified
		{
			get
			{
				return applyFontFormatsFieldSpecified;
			}
			set
			{
				applyFontFormatsFieldSpecified = value;
			}
		}

		public bool applyPatternFormats
		{
			get
			{
				return applyPatternFormatsField;
			}
			set
			{
				applyPatternFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyPatternFormatsSpecified
		{
			get
			{
				return applyPatternFormatsFieldSpecified;
			}
			set
			{
				applyPatternFormatsFieldSpecified = value;
			}
		}

		public bool applyAlignmentFormats
		{
			get
			{
				return applyAlignmentFormatsField;
			}
			set
			{
				applyAlignmentFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyAlignmentFormatsSpecified
		{
			get
			{
				return applyAlignmentFormatsFieldSpecified;
			}
			set
			{
				applyAlignmentFormatsFieldSpecified = value;
			}
		}

		public bool applyWidthHeightFormats
		{
			get
			{
				return applyWidthHeightFormatsField;
			}
			set
			{
				applyWidthHeightFormatsField = value;
			}
		}

		[XmlIgnore]
		public bool applyWidthHeightFormatsSpecified
		{
			get
			{
				return applyWidthHeightFormatsFieldSpecified;
			}
			set
			{
				applyWidthHeightFormatsFieldSpecified = value;
			}
		}

		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
			}
		}
	}
}
