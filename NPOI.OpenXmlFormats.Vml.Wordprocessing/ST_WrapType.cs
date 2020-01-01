using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:word", IsNullable = false)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:word")]
	public enum ST_WrapType
	{
		topAndBottom,
		square,
		none,
		tight,
		through
	}
}
