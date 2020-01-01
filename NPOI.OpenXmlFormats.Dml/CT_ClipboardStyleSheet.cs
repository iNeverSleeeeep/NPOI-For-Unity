using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_ClipboardStyleSheet
	{
		private CT_BaseStyles themeElementsField;

		private CT_ColorMapping clrMapField;

		[XmlElement(Order = 0)]
		public CT_BaseStyles themeElements
		{
			get
			{
				return themeElementsField;
			}
			set
			{
				themeElementsField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ColorMapping clrMap
		{
			get
			{
				return clrMapField;
			}
			set
			{
				clrMapField = value;
			}
		}

		public CT_ClipboardStyleSheet()
		{
			clrMapField = new CT_ColorMapping();
			themeElementsField = new CT_BaseStyles();
		}
	}
}
