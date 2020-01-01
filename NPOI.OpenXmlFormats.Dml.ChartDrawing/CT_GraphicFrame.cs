using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	[DebuggerStepThrough]
	public class CT_GraphicFrame
	{
		private CT_GraphicFrameNonVisual nvGraphicFramePrField;

		private CT_Transform2D xfrmField;

		private CT_GraphicalObject graphicField;

		private string macroField;

		private bool fPublishedField;

		[XmlElement(Order = 0)]
		public CT_GraphicFrameNonVisual nvGraphicFramePr
		{
			get
			{
				return nvGraphicFramePrField;
			}
			set
			{
				nvGraphicFramePrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Transform2D xfrm
		{
			get
			{
				return xfrmField;
			}
			set
			{
				xfrmField = value;
			}
		}

		[XmlElement(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 2)]
		public CT_GraphicalObject graphic
		{
			get
			{
				return graphicField;
			}
			set
			{
				graphicField = value;
			}
		}

		[XmlAttribute]
		public string macro
		{
			get
			{
				return macroField;
			}
			set
			{
				macroField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool fPublished
		{
			get
			{
				return fPublishedField;
			}
			set
			{
				fPublishedField = value;
			}
		}

		public CT_GraphicFrame()
		{
			graphicField = new CT_GraphicalObject();
			nvGraphicFramePrField = new CT_GraphicFrameNonVisual();
			fPublishedField = false;
		}
	}
}
