using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_ColorSchemeIndex
	{
		dark1,
		light1,
		dark2,
		light2,
		accent1,
		accent2,
		accent3,
		accent4,
		accent5,
		accent6,
		hyperlink,
		followedHyperlink
	}
}
