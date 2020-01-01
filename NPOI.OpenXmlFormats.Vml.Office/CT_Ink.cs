using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml.Office
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:office:office", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:office:office")]
	public class CT_Ink
	{
		private byte[] iField;

		private ST_TrueFalse annotationField;

		private bool annotationFieldSpecified;

		[XmlAttribute(DataType = "base64Binary")]
		public byte[] i
		{
			get
			{
				return iField;
			}
			set
			{
				iField = value;
			}
		}

		[XmlAttribute]
		public ST_TrueFalse annotation
		{
			get
			{
				return annotationField;
			}
			set
			{
				annotationField = value;
			}
		}

		[XmlIgnore]
		public bool annotationSpecified
		{
			get
			{
				return annotationFieldSpecified;
			}
			set
			{
				annotationFieldSpecified = value;
			}
		}
	}
}
