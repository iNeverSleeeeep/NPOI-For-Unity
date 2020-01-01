using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_ShapeNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualDrawingShapeProps cNvSpPrField;

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
		public CT_NonVisualDrawingShapeProps cNvSpPr
		{
			get
			{
				return cNvSpPrField;
			}
			set
			{
				cNvSpPrField = value;
			}
		}

		public CT_ShapeNonVisual()
		{
			cNvSpPrField = new CT_NonVisualDrawingShapeProps();
			cNvPrField = new CT_NonVisualDrawingProps();
		}
	}
}
