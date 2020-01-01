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
	public class CT_Marker
	{
		private double xField;

		private double yField;

		[XmlElement(Order = 0)]
		public double x
		{
			get
			{
				return xField;
			}
			set
			{
				xField = value;
			}
		}

		[XmlElement(Order = 1)]
		public double y
		{
			get
			{
				return yField;
			}
			set
			{
				yField = value;
			}
		}
	}
}
