using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class CT_ColorSchemeList
	{
		private List<CT_ColorSchemeAndMapping> extraClrSchemeField;

		[XmlElement("extraClrScheme", Order = 0)]
		public List<CT_ColorSchemeAndMapping> extraClrScheme
		{
			get
			{
				return extraClrSchemeField;
			}
			set
			{
				extraClrSchemeField = value;
			}
		}

		public CT_ColorSchemeList()
		{
			extraClrSchemeField = new List<CT_ColorSchemeAndMapping>();
		}
	}
}
