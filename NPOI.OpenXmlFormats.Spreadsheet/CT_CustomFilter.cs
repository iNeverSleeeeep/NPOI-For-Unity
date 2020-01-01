using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_CustomFilter
	{
		private ST_FilterOperator operatorField;

		private string valField;

		[DefaultValue(ST_FilterOperator.equal)]
		public ST_FilterOperator @operator
		{
			get
			{
				return operatorField;
			}
			set
			{
				operatorField = value;
			}
		}

		public string val
		{
			get
			{
				return valField;
			}
			set
			{
				valField = value;
			}
		}

		public CT_CustomFilter()
		{
			operatorField = ST_FilterOperator.equal;
		}
	}
}
