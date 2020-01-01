using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_ChildPref
	{
		private int valField;

		[DefaultValue(-1)]
		[XmlAttribute]
		public int val
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

		public CT_ChildPref()
		{
			valField = -1;
		}
	}
}
