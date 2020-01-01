using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_Top10
	{
		private bool topField;

		private bool percentField;

		private double valField;

		private double filterValField;

		private bool filterValFieldSpecified;

		[DefaultValue(true)]
		public bool top
		{
			get
			{
				return topField;
			}
			set
			{
				topField = value;
			}
		}

		[DefaultValue(false)]
		public bool percent
		{
			get
			{
				return percentField;
			}
			set
			{
				percentField = value;
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

		public double filterVal
		{
			get
			{
				return filterValField;
			}
			set
			{
				filterValField = value;
			}
		}

		[XmlIgnore]
		public bool filterValSpecified
		{
			get
			{
				return filterValFieldSpecified;
			}
			set
			{
				filterValFieldSpecified = value;
			}
		}

		public CT_Top10()
		{
			topField = true;
			percentField = false;
		}
	}
}
