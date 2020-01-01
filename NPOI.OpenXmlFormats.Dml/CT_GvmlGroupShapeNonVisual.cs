using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_GvmlGroupShapeNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualGroupDrawingShapeProps cNvGrpSpPrField;

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
		public CT_NonVisualGroupDrawingShapeProps cNvGrpSpPr
		{
			get
			{
				return cNvGrpSpPrField;
			}
			set
			{
				cNvGrpSpPrField = value;
			}
		}
	}
}
