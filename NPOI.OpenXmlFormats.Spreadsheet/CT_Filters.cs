using System.Collections.Generic;
using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_Filters
	{
		private List<CT_Filter> filterField;

		private List<CT_DateGroupItem> dateGroupItemField;

		private bool blankField;

		private ST_CalendarType calendarTypeField;

		public List<CT_Filter> filter
		{
			get
			{
				return filterField;
			}
			set
			{
				filterField = value;
			}
		}

		public List<CT_DateGroupItem> dateGroupItem
		{
			get
			{
				return dateGroupItemField;
			}
			set
			{
				dateGroupItemField = value;
			}
		}

		[DefaultValue(false)]
		public bool blank
		{
			get
			{
				return blankField;
			}
			set
			{
				blankField = value;
			}
		}

		[DefaultValue(ST_CalendarType.none)]
		public ST_CalendarType calendarType
		{
			get
			{
				return calendarTypeField;
			}
			set
			{
				calendarTypeField = value;
			}
		}

		public CT_Filters()
		{
			dateGroupItemField = new List<CT_DateGroupItem>();
			filterField = new List<CT_Filter>();
			blankField = false;
			calendarTypeField = ST_CalendarType.none;
		}
	}
}
