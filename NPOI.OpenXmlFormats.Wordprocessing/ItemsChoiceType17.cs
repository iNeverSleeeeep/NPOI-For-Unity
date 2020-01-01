using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IncludeInSchema = false)]
	public enum ItemsChoiceType17
	{
		alias,
		bibliography,
		citation,
		comboBox,
		dataBinding,
		date,
		docPartList,
		docPartObj,
		dropDownList,
		equation,
		group,
		id,
		@lock,
		picture,
		placeholder,
		rPr,
		richText,
		showingPlcHdr,
		tag,
		temporary,
		text
	}
}
