using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_DocParts
	{
		private List<CT_DocPart> itemsField;

		[XmlElement("docPart", Order = 0)]
		public List<CT_DocPart> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		public CT_DocParts()
		{
			itemsField = new List<CT_DocPart>();
		}
	}
}
