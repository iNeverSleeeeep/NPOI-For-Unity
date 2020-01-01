using System;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	public class CT_RecipientData
	{
		private CT_OnOff activeField;

		private CT_DecimalNumber columnField;

		private byte[] uniqueTagField;

		[XmlElement(Order = 0)]
		public CT_OnOff active
		{
			get
			{
				return activeField;
			}
			set
			{
				activeField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_DecimalNumber column
		{
			get
			{
				return columnField;
			}
			set
			{
				columnField = value;
			}
		}

		[XmlElement(DataType = "base64Binary", Order = 2)]
		public byte[] uniqueTag
		{
			get
			{
				return uniqueTagField;
			}
			set
			{
				uniqueTagField = value;
			}
		}

		public CT_RecipientData()
		{
			columnField = new CT_DecimalNumber();
			activeField = new CT_OnOff();
		}
	}
}
