using System;
using System.Collections.Generic;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_DocPartTypes
	{
		private List<CT_DocPartType> itemsField;

		private ST_OnOff allField;

		private bool allFieldSpecified;

		[XmlElement("type", Order = 0)]
		public List<CT_DocPartType> Items
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

		[XmlAttribute(Form = XmlSchemaForm.Qualified)]
		public ST_OnOff all
		{
			get
			{
				return allField;
			}
			set
			{
				allField = value;
			}
		}

		[XmlIgnore]
		public bool allSpecified
		{
			get
			{
				return allFieldSpecified;
			}
			set
			{
				allFieldSpecified = value;
			}
		}

		public CT_DocPartTypes()
		{
			itemsField = new List<CT_DocPartType>();
		}
	}
}
