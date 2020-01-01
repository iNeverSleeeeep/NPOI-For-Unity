using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/bibliography")]
	[DebuggerStepThrough]
	public class CT_AuthorType
	{
		private List<object> itemsField;

		private List<ItemsChoiceType> itemsElementNameField;

		[XmlElement("Counsel", typeof(CT_NameType))]
		[XmlElement("Inventor", typeof(CT_NameType))]
		[XmlElement("Artist", typeof(CT_NameType))]
		[XmlElement("Author", typeof(CT_NameOrCorporateType))]
		[XmlElement("BookAuthor", typeof(CT_NameType))]
		[XmlElement("Compiler", typeof(CT_NameType))]
		[XmlElement("Composer", typeof(CT_NameType))]
		[XmlElement("Conductor", typeof(CT_NameType))]
		[XmlElement("Director", typeof(CT_NameType))]
		[XmlElement("Editor", typeof(CT_NameType))]
		[XmlElement("Interviewee", typeof(CT_NameType))]
		[XmlElement("Interviewer", typeof(CT_NameType))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("Performer", typeof(CT_NameOrCorporateType))]
		[XmlElement("ProducerName", typeof(CT_NameType))]
		[XmlElement("Translator", typeof(CT_NameType))]
		[XmlElement("Writer", typeof(CT_NameType))]
		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		[XmlIgnore]
		public List<ItemsChoiceType> ItemsElementName
		{
			get
			{
				return itemsElementNameField;
			}
			set
			{
				itemsElementNameField = value;
			}
		}

		public CT_AuthorType()
		{
			itemsElementNameField = new List<ItemsChoiceType>();
			itemsField = new List<object>();
		}
	}
}
