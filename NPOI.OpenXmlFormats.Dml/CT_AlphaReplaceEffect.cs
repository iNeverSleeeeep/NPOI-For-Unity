using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Dml
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/drawingml/2006/main", IsNullable = true)]
	public class CT_AlphaReplaceEffect
	{
		private int aField;

		[XmlAttribute]
		public int a
		{
			get
			{
				return aField;
			}
			set
			{
				aField = value;
			}
		}
	}
}
