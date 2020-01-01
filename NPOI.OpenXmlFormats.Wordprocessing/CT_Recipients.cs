using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot("recipients", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_Recipients
	{
		private List<CT_RecipientData> recipientDataField;

		[XmlElement("recipientData", Order = 0)]
		public List<CT_RecipientData> recipientData
		{
			get
			{
				return recipientDataField;
			}
			set
			{
				recipientDataField = value;
			}
		}

		public CT_Recipients()
		{
			recipientDataField = new List<CT_RecipientData>();
		}
	}
}
