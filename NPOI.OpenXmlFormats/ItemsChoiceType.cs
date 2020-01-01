using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes", IncludeInSchema = false)]
	public enum ItemsChoiceType
	{
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:bool")]
		@bool,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:bstr")]
		bstr,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:cy")]
		cy,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:date")]
		date,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:decimal")]
		@decimal,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:error")]
		error,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:i1")]
		i1,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:i2")]
		i2,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:i4")]
		i4,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:int")]
		@int,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:r4")]
		r4,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:r8")]
		r8,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:ui1")]
		ui1,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:ui2")]
		ui2,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:ui4")]
		ui4,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:uint")]
		@uint,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/docPropsVTypes:variant")]
		variant
	}
}
