using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	public class CT_TableCell
	{
		private CT_TextBody txBodyField;

		private CT_TableCellProperties tcPrField;

		private CT_OfficeArtExtensionList extLstField;

		private int rowSpanField;

		private int gridSpanField;

		private bool hMergeField;

		private bool vMergeField;

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

		[XmlElement(Order = 1)]
		public CT_TableCellProperties tcPr
		{
			get
			{
				return tcPrField;
			}
			set
			{
				tcPrField = value;
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

		[DefaultValue(1)]
		[XmlAttribute]
		public int rowSpan
		{
			get
			{
				return rowSpanField;
			}
			set
			{
				rowSpanField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(1)]
		public int gridSpan
		{
			get
			{
				return gridSpanField;
			}
			set
			{
				gridSpanField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool hMerge
		{
			get
			{
				return hMergeField;
			}
			set
			{
				hMergeField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool vMerge
		{
			get
			{
				return vMergeField;
			}
			set
			{
				vMergeField = value;
			}
		}

		public CT_TableCell()
		{
			rowSpanField = 1;
			gridSpanField = 1;
			hMergeField = false;
			vMergeField = false;
		}
	}
}
