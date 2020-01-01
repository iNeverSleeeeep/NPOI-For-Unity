using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_AutoCaptions
	{
		private List<CT_AutoCaption> autoCaptionField;

		[XmlElement("autoCaption", Order = 0)]
		public List<CT_AutoCaption> autoCaption
		{
			get
			{
				return autoCaptionField;
			}
			set
			{
				autoCaptionField = value;
			}
		}

		public CT_AutoCaptions()
		{
			autoCaptionField = new List<CT_AutoCaption>();
		}
	}
}
