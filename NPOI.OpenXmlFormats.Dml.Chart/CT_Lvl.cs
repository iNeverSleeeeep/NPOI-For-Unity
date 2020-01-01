using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Chart
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chart", IsNullable = true)]
	public class CT_Lvl
	{
		private List<CT_StrVal> ptField;

		[XmlElement("pt", Order = 0)]
		public List<CT_StrVal> pt
		{
			get
			{
				return ptField;
			}
			set
			{
				ptField = value;
			}
		}
	}
}
