using System.Collections.Generic;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_Revisions
	{
		private List<object> itemsField;

		public List<object> Items
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

		public CT_Revisions()
		{
			itemsField = new List<object>();
		}
	}
}
