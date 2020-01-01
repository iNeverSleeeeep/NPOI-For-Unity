using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_BlendEffect
	{
		private CT_EffectContainer contField;

		private ST_BlendMode blendField;

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

		[XmlAttribute]
		public ST_BlendMode blend
		{
			get
			{
				return blendField;
			}
			set
			{
				blendField = value;
			}
		}

		public CT_BlendEffect()
		{
			contField = new CT_EffectContainer();
		}
	}
}
