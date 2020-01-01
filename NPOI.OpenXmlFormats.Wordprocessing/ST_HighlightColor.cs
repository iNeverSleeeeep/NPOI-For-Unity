using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_HighlightColor
	{
		black,
		blue,
		cyan,
		green,
		magenta,
		red,
		yellow,
		white,
		darkBlue,
		darkCyan,
		darkGreen,
		darkMagenta,
		darkRed,
		darkYellow,
		darkGray,
		lightGray,
		none
	}
}
