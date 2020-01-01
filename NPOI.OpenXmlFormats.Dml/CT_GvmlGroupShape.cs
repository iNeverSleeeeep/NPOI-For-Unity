using System;
using System.Collections.Generic;
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
	public class CT_GvmlGroupShape
	{
		private CT_GvmlGroupShapeNonVisual nvGrpSpPrField;

		private CT_GroupShapeProperties grpSpPrField;

		private List<object> itemsField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_GvmlGroupShapeNonVisual nvGrpSpPr
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

		[XmlElement("graphicFrame", typeof(CT_GvmlGraphicalObjectFrame), Order = 2)]
		[XmlElement("grpSp", typeof(CT_GvmlGroupShape), Order = 2)]
		[XmlElement("pic", typeof(CT_GvmlPicture), Order = 2)]
		[XmlElement("sp", typeof(CT_GvmlShape), Order = 2)]
		[XmlElement("cxnSp", typeof(CT_GvmlConnector), Order = 2)]
		[XmlElement("txSp", typeof(CT_GvmlTextShape), Order = 2)]
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

		[XmlElement(Order = 3)]
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
	}
}
