using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/officeDocument/2006/characteristics")]
	public class CT_AdditionalCharacteristics
	{
		private List<CT_Characteristic> characteristicField;

		public List<CT_Characteristic> characteristic
		{
			get
			{
				return characteristicField;
			}
			set
			{
				characteristicField = value;
			}
		}

		public CT_AdditionalCharacteristics()
		{
			characteristicField = new List<CT_Characteristic>();
		}
	}
}
