using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	public class CT_Scale2D
	{
		private CT_Ratio sxField;

		private CT_Ratio syField;

		[XmlElement(Order = 0)]
		public CT_Ratio sx
		{
			get
			{
				return sxField;
			}
			set
			{
				sxField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Ratio sy
		{
			get
			{
				return syField;
			}
			set
			{
				syField = value;
			}
		}

		public CT_Scale2D()
		{
			syField = new CT_Ratio();
			sxField = new CT_Ratio();
		}
	}
}
