using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_AnimationDgmBuildProperties
	{
		private string bldField;

		private bool revField;

		[XmlAttribute]
		[DefaultValue("allAtOnce")]
		public string bld
		{
			get
			{
				return bldField;
			}
			set
			{
				bldField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool rev
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

		public CT_AnimationDgmBuildProperties()
		{
			bldField = "allAtOnce";
			revField = false;
		}
	}
}
