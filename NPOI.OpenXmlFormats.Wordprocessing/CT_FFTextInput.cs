using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_FFTextInput
	{
		private CT_FFTextType typeField;

		private CT_String defaultField;

		private CT_DecimalNumber maxLengthField;

		private CT_String formatField;

		[XmlElement(Order = 0)]
		public CT_FFTextType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_String @default
		{
			get
			{
				return defaultField;
			}
			set
			{
				defaultField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_DecimalNumber maxLength
		{
			get
			{
				return maxLengthField;
			}
			set
			{
				maxLengthField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_String format
		{
			get
			{
				return formatField;
			}
			set
			{
				formatField = value;
			}
		}

		public CT_FFTextInput()
		{
			formatField = new CT_String();
			maxLengthField = new CT_DecimalNumber();
			defaultField = new CT_String();
			typeField = new CT_FFTextType();
		}
	}
}
