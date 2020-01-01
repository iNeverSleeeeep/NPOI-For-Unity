using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public enum ST_ExternalConnectionType
	{
		general,
		text,
		MDY,
		DMY,
		YMD,
		MYD,
		DYM,
		YDM,
		skip,
		EMD
	}
}
