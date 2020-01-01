using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_AnimLvl
	{
		private ST_AnimLvlStr valField;

		[DefaultValue(ST_AnimLvlStr.none)]
		[XmlAttribute]
		public ST_AnimLvlStr val
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

		public CT_AnimLvl()
		{
			valField = ST_AnimLvlStr.none;
		}
	}
}
