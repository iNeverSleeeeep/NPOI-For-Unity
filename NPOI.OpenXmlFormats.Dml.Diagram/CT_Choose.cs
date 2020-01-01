using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_Choose
	{
		private List<CT_When> ifField;

		private CT_Otherwise elseField;

		private string nameField;

		[XmlElement("if", Order = 0)]
		public List<CT_When> @if
		{
			get
			{
				return ifField;
			}
			set
			{
				ifField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Otherwise @else
		{
			get
			{
				return elseField;
			}
			set
			{
				elseField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue("")]
		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public CT_Choose()
		{
			elseField = new CT_Otherwise();
			ifField = new List<CT_When>();
			nameField = "";
		}
	}
}
