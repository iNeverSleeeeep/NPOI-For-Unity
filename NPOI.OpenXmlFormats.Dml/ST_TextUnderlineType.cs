using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_TextUnderlineType
	{
		none,
		words,
		sng,
		dbl,
		heavy,
		dotted,
		dottedHeavy,
		dash,
		dashHeavy,
		dashLong,
		dashLongHeavy,
		dotDash,
		dotDashHeavy,
		dotDotDash,
		dotDotDashHeavy,
		wavy,
		wavyHeavy,
		wavyDbl
	}
}
