using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_TextShapeType
	{
		textNoShape,
		textPlain,
		textStop,
		textTriangle,
		textTriangleInverted,
		textChevron,
		textChevronInverted,
		textRingInside,
		textRingOutside,
		textArchUp,
		textArchDown,
		textCircle,
		textButton,
		textArchUpPour,
		textArchDownPour,
		textCirclePour,
		textButtonPour,
		textCurveUp,
		textCurveDown,
		textCanUp,
		textCanDown,
		textWave1,
		textWave2,
		textDoubleWave1,
		textWave4,
		textInflate,
		textDeflate,
		textInflateBottom,
		textDeflateBottom,
		textInflateTop,
		textDeflateTop,
		textDeflateInflate,
		textDeflateInflateDeflate,
		textFadeRight,
		textFadeLeft,
		textFadeUp,
		textFadeDown,
		textSlantUp,
		textSlantDown,
		textCascadeUp,
		textCascadeDown
	}
}
