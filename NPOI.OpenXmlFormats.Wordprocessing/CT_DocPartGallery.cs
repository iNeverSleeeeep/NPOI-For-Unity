using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_DocPartGallery
	{
		private ST_DocPartGallery valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_DocPartGallery val
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
	}
}
