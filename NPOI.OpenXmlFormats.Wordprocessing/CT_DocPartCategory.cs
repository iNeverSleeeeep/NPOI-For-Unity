using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_DocPartCategory
	{
		private CT_String nameField;

		private CT_DocPartGallery galleryField;

		[XmlElement(Order = 0)]
		public CT_String name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_DocPartGallery gallery
		{
			get
			{
				return galleryField;
			}
			set
			{
				galleryField = value;
			}
		}

		public CT_DocPartCategory()
		{
			galleryField = new CT_DocPartGallery();
			nameField = new CT_String();
		}
	}
}
