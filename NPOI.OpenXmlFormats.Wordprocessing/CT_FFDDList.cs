using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_FFDDList
	{
		private CT_DecimalNumber resultField;

		private CT_DecimalNumber defaultField;

		private List<CT_String> listEntryField;

		[XmlElement(Order = 0)]
		public CT_DecimalNumber result
		{
			get
			{
				return resultField;
			}
			set
			{
				resultField = value;
			}
		}

		[XmlElement(Order = 1)]
		public CT_DecimalNumber @default
		{
			get
			{
				return defaultField;
			}
			set
			{
				defaultField = value;
			}
		}

		[XmlElement("listEntry", Order = 2)]
		public List<CT_String> listEntry
		{
			get
			{
				return listEntryField;
			}
			set
			{
				listEntryField = value;
			}
		}

		public CT_FFDDList()
		{
			listEntryField = new List<CT_String>();
			defaultField = new CT_DecimalNumber();
			resultField = new CT_DecimalNumber();
		}
	}
}
