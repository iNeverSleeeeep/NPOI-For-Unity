using System;

namespace NPOI.OpenXmlFormats.Spreadsheet
{
	public class CT_SharedUser
	{
		private CT_ExtensionList extLstField;

		private string guidField;

		private string nameField;

		private int idField;

		private DateTime dateTimeField;

		public CT_ExtensionList extLst
		{
			get
			{
				return extLstField;
			}
			set
			{
				extLstField = value;
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

		public int id
		{
			get
			{
				return idField;
			}
			set
			{
				idField = value;
			}
		}

		public DateTime dateTime
		{
			get
			{
				return dateTimeField;
			}
			set
			{
				dateTimeField = value;
			}
		}

		public CT_SharedUser()
		{
			extLstField = new CT_ExtensionList();
		}
	}
}
