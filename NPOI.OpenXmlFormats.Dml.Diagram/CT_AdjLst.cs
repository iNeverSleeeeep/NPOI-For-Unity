using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	public class CT_AdjLst
	{
		private List<CT_Adj> adjField;

		[XmlElement("adj", Order = 0)]
		public List<CT_Adj> adj
		{
			get
			{
				return adjField;
			}
			set
			{
				adjField = value;
			}
		}

		public CT_AdjLst()
		{
			adjField = new List<CT_Adj>();
		}
	}
}
