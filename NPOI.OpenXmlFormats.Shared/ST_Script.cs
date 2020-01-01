using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public enum ST_Script
	{
		roman,
		script,
		fraktur,
		[XmlEnum("double-struck")]
		doublestruck,
		[XmlEnum("sans-serif")]
		sansserif,
		monospace
	}
}
