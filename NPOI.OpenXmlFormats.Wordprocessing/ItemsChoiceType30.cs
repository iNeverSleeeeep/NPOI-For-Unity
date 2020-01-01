using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IncludeInSchema = false)]
	public enum ItemsChoiceType30
	{
		bookmarkEnd,
		bookmarkStart,
		commentRangeEnd,
		commentRangeStart,
		customXmlDelRangeEnd,
		customXmlDelRangeStart,
		customXmlInsRangeEnd,
		customXmlInsRangeStart,
		customXmlMoveFromRangeEnd,
		customXmlMoveFromRangeStart,
		customXmlMoveToRangeEnd,
		customXmlMoveToRangeStart,
		moveFromRangeEnd,
		moveFromRangeStart,
		moveToRangeEnd,
		moveToRangeStart
	}
}
