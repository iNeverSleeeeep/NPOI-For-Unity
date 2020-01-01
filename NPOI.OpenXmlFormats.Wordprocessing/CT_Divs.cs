using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Divs
	{
		private List<CT_Div> divField;

		[XmlElement("div", Order = 0)]
		public List<CT_Div> div
		{
			get
			{
				return divField;
			}
			set
			{
				divField = value;
			}
		}

		public CT_Divs()
		{
			divField = new List<CT_Div>();
		}
	}
}
