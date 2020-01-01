using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_Tabs
	{
		private List<CT_TabStop> tabField;

		[XmlElement("tab", Order = 0)]
		public List<CT_TabStop> tab
		{
			get
			{
				return tabField;
			}
			set
			{
				tabField = value;
			}
		}

		public CT_Tabs()
		{
			tabField = new List<CT_TabStop>();
		}

		public CT_TabStop AddNewTab()
		{
			CT_TabStop cT_TabStop = new CT_TabStop();
			tabField.Add(cT_TabStop);
			return cT_TabStop;
		}
	}
}
