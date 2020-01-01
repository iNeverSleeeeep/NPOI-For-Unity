using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public enum ST_MailMergeDocType
	{
		catalog,
		envelopes,
		mailingLabels,
		formLetters,
		email,
		fax
	}
}
