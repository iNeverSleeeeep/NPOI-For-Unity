using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_GvmlGraphicFrameNonVisual
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
	}
}
