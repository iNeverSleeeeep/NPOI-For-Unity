using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[GeneratedCode("System.Xml", "4.0.30319.17379")]
	[DesignerCategory("code")]
	public class CT_CTCategory
	{
		private string typeField;

		private uint priField;

		[XmlAttribute(DataType = "anyURI")]
		public string type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlAttribute]
		public uint pri
		{
			get
			{
				return priField;
			}
			set
			{
				priField = value;
			}
		}
	}
}
