using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	public class CT_Picture
	{
		private CT_PictureNonVisual nvPicPrField;

		private CT_BlipFillProperties blipFillField;

		private CT_ShapeProperties spPrField;

		private CT_ShapeStyle styleField;

		private string macroField;

		private bool fPublishedField;

		[XmlElement(Order = 0)]
		public CT_PictureNonVisual nvPicPr
		{
			get
			{
				return nvPicPrField;
			}
			set
			{
				nvPicPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_BlipFillProperties blipFill
		{
			get
			{
				return blipFillField;
			}
			set
			{
				blipFillField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_ShapeStyle style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
			}
		}

		[DefaultValue("")]
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

		public CT_Picture()
		{
			blipFillField = new CT_BlipFillProperties();
			nvPicPrField = new CT_PictureNonVisual();
			macroField = "";
			fPublishedField = false;
		}
	}
}
