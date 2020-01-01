using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_MCS
	{
		private List<CT_MC> mcField;

		[XmlElement("mc", Order = 0)]
		public List<CT_MC> mc
		{
			get
			{
				return mcField;
			}
			set
			{
				mcField = value;
			}
		}

		public CT_MCS()
		{
			mcField = new List<CT_MC>();
		}
	}
}
