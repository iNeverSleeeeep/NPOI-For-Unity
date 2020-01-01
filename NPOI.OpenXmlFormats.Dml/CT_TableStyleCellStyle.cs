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
	public class CT_TableStyleCellStyle
	{
		private CT_TableCellBorderStyle tcBdrField;

		private CT_FillProperties fillField;

		private CT_StyleMatrixReference fillRefField;

		private CT_Cell3D cell3DField;

		[XmlElement(Order = 0)]
		public CT_TableCellBorderStyle tcBdr
		{
			get
			{
				return tcBdrField;
			}
			set
			{
				tcBdrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_FillProperties fill
		{
			get
			{
				return fillField;
			}
			set
			{
				fillField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_StyleMatrixReference fillRef
		{
			get
			{
				return fillRefField;
			}
			set
			{
				fillRefField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_Cell3D cell3D
		{
			get
			{
				return cell3DField;
			}
			set
			{
				cell3DField = value;
			}
		}

		public CT_TableStyleCellStyle()
		{
			cell3DField = new CT_Cell3D();
			fillRefField = new CT_StyleMatrixReference();
			fillField = new CT_FillProperties();
			tcBdrField = new CT_TableCellBorderStyle();
		}
	}
}
