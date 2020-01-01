using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot("colorsDefHdrLst", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = false)]
	[GeneratedCode("System.Xml", "4.0.30319.17379")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_ColorTransformHeaderLst
	{
		private List<CT_ColorTransformHeader> colorsDefHdrField;

		[XmlElement("colorsDefHdr", Order = 0)]
		public List<CT_ColorTransformHeader> colorsDefHdr
		{
			get
			{
				return colorsDefHdrField;
			}
			set
			{
				colorsDefHdrField = value;
			}
		}

		public CT_ColorTransformHeaderLst()
		{
			colorsDefHdrField = new List<CT_ColorTransformHeader>();
		}
	}
}
