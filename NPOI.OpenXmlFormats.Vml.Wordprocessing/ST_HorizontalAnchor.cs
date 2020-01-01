using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:word")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:word", IsNullable = false)]
	public enum ST_HorizontalAnchor
	{
		margin,
		page,
		text,
		@char
	}
}
