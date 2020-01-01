using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml", IncludeInSchema = false)]
	public enum ItemsChoiceType2
	{
		[XmlEnum("urn:schemas-microsoft-com:office:excel:ClientData")]
		ClientData,
		[XmlEnum("urn:schemas-microsoft-com:office:powerpoint:textdata")]
		textdata,
		[XmlEnum("urn:schemas-microsoft-com:office:word:anchorlock")]
		anchorlock,
		[XmlEnum("urn:schemas-microsoft-com:office:word:borderbottom")]
		borderbottom,
		[XmlEnum("urn:schemas-microsoft-com:office:word:borderleft")]
		borderleft,
		[XmlEnum("urn:schemas-microsoft-com:office:word:borderright")]
		borderright,
		[XmlEnum("urn:schemas-microsoft-com:office:word:bordertop")]
		bordertop,
		[XmlEnum("urn:schemas-microsoft-com:office:word:wrap")]
		wrap,
		fill,
		formulas,
		handles,
		imagedata,
		path,
		shadow,
		stroke,
		textbox,
		textpath
	}
}
