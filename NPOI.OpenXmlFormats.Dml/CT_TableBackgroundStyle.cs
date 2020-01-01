using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_TableBackgroundStyle
	{
		private CT_FillProperties fillField;

		private CT_StyleMatrixReference fillRefField;

		private CT_EffectProperties effectField;

		private CT_StyleMatrixReference effectRefField;

		[XmlElement(Order = 0)]
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

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
		public CT_EffectProperties effect
		{
			get
			{
				return effectField;
			}
			set
			{
				effectField = value;
			}
		}

		[XmlElement(Order = 3)]
		public CT_StyleMatrixReference effectRef
		{
			get
			{
				return effectRefField;
			}
			set
			{
				effectRefField = value;
			}
		}

		public CT_TableBackgroundStyle()
		{
			effectRefField = new CT_StyleMatrixReference();
			effectField = new CT_EffectProperties();
			fillRefField = new CT_StyleMatrixReference();
			fillField = new CT_FillProperties();
		}
	}
}
