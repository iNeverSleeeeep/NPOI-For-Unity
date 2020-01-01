namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_Mdx
	{
		private object itemField;

		private uint nField;

		private ST_MdxFunctionType fField;

		public object Item
		{
			get
			{
				return itemField;
			}
			set
			{
				itemField = value;
			}
		}

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

		public ST_MdxFunctionType f
		{
			get
			{
				return fField;
			}
			set
			{
				fField = value;
			}
		}
	}
}
