using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	public enum ST_BreakBinSub
	{
		[XmlEnum("--")]
		Item,
		[XmlEnum("-+")]
		Item1,
		[XmlEnum("+-")]
		Item2
	}
}
