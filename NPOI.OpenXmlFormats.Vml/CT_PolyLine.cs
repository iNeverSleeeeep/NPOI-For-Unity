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
	[DesignerCategory("code")]
	[XmlType(Namespace = "urn:schemas-microsoft-com:vml")]
	[XmlRoot(Namespace = "urn:schemas-microsoft-com:vml", IsNullable = true)]
	public class CT_PolyLine
	{
		private List<object> itemsField = new List<object>();

		private List<ItemsChoiceType3> itemsElementNameField = new List<ItemsChoiceType3>();

		private string pointsField;

		[XmlElement("formulas", typeof(CT_Formulas))]
		[XmlElement("textdata", typeof(CT_Rel), Namespace = "urn:schemas-microsoft-com:office:powerpoint")]
		[XmlElement("borderright", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("bordertop", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("wrap", typeof(CT_Wrap), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("fill", typeof(CT_Fill))]
		[XmlElement("handles", typeof(CT_Handles))]
		[XmlElement("imagedata", typeof(CT_ImageData))]
		[XmlElement("path", typeof(CT_Path))]
		[XmlElement("shadow", typeof(CT_Shadow))]
		[XmlElement("stroke", typeof(CT_Stroke))]
		[XmlElement("textbox", typeof(CT_Textbox))]
		[XmlElement("textpath", typeof(CT_TextPath))]
		[XmlChoiceIdentifier("ItemsElementName")]
		[XmlElement("anchorlock", typeof(CT_AnchorLock), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("borderbottom", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("borderleft", typeof(CT_Border), Namespace = "urn:schemas-microsoft-com:office:word")]
		[XmlElement("ClientData", typeof(CT_ClientData), Namespace = "urn:schemas-microsoft-com:office:excel")]
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
		public ItemsChoiceType3[] ItemsElementName
		{
			get
			{
				return itemsElementNameField.ToArray();
			}
			set
			{
				if (value == null)
				{
					itemsElementNameField = new List<ItemsChoiceType3>();
				}
				else
				{
					itemsElementNameField = new List<ItemsChoiceType3>(value);
				}
			}
		}

		[XmlAttribute]
		public string points
		{
			get
			{
				return pointsField;
			}
			set
			{
				pointsField = value;
			}
		}
	}
}
