using System.Collections.Generic;
using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_CustomFilters
	{
		private List<CT_CustomFilter> customFilterField;

		private bool andField;

		public List<CT_CustomFilter> customFilter
		{
			get
			{
				return customFilterField;
			}
			set
			{
				customFilterField = value;
			}
		}

		[DefaultValue(false)]
		public bool and
		{
			get
			{
				return andField;
			}
			set
			{
				andField = value;
			}
		}

		public CT_CustomFilters()
		{
			customFilterField = new List<CT_CustomFilter>();
			andField = false;
		}
	}
}
