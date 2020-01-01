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
	public class CT_GraphicFrameNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualGraphicFrameProperties cNvGraphicFramePrField;

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
		public CT_NonVisualGraphicFrameProperties cNvGraphicFramePr
		{
			get
			{
				return cNvGraphicFramePrField;
			}
			set
			{
				cNvGraphicFramePrField = value;
			}
		}

		public CT_GraphicFrameNonVisual()
		{
			cNvGraphicFramePrField = new CT_NonVisualGraphicFrameProperties();
		}
	}
}
