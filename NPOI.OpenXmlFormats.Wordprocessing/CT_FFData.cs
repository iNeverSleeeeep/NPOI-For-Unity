using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_FFData
	{
		private object[] itemsField;

		private ItemsChoiceType14[] itemsElementNameField;

		[XmlElement("textInput", typeof(CT_FFTextInput), Order = 0)]
		[XmlElement("exitMacro", typeof(CT_MacroName), Order = 0)]
		[XmlElement("helpText", typeof(CT_FFHelpText), Order = 0)]
		[XmlElement("name", typeof(CT_FFName), Order = 0)]
		[XmlElement("statusText", typeof(CT_FFStatusText), Order = 0)]
		[XmlElement("checkBox", typeof(CT_FFCheckBox), Order = 0)]
		[XmlElement("entryMacro", typeof(CT_MacroName), Order = 0)]
		[XmlElement("calcOnExit", typeof(CT_OnOff), Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("ddList", typeof(CT_FFDDList), Order = 0)]
		[XmlElement("enabled", typeof(CT_OnOff), Order = 0)]
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

		[XmlIgnore]
		[XmlElement("ItemsElementName", Order = 1)]
		public ItemsChoiceType14[] ItemsElementName
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

		public CT_FFData()
		{
			itemsElementNameField = new ItemsChoiceType14[0];
			itemsField = new object[0];
		}
	}
}
