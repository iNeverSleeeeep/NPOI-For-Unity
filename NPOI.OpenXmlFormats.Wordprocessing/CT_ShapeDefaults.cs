using System;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_ShapeDefaults
	{
		private XmlElement[] itemsField;

		[XmlAnyElement(Namespace = "urn:schemas-microsoft-com:office:office", Order = 0)]
		public XmlElement[] Items
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

		public CT_ShapeDefaults()
		{
			itemsField = new XmlElement[0];
		}
	}
}
