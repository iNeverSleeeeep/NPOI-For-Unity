using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot("fonts", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	public class CT_FontsList
	{
		private List<CT_Font> fontField;

		[XmlElement("font", Order = 0)]
		public List<CT_Font> font
		{
			get
			{
				return fontField;
			}
			set
			{
				fontField = value;
			}
		}

		public CT_FontsList()
		{
			fontField = new List<CT_Font>();
		}
	}
}
