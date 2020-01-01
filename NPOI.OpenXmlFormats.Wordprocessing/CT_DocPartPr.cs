using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_DocPartPr
	{
		private object[] itemsField;

		private ItemsChoiceType11[] itemsElementNameField;

		[XmlElement("guid", typeof(CT_Guid), Order = 0)]
		[XmlElement("name", typeof(CT_DocPartName), Order = 0)]
		[XmlElement("style", typeof(CT_String), Order = 0)]
		[XmlElement("behaviors", typeof(CT_DocPartBehaviors), Order = 0)]
		[XmlElement("types", typeof(CT_DocPartTypes), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("description", typeof(CT_String), Order = 0)]
		[XmlElement("category", typeof(CT_DocPartCategory), Order = 0)]
		public object[] Items
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

		[XmlElement("ItemsElementName", Order = 1)]
		[XmlIgnore]
		public ItemsChoiceType11[] ItemsElementName
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

		public CT_DocPartPr()
		{
			itemsElementNameField = new ItemsChoiceType11[0];
			itemsField = new object[0];
		}
	}
}
