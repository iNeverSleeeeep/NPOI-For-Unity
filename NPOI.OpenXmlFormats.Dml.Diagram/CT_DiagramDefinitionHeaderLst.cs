using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot("layoutDefHdrLst", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DebuggerStepThrough]
	public class CT_DiagramDefinitionHeaderLst
	{
		private List<CT_DiagramDefinitionHeader> layoutDefHdrField;

		[XmlElement("layoutDefHdr", Order = 0)]
		public List<CT_DiagramDefinitionHeader> layoutDefHdr
		{
			get
			{
				return layoutDefHdrField;
			}
			set
			{
				layoutDefHdrField = value;
			}
		}

		public CT_DiagramDefinitionHeaderLst()
		{
			layoutDefHdrField = new List<CT_DiagramDefinitionHeader>();
		}
	}
}
