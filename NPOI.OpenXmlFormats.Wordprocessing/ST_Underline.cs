using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_Underline
	{
		single,
		words,
		@double,
		thick,
		dotted,
		dottedHeavy,
		dash,
		dashedHeavy,
		dashLong,
		dashLongHeavy,
		dotDash,
		dashDotHeavy,
		dotDotDash,
		dashDotDotHeavy,
		wave,
		wavyHeavy,
		wavyDouble,
		none
	}
}
