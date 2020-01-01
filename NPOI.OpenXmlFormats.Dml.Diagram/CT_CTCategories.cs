using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[GeneratedCode("System.Xml", "4.0.30319.17379")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	public class CT_CTCategories
	{
		private List<CT_CTCategory> catField;

		[XmlElement("cat", Order = 0)]
		public List<CT_CTCategory> cat
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

		public CT_CTCategories()
		{
			catField = new List<CT_CTCategory>();
		}
	}
}
