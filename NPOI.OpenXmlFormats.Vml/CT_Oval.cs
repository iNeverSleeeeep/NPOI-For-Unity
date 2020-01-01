using NPOI.OpenXmlFormats.Vml.Presentation;
using NPOI.OpenXmlFormats.Vml.Spreadsheet;
using NPOI.OpenXmlFormats.Vml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Vml
{
	[Serializable]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	public class CT_Oval
	{
		private List<object> itemsField = new List<object>();

		private List<ItemsChoiceType2> itemsElementNameField = new List<ItemsChoiceType2>();

		[XmlElement("handles", typeof(CT_Handles))]
		[XmlElement("borderleft", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("borderright", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("bordertop", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("wrap", typeof(CT_Wrap), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("fill", typeof(CT_Fill))]
		[XmlElement("formulas", typeof(CT_Formulas))]
		[XmlElement("textpath", typeof(CT_TextPath))]
		[XmlElement("borderbottom", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("path", typeof(CT_Path))]
		[XmlElement("shadow", typeof(CT_Shadow))]
		[XmlElement("stroke", typeof(CT_Stroke))]
		[XmlElement("textbox", typeof(CT_Textbox))]
		[XmlElement("imagedata", typeof(CT_ImageData))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("ClientData", typeof(CT_ClientData), Namespace = "urn:schemas-microsoft-com:office:excel")]
		[XmlElement("textdata", typeof(CT_Rel), Namespace = "urn:schemas-microsoft-com:office:powerpoint")]
		[XmlElement("anchorlock", typeof(CT_AnchorLock), Namespace = "urn:schemas-microsoft-com:office:word")]
		public object[] Items
		{
			get
			{
				return itemsField.ToArray();
			}
			set
			{
				if (value == null)
				{
					itemsField = new List<object>();
				}
				else
				{
					itemsField = new List<object>(value);
				}
			}
		}

		[XmlIgnore]
		[XmlElement("ItemsElementName")]
		public ItemsChoiceType2[] ItemsElementName
		{
			get
			{
				return itemsElementNameField.ToArray();
			}
			set
			{
				if (value == null)
				{
					itemsElementNameField = new List<ItemsChoiceType2>();
				}
				else
				{
					itemsElementNameField = new List<ItemsChoiceType2>(value);
				}
			}
		}
	}
}
