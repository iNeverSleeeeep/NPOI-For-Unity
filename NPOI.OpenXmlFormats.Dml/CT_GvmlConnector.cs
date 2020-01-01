using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_GvmlConnector
	{
		private CT_GvmlConnectorNonVisual nvCxnSpPrField;

		private CT_ShapeProperties spPrField;

		private CT_ShapeStyle styleField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_GvmlConnectorNonVisual nvCxnSpPr
		{
			get
			{
				return nvCxnSpPrField;
			}
			set
			{
				nvCxnSpPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ShapeProperties spPr
		{
			get
			{
				return spPrField;
			}
			set
			{
				spPrField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_ShapeStyle style
		{
			get
			{
				return styleField;
			}
			set
			{
				styleField = value;
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
