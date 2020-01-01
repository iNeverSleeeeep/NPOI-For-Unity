using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_DocPartName
	{
		private string valField;

		private ST_OnOff decoratedField;

		private bool decoratedFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public string val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff decorated
		{
			get
			{
				return decoratedField;
			}
			set
			{
				decoratedField = value;
			}
		}

		[XmlIgnore]
		public bool decoratedSpecified
		{
			get
			{
				return decoratedFieldSpecified;
			}
			set
			{
				decoratedFieldSpecified = value;
			}
		}
	}
}
