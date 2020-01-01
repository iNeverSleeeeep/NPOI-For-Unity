using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_DynamicFilter
	{
		private ST_DynamicFilterType typeField;

		private double valField;

		private bool valFieldSpecified;

		private double maxValField;

		private bool maxValFieldSpecified;

		public ST_DynamicFilterType type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		public double val
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

		[XmlIgnore]
		public bool valSpecified
		{
			get
			{
				return valFieldSpecified;
			}
			set
			{
				valFieldSpecified = value;
			}
		}

		public double maxVal
		{
			get
			{
				return maxValField;
			}
			set
			{
				maxValField = value;
			}
		}

		[XmlIgnore]
		public bool maxValSpecified
		{
			get
			{
				return maxValFieldSpecified;
			}
			set
			{
				maxValFieldSpecified = value;
			}
		}
	}
}
