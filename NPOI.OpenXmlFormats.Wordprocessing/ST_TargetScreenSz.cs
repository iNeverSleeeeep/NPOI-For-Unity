using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_TargetScreenSz
	{
		[XmlEnum("544x376")]
		Item544x376,
		[XmlEnum("640x480")]
		Item640x480,
		[XmlEnum("720x512")]
		Item720x512,
		[XmlEnum("800x600")]
		Item800x600,
		[XmlEnum("1024x768")]
		Item1024x768,
		[XmlEnum("1152x882")]
		Item1152x882,
		[XmlEnum("1152x900")]
		Item1152x900,
		[XmlEnum("1280x1024")]
		Item1280x1024,
		[XmlEnum("1600x1200")]
		Item1600x1200,
		[XmlEnum("1800x1440")]
		Item1800x1440,
		[XmlEnum("1920x1200")]
		Item1920x1200
	}
}
