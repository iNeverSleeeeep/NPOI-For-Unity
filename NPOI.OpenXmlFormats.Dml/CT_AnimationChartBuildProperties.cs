using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_AnimationChartBuildProperties
	{
		private string bldField;

		private bool animBgField;

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

		[DefaultValue(true)]
		[XmlAttribute]
		public bool animBg
		{
			get
			{
				return animBgField;
			}
			set
			{
				animBgField = value;
			}
		}

		public CT_AnimationChartBuildProperties()
		{
			bldField = "allAtOnce";
			animBgField = true;
		}
	}
}
