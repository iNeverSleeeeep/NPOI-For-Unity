using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlRoot("ST_TrueFalse", Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
	[XmlType(TypeName = "ST_TrueFalse", Namespace = "urn:schemas-microsoft-com:office:office")]
	public enum ST_TrueFalse
	{
		t,
		f,
		@true,
		@false
	}
}
