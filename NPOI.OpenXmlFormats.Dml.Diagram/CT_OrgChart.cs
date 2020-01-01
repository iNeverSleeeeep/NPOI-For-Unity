using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_OrgChart
	{
		private bool valField;

		[XmlAttribute]
		[DefaultValue(false)]
		public bool val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public CT_OrgChart()
		{
			valField = false;
		}
	}
}
