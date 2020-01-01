using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_PixelsMeasure
	{
		private ulong valField;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ulong val
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
