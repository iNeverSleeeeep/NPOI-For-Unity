using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_GvmlGraphicalObjectFrame
	{
		private CT_GvmlGraphicFrameNonVisual nvGraphicFramePrField;

		private CT_GraphicalObject graphicField;

		private CT_Transform2D xfrmField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_GvmlGraphicFrameNonVisual nvGraphicFramePr
		{
			get
			{
				return nvGraphicFramePrField;
			}
			set
			{
				nvGraphicFramePrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_GraphicalObject graphic
		{
			get
			{
				return graphicField;
			}
			set
			{
				graphicField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_Transform2D xfrm
		{
			get
			{
				return xfrmField;
			}
			set
			{
				xfrmField = value;
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
