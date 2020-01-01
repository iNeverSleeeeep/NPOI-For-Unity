using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_HSLEffect
	{
		private int hueField;

		private int satField;

		private int lumField;

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

		[XmlAttribute]
		[DefaultValue(0)]
		public int sat
		{
			get
			{
				return satField;
			}
			set
			{
				satField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int lum
		{
			get
			{
				return lumField;
			}
			set
			{
				lumField = value;
			}
		}

		public CT_HSLEffect()
		{
			hueField = 0;
			satField = 0;
			lumField = 0;
		}
	}
}
