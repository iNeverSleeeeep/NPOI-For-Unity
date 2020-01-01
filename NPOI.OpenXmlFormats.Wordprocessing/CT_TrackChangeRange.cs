using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_TrackChangeRange : CT_TrackChange
	{
		private ST_DisplacedByCustomXml displacedByCustomXmlField;

		private bool displacedByCustomXmlFieldSpecified;

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_DisplacedByCustomXml displacedByCustomXml
		{
			get
			{
				return displacedByCustomXmlField;
			}
			set
			{
				displacedByCustomXmlField = value;
			}
		}

		[XmlIgnore]
		public bool displacedByCustomXmlSpecified
		{
			get
			{
				return displacedByCustomXmlFieldSpecified;
			}
			set
			{
				displacedByCustomXmlFieldSpecified = value;
			}
		}
	}
}
