using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public enum ST_MarkerStyle
	{
		circle,
		dash,
		diamond,
		dot,
		none,
		picture,
		plus,
		square,
		star,
		triangle,
		x
	}
}
