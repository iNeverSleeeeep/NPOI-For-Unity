namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionQueryTableField
	{
		private uint sheetIdField;

		private string refField;

		private uint fieldIdField;

		public uint sheetId
		{
			get
			{
				return sheetIdField;
			}
			set
			{
				sheetIdField = value;
			}
		}

		public string @ref
		{
			get
			{
				return refField;
			}
			set
			{
				refField = value;
			}
		}

		public uint fieldId
		{
			get
			{
				return fieldIdField;
			}
			set
			{
				fieldIdField = value;
			}
		}
	}
}
