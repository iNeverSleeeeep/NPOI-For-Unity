using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:word")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:word", IsNullable = false)]
	public enum ST_BorderType
	{
		none,
		single,
		thick,
		@double,
		hairline,
		dot,
		dash,
		dotDash,
		dashDotDot,
		triple,
		thinThickSmall,
		thickThinSmall,
		thickBetweenThinSmall,
		thinThick,
		thickThin,
		thickBetweenThin,
		thinThickLarge,
		thickThinLarge,
		thickBetweenThinLarge,
		wave,
		doubleWave,
		dashedSmall,
		dashDotStroked,
		threeDEmboss,
		threeDEngrave,
		HTMLOutset,
		HTMLInset
	}
}
