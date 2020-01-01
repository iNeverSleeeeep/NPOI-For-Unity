using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_IconFilter
	{
		private ST_IconSetType iconSetField;

		private uint iconIdField;

		private bool iconIdFieldSpecified;

		public ST_IconSetType iconSet
		{
			get
			{
				return iconSetField;
			}
			set
			{
				iconSetField = value;
			}
		}

		public uint iconId
		{
			get
			{
				return iconIdField;
			}
			set
			{
				iconIdField = value;
			}
		}

		[XmlIgnore]
		public bool iconIdSpecified
		{
			get
			{
				return iconIdFieldSpecified;
			}
			set
			{
				iconIdFieldSpecified = value;
			}
		}
	}
}
