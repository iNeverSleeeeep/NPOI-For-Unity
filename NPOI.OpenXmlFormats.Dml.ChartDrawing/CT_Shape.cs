using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	public class CT_Shape
	{
		private CT_ShapeNonVisual nvSpPrField;

		private CT_ShapeProperties spPrField;

		private CT_ShapeStyle styleField;

		private CT_TextBody txBodyField;

		private string macroField;

		private string textlinkField;

		private bool fLocksTextField;

		private bool fPublishedField;

		[XmlElement(Order = 0)]
		public CT_ShapeNonVisual nvSpPr
		{
			get
			{
				return nvSpPrField;
			}
			set
			{
				nvSpPrField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
		public CT_TextBody txBody
		{
			get
			{
				return txBodyField;
			}
			set
			{
				txBodyField = value;
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

		[XmlAttribute]
		public string textlink
		{
			get
			{
				return textlinkField;
			}
			set
			{
				textlinkField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(true)]
		public bool fLocksText
		{
			get
			{
				return fLocksTextField;
			}
			set
			{
				fLocksTextField = value;
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

		public CT_Shape()
		{
			txBodyField = new CT_TextBody();
			styleField = new CT_ShapeStyle();
			spPrField = new CT_ShapeProperties();
			nvSpPrField = new CT_ShapeNonVisual();
			fLocksTextField = true;
			fPublishedField = false;
		}
	}
}
