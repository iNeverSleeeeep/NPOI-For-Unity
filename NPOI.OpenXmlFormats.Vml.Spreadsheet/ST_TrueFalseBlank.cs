using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:excel", IsNullable = false)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:excel")]
	public enum ST_TrueFalseBlank
	{
		NONE,
		[XmlEnum("True")]
		@true,
		t,
		[XmlEnum("False")]
		@false,
		f
	}
}
