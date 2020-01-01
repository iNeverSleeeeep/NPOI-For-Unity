using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = false)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public enum ST_Ext
	{
		NONE,
		view,
		edit,
		backwardCompatible
	}
}
