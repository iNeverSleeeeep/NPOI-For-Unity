using System;
using System.Collections.Generic;
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
	public class CT_SDCategories
	{
		private List<CT_SDCategory> catField;

		[XmlElement("cat", Order = 0)]
		public List<CT_SDCategory> cat
		{
			get
			{
				return catField;
			}
			set
			{
				catField = value;
			}
		}

		public CT_SDCategories()
		{
			catField = new List<CT_SDCategory>();
		}
	}
}
