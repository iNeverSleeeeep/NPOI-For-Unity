using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IncludeInSchema = false)]
	public enum ItemsChoiceType
	{
		arcTo,
		close,
		cubicBezTo,
		lnTo,
		moveTo,
		quadBezTo
	}
}
