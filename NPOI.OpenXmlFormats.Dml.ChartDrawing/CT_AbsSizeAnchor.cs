using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	[DebuggerStepThrough]
	public class CT_AbsSizeAnchor
	{
		private CT_Marker fromField;

		private CT_PositiveSize2D extField;

		private object itemField;

		[XmlElement(Order = 0)]
		public CT_Marker from
		{
			get
			{
				return fromField;
			}
			set
			{
				fromField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_PositiveSize2D ext
		{
			get
			{
				return extField;
			}
			set
			{
				extField = value;
			}
		}

		[XmlElement("pic", typeof(CT_Picture), Order = 2)]
		[XmlElement("sp", typeof(CT_Shape), Order = 2)]
		[XmlElement("grpSp", typeof(CT_GroupShape), Order = 2)]
		[XmlElement("cxnSp", typeof(CT_Connector), Order = 2)]
		[XmlElement("graphicFrame", typeof(CT_GraphicFrame), Order = 2)]
		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}

		public CT_AbsSizeAnchor()
		{
			fromField = new CT_Marker();
		}
	}
}
