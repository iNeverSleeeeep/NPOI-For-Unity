using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.Diagram
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/diagram")]
	public class CT_SampleData
	{
		private CT_DataModel dataModelField;

		private bool useDefField;

		[XmlElement(Order = 0)]
		public CT_DataModel dataModel
		{
			get
			{
				return dataModelField;
			}
			set
			{
				dataModelField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool useDef
		{
			get
			{
				return useDefField;
			}
			set
			{
				useDefField = value;
			}
		}

		public CT_SampleData()
		{
			dataModelField = new CT_DataModel();
			useDefField = false;
		}
	}
}
