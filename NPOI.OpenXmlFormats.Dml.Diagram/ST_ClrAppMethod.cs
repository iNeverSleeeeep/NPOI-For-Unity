using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[GeneratedCode("System.Xml", "4.0.30319.17379")]
	public enum ST_ClrAppMethod
	{
		span,
		cycle,
		repeat
	}
}
