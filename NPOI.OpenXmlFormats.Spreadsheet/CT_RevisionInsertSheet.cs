using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionInsertSheet
	{
		private uint rIdField;

		private bool uaField;

		private bool raField;

		private uint sheetIdField;

		private string nameField;

		private uint sheetPositionField;

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

		public string name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public uint sheetPosition
		{
			get
			{
				return sheetPositionField;
			}
			set
			{
				sheetPositionField = value;
			}
		}

		public CT_RevisionInsertSheet()
		{
			uaField = false;
			raField = false;
		}
	}
}
