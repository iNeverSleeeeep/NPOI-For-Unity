using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	public enum ST_RelFromH
	{
		margin,
		page,
		column,
		character,
		leftMargin,
		rightMargin,
		insideMargin,
		outsideMargin
	}
}
