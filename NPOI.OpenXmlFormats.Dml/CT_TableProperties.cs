using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TableProperties
	{
		private CT_NoFillProperties noFillField;

		private CT_SolidColorFillProperties solidFillField;

		private CT_GradientFillProperties gradFillField;

		private CT_BlipFillProperties blipFillField;

		private CT_PatternFillProperties pattFillField;

		private CT_GroupFillProperties grpFillField;

		private CT_EffectList effectLstField;

		private CT_EffectContainer effectDagField;

		private object itemField;

		private CT_OfficeArtExtensionList extLstField;

		private bool rtlField;

		private bool firstRowField;

		private bool firstColField;

		private bool lastRowField;

		private bool lastColField;

		private bool bandRowField;

		private bool bandColField;

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

		[XmlElement("tableStyleId", typeof(string), DataType = "token", Order = 8)]
		[XmlElement("tableStyle", typeof(CT_TableStyle), Order = 8)]
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

		[XmlElement(Order = 9)]
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
		[DefaultValue(false)]
		public bool rtl
		{
			get
			{
				return rtlField;
			}
			set
			{
				rtlField = value;
			}
		}

		[DefaultValue(false)]
		[XmlAttribute]
		public bool firstRow
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

		[XmlAttribute]
		[DefaultValue(false)]
		public bool firstCol
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

		[DefaultValue(false)]
		[XmlAttribute]
		public bool lastRow
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

		[DefaultValue(false)]
		[XmlAttribute]
		public bool lastCol
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

		[DefaultValue(false)]
		[XmlAttribute]
		public bool bandRow
		{
			get
			{
				return bandRowField;
			}
			set
			{
				bandRowField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(false)]
		public bool bandCol
		{
			get
			{
				return bandColField;
			}
			set
			{
				bandColField = value;
			}
		}
	}
}
