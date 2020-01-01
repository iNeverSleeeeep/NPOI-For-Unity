using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = false)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	public enum ST_HrAlign
	{
		left,
		right,
		center
	}
}
