using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_FontRel : CT_Rel
	{
		private string fontKeyField;

		private ST_OnOff subsettedField;

		private bool subsettedFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "token")]
		public string fontKey
		{
			get
			{
				return fontKeyField;
			}
			set
			{
				fontKeyField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff subsetted
		{
			get
			{
				return subsettedField;
			}
			set
			{
				subsettedField = value;
			}
		}

		[XmlIgnore]
		public bool subsettedSpecified
		{
			get
			{
				return subsettedFieldSpecified;
			}
			set
			{
				subsettedFieldSpecified = value;
			}
		}
	}
}
