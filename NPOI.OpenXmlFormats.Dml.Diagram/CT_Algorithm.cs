using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_Algorithm
	{
		private List<CT_Parameter> paramField;

		private CT_OfficeArtExtensionList extLstField;

		private ST_AlgorithmType typeField;

		private uint revField;

		[XmlElement("param", Order = 0)]
		public List<CT_Parameter> param
		{
			get
			{
				return paramField;
			}
			set
			{
				paramField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_OfficeArtExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
			}
		}

		[XmlAttribute]
		public ST_AlgorithmType type
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
		[DefaultValue(typeof(uint), "0")]
		public uint rev
		{
			get
			{
				return revField;
			}
			set
			{
				revField = value;
			}
		}

		public CT_Algorithm()
		{
			paramField = new List<CT_Parameter>();
			revField = 0u;
		}
	}
}
