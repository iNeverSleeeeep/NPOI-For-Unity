using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_GvmlPictureNonVisual
	{
		private CT_NonVisualDrawingProps cNvPrField;

		private CT_NonVisualPictureProperties cNvPicPrField;

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
		public CT_NonVisualPictureProperties cNvPicPr
		{
			get
			{
				return cNvPicPrField;
			}
			set
			{
				cNvPicPrField = value;
			}
		}
	}
}
