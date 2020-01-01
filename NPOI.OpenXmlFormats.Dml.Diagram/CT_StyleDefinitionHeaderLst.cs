using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot("styleDefHdrLst", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_StyleDefinitionHeaderLst
	{
		private List<CT_StyleDefinitionHeader> styleDefHdrField;

		[XmlElement("styleDefHdr", Order = 0)]
		public List<CT_StyleDefinitionHeader> styleDefHdr
		{
			get
			{
				return styleDefHdrField;
			}
			set
			{
				styleDefHdrField = value;
			}
		}

		public CT_StyleDefinitionHeaderLst()
		{
			styleDefHdrField = new List<CT_StyleDefinitionHeader>();
		}
	}
}
