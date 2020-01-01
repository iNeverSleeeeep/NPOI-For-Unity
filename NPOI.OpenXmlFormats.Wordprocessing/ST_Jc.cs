using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_Jc
	{
		left,
		center,
		right,
		both,
		mediumKashida,
		distribute,
		numTab,
		highKashida,
		lowKashida,
		thaiDistribute
	}
}
