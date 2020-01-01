namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_MdxKPI
	{
		private uint nField;

		private uint npField;

		private ST_MdxKPIProperty pField;

		public uint n
		{
			get
			{
				return nField;
			}
			set
			{
				nField = value;
			}
		}

		public uint np
		{
			get
			{
				return npField;
			}
			set
			{
				npField = value;
			}
		}

		public ST_MdxKPIProperty p
		{
			get
			{
				return pField;
			}
			set
			{
				pField = value;
			}
		}
	}
}
