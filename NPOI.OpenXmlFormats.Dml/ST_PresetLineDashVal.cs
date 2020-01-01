using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_PresetLineDashVal
	{
		solid,
		dot,
		dash,
		lgDash,
		dashDot,
		lgDashDot,
		lgDashDotDot,
		sysDash,
		sysDot,
		sysDashDot,
		sysDashDotDot
	}
}
