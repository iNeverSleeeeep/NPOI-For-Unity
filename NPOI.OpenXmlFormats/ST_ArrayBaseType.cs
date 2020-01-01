using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
	public enum ST_ArrayBaseType
	{
		variant,
		i1,
		i2,
		i4,
		@int,
		ui1,
		ui2,
		ui4,
		@uint,
		r4,
		r8,
		@decimal,
		bstr,
		date,
		@bool,
		cy,
		error
	}
}
