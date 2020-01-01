using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_SmartTagPr
	{
		private List<CT_Attr> attrField;

		[XmlElement("attr", Order = 0)]
		public List<CT_Attr> attr
		{
			get
			{
				return attrField;
			}
			set
			{
				attrField = value;
			}
		}

		public CT_SmartTagPr()
		{
			attrField = new List<CT_Attr>();
		}
	}
}
