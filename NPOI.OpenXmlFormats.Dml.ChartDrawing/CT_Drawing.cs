using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	[DesignerCategory("code")]
	public class CT_Drawing
	{
		private List<object> itemsField;

		[XmlElement("absSizeAnchor", typeof(CT_AbsSizeAnchor), Order = 0)]
		[XmlElement("relSizeAnchor", typeof(CT_RelSizeAnchor), Order = 0)]
		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		public CT_Drawing()
		{
			itemsField = new List<object>();
		}
	}
}
