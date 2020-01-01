using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public enum ST_AlgorithmType
	{
		composite,
		conn,
		cycle,
		hierChild,
		hierRoot,
		pyra,
		lin,
		sp,
		tx,
		snake
	}
}
