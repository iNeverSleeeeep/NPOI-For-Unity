using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Font
	{
		private CT_String altNameField;

		private CT_Panose panose1Field;

		private CT_UcharHexNumber charsetField;

		private CT_FontFamily familyField;

		private CT_OnOff notTrueTypeField;

		private CT_Pitch pitchField;

		private CT_FontSig sigField;

		private CT_FontRel embedRegularField;

		private CT_FontRel embedBoldField;

		private CT_FontRel embedItalicField;

		private CT_FontRel embedBoldItalicField;

		private string nameField;

		[XmlElement(Order = 0)]
		public CT_String altName
		{
			get
			{
				return altNameField;
			}
			set
			{
				altNameField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Panose panose1
		{
			get
			{
				return panose1Field;
			}
			set
			{
				panose1Field = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_UcharHexNumber charset
		{
			get
			{
				return charsetField;
			}
			set
			{
				charsetField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_FontFamily family
		{
			get
			{
				return familyField;
			}
			set
			{
				familyField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_OnOff notTrueType
		{
			get
			{
				return notTrueTypeField;
			}
			set
			{
				notTrueTypeField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_Pitch pitch
		{
			get
			{
				return pitchField;
			}
			set
			{
				pitchField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_FontSig sig
		{
			get
			{
				return sigField;
			}
			set
			{
				sigField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_FontRel embedRegular
		{
			get
			{
				return embedRegularField;
			}
			set
			{
				embedRegularField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_FontRel embedBold
		{
			get
			{
				return embedBoldField;
			}
			set
			{
				embedBoldField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_FontRel embedItalic
		{
			get
			{
				return embedItalicField;
			}
			set
			{
				embedItalicField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_FontRel embedBoldItalic
		{
			get
			{
				return embedBoldItalicField;
			}
			set
			{
				embedBoldItalicField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public CT_Font()
		{
			embedBoldItalicField = new CT_FontRel();
			embedItalicField = new CT_FontRel();
			embedBoldField = new CT_FontRel();
			embedRegularField = new CT_FontRel();
			sigField = new CT_FontSig();
			pitchField = new CT_Pitch();
			notTrueTypeField = new CT_OnOff();
			familyField = new CT_FontFamily();
			charsetField = new CT_UcharHexNumber();
			panose1Field = new CT_Panose();
			altNameField = new CT_String();
		}
	}
}
