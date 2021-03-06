using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_BevelPresetType
	{
		relaxedInset,
		circle,
		slope,
		cross,
		angle,
		softRound,
		convex,
		coolSlant,
		divot,
		riblet,
		hardEdge,
		artDeco
	}
}
