using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public class CT_Style
	{
		private ST_Style valField;

		private bool valFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_Style val
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

		[XmlIgnore]
		public bool valSpecified
		{
			get
			{
				return valFieldSpecified;
			}
			set
			{
				valFieldSpecified = value;
			}
		}
	}
}
