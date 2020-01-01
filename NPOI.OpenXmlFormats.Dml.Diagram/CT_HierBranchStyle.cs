using System;
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
	public class CT_HierBranchStyle
	{
		private ST_HierBranchStyle valField;

		[XmlAttribute]
		[DefaultValue(ST_HierBranchStyle.std)]
		public ST_HierBranchStyle val
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

		public CT_HierBranchStyle()
		{
			valField = ST_HierBranchStyle.std;
		}
	}
}
