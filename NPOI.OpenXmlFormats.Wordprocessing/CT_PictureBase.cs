using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlInclude(typeof(CT_Object))]
	[XmlInclude(typeof(CT_Picture))]
	[XmlInclude(typeof(CT_Background))]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_PictureBase
	{
		private List<XmlNode> itemsField;

		private List<ItemsChoiceType9> itemsElementNameField;

		[XmlAnyElement(Namespace = "urn:schemas-microsoft-com:vml", Order = 0)]
		[XmlAnyElement(Namespace = "urn:schemas-microsoft-com:office:office", Order = 0)]
		[XmlChoiceIdentifier("ItemsElementName")]
		public List<XmlNode> Items
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
		public List<ItemsChoiceType9> ItemsElementName
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

		public CT_PictureBase()
		{
			itemsElementNameField = new List<ItemsChoiceType9>();
			itemsField = new List<XmlNode>();
		}

		public void Set(object obj)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
			StringBuilder stringBuilder = new StringBuilder();
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Encoding = Encoding.UTF8;
			xmlWriterSettings.OmitXmlDeclaration = true;
			XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings);
			xmlSerializer.Serialize(xmlWriter, obj);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(stringBuilder.ToString());
			lock (this)
			{
				itemsField.Add(xmlDocument.DocumentElement.CloneNode(true));
				itemsElementNameField.Add(ItemsChoiceType9.vml);
			}
		}
	}
}
