using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot("themeOverride", Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = false)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_BaseStylesOverride
	{
		private CT_ColorScheme clrSchemeField;

		private CT_FontScheme fontSchemeField;

		private CT_StyleMatrix fmtSchemeField;

		[XmlElement(Order = 0)]
		public CT_ColorScheme clrScheme
		{
			get
			{
				return clrSchemeField;
			}
			set
			{
				clrSchemeField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_FontScheme fontScheme
		{
			get
			{
				return fontSchemeField;
			}
			set
			{
				fontSchemeField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_StyleMatrix fmtScheme
		{
			get
			{
				return fmtSchemeField;
			}
			set
			{
				fmtSchemeField = value;
			}
		}

		public CT_BaseStylesOverride()
		{
			fmtSchemeField = new CT_StyleMatrix();
			fontSchemeField = new CT_FontScheme();
			clrSchemeField = new CT_ColorScheme();
		}
	}
}
