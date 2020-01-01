using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes")]
	public enum ST_VectorBaseType
	{
		variant,
		i1,
		i2,
		i4,
		i8,
		ui1,
		ui2,
		ui4,
		ui8,
		r4,
		r8,
		lpstr,
		lpwstr,
		bstr,
		date,
		filetime,
		@bool,
		cy,
		error,
		clsid,
		cf
	}
}
