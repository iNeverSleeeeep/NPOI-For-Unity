using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_AlphaModulateFixedEffect
	{
		private int amtField;

		[XmlAttribute]
		[DefaultValue(100000)]
		public int amt
		{
			get
			{
				return amtField;
			}
			set
			{
				amtField = value;
			}
		}

		public CT_AlphaModulateFixedEffect()
		{
			amtField = 100000;
		}
	}
}
