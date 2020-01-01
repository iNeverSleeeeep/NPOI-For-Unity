using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
	public enum ST_BWMode
	{
		color,
		auto,
		grayScale,
		lightGrayscale,
		inverseGray,
		grayOutline,
		highContrast,
		black,
		white,
		hide,
		undrawn,
		blackTextAndLines
	}
}
