using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	public enum ST_ScreenSize
	{
		[XmlEnum("544,376")]
		Item544376,
		[XmlEnum("640,480")]
		Item640480,
		[XmlEnum("720,512")]
		Item720512,
		[XmlEnum("800,600")]
		Item800600,
		[XmlEnum("1024,768")]
		Item1024768,
		[XmlEnum("1152,862")]
		Item1152862
	}
}
