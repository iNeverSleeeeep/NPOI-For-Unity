using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public enum ST_FillType
	{
		solid,
		gradient,
		gradientRadial,
		tile,
		pattern,
		frame
	}
}
