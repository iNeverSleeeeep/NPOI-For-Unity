using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public enum ST_OnOff
	{
		[XmlEnum("0")]
		Value0,
		[XmlEnum("1")]
		Value1,
		on,
		off
	}
}
