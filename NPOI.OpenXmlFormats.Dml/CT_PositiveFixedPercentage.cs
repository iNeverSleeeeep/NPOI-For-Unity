using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_PositiveFixedPercentage
	{
		private int valField;

		[XmlAttribute]
		public int val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}
	}
}
