using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IncludeInSchema = false)]
	public enum ItemsChoiceType9
	{
		[XmlEnum("urn:schemas-microsoft-com:office:office:")]
		office,
		[XmlEnum("urn:schemas-microsoft-com:vml:")]
		vml
	}
}
