using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_DocPart
	{
		private CT_DocPartPr docPartPrField;

		private CT_Body docPartBodyField;

		[XmlElement(Order = 0)]
		public CT_DocPartPr docPartPr
		{
			get
			{
				return docPartPrField;
			}
			set
			{
				docPartPrField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_Body docPartBody
		{
			get
			{
				return docPartBodyField;
			}
			set
			{
				docPartBodyField = value;
			}
		}
	}
}
