using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_LightRigType
	{
		legacyFlat1,
		legacyFlat2,
		legacyFlat3,
		legacyFlat4,
		legacyNormal1,
		legacyNormal2,
		legacyNormal3,
		legacyNormal4,
		legacyHarsh1,
		legacyHarsh2,
		legacyHarsh3,
		legacyHarsh4,
		threePt,
		balanced,
		soft,
		harsh,
		flood,
		contrasting,
		morning,
		sunrise,
		sunset,
		chilly,
		freezing,
		flat,
		twoPt,
		glow,
		brightRoom
	}
}
