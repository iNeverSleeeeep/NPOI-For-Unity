using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Guid
	{
		private string valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified, DataType = "token")]
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
	}
}
