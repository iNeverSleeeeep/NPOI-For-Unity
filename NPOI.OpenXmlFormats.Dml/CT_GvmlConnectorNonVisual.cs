using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_GvmlConnectorNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualConnectorProperties cNvCxnSpPrField;

		[XmlElement(Order = 0)]
		public CT_NonVisualDrawingProps cNvPr
		{
			get
			{
				return cNvPrField;
			}
			set
			{
				cNvPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_NonVisualConnectorProperties cNvCxnSpPr
		{
			get
			{
				return cNvCxnSpPrField;
			}
			set
			{
				cNvCxnSpPrField = value;
			}
		}
	}
}
