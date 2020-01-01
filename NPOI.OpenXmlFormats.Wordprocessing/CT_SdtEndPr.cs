using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Wordprocessing
{
	[Serializable]
	[XmlType(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main")]
	[XmlRoot(Namespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main", IsNullable = true)]
	public class CT_SdtEndPr
	{
		private List<CT_RPr> itemsField;

		[XmlElement("rPr", Order = 0)]
		public List<CT_RPr> Items
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

		public CT_SdtEndPr()
		{
			itemsField = new List<CT_RPr>();
		}

		public CT_RPr AddNewRPr()
		{
			CT_RPr cT_RPr = new CT_RPr();
			itemsField.Add(cT_RPr);
			return cT_RPr;
		}
	}
}
