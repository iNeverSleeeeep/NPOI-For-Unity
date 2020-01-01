using System.ComponentModel;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_RevisionComment
	{
		private uint sheetIdField;

		private string cellField;

		private string guidField;

		private ST_RevisionAction actionField;

		private bool alwaysShowField;

		private bool oldField;

		private bool hiddenRowField;

		private bool hiddenColumnField;

		private string authorField;

		private uint oldLengthField;

		private uint newLengthField;

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

		public string cell
		{
			get
			{
				return cellField;
			}
			set
			{
				cellField = value;
			}
		}

		public string guid
		{
			get
			{
				return guidField;
			}
			set
			{
				guidField = value;
			}
		}

		[DefaultValue(ST_RevisionAction.add)]
		public ST_RevisionAction action
		{
			get
			{
				return actionField;
			}
			set
			{
				actionField = value;
			}
		}

		[DefaultValue(false)]
		public bool alwaysShow
		{
			get
			{
				return alwaysShowField;
			}
			set
			{
				alwaysShowField = value;
			}
		}

		[DefaultValue(false)]
		public bool old
		{
			get
			{
				return oldField;
			}
			set
			{
				oldField = value;
			}
		}

		[DefaultValue(false)]
		public bool hiddenRow
		{
			get
			{
				return hiddenRowField;
			}
			set
			{
				hiddenRowField = value;
			}
		}

		[DefaultValue(false)]
		public bool hiddenColumn
		{
			get
			{
				return hiddenColumnField;
			}
			set
			{
				hiddenColumnField = value;
			}
		}

		public string author
		{
			get
			{
				return authorField;
			}
			set
			{
				authorField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		public uint oldLength
		{
			get
			{
				return oldLengthField;
			}
			set
			{
				oldLengthField = value;
			}
		}

		[DefaultValue(typeof(uint), "0")]
		public uint newLength
		{
			get
			{
				return newLengthField;
			}
			set
			{
				newLengthField = value;
			}
		}

		public CT_RevisionComment()
		{
			actionField = ST_RevisionAction.add;
			alwaysShowField = false;
			oldField = false;
			hiddenRowField = false;
			hiddenColumnField = false;
			oldLengthField = 0u;
			newLengthField = 0u;
		}
	}
}
