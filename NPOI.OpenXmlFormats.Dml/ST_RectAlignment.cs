using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_RectAlignment
	{
		tl,
		t,
		tr,
		l,
		ctr,
		r,
		bl,
		b,
		br
	}
}
