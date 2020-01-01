using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public enum ST_SchemeColorVal
	{
		bg1,
		tx1,
		bg2,
		tx2,
		accent1,
		accent2,
		accent3,
		accent4,
		accent5,
		accent6,
		hlink,
		folHlink,
		phClr,
		dk1,
		lt1,
		dk2,
		lt2
	}
}
