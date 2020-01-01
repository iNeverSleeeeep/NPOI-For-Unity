using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_MailMergeSourceType
	{
		database,
		addressBook,
		document1,
		document2,
		text,
		email,
		native,
		legacy,
		master
	}
}
