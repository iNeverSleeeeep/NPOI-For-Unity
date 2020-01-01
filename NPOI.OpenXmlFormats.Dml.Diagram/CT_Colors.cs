using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[GeneratedCode("System.Xml", "4.0.30319.17379")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_Colors
	{
		private List<object> itemsField;

		private ST_ClrAppMethod methField;

		private ST_HueDir hueDirField;

		[XmlElement("prstClr", typeof(CT_PresetColor), Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 0)]
		[XmlElement("scrgbClr", typeof(CT_ScRgbColor), Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 0)]
		[XmlElement("sysClr", typeof(CT_SystemColor), Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 0)]
		[XmlElement("srgbClr", typeof(CT_SRgbColor), Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 0)]
		[XmlElement("hslClr", typeof(CT_HslColor), Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 0)]
		[XmlElement("schemeClr", typeof(CT_SchemeColor), Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", Order = 0)]
		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		[DefaultValue(ST_ClrAppMethod.span)]
		[XmlAttribute]
		public ST_ClrAppMethod meth
		{
			get
			{
				return methField;
			}
			set
			{
				methField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_HueDir.cw)]
		public ST_HueDir hueDir
		{
			get
			{
				return hueDirField;
			}
			set
			{
				hueDirField = value;
			}
		}

		public CT_Colors()
		{
			itemsField = new List<object>();
			methField = ST_ClrAppMethod.span;
			hueDirField = ST_HueDir.cw;
		}
	}
}
