using System;
using System.Collections.Generic;
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
	public class CT_TableGrid
	{
		private List<CT_TableCol> gridColField;

		[XmlElement("gridCol", Order = 0)]
		public List<CT_TableCol> gridCol
		{
			get
			{
				return gridColField;
			}
			set
			{
				gridColField = value;
			}
		}

		public CT_TableGrid()
		{
			gridColField = new List<CT_TableCol>();
		}
	}
}
