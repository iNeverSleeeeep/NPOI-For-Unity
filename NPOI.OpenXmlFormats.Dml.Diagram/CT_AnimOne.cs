using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_AnimOne
	{
		private ST_AnimOneStr valField;

		[DefaultValue(ST_AnimOneStr.one)]
		[XmlAttribute]
		public ST_AnimOneStr val
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

		public CT_AnimOne()
		{
			valField = ST_AnimOneStr.one;
		}
	}
}
