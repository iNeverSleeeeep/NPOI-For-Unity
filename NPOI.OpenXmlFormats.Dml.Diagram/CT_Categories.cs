using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_Categories
	{
		private List<CT_Category> catField;

		[XmlElement("cat", Order = 0)]
		public List<CT_Category> cat
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

		public CT_Categories()
		{
			catField = new List<CT_Category>();
		}
	}
}
