using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DesignerCategory("code")]
	public class CT_BulletEnabled
	{
		private bool valField;

		[DefaultValue(false)]
		[XmlAttribute]
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

		public CT_BulletEnabled()
		{
			valField = false;
		}
	}
}
