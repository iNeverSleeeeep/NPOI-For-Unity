using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_ColorMRU
	{
		private List<object> itemsField;

		[XmlElement("scrgbClr", typeof(CT_ScRgbColor), Order = 0)]
		[XmlElement("prstClr", typeof(CT_PresetColor), Order = 0)]
		[XmlElement("schemeClr", typeof(CT_SchemeColor), Order = 0)]
		[XmlElement("hslClr", typeof(CT_HslColor), Order = 0)]
		[XmlElement("srgbClr", typeof(CT_SRgbColor), Order = 0)]
		[XmlElement("sysClr", typeof(CT_SystemColor), Order = 0)]
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

		public CT_ColorMRU()
		{
			itemsField = new List<object>();
		}
	}
}
