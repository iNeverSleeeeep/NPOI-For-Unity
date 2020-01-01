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
	public class CT_ThemeableLineStyle
	{
		private object itemField;

		[XmlElement("lnRef", typeof(CT_StyleMatrixReference), Order = 0)]
		[XmlElement("ln", typeof(CT_LineProperties), Order = 0)]
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
	}
}
