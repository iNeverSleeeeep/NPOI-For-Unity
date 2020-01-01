using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_PtList
	{
		private List<CT_Pt> ptField;

		[XmlElement("pt", Order = 0)]
		public List<CT_Pt> pt
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

		public CT_PtList()
		{
			ptField = new List<CT_Pt>();
		}
	}
}
