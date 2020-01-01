using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_Shd
	{
		nil,
		clear,
		solid,
		horzStripe,
		vertStripe,
		reverseDiagStripe,
		diagStripe,
		horzCross,
		diagCross,
		thinHorzStripe,
		thinVertStripe,
		thinReverseDiagStripe,
		thinDiagStripe,
		thinHorzCross,
		thinDiagCross,
		pct5,
		pct10,
		pct12,
		pct15,
		pct20,
		pct25,
		pct30,
		pct35,
		pct37,
		pct40,
		pct45,
		pct50,
		pct55,
		pct60,
		pct62,
		pct65,
		pct70,
		pct75,
		pct80,
		pct85,
		pct87,
		pct90,
		pct95
	}
}
