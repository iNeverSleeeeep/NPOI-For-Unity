using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:excel", IsNullable = false)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:excel")]
	public enum ST_CF
	{
		PictOld,
		Pict,
		Bitmap,
		PictPrint,
		PictScreen
	}
}
