using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Path2DLineTo
	{
		private CT_AdjPoint2D ptField;

		[XmlElement(Order = 0)]
		public CT_AdjPoint2D pt
		{
			get
			{
				return ptField;
			}
			set
			{
				ptField = value;
			}
		}

		public CT_Path2DLineTo()
		{
			ptField = new CT_AdjPoint2D();
		}
	}
}
