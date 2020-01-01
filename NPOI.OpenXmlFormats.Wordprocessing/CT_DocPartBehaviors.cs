using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_DocPartBehaviors
	{
		private List<CT_DocPartBehavior> itemsField;

		[XmlElement("behavior", Order = 0)]
		public List<CT_DocPartBehavior> Items
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

		public CT_DocPartBehaviors()
		{
			itemsField = new List<CT_DocPartBehavior>();
		}
	}
}
