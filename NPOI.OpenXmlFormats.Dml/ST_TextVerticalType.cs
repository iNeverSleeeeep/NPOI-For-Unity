using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = false)]
	public enum ST_TextVerticalType
	{
		horz,
		vert,
		vert270,
		wordArtVert,
		eaVert,
		mongolianVert,
		wordArtVertRtl
	}
}
