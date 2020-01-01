using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public enum ST_AxisType
	{
		self,
		ch,
		des,
		desOrSelf,
		par,
		ancst,
		ancstOrSelf,
		followSib,
		precedSib,
		follow,
		preced,
		root,
		none
	}
}
