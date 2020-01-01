using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_Path2DMoveTo
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

		public CT_Path2DMoveTo()
		{
			ptField = new CT_AdjPoint2D();
		}
	}
}
