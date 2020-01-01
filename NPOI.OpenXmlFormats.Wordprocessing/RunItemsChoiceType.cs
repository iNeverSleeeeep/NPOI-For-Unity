using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IncludeInSchema = false)]
	public enum RunItemsChoiceType
	{
		annotationRef,
		br,
		commentReference,
		continuationSeparator,
		cr,
		dayLong,
		dayShort,
		delInstrText,
		delText,
		drawing,
		endnoteRef,
		endnoteReference,
		fldChar,
		footnoteRef,
		footnoteReference,
		instrText,
		lastRenderedPageBreak,
		monthLong,
		monthShort,
		noBreakHyphen,
		@object,
		pgNum,
		pict,
		ptab,
		ruby,
		separator,
		softHyphen,
		sym,
		t,
		tab,
		yearLong,
		yearShort
	}
}
