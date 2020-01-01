using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IncludeInSchema = false)]
	public enum ItemsChoiceType14
	{
		calcOnExit,
		checkBox,
		ddList,
		enabled,
		entryMacro,
		exitMacro,
		helpText,
		name,
		statusText,
		textInput
	}
}
