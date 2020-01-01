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
	public class CT_GvmlTextShape
	{
		private CT_TextBody txBodyField;

		private object itemField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_TextBody txBody
		{
			get
			{
				return txBodyField;
			}
			set
			{
				txBodyField = value;
			}
		}

		[XmlElement("xfrm", typeof(CT_Transform2D), Order = 1)]
		[XmlElement("useSpRect", typeof(CT_GvmlUseShapeRectangle), Order = 1)]
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

		[XmlElement(Order = 2)]
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
