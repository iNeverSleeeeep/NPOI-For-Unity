using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	public enum ST_Angle
	{
		any,
		[XmlEnum("30")]
		Item30,
		[XmlEnum("45")]
		Item45,
		[XmlEnum("60")]
		Item60,
		[XmlEnum("90")]
		Item90,
		auto
	}
}
