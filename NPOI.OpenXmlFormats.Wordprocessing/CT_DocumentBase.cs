using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlInclude(typeof(CT_GlossaryDocument))]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlInclude(typeof(CT_Document))]
	public class CT_DocumentBase
	{
		private CT_Background backgroundField;

		[XmlElement(Order = 0)]
		public CT_Background background
		{
			get
			{
				return backgroundField;
			}
			set
			{
				backgroundField = value;
			}
		}
	}
}
