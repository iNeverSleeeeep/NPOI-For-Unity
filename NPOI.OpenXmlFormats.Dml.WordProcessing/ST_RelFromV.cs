using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.WordProcessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/wordprocessingDrawing")]
	public enum ST_RelFromV
	{
		margin,
		page,
		paragraph,
		line,
		topMargin,
		bottomMargin,
		insideMargin,
		outsideMargin
	}
}
