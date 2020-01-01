using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TableCellProperties
	{
		private CT_LineProperties lnLField;

		private CT_LineProperties lnRField;

		private CT_LineProperties lnTField;

		private CT_LineProperties lnBField;

		private CT_LineProperties lnTlToBrField;

		private CT_LineProperties lnBlToTrField;

		private CT_Cell3D cell3DField;

		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_BlipFillProperties blipFillField;

		private CT_PatternFillProperties pattFillField;

		private CT_GroupFillProperties grpFillField;

		private CT_OfficeArtExtensionList extLstField;

		private int marLField;

		private int marRField;

		private int marTField;

		private int marBField;

		private ST_TextVerticalType vertField;

		private ST_TextAnchoringType anchorField;

		private bool anchorCtrField;

		private ST_TextHorzOverflowType horzOverflowField;

		[XmlElement(Order = 0)]
		public CT_LineProperties lnL
		{
			get
			{
				return lnLField;
			}
			set
			{
				lnLField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_LineProperties lnR
		{
			get
			{
				return lnRField;
			}
			set
			{
				lnRField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_LineProperties lnT
		{
			get
			{
				return lnTField;
			}
			set
			{
				lnTField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_LineProperties lnB
		{
			get
			{
				return lnBField;
			}
			set
			{
				lnBField = value;
			}
		}

		[XmlElement(Order = 4)]
		public CT_LineProperties lnTlToBr
		{
			get
			{
				return lnTlToBrField;
			}
			set
			{
				lnTlToBrField = value;
			}
		}

		[XmlElement(Order = 5)]
		public CT_LineProperties lnBlToTr
		{
			get
			{
				return lnBlToTrField;
			}
			set
			{
				lnBlToTrField = value;
			}
		}

		[XmlElement(Order = 6)]
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

		[XmlElement(Order = 7)]
		public CT_NoFillProperties noFill
		{
			get
			{
				return noFillField;
			}
			set
			{
				noFillField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_SolidColorFillProperties solidFill
		{
			get
			{
				return solidFillField;
			}
			set
			{
				solidFillField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_GradientFillProperties gradFill
		{
			get
			{
				return gradFillField;
			}
			set
			{
				gradFillField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_BlipFillProperties blipFill
		{
			get
			{
				return blipFillField;
			}
			set
			{
				blipFillField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_PatternFillProperties pattFill
		{
			get
			{
				return pattFillField;
			}
			set
			{
				pattFillField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_GroupFillProperties grpFill
		{
			get
			{
				return grpFillField;
			}
			set
			{
				grpFillField = value;
			}
		}

		[XmlElement(Order = 13)]
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

		[XmlAttribute]
		[DefaultValue(91440)]
		public int marL
		{
			get
			{
				return marLField;
			}
			set
			{
				marLField = value;
			}
		}

		[DefaultValue(91440)]
		[XmlAttribute]
		public int marR
		{
			get
			{
				return marRField;
			}
			set
			{
				marRField = value;
			}
		}

		[DefaultValue(45720)]
		[XmlAttribute]
		public int marT
		{
			get
			{
				return marTField;
			}
			set
			{
				marTField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(45720)]
		public int marB
		{
			get
			{
				return marBField;
			}
			set
			{
				marBField = value;
			}
		}

		[DefaultValue(ST_TextVerticalType.horz)]
		[XmlAttribute]
		public ST_TextVerticalType vert
		{
			get
			{
				return vertField;
			}
			set
			{
				vertField = value;
			}
		}

		[DefaultValue(ST_TextAnchoringType.t)]
		[XmlAttribute]
		public ST_TextAnchoringType anchor
		{
			get
			{
				return anchorField;
			}
			set
			{
				anchorField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool anchorCtr
		{
			get
			{
				return anchorCtrField;
			}
			set
			{
				anchorCtrField = value;
			}
		}

		[DefaultValue(ST_TextHorzOverflowType.clip)]
		[XmlAttribute]
		public ST_TextHorzOverflowType horzOverflow
		{
			get
			{
				return horzOverflowField;
			}
			set
			{
				horzOverflowField = value;
			}
		}

		public CT_TableCellProperties()
		{
			marLField = 91440;
			marRField = 91440;
			marTField = 45720;
			marBField = 45720;
			vertField = ST_TextVerticalType.horz;
			anchorField = ST_TextAnchoringType.t;
			anchorCtrField = false;
			horzOverflowField = ST_TextHorzOverflowType.clip;
		}
	}
}
