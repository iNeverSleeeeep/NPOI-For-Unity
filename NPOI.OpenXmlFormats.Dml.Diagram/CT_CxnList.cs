using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_CxnList
	{
		private List<CT_Cxn> cxnField;

		[XmlElement("cxn", Order = 0)]
		public List<CT_Cxn> cxn
		{
			get
			{
				return cxnField;
			}
			set
			{
				cxnField = value;
			}
		}

		public CT_CxnList()
		{
			cxnField = new List<CT_Cxn>();
		}
	}
}
