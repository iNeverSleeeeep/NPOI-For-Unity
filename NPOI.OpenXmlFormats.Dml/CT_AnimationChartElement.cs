using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_AnimationChartElement
	{
		private int seriesIdxField;

		private int categoryIdxField;

		private ST_ChartBuildStep bldStepField;

		[DefaultValue(-1)]
		[XmlAttribute]
		public int seriesIdx
		{
			get
			{
				return seriesIdxField;
			}
			set
			{
				seriesIdxField = value;
			}
		}

		[DefaultValue(-1)]
		[XmlAttribute]
		public int categoryIdx
		{
			get
			{
				return categoryIdxField;
			}
			set
			{
				categoryIdxField = value;
			}
		}

		[XmlAttribute]
		public ST_ChartBuildStep bldStep
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

		public CT_AnimationChartElement()
		{
			seriesIdxField = -1;
			categoryIdxField = -1;
		}
	}
}
