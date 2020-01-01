using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_LuminanceEffect
	{
		private int brightField;

		private int contrastField;

		[XmlAttribute]
		[DefaultValue(0)]
		public int bright
		{
			get
			{
				return brightField;
			}
			set
			{
				brightField = value;
			}
		}

		[DefaultValue(0)]
		[XmlAttribute]
		public int contrast
		{
			get
			{
				return contrastField;
			}
			set
			{
				contrastField = value;
			}
		}

		public CT_LuminanceEffect()
		{
			brightField = 0;
			contrastField = 0;
		}
	}
}
