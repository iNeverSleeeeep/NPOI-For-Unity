using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IncludeInSchema = false)]
	public enum Items1ChoiceType
	{
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/math:oMath")]
		oMath,
		[XmlEnum("http://schemas.openxmlformats.org/officeDocument/2006/math:oMathPara")]
		oMathPara,
		bookmarkEnd,
		bookmarkStart,
		commentRangeEnd,
		commentRangeStart,
		customXml,
		customXmlDelRangeEnd,
		customXmlDelRangeStart,
		customXmlInsRangeEnd,
		customXmlInsRangeStart,
		customXmlMoveFromRangeEnd,
		customXmlMoveFromRangeStart,
		customXmlMoveToRangeEnd,
		customXmlMoveToRangeStart,
		del,
		ins,
		moveFrom,
		moveFromRangeEnd,
		moveFromRangeStart,
		moveTo,
		moveToRangeEnd,
		moveToRangeStart,
		permEnd,
		permStart,
		proofErr,
		sdt,
		tr
	}
}
