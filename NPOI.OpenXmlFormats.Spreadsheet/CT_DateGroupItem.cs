using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_DateGroupItem
	{
		private ushort yearField;

		private ushort monthField;

		private bool monthFieldSpecified;

		private ushort dayField;

		private bool dayFieldSpecified;

		private ushort hourField;

		private bool hourFieldSpecified;

		private ushort minuteField;

		private bool minuteFieldSpecified;

		private ushort secondField;

		private bool secondFieldSpecified;

		private ST_DateTimeGrouping dateTimeGroupingField;

		public ushort year
		{
			get
			{
				return yearField;
			}
			set
			{
				yearField = value;
			}
		}

		public ushort month
		{
			get
			{
				return monthField;
			}
			set
			{
				monthField = value;
			}
		}

		[XmlIgnore]
		public bool monthSpecified
		{
			get
			{
				return monthFieldSpecified;
			}
			set
			{
				monthFieldSpecified = value;
			}
		}

		public ushort day
		{
			get
			{
				return dayField;
			}
			set
			{
				dayField = value;
			}
		}

		[XmlIgnore]
		public bool daySpecified
		{
			get
			{
				return dayFieldSpecified;
			}
			set
			{
				dayFieldSpecified = value;
			}
		}

		public ushort hour
		{
			get
			{
				return hourField;
			}
			set
			{
				hourField = value;
			}
		}

		[XmlIgnore]
		public bool hourSpecified
		{
			get
			{
				return hourFieldSpecified;
			}
			set
			{
				hourFieldSpecified = value;
			}
		}

		public ushort minute
		{
			get
			{
				return minuteField;
			}
			set
			{
				minuteField = value;
			}
		}

		[XmlIgnore]
		public bool minuteSpecified
		{
			get
			{
				return minuteFieldSpecified;
			}
			set
			{
				minuteFieldSpecified = value;
			}
		}

		public ushort second
		{
			get
			{
				return secondField;
			}
			set
			{
				secondField = value;
			}
		}

		[XmlIgnore]
		public bool secondSpecified
		{
			get
			{
				return secondFieldSpecified;
			}
			set
			{
				secondFieldSpecified = value;
			}
		}

		public ST_DateTimeGrouping dateTimeGrouping
		{
			get
			{
				return dateTimeGroupingField;
			}
			set
			{
				dateTimeGroupingField = value;
			}
		}
	}
}
