using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_RelativeOffsetEffect
	{
		private int txField;

		private int tyField;

		[XmlAttribute]
		[DefaultValue(0)]
		public int tx
		{
			get
			{
				return txField;
			}
			set
			{
				txField = value;
			}
		}

		[XmlAttribute]
		[DefaultValue(0)]
		public int ty
		{
			get
			{
				return tyField;
			}
			set
			{
				tyField = value;
			}
		}

		public CT_RelativeOffsetEffect()
		{
			txField = 0;
			tyField = 0;
		}
	}
}
