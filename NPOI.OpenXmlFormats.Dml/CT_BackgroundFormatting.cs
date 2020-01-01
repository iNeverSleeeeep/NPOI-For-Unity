using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	public class CT_BackgroundFormatting
	{
		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_BlipFillProperties blipFillField;

		private CT_PatternFillProperties pattFillField;

		private CT_GroupFillProperties grpFillField;

		private CT_EffectList effectLstField;

		private CT_EffectContainer effectDagField;

		[XmlElement(Order = 0)]
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

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
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

		[XmlElement(Order = 3)]
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

		[XmlElement(Order = 4)]
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

		[XmlElement(Order = 5)]
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

		[XmlElement(Order = 6)]
		public CT_EffectList effectLst
		{
			get
			{
				return effectLstField;
			}
			set
			{
				effectLstField = value;
			}
		}

		[XmlElement(Order = 7)]
		public CT_EffectContainer effectDag
		{
			get
			{
				return effectDagField;
			}
			set
			{
				effectDagField = value;
			}
		}
	}
}
