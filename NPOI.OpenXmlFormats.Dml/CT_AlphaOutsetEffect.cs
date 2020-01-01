using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_AlphaOutsetEffect
	{
		private long radField;

		[XmlAttribute]
		[DefaultValue(typeof(long), "0")]
		public long rad
		{
			get
			{
				return radField;
			}
			set
			{
				radField = value;
			}
		}

		public CT_AlphaOutsetEffect()
		{
			radField = 0L;
		}
	}
}
