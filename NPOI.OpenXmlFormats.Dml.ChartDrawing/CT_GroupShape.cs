using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml.ChartDrawing
{
	[Serializable]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing", IsNullable = true)]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/chartDrawing")]
	public class CT_GroupShape
	{
		private CT_GroupShapeNonVisual nvGrpSpPrField;

		private CT_GroupShapeProperties grpSpPrField;

		private List<object> itemsField;

		[XmlElement(Order = 0)]
		public CT_GroupShapeNonVisual nvGrpSpPr
		{
			get
			{
				return nvGrpSpPrField;
			}
			set
			{
				nvGrpSpPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_GroupShapeProperties grpSpPr
		{
			get
			{
				return grpSpPrField;
			}
			set
			{
				grpSpPrField = value;
			}
		}

		[XmlElement("pic", typeof(CT_Picture), Order = 2)]
		[XmlElement("grpSp", typeof(CT_GroupShape), Order = 2)]
		[XmlElement("cxnSp", typeof(CT_Connector), Order = 2)]
		[XmlElement("sp", typeof(CT_Shape), Order = 2)]
		[XmlElement("graphicFrame", typeof(CT_GraphicFrame), Order = 2)]
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

		public CT_GroupShape()
		{
			itemsField = new List<object>();
			grpSpPrField = new CT_GroupShapeProperties();
			nvGrpSpPrField = new CT_GroupShapeNonVisual();
		}
	}
}
