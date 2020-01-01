using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Spreadsheet
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:excel", IsNullable = false)]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:excel")]
	public enum ST_ObjectType
	{
		Button,
		Checkbox,
		Dialog,
		Drop,
		Edit,
		GBox,
		Label,
		LineA,
		List,
		Movie,
		Note,
		Pict,
		Radio,
		RectA,
		Scroll,
		Spin,
		Shape,
		Group,
		Rect
	}
}
