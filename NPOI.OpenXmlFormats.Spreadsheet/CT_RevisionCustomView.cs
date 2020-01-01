namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionCustomView
	{
		private string guidField;

		private ST_RevisionAction actionField;

		public string guid
		{
			get
			{
				return guidField;
			}
			set
			{
				guidField = value;
			}
		}

		public ST_RevisionAction action
		{
			get
			{
				return actionField;
			}
			set
			{
				actionField = value;
			}
		}
	}
}
