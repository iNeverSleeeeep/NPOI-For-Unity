using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public enum ST_FillMethod
	{
		none,
		linear,
		sigma,
		any,
		[XmlEnum("linear sigma")]
		linearsigma
	}
}
