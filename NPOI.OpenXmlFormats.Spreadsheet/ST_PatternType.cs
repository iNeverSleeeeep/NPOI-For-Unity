using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/spreadsheetml/2006/main")]
	public enum ST_PatternType
	{
		none,
		solid,
		mediumGray,
		darkGray,
		lightGray,
		darkHorizontal,
		darkVertical,
		darkDown,
		darkUp,
		darkGrid,
		darkTrellis,
		lightHorizontal,
		lightVertical,
		lightDown,
		lightUp,
		lightGrid,
		lightTrellis,
		gray125,
		gray0625
	}
}
