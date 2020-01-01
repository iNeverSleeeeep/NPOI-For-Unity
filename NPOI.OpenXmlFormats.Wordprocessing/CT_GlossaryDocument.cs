using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot("glossaryDocument", Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = false)]
	public class CT_GlossaryDocument : CT_DocumentBase
	{
		private CT_DocParts docPartsField;

		[XmlElement(Order = 0)]
		public CT_DocParts docParts
		{
			get
			{
				return docPartsField;
			}
			set
			{
				docPartsField = value;
			}
		}

		public CT_GlossaryDocument()
		{
			docPartsField = new CT_DocParts();
		}
	}
}
