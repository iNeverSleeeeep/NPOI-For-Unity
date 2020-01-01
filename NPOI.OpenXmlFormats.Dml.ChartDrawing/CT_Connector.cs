using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_Connector
	{
		private CT_ConnectorNonVisual nvCxnSpPrField;

		private CT_ShapeProperties spPrField;

		private CT_ShapeStyle styleField;

		private string macroField;

		private bool fPublishedField;

		[XmlElement(Order = 0)]
		public CT_ConnectorNonVisual nvCxnSpPr
		{
			get
			{
				return nvCxnSpPrField;
			}
			set
			{
				nvCxnSpPrField = value;
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

		public CT_Connector()
		{
			nvCxnSpPrField = new CT_ConnectorNonVisual();
			fPublishedField = false;
		}
	}
}
