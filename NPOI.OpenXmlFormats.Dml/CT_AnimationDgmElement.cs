using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_AnimationDgmElement
	{
		private string idField;

		private ST_DgmBuildStep bldStepField;

		[DefaultValue("{00000000-0000-0000-0000-000000000000}")]
		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/relationships")]
		public string id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(ST_DgmBuildStep.sp)]
		public ST_DgmBuildStep bldStep
		{
			get
			{
				return bldStepField;
			}
			set
			{
				bldStepField = value;
			}
		}

		public CT_AnimationDgmElement()
		{
			idField = "{00000000-0000-0000-0000-000000000000}";
			bldStepField = ST_DgmBuildStep.sp;
		}
	}
}
