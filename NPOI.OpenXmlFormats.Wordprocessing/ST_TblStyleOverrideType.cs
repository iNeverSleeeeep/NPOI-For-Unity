using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_TblStyleOverrideType
	{
		wholeTable,
		firstRow,
		lastRow,
		firstCol,
		lastCol,
		band1Vert,
		band2Vert,
		band1Horz,
		band2Horz,
		neCell,
		nwCell,
		seCell,
		swCell
	}
}
