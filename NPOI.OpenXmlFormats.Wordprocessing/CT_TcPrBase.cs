using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlInclude(typeof(CT_TcPrInner))]
	[XmlInclude(typeof(CT_TcPr))]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TcPrBase
	{
		private CT_Cnf cnfStyleField;

		private CT_TblWidth tcWField;

		private CT_DecimalNumber gridSpanField;

		private CT_HMerge hMergeField;

		private CT_VMerge vMergeField;

		private CT_TcBorders tcBordersField;

		private CT_Shd shdField;

		private CT_OnOff noWrapField;

		private CT_TcMar tcMarField;

		private CT_TextDirection textDirectionField;

		private CT_OnOff tcFitTextField;

		private CT_VerticalJc vAlignField;

		private CT_OnOff hideMarkField;

		[XmlElement(Order = 0)]
		public CT_Cnf cnfStyle
		{
			get
			{
				return cnfStyleField;
			}
			set
			{
				cnfStyleField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_TblWidth tcW
		{
			get
			{
				return tcWField;
			}
			set
			{
				tcWField = value;
			}
		}

		[XmlElement(Order = 2)]
		public CT_DecimalNumber gridSpan
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

		[XmlElement(Order = 3)]
		public CT_HMerge hMerge
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

		[XmlElement(Order = 4)]
		public CT_VMerge vMerge
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

		[XmlElement(Order = 5)]
		public CT_TcBorders tcBorders
		{
			get
			{
				return tcBordersField;
			}
			set
			{
				tcBordersField = value;
			}
		}

		[XmlElement(Order = 6)]
		public CT_Shd shd
		{
			get
			{
				return shdField;
			}
			set
			{
				shdField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_OnOff noWrap
		{
			get
			{
				return noWrapField;
			}
			set
			{
				noWrapField = value;
			}
		}

		[XmlElement(Order = 8)]
		public CT_TcMar tcMar
		{
			get
			{
				return tcMarField;
			}
			set
			{
				tcMarField = value;
			}
		}

		[XmlElement(Order = 9)]
		public CT_TextDirection textDirection
		{
			get
			{
				return textDirectionField;
			}
			set
			{
				textDirectionField = value;
			}
		}

		[XmlElement(Order = 10)]
		public CT_OnOff tcFitText
		{
			get
			{
				return tcFitTextField;
			}
			set
			{
				tcFitTextField = value;
			}
		}

		[XmlElement(Order = 11)]
		public CT_VerticalJc vAlign
		{
			get
			{
				return vAlignField;
			}
			set
			{
				vAlignField = value;
			}
		}

		[XmlElement(Order = 12)]
		public CT_OnOff hideMark
		{
			get
			{
				return hideMarkField;
			}
			set
			{
				hideMarkField = value;
			}
		}

		public CT_Shd AddNewShd()
		{
			shdField = new CT_Shd();
			return shdField;
		}

		public bool IsSetShd()
		{
			return shdField != null;
		}

		public CT_VerticalJc AddNewVAlign()
		{
			vAlign = new CT_VerticalJc();
			return vAlign;
		}

		public CT_VMerge AddNewVMerge()
		{
			vMerge = new CT_VMerge();
			return vMerge;
		}

		public CT_TcBorders AddNewTcBorders()
		{
			tcBorders = new CT_TcBorders();
			return tcBorders;
		}

		public CT_HMerge AddNewHMerge()
		{
			hMerge = new CT_HMerge();
			return hMerge;
		}

		public CT_DecimalNumber AddNewGridspan()
		{
			gridSpanField = new CT_DecimalNumber();
			return gridSpanField;
		}
	}
}
