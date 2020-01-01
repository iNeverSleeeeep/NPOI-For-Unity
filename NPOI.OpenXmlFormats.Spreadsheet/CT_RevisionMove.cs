using System.Collections.Generic;
using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionMove
	{
		private List<object> itemsField;

		private uint rIdField;

		private bool uaField;

		private bool raField;

		private uint sheetIdField;

		private string sourceField;

		private string destinationField;

		private uint sourceSheetIdField;

		public List<object> Items
		{
			get
			{
				return itemsField;
			}
			set
			{
				itemsField = value;
			}
		}

		public uint rId
		{
			get
			{
				return rIdField;
			}
			set
			{
				rIdField = value;
			}
		}

		[DefaultValue(false)]
		public bool ua
		{
			get
			{
				return uaField;
			}
			set
			{
				uaField = value;
			}
		}

		[DefaultValue(false)]
		public bool ra
		{
			get
			{
				return raField;
			}
			set
			{
				raField = value;
			}
		}

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

		public string source
		{
			get
			{
				return sourceField;
			}
			set
			{
				sourceField = value;
			}
		}

		public string destination
		{
			get
			{
				return destinationField;
			}
			set
			{
				destinationField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		public uint sourceSheetId
		{
			get
			{
				return sourceSheetIdField;
			}
			set
			{
				sourceSheetIdField = value;
			}
		}

		public CT_RevisionMove()
		{
			itemsField = new List<object>();
			uaField = false;
			raField = false;
			sourceSheetIdField = 0u;
		}
	}
}
