using System;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class ST_UnsignedshortHex
	{
		private string stringValueField;

		public string StringValue
		{
			get
			{
				return stringValueField;
			}
			set
			{
				stringValueField = value;
			}
		}

		public byte[] ToBytes()
		{
			throw new NotImplementedException();
		}
	}
}
