using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IncludeInSchema = false)]
	public enum ItemsChoiceType2
	{
		cantSplit,
		cnfStyle,
		divId,
		gridAfter,
		gridBefore,
		hidden,
		jc,
		tblCellSpacing,
		tblHeader,
		trHeight,
		wAfter,
		wBefore
	}
}
