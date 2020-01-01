using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_WholeE2oFormatting
	{
		private CT_LineProperties lnField;

		private CT_EffectList effectLstField;

		private CT_EffectContainer effectDagField;

		[XmlElement(Order = 0)]
		public CT_LineProperties ln
		{
			get
			{
				return lnField;
			}
			set
			{
				lnField = value;
			}
		}

		[XmlElement(Order = 1)]
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

		[XmlElement(Order = 2)]
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

		public CT_WholeE2oFormatting()
		{
			effectDagField = new CT_EffectContainer();
			effectLstField = new CT_EffectList();
			lnField = new CT_LineProperties();
		}
	}
}
