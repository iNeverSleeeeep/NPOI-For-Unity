using System.ComponentModel;
using System.Xml.Serialization;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_ColorFilter
	{
		private uint dxfIdField;

		private bool dxfIdFieldSpecified;

		private bool cellColorField;

		public uint dxfId
		{
			get
			{
				return dxfIdField;
			}
			set
			{
				dxfIdField = value;
			}
		}

		[XmlIgnore]
		public bool dxfIdSpecified
		{
			get
			{
				return dxfIdFieldSpecified;
			}
			set
			{
				dxfIdFieldSpecified = value;
			}
		}

		[DefaultValue(true)]
		public bool cellColor
		{
			get
			{
				return cellColorField;
			}
			set
			{
				cellColorField = value;
			}
		}

		public CT_ColorFilter()
		{
			cellColorField = true;
		}
	}
}
