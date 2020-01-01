using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlRoot("ST_FillType", Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
	[XmlType(TypeName = "ST_FillType", Namespace = "urn:schemas-microsoft-com:office:office")]
	public enum ST_FillType1
	{
		gradientCenter,
		solid,
		pattern,
		tile,
		frame,
		gradientUnscaled,
		gradientRadial,
		gradient,
		background
	}
}
