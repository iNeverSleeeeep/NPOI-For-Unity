using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_EffectStyleList
	{
		private List<CT_EffectStyleItem> effectStyleField;

		[XmlElement("effectStyle", Order = 0)]
		public List<CT_EffectStyleItem> effectStyle
		{
			get
			{
				return effectStyleField;
			}
			set
			{
				effectStyleField = value;
			}
		}

		public CT_EffectStyleList()
		{
			effectStyleField = new List<CT_EffectStyleItem>();
		}
	}
}
