using System;
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
	public class CT_TableCellBorderStyle
	{
		private CT_ThemeableLineStyle leftField;

		private CT_ThemeableLineStyle rightField;

		private CT_ThemeableLineStyle topField;

		private CT_ThemeableLineStyle bottomField;

		private CT_ThemeableLineStyle insideHField;

		private CT_ThemeableLineStyle insideVField;

		private CT_ThemeableLineStyle tl2brField;

		private CT_ThemeableLineStyle tr2blField;

		private CT_OfficeArtExtensionList extLstField;

		[XmlElement(Order = 0)]
		public CT_ThemeableLineStyle left
		{
			get
			{
				return leftField;
			}
			set
			{
				leftField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_ThemeableLineStyle right
		{
			get
			{
				return rightField;
			}
			set
			{
				rightField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_ThemeableLineStyle top
		{
			get
			{
				return topField;
			}
			set
			{
				topField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_ThemeableLineStyle bottom
		{
			get
			{
				return bottomField;
			}
			set
			{
				bottomField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_ThemeableLineStyle insideH
		{
			get
			{
				return insideHField;
			}
			set
			{
				insideHField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_ThemeableLineStyle insideV
		{
			get
			{
				return insideVField;
			}
			set
			{
				insideVField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_ThemeableLineStyle tl2br
		{
			get
			{
				return tl2brField;
			}
			set
			{
				tl2brField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_ThemeableLineStyle tr2bl
		{
			get
			{
				return tr2blField;
			}
			set
			{
				tr2blField = value;
			}
		}

		[XmlElement(Order = 8)]
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

		public CT_TableCellBorderStyle()
		{
			extLstField = new CT_OfficeArtExtensionList();
			tr2blField = new CT_ThemeableLineStyle();
			tl2brField = new CT_ThemeableLineStyle();
			insideVField = new CT_ThemeableLineStyle();
			insideHField = new CT_ThemeableLineStyle();
			bottomField = new CT_ThemeableLineStyle();
			topField = new CT_ThemeableLineStyle();
			rightField = new CT_ThemeableLineStyle();
			leftField = new CT_ThemeableLineStyle();
		}
	}
}
