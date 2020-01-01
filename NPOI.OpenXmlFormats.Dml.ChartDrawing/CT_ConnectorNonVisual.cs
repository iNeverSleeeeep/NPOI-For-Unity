using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	public class CT_ConnectorNonVisual
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

		public CT_ConnectorNonVisual()
		{
			cNvCxnSpPrField = new CT_NonVisualConnectorProperties();
		}
	}
}
