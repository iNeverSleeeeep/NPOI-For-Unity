using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_AlphaModulateEffect
	{
		private CT_EffectContainer contField;

		[XmlElement(Order = 0)]
		public CT_EffectContainer cont
		{
			get
			{
				return contField;
			}
			set
			{
				contField = value;
			}
		}

		public CT_AlphaModulateEffect()
		{
			contField = new CT_EffectContainer();
		}
	}
}
