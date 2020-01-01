using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_TintEffect
	{
		private int hueField;

		private int amtField;

		[XmlAttribute]
		[DefaultValue(0)]
		public int hue
		{
			get
			{
				return hueField;
			}
			set
			{
				hueField = value;
			}
		}

		[DefaultValue(0)]
		[XmlAttribute]
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

		public CT_TintEffect()
		{
			hueField = 0;
			amtField = 0;
		}
	}
}
