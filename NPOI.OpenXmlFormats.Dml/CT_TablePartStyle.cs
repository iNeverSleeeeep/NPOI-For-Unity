using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DebuggerStepThrough]
	public class CT_TablePartStyle
	{
		private CT_TableStyleTextStyle tcTxStyleField;

		private CT_TableStyleCellStyle tcStyleField;

		[XmlElement(Order = 0)]
		public CT_TableStyleTextStyle tcTxStyle
		{
			get
			{
				return tcTxStyleField;
			}
			set
			{
				tcTxStyleField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TableStyleCellStyle tcStyle
		{
			get
			{
				return tcStyleField;
			}
			set
			{
				tcStyleField = value;
			}
		}

		public CT_TablePartStyle()
		{
			tcStyleField = new CT_TableStyleCellStyle();
			tcTxStyleField = new CT_TableStyleTextStyle();
		}
	}
}
