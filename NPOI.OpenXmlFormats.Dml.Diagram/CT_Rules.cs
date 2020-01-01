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
	public class CT_Rules
	{
		private List<CT_NumericRule> ruleField;

		[XmlElement("rule", Order = 0)]
		public List<CT_NumericRule> rule
		{
			get
			{
				return ruleField;
			}
			set
			{
				ruleField = value;
			}
		}

		public CT_Rules()
		{
			ruleField = new List<CT_NumericRule>();
		}
	}
}
