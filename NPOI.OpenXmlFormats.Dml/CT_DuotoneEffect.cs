using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_DuotoneEffect
	{
		private List<object> itemsField;

		[XmlElement("sysClr", typeof(CT_SystemColor), Order = 0)]
		[XmlElement("schemeClr", typeof(CT_SchemeColor), Order = 0)]
		[XmlElement("hslClr", typeof(CT_HslColor), Order = 0)]
		[XmlElement("prstClr", typeof(CT_PresetColor), Order = 0)]
		[XmlElement("scrgbClr", typeof(CT_ScRgbColor), Order = 0)]
		[XmlElement("srgbClr", typeof(CT_SRgbColor), Order = 0)]
		public List<object> Items
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

		public CT_DuotoneEffect()
		{
			itemsField = new List<object>();
		}
	}
}
