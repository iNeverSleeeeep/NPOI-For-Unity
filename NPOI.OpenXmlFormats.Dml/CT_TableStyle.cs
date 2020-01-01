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
	public class CT_TableStyle
	{
		private CT_TableBackgroundStyle tblBgField;

		private CT_TablePartStyle wholeTblField;

		private CT_TablePartStyle band1HField;

		private CT_TablePartStyle band2HField;

		private CT_TablePartStyle band1VField;

		private CT_TablePartStyle band2VField;

		private CT_TablePartStyle lastColField;

		private CT_TablePartStyle firstColField;

		private CT_TablePartStyle lastRowField;

		private CT_TablePartStyle seCellField;

		private CT_TablePartStyle swCellField;

		private CT_TablePartStyle firstRowField;

		private CT_TablePartStyle neCellField;

		private CT_TablePartStyle nwCellField;

		private CT_OfficeArtExtensionList extLstField;

		private string styleIdField;

		private string styleNameField;

		[XmlElement(Order = 0)]
		public CT_TableBackgroundStyle tblBg
		{
			get
			{
				return tblBgField;
			}
			set
			{
				tblBgField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TablePartStyle wholeTbl
		{
			get
			{
				return wholeTblField;
			}
			set
			{
				wholeTblField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_TablePartStyle band1H
		{
			get
			{
				return band1HField;
			}
			set
			{
				band1HField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_TablePartStyle band2H
		{
			get
			{
				return band2HField;
			}
			set
			{
				band2HField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_TablePartStyle band1V
		{
			get
			{
				return band1VField;
			}
			set
			{
				band1VField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_TablePartStyle band2V
		{
			get
			{
				return band2VField;
			}
			set
			{
				band2VField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_TablePartStyle lastCol
		{
			get
			{
				return lastColField;
			}
			set
			{
				lastColField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_TablePartStyle firstCol
		{
			get
			{
				return firstColField;
			}
			set
			{
				firstColField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_TablePartStyle lastRow
		{
			get
			{
				return lastRowField;
			}
			set
			{
				lastRowField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_TablePartStyle seCell
		{
			get
			{
				return seCellField;
			}
			set
			{
				seCellField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_TablePartStyle swCell
		{
			get
			{
				return swCellField;
			}
			set
			{
				swCellField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_TablePartStyle firstRow
		{
			get
			{
				return firstRowField;
			}
			set
			{
				firstRowField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_TablePartStyle neCell
		{
			get
			{
				return neCellField;
			}
			set
			{
				neCellField = value;
			}
		}

		[XmlElement(Order = 13)]
		public CT_TablePartStyle nwCell
		{
			get
			{
				return nwCellField;
			}
			set
			{
				nwCellField = value;
			}
		}

		[XmlElement(Order = 14)]
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

		[XmlAttribute(DataType = "token")]
		public string styleId
		{
			get
			{
				return styleIdField;
			}
			set
			{
				styleIdField = value;
			}
		}

		[XmlAttribute]
		public string styleName
		{
			get
			{
				return styleNameField;
			}
			set
			{
				styleNameField = value;
			}
		}

		public CT_TableStyle()
		{
			extLstField = new CT_OfficeArtExtensionList();
			nwCellField = new CT_TablePartStyle();
			neCellField = new CT_TablePartStyle();
			firstRowField = new CT_TablePartStyle();
			swCellField = new CT_TablePartStyle();
			seCellField = new CT_TablePartStyle();
			lastRowField = new CT_TablePartStyle();
			firstColField = new CT_TablePartStyle();
			lastColField = new CT_TablePartStyle();
			band2VField = new CT_TablePartStyle();
			band1VField = new CT_TablePartStyle();
			band2HField = new CT_TablePartStyle();
			band1HField = new CT_TablePartStyle();
			wholeTblField = new CT_TablePartStyle();
			tblBgField = new CT_TableBackgroundStyle();
		}
	}
}
