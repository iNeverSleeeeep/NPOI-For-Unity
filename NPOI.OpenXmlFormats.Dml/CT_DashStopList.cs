using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_DashStopList
	{
		private List<CT_DashStop> dsField;

		[XmlElement("ds", Order = 0)]
		public List<CT_DashStop> ds
		{
			get
			{
				return dsField;
			}
			set
			{
				dsField = value;
			}
		}
	}
}
