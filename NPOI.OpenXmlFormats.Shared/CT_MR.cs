using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Shared
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/math", IsNullable = true)]
	public class CT_MR
	{
		private List<CT_OMathArg> eField;

		[XmlElement("e", Order = 0)]
		public List<CT_OMathArg> e
		{
			get
			{
				return eField;
			}
			set
			{
				eField = value;
			}
		}

		public CT_MR()
		{
			eField = new List<CT_OMathArg>();
		}
	}
}
