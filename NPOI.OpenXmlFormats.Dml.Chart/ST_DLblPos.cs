using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	public enum ST_DLblPos
	{
		bestFit,
		b,
		ctr,
		inBase,
		inEnd,
		l,
		outEnd,
		r,
		t
	}
}
